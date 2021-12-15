using UnityEngine;
// this whole class just displays the generated textures. 
public class AllDisplays : MonoBehaviour {
	public Renderer MapTextureRenderer;
	public Renderer SkyTextureRenderer;
	public MeshFilter MapMeshFilter;
	public MeshRenderer MapMeshRenderer;
	//we cant just use textureRendere.material as that is only instantiated at runtime and we want to be able to change this
	//so for all of these we use shared stuff so that it can be changed and modified at will.
	//its passed in the texture as a material to set it.
	public void CreateMapTexture(Texture2D texture) {
		MapTextureRenderer.sharedMaterial.mainTexture = texture;
		//set the size of the plane to be the same size as the map using the size of the texture as a basis.
		MapTextureRenderer.transform.localScale = new Vector3 (texture.width, 1, texture.height);	
	}
	
	public void CreateSkyTexture(Texture2D texture) {
		SkyTextureRenderer.sharedMaterial.mainTexture = texture;
		//set the size of the plane to be the same size as the Sky.
		SkyTextureRenderer.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}

	public void CreateMesh(MeshData Data, Texture2D texture) {
		//this just uses the mesh data sent to create the mesh in and the, use the texture to apply the texture to the mesh
		MapMeshFilter.sharedMesh = Data.CreateMesh ();
		MapMeshRenderer.sharedMaterial.mainTexture = texture;
	}

}
