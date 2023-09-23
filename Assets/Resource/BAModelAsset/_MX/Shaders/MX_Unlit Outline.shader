Shader "MX/Unlit Outline" {
	Properties {
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
		[Space] [HDR] _Tint ("Tint", Vector) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "white" {}
		[Space] [HDR] _OutlineTint ("Outline Tint", Vector) = (0.5,0.5,0.5,1)
		[HideInInspector] _OutlineZCorrection ("Outline Correction (world z)", Range(-0.0005, 0.0005)) = 0
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
}