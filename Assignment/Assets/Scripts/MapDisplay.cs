using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

	public Renderer MapTextureRenderer;

	public void CreateTexture(Texture2D texture) {
		MapTextureRenderer.sharedMaterial.mainTexture = texture;
		MapTextureRenderer.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}
	
}
