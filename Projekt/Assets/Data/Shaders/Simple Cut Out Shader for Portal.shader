Shader "SimpleCutOutShader"//name of shader, declaration
{
	Properties//public variables in Unity
	{
		_MainTex ("Texture", 2D) = "white" {} // texture and its default color
	}
	SubShader //shader proper 
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }//tags to tell unity about rendering 
		Lighting Off //disabling light effecting shader
		Cull Back //default disabling of rendering polygons on the other side
		ZWrite On //z buffer setting, with this depth buffer is active
		ZTest Less //setting for drawing geometry
		
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
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);//converts local position to clip space, last before screen space
				o.screenPos = ComputeScreenPos(o.vertex);//converts clips space position to screen space position
				return o;
			}
			
			sampler2D _MainTex; //texture declared as sampler

			fixed4 frag (v2f i) : SV_Target//frag function return values to framebuffer
			{
				i.screenPos /= i.screenPos.w;//last value of float4 for normalization
				fixed4 col = tex2D(_MainTex, float2(i.screenPos.x, i.screenPos.y));//taking screen positions from camera texture to put on object
				
				return col;
			}
			ENDCG
		}
	}
}
