using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float distance;
    public LayerMask layerMask;
    public float checkRate;
    public GameObject currentGameObject;
    private InteractableObject currentInteractableObject;
    public TextMeshProUGUI promptText;

    private float lastCheckTime;

    private Camera camera;
    private PlayerAnim anim;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        anim = GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            CheckForInteractable();
            lastCheckTime = Time.time;
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
        {
            if (currentGameObject == null || currentGameObject.gameObject != hit.collider.gameObject)
            {
                currentGameObject = hit.collider.gameObject;
                currentInteractableObject = currentGameObject.GetComponent<InteractableObject>();
                SetPromptText();
            }
        }
        else
        {
            currentGameObject = null;
            currentInteractableObject = null;
            SetPromptText();
        }
    }

    private void SetPromptText()
    {
        if (currentInteractableObject == null)
        {
            promptText.text = string.Empty;
            promptText.gameObject.SetActive(false);
        }
        else
        {
            promptText.text = currentInteractableObject.GetInteractText();
            promptText.gameObject.SetActive(true);
        }
    }

    public void Interact()
    {
        if (currentGameObject)
        {
            currentInteractableObject.OnInteract();
            currentInteractableObject = null;
            currentGameObject = null;
            SetPromptText();

            anim.Interact();
        }
    }
}
