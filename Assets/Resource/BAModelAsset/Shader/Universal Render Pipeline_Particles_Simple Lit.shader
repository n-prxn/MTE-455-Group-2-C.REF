Shader "Universal Render Pipeline/Particles/Simple Lit" {
	Properties {
		_BaseMap ("Base Map", 2D) = "white" {}
		_BaseColor ("Base Color", Vector) = (1,1,1,1)
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
		_SpecGlossMap ("Specular", 2D) = "white" {}
		_SpecColor ("Specular", Vector) = (1,1,1,1)
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
		_BumpScale ("Scale", Float) = 1
		_BumpMap ("Normal Map", 2D) = "bump" {}
		[HDR] _EmissionColor ("Color", Vector) = (0,0,0,1)
		_EmissionMap ("Emission", 2D) = "white" {}
		_SmoothnessSource ("Smoothness Source", Float) = 0
		_SpecularHighlights ("Specular Highlights", Float) = 1
		[ToggleUI] _ReceiveShadows ("Receive Shadows", Float) = 1
		_SoftParticlesNearFadeDistance ("Soft Particles Near Fade", Float) = 0
		_SoftParticlesFarFadeDistance ("Soft Particles Far Fade", Float) = 1
		_CameraNearFadeDistance ("Camera Near Fade", Float) = 1
		_CameraFarFadeDistance ("Camera Far Fade", Float) = 2
		_DistortionBlend ("Distortion Blend", Range(0, 1)) = 0.5
		_DistortionStrength ("Distortion Strength", Float) = 1
		_Surface ("__surface", Float) = 0
		_Blend ("__mode", Float) = 0
		_Cull ("__cull", Float) = 2
		[ToggleUI] _AlphaClip ("__clip", Float) = 0
		[HideInInspector] _BlendOp ("__blendop", Float) = 0
		[HideInInspector] _SrcBlend ("__src", Float) = 1
		[HideInInspector] _DstBlend ("__dst", Float) = 0
		[HideInInspector] _ZWrite ("__zw", Float) = 1
		_ColorMode ("_ColorMode", Float) = 0
		[HideInInspector] _BaseColorAddSubDiff ("_ColorMode", Vector) = (0,0,0,0)
		[Toggle(_FLIPBOOKBLENDING_ON)] _FlipbookBlending ("__flipbookblending", Float) = 0
		[ToggleUI] _SoftParticlesEnabled ("__softparticlesenabled", Float) = 0
		[ToggleUI] _CameraFadingEnabled ("__camerafadingenabled", Float) = 0
		[ToggleUI] _DistortionEnabled ("__distortionenabled", Float) = 0
		[HideInInspector] _SoftParticleFadeParams ("__softparticlefadeparams", Vector) = (0,0,0,0)
		[HideInInspector] _CameraFadeParams ("__camerafadeparams", Vector) = (0,0,0,0)
		[HideInInspector] _DistortionStrengthScaled ("Distortion Strength Scaled", Float) = 0.1
		_QueueOffset ("Queue offset", Float) = 0
		[HideInInspector] _FlipbookMode ("flipbook", Float) = 0
		[HideInInspector] _Glossiness ("gloss", Float) = 0
		[HideInInspector] _Mode ("mode", Float) = 0
		[HideInInspector] _Color ("color", Vector) = (1,1,1,1)
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
	Fallback "Universal Render Pipeline/Particles/Unlit"
	//CustomEditor "UnityEditor.Rendering.Universal.ShaderGUI.ParticlesSimpleLitShader"
}