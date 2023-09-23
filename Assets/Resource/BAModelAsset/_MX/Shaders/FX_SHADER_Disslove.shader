Shader "FX_SHADER_Disslove" {
	Properties {
		_Maintex ("Maintex", 2D) = "white" {}
		_Mask ("Mask", 2D) = "white" {}
		[HDR] _Maincolor ("Maincolor", Vector) = (0.9433962,0.06674972,0.5951934,0)
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