using UnityEngine;
using System.Collections;

public static class Noise {

	public enum NormalizeMode {Local, Global};

	public static float[,] GenerateNoiseMap(int Width, int Height, int Seed, float Scale, int Octaves, float Persistance, float Lacunarity, Vector2 Offset, NormalizeMode normalizeMode) {
		float[,] NoiseMap = new float[Width,Height];

		System.Random prng = new System.Random (Seed);
		Vector2[] octaveOffsets = new Vector2[Octaves];

		float maxPossibleHeight = 0;
		float amplitude = 1;
		float frequency = 1;

		for (int i = 0; i < Octaves; i++) {
			float OffsetX = prng.Next (-100000, 100000) + Offset.x;
			float OffsetY = prng.Next (-100000, 100000) - Offset.y;
			octaveOffsets [i] = new Vector2 (OffsetX, OffsetY);

			maxPossibleHeight += amplitude;
			amplitude *= Persistance;
		}

		if (Scale <= 0) {
			Scale = 0.0001f;
		}

		float maxLocalNoiseHeight = float.MinValue;
		float minLocalNoiseHeight = float.MaxValue;

		float halfWidth = Width / 2f;
		float halfHeight = Height / 2f;


		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width; x++) {

				amplitude = 1;
				frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < Octaves; i++) {
					float sampleX = (x-halfWidth + octaveOffsets[i].x) / Scale * frequency;
					float sampleY = (y-halfHeight + octaveOffsets[i].y) / Scale * frequency;

					float PerlinNoiseAmmount = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += PerlinNoiseAmmount * amplitude;

					amplitude *= Persistance;
					frequency *= Lacunarity;
				}

				if (noiseHeight > maxLocalNoiseHeight) {
					maxLocalNoiseHeight = noiseHeight;
				} else if (noiseHeight < minLocalNoiseHeight) {
					minLocalNoiseHeight = noiseHeight;
				}
				NoiseMap [x, y] = noiseHeight;
			}
		}

		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width; x++) {
				if (normalizeMode == NormalizeMode.Local) {
					NoiseMap [x, y] = Mathf.InverseLerp (minLocalNoiseHeight, maxLocalNoiseHeight, NoiseMap [x, y]);
				} else {
					float normalizedHeight = (NoiseMap [x, y] + 1) / (maxPossibleHeight/0.9f);
					NoiseMap [x, y] = Mathf.Clamp(normalizedHeight,0, int.MaxValue);
				}
			}
		}

		return NoiseMap;
	}

}
