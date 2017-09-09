// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MyCollection/Transparent1"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_TextureSample0("Texture Sample 0", 2D) = "bump" {}
		_Distortion("Distortion", Range( 0 , 1)) = 50
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		GrabPass{ "_GrabScreen0" }
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float4 screenPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _GrabScreen0;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Distortion;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPos3 = ase_screenPos;
			ase_screenPos3.xyzw /= ase_screenPos3.w;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Albedo = tex2Dproj( _GrabScreen0, UNITY_PROJ_COORD( ( ase_screenPos3 + ( tex2D( _TextureSample0, uv_TextureSample0 ) * _Distortion ) ) ) ).rgb;
			o.Alpha = 0.5;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=10001
284;613;1304;763;740.9044;108.735;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;6;-632.49,301.965;Float;False;Property;_Distortion;Distortion;1;0;50;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-1050.3,57.255;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;None;True;0;True;bump;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ScreenPosInputsNode;3;-712,-151.5;Float;False;0;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-697.6949,78.40488;Float;False;2;0;FLOAT4;0.0;False;1;FLOAT;0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-483,-8.5;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.ScreenColorNode;1;-300,-61.5;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Object;-1;1;0;FLOAT4;0,0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;7;-167.6843,288.7449;Float;False;Constant;_Float0;Float 0;2;0;0.5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;55.04998,-40.36999;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;MyCollection/Transparent1;False;False;False;False;False;False;False;False;True;True;True;True;Back;0;0;False;0;0;Transparent;0.5;True;False;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;-1;-1;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;2;0
WireConnection;5;1;6;0
WireConnection;4;0;3;0
WireConnection;4;1;5;0
WireConnection;1;0;4;0
WireConnection;0;0;1;0
WireConnection;0;9;7;0
ASEEND*/
//CHKSM=593BF4D78F660AB5FA5AF47CC875725253D59625