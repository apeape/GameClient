float4x4 World;
float4x4 View;
float4x4 Projection;
float4 lightDirection = {-0.7, 1, 1, 0};

texture ColorMap;
sampler ColorMapSampler = sampler_state
{
   Texture = <ColorMap>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
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
    float3 Normal : TEXCOORD1;
    float3 worldPosition : TEXCOORD2;

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

    //output.worldPosition = worldPosition;
	output.worldPosition = input.Position.xyz / input.Position.w;
    //output.Normal = mul(input.Normal, worldPosition);
	output.Normal = normalize(mul(input.Normal, worldPosition));

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
    float scale = 0.03f;

    float tighten = 0.4679f;
    float4 mXY = abs(input.Normal.z) - tighten;
    float4 mXZ = abs(input.Normal.y) - tighten;
    float4 mYZ = abs(input.Normal.x) - tighten;

    float total = mXY + mXZ + mYZ;
    mXY /= total;
    mXZ /= total;
    mYZ /= total;

    float4 cXY = tex2D(ColorMapSampler, input.worldPosition.xy * scale);
    float4 cXZ = tex2D(ColorMapSampler, input.worldPosition.xz * scale);
    float4 cYZ = tex2D(ColorMapSampler, input.worldPosition.yz * scale);

	// directional + ambient light
	float4 light = normalize(lightDirection);
	//float4 light = normalize(-lightDirection);
	float ldn = dot(light, input.Normal);
	ldn = max(0, ldn);

	float ambient = 0.2f;
    return cXY*mXY + cXZ*mXZ + cYZ*mYZ  * (ambient + ldn);
    //return float4(1, 0, 0, 1);
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
