Shader "MX/C-Eyebrow-OneColor" {
	Properties {
		[HDR] _Tint ("Tint", Vector) = (1,1,1,1)
		[NoScaleOffset] _MainTex ("Main Tex", 2D) = "white" {}

		_ASEOutlineWidth( "Outline Width", Float ) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_ASEOutlineColor( "Outline Color", Color ) = (0,0,0,0)
		_shadow_clip("shadow_clip", Range( 0 , 1)) = 0
		_shadow_edge("shadow_edge", Range( 0 , 1)) = 0
		_Base_color("Base_color", Color) = (1,1,1,0)
		_light("light", Color) = (1,1,1,0)
		_drak("drak", Color) = (0,0,0,0)
		_Mouth_mask("Mouth_mask", 2D) = "black" {}
		[HDR]_frensel_color("frensel_color", Color) = (0.3166077,1,0,0)
		_frensel_power("frensel_power", Range( 0 , 3)) = 0.3368064
		_frensel_range("frensel_range", Range( 0 , 1)) = 0.5524385
		_frensel_hard("frensel_hard", Range( 0 , 1)) = 1
		[Enum(off,0,on,1)]_Zwrite("Zwrite", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)]_Cull("Cull", Float) = 0

		_ZCorrection ("Z Correction", Range(0, 0.1)) = 0
		[Space(5)] [Header(Additional Light)] [Space(6)] _AdditionalLightStrength ("Additional Light Strength", Range(0, 2)) = 0.15
		_AdditionalLightSharpness ("Additional Light Sharpness", Range(0, 100)) = 5
		[HideInInspector] _CodeAddColor ("_CodeAddColor", Vector) = (0,0,0,0)
		[HideInInspector] _CodeMultiplyColor ("_CodeMultiplyColor", Vector) = (1,1,1,1)
		[HideInInspector] _CodeAddRimColor ("_CodeAddRimColor", Vector) = (0,0,0,0)
		[HideInInspector] _DitherThreshold ("_DitherThreshold", Range(0, 1)) = 0
		[HideInInspector] _GrayBrightness ("_GrayBrightness", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlend ("Src Blend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlend ("Dst Blend", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _SrcBlendAlpha ("Src Blend Alpha", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] [HideInInspector] _DstBlendAlpha ("Dst Blend Alpha", Float) = 0
		[Enum(Off,0, On,1)] [HideInInspector] _ZWrite ("Z Write", Float) = 1
		[Enum(UnityEngine.Rendering.CullMode)] [HideInInspector] _Cull ("Cull", Float) = 2
	
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags {}
		Cull Front
		CGPROGRAM
		
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 

		float4 _ASEOutlineColor;
		float _ASEOutlineWidth;

		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += ( v.normal * _ASEOutlineWidth );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _ASEOutlineColor.rgb;
			o.Alpha = 1;
		}

		ENDCG

		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			float3 viewDir;
		};

		uniform float _Zwrite;
		uniform float _Cull;
		uniform float _Ztest = 4;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform sampler2D _MouthTileTex;
		uniform float _MouthTileRows;
		uniform float _MouthTileCols;
		uniform sampler2D _Mouth_mask;
		uniform float4 _Mouth_mask_ST;
		uniform float4 _drak;
		uniform float4 _light;
		uniform float _shadow_clip;
		uniform float _shadow_edge;
		uniform float4 _Base_color;
		uniform float4 _frensel_color;
		uniform float _frensel_power;
		uniform float _frensel_range;
		uniform float _frensel_hard;
		uniform float _Cutoff = 0.5;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;

			#ifdef _IF_44_MOUTH_ON
			float2 appendResult34 = (float2(( ceil( ( _MouthTileRows - 1.0 ) ) * 0.25 ) , ( ceil( ( _MouthTileCols - 9.0 ) ) * 0.25 )));
			#else
			float2 appendResult34 = (float2(( ceil( ( _MouthTileRows - 1.0 ) ) * 0.125 ) , ( ceil( ( _MouthTileCols - 9.0 ) ) * 0.125 )));
			#endif

			#ifdef _IF_44_MOUTH_ON
			float4 tex2DNode36 = tex2D( _MouthTileTex, ( ( i.uv_texcoord * 1 ) + appendResult34 ) );
			#else
			float4 tex2DNode36 = tex2D( _MouthTileTex, ( ( i.uv_texcoord * 0.5 ) + appendResult34 ) );
			#endif

			float2 uv_Mouth_mask = i.uv_texcoord * _Mouth_mask_ST.xy + _Mouth_mask_ST.zw;

			float staticSwitch75 = 0.0;

			float temp_output_37_0 = ( staticSwitch75 * tex2DNode36.a );
			float4 lerpResult39 = lerp( tex2D( _MainTex, uv_MainTex ) , tex2DNode36 , temp_output_37_0);
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = i.worldNormal;
			float dotResult6 = dot( ase_worldlightDir , ase_worldNormal );
			float smoothstepResult14 = smoothstep( _shadow_clip , ( _shadow_clip + _shadow_edge ) , ( ( dotResult6 + 1.0 ) * 0.5 ));
			float4 lerpResult17 = lerp( _drak , _light , smoothstepResult14);
			float3 base_part21 = (( lerpResult39 * lerpResult17 * _Base_color )).rgb;
			float dotResult51 = dot( ase_worldNormal , i.viewDir );
			float smoothstepResult55 = smoothstep( _frensel_range , ( _frensel_range + _frensel_hard ) , ( 1.0 - dotResult51 ));
			float3 frensel61 = (( _frensel_color * _frensel_power * saturate( smoothstepResult55 ) )).rgb;
			float alpha43 = saturate( ( ( 1.0 - staticSwitch75 ) + temp_output_37_0 ) );
			float4 appendResult24 = (float4((( base_part21 + frensel61 )).xyz , alpha43));
			o.Emission = appendResult24.xyz;
			o.Alpha = 1;
			float temp_output_46_0 = alpha43;
			clip( temp_output_46_0 - _Cutoff );
		}

		ENDCG
		
		CGPROGRAM
		#pragma surface surf Unlit keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = worldViewDir;
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	//CustomEditor "MXCharacterEtcShaderGUI"
}