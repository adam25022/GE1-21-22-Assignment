using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour 
{

	public enum DrawMode {NoiseMap, ColorMap};
	public DrawMode drawMode;

	public int Width;
	public int Height;
	public float noiseScale;

	public int Octaves;
	[Range(0,1)]
	public float Persistance;
	public float Lacunarity;

	public int Seed;
	public Vector2 Offset;

	public bool autoUpdate;

	public TerrainType[] regions;

	public void GenerateMap() 
	{
		float[,] NoiseMap = Noise.GenerateNoiseMap (Width, Height, Seed, noiseScale, Octaves, Persistance, Lacunarity, Offset);

		Color[] ColorMap = new Color[Width * Height];
		for (int y = 0; y < Height; y++) 
		{
			for (int x = 0; x < Width; x++) 
			{
				float CurrentHeight = NoiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) 
				{
					if (CurrentHeight <= regions [i].Height) 
					{
						ColorMap [y * Width + x] = regions [i].Color;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) 
		{
			display.CreateTexture (TextureGenerator.TextureFromHeightMap(NoiseMap));
		} else if (drawMode == DrawMode.ColorMap) 
		{
			display.CreateTexture (TextureGenerator.TextureFromColorMap(ColorMap, Width, Height));
		}
	}

	void OnValidate() 
	{
		if (Width < 1) 
		{
			Width = 1;
		}
		if (Height < 1) 
		{
			Height = 1;
		}
		if (Lacunarity < 1) 
		{
			Lacunarity = 1;
		}
		if (Octaves < 0) 
		{
			Octaves = 0;
		}
	}
}

[System.Serializable]
public struct TerrainType 
{
	public string Name;
	public float Height;
	public Color Color;
}