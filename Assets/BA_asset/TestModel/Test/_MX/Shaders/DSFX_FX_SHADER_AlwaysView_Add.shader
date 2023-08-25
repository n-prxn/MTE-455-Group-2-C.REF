Shader "DSFX/FX_SHADER_AlwaysView_Add" {
	Properties {
		_FX_TEX_FocusLine ("FX_TEX_FocusLine", 2D) = "white" {}
		_TextureSample0 ("Texture Sample 0", 2D) = "white" {}
		[HDR] _Color ("Color", Vector) = (1,1,1,1)
		[HideInInspector] _texcoord ("", 2D) = "white" {}
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
	//CustomEditor "ASEMaterialInspector"
}