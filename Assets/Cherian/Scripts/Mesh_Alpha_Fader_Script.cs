using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_Alpha_Fader_Script : MonoBehaviour
{
    public Renderer meshRenderer;
    public float fadeDuration = 1f;
    public float fadeInterval = 3f;
    public float fadeAlphaIn = 0.3f;

    private Material material;
    private bool isFadingIn = true;
    private Color materialColor;

    private void Start()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<Renderer>();
        }

        material = meshRenderer.sharedMaterial;
        materialColor = material.color;

        StartCoroutine(FadeAlphaCoroutine());
    }

    private IEnumerator FadeAlphaCoroutine()
    {
        while (true)
        {
            float targetAlpha = isFadingIn ? fadeAlphaIn : 0f;
            float startAlpha = isFadingIn ? 0f : fadeAlphaIn;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / fadeDuration;
                float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                SetMaterialAlpha(currentAlpha);
                yield return null;
            }

            isFadingIn = !isFadingIn;
            yield return new WaitForSeconds(fadeInterval);
        }
    }

    private void SetMaterialAlpha(float alpha)
    {
        materialColor.a = alpha;
        material.color = materialColor;
    }
}
