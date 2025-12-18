//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2018 //
/// Shader generate with Shadero 1.9.6                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Customs/button_2"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_Color ("Tint", Color) = (1,1,1,1)
[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
RotationUV_Rotation_1("RotationUV_Rotation_1", Range(-360, 360)) = -174.857
RotationUV_Rotation_PosX_1("RotationUV_Rotation_PosX_1", Range(-1, 2)) = 0.5
RotationUV_Rotation_PosY_1("RotationUV_Rotation_PosY_1", Range(-1, 2)) =0.5
RotationUV_Rotation_Speed_1("RotationUV_Rotation_Speed_1", Range(-8, 8)) =0
AnimatedMouvementUV_X_1("AnimatedMouvementUV_X_1", Range(-1, 1)) = -1
AnimatedMouvementUV_Y_1("AnimatedMouvementUV_Y_1", Range(-1, 1)) = 0
AnimatedMouvementUV_Speed_1("AnimatedMouvementUV_Speed_1", Range(-1, 1)) = 0
AnimatedOffsetUV_X_1("AnimatedOffsetUV_X_1", Range(-1, 1)) = 1
AnimatedOffsetUV_Y_1("AnimatedOffsetUV_Y_1", Range(-1, 1)) = 0
AnimatedOffsetUV_ZoomX_1("AnimatedOffsetUV_ZoomX_1", Range(1, 10)) = 1
AnimatedOffsetUV_ZoomY_1("AnimatedOffsetUV_ZoomY_1", Range(1, 10)) = 1
AnimatedOffsetUV_Speed_1("AnimatedOffsetUV_Speed_1", Range(-1, 1)) = 0.466
_NewTex_1("NewTex_1(RGB)", 2D) = "white" { }
_ShinyFX_Pos_1("_ShinyFX_Pos_1", Range(-1, 1)) = 0
_ShinyFX_Size_1("_ShinyFX_Size_1", Range(-1, 1)) = -0.1
_ShinyFX_Smooth_1("_ShinyFX_Smooth_1", Range(0, 1)) = 0.261
_ShinyFX_Intensity_1("_ShinyFX_Intensity_1", Range(0, 4)) = 1
_ShinyFX_Speed_1("_ShinyFX_Speed_1", Range(0, 8)) = 0
_OperationBlend_Fade_1("_OperationBlend_Fade_1", Range(0, 1)) = 0
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

}

