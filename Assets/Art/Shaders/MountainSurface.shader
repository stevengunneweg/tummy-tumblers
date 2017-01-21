// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9243,x:32719,y:32712,varname:node_9243,prsc:2|emission-3275-OUT,custl-5379-OUT;n:type:ShaderForge.SFN_Color,id:8818,x:31608,y:32676,ptovrint:False,ptlb:GrassColor,ptin:_GrassColor,varname:node_8818,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.2941176,c3:0.01419881,c4:1;n:type:ShaderForge.SFN_Color,id:6708,x:31239,y:32185,ptovrint:False,ptlb:HillColor,ptin:_HillColor,varname:node_6708,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5147059,c2:0.4414151,c3:0.3103374,c4:1;n:type:ShaderForge.SFN_LightAttenuation,id:7044,x:31874,y:33106,varname:node_7044,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5379,x:32232,y:33197,varname:node_5379,prsc:2|A-7044-OUT,B-1646-RGB,C-2284-OUT;n:type:ShaderForge.SFN_LightColor,id:1646,x:31874,y:33245,varname:node_1646,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:9003,x:31768,y:32209,ptovrint:False,ptlb:Grid,ptin:_Grid,varname:node_9003,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b8280de85b7804ad2a856a7ad1f159d1,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:3275,x:32535,y:32409,varname:node_3275,prsc:2|A-1439-OUT,B-8818-RGB;n:type:ShaderForge.SFN_LightVector,id:4770,x:31200,y:32874,varname:node_4770,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:71,x:31159,y:33056,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:5716,x:31405,y:32988,varname:node_5716,prsc:2,dt:0|A-4770-OUT,B-71-OUT;n:type:ShaderForge.SFN_Multiply,id:2284,x:31885,y:32906,varname:node_2284,prsc:2|A-8818-RGB,B-7059-OUT;n:type:ShaderForge.SFN_Multiply,id:8005,x:31452,y:33150,varname:node_8005,prsc:2|A-5716-OUT,B-5426-OUT;n:type:ShaderForge.SFN_Vector1,id:5426,x:31298,y:33298,varname:node_5426,prsc:2,v1:11;n:type:ShaderForge.SFN_Round,id:6881,x:31603,y:33076,varname:node_6881,prsc:2|IN-8005-OUT;n:type:ShaderForge.SFN_Divide,id:7059,x:31586,y:33318,varname:node_7059,prsc:2|A-6881-OUT,B-5426-OUT;n:type:ShaderForge.SFN_Color,id:5585,x:31768,y:31974,ptovrint:False,ptlb:GridColor,ptin:_GridColor,varname:node_5585,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:1439,x:32319,y:32097,varname:node_1439,prsc:2|A-5585-RGB,B-9003-A;n:type:ShaderForge.SFN_Vector3,id:8965,x:30659,y:32384,varname:node_8965,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_NormalVector,id:4500,x:30659,y:32502,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:2897,x:30930,y:32444,varname:node_2897,prsc:2,dt:0|A-8965-OUT,B-4500-OUT;proporder:8818-9003-5585-6708;pass:END;sub:END;*/

Shader "Custom/MountainSurface" {
    Properties {
        _GrassColor ("GrassColor", Color) = (0,0.2941176,0.01419881,1)
        _Grid ("Grid", 2D) = "white" {}
        _GridColor ("GridColor", Color) = (0,0,0,1)
        _HillColor ("HillColor", Color) = (0.5147059,0.4414151,0.3103374,1)
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
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _GrassColor;
            uniform sampler2D _Grid; uniform float4 _Grid_ST;
            uniform float4 _GridColor;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _Grid_var = tex2D(_Grid,TRANSFORM_TEX(i.uv0, _Grid));
                float3 emissive = ((_GridColor.rgb*_Grid_var.a)+_GrassColor.rgb);
                float node_5426 = 11.0;
                float3 finalColor = emissive + (attenuation*_LightColor0.rgb*(_GrassColor.rgb*(round((dot(lightDirection,i.normalDir)*node_5426))/node_5426)));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _GrassColor;
            uniform sampler2D _Grid; uniform float4 _Grid_ST;
            uniform float4 _GridColor;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_5426 = 11.0;
                float3 finalColor = (attenuation*_LightColor0.rgb*(_GrassColor.rgb*(round((dot(lightDirection,i.normalDir)*node_5426))/node_5426)));
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
