using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    void Start()
    {
        Interaction interaction = PlayerManager.Instance.Player.GetComponent<Interaction>();
        interaction.promptText = GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }
}
