Shader "DSFX/FX_SHADER_Glitch_Tex" {
	Properties {
		_MainTex ("MainTex", 2D) = "white" {}
		_x ("x", Float) = 3
		_y ("y", Float) = 16
		_Speed_Value ("Speed_Value", Float) = 4
		_NoiseTex ("NoiseTex", 2D) = "white" {}
		_Shaking ("Shaking", Float) = 2
		_Glitch_value ("Glitch_value", Float) = 0.5
		_Jitter ("Jitter", Float) = 0.5
		[Enum(UnityEngine.Rendering.CullMode)] _Cull_Mode ("Cull_Mode", Float) = 2
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "ASEMaterialInspector"
}