using UnityEngine;
using System.Collections;

public static class TextureGen {

	public static Texture2D TextureFromWorldMap(Color[] WorldMap, int width, int Height) {
		//create a new tecture that will be created with the height of the map
		Texture2D texture = new Texture2D (width, Height);
		// this fixes the blurryness of the textures.
		texture.filterMode = FilterMode.Point;
		//this stops the map from wrappign around, and the side of one side being visable on the edge of the other.
		texture.wrapMode = TextureWrapMode.Clamp;
		//set the textures of the pixels onto the worldmap.
		texture.SetPixels (WorldMap);
		//apply the texture to the object its attached to.
		texture.Apply ();
		return texture;
	}


	public static Texture2D TextureFromHeightMap(float[,] heightMap) {
		//get the width and height of the map.
		int width = heightMap.GetLength (0);
		int Height = heightMap.GetLength (1);
		//for each individual square of the map, change the colour of the square using the X and Y co-ordinates of the square
		Color[] WorldMap = new Color[width * Height];
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < width; x++) {
				//this part of the script only sets the colour between black and white, using grays and such in between.
				WorldMap [y * width + x] = Color.Lerp (Color.black, Color.white, heightMap [x, y]);
			}
		}

		return TextureFromWorldMap (WorldMap, width, Height);
	}

}
