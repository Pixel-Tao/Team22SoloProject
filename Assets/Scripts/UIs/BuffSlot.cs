using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffSlot : MonoBehaviour
{
    public float ActivatePassiveValue => gameObject.activeInHierarchy ? passiveValue : 0;
    [SerializeField] private float passiveValue;
    public Image durationBar;
    private float duration;
    private float tick;
    private float time;

    private WaitForSeconds wait;

    public void Buff(float duration)
    {
        gameObject.SetActive(true);
        durationBar.fillAmount = 1;
        time = 0;
        this.duration = duration;
        this.tick = duration * 0.01f;
        wait = new WaitForSeconds(this.tick);
        StartCoroutine(BuffTimer());
    }
    IEnumerator BuffTimer()
    {
        while (time <= duration)
        {
            yield return wait;
            time += this.tick;
            durationBar.fillAmount = 1 - time / duration;
            Debug.Log(time);
        }

        gameObject.SetActive(false);
    }
}
