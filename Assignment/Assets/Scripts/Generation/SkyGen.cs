using UnityEngine;
using System.Collections;

public class SkyGen : MonoBehaviour {

	public enum DrawMode {PerlinNoise, SkyMap, FalloffMap};
	public DrawMode drawMode;

	public Noise.NormalizeMode normalizeMode;

	public const int SizeOfMap = 400;
	[Range(0,6)]
	public int editorPreviewLOD;
	public float noiseScale;
	float OffsetUpdate=0;
	public int Octaves;
	[Range(0,1)]
	public float Persistance;
	public float Lacunarity;

	public int Seed;
	public Vector2 Offset;

	public bool useFalloff;

	int frames=0;
	public bool autoUpdate;

	public TerrainType[] regions;

	float[,] falloffMap;
	
	void Awake() {
		falloffMap = FallOffMap.GenerateFalloffMap (SizeOfMap);
	}

	public void DrawMapInEditor() {
		SkyData skyData = GenerateSkyData (Vector2.zero);

		SkyDisplay Display = FindObjectOfType<SkyDisplay> ();
		if (drawMode == DrawMode.PerlinNoise) {
			Display.CreateTexture (TextureGenerator.TextureFromHeightMap (skyData.heightMap));
		} else if (drawMode == DrawMode.SkyMap) {
			Display.CreateTexture (TextureGenerator.TextureFromWorldMap (skyData.SkyMap, SizeOfMap, SizeOfMap));
		} else if (drawMode == DrawMode.FalloffMap) {
			Display.CreateTexture(TextureGenerator.TextureFromHeightMap(FallOffMap.GenerateFalloffMap(SizeOfMap)));
		}
	}

	SkyData GenerateSkyData(Vector2 centre) {
		float[,] PerlinNoise = Noise.GenerateNoiseMap (SizeOfMap, SizeOfMap, Seed, noiseScale, Octaves, Persistance, Lacunarity, centre + Offset, normalizeMode);

		Color[] SkyMap = new Color[SizeOfMap * SizeOfMap];
		for (int y = 0; y < SizeOfMap; y++) {
			for (int x = 0; x < SizeOfMap; x++) {
				if (useFalloff) {
					PerlinNoise [x, y] = Mathf.Clamp01(PerlinNoise [x, y] - falloffMap [x, y]);
				}
				float CurrentHeight = PerlinNoise [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (CurrentHeight >= regions [i].Height) {
						SkyMap [y * SizeOfMap + x] = regions [i].Color;
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

		falloffMap = FallOffMap.GenerateFalloffMap (SizeOfMap);
	}

	void LateUpdate()
    {	
		frames++;
        OffsetUpdate=OffsetUpdate+0.02f;
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


