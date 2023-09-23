Shader "Decal/Radial Flow" {
	Properties {
		[Header(add script. ScreenSpaceDecal)] [Space(10)] [HDR] _Tint ("Tint", Vector) = (1,1,1,1)
		[NoScaleOffset] _MainTex ("Main Tex (vertical strip, A: flow mask)", 2D) = "white" {}
		[Space(10)] [NoScaleOffset] _FlowTex ("Flow Tex (vertical strip)", 2D) = "black" {}
		_FlowSpeed ("Flow Speed", Float) = 0.5
		[HideInInspector] _invMatrixMVP0 ("_invMatrixMVP0", Vector) = (1,0,0,0)
		[HideInInspector] _invMatrixMVP1 ("_invMatrixMVP1", Vector) = (0,1,0,0)
		[HideInInspector] _invMatrixMVP2 ("_invMatrixMVP2", Vector) = (0,0,1,0)
		[HideInInspector] _invMatrixMVP3 ("_invMatrixMVP3", Vector) = (0,0,0,1)
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