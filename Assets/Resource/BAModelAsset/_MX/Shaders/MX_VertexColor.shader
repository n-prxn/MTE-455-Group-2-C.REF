Shader "MX/VertexColor" {
	Properties {
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlend ("Src Blend", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlend ("Dst Blend", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlendAlpha ("Src Blend Alpha", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlendAlpha ("Dst Blend Alpha", Float) = 10
		[Enum(Off,0, On,1)] [HideInInspector] _ZWrite ("Z Write", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)] [HideInInspector] _Cull ("Cull", Float) = 2
		[HDR] _TintColor ("Main Color", Vector) = (1,1,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "MXFlexibleShaderGUI"
}