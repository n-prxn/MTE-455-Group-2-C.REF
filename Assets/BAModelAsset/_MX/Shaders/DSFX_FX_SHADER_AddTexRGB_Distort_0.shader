Shader "DSFX/FX_SHADER_AddTexRGB_Distort_0" {
	Properties {
		[HDR] _Color ("Color", Vector) = (1,1,1,1)
		_Main_Multiply ("Main_Multiply", Float) = 1
		_Tex_Main ("Tex_Main", 2D) = "white" {}
		[Toggle] _Main_Distortion_Use ("Main_Distortion_Use", Float) = 1
		_Main_Speed_X ("Main_Speed_X", Float) = 0
		_Main_Speed_Y ("Main_Speed_Y", Float) = 0
		[Toggle] _Custom_Data_MainMask_Offset_Use ("Custom_Data_Main/Mask_Offset_Use", Float) = 0
		[HDR] _Add_Color ("Add_Color", Vector) = (1,0,0,1)
		_Add_Multiply ("Add_Multiply", Float) = 0.39
		_Tex_AddRGB ("Tex_Add[RGB]", 2D) = "white" {}
		[Toggle] _RAdd_Distortion_Use ("[R]Add_Distortion_Use", Float) = 1
		[Toggle] _GAdd_Distortion_Use ("[G]Add_Distortion_Use", Float) = 0
		[Toggle] _BAdd_Distortion_Use ("[B]Add_Distortion_Use", Float) = 0
		[Toggle] _Add_Distortion_Use ("Add_Distortion_Use", Float) = 1
		_Add_Speed_X ("Add_Speed_X", Float) = -1
		_Add_Speed_Y ("Add_Speed_Y", Float) = 0
		_Tex_Distort ("Tex_Distort", 2D) = "white" {}
		_Dis_Speed_X ("Dis_Speed_X", Float) = 0
		_Dis_Speed_Y ("Dis_Speed_Y", Float) = 0
		_Distortion_Power_X ("Distortion_Power_X", Float) = 0
		_Distortion_Power_Y ("Distortion_Power_Y", Float) = 0
		[Toggle] _UV_Add_Use ("UV_Add_Use", Float) = 0
		[Toggle] _Custom_Data_Distort_Power_Use ("Custom_Data_Distort_Power_Use", Float) = 0
		_UV_Add_TilingOffset ("UV_Add_Tiling/Offset", Vector) = (0,0,0,0)
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