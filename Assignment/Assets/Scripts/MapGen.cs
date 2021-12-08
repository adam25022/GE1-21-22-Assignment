using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

	public enum DrawMode {NoiseMap, Sky, Mesh, FalloffMap};
	public DrawMode drawMode;

	const int MapChunkSize = 25;
	[Range(0,6)]
	public int levelOfDetail;
	public float noiseScale;

	public int Octaves;
	[Range(0,1)]
	public float Persistance;
	public float Lacunarity;

	public int Seed;
	public Vector2 Offset;

	public float MeshHeightChanger;
	public AnimationCurve MeshHeightCurveAmmount;

	public bool autoUpdate;
	float[,] falloffMap;
	public TerrainType[] regions;
	
	public void GenerateMap() {
		float[,] NoiseMap = Noise.GenerateNoiseMap (MapChunkSize, MapChunkSize, Seed, noiseScale, Octaves, Persistance, Lacunarity, Offset);

		Color[] ColorMap = new Color[MapChunkSize * MapChunkSize];
		for (int y = 0; y < MapChunkSize; y++) {
			for (int x = 0; x < MapChunkSize; x++) {
				float CurrentHeight = NoiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (CurrentHeight <= regions [i].Height) {
						ColorMap [y * MapChunkSize + x] = regions [i].Color;
						break;
					}
				}
			}
		}

		MapDisplay Display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			Display.CreateTexture (TextureGenerator.TextureFromHeightMap (NoiseMap));
		} else if (drawMode == DrawMode.Mesh) {
			Display.CreateMesh (MeshGenerator.GenerateTerrainMesh (NoiseMap, MeshHeightChanger, MeshHeightCurveAmmount, levelOfDetail), TextureGenerator.TextureFromColorMap (ColorMap, MapChunkSize, MapChunkSize));
		} else if (drawMode == DrawMode.FalloffMap) {
			Display.CreateTexture(TextureGenerator.TextureFromHeightMap(FallOffMap.GenerateFalloffMap(MapChunkSize)));
		}
	}

	void OnValidate() {
		if (Lacunarity < 1) {
			Lacunarity = 1;
		}
		if (Octaves < 0) {
			Octaves = 0;
		}
	}
}

[System.Serializable]
public struct TerrainType {
	public string Name;
	public float Height;
	public Color Color;
}