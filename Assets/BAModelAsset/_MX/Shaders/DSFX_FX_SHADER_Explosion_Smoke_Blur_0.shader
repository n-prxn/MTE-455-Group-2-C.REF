Shader "DSFX/FX_SHADER_Explosion_Smoke_Blur_0" {
	Properties {
		[HDR] _RColor ("[R]Color", Vector) = (1,1,1,1)
		_Mutiply ("Mutiply", Float) = 1
		_Tex_Main ("Tex_Main", 2D) = "white" {}
		_Main_Speed_X ("Main_Speed_X", Float) = 0
		_Main_Speed_Y ("Main_Speed_Y", Float) = 0
		[Toggle] _Custom_Data_Main_Offset_Use ("Custom_Data_Main_Offset_Use", Float) = 0
		[Toggle] _Color_Custom_Data_Use ("Color_Custom_Data_Use", Float) = 1
		[HDR] _GColor ("[G]Color", Vector) = (1,0.2989459,0,0)
		[Toggle] _GCustom_Data_Use ("[G]Custom_Data_Use", Float) = 1
		_GColor_Glow_Value ("[G]Color_Glow_Value", Float) = 20
		_GDisappear_Offset ("[G]Disappear_Offset", Range(0, 1)) = 0
		[Toggle] _BCustom_Data_Use ("[B]Custom_Data_Use", Float) = 1
		_BStep_Glow_Value ("[B]Step_Glow_Value", Float) = 20
		_BDisappear_Offset ("[B]Disappear_Offset", Range(0, 1)) = 0
		[Toggle] _Vertex_Alpha_Use ("Vertex_Alpha_Use", Float) = 0
		[Toggle] _Step_Custom_DataVertex_color_Use ("Step_Custom_Data/Vertex_color_Use", Float) = 0
		_Shadow_Color_Multiply ("Shadow_Color_Multiply", Range(0, 1)) = 0.5
		_Shadow_Value ("Shadow_Value", Range(-0.3, 0)) = 0
		_Shadow_Glow_Value ("Shadow_Glow_Value", Float) = 20
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest_Mode ("ZTest_Mode", Float) = 4
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