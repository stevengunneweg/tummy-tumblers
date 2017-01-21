// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.006,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:7404,x:32719,y:32712,varname:node_7404,prsc:2|emission-9309-OUT;n:type:ShaderForge.SFN_Color,id:7512,x:31725,y:32388,ptovrint:False,ptlb:UpperColor,ptin:_UpperColor,varname:node_7512,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:4734,x:31482,y:32581,varname:node_4734,prsc:2,uv:0;n:type:ShaderForge.SFN_Color,id:8815,x:31894,y:32911,ptovrint:False,ptlb:LowerColor,ptin:_LowerColor,varname:node_8815,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.710345,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:9414,x:31960,y:32566,varname:node_9414,prsc:2|A-7512-RGB,B-4734-V;n:type:ShaderForge.SFN_Multiply,id:1746,x:32110,y:32777,varname:node_1746,prsc:2|A-1460-OUT,B-8815-RGB;n:type:ShaderForge.SFN_OneMinus,id:1460,x:31894,y:32739,varname:node_1460,prsc:2|IN-4734-V;n:type:ShaderForge.SFN_Add,id:9309,x:32327,y:32647,varname:node_9309,prsc:2|A-9414-OUT,B-1746-OUT,C-1636-OUT;n:type:ShaderForge.SFN_Color,id:4792,x:32076,y:31970,ptovrint:False,ptlb:MiddleColor,ptin:_MiddleColor,varname:node_4792,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.08965516,c4:1;n:type:ShaderForge.SFN_TexCoord,id:3869,x:31819,y:32083,varname:node_3869,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:5113,x:32506,y:32232,varname:node_5113,prsc:2|A-4792-RGB,B-1217-OUT,C-4767-OUT;n:type:ShaderForge.SFN_Vector1,id:3010,x:31819,y:32245,varname:node_3010,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:1217,x:32069,y:32285,varname:node_1217,prsc:2|A-3869-V,B-3010-OUT;n:type:ShaderForge.SFN_OneMinus,id:8228,x:32035,y:32128,varname:node_8228,prsc:2|IN-3869-V;n:type:ShaderForge.SFN_Add,id:4767,x:32194,y:32154,varname:node_4767,prsc:2|A-8228-OUT,B-3010-OUT;n:type:ShaderForge.SFN_Power,id:1636,x:32741,y:32082,varname:node_1636,prsc:2|VAL-5113-OUT,EXP-1021-OUT;n:type:ShaderForge.SFN_Vector1,id:1021,x:32517,y:32104,varname:node_1021,prsc:2,v1:7;proporder:7512-8815-4792;pass:END;sub:END;*/

Shader "Custom/SkyShader" {
    Properties {
        _UpperColor ("UpperColor", Color) = (1,0,0,1)
        _LowerColor ("LowerColor", Color) = (0,0.710345,1,1)
        _MiddleColor ("MiddleColor", Color) = (0,1,0.08965516,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _UpperColor;
            uniform float4 _LowerColor;
            uniform float4 _MiddleColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_3010 = 0.5;
                float3 emissive = ((_UpperColor.rgb*i.uv0.g)+((1.0 - i.uv0.g)*_LowerColor.rgb)+pow((_MiddleColor.rgb*(i.uv0.g+node_3010)*((1.0 - i.uv0.g)+node_3010)),7.0));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
