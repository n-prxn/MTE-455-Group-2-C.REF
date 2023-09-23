Shader "DSFX/FX_SHADER_Smoke_Scroll_0" {
	Properties {
		[HDR] _Color ("Color", Vector) = (1,1,1,1)
		_Multiply ("Multiply", Float) = 1
		_Tex_Main ("Tex_Main", 2D) = "white" {}
		_Main_Speed_X ("Main_Speed_X", Float) = -0.12
		_Main_Speed_Y ("Main_Speed_Y", Float) = 0
		_Shadow_Color_Value ("Shadow_Color_Value", Float) = 0.5
		_Shadow_Value ("Shadow_Value", Range(-0.3, 0)) = -0.3
		[HDR] _Color2 ("Color2", Vector) = (1,1,1,0)
		[Toggle] _Color2_Scroll_Use ("Color2_Scroll_Use", Float) = 0
		_Color2_Offset ("Color2_Offset", Range(-1, 1)) = 0.5035898
		[Toggle] _Step_Scroll_Use ("Step_Scroll_Use", Float) = 0
		_Step_Power ("Step_Power", Range(0, 2)) = 0.1484019
		_Step_Scale ("Step_Scale", Float) = 0
		_Step_Offset ("Step_Offset", Float) = 0
		_Tex_Mask ("Tex_Mask", 2D) = "white" {}
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 0
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