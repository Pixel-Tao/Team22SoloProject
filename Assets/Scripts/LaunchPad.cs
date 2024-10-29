using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [Range(0, 1000)] public float force;
    public float LaunchTime;
    public TextMeshProUGUI timerTextLeft;
    public TextMeshProUGUI timerTextRight;

    private float lastLaunchTime;

    private PlayerMovement movement;

    private void Start()
    {
        timerTextLeft.text = string.Empty;
        timerTextRight.text = string.Empty;
    }

    private void Update()
    {
        if (movement == null) return;
        float timer = LaunchTime - (Time.time - lastLaunchTime);
        timerTextLeft.text = timer.ToString("F0");
        timerTextRight.text = timer.ToString("F0");
        if (Time.time - lastLaunchTime > LaunchTime)
        {
            lastLaunchTime = Time.time;
            Launch();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out movement))
        {
            timerTextLeft.text = LaunchTime.ToString("F0");
            timerTextRight.text = LaunchTime.ToString("F0");
            lastLaunchTime = Time.time;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement movement))
        {
            timerTextLeft.text = string.Empty;
            timerTextRight.text = string.Empty;
        }
    }

    private void Launch()
    {
        Vector3 forceVector = (Vector3.up + transform.forward) * force;
        Rigidbody rigidbody = movement.GetComponent<Rigidbody>();
        this.movement.isLaunching = true;
        this.movement.enabled = false;
        rigidbody.AddForce(forceVector, ForceMode.Impulse);
        Invoke("EnablePlayerMovement", 1f);
    }

    private void EnablePlayerMovement()
    {
        if (this.movement != null)
        {
            this.movement.enabled = true;
            this.movement = null;
        }
    }
}
