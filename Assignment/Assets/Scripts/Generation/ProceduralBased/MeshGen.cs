using UnityEngine;
using System.Collections;

public static class MeshGen {
	public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve _heightCurve) {
		AnimationCurve heightCurve = new AnimationCurve (_heightCurve.keys);
		//this just uses the heightmap to get the width and height of the map that it will need to manipulate.
		int Width = heightMap.GetLength (0);
		int Height = heightMap.GetLength (1);
		//this sets a pointer at the topleft x and z co-ordinates. we do this because we want to use the middle of the mesh as 0,0 instead of 0,0 being the corner
		float topLeftX = (Width - 1) / -2f; //the f is important as it makes it a float meaning it wont be rounded.
		float topLeftZ = (Height - 1) / 2f;
		// this is here as in the sample program i used he passed it in as an integer but i am not using it so its just default set to 0.
		int levelOfDetail=0;
		int MeshSimplification = (levelOfDetail == 0)?1:levelOfDetail * 2;
		int verticesPerLine = (Width - 1) / MeshSimplification + 1;

		MeshData meshData = new MeshData (verticesPerLine, verticesPerLine);
		//this is for the triangles to know which is part of which, as theres 3 vertex's per triangle so its easy to keep track.
		int vertexIndex = 0;
		//like all x and y double for loops this one is again for the vector 2 x and y co-ordinates.
		for (int y = 0; y < Height; y += MeshSimplification) {
			for (int x = 0; x < Width; x += MeshSimplification) {

				meshData.vertices [vertexIndex] = new Vector3 (topLeftX + x, heightCurve.Evaluate (heightMap [x, y]) * heightMultiplier, topLeftZ - y);
				//this to tell the vertices to tell it where it is in relation to the rest of the map. between 0 and 1
				meshData.uvs [vertexIndex] = new Vector2 (x / (float)Width, y / (float)Height);
				// we will be looping through the map, so if were at vertex 1 we willb e setting both of the triangles in the squares attached.
				// if there are any vertices that arent attached to a uncompleted triangle you skip them
				// its a 2d grid, so if in the first row you just iave i, then i+1, in the next row you would have i+w, i+w+1,
				// as its a square(2x2 grid, 2x2=4 sides, object with 4 equal distnacesides is a square), 2 triangles
				// this means that the first triangle will go from i, i+w+1, i+w and the second triangle will go from i+w1, i, i+1
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
	//this is a vector 2 array for each of the vertices to tell it where it is in relation to the rest of the map. between 0 and 1
	public Vector2[] uvs;

	int triangleIndex;
	// this is the data that will be used above.
	public MeshData(int meshWidth, int meshHeight) {
		//create a new array  with the size of width*height.
		vertices = new Vector3[meshWidth * meshHeight];
		uvs = new Vector2[meshWidth * meshHeight];
		//we need to find out how many squares the vertices form which is 
		//width -1 multiplied by the height -1, each square is made up of 3 triangles so that is 6 vertices.
		triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
	}

	public void AddTriangle(int a, int b, int c) {
		//use the 3 vertices that are used for the creation of the triangle
		triangles [triangleIndex] = a;
		triangles [triangleIndex + 1] = b;
		triangles [triangleIndex + 2] = c;
		triangleIndex += 3;
	}
	// this uses the mesh data to actually create the mesh. it uses the vertices and the triangles and uv's and calculates the normals.
	public Mesh CreateMesh() {
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		// calcualting the normals is just so lighting doesnt bug out.
		mesh.RecalculateNormals ();
		return mesh;
	}

}