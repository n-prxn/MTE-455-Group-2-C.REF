Shader "DSFX/FX_SHADER_Multiplicative_0" {
	Properties {
		[HDR] _Color ("Color", Vector) = (1,1,1,1)
		_Multiply ("Multiply", Float) = 1
		_Tex_Main ("Tex_Main", 2D) = "white" {}
		[Toggle] _RGBRGBA ("RGB>RGBA", Float) = 1
		[Toggle] _Main_Texture_No ("Main_Texture_No", Float) = 0
		[Toggle] _Custom_Data_MainMask_Offset_Use ("Custom_Data_Main/Mask_Offset_Use", Float) = 0
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest_Mode ("ZTest_Mode", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode ("Cull_Mode", Float) = 2
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