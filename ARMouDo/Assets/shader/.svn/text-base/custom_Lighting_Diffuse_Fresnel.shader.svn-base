// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33364,y:32659,varname:node_9361,prsc:2|custl-8329-OUT;n:type:ShaderForge.SFN_Tex2d,id:349,x:32699,y:32749,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:_Diffuse,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:42913dd1919630f4cbcc18497371955f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:5054,x:32826,y:33500,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1030425,c2:0.02941179,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5234,x:33099,y:33186,varname:node_5234,prsc:2|A-2366-OUT,B-5054-RGB,C-4379-OUT;n:type:ShaderForge.SFN_Fresnel,id:2366,x:32826,y:33320,varname:node_2366,prsc:2|NRM-2159-OUT,EXP-7450-OUT;n:type:ShaderForge.SFN_NormalVector,id:2159,x:32635,y:33297,prsc:2,pt:False;n:type:ShaderForge.SFN_Add,id:8329,x:33006,y:32825,varname:node_8329,prsc:2|A-5234-OUT,B-349-RGB;n:type:ShaderForge.SFN_Slider,id:7450,x:32490,y:33479,ptovrint:False,ptlb:FanWei,ptin:_FanWei,varname:_FanWei,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Slider,id:4379,x:32772,y:33642,ptovrint:False,ptlb:Liangdu,ptin:_Liangdu,varname:_Liangdu,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;proporder:349-5054-7450-4379;pass:END;sub:END;*/

Shader "Shader Forge/custom_Lighting" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Color ("Color", Color) = (0.1030425,0.02941179,1,1)
        _FanWei ("FanWei", Range(0, 10)) = 0
        _Liangdu ("Liangdu", Range(0, 5)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform float4 _Color;
            uniform float _FanWei;
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
////// Lighting:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 finalColor = ((pow(1.0-max(0,dot(i.normalDir, viewDirection)),_FanWei)*_Color.rgb*_Liangdu)+_Diffuse_var.rgb);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
