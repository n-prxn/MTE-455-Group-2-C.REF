Shader "DSFX/FX_SHADER_AlphaBlend_2_Roz" {
	Properties {
		_Texture ("Texture", 2D) = "white" {}
		[HDR] _Color0 ("Color 0", Vector) = (0,0,0,0)
		_Float0 ("Float 0", Float) = 0
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