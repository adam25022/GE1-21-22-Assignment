using UnityEngine;
using System.Collections;

public static class TextureGenerator {

	public static Texture2D TextureFromColorMap(Color[] ColorMap, int width, int Height) {
		Texture2D texture = new Texture2D (width, Height);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels (ColorMap);
		texture.Apply ();
		return texture;
	}


	public static Texture2D TextureFromHeightMap(float[,] heightMap) {
		int width = heightMap.GetLength (0);
		int Height = heightMap.GetLength (1);

		Color[] ColorMap = new Color[width * Height];
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < width; x++) {
				ColorMap [y * width + x] = Color.Lerp (Color.black, Color.white, heightMap [x, y]);
			}
		}

		return TextureFromColorMap (ColorMap, width, Height);
	}

}
