using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningUI : MonoBehaviour
{
    private Image warningImage;
    public bool isActivated = false;
    public float blinkDuration = 2f;
    public float duration = 5f;
    public float time;

    public TextMeshProUGUI countDown;

    public Color color = new Color(1f, 0.8f, 0.25f, 0.1f);

    private void Awake()
    {
        warningImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.Player.warning = this;
        warningImage.color = color;
        Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            float timer = (Time.time - time);
            countDown.text = (duration - timer).ToString("F1");
            if (timer > duration)
            {
                // 피해? 아니면 함정? 무언가...

                Stop();
                return;
            }


            Color color = warningImage.color;
            color.a = Mathf.PingPong(Time.time, blinkDuration) * 0.1f;
            warningImage.color = color;
        }
    }

    public void Play(float duration)
    {
        if (isActivated) return;
        time = Time.time;
        countDown.text = duration.ToString("F1");
        this.duration = duration;
        gameObject.SetActive(true);
        isActivated = true;
    }

    public void Stop()
    {
        countDown.text = string.Empty;
        gameObject.SetActive(false);
        isActivated = false;
    }
}
