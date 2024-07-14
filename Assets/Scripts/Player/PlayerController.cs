using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;

    private CharacterController characterController;
    private Player player;

    private Vector2 moveInput;
    private Vector2 mousePosition;
    public LayerMask enemyLayers;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && player.CurrentWeapon != null)
        {
            player.CurrentWeapon.Fire(enemyLayers);
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if(context.performed && player.CurrentWeapon != null)
        {
            player.ReloadWeapon();
        }
    }

    void Update()
    {
        if (player.IsTimeStopped())
        {
            Move(Time.unscaledDeltaTime);
            Rotate(Time.unscaledDeltaTime);
        }
        else
        {
            Move(Time.deltaTime);
            Rotate(Time.deltaTime);
        }
    }

    void Move(float deltaTime)
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * deltaTime;
        characterController.Move(move);
    }

    void Rotate(float deltaTime)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        if (playerPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = (targetPoint - transform.position).normalized;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * deltaTime);
            }
        }
    }
}
