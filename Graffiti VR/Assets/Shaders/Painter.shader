Shader "Unlit/Painter"
{
    Properties
    {
        _Color("Color", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _Position;
            float _Radius, _Hardness, _Strength;
            float4 _Color;

            float mask(float3 position, float3 center, float radius, float hardness) {
                float m = distance(center, position);
                return 1 - smoothstep(radius * hardness, radius, m);
            }

            v2f vert(appdata v)
            {
                v2f output;
                output.worldPos = mul(unity_ObjectToWorld, v.vertex);
                output.uv = v.uv;
                float4 uv = float4(0, 0, 0, 1);
                uv.xy = (v.uv.xy * float2(2, 2) - float2(1, 1)) * float2(1, _ProjectionParams.x);
                output.vertex = uv;

                return output;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float m = mask(i.worldPos, _Position, _Radius, _Hardness);
                float edge = m * _Strength;

                return lerp(col, _Color, edge);
            }
            ENDCG
        }
    }
}
