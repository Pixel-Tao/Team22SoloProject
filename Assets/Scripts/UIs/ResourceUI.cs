using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void Start()
    {
        PlayerManager.Instance.CoinChangedEvent += CoinChanged;
        coinText.text = PlayerManager.Instance.Coin.ToString();
    }

    private void CoinChanged(int value)
    {
        coinText.text = value.ToString();
    }
}
