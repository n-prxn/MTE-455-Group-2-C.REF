Shader "DSFX/FX_SHADER_Matcap" {
	Properties {
		[HDR] _Main_Color ("Main_Color", Vector) = (0,0,0,0)
		_Main_Tex ("Main_Tex", 2D) = "white" {}
		_Matcap_Tex ("Matcap_Tex", 2D) = "white" {}
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 1
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode ("Cull_Mode", Float) = 2
		[HideInInspector] _texcoord ("", 2D) = "white" {}
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
	//CustomEditor "ASEMaterialInspector"
}