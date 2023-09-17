Shader "MX/E-Obstacle" {
	Properties {
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlend ("Src Blend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlend ("Dst Blend", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlendAlpha ("Src Blend Alpha", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlendAlpha ("Dst Blend Alpha", Float) = 0
		[Enum(Off,0, On,1)] [HideInInspector] _ZWrite ("Z Write", Float) = 1
		[Enum(UnityEngine.Rendering.CullMode)] [HideInInspector] _Cull ("Cull", Float) = 2
		[Toggle(_DYNAMIC_LIGHTS)] _RealTimeLightMap ("Realtime Light Mode", Float) = 1
		[HDR] _Color ("Tint", Vector) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		[NoScaleOffset] _SpecTex ("Spec Color (RGB)", 2D) = "white" {}
		[HDR] _MaskColor ("Mask Color", Vector) = (1,1,1,1)
		[MaterialToggle] _BlendValue ("Blend On/Off", Float) = 0
		_BlendTex ("Blend Texture", 2D) = "white" {}
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
	//CustomEditor "MXFlexibleShaderGUI"
}