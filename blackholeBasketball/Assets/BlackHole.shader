Shader "BlackHole"
{
    Properties
    {
        [ShowAsVector2]
        _radius ("radius",float) = 1
        _distance ("distance",float) = 1
    }
    SubShader
    {
        // Draw after all opaque geometry
        Tags { "Queue" = "Transparent" }

        // Grab the screen behind the object into _BackgroundTexture
        GrabPass
        {
            //"_BackgroundTexture"
        }

        // Render the object with the texture generated above, and invert the colors
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 objScreenPoint : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO  
                
            };
            float _radius;
            float _distance;
            v2f vert(appdata_base v) {
                v2f o;
                // use UnityObjectToClipPos from UnityCG.cginc to calculate 
                // the clip-space of the vertex
                o.pos = UnityObjectToClipPos(v.vertex);
                // use ComputeGrabScreenPos function from UnityCG.cginc
                // to get the correct texture coordinate
                o.grabPos = ComputeGrabScreenPos(o.pos);
                o.objScreenPoint = ComputeScreenPos(UnityObjectToClipPos(fixed4(0, 0, 0, 1)));
                #if UNITY_UV_STARTS_AT_TOP
                if (_ProjectionParams.x > 0)
                        o.objScreenPoint.y = 1-o.objScreenPoint.y;
                #endif
                
                return o;
            }

            sampler2D _GrabTexture;
            
            half4 frag(v2f i) : SV_Target
            {
                //get position of object
                float4 pos =i.objScreenPoint; 
                //float4 pos = (i.objScreenPoint-float4(0,1,0,0))*float4(1,-1,1,1);
                
                
                float4 offset = i.grabPos-pos;
                float2 ratio = float2(_ScreenParams.y/_ScreenParams.x,1);

                //do black hole magic
                float rad = length(offset.xy/ratio);
                float deformation = 1/pow(rad*pow(_distance,0.5),2)*_radius*2*0.7;
                deformation*=max(0,1-rad*10);
                //apply magic
                offset = offset*(1+deformation);

                //reset offset
                offset += pos;


                half4 bgcolor = tex2Dproj(_GrabTexture, offset);
                
                return bgcolor;
            }
            ENDCG
        }

    }
}
