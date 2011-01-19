float4x4 World;
float4x4 View;
float4x4 Projection;
float4 lightDirection = { 1, -0.7, 1, 0};
float textureScale;

texture ColorMap;
sampler ColorMapSampler = sampler_state
{
   Texture = <ColorMap>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = Linear;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};


// TODO: add effect parameters here.

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float3 Normal : TEXCOORD0;
    float3 worldPosition : TEXCOORD1;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	output.worldPosition = input.Position.xyz / input.Position.w;
	output.Normal = normalize(input.Normal);

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 absNormal = abs(input.Normal);
	float3 blend_weights = absNormal;
	//blend_weights = blend_weights - 0.2679f;
	blend_weights = max(blend_weights, 0);
	// force sum to 1.0
	blend_weights /= (blend_weights.x + blend_weights.y + blend_weights.z).xxx;

	float4 blended_color;
	//float tex_scale = 0.05f;

	float2 coord1 = input.worldPosition.yz * textureScale;
	float2 coord2 = input.worldPosition.zx * textureScale;
	float2 coord3 = input.worldPosition.xy * textureScale;

	float4 col1 = tex2D(ColorMapSampler, coord1); //* 0.01 + float4(1.0,0.0,0.0,1.0); // uncomment to see the blending in red/green/blue only
	float4 col2 = tex2D(ColorMapSampler, coord2); //* 0.01 + float4(0.0,1.0,0.0,1.0);
	float4 col3 = tex2D(ColorMapSampler, coord3); //* 0.01 + float4(0.0,0.0,1.0,1.0);

	blended_color = col1.xyzw * blend_weights.xxxx +  
					col2.xyzw * blend_weights.yyyy +  
					col3.xyzw * blend_weights.zzzz;

	// directional lighting
	float4 light = -normalize(lightDirection);
	float ldn = max(0, dot(light, input.Normal));
	float ambient = 0.2f;

	return blended_color * (ambient + ldn);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
