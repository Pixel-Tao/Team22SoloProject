using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour, ILeverTarget
{
    [Range(1, 100f)] public float distance = 10f;
    [Range(0, 1f)] public float width;
    [Range(0, 10f)] public float checkTime;
    public float warningTime;
    public LayerMask layerMask;
    public Color color;

    public Player player;

    private LineRenderer lineRenderer;
    private float lastCheckTime;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        LeverOn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkTime)
        {
            lastCheckTime = Time.time;
            CheckObject();
        }
    }

    private void CheckObject()
    {
        if(lineRenderer.enabled)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out player))
                {
                    player.warning.Play(warningTime);
                }
            }
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * distance);
        }
    }

    public void LeverOn()
    {
        lineRenderer.enabled = true;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    public void LeverOff()
    {
        lineRenderer.enabled = false;
        player?.warning.Stop();
        player = null;
    }
}
