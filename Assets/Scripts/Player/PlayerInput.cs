using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
    private Interaction interaction;
    private PlayerAttack attack;
    private Player player;

    private bool isAttackKeyDown = false;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        interaction = GetComponent<Interaction>();
        attack = GetComponent<PlayerAttack>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (isAttackKeyDown)
        {
            attack.Attack();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            movement.Move(context.ReadValue<Vector2>());
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movement.Move(Vector2.zero);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            movement.Jump();
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            movement.RunToggle();
        }
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            interaction.Interact();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            PlayerManager.Instance.Player.inventory.Toggle();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (player.inventory.isOpen) return;

            isAttackKeyDown = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isAttackKeyDown = false;
        }
    }
}
