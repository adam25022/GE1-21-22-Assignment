using UnityEngine;
using System.Collections;

// no reason to inherit from monobehavour since it doesnt give us anything.
// only want 1 instance of this script so i make it static.
public static class Noise {

	public enum NormalizeMode {Local, Global};

	//here we want a method to generate the noise map that we will be using.
	// it will return a 2d array of values between 0 and 1, if the height and width are 10, it will return 100 in a 10x10 grid.
	// we pass in the width and height, which is essentially the horizontal and verticle co-ordinates of the grid,
	// the scale value is to make it so that we dont just get 1-height in the loops.
	//
	public static float[,] GenerateNoiseMap(int Width, int Height, int Seed, float Scale, int NumberOfOctaves, float Persistance, float Lacunarity, Vector2 Offset, NormalizeMode normalizeMode) {
		//new 2d array with the sizes height and width.
		float[,] PerlinNoise = new float[Width,Height];

		//this is the value for setting the seed. will return a procedural noisemap based on the seed.
		System.Random prng = new System.Random (Seed);
		Vector2[] octaveOffsets = new Vector2[NumberOfOctaves];

		float maxPossibleHeight = 0;
		float amplitude = 1;
		float frequency = 1;
		// these will be used to reset the values to be between 0 and 1 later.
		float maxLocalNoiseHeight = float.MinValue;
		float minLocalNoiseHeight = float.MaxValue;
		
		//half the size of the map
		float halfWidth = Width / 2f;
		float halfHeight = Height / 2f;

		// this just sets it however much offset it has been set. if its above 100000 or below -100000 it just breaks so we limit that.
		for (int i = 0; i < NumberOfOctaves; i++) {
			float OffsetX = prng.Next (-100000, 100000) + Offset.x;
			float OffsetY = prng.Next (-100000, 100000) - Offset.y;
			octaveOffsets [i] = new Vector2 (OffsetX, OffsetY);

			maxPossibleHeight += amplitude;
			amplitude *= Persistance;
		}
		//if scale is 0 we would get a division by 1 error so we need to make sure its just slightly more than 0
		if (Scale <= 0) {
			Scale = 0.00000001f;
		}

		// this is the actual script that makes the  noisemap.
		// as its a 2d co-ordinate system we use the x and y co-ordinates as the square that we are at. 
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width; x++) {

				amplitude = 1;
				frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < NumberOfOctaves; i++) {
					//here without the scale it would just be the integers for x and y and not a float
					// We want our frequency to take effect so we multiply by frequency too.
					//higher frequency means less samples which means more rapid changes in landscape. 
					float sampleX = (x-halfWidth + octaveOffsets[i].x) / Scale * frequency;
					float sampleY = (y-halfHeight + octaveOffsets[i].y) / Scale * frequency;
					
					// this uses the perlin noise function from mathf, and creates the noise at that square using 
					// the calculated value for x above and the calculated value for y.
					float PerlinNoiseAmmount = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					//we increase the noiseheight by the amount of perlin noise
					noiseHeight += PerlinNoiseAmmount * amplitude;

					//at the end of each octave the amplitude is multiplied by the persistance(0-1)
					// amplitude decreases each octave
					amplitude *= Persistance;
					// frequency increases or stays the same each octave as lacunarity is bigger or equal to 1
					frequency *= Lacunarity;
				}
				//this just normalizes everything so that the values are back in the range 0 to 1 so that it works 
				// with the noisemap(1 being white and 0 being black, or any other colours we will change them to in the future)
				if (noiseHeight > maxLocalNoiseHeight) {
					maxLocalNoiseHeight = noiseHeight;
				} else if (noiseHeight < minLocalNoiseHeight) {
					minLocalNoiseHeight = noiseHeight;
				}
				PerlinNoise [x, y] = noiseHeight;
			}
		}
		//same loop as above for x and y
		// as its a 2d co-ordinate system we use the x and y co-ordinates as the square that we are at. 
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width; x++) {

				if (normalizeMode == NormalizeMode.Local) {
					// for each value in the noisemap set it to be the value between 0 and 1 that it is equal to in max and min noiseheight. 
					// e.g. if min is 0 and max is 100, if the value is 50 it will return 0.5 and if the value is 100 it will return 1
					PerlinNoise [x, y] = Mathf.InverseLerp (minLocalNoiseHeight, maxLocalNoiseHeight, PerlinNoise [x, y]);
				} else {
					float normalizedHeight = (PerlinNoise [x, y] + 1) / (maxPossibleHeight/0.9f);
					PerlinNoise [x, y] = Mathf.Clamp(normalizedHeight,0, int.MaxValue);
				}
			}
		}
		// this then passes the noise map to the function that called it.
		return PerlinNoise;
	}

}
