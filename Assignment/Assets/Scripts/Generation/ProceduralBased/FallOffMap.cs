using UnityEngine;
using System.Collections;

public static class FallOffMap {

	//2d float array for the fall off map. it takes in an int for the size of the map that is being generated.
	public static float[,] GenerateFalloffMap(int MapSize) {
		// initialise the map with the size of the dimensions that are passed in as the size of the map.
		float[,] FallOfMap = new float[MapSize,MapSize];

		//as the map is 2d you need 2 loops to loop through 
		//loop 1 is like the x co-ordinates in the map
		for (int Loop1 = 0; Loop1 < MapSize; Loop1++) {
			//loop 2 is like the y co-ordinates of the map.
			for (int Loop2 = 0; Loop2 < MapSize; Loop2++) {
				// we want it in the range negative -1 to 1
				float x = Loop1 / (float)MapSize * 2 - 1;
				float y = Loop2 / (float)MapSize * 2 - 1;
				// to get this we find out which one, x or y is closest to the edge of the square.
				float WhichIsClosestToEdge = Mathf.Max (Mathf.Abs (x), Mathf.Abs (y));
				FallOfMap [Loop1, Loop2] = Evaluate(WhichIsClosestToEdge);
			}
		}
		//this returns the completed map.
		return FallOfMap;
	}

	static float Evaluate(float value) {
		float a = 3;
		float b = 2.2f;

		return Mathf.Pow (value, a) / (Mathf.Pow (value, a) + Mathf.Pow (b - b * value, a));
	}
}
