using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PortalTextureSetup : MonoBehaviour
{
    public List<Camera> cameras;

    public List<Material> materials;


    private void Start()
    {
        if (cameras.Count == 0 || cameras.Count % 2 == 1)
        {
            return;
        }
        if (materials.Count == 0 || materials.Count % 2 == 1)
        {
            return;
        }
        if (cameras.Count != materials.Count)
        {
            return;
        }

        for (int i = 0; i < cameras.Count; i += 2)
        {

            if (cameras[i + 1].targetTexture != null) cameras[i + 1].targetTexture.Release();
            cameras[i + 1].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            materials[i + 1].mainTexture = cameras[i + 1].targetTexture;

            if (cameras[i].targetTexture != null) cameras[i].targetTexture.Release();
            cameras[i].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            materials[i].mainTexture = cameras[i].targetTexture;

        }
    }
}
