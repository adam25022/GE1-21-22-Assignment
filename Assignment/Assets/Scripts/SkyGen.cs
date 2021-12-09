using UnityEngine;
using System.Collections;

public class SkyGen : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColorMap, FalloffMap};
	public DrawMode drawMode;

	public Noise.NormalizeMode normalizeMode;

	public const int MapChunkSize = 400;
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

	
	public bool autoUpdate;

	public TerrainType[] regions;

	float[,] falloffMap;
	void Awake() {
		falloffMap = FallOffMap.GenerateFalloffMap (MapChunkSize);
	}

	public void DrawMapInEditor() {
		SkyData skyData = GenerateSkyData (Vector2.zero);

		SkyDisplay Display = FindObjectOfType<SkyDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			Display.CreateTexture (TextureGenerator.TextureFromHeightMap (skyData.heightMap));
		} else if (drawMode == DrawMode.ColorMap) {
			Display.CreateTexture (TextureGenerator.TextureFromColorMap (skyData.ColorMap, MapChunkSize, MapChunkSize));
		} else if (drawMode == DrawMode.FalloffMap) {
			Display.CreateTexture(TextureGenerator.TextureFromHeightMap(FallOffMap.GenerateFalloffMap(MapChunkSize)));
		}
	}

	SkyData GenerateSkyData(Vector2 centre) {
		float[,] NoiseMap = Noise.GenerateNoiseMap (MapChunkSize, MapChunkSize, Seed, noiseScale, Octaves, Persistance, Lacunarity, centre + Offset, normalizeMode);

		Color[] ColorMap = new Color[MapChunkSize * MapChunkSize];
		for (int y = 0; y < MapChunkSize; y++) {
			for (int x = 0; x < MapChunkSize; x++) {
				if (useFalloff) {
					NoiseMap [x, y] = Mathf.Clamp01(NoiseMap [x, y] - falloffMap [x, y]);
				}
				float CurrentHeight = NoiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (CurrentHeight >= regions [i].Height) {
						ColorMap [y * MapChunkSize + x] = regions [i].Color;
					} else {
						break;
					}
				}
			}
		}


		return new SkyData (NoiseMap, ColorMap);
	}

	void OnValidate() {
		if (Lacunarity < 1) {
			Lacunarity = 1;
		}
		if (Octaves < 0) {
			Octaves = 0;
		}

		falloffMap = FallOffMap.GenerateFalloffMap (MapChunkSize);
	}

	void Update()
    {
		
        OffsetUpdate=OffsetUpdate+0.05f;
		Offset.Set(OffsetUpdate, 0);
		DrawMapInEditor ();
    }
}

public struct SkyData {
	public readonly float[,] heightMap;
	public readonly Color[] ColorMap;

	public SkyData (float[,] heightMap, Color[] ColorMap)
	{
		this.heightMap = heightMap;
		this.ColorMap = ColorMap;
	}
}


