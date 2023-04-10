Shader "Custom/Depth 1"
{
    SubShader
    {
        //AlphaTest Greater 0.0
        ZTest LEqual
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord:TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;

                float3 vert  : POSITION1;
                float4 bc    : TEXCOORD0;
                float2 uv    : TEXCOORD1;
                float4 depth : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform float _clipValue;//透明度测试时使用的阈值
            uniform float3 _l;
            uniform float4x4 _gWorldToLightCamera0, _gWorldToLightCamera1, _gWorldToLightCamera;
            uniform float farPlane;
            float sfunc(float F, float3 v)
            {

                float ll = length(v);
                return ll - 2.0 * F * ll / (ll + v.z) + F;
            }


            v2f vert(appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);

                //float3 hp = UnityObjectToViewPos(v.vertex);

                float4 hp = mul(_gWorldToLightCamera, mul(unity_ObjectToWorld, v.vertex));
                //hp = UnityObjectToClipPos(v.vertex);

                o.depth = v.vertex;
                o.bc.xyz = v.vertex.xyz;

                hp.z = -hp.z;

                //o.pos.xyz = hp;

                //o.pos.xyz = hp;
                //float3 dp = normalize(hp.xyz);

                float magnitude = length(hp.xyz);
                float3 dp = hp.xyz / magnitude;

                o.pos.x = dp.x / (dp.z + 1.0);
                o.pos.y = -dp.y / (dp.z + 1.0);

                //o.pos.z = -o.pos.z;

                float focal_length = -0.04;
                o.pos.z =  1-(sfunc(focal_length, hp.xyz) - 0.01) / (farPlane - 0.01);


                
                //o.pos.z = (ll - .001) / (10.0 - .001) * 2.0 - 1.0;
                o.pos.w = 1.0;



                //o.pos = UnityObjectToClipPos(v.vertex);
                //float3 vertex = mul(_gWorldToLightCamera, mul(unity_ObjectToWorld, v.vertex));    // 变换到相机坐标系
                ////float3 vertex = UnityObjectToViewPos(v.vertex);    // 变换到相机坐标系
                //vertex.z = -vertex.z;    // 右手坐标系，相机前方为-Z，翻转轴向
                //float magnitude1 = length(vertex.xyz);
                //float3 normalizedVertPos = vertex.xyz / magnitude1;    // 归一
                //o.pos.xy = normalizedVertPos.xy / (normalizedVertPos.z + 1);    // Normal = Incident + Reflection
                //o.pos.y = -o.pos.y;   // DX下如果渲染到 RenderTexture则需要加上这句

                
                

                o.bc.xyz = v.vertex.xyz;
                //o.bc.w = v.vertex.w;
                o.bc.w = hp.z / hp.w;


                o.vert = v.vertex;
                //o.bc = v.vertex;

                

                return o;

                
            }
                                          
            float4 frag(v2f i) : SV_Target
            {

                //return 1;
                //return i.bc;
                /*float dis = length(mul( unity_ObjectToWorld, i.bc ) - _l);
                float d = dis * _ProjectionParams.w;
                return d;*/

                float3 worldPos = mul(unity_ObjectToWorld, float4(i.vert.xyz, 1.0)).xyz;
                

                //float3 worldPos = mul(unity_ObjectToWorld, i.bc).xyz;
                float3 ldir = worldPos - _l;

                //float depth = length(ldir)  * _ProjectionParams.w;
                float depth = length(ldir) / 60;
                float4 Frag1;

                float PI = 3.14159265358979;
                
                float4 kv = PI * depth * float4(1.0, 3.0, 5.0, 7.0);
                Frag1 = cos(kv);

                Frag1 = Frag1 * 0.5 + 0.5;



                //Frag1 = (Frag1 + 1.0f) / 2.0f;
                return Frag1;
                //return float4(length(ldir),0,0, 1);

                

            }
            ENDCG
        }
    }
}