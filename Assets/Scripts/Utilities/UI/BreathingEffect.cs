using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BreathingEffect : MonoBehaviour
{
    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public float duration = 1f;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Breathe());
    }


    IEnumerator Breathe()
    {
        while (true)
        {
            yield return StartCoroutine(ChangeColor(startColor, endColor, duration / 2f));
            yield return StartCoroutine(ChangeColor(endColor, startColor, duration / 2f));
        }
    }

    IEnumerator ChangeColor(Color fromColor, Color toColor, float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            image.color = Color.Lerp(fromColor, toColor, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.color = toColor; // Ensure the color ends exactly at 'toColor'
    }
}
