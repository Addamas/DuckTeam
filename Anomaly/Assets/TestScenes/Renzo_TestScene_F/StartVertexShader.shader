Shader "Custom/StartVertexShader" {
	Properties {
		//frans bauer
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_XScrollSpeed("XScrollingSpeed", float) = 0
		_YScrollSpeed("YScrollingSpeed", float) = 0
		_EmissionTex("EmissionTex", 2D) = "gray" {}
		_EmissionColor("Color", Color) = (1,0,0,1)
		_EmissionIntensity("EmissionIntensity", Range(0,1)) = 0.5
		_CutOffTex("CutOff", 2D) = "white"{}

		_Extrusion ("Extrusion Amount", Range (-0.25, 0.5)) = 0.0
		_Frequency ("Frequency Amount", Range (0, 25)) = 2.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex:vert
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _EmissionTex;
		sampler2D _CutOffTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_EmissionTex;
			float3 vertexColors;
		};

		half _EmissionIntensity;
		fixed4 _EmissionColor;
		float _XScrollSpeed;
		float _YScrollSpeed;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float _Extrusion;
		float _Frequency;

		void vert(inout appdata_full v, out Input o) {

			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.vertexColors = v.normal;

			v.vertex.y += sin((v.vertex.x + _Time * _Frequency) * 10) *_Extrusion;
			v.vertex.x += sin((v.vertex.z + _Time * _Frequency) * 10) *_Extrusion;
			v.vertex.z += sin((v.vertex.y + _Time * _Frequency) * 10) *_Extrusion;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed2 scrolledUV = IN.uv_EmissionTex;
			fixed xScrollValue = _XScrollSpeed * _Time;
			fixed yScrollValue = _YScrollSpeed * _Time;

			scrolledUV += fixed2(xScrollValue, yScrollValue);

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 e = tex2D(_EmissionTex, scrolledUV) * _EmissionColor;
			fixed4 m = tex2D(_CutOffTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			o.Emission = e.rgb * m.rgb * _EmissionIntensity;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
