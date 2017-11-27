using UnityEngine;

[ExecuteInEditMode]
public class CustomEffect : MonoBehaviour
{
    public Material WaveMaterial;
    //public Material DarkenMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, WaveMaterial);
        //Graphics.Blit(src, dst, DarkenMaterial);
    }

}
