Shader "Custom/DepthMapSinZ"
{
    Properties
    {
    }
    
    SubShader
    {
        Tags {"Spot Shadow"="2" "Point Shadow"="3"}
        
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform float3 _l;
    uniform float farPlane;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                //float4 depth : TEXCOORD0;
                float4 vert : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.vertex = mul( unity_MatrixMV, v.vertex );
                // float d = length(mul( unity_MatrixMV, v.vertex ).xyz )*_ProjectionParams.w;
                // //d = COMPUTE_DEPTH_01;
                // float4 kv = PI*(d)*float4(1.0f,3.0f,5.0f,7.0f);
                // float4 depth = d*sin(kv);
                // o.depth = (depth+1.0f)/2.0f;

                o.vert = v.vertex;
                // #if true
                //     float4 worldpos = mul(unity_ObjectToWorld, v.vertex);
                //     o.vertex = mul(_ShadowVP, worldpos);
                //     float d = o.vertex.z / o.vertex.w;
                //     d = d * 0.5 + 0.5;
                // #else
                //     o.pos = UnityObjectToClipPos(v.vertex);
                //     float d = o.pos.z / o.pos.w;
                //     if(UNITY_NEAR_CLIP_VALUE == -1){
                //         d = d * 0.5 + 0.5;
                //     }
                //     #if UNITY_REVERSED_Z
                //         d = 1 - d;
                //     #endif
                // #endif
                // o.depth = d;
                return o;
            }
            uniform int _CubeCutCorner;
            float4 frag(v2f i) : SV_Target
            {
            //      float dis = length(mul(unity_ObjectToWorld, float4(i.vert.xyz, 1.0)) - _l);
            ////float dis = length(mul( unity_ObjectToWorld, i.vert ) - _l);
            //    if(dis > 1.0f/_ProjectionParams.w && _CubeCutCorner == 1)return 0.5f;
            //    float d = dis*_ProjectionParams.w;
            //    float4 kv = PI*(d)*float4(1.0f,3.0f,5.0f,7.0f);
            //    float4 depth = d*sin(kv);
            //    return  (depth+1.0f)/2.0f;
            //    //return i.depth;

                                float3 worldPos = mul(unity_ObjectToWorld, float4(i.vert.xyz, 1.0)).xyz;
                                //float3 worldPos = mul(unity_ObjectToWorld, i.bc).xyz;
                                float3 ldir = worldPos - _l;
                                //float depth = length(ldir)  * _ProjectionParams.w;
                                float depth = length(ldir) / 60;
                                //return depth;




                                float4 Frag4;

                                float PI = 3.14159265358979;

                                float4 kv = PI * depth * float4(1.0, 3.0, 5.0, 7.0);
                                Frag4 = depth * sin(kv);

                                //Frag4 = Frag4 * 0.5 + 0.5;

                                Frag4 = (Frag4 + 1.0f) / 2.0f;
                                return Frag4;

                                //light position
            }
            
            ENDCG
            
        }
    }

}
