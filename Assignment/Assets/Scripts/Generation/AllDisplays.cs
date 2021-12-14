using UnityEngine;
// this whole class just displays the 
public class AllDisplays : MonoBehaviour {
	public Renderer MapTextureRenderer;
	public MeshFilter MapMeshFilter;
	public MeshRenderer MapMeshRenderer;

	public void CreateTexture(Texture2D texture) {
		MapTextureRenderer.sharedMaterial.mainTexture = texture;
		MapTextureRenderer.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}

	public void CreateMesh(MeshData Data, Texture2D texture) {
		MapMeshFilter.sharedMesh = Data.CreateMesh ();
		MapMeshRenderer.sharedMaterial.mainTexture = texture;
	}

}
