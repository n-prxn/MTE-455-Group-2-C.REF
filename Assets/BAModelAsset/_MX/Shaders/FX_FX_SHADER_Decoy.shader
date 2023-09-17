Shader "FX/FX_SHADER_Decoy" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,0)
		_Color_Inside ("Color_Inside", Vector) = (0,0.05771368,0.3396226,0)
		_MainTexture ("MainTexture", 2D) = "white" {}
		_Disappear ("Disappear", Range(0, 2)) = 0
		_NoiseScale ("NoiseScale", Float) = 2
		_RampPow ("RampPow", Float) = 1
		[HideInInspector] _texcoord ("", 2D) = "white" {}
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