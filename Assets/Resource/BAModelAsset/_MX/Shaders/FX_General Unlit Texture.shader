Shader "FX/General Unlit Texture" {
	Properties {
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlend ("Src Blend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlend ("Dst Blend", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlendAlpha ("Src Blend Alpha", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlendAlpha ("Dst Blend Alpha", Float) = 0
		[Enum(Off,0, On,1)] [HideInInspector] _ZWrite ("Z Write", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] [HideInInspector] _ZTest ("Z Test", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] [HideInInspector] _Cull ("Cull", Float) = 2
		[HideInInspector] _ZOffsetFactor ("Z Offset Factor", Range(-1, 1)) = 0
		[HideInInspector] _ZOffsetUnits ("Z Offset Units", Range(-1, 1)) = 0
		[HDR] _Color ("Tint Color", Vector) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "white" {}
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