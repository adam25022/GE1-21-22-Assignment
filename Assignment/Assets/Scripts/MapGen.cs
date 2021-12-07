using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

	public int Width;
	public int Height;
	public float noiseScale;
	public int Octaves; [Range(0,1)]
	public float Persistance;
	public float Lacunarity;
	public int Seed;
	public Vector2 Offset;
	public bool autoUpdate;

	public void GenerateMap() {
		float[,] noiseMap = Noise.GenerateNoiseMap (Width, Height, Seed, noiseScale, Octaves, Persistance, Lacunarity, Offset);


		MapDisplay display = FindObjectOfType<MapDisplay> ();
		display.DrawNoiseMap (noiseMap);
	}

	void OnValidate() {
		if (Width < 1) {
			Width = 1;
		}
		if (Height < 1) {
			Height = 1;
		}
		if (Lacunarity < 1) {
			Lacunarity = 1;
		}
		if (Octaves < 0) {
			Octaves = 0;
		}
	}

}
