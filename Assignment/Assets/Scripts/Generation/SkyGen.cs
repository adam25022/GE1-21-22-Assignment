using UnityEngine;
using System.Collections;

public class SkyGen : MonoBehaviour {

	public enum DrawMode {PerlinNoise, SkyMap};
	public DrawMode drawMode;

	public Noise.NormalizeMode normalizeMode;
	//this maxes out at 500 as anything more essentially grinds unity to a halt with 1fps
	[Range(0,500)]
	public const int SizeOfSky = 400;
	public float noiseScale;
	float OffsetUpdate=0;
	public int Octaves;
	[Range(0,1)]
	public float Persistance;
	public float Lacunarity;
	public float SpeedOfSky;
	public int Seed;
	public Vector2 Offset;
	int frames=0;
	public bool autoUpdate;

	public TerrainType[] regions;
	
	
	public void DrawMapInEditor() {
		SkyData skyData = GenerateSkyData (Vector2.zero);

		Display Display = FindObjectOfType<Display> ();
		if (drawMode == DrawMode.PerlinNoise) {
			Display.CreateTexture (TextureGenerator.TextureFromHeightMap (skyData.heightMap));
		} else if (drawMode == DrawMode.SkyMap) {
			Display.CreateTexture (TextureGenerator.TextureFromWorldMap (skyData.SkyMap, SizeOfSky, SizeOfSky));
		}
	}

	SkyData GenerateSkyData(Vector2 centre) {
		float[,] PerlinNoise = Noise.GenerateNoiseMap (SizeOfSky, SizeOfSky, Seed, noiseScale, Octaves, Persistance, Lacunarity, centre + Offset, normalizeMode);

		Color[] SkyMap = new Color[SizeOfSky * SizeOfSky];
		for (int y = 0; y < SizeOfSky; y++) {
			for (int x = 0; x < SizeOfSky; x++) {
				float CurrentHeight = PerlinNoise [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (CurrentHeight >= regions [i].Height) {
						SkyMap [y * SizeOfSky + x] = regions [i].Color;
					} else {
						break;
					}
				}
			}
		}


		return new SkyData (PerlinNoise, SkyMap);
	}

	void OnValidate() {
		if (Lacunarity < 1) {
			Lacunarity = 1;
		}
		if (Octaves < 0) {
			Octaves = 0;
		}
		if (SpeedOfSky < 0) {
			SpeedOfSky = 0;
		}

	}

	void LateUpdate()
    {	
		frames++;
        OffsetUpdate=OffsetUpdate+(SpeedOfSky/100);
		Offset.Set(OffsetUpdate, 0);
		if(frames%3==0)
		{
			DrawMapInEditor ();
		}
    }
}

public struct SkyData {
	public readonly float[,] heightMap;
	public readonly Color[] SkyMap;

	public SkyData (float[,] heightMap, Color[] SkyMap)
	{
		this.heightMap = heightMap;
		this.SkyMap = SkyMap;
	}
}


