#ifndef SCANEFFECT_FUNCTIONS_INCLUDED
#define SCANEFFECT_FUNCTIONS_INCLUDED


float4 horizontalBars(float y, float scale, float size){
    return 1 - saturate(round(abs(frac(y * scale) * size)));
}

float4 MultitextureTriplanar(Texture2D TextureA, Texture2D TextureB, Texture2D TextureC, SamplerState Sampler, float3 Position, float3 Normal, float Tile, float Blend){
    float3 Node_UV = Position * Tile;
    float3 Node_Blend = pow(abs(Normal), Blend);
    Node_Blend /= dot(Node_Blend, 1.0);
    float4 Node_X = SAMPLE_TEXTURE2D(TextureA, Sampler, Node_UV.zy);
    float4 Node_Y = SAMPLE_TEXTURE2D(TextureB, Sampler, Node_UV.xz);
    float4 Node_Z = SAMPLE_TEXTURE2D(TextureC, Sampler, Node_UV.xy);
    return Node_X * Node_Blend.x + Node_Y * Node_Blend.y + Node_Z * Node_Blend.z;
}

float4 MultitextureTriplanarLod(Texture2D TextureA, Texture2D TextureB, Texture2D TextureC, SamplerState Sampler, float3 Position, float3 Normal, float Tile, float Blend, float Lod){
    float3 Node_UV = Position * Tile;
    float3 Node_Blend = pow(abs(Normal), Blend);
    Node_Blend /= dot(Node_Blend, 1.0);
    float4 Node_X = SAMPLE_TEXTURE2D_X_LOD(TextureA, Sampler, Node_UV.zy, Lod);
    float4 Node_Y = SAMPLE_TEXTURE2D_X_LOD(TextureB, Sampler, Node_UV.xz, Lod);
    float4 Node_Z = SAMPLE_TEXTURE2D_X_LOD(TextureC, Sampler, Node_UV.xy, Lod);
    return Node_X * Node_Blend.x + Node_Y * Node_Blend.y + Node_Z * Node_Blend.z;
}

float ScanMask(float Dis, float ScanDis, float ScanWidth){
    float scanCol = 0;
    if(Dis < ScanDis && Dis > ScanDis - ScanWidth){
        float diff = 1- (ScanDis - Dis) / ScanWidth;
        scanCol = diff;
    }

    return scanCol;
}

float FresnelEffect(float3 Normal, float3 ViewDir, float Power){
    return pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
}
#endif