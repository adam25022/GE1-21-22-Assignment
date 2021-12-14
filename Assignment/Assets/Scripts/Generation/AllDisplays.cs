using UnityEngine;
// this whole class just displays the 
public class AllDisplays : MonoBehaviour {
	public Renderer MapTextureRenderer;
	public Renderer SkyTextureRenderer;
	public MeshFilter MapMeshFilter;
	public MeshRenderer MapMeshRenderer;

	public void CreateMapTexture(Texture2D texture) {
		MapTextureRenderer.sharedMaterial.mainTexture = texture;
		MapTextureRenderer.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}
	public void CreateSkyTexture(Texture2D texture) {
		SkyTextureRenderer.sharedMaterial.mainTexture = texture;
		SkyTextureRenderer.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}

	public void CreateMesh(MeshData Data, Texture2D texture) {
		MapMeshFilter.sharedMesh = Data.CreateMesh ();
		MapMeshRenderer.sharedMaterial.mainTexture = texture;
	}

}
