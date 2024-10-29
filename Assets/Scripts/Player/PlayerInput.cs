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
    private PlayerCondition player;
    private PlayerLook look;

    private bool isAttackKeyDown = false;
    private Vector2 moveDirection;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        interaction = GetComponent<Interaction>();
        attack = GetComponent<PlayerAttack>();
        player = GetComponent<PlayerCondition>();
        look = GetComponent<PlayerLook>();
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
        movement.Move(moveDirection);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveDirection = Vector2.zero;
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

    public void OnLook(InputAction.CallbackContext context)
    {
        if (player.inventory.isOpen)
        {
            look.Look(Vector2.zero);
        }
        else
        {
            look.Look(context.ReadValue<Vector2>());
        }
    }
}
