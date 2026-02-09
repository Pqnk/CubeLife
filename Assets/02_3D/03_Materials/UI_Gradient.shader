Shader "Custom/NewUnlitUniversalRenderPipelineShader"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (0,0,0,1) 
        _BottomColor ("Bottom Color", Color) = (0,0.35,0.7,1)
        _Power ("Gradient Power", Range(0.1,4)) = 0.2
        _Offset ("Vertical Offset", Range(-1,1)) = 0
    }
   SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _TopColor;
            fixed4 _BottomColor;
            half _Power;
            half _Offset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half t = saturate(i.uv.y + _Offset);
                t = pow(t, _Power);
                return lerp(_BottomColor, _TopColor, t);
            }
            ENDCG
        }
    }
}
