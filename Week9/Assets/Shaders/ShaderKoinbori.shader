// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "KoinoboriShader"
{
	Properties
	{
		_SpecColor("Specular Color",Color)=(1,1,1,1)
		_Albedo("Albedo", 2D) = "white" {}
		_Gloss("Gloss", Float) = 0
		_Specular("Specular", Float) = 0
		_Factor("Factor", Float) = 0
		_FactorSuf("FactorSuf", Float) = 0
		_FinPhaseFactor("FinPhaseFactor", Float) = 0
		_Amplitude("Amplitude", Float) = 0
		_AmplitudeSuf("AmplitudeSuf", Float) = 0
		_Frquency("Frquency", Float) = 0
		_FreqSurf("FreqSurf", Float) = 0
		_FinFrequency("FinFrequency", Float) = 2
		_Adjust("Adjust", Float) = 0
		_AdjustFur("AdjustFur", Float) = 0
		_FinAmplitude("FinAmplitude", Float) = 1
		_SideFaceCurve("SideFaceCurve", Float) = 1
		_PitchCurve("PitchCurve", Float) = 1
		_SideFaceCurveMid("SideFaceCurveMid", Float) = 0
		_Float4("Float 4", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 5.0
		#pragma surface surf BlinnPhong keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Frquency;
		uniform float _Factor;
		uniform float _Amplitude;
		uniform float _Adjust;
		uniform float _Float4;
		uniform float _PitchCurve;
		uniform float _FinFrequency;
		uniform float _FinAmplitude;
		uniform float _FinPhaseFactor;
		uniform float _FreqSurf;
		uniform float _FactorSuf;
		uniform float _AmplitudeSuf;
		uniform float _AdjustFur;
		uniform float _SideFaceCurveMid;
		uniform float _SideFaceCurve;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _Specular;
		uniform float _Gloss;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float temp_output_17_0 = ( ( _Frquency * _Time.y ) + ( ase_vertex3Pos.x * _Factor ) );
			float temp_output_21_0 = ( ase_vertex3Pos.x * _Adjust );
			float4 appendResult10 = (float4(0.0 , ( ( sin( temp_output_17_0 ) * _Amplitude * temp_output_21_0 ) + ( pow( ( ase_vertex3Pos.x - _Float4 ) , 2.0 ) * _PitchCurve ) ) , ( ( cos( temp_output_17_0 ) * _Amplitude * temp_output_21_0 ) + ( v.color.r * sin( ( _FinFrequency * _Time.y ) ) * _FinAmplitude * 0.01 * ( ase_vertex3Pos.y * _FinPhaseFactor ) ) + ( sin( ( ( _FreqSurf * _Time.y ) + ( ase_vertex3Pos.x * _FactorSuf ) ) ) * _AmplitudeSuf * ( ase_vertex3Pos.x * _AdjustFur ) ) + ( ( pow( ( ase_vertex3Pos.x - _SideFaceCurveMid ) , 2.0 ) - pow( _SideFaceCurveMid , 2.0 ) ) * _SideFaceCurve ) ) , 0.0));
			v.vertex.xyz += appendResult10.xyz;
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = tex2D( _Albedo, uv_Albedo ).rgb;
			o.Specular = _Specular;
			o.Gloss = _Gloss;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
-1105.333;587;1066;559;379.3712;-301.443;1.619006;True;False
Node;AmplifyShaderEditor.CommentaryNode;63;-1875.311,-1415.559;Float;False;1077.746;721.9631;Comment;13;50;51;53;55;54;52;60;56;62;58;57;61;59;Curface Crinkle;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;73;-762.6096,671.5609;Float;False;897.5938;379.8691;turn on the side face;8;85;72;66;71;67;69;68;84;Side Curve;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;48;-1869.751,-497.2827;Float;False;997.3438;806.6014;wave;15;14;15;4;19;16;18;17;20;22;38;12;21;9;39;11;Overall Wave Circle;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-722.2047,906.6082;Float;False;Property;_SideFaceCurveMid;SideFaceCurveMid;17;0;Create;True;0;0;False;0;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1739.324,-447.2829;Float;False;Property;_Frquency;Frquency;9;0;Create;True;0;0;False;0;0;1.98;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;51;-1825.311,-1286.079;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;14;-1768.774,-367.8029;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;80;163.9922,655.5895;Float;False;913.3228;394.5782;Comment;8;77;76;75;88;87;79;78;74;Pitch Curve;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-1795.861,-1365.559;Float;False;Property;_FreqSurf;FreqSurf;10;0;Create;True;0;0;False;0;0;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;53;-1667.487,-1217.636;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-1786.125,-0.08271217;Float;False;Property;_Factor;Factor;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1633.86,-1071.962;Float;False;Property;_FactorSuf;FactorSuf;5;0;Create;True;0;0;False;0;0;50;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;47;-1894.422,488.058;Float;False;997.2874;783.258;Fin;11;30;31;41;42;33;43;45;36;27;25;24;Fin;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;66;-742.7759,728.7632;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;4;-1819.751,-159.3189;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1574.222,-447.2829;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-1394.097,-1167.155;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-1808.668,747.5789;Float;False;Property;_FinFrequency;FinFrequency;11;0;Create;True;0;0;False;0;2;30;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;74;213.9922,705.5895;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-1630.76,-1365.559;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;31;-1809.24,840.5361;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;195.8757,882.8544;Float;False;Property;_Float4;Float 4;18;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;71;-552.3453,734.2914;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1559.922,-140.4828;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;85;-441.423,903.5969;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-1478.067,-808.5952;Float;False;Property;_AdjustFur;AdjustFur;13;0;Create;True;0;0;False;0;0;2.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-1392.221,-328.9829;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;75;424.3673,708.9916;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;67;-399.7756,738.1619;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1530.177,194.3185;Float;False;Property;_Adjust;Adjust;12;0;Create;True;0;0;False;0;0;8.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-1810.795,1156.317;Float;False;Property;_FinPhaseFactor;FinPhaseFactor;6;0;Create;True;0;0;False;0;0;300;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;41;-1844.422,997.0801;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;60;-1592.662,-960.1169;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-1318.771,-1305.201;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;20;-1589.727,7.900414;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-1620.462,779.8231;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-1303.092,1096.922;Float;False;Property;_FinAmplitude;FinAmplitude;14;0;Create;True;0;0;False;0;1;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1345.266,-181.8824;Float;False;Property;_Amplitude;Amplitude;7;0;Create;True;0;0;False;0;0;0.015;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-1285.19,-897.412;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;9;-1241.052,-288.3658;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;24;-1510.75,538.0581;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;76;570.278,713.2031;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;595.3524,909.8207;Float;False;Property;_PitchCurve;PitchCurve;16;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;57;-1141.863,-1292.238;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;36;-1422.754,774.348;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;38;-1234.706,-396.5977;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-1262.515,-1089.831;Float;False;Property;_AmplitudeSuf;AmplitudeSuf;8;0;Create;True;0;0;False;0;0;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-1093.135,942.6682;Float;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;84;-236.1395,737.3823;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1337.311,0.6012554;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-233.4921,912.9971;Float;False;Property;_SideFaceCurve;SideFaceCurve;15;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-1590.37,1012.066;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1045.023,-316.8542;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-969.5646,-1153.723;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;83;-390.9594,57.8273;Float;False;205.3333;183;Comment;1;81;Y;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;82;-665.5713,295.0204;Float;False;206.6667;229;Comment;1;40;Z;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;922.0204,720.0884;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-53.89632,729.6617;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1224.467,615.392;Float;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1022.886,-83.80959;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;49;69.30313,-634.1371;Float;False;374;433.5642;Comment;3;8;1;6;Texture;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;81;-340.9594,107.8273;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-618.1691,358.01;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;88;770.0835,719.0336;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;87;400.3669,889.4874;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;137.7119,-388.642;Float;False;Property;_Specular;Specular;3;0;Create;True;0;0;False;0;0;0.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;125.8936,-315.5728;Float;False;Property;_Gloss;Gloss;2;0;Create;True;0;0;False;0;0;-1.43;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;10;-73.13467,131.2513;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;1;119.3032,-584.1371;Float;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;False;0;None;bdfdd9b6f86ff814b920b7d66bd04f9c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;65;568.6715,-99.35721;Float;False;True;7;Float;ASEMaterialInspector;0;0;BlinnPhong;KoinoboriShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;0;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;15;0
WireConnection;16;1;14;0
WireConnection;54;0;53;1
WireConnection;54;1;55;0
WireConnection;52;0;50;0
WireConnection;52;1;51;0
WireConnection;71;0;66;1
WireConnection;71;1;72;0
WireConnection;18;0;4;1
WireConnection;18;1;19;0
WireConnection;85;0;72;0
WireConnection;17;0;16;0
WireConnection;17;1;18;0
WireConnection;75;0;74;1
WireConnection;75;1;79;0
WireConnection;67;0;71;0
WireConnection;56;0;52;0
WireConnection;56;1;54;0
WireConnection;33;0;30;0
WireConnection;33;1;31;0
WireConnection;61;0;60;1
WireConnection;61;1;62;0
WireConnection;9;0;17;0
WireConnection;76;0;75;0
WireConnection;57;0;56;0
WireConnection;36;0;33;0
WireConnection;38;0;17;0
WireConnection;84;0;67;0
WireConnection;84;1;85;0
WireConnection;21;0;20;1
WireConnection;21;1;22;0
WireConnection;43;0;41;2
WireConnection;43;1;42;0
WireConnection;39;0;38;0
WireConnection;39;1;12;0
WireConnection;39;2;21;0
WireConnection;59;0;57;0
WireConnection;59;1;58;0
WireConnection;59;2;61;0
WireConnection;77;0;76;0
WireConnection;77;1;78;0
WireConnection;68;0;84;0
WireConnection;68;1;69;0
WireConnection;25;0;24;1
WireConnection;25;1;36;0
WireConnection;25;2;27;0
WireConnection;25;3;45;0
WireConnection;25;4;43;0
WireConnection;11;0;9;0
WireConnection;11;1;12;0
WireConnection;11;2;21;0
WireConnection;81;0;11;0
WireConnection;81;1;77;0
WireConnection;40;0;39;0
WireConnection;40;1;25;0
WireConnection;40;2;59;0
WireConnection;40;3;68;0
WireConnection;88;1;87;0
WireConnection;87;0;79;0
WireConnection;10;1;81;0
WireConnection;10;2;40;0
WireConnection;65;0;1;0
WireConnection;65;3;6;0
WireConnection;65;4;8;0
WireConnection;65;11;10;0
ASEEND*/
//CHKSM=87DA8A0EB59BB44948AB626440DFEC4C970E8765