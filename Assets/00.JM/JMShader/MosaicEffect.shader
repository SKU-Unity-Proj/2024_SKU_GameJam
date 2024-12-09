Shader "Custom/MosaicEffect"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {} // �ؽ�ó
        _BlockSize("Block Size", Float) = 20.0   // ������ũ ��� ũ��
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

                sampler2D _MainTex; // �ؽ�ó ���÷�
                float4 _MainTex_ST; // �ؽ�ó ��ǥ ��ȯ
                float _BlockSize;   // ��� ũ��

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

                    // �ؽ�ó ��ǥ ��ȯ (TRANSFORM_TEX ��� ���� ���)
                    o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // ��� ũ�� ���
                    float2 blockSize = float2(_BlockSize / _ScreenParams.x, _BlockSize / _ScreenParams.y);

                    // ������ũ ��� ��ǥ ���
                    float2 blockUV = floor(i.uv / blockSize) * blockSize;

                    // ��� UV�� ������� �ؽ�ó ���� ���ø�
                    fixed4 color = tex2D(_MainTex, blockUV);

                    return color;
                }
                ENDCG
            }
        }
}
