Shader "DSFX/FX_SHADER_Explosion_Smoke_Blur_Flowmap_0" {
	Properties {
		[HDR] _RColor ("[R]Color", Vector) = (1,1,1,1)
		_Mutiply ("Mutiply", Float) = 1
		_MainTexRGBA ("MainTex (RGBA)", 2D) = "white" {}
		[HDR] _GColor ("[G]Color", Vector) = (1,0.2989459,0,0)
		[Toggle] _Color_Custom_Data_Use ("Color_Custom_Data_Use", Float) = 1
		[Toggle] _GCustom_Data_Use ("[G]Custom_Data_Use", Float) = 1
		_GColor_Sharpness ("[G]Color_Sharpness", Float) = 20
		_GDisappear_Offset ("[G]Disappear_Offset", Range(0, 1)) = 0
		[Toggle] _BCustom_Data_Use ("[B]Custom_Data_Use", Float) = 1
		_BStep_Glow_Value ("[B]Step_Glow_Value", Float) = 20
		_BDisappear_Offset ("[B]Disappear_Offset", Range(0, 1)) = 0
		_Shadow_Color_Multiply ("Shadow_Color_Multiply", Range(0, 1)) = 0.5
		[Toggle] _Shadow_Value_Data_Use ("Shadow_Value_Data_Use", Float) = 0
		_Shadow_Value ("Shadow_Value", Range(-0.3, 0)) = 0
		_Shadow_Glow_Value ("Shadow_Glow_Value", Float) = 20
		_FlowTexRGB ("FlowTex(RGB)", 2D) = "white" {}
		[Toggle] _Flowspeed_Data_Use ("Flowspeed_Data_Use", Float) = 0
		_FlowSpeed ("FlowSpeed", Float) = 1
		[Toggle] _ZWrite_Mode ("ZWrite_Mode", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest_Mode ("ZTest_Mode", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode ("Cull_Mode", Float) = 2
		[HideInInspector] _texcoord ("", 2D) = "white" {}
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