Shader "DSFX/FX_SHADER_AlphaBlend_Add" {
	Properties {
		[HDR] _Color ("Color", Vector) = (1,1,1,1)
		_Multiply ("Multiply", Float) = 1
		_Texture ("Texture", 2D) = "white" {}
		[Toggle] _RGBRGBA ("RGB>RGBA", Float) = 0
		[Toggle] _Main_Texture_No ("Main_Texture_No", Float) = 0
		[Toggle] _Custom_Data_Offset_Use ("Custom_Data_Offset_Use", Float) = 0
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode ("Cull_Mode", Float) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest_Mode ("ZTest_Mode", Float) = 4
		_ZOffsetFactor ("ZOffsetFactor", Float) = 0
		_ZOffsetUnits ("ZOffsetUnits", Float) = 0
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