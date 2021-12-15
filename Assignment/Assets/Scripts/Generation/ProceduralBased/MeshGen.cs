using UnityEngine;
using System.Collections;

public static class MeshGen {
	public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve _heightCurve) {
		AnimationCurve heightCurve = new AnimationCurve (_heightCurve.keys);
		//this just uses the heightmap to get the width and height of the map that it will need to manipulate.
		int Width = heightMap.GetLength (0);
		int Height = heightMap.GetLength (1);
		//this sets a pointer at the topleft x and z co-ordinates.
		float topLeftX = (Width - 1) / -2f;
		float topLeftZ = (Height - 1) / 2f;
		int levelOfDetail=0;
		int MeshSimplification = (levelOfDetail == 0)?1:levelOfDetail * 2;
		int verticesPerLine = (Width - 1) / MeshSimplification + 1;

		MeshData meshData = new MeshData (verticesPerLine, verticesPerLine);
		int vertexIndex = 0;
		//like all x and y double for loops this one is again for the vector 2 x and y co-ordinates.
		for (int y = 0; y < Height; y += MeshSimplification) {
			for (int x = 0; x < Width; x += MeshSimplification) {
				meshData.vertices [vertexIndex] = new Vector3 (topLeftX + x, heightCurve.Evaluate (heightMap [x, y]) * heightMultiplier, topLeftZ - y);
				meshData.uvs [vertexIndex] = new Vector2 (x / (float)Width, y / (float)Height);

				if (x < Width - 1 && y < Height - 1) {
					meshData.AddTriangle (vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
					meshData.AddTriangle (vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
				}

				vertexIndex++;
			}
		}

		return meshData;

	}
}

public class MeshData {
	// these are the vertices and triangles we are going to be using to create the mesh.
	public Vector3[] vertices;
	public int[] triangles;
	public Vector2[] uvs;

	int triangleIndex;

	public MeshData(int meshWidth, int meshHeight) {
		vertices = new Vector3[meshWidth * meshHeight];
		uvs = new Vector2[meshWidth * meshHeight];
		triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
	}

	public void AddTriangle(int a, int b, int c) {
		triangles [triangleIndex] = a;
		triangles [triangleIndex + 1] = b;
		triangles [triangleIndex + 2] = c;
		triangleIndex += 3;
	}

	public Mesh CreateMesh() {
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals ();
		return mesh;
	}

}