Shader "DSFX/FX_SHADER_Water_Distortion_0" {
	Properties {
		[HDR] _MainColor ("MainColor", Vector) = (1,1,1,1)
		_Mutiply ("Mutiply", Float) = 1
		_Main_TexRGBA ("Main_Tex (RGBA)", 2D) = "white" {}
		[Toggle] _Custom_Data_MainRB_Offset_Use ("Custom_Data_Main[R,B]_Offset_Use", Float) = 0
		_Main_Speed_X ("Main_Speed_X", Float) = 0
		_Main_Speed_Y ("Main_Speed_Y", Float) = 0
		[HDR] _SubColor ("SubColor", Vector) = (2,2,2,1)
		_Sub_Tex ("Sub_Tex", 2D) = "white" {}
		_Sub_Speed_X ("Sub_Speed_X", Float) = 0
		_Sub_Speed_Y ("Sub_Speed_Y", Float) = 0
		[Toggle] _GCustom_Data_Use ("[G]Custom_Data_Use", Float) = 1
		_GColor_Sharpness ("[G]Color_Sharpness", Float) = 20
		_GDisappear_Offset ("[G]Disappear_Offset", Range(0, 1)) = 0
		[Toggle] _BCustom_Data_Use ("[B]Custom_Data_Use", Float) = 1
		_BDisappear_Offset ("[B]Disappear_Offset", Range(0, 1)) = 0
		[Toggle] _Custom_Data_Shadow_Value_Use ("Custom_Data_Shadow_Value_Use", Float) = 1
		_Shadow_Value ("Shadow_Value", Float) = -0.55
		_Tex_Distort ("Tex_Distort", 2D) = "white" {}
		_Dis_Speed_X ("Dis_Speed_X", Float) = 0
		_Dis_Speed_Y ("Dis_Speed_Y", Float) = 0
		_Distortion_Power_X ("Distortion_Power_X", Float) = 0
		_Distortion_Power_Y ("Distortion_Power_Y", Float) = 0
		[Toggle] _Custom_Data_Distort_Power_Offset_Use ("Custom_Data_Distort_Power_Offset_Use", Float) = 0
		[Toggle] _UV_Add_Use ("UV_Add_Use", Float) = 0
		_UV_Add_TilingOffset ("UV_Add_Tiling/Offset", Vector) = (0,0,0,0)
		_AlphaClip_Value ("AlphaClip_Value", Float) = 0
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode ("Cull_Mode", Float) = 2
		_ZOffsetFactor ("ZOffsetFactor", Float) = 0
		_ZOffsetUnits ("ZOffsetUnits", Float) = 0
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