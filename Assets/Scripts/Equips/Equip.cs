using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public int damage;
    [Range(0, 100)] public float attackRate;
    [Range(0, 100)] public float attackDistance;
    [Range(0, 100)] public int staminaCost;
    [Range(1, 180)] public float attackAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
