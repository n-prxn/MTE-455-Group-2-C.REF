Shader "DSFX/FX_SHADER_Two_Step_Distort_0" {
	Properties {
		[HDR] _Color ("Color", Vector) = (1,0.9456895,0,0)
		_Multiply ("Multiply", Float) = 1
		_Multiply_G ("Multiply_[G]", Float) = 1
		_Tex_MainRGB ("Tex_Main[R,G,B]", 2D) = "white" {}
		[Toggle] _Custom_Data_Main_Offset_Use ("Custom_Data_Main_Offset_Use", Float) = 1
		_Main_Speed_X ("Main_Speed_X", Float) = 0
		_Main_Speed_Y ("Main_Speed_Y", Float) = 0
		[Toggle] _Step_Scroll_Use ("Step_Scroll_Use", Float) = 1
		_Step_Value ("Step_Value", Range(0, 1)) = 0.6019696
		_Tex_Distort ("Tex_Distort", 2D) = "white" {}
		_Dis_Speed_X ("Dis_Speed_X", Float) = 0
		_Dis_Speed_Y ("Dis_Speed_Y", Float) = 0
		_Dis_Power_X ("Dis_Power_X", Float) = 0
		_Dis_Power_Y ("Dis_Power_Y", Float) = 0
		[Toggle] _Custom_Data_Distort_Power_Use ("Custom_Data_Distort_Power_Use", Float) = 1
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 1
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode ("Cull_Mode", Float) = 0
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