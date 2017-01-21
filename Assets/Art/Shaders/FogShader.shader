// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1913,x:32720,y:32712,varname:node_1913,prsc:2|emission-3960-OUT,alpha-3887-OUT;n:type:ShaderForge.SFN_Tex2d,id:1224,x:31931,y:32697,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_1224,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cc12fc742e3f044c0b97a771910f2db4,ntxv:0,isnm:False|UVIN-4880-OUT;n:type:ShaderForge.SFN_Color,id:6568,x:31931,y:32926,ptovrint:False,ptlb:FogColor,ptin:_FogColor,varname:node_6568,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:658,x:30963,y:33215,varname:node_658,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3960,x:32164,y:32601,varname:node_3960,prsc:2|A-1224-RGB,B-6568-RGB;n:type:ShaderForge.SFN_Slider,id:1462,x:31120,y:32972,ptovrint:False,ptlb:FogScale,ptin:_FogScale,varname:node_1462,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:4880,x:31547,y:32997,varname:node_4880,prsc:2|A-1462-OUT,B-7227-OUT;n:type:ShaderForge.SFN_Time,id:4547,x:30963,y:33070,varname:node_4547,prsc:2;n:type:ShaderForge.SFN_Add,id:7227,x:31315,y:33117,varname:node_7227,prsc:2|A-9331-OUT,B-658-UVOUT;n:type:ShaderForge.SFN_Slider,id:5450,x:30884,y:33447,ptovrint:False,ptlb:FogSpeed,ptin:_FogSpeed,varname:node_5450,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.005,max:1;n:type:ShaderForge.SFN_Multiply,id:9331,x:31195,y:33288,varname:node_9331,prsc:2|A-4547-T,B-5450-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:2570,x:31404,y:33600,varname:node_2570,prsc:2;n:type:ShaderForge.SFN_ViewPosition,id:7810,x:31404,y:33768,varname:node_7810,prsc:2;n:type:ShaderForge.SFN_Distance,id:5900,x:31620,y:33707,varname:node_5900,prsc:2|A-2570-XYZ,B-7810-XYZ;n:type:ShaderForge.SFN_ProjectionParameters,id:9780,x:31860,y:33848,varname:node_9780,prsc:2;n:type:ShaderForge.SFN_Subtract,id:9529,x:32046,y:33748,varname:node_9529,prsc:2|A-3240-OUT,B-9780-NEAR;n:type:ShaderForge.SFN_Clamp01,id:5199,x:32276,y:33748,varname:node_5199,prsc:2|IN-9529-OUT;n:type:ShaderForge.SFN_Slider,id:1386,x:31526,y:33453,ptovrint:False,ptlb:node_1386,ptin:_node_1386,varname:node_1386,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1546744,max:5;n:type:ShaderForge.SFN_Multiply,id:3240,x:31841,y:33571,varname:node_3240,prsc:2|A-1386-OUT,B-5900-OUT;n:type:ShaderForge.SFN_Multiply,id:3887,x:32465,y:33073,varname:node_3887,prsc:2|A-1224-A,B-1234-OUT;n:type:ShaderForge.SFN_OneMinus,id:1234,x:32320,y:33452,varname:node_1234,prsc:2|IN-5199-OUT;proporder:1224-6568-1462-5450-1386;pass:END;sub:END;*/

Shader "Custom/FogShader" {
    Properties {
        _Tex ("Tex", 2D) = "white" {}
        _FogColor ("FogColor", Color) = (1,0,0,1)
        _FogScale ("FogScale", Range(0, 10)) = 1
        _FogSpeed ("FogSpeed", Range(0, 1)) = 0.005
        _node_1386 ("node_1386", Range(0, 5)) = 0.1546744
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform float4 _FogColor;
            uniform float _FogScale;
            uniform float _FogSpeed;
            uniform float _node_1386;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_4547 = _Time + _TimeEditor;
                float2 node_4880 = (_FogScale*((node_4547.g*_FogSpeed)+i.uv0));
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_4880, _Tex));
                float3 emissive = (_Tex_var.rgb*_FogColor.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_Tex_var.a*(1.0 - saturate(((_node_1386*distance(i.posWorld.rgb,_WorldSpaceCameraPos))-_ProjectionParams.g)))));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
