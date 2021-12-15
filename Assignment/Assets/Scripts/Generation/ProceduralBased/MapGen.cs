using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

	public enum DrawMode {PerlinNoise, WorldMap, Mesh, FalloffMap};
	public DrawMode drawMode;
	public Noise.NormalizeMode normalizeMode;
	
	public int SizeOfMap;

	public float noiseScale;
	public int NumberOfOctaves;
	public float Lacunarity;
	// this generates new noisemaps based on the seed even if all other values are identicle.
	public int Seed;
	public Vector2 Offset;
	public float MeshHeightChanger;
	[Range(0,1)]
	public float Persistance;
	//curve map to set which sections of the map will be at which height, if its all 0 or all 1 it will just be a flat 2d picture, 
	//varying the range to go upwards with flat zones in the middle gives a feel of a mountain
	public AnimationCurve MeshHeightCurveAmmount;
	// tickbox to automatically update the map
	public bool AutomaticallyUpdate;
	public TerrainType[] regions;
	float[,] falloffMap;
	
	public void UserSelectedDrawingStyle() {
		MapData mapData = GenerateMapData (Vector2.zero);

		AllDisplays display = FindObjectOfType<AllDisplays> ();
		if (drawMode == DrawMode.PerlinNoise) {
			display.CreateMapTexture (TextureGen.TextureFromHeightMap (mapData.heightMap));
		} else if (drawMode == DrawMode.WorldMap) {
			display.CreateMapTexture (TextureGen.TextureFromWorldMap (mapData.WorldMap, SizeOfMap, SizeOfMap));
		} else if (drawMode == DrawMode.Mesh) {
			display.CreateMesh (MeshGen.GenerateTerrainMesh (mapData.heightMap, MeshHeightChanger, MeshHeightCurveAmmount), TextureGen.TextureFromWorldMap (mapData.WorldMap, SizeOfMap, SizeOfMap));
		} else if (drawMode == DrawMode.FalloffMap) {
			display.CreateMapTexture(TextureGen.TextureFromHeightMap(FallOffMap.GenerateFalloffMap(SizeOfMap)));
		}
	}

	MapData GenerateMapData(Vector2 centre) {
		// this retrieves the noisemap from the noise.cs file using the inputted data of size, seed, noise, NumberOfOctaves, persistance, lacrunarity, offset and the way it is being normalized.
		// these values are explained in the noise class in more detail, but the gist of it is each of them modifies the noise map in a unique way.
		float[,] PerlinNoise = Noise.GenerateNoiseMap (SizeOfMap, SizeOfMap, Seed, noiseScale, NumberOfOctaves, 
														Persistance, Lacunarity, centre + Offset, normalizeMode);
		//generate the colours for all of the pixels
		Color[] WorldMap = new Color[SizeOfMap * SizeOfMap];
		//again its a 2d array so this is the y co-ordinate
		for (int y = 0; y < SizeOfMap; y++) {
			//this is the x co-ordinate
			for (int x = 0; x < SizeOfMap; x++) {
				// here we tell it to set all the pieces on the outside of the block to use the falloff map
				PerlinNoise [x, y] = Mathf.Clamp01(PerlinNoise [x, y] - falloffMap [x, y]);
				// the height at this point is equal to the value of the noisemap at them co-ordinates.
				float CurrentHeight = PerlinNoise [x, y];
				for (int i = 0; i < regions.Length; i++) {
					//we have found the region that it falls between
					if (CurrentHeight >= regions [i].Height) {
						//color this section of the map the color for that region.
						WorldMap [y * SizeOfMap + x] = regions [i].Color;
					} else {
						//if we cant find the colour that is atributed to this area break.
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
		if (NumberOfOctaves < 0) {
			NumberOfOctaves = 0;
		}

		falloffMap = FallOffMap.GenerateFalloffMap (SizeOfMap);
	}
	void Start(){
		UserSelectedDrawingStyle ();
	}
		
}

//this is the part where we pick the colour that will go in each section of the heightmap.
//i.e. 0-2 is blue for water, 2-8 is green for grass, 8-10 is mountian.
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
