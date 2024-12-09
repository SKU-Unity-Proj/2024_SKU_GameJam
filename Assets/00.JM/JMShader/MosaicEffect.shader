Shader "Custom/MosaicEffect"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {} // 텍스처
        _BlockSize("Block Size", Float) = 20.0   // 모자이크 블록 크기
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                sampler2D _MainTex; // 텍스처 샘플러
                float4 _MainTex_ST; // 텍스처 좌표 변환
                float _BlockSize;   // 블록 크기

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    // 텍스처 좌표 변환 (TRANSFORM_TEX 대신 직접 계산)
                    o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // 블록 크기 계산
                    float2 blockSize = float2(_BlockSize / _ScreenParams.x, _BlockSize / _ScreenParams.y);

                    // 모자이크 블록 좌표 계산
                    float2 blockUV = floor(i.uv / blockSize) * blockSize;

                    // 블록 UV를 기반으로 텍스처 색상 샘플링
                    fixed4 color = tex2D(_MainTex, blockUV);

                    return color;
                }
                ENDCG
            }
        }
}
