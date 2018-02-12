// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Pixelate"{
	Properties{
		_MainTex ("Texture", 2D) = "white" {}
		_PixelateX("Pixelate X",Int) = 5
		_PixelateY("Pixelate Y",Int) = 5
		_PixelateField("Pixelate Field",Int) = 0
	}
	SubShader{
		Cull Off ZWrite Off ZTest Always
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			int _PixelateX;
			int _PixelateY;
			int _PixelateField;

			struct appdata{
				float4 vertex:POSITION;
				float2 uv:TEXCOORD0;
			};

			struct v2f{
				float2 uv:TEXCOORD0;
				float4 vertex:SV_POSITION;
			};

			v2f vert(appdata v){
				v2f o;
				o.vertex=UnityObjectToClipPos(v.vertex);
				o.uv=v.uv;
				return o;
			}

			fixed4 frag(v2f i):SV_Target{
				float xDis = abs(i.vertex.x - _ScreenParams.x/2);
				float yDis = abs(i.vertex.y - _ScreenParams.y/2);
				int pixelX = _PixelateX;
				int pixelY = _PixelateY;
				/*
				float dis = sqrt((xDis*xDis)+(yDis*yDis));
				pixelX += _PixelateField*dis/_ScreenParams.x*dis/_ScreenParams.x;
				pixelY += _PixelateField*dis/_ScreenParams.y*dis/_ScreenParams.y;*/
				if(xDis > yDis) {
					pixelX += _PixelateField*(xDis/_ScreenParams.x)*(xDis/_ScreenParams.x);
					pixelY += _PixelateField*(xDis/_ScreenParams.x)*(xDis/_ScreenParams.x);
				} else {
					pixelX += _PixelateField*(yDis/_ScreenParams.y)*(yDis/_ScreenParams.y);
					pixelY += _PixelateField*(yDis/_ScreenParams.y)*(yDis/_ScreenParams.y);
				}
				 
				int2 Pixelate=int2(pixelX,pixelY);
				fixed4 rescol=float4(0,0,0,0);
				if(pixelX>1 || pixelY>1){
					float2 PixelSize=1/float2(_ScreenParams.x,_ScreenParams.y);
					float2 BlockSize=PixelSize*Pixelate;
					float2 CurrentBlock=float2(
						(floor(i.uv.x/BlockSize.x)*BlockSize.x),
						(floor(i.uv.y/BlockSize.y)*BlockSize.y)
					);
					rescol=tex2D(_MainTex,CurrentBlock+BlockSize/2);
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/4,BlockSize.y/4));
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/2,BlockSize.y/4));
					rescol+=tex2D(_MainTex,CurrentBlock+float2((BlockSize.x/4)*3,BlockSize.y/4));
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/4,BlockSize.y/2));
					rescol+=tex2D(_MainTex,CurrentBlock+float2((BlockSize.x/4)*3,BlockSize.y/2));
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/4,(BlockSize.y/4)*3));
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/2,(BlockSize.y/4)*3));
					rescol+=tex2D(_MainTex,CurrentBlock+float2((BlockSize.x/4)*3,(BlockSize.y/4)*3));
					rescol/=9;
				}else{
					rescol=tex2D(_MainTex,i.uv);
				}
				return rescol;
			}

			ENDCG
		}
	}
}
