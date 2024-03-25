Shader "SimpleTestShader"//name of shader, declaration
{
	Properties//public variables in Unity
	{
		_MainTex ("Texture", 2D) = "white" {} // texture and its default color
		_TintColor("Tint Color", Color) = (1,1,1,1)
		_Transparency("Transparency", Range(0.0,0.5)) = 0.25
		_CutoutTresh("Cutout Treshold",Range(0.0,1.0))=0.2
		_Distance("Distance",Float)=1
		_Amplitude("Amplitude",Float)=1
		_Speed("Speed",Float)=1
		_Amount("Amount",Float)=1
	}
	SubShader //shader proper 
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }//tags to tell unity about rendering 
		Lighting Off //disabling light effecting shader
		Cull Back //default disabling of rendering polygons on the other side
		ZWrite Off //z buffer setting, with this depth buffer is active
		ZTest Less //setting for drawing geometry
		Blend SrcAlpha OneMinusSrcAlpha
		
		Fog{ Mode Off }//fog disabled

		Pass //calling gpu do calculate
		{
			CGPROGRAM//start shader
			#pragma vertex vert //vertex function
			#pragma fragment frag //fragment function
			
			#include "UnityCG.cginc" // normal C include

			struct appdata //structure to hold data on vertex position and its uv coordinate for texture
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f //object return from vertex function
			{
				float4 vertex : SV_POSITION; //screen space position
				float4 screenPos : TEXCOORD1;//position in screen space
				float2 uv: TEXCOORD0;
			};

			sampler2D _MainTex; //texture declared as sampler
			float4 _MainTex_ST;
			float4 _TintColor;
			float _Transparency;
			float _CutoutTresh;
			float _Distance;
			float _Amplitude;
			float _Speed;
			float _Amount;

			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
				v.vertex.y += sin(_Time.y * _Speed + v.vertex.x * _Amplitude) * _Distance * _Amount;
				o.vertex = UnityObjectToClipPos(v.vertex);//converts local position to clip space, last before screen space
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.screenPos = ComputeScreenPos(o.vertex);//converts clips space position to screen space position
				return o;
			}
			
			

			fixed4 frag (v2f i) : SV_Target//frag function return values to framebuffer
			{
				i.screenPos /= i.screenPos.w;//last value of float4 for normalization
				//fixed4 col = tex2D(_MainTex, float2(i.screenPos.x, i.screenPos.y));//taking screen positions from camera texture to put on object
				fixed4 col = tex2D(_MainTex, i.uv)+_TintColor;
				col.a = _Transparency;
				clip(col.g - _CutoutTresh);
				return col;
			}
			ENDCG
		}
	}
}
