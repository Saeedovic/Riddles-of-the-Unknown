using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace OD.Effect.HDRP {
    public enum DebugMode{None, ScanMask, ColorMask, Color, FinalColor};
    public enum BlendMode{Ovelay = 1, Aditive = 2, BlackOverlay = 3};

    [System.Serializable]
    public sealed class DebugModeParameter : VolumeParameter<DebugMode>{
        public DebugModeParameter(DebugMode value, bool overrideState = false) : base(value, overrideState) {}
    }

    [System.Serializable]
    public sealed class BlendModeParameter : VolumeParameter<BlendMode>{
        public BlendModeParameter(BlendMode value, bool overrideState = false) : base(value, overrideState) {}
    }


    [System.Serializable, VolumeComponentMenu("Post-processing/OD/ScanEffect")]
    public sealed class ScanEffect : CustomPostProcessVolumeComponent, IPostProcessComponent {
        public static Vector3 scanPos;
        public static float scanRadius;

        public static bool InScanRadius(Vector3 pos){
            if(ScanEffect.scanRadius == 0) return false;

            return Vector3.Distance(pos, ScanEffect.scanPos) <= scanRadius;
        }

        [Tooltip("Eneable/Disable")]
        public BoolParameter enable = new BoolParameter(false);

        [Tooltip("Debug Mode")]
        public DebugModeParameter debugMode = new DebugModeParameter(DebugMode.None);
        [Tooltip("Blend Mode")]
        public BlendModeParameter blendMode = new BlendModeParameter(BlendMode.Ovelay);

        [Header("ScanMask")]
        [Tooltip("The static variables are used by script to animate. Enable this to Debug/Adjust this effect")]
        public BoolParameter useStaticPosAndRadius = new BoolParameter(true);
        [Tooltip("Source of the scan")]
        public Vector3Parameter pos = new Vector3Parameter(Vector3.zero);
        [Tooltip("Scan Radius")]
        public MinFloatParameter radius = new MinFloatParameter(30, 0);
        
        [Tooltip("Scan Width")]
        public MinFloatParameter width = new MinFloatParameter(30, 0);
        [Tooltip("Scan Power")]
        public MinFloatParameter power = new MinFloatParameter(1, 0.1f);
        [Tooltip("Scan Mask Intensity")]
        public MinFloatParameter scanMaskIntensity = new MinFloatParameter(1, 0);

        [Header("ColorMask")]
        [Tooltip("Use Flat Color Mask")]
        public BoolParameter useFlatColorMask = new BoolParameter(true);
        [Tooltip("Flat Color Mask Power")]
        public MinFloatParameter flatColorMaskPower = new MinFloatParameter(1, 0.1f);
        [Tooltip("Flat Color Mask Intensity")]
        public MinFloatParameter flatColorMaskIntensity = new MinFloatParameter(1, 0);
        [Tooltip("Use Fresnel Mask")]
        public BoolParameter useFresnelMask = new BoolParameter(false);
        [Tooltip("Fresnel Mask Power")]
        public MinFloatParameter fresnelMaskPower = new MinFloatParameter(3, 0.1f);
        [Tooltip("Use Sobel Mask")]
        public BoolParameter useSobelMask = new BoolParameter(false);

        [Tooltip("Use Texture Overlay Mask")]
        public BoolParameter useTextureOverlayMask = new BoolParameter(false);
        [Tooltip("The texture Overlay Mask need to be black white")]
        public TextureParameter textureOverlayMask = new TextureParameter(null);
        [Tooltip("Texture Overlay Mask Scale")]
        public FloatParameter textureOverlayMaskScale = new FloatParameter(.1f);
        [Tooltip("if this is less than 0 the texture lod is disabled. This can be used to avoid aliasing")]
        public FloatParameter textureOverlayMaskLod = new FloatParameter(-1);

        [Header("Color")]
        [Tooltip("Main Color")]
        public ColorParameter mainColor = new ColorParameter(Color.white);
        [Tooltip("Color Intensity")]
        public MinFloatParameter colorIntensity = new MinFloatParameter(1, 0);

        [Tooltip("Use Gradient Color")]
        public BoolParameter useGradientColor = new BoolParameter(false);
        [Tooltip("Middle Color")]
        public ColorParameter middleColor = new ColorParameter(Color.white);
        [Tooltip("Trail Color")]
        public ColorParameter trailColor = new ColorParameter(Color.white);
        [Tooltip("Gradient Falloff")]
        public FloatParameter gradientFalloff = new FloatParameter(1);

        [Tooltip("Use Horizontal Bar")]
        public BoolParameter useHorizontalBar = new BoolParameter(false);
        [Tooltip("Horizontal Bar Color")]
        public ColorParameter horizontalBarColor = new ColorParameter(Color.white);
        [Tooltip("Horizontal Bar Scale")]
        public FloatParameter horizontalBarScale = new FloatParameter(100);
        [Tooltip("Horizontal Bar Size")]
        public FloatParameter horizontalBarSize = new FloatParameter(2);

        Material _material;

        public bool IsActive() => _material != null && enable.value == true;

        public override CustomPostProcessInjectionPoint injectionPoint =>
            CustomPostProcessInjectionPoint.BeforePostProcess;

        public override void Setup(){
            _material = CoreUtils.CreateEngineMaterial("Hidden/OD/PostProcess/ScanEffect");
        }

        public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle srcRT, RTHandle destRT){
            if (_material == null) return;

            _material.SetFloat("_debugMode", (int)debugMode.value);
            _material.SetFloat("_blendMode", (int)blendMode.value);

            _material.SetVector("_pos", useStaticPosAndRadius.value ? scanPos : pos.value);
            _material.SetFloat("_radius", useStaticPosAndRadius.value ? scanRadius : radius.value);

            _material.SetFloat("_width", width.value);
            _material.SetFloat("_power", power.value);
            _material.SetFloat("_scanMaskIntensity", scanMaskIntensity.value);

            _material.SetFloat("_useFlatColorMask", useFlatColorMask.value ? 1 : 0);
            _material.SetFloat("_flatColorMaskPower", flatColorMaskPower.value);
            _material.SetFloat("_flatColorMaskIntensity", flatColorMaskIntensity.value);
            _material.SetFloat("_useFresnellMask", useFresnelMask.value ? 1 : 0);
            _material.SetFloat("_fresnelMaskPower", fresnelMaskPower.value);
            _material.SetFloat("_useSobelMask", useSobelMask.value ? 1 : 0);

            _material.SetFloat("_useTextureOverlayMask", useTextureOverlayMask.value ? 1 : 0);
            _material.SetTexture("_textureOverlayMask", textureOverlayMask.value);
            _material.SetFloat("_textureOverlayMaskScale", textureOverlayMaskScale.value);
            _material.SetFloat("_textureOverlayMaskLod", textureOverlayMaskLod.value);

            _material.SetFloat("_colorIntensity", colorIntensity.value);
            _material.SetVector("_mainColor", mainColor.value);

            _material.SetFloat("_useGradientColor", useGradientColor.value ? 1 : 0);
            _material.SetVector("_middleColor", middleColor.value);
            _material.SetVector("_trailColor", trailColor.value);
            _material.SetFloat("_gradientFalloff", gradientFalloff.value);


            _material.SetFloat("_useHorizontalBar", useHorizontalBar.value ? 1 : 0);
            _material.SetVector("_horizontalBarColor", horizontalBarColor.value);
            _material.SetFloat("_horizontalBarScale", horizontalBarScale.value);
            _material.SetFloat("_horizontalBarSize", horizontalBarSize.value);
            
            _material.SetTexture("_InputTexture", srcRT);


            HDUtils.DrawFullScreen(cmd, _material, destRT);
        }

        public override void Cleanup(){
            CoreUtils.Destroy(_material);
        }
    }
}
