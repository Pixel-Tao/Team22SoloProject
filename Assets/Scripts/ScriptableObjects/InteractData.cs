using System.Collections;
using UnityEngine;

public enum InteractType
{
    None,
    Item,
    Box,
    Button,
    Pickup,
}

[CreateAssetMenu(fileName = "InteractData", menuName = "Datas/InteractData")]
public class InteractData : ScriptableObject
{
    public string title;
    public string description;
    public InteractType interactType;

    public GameObject interactPrefab;
}
