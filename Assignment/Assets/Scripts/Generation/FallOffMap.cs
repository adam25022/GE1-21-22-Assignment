using UnityEngine;
using System.Collections;

public static class FallOffMap {
	
	public static float[,] GenerateFalloffMap(int MapSize) {
		float[,] FallOfMap = new float[MapSize,MapSize];

		for (int Loop1 = 0; Loop1 < MapSize; Loop1++) {
			for (int Loop2 = 0; Loop2 < MapSize; Loop2++) {
				float x = Loop1 / (float)MapSize * 2 - 1;
				float y = Loop2 / (float)MapSize * 2 - 1;

				float value = Mathf.Max (Mathf.Abs (x), Mathf.Abs (y));
				FallOfMap [Loop1, Loop2] = Evaluate(value);
			}
		}

		return FallOfMap;
	}

	static float Evaluate(float value) {
		float a = 3;
		float b = 2.2f;

		return Mathf.Pow (value, a) / (Mathf.Pow (value, a) + Mathf.Pow (b - b * value, a));
	}
}
