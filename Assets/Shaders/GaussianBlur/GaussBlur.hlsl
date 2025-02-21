// # ifndef MYHLSLINCLUDE_INCLUDED
// # define MYHLSLINCLUDE_INCLUDED

void GaussianBlur_float(Texture2D tex, SamplerState samplerTex, float2 uv, float2 offset, out float4 Out) {
	float4 color = float4(0, 0, 0, 0);
	float weightSum = 0.0;

	for (int x = -2; x <= 2; x++) {
		for (int y = -2; y <= 2; y++) {
			float2 sampleUV = uv + float2(x, y) * offset;
			float weight = exp(-(x * x + y * y) / (2.0 * 2.0));
			color += tex.Sample(samplerTex, sampleUV) * weight;
			weightSum += weight;
		}
	}

	Out = color / weightSum;
}

// # endif