using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float time;
    private float time2 = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time2 - Time.time < 0)
        {
            time2 = Time.time + 2f;
            rigidbody.AddForce(Vector3.up * 80, ForceMode.Impulse);
        }

    }
}
