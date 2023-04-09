Shader "Hidden/OD/PostProcess/ScanEffect" {
    HLSLINCLUDE

    #pragma target 4.5
    #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/AreaLighting.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/NormalBuffer.hlsl"

    #include "ScanEffectFunction.hlsl"

    struct Attributes{
        uint vertexID : SV_VertexID;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct Varyings{
        float4 positionCS : SV_POSITION;
        float2 texcoord   : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    Varyings Vert(Attributes input){
        Varyings output;
        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
        output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
        output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
        return output;
    }

    
    TEXTURE2D_X(_InputTexture);

    float _debugMode;
    float _blendMode;

    float3 _pos;
    float _radius;
    float _width;
    float _power;
    float _scanMaskIntensity;

    //----------- ColorMask -----------

    float _useFlatColorMask;
    float _flatColorMaskPower;
    float _flatColorMaskIntensity;

    float _useFresnellMask;
    float _fresnelMaskPower;

    float _useSobelMask;

    float _useTextureOverlayMask;
    TEXTURE2D(_textureOverlayMask);
    float _textureOverlayMaskScale;
    float _textureOverlayMaskLod;

    //------------ Color ----------------

    float4 _mainColor;
    float _colorIntensity;

    float _useGradientColor;
    float4 _middleColor;
    float4 _trailColor;
    float _gradientFalloff;

    float _useHorizontalBar;
    float4 _horizontalBarColor;
    float _horizontalBarScale;
    float _horizontalBarSize;

    inline float Linear01Depth( float z ){
        return 1.0 / (_ZBufferParams.x * z + _ZBufferParams.y);
    }

    inline float3 WorldSpaceViewDir(float3 pos){
        return _WorldSpaceCameraPos.xyz - pos;
    }

    float4 Lerp3(float4 a, float4 b, float4 c, float t){
        //if(t >= 1) return c;
        //if(t <= 0) return a;

        if (t < 0.5f)
            return lerp(a, b, t / 0.5f);

        return lerp(b, c, (t - 0.5f) / 0.5f);
    }

    float4 GetScanColor(float mask, float y){
        float4 scanCol = _mainColor;

        if(_useGradientColor > 0){
            //scanCol = Lerp3(_TrailColor, _MiddleColor, _MainColor, mask);
            scanCol = Lerp3(_trailColor, _middleColor, _mainColor, pow(mask, _gradientFalloff));
        }

        if(_useHorizontalBar > 0){
            scanCol += horizontalBars(y, _horizontalBarScale, _horizontalBarSize) * _horizontalBarColor;
        }
        return scanCol;
    }

    float4 ColorSobel(float2 uv, float2 TexelSize){
        float x = 0;
        float y = 0;

        float2 texelSize = TexelSize;

        x += LOAD_TEXTURE2D_X(_InputTexture, uv + float2(-texelSize.x, -texelSize.y)) * -1.0;
        x += LOAD_TEXTURE2D_X(_InputTexture, uv + float2(-texelSize.x,            0)) * -2.0;
        x += LOAD_TEXTURE2D_X(_InputTexture, uv + float2(-texelSize.x,  texelSize.y)) * -1.0;

        x += LOAD_TEXTURE2D_X(_InputTexture, uv + float2( texelSize.x, -texelSize.y)) *  1.0;
        x += LOAD_TEXTURE2D_X(_InputTexture, uv + float2( texelSize.x,            0)) *  2.0;
        x += LOAD_TEXTURE2D_X(_InputTexture, uv + float2( texelSize.x,  texelSize.y)) *  1.0;

        y += LOAD_TEXTURE2D_X(_InputTexture, uv + float2(-texelSize.x, -texelSize.y)) * -1.0;
        y += LOAD_TEXTURE2D_X(_InputTexture, uv + float2(           0, -texelSize.y)) * -2.0;
        y += LOAD_TEXTURE2D_X(_InputTexture, uv + float2( texelSize.x, -texelSize.y)) * -1.0;

        y += LOAD_TEXTURE2D_X(_InputTexture, uv + float2(-texelSize.x,  texelSize.y)) *  1.0;
        y += LOAD_TEXTURE2D_X(_InputTexture, uv + float2(           0,  texelSize.y)) *  2.0;
        y += LOAD_TEXTURE2D_X(_InputTexture, uv + float2( texelSize.x,  texelSize.y)) *  1.0;

        return sqrt(x * x + y * y);
    }

    float4 CustomPostProcess(Varyings input) : SV_Target{
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        //-------------------- General ---------------------
        uint2 positionSS = input.texcoord * _ScreenSize.xy;
        float4 sceneColor = LOAD_TEXTURE2D_X(_InputTexture, positionSS);

        NormalData normalData;
        DecodeFromNormalBuffer(input.positionCS.xy, normalData);
        float3 normal = normalData.normalWS;

        float depth = LoadCameraDepth(input.positionCS.xy);
        PositionInputs posInput = GetPositionInput(input.positionCS.xy, _ScreenSize.zw, depth, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);
        float3 wp = GetAbsolutePositionWS(posInput.positionWS);

        //-------------------- Scan Mask ---------------------

        float Dis = distance(float4(_pos.xyz,1), float4(wp.xyz,1));
        float scanMask = ScanMask(Dis, _radius, _width);
        scanMask = saturate(pow(scanMask * _scanMaskIntensity, _power));


        //-------------------- Colors Mask ---------------------
        float4 colorSobel = ColorSobel(positionSS, _ScreenSize.xy * 0.001) * _useSobelMask;
        float4 patter = MultitextureTriplanar(_textureOverlayMask,_textureOverlayMask,_textureOverlayMask,s_trilinear_repeat_sampler, wp, normal, _textureOverlayMaskScale, 1)
            * _useTextureOverlayMask;
        if(_textureOverlayMaskLod > 0)
            patter = MultitextureTriplanarLod(_textureOverlayMask,_textureOverlayMask,_textureOverlayMask,s_trilinear_repeat_sampler, wp, normal, _textureOverlayMaskScale, 1, _textureOverlayMaskLod)
            * _useTextureOverlayMask;

        float fresnel = FresnelEffect(normal, GetWorldSpaceNormalizeViewDir(posInput.positionWS), _fresnelMaskPower) * _useFresnellMask;
        float flatColor = saturate(pow(scanMask * _flatColorMaskIntensity, _flatColorMaskPower)) * _useFlatColorMask;

        float finalColorMask = saturate(fresnel + colorSobel.x + patter.r + flatColor) * _colorIntensity;

        //-------------------- Colors ---------------------
        float4 scanColor = GetScanColor(scanMask, input.texcoord.y) * _colorIntensity;

        float4 finalColor = float4(finalColorMask.xxx,1) * scanColor;
        float4 toBlendColor = finalColor;

        if(_debugMode == 1) return float4(scanMask.xxx, 1);
        if(_debugMode == 2) return float4(finalColorMask.xxx,1);
        if(_debugMode == 3) return scanColor;
        if(_debugMode == 4) return finalColor;
        
        if(Linear01Depth (depth) > 0.95) return sceneColor;

        if(_blendMode == 1) toBlendColor = lerp(sceneColor, finalColor, finalColorMask);
        if(_blendMode == 2) toBlendColor = sceneColor + finalColor;
        if(_blendMode == 3) toBlendColor = finalColor;

        return lerp(sceneColor, toBlendColor, scanMask);
    }

    ENDHLSL

    SubShader{
        Pass{
            Name "ScanEffect"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
                #pragma fragment CustomPostProcess
                #pragma vertex Vert
            ENDHLSL
        }
    }
    Fallback Off
}
