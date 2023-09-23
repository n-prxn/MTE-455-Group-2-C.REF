Shader "MX/E-FloorReflectedMesh" {
	Properties {
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlend ("Src Blend", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlend ("Dst Blend", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlendAlpha ("Src Blend Alpha", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlendAlpha ("Dst Blend Alpha", Float) = 10
		[HDR] _Color ("Tint Color", Vector) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "white" {}
		_FadeDistance ("Fade Distance", Float) = 0
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