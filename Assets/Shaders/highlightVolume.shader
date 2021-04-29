Shader "Unlit/highlightVolume"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,.2) 
    }
    SubShader
    {
        Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
        Pass
        {
            Stencil
            {
                Ref 172
                Comp Always
                Pass Replace
                ZFail Zero
            }
            Blend Zero One
            Cull Front
            ZTest  GEqual
            ZWrite Off
 
        }// end stencil pass
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Stencil
            {
                Ref 172
                Comp Equal
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            float4 _Color;
            struct appdata
            {
                float4 vertex : POSITION;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }//end color pass
    }
}