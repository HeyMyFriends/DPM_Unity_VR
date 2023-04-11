Shader "Custom/Depth"
{
    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Structures
            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord:TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 vert : POSITION1;
            };

            // Uniforms
            uniform float3 _l;
            uniform float4x4 _gWorldToLightCamera;
            uniform float farPlane;

            // Functions
            float sfunc(float F, float3 v)
            {
                float ll = length(v);
                return ll - 2.0 * F * ll / (ll + v.z) + F;
            }


            v2f vert(appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);

                float4 hp =  mul(_gWorldToLightCamera, mul(unity_ObjectToWorld, v.vertex));
                hp.z = -hp.z;

                float magnitude = length(hp.xyz);
                float3 dp = hp.xyz / magnitude;

                o.pos.x = dp.x / (dp.z + 1.0) ;
                o.pos.y = -dp.y / (dp.z + 1.0) ;
                float focal_length = 0.04;
                o.pos.z = 1 - (sfunc(focal_length, hp.xyz) - 0.01) / (farPlane - 0.01);
                o.pos.w = 1.0;

                o.vert = v.vertex;

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 worldPos = mul(unity_ObjectToWorld, float4(i.vert.xyz, 1.0)).xyz;
                float3 ldir = (worldPos - _l).xyz;
                float depth = length(ldir) / 60;       

                float4 Frag0;
                Frag0 = float4(depth, 1.0, 1.0, 1.0);
                float PI = 3.14159265358979;
                float4 kv = PI * depth * float4(1.0, 3.0, 5.0, 7.0);
                Frag0 = Frag0 * 0.5 + 0.5;

                return Frag0;
            }
            ENDCG
        }
    }
}
