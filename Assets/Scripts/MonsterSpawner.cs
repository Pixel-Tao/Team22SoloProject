using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public void Spawn()
    {
        foreach(Transform monster in gameObject.transform)
        {
            monster.gameObject.SetActive(true);
        }
    }
}
