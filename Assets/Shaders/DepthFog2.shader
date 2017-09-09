// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MyCollection/DepthFog2"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_FogIntensity("FogIntensity", Range( 0 , 1)) = 0
		_FogMaxIntensity("FogMaxIntensity", Range( 0 , 1)) = 0
		_FogColor("FogColor", Color) = (1,1,1,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float2 texcoord_0;
			float4 screenPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _FogColor;
		uniform sampler2D _TextureSample1;
		uniform sampler2D _CameraDepthTexture;
		uniform float _FogIntensity;
		uniform float _FogMaxIntensity;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_output_15_0 = (abs( i.texcoord_0+_CosTime.y * float2(0.05,0.02 )));
			o.Albedo = ( tex2D( _TextureSample0, temp_output_15_0 ) * _FogColor ).rgb;
			o.Emission = clamp( ( tex2D( _TextureSample1, temp_output_15_0 ) * float4( 0,0,0,0 ) ) , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) ).xyz;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPos2 = ase_screenPos;
			ase_screenPos2.xyzw /= ase_screenPos2.w;
			float eyeDepth3 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos2))));
			o.Alpha = clamp( ( abs( ( eyeDepth3 - ase_screenPos2.w ) ) * _FogIntensity ) , 0.0 , _FogMaxIntensity );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=10001
1166;644;1304;756;1702.208;845.9296;1.62;True;True
Node;AmplifyShaderEditor.ScreenPosInputsNode;2;-1069.38,-48.58994;Float;False;0;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CosTime;22;-1398.44,-508.1558;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1577.774,-712.3331;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ScreenDepthNode;3;-812.3205,-114.5999;Float;False;0;1;0;FLOAT4;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.PannerNode;15;-1175.374,-705.0735;Float;False;0.05;0.02;2;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-614.8807,26.88007;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.AbsOpNode;6;-420.2303,32.21006;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;19;-749.5246,-310.87;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;7;-775.5197,157.87;Float;False;Property;_FogIntensity;FogIntensity;0;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;11;-792.0341,-495.5144;Float;False;Property;_FogColor;FogColor;2;0;1,1,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;9;-619.5997,453.4503;Float;False;Property;_FogMaxIntensity;FogMaxIntensity;1;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-186.2498,28.40009;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;13;-859.7651,-732.6238;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-399.8449,-204.8498;Float;False;2;0;FLOAT4;0.0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ClampOpNode;10;-176.7398,262.2704;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-343.5144,-420.0339;Float;False;2;0;FLOAT4;0.0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.ClampOpNode;21;-228.7245,-178.8098;Float;False;3;0;FLOAT4;0.0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT4;1,1,1,1;False;1;FLOAT4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;15,-198;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;MyCollection/DepthFog2;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Transparent;0.5;True;False;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;-1;-1;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;15;0;14;0
WireConnection;15;1;22;2
WireConnection;4;0;3;0
WireConnection;4;1;2;4
WireConnection;6;0;4;0
WireConnection;19;1;15;0
WireConnection;5;0;6;0
WireConnection;5;1;7;0
WireConnection;13;1;15;0
WireConnection;20;0;19;0
WireConnection;10;0;5;0
WireConnection;10;2;9;0
WireConnection;16;0;13;0
WireConnection;16;1;11;0
WireConnection;21;0;20;0
WireConnection;0;0;16;0
WireConnection;0;2;21;0
WireConnection;0;9;10;0
ASEEND*/
//CHKSM=427EC73842C906719A1D6C4832CF759F91F00E3C