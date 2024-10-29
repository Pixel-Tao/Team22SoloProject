using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorUI : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    void Start()
    {
        PlayerManager.Instance.Player.damageIndicator = this;
    }

    public void Flash()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        image.enabled = true;
        image.color = new Color(1, 100f / 255f, 100f / 255f, 0.5f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = image.color.a;
        float a = startAlpha;

        while (a > 0)
        {
            a -= Time.deltaTime * flashSpeed;
            image.color = new Color(1, 100f / 255f, 100f / 255f, a);
            yield return null;
        }

        image.enabled = false;
    }
}
