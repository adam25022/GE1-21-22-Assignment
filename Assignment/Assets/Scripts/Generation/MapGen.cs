using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

	public enum DrawMode {PerlinNoise, WorldMap, Mesh, FalloffMap};
	public DrawMode drawMode;
	public Noise.NormalizeMode normalizeMode;
	
	public int SizeOfMap;
	[Range(0,10)]
	public int editorPreviewLOD;
	public float noiseScale;
	public int Octaves;
	[Range(0,1)]
	public float Persistance;
	public float Lacunarity;
	public int Seed;
	public Vector2 Offset;
	public bool useFalloff;
	public float MeshHeightChanger;
	public AnimationCurve MeshHeightCurveAmmount;
	public bool autoUpdate;
	public TerrainType[] regions;
	float[,] falloffMap;
	void Awake() {
		falloffMap = FallOffMap.GenerateFalloffMap (SizeOfMap);
	}

	public void DrawMapInEditor() {
		MapData mapData = GenerateMapData (Vector2.zero);

		MapDisplay Display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.PerlinNoise) {
			Display.CreateTexture (TextureGenerator.TextureFromHeightMap (mapData.heightMap));
		} else if (drawMode == DrawMode.WorldMap) {
			Display.CreateTexture (TextureGenerator.TextureFromWorldMap (mapData.WorldMap, SizeOfMap, SizeOfMap));
		} else if (drawMode == DrawMode.Mesh) {
			Display.CreateMesh (MeshGenerator.GenerateTerrainMesh (mapData.heightMap, MeshHeightChanger, MeshHeightCurveAmmount, editorPreviewLOD), TextureGenerator.TextureFromWorldMap (mapData.WorldMap, SizeOfMap, SizeOfMap));
		} else if (drawMode == DrawMode.FalloffMap) {
			Display.CreateTexture(TextureGenerator.TextureFromHeightMap(FallOffMap.GenerateFalloffMap(SizeOfMap)));
		}
	}

	MapData GenerateMapData(Vector2 centre) {
		float[,] PerlinNoise = Noise.GenerateNoiseMap (SizeOfMap, SizeOfMap, Seed, noiseScale, Octaves, Persistance, Lacunarity, centre + Offset, normalizeMode);

		Color[] WorldMap = new Color[SizeOfMap * SizeOfMap];
		for (int y = 0; y < SizeOfMap; y++) {
			for (int x = 0; x < SizeOfMap; x++) {
				if (useFalloff) {
					PerlinNoise [x, y] = Mathf.Clamp01(PerlinNoise [x, y] - falloffMap [x, y]);
				}
				float CurrentHeight = PerlinNoise [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (CurrentHeight >= regions [i].Height) {
						WorldMap [y * SizeOfMap + x] = regions [i].Color;
					} else {
						break;
					}
				}
			}
		}


		return new MapData (PerlinNoise, WorldMap);
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
	void Start(){
		DrawMapInEditor ();
	}
		
}

[System.Serializable]
public struct TerrainType {
	public string Name;
	public float Height;
	public Color Color;
}

public struct MapData {
	public readonly float[,] heightMap;
	public readonly Color[] WorldMap;

	public MapData (float[,] heightMap, Color[] WorldMap)
	{
		this.heightMap = heightMap;
		this.WorldMap = WorldMap;
	}
}
