// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FFF"
{
	Properties
	{
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_Vector1("Vector 1", Vector) = (0,0,0,0)
		_Vector2("Vector 2", Vector) = (0,0,0,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float2 _Vector0;
		uniform float3 _Vector1;
		uniform float4 _Vector2;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner15 = ( _Time.y * _Vector0 + i.uv_texcoord);
			o.Albedo = ( tex2D( _TextureSample0, panner15 ) * float4( _Vector1 , 0.0 ) ).rgb;
			float3 temp_cast_2 = (_Vector2.x).xxx;
			o.Emission = temp_cast_2;
			o.Metallic = _Vector2.y;
			o.Smoothness = _Vector2.z;
			o.Alpha = _Vector2.w;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16301
909.3334;72.66667;796;847;1295.667;1005.695;3.161693;True;False
Node;AmplifyShaderEditor.Vector2Node;12;-269.8111,96.35919;Float;False;Property;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;0,0;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-277.8111,-54.04079;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;16;-303.4111,264.3591;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;15;-37.81109,123.5592;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;18;181.3887,70.75919;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;None;84508b93f15f2b64386ec07486afc7a3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;13;80.58881,341.1593;Float;False;Property;_Vector1;Vector 1;1;0;Create;True;0;0;False;0;0,0,0;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;306.189,361.9594;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector4Node;14;80.58888,640.3592;Float;False;Property;_Vector2;Vector 2;2;0;Create;True;0;0;False;0;0,0,0,0;0.2,0.5,0.5,0.8;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;535.3384,492.3666;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;FFF;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;19;0
WireConnection;15;2;12;0
WireConnection;15;1;16;2
WireConnection;18;1;15;0
WireConnection;17;0;18;0
WireConnection;17;1;13;0
WireConnection;0;0;17;0
WireConnection;0;2;14;1
WireConnection;0;3;14;2
WireConnection;0;4;14;3
WireConnection;0;9;14;4
ASEEND*/
//CHKSM=1233658F19C36225298FA5320D6128DF351E106B