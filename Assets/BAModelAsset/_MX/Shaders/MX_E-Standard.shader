Shader "MX/E-Standard" {
	Properties {
		_Cutoff ("Alpha Clipping", Range(0, 1)) = 0.5
		_PrefabLightmapTex ("_PrefabLightmapTex", 2D) = "gray" {}
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlend ("Src Blend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlend ("Dst Blend", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlendAlpha ("Src Blend Alpha", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlendAlpha ("Dst Blend Alpha", Float) = 0
		[Enum(Off,0, On,1)] [HideInInspector] _ZWrite ("Z Write", Float) = 1
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
		_ZOffsetFactor ("Z Offset Factor", Range(-1, 1)) = 0
		_ZOffsetUnits ("Z Offset Units", Range(-1, 1)) = 0
		[HDR] _Color ("Tint", Vector) = (1,1,1,1)
		_MainTex ("MainTex (RGB) / ?? (A)", 2D) = "white" {}
		_ReflectTex ("Fake Reflection (RGB)", 2D) = "white" {}
		_ReflectBaseAmount ("Reflect Base Amount", Range(0, 1)) = 0
		_ReflectAnglePower ("Reflect Angle", Range(1, 8)) = 2
		_ShadowAttenRefl ("Apply Shadows", Range(0, 1)) = 0
		_ReflectStrength ("Reflect Strength", Range(0, 10)) = 0
		[NoScaleOffset] _EmissionTex ("Emission Color (RGB)", 2D) = "white" {}
		_EmissionStrength ("Emission Strength", Range(0, 50)) = 0
		[NoScaleOffset] _SpecTex ("Spec Color (RGB)", 2D) = "white" {}
		_SpecLightDir ("Spec Light Dir", Vector) = (0,0.94,0.342,0)
		[HDR] _SpecLightColor ("Spec Light Color", Vector) = (1,1,1,1)
		_SpecPower ("Spec Power", Range(1, 512)) = 1
		_ShadowAttenSpec ("Apply Shadows", Range(0, 0.5)) = 0.2
		[HideInInspector] _CodeAddColor ("_CodeAddColor", Vector) = (0,0,0,0)
		[HideInInspector] _CodeMultiplyColor ("_CodeMultiplyColor", Vector) = (1,1,1,1)
		[HideInInspector] _CodeAddRimColor ("_CodeAddRimColor", Vector) = (0,0,0,0)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "MXEnvStandardShaderGUI"
}