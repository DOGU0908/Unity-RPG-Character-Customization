// This shader was created by Hippo

Shader "Custom/EyePaint" {
    Properties {
        [PerRendererData]_MainTex ("MainTex", 2D) = "white" {}
        _SaturationBound ("Saturation Bound", Range(0, 1)) = 0.25
        _ColorMultiplier ("Color Multiplier", Range(1, 2)) = 2
        [MaterialToggle] _Inverse ("Inverse", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

		// these six unused properties are required when a shader is used in the UI system, or you get a warning.
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"            
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            ZTest [unity_GUIZTestMode]
            Blend SrcAlpha OneMinusSrcAlpha
            
            Stencil {
                Ref [_Stencil]
                Comp [_StencilComp]
                Pass [_StencilOp]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            #pragma multi_compile_fwdbase
            //#pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _SaturationBound;
            float3 ToGrayscale( float3 pixel , float3 color , float saturation , float saturationBound , float colorMultiplier , half inverse ){
            if (saturation > saturationBound && !inverse) return pixel;
            if (saturation < saturationBound && inverse) return pixel;
            float gray = 0.3 * pixel.r + 0.59 * pixel.g + 0.11 * pixel.b;
            return colorMultiplier * color * gray;
            }
            
            uniform float _ColorMultiplier;
            uniform fixed _Inverse;
            float4 _ClipRect;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;               
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 worldPosition : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.worldPosition = v.vertex;
                o.pos = UnityObjectToClipPos( o.worldPosition );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex)); // Input Texture
                float4 node_3046_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_3046_p = lerp(float4(float4(_MainTex_var.rgb,0.0).zy, node_3046_k.wz), float4(float4(_MainTex_var.rgb,0.0).yz, node_3046_k.xy), step(float4(_MainTex_var.rgb,0.0).z, float4(_MainTex_var.rgb,0.0).y));
                float4 node_3046_q = lerp(float4(node_3046_p.xyw, float4(_MainTex_var.rgb,0.0).x), float4(float4(_MainTex_var.rgb,0.0).x, node_3046_p.yzx), step(node_3046_p.x, float4(_MainTex_var.rgb,0.0).x));
                float node_3046_d = node_3046_q.x - min(node_3046_q.w, node_3046_q.y);
                float node_3046_e = 1.0e-10;
                float3 node_3046 = float3(abs(node_3046_q.z + (node_3046_q.w - node_3046_q.y) / (6.0 * node_3046_d + node_3046_e)), node_3046_d / (node_3046_q.x + node_3046_e), node_3046_q.x);;
                float3 node_9613 = ToGrayscale( _MainTex_var.rgb , i.vertexColor.rgb , node_3046.g , _SaturationBound , _ColorMultiplier , _Inverse );
                float3 emissive = node_9613;
                float3 finalColor = emissive;
				_MainTex_var.a -= 1 - i.vertexColor.a;

                #ifdef UNITY_UI_CLIP_RECT
                finalColor.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (finalColor.a - 0.001);
                #endif

                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off

			Stencil {
                Ref [_Stencil]
                Comp [_StencilComp]
                Pass [_StencilOp]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            //#pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 2.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
				float4 worldPosition : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
				o.worldPosition = v.vertex;
                o.pos = UnityObjectToClipPos( o.worldPosition );                
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
    //CustomEditor "ShaderForgeMaterialInspector"
}
