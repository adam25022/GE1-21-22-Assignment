using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {
	// this is just the users choice list.
	public enum DrawMode {PerlinNoise, WorldMap, Mesh, FalloffMap};
	// this is where the user actually makes his choice.
	public DrawMode drawMode;
	// this is the normalization the user will chose to use
	public Noise.NormalizeMode normalizeMode;
	// this is how big the map is.
	public int SizeOfMap;
	// Used to control the initial division of offset coordinates into a Perlin noisemap, this provides zoom.
	public float noiseScale;
	// number of layers of perlin noise.
	public int NumberOfOctaves;
	//this gives the perlin noise map a focus on overall terrain heights with each passing octave growing larger and larger focusing on finer details, improving your ability to emulate terrain
	public float Lacunarity;
	// this generates new noisemaps based on the seed even if all other values are identicle.
	public int Seed;
	// this is the offset of how far you want the noisemap to be from the center of the terrain.
	public Vector2 Offset;
	// this is how high you want the mesh to be.
	public float MeshHeightChanger;
	//What fraction of amplitude persists in each Octave. (0-1)
	[Range(0,1)]
	public float Persistance;
	//curve map to set which sections of the map will be at which height, if its all 0 or all 1 it will just be a flat 2d picture, 
	//varying the range to go upwards with flat zones in the middle gives a feel of a mountain
	public AnimationCurve MeshHeightCurveAmmount;
	// tickbox to automatically update the map
	public bool AutomaticallyUpdate;
	// the colours of each of the region and at what height they are at.
	public TerrainType[] regions;
	// this is the falloffmap, every time this script validates it creates a new one.
	float[,] falloffMap;
	
	public void UserSelectedDrawingStyle() {
		MapData mapData = GenerateMapData (Vector2.zero);
		//this is the user selection options, the user can shose what he wants to do in this section and then dispalys it
		AllDisplays display = FindObjectOfType<AllDisplays> ();
		//in this the user has chosen to create a perlin noisemap 
		if (drawMode == DrawMode.PerlinNoise) {
			display.CreateMapTexture (TextureGen.TextureFromHeightMap (mapData.heightMap));
		//here the user has chosen to create a coloured worldmap
		} else if (drawMode == DrawMode.WorldMap) {
			display.CreateMapTexture (TextureGen.TextureFromWorldMap (mapData.WorldMap, SizeOfMap, SizeOfMap));
		//here the user has chosen to create a mesh of the map. 
		} else if (drawMode == DrawMode.Mesh) {
			display.CreateMesh (MeshGen.GenerateTerrainMesh (mapData.heightMap, MeshHeightChanger, MeshHeightCurveAmmount), TextureGen.TextureFromWorldMap (mapData.WorldMap, SizeOfMap, SizeOfMap));
			//here the user has chosen to display the falloffmap that is being used to filter all the other maps.
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
	// this just makes sure none of the values go out of bounds so that the program doesnt break.
	void OnValidate() {
		if (Lacunarity < 1) {
			Lacunarity = 1;
		}
		if (NumberOfOctaves < 0) {
			NumberOfOctaves = 0;
		}
		//generates the fallout map into the variable.
		falloffMap = FallOffMap.GenerateFalloffMap (SizeOfMap);
	}
	void Start(){
		// draw the map when the program starts so theres not just an empty mesh
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
