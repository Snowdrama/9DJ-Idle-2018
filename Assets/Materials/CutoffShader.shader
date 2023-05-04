Shader "Unlit/CutoffShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {} //Main Texture, leave empty
		_TransTex ("Transition Texture", 2D) = "black" {} //Transition texture. Leave blank if using color
		_PatternTex ("Pattern Texture", 2d) = "white" {} //The pattern transition texture
		_Cutoff("Progress", Range (0, 1)) = 0 //Cut off slider
		_Color("Color", Color) = (0,0,0,1) //defaults to black
		_TintColor("Tint Color", Color) = (1,1,1,1) //defaults to white
		[MaterialToggle] _FlipFadeDirection("Flip Fade Direction", Float) = 0 //color or texture toggle
		[MaterialToggle] _UseColor("Use Color", Float) = 0 //color or texture toggle
		[MaterialToggle] _UseTransitionTexture("Use Transition Texture", Float) = 0 //color or texture toggle
	}
	SubShader
	{
        Tags {"Queue"="Transparent" "RenderType"="Cutout" }
		ZWrite Off
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _TransTex;
			sampler2D _PatternTex;
			float4 _MainTex_ST;
			float _Cutoff;
			fixed4 _Color;
			fixed4 _TintColor;
			float _UseColor;
			float _UseTransitionTexture;
			float _FlipFadeDirection;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 pattern = tex2D(_PatternTex, i.uv);
				if(_Cutoff < 0){
					_Cutoff = 0;
				}
				if(!_FlipFadeDirection && ((pattern.r+pattern.g+pattern.b) * 0.3333 > _Cutoff-0.0001)){
					// if _UseColor is enabled, use color instead
					if(_UseColor){
						return _Color;
					} else if(_UseTransitionTexture){
						// _Use color is not enabled, use the _TranTex
						fixed4 col = tex2D(_TransTex, i.uv);
						return col;
					} else {
						// _Use color is not enabled, use the _TranTex
						fixed4 col = tex2D(_TransTex, i.uv);
						col.a = 0;
						clip(col.a - _Cutoff - 0.0001);
						return col;
					}
				} else if(_FlipFadeDirection && ((pattern.r+pattern.g+pattern.b) * 0.3333 < _Cutoff)){
					// if _UseColor is enabled, use color instead
					if(_UseColor){
						return _Color;
					} else if(_UseTransitionTexture){
						// _Use color is not enabled, use the _TranTex
						fixed4 col = tex2D(_MainTex, i.uv);
						return col;
					} else {
						// _Use color is not enabled, use the _TranTex
						fixed4 col = tex2D(_MainTex, i.uv);
						col.a = 0;
						clip(col.a - _Cutoff - 0.0001);
						return col;
					} 
				}
                fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;
                clip(col.a - 0.001);
				return col;
			}
			ENDCG
		}
	}
}
