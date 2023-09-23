Shader "DSFX/FX_SHADER_AlwaysView_AlphaBlend_0" {
	Properties {
		[HDR] _Color1 ("Color", Vector) = (1,1,1,1)
		_Multiply1 ("Multiply", Float) = 1
		_Texture1 ("Texture", 2D) = "white" {}
		[Toggle] _RGBRGBA1 ("RGB>RGBA", Float) = 1
		[Toggle] _Main_Texture_No1 ("Main_Texture_No", Float) = 0
		[Toggle] _Custom_Data_Offset_Use1 ("Custom_Data_Offset_Use", Float) = 1
		[Toggle] _ZWrite_Mode1 ("ZWrite_Mode", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode1 ("Cull_Mode", Float) = 2
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