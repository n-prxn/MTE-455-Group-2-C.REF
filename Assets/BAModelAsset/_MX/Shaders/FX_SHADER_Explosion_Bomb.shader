Shader "FX_SHADER_Explosion_Bomb" {
	Properties {
		[HDR] _Smoke_color ("Smoke_color", Vector) = (0.7264151,0.448312,0.2706924,0)
		[HDR] _Outline_color ("Outline_color", Vector) = (0.490566,0.4636559,0.4466002,0)
		_last_color ("last_color", Vector) = (0,0,0,0)
		_Tex_u ("Tex_u", Float) = 0
		_Tex_v ("Tex_v", Float) = 0
		_Diffuse ("Diffuse", 2D) = "white" {}
		_second_tex ("second_tex", 2D) = "white" {}
		_Step ("Step", 2D) = "white" {}
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