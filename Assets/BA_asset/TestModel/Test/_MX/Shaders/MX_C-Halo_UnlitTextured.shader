Shader "MX/C-Halo/UnlitTextured" {
	Properties {
		[HDR] _Tint ("Tint", Vector) = (1,1,1,1)
		[NoScaleOffset] _MainTex ("Main Tex", 2D) = "white" {}
		_GlowMaskColor0 ("Glow Mask Color", Vector) = (1,1,1,1)
		_GlowStrictness0 ("Glow Mask Tolerance", Range(30, 0.34)) = 3
		[HDR] _GlowTint0 ("Glow Tint", Vector) = (0,0,0,1)
		_GlowStrength0 ("Glow Strength", Range(0, 10)) = 0
		[HideInInspector] _DitherThreshold ("_DitherThreshold", Range(0, 1)) = 0
		[HideInInspector] _GrayBrightness ("_GrayBrightness", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlend ("Src Blend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlend ("Dst Blend", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlendAlpha ("Src Blend Alpha", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlendAlpha ("Dst Blend Alpha", Float) = 0
		[Enum(Off,0, On,1)] [HideInInspector] _ZWrite ("Z Write", Float) = 1
		[Enum(UnityEngine.Rendering.CullMode)] [HideInInspector] _Cull ("Cull", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "MXHaloUnlitShaderGUI"
}