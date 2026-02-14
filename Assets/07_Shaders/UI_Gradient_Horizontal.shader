Shader "Custom/UI_Gradient_Horizontal"
{
   Properties
    {
        _ColorLeft("Left Color", Color) = (1, 0, 0, 1)
        _ColorRight("Right Color", Color) = (0, 0, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" 
       "Queue"="Transparent"
       "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _ColorLeft;
                half4 _ColorRight;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return lerp(_ColorLeft, _ColorRight, IN.uv.x);
            }
            ENDHLSL
        }
    }
}
