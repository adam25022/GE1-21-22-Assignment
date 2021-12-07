using UnityEngine;
using System.Collections;

public static class MeshGenerator 
{
	public static MeshData GenerateTerrainMesh(float[,] heightMap) 
    {
		int width = heightMap.GetLength (0);
		int Height = heightMap.GetLength (1);
		float topLeftX = (width - 1) / -2f;
		float topLeftZ = (Height - 1) / 2f;

		MeshData MeshData = new MeshData (width, Height);
		int vertexIndex = 0;

		for (int y = 0; y < Height; y++) 
        {
			for (int x = 0; x < width; x++) 
            {

				MeshData.Verticies [vertexIndex] = new Vector3 (topLeftX + x, heightMap [x, y], topLeftZ - y);
				MeshData.UVS [vertexIndex] = new Vector2 (x / (float)width, y / (float)Height);

				if (x < width - 1 && y < Height - 1) 
                {
					MeshData.AddTriangle (vertexIndex, vertexIndex + width + 1, vertexIndex + width);
					MeshData.AddTriangle (vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
				}

				vertexIndex++;
			}
		}

		return MeshData;

	}
}

public class MeshData 
{
	public Vector3[] Verticies;
	public int[] Triangles;
	public Vector2[] UVS;

	int triangleIndex;

	public MeshData(int meshWidth, int meshHeight) 
    {
		Verticies = new Vector3[meshWidth * meshHeight];
		UVS = new Vector2[meshWidth * meshHeight];
		Triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
	}

	public void AddTriangle(int a, int b, int c) 
    {
		Triangles [triangleIndex] = a;
		Triangles [triangleIndex + 1] = b;
		Triangles [triangleIndex + 2] = c;
		triangleIndex += 3;
	}

	public Mesh CreateMesh() 
    {
		Mesh Mesh = new Mesh ();
		Mesh.Verticies = Verticies;
		Mesh.Triangles = Triangles;
		Mesh.UV = UVS;
		Mesh.RecalculateNormals ();
		return Mesh;
	}

}