Shader "Custom/DepthMap"
{
    Properties
    {
    }
    SubShader
    {
        Tags {"Directional Shadow"="1" "Spot Shadow"="2" "Point Shadow"="3" }
        
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            uniform float farPlane;
            uniform float3 _l;
            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                //float depth : TEXCOORD0;
                float4 vert : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.vertex = mul( unity_MatrixMV, v.vertex );
                // o.depth = COMPUTE_DEPTH_01;
                // o.depth = length(mul( unity_MatrixMV, v.vertex ).xyz )*_ProjectionParams.w;
                // o.depth = (o.depth+1.0f)/2.0f;
                o.vert = v.vertex;


                
                //o.depth = -mul( unity_MatrixMV, v.vertex ).z *_ProjectionParams.w;
                //o.depth = length(o.vertex - _l)*_ProjectionParams.w;
                
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
                //float dis = length(mul(unity_ObjectToWorld, float4(i.vert.xyz, 1.0)) - _l);
                //float dis = length(mul( unity_ObjectToWorld, i.vert ) - _l);
                //if(dis > 1.0f/_ProjectionParams.w && _CubeCutCorner == 1)return 1.0f;
                //float d = dis * _ProjectionParams.w;
                //float d = length(mul( unity_MatrixMV, i.vert ).xyz )/_CubeFarPlane;

                //d = (d+1.0f)/2.0f;
                //return float4(d,1.0f,1.0f,1.0f);
                //return float4(i.depth,1,1,1);


                float3 worldPos = mul(unity_ObjectToWorld, float4(i.vert.xyz, 1.0)).xyz;

                //float4 worldPos = mul(unity_ObjectToWorld, i.bc);

                float3 ldir = (worldPos - _l).xyz;
                //float depth = length(ldir)  * _ProjectionParams.w;
                float depth = length(ldir) / 60;
                //return depth;

                //depth = length(i.bc.xyz - _l) / 10.0;

                float4 Frag0;

                float PI = 3.14159265358979;
                Frag0 = float4(depth, 1.0, 1.0, 1.0);

                float4 kv = PI * depth * float4(1.0, 3.0, 5.0, 7.0);

                Frag0 = Frag0 * 0.5 + 0.5;

                //if (Frag0.x > 1.0)
                //    return fixed4(1, 0, 0, 1);
                //else
                //    return fixed4(0, 0, 0, 1);

                //Frag0 = (Frag0 + 1.0f) / 2.0f;      // [0,1]


                //if (Frag0.z > 1.0)
                //    return fixed4(1,0,0,1);
                //else
                //    return fixed4(0,0,0,1);



                return Frag0;
            }
            
            ENDCG
            
        }
    }
    
    

}
