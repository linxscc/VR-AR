// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33049,y:32599,varname:node_9361,prsc:2|custl-8861-OUT,clip-555-OUT;n:type:ShaderForge.SFN_Tex2d,id:6400,x:32530,y:32315,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:_Diffuse,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bf8e2067baa542d44be82e2f9f7be26a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4749,x:32240,y:33062,ptovrint:False,ptlb:OPACITY,ptin:_OPACITY,varname:_OPACITY,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1ba08a0a7b6dad64ea027eb0975d96c2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:3451,x:32270,y:33298,ptovrint:False,ptlb:Opacityclip,ptin:_Opacityclip,varname:_Opacityclip,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:555,x:32807,y:33063,varname:node_555,prsc:2|A-4749-R,B-3451-OUT;n:type:ShaderForge.SFN_Fresnel,id:8040,x:32402,y:32526,varname:node_8040,prsc:2|NRM-2983-OUT,EXP-1890-OUT;n:type:ShaderForge.SFN_Slider,id:1890,x:32055,y:32676,ptovrint:False,ptlb:Fanwei,ptin:_Fanwei,varname:_Fanwei,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8888889,max:10;n:type:ShaderForge.SFN_Multiply,id:2735,x:32688,y:32657,varname:node_2735,prsc:2|A-8040-OUT,B-4885-RGB,C-6966-OUT;n:type:ShaderForge.SFN_Color,id:4885,x:32363,y:32705,ptovrint:False,ptlb:color,ptin:_color,varname:_color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_NormalVector,id:2983,x:32149,y:32443,prsc:2,pt:False;n:type:ShaderForge.SFN_Slider,id:6966,x:32206,y:32886,ptovrint:False,ptlb:Liangdu,ptin:_Liangdu,varname:_Liangdu,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Add,id:8861,x:32877,y:32505,varname:node_8861,prsc:2|A-2735-OUT,B-6400-RGB;proporder:6400-4749-3451-1890-4885-6966;pass:END;sub:END;*/

Shader "Shader Forge/custom_Lighting_D_O" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _OPACITY ("OPACITY", 2D) = "white" {}
        _Opacityclip ("Opacityclip", Range(0, 10)) = 1
        _Fanwei ("Fanwei", Range(0, 10)) = 0.8888889
        _color ("color", Color) = (0.5,0.5,0.5,1)
        _Liangdu ("Liangdu", Range(0, 5)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _OPACITY; uniform float4 _OPACITY_ST;
            uniform float _Opacityclip;
            uniform float _Fanwei;
            uniform float4 _color;
            uniform float _Liangdu;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _OPACITY_var = tex2D(_OPACITY,TRANSFORM_TEX(i.uv0, _OPACITY));
                clip((_OPACITY_var.r*_Opacityclip) - 0.5);
////// Lighting:
                float node_8040 = pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Fanwei);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 finalColor = ((node_8040*_color.rgb*_Liangdu)+_Diffuse_var.rgb);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _OPACITY; uniform float4 _OPACITY_ST;
            uniform float _Opacityclip;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _OPACITY_var = tex2D(_OPACITY,TRANSFORM_TEX(i.uv0, _OPACITY));
                clip((_OPACITY_var.r*_Opacityclip) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
