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

float4 PixelShaderFunction(float2 texCoord: TEXCOORD0) : COLOR0
{
    float4 color = tex2D(NormalMapSampler, texCoord);
    //color.rgb = dot(color.rgb, float3(0.3, 0.59, 0.11)); 
    return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
