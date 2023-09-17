Shader "DSFX/FX_SHADER_Step_Shadow_Distortion_0" {
	Properties {
		_Multiply ("Multiply", Float) = 1
		_Tex_Main ("Tex_Main", 2D) = "white" {}
		_Main_Speed_X ("Main_Speed_X", Float) = 0
		_Main_Speed_Y ("Main_Speed_Y", Float) = 0
		[Toggle] _Custom_Data_Main_Offset_Use ("Custom_Data_Main_Offset_Use", Float) = 0
		[Toggle] _Step_Scroll_Use ("Step_Scroll_Use", Float) = 1
		_Step_Value ("Step_Value", Range(0, 1)) = 0.23
		[Toggle] _Step_Custom_DataVertex_color_Use ("Step_Custom_Data/Vertex_color_Use", Float) = 1
		[Toggle] _Vertex_Alpha_Use ("Vertex_Alpha_Use", Float) = 0
		_Shadow_Value ("Shadow_Value", Float) = 0.95
		_Shadow_Color_Value ("Shadow_Color_Value", Float) = 1
		_Tex_Distort ("Tex_Distort", 2D) = "white" {}
		_Dis_Speed_X ("Dis_Speed_X", Float) = 0
		_Dis_Speed_Y ("Dis_Speed_Y", Float) = 0
		_Distortion_Power_X ("Distortion_Power_X", Float) = 0
		_Distortion_Power_Y ("Distortion_Power_Y", Float) = 0
		[Toggle] _Custom_Data_Distort_Power_Use ("Custom_Data_Distort_Power_Use", Float) = 0
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode ("Cull_Mode", Float) = 2
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