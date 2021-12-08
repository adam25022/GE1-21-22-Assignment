using UnityEngine;
using System.Collections;

public static class Noise {

	public static float[,] GenerateNoiseMap(int Width, int Height, int Seed, float Scale, int Octaves, float Persistance, float Lacunarity, Vector2 Offset) {
		float[,] NoiseMap = new float[Width,Height];

		System.Random prng = new System.Random (Seed);
		Vector2[] OctaveOffsetAmmount = new Vector2[Octaves];
		for (int i = 0; i < Octaves; i++) {
			float OffsetX = prng.Next (-100000, 100000) + Offset.x;
			float OffsetY = prng.Next (-100000, 100000) + Offset.y;
			OctaveOffsetAmmount [i] = new Vector2 (OffsetX, OffsetY);
		}

		
		float MaximumNoiseHeight = float.MinValue;
		float MinimumNoiseHeight = float.MaxValue;

		float halfWidth = Width / 2f;
		float halfHeight = Height / 2f;


		if (Scale <= 0) {
			Scale = 0.0001f;
		}

		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width; x++) {
		
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < Octaves; i++) {
					float sampleX = (x-halfWidth) / Scale * frequency + OctaveOffsetAmmount[i].x;
					float sampleY = (y-halfHeight) / Scale * frequency + OctaveOffsetAmmount[i].y;

					float PerlinNoiseAmmount = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += PerlinNoiseAmmount * amplitude;

					amplitude *= Persistance;
					frequency *= Lacunarity;
				}

				if (noiseHeight > MaximumNoiseHeight) {
					MaximumNoiseHeight = noiseHeight;
				} else if (noiseHeight < MinimumNoiseHeight) {
					MinimumNoiseHeight = noiseHeight;
				}
				NoiseMap [x, y] = noiseHeight;
			}
		}

		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width; x++) {
				NoiseMap [x, y] = Mathf.InverseLerp (MinimumNoiseHeight, MaximumNoiseHeight, NoiseMap [x, y]);
			}
		}

		return NoiseMap;
	}

}
