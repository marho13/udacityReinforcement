 Shader "Custom/Distance" {
     Properties {
         _MainTex ("Base (RGB)", 2D) = "white" {}
         _MinColor ("Color in Minimal", Color) = (1, 1, 1, 1)
         _MaxColor ("Color in Maxmal", Color) = (0, 0, 0, 0)
         _MinDistance ("Min Distance", Float) = 0
         _MaxDistance ("Max Distance", Float) = 1000
     }
     SubShader {
         Tags { "RenderType"="Opaque" }
         Lighting Off
         
         
         LOD 200
         
         CGPROGRAM
         #pragma surface surf Lambert
 
         sampler2D _MainTex;
 
         struct Input {
             float3 worldPos;
         };
 
         float _MaxDistance;
         float _MinDistance;
         half4 _MinColor;
         half4 _MaxColor;
 
         void surf (Input IN, inout SurfaceOutput o) {
             float dist = distance(_WorldSpaceCameraPos, IN.worldPos);
             half weight = saturate( (dist - _MinDistance) / (_MaxDistance - _MinDistance) );
             half4 distanceColor = lerp(_MinColor, _MaxColor, weight);
 
             o.Albedo = distanceColor.rgb;
             o.Normal = 0;
         }
         ENDCG
     } 
     FallBack "Diffuse"
 }