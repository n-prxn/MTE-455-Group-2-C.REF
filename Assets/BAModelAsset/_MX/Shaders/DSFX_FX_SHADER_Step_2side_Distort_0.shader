Shader "DSFX/FX_SHADER_Step_2side_Distort_0" {
	Properties {
		_Multiply ("Multiply", Float) = 1
		[HDR] _FrontFacesColor ("Front Faces Color", Vector) = (1,0,0,0)
		[HDR] _BackFacesColor ("Back Faces Color", Vector) = (0,0.04827571,1,0)
		_Tex_Main ("Tex_Main", 2D) = "white" {}
		[Toggle] _Custom_Data_Main_Offset_Use ("Custom_Data_Main_Offset_Use", Float) = 0
		[Toggle] _Custom_Data_Mask_Offset_Use ("Custom_Data_Mask_Offset_Use", Float) = 0
		[Toggle] _RGBRGBA ("RGB>RGBA", Float) = 0
		_Main_Speed_X ("Main_Speed_X", Float) = 0
		_Main_Speed_Y ("Main_Speed_Y", Float) = 0
		_Tex_Mask ("Tex_Mask", 2D) = "white" {}
		_Mask_Speed_X ("Mask_Speed_X", Float) = 0
		_Mask_Speed_Y ("Mask_Speed_Y", Float) = 0
		_Tex_DistortVertexnormal ("Tex_Distort/Vertexnormal", 2D) = "white" {}
		_Dis_Speed_X ("Dis_Speed_X", Float) = 0
		_Dis_Speed_Y ("Dis_Speed_Y", Float) = 0
		_Distortion_Power_X ("Distortion_Power_X", Float) = 0
		_Distortion_Power_Y ("Distortion_Power_Y", Float) = 0
		_VertexNormal_Offset ("VertexNormal_Offset", Vector) = (0,0,0,0)
		_VertexNormal_Speed_X ("VertexNormal_Speed_X", Float) = 0
		_VertexNormal_Speed_Y ("VertexNormal_Speed_Y", Float) = 0
		[Toggle] _Custom_Data_Use ("Custom_Data_Use", Float) = 1
		_Step_Power ("Step_Power", Range(0, 1)) = 1
		_AlphaClip_Value ("AlphaClip_Value", Float) = 0.1
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