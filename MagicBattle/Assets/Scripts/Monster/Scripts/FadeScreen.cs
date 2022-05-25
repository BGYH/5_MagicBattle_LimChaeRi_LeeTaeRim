using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 0.5f;
    public Color fadeColor;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        
    }
    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1.5f);
        Fade(0.5f, 0);
    }
    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.5f);
        Fade(0, 0.5f);
    }

    // Update is called once per frame
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }
    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while(timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_BaseColor", newColor);

            timer += Time.deltaTime;
            yield return null;
        }
        Color newColor2 = fadeColor;
        newColor2.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

        rend.material.SetColor("_BaseColor", newColor2);
    }
}