SubShader
{
Tags
{
"Queue" = "Transparent"
"IgnoreProjector" = "True"
"RenderType" = "Transparent"
"PreviewType" = "Plane"
"CanUseSpriteAtlas" = "True"

}

Cull Off
Lighting Off
ZWrite Off
Blend SrcAlpha OneMinusSrcAlpha


CGPROGRAM

#pragma surface surf Lambert vertex:vert  nolightmap nodynlightmap keepalpha noinstancing
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnitySprites.cginc"
struct Input
{
float2 uv_MainTex;
float4 color;
float3 worldPos;
};

float _SpriteFade;
float RotationUV_Rotation_1;
float RotationUV_Rotation_PosX_1;
float RotationUV_Rotation_PosY_1;
float RotationUV_Rotation_Speed_1;
float AnimatedMouvementUV_X_1;
float AnimatedMouvementUV_Y_1;
float AnimatedMouvementUV_Speed_1;
float AnimatedOffsetUV_X_1;
float AnimatedOffsetUV_Y_1;
float AnimatedOffsetUV_ZoomX_1;
float AnimatedOffsetUV_ZoomY_1;
float AnimatedOffsetUV_Speed_1;
sampler2D _NewTex_1;
float _ShinyFX_Pos_1;
float _ShinyFX_Size_1;
float _ShinyFX_Smooth_1;
float _ShinyFX_Intensity_1;
float _ShinyFX_Speed_1;
float _OperationBlend_Fade_1;

void vert(inout appdata_full v, out Input o)
{
v.vertex.xy *= _Flip.xy;
#if defined(PIXELSNAP_ON)
v.vertex = UnityPixelSnap (v.vertex);
#endif
UNITY_INITIALIZE_OUTPUT(Input, o);
o.worldPos = mul(unity_ObjectToWorld, v.vertex);
o.color = v.color * _Color * _RendererColor;
}


float2 AnimatedOffsetUV(float2 uv, float offsetx, float offsety, float zoomx, float zoomy, float speed)
{
speed *=_Time*25;
uv += float2(offsetx*speed, offsety*speed);
uv = fmod(uv * float2(zoomx, zoomy), 1);
return uv;
}
float2 RotationUV(float2 uv, float rot, float posx, float posy, float speed)
{
rot=rot+(_Time*speed*360);
uv = uv - float2(posx, posy);
float angle = rot * 0.01744444;
float sinX = sin(angle);
float cosX = cos(angle);
float2x2 rotationMatrix = float2x2(cosX, -sinX, sinX, cosX);
uv = mul(uv, rotationMatrix) + float2(posx, posy);
return uv;
}
float4 OperationBlend(float4 origin, float4 overlay, float blend)
{
float4 o = origin; 
o.a = overlay.a + origin.a * (1 - overlay.a);
o.rgb = (overlay.rgb * overlay.a + origin.rgb * origin.a * (1 - overlay.a)) * (o.a+0.0000001);
o.a = saturate(o.a);
o = lerp(origin, o, blend);
return o;
}
float2 AnimatedMouvementUV(float2 uv, float offsetx, float offsety, float speed)
{
speed *=_Time*50;
uv += float2(offsetx, offsety)*speed;
uv = fmod(uv,1);
return uv;
}
float4 ShinyFX(float4 txt, float2 uv, float pos, float size, float smooth, float intensity, float speed)
{
pos = pos + 0.5+sin(_Time*20*speed)*0.5;
uv = uv - float2(pos, 0.5);
float a = atan2(uv.x, uv.y) + 1.4, r = 3.1415;
float d = cos(floor(0.5 + a / r) * r - a) * length(uv);
float dist = 1.0 - smoothstep(size, size + smooth, d);
txt.rgb += dist*intensity;
return txt;
}
void surf(Input i, inout SurfaceOutput o)
{
float2 RotationUV_1 = RotationUV(i.uv_MainTex,RotationUV_Rotation_1,RotationUV_Rotation_PosX_1,RotationUV_Rotation_PosY_1,RotationUV_Rotation_Speed_1);
float2 AnimatedMouvementUV_1 = AnimatedMouvementUV(RotationUV_1,AnimatedMouvementUV_X_1,AnimatedMouvementUV_Y_1,AnimatedMouvementUV_Speed_1);
float2 AnimatedOffsetUV_1 = AnimatedOffsetUV(AnimatedMouvementUV_1,AnimatedOffsetUV_X_1,AnimatedOffsetUV_Y_1,AnimatedOffsetUV_ZoomX_1,AnimatedOffsetUV_ZoomY_1,AnimatedOffsetUV_Speed_1);
float4 NewTex_1 = tex2D(_NewTex_1, i.uv_MainTex);
float4 _ShinyFX_1 = ShinyFX(NewTex_1,AnimatedOffsetUV_1,_ShinyFX_Pos_1,_ShinyFX_Size_1,_ShinyFX_Smooth_1,_ShinyFX_Intensity_1,_ShinyFX_Speed_1);
float4 OperationBlend_1 = OperationBlend(_ShinyFX_1, float4(1,1,0,1), _OperationBlend_Fade_1); 
float4 FinalResult = OperationBlend_1;
o.Albedo = FinalResult.rgb* i.color.rgb;
o.Alpha = FinalResult.a * _SpriteFade * i.color.a;
clip(o.Alpha - 0.05);
}

ENDCG
}
Fallback "Sprites /Default"
}
