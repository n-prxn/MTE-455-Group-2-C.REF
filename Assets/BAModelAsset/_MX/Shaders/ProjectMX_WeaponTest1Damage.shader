Shader "ProjectMX/WeaponTest1Damage" {
	Properties {
		[Toggle(_DAMAGE_0)] _DamageON ("Damage Check", Range(0, 1)) = 0
		_Color ("Color", Vector) = (1,1,1,1)
		_mainTex ("Main Tex", 2D) = "white" {}
		_sourceTex ("Source Tex", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		[Header(Damage and Fire)] _CrushScale ("CrushNoiseScale", Range(0, 1)) = 0
		_NoiseDir ("Noise Direction", Vector) = (0,0,0,0)
		_DmgCol ("Damage Color", Vector) = (0.57,0.56,0.56,0)
		_NoiseColStrong ("Damage ColorPow", Float) = 2
		_Damage ("Damage", Range(0, 1)) = 0
		[HDR] _FireCol ("Fire Color", Vector) = (0.91,0.74,0,0)
		_FireBackCol_Str ("Fire BackColor Strength", Range(0, 0.2)) = 0.1
		_FireValue ("Fire Light Threshold", Range(0, 2)) = 1
		_Fire ("Fire", Range(0, 1)) = 0
		[Header (Base Option)] _ShadowThreshold ("Shadow Threshold", Range(0, 1)) = 0.5
		_ShadowStrong ("Shadow Strength", Float) = 30
		_LightValue ("Light Threshold", Range(0, 1)) = 0.5
		_LightStrong ("Light Strength", Float) = 20
		_SpecStrong ("Spec Color Strength", Range(0, 1)) = 0.1
		_ShadowTint ("Shadow Tint", Vector) = (0,0,0,0)
		_SpecColor ("Spec Tint", Vector) = (0,0,0,0)
		_FakeLightDir ("Light Direction", Vector) = (0.1,0.65,0,0)
		[Space(10)] [Header(Glow Option)] [Toggle(_GLOW_0)] _UseGlow ("use glow?", Float) = 0
		_GlowMaskColor0 ("Glow Mask Color", Vector) = (1,1,1,1)
		_GlowStrictness0 ("Glow Mask Tolerance", Range(30, 0.34)) = 3
		[HDR] _GlowTint0 ("Glow Tint", Vector) = (0,0,0,1)
		_GlowStrength0 ("Glow Strength", Range(0, 10)) = 0
		[Header(OUTLINE)] _OutlineTint ("Outline Tint", Vector) = (0.5,0.5,0.5,1)
		[HideInInspector] _CodeAddColor ("_CodeAddColor", Vector) = (0,0,0,0)
		[HideInInspector] _CodeMultiplyColor ("_CodeMultiplyColor", Vector) = (1,1,1,1)
		[HideInInspector] _CodeAddRimColor ("_CodeAddRimColor", Vector) = (0,0,0,0)
		[HideInInspector] _GrayBrightness ("_GrayBrightness", Float) = 1
		[Space(10)] [Toggle(_DITHER_HORIZONTAL_LINES)] _IsDither ("_DITHER_HORIZONTAL_LINES", Float) = 0
		_DitherThreshold ("_DitherThreshold", Range(0, 1)) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
}