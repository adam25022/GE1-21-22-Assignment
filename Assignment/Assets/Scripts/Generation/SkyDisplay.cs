using UnityEngine;
using System.Collections;

public class SkyDisplay : MonoBehaviour {

	public Renderer MapTextureRenderer;
	public MeshFilter MapMeshFilter;
	public MeshRenderer MapMeshRenderer;

	public void CreateTexture(Texture2D texture) {
		MapTextureRenderer.sharedMaterial.mainTexture = texture;
		MapTextureRenderer.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}

	public void CreateMesh(MeshData meshData, Texture2D texture) {
		MapMeshFilter.sharedMesh = meshData.CreateMesh ();
		MapMeshRenderer.sharedMaterial.mainTexture = texture;
	}

}
