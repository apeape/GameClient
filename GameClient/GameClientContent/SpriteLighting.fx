float4x4 World; 
float4x4 Projection;

/// <summary>Concatenated world, view and projection matrix</summary>
//float4x4 WorldViewProjection : VIEWPROJECTION;

Texture Diffuse;
Texture NormalMap;
Texture HeightMap;

sampler DiffuseSampler = sampler_state {
	Texture = <Diffuse>;
};
sampler NormalMapSampler = sampler_state {
	Texture = <NormalMap>;
};
sampler HeightMapSampler = sampler_state {
	Texture = <HeightMap>;
};

// TODO: add effect parameters here.

struct VertexShaderInput
{
	/// <summary>Position in world space</summary>
    float3 Position : POSITION0;
	float2 texCoord : TEXCOORD0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
	
	/// <summary>Position of the particle in screen space</summary>
    float4 Position : POSITION0;
	float2 texCoord : TEXCOORD0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	//output.Position = mul(input.Position, WorldViewProjection);
	output.Position = mul(input.Position, World);     
	output.Position = mul(output.Position, Projection);
	output.texCoord = input.texCoord;

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
	return tex2D(DiffuseSampler, input.texCoord);
    //return float4(1, 0, 0, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.
		//AlphaBlendEnable = true;
		//SrcBlend = SrcAlpha;
		//DestBlend = One; // InvSrcAlpha;

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
