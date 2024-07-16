using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float cameraFollowSpeed = 0.1f;
    public float cursorFollowFactor = 0.5f;

    private Player player;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Transform cameraTransform;

    private Vector2 moveInput;
    private Vector2 mousePosition;
    private Vector3 cameraOffset;
    public LayerMask enemyLayers;

    void Awake()
    {
        player = GetComponent<Player>();
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (cinemachineVirtualCamera != null)
        {
            cameraTransform = cinemachineVirtualCamera.transform;
            cameraOffset = cameraTransform.position - player.transform.position;
        }
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
        if (context.performed && player.CurrentWeapon != null && !player.IsSuppressed)
        {
            player.CurrentWeapon.Fire(enemyLayers);
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if(context.performed && player.CurrentWeapon != null && !player.IsSuppressed)
        {
            player.ReloadWeapon();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && !player.IsSuppressed)
        {
            if(player.currentPlaceableItem != null && player.currentBlueprint != null)
            {
                player.StartInstallation();
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 2f))
                {
                    Item item = hit.collider.GetComponent<Item>();
                    if(item != null)
                    {
                        item.Use(player);
                    }
                }
            }
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.CancelInstallation();
        }
    }

    public void OnTimeStop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.TimeStop();
        }
    }

    void Update()
    {
        float deltaTime = player.IsTimeStopped() ? Time.unscaledDeltaTime : Time.deltaTime;
        Move(deltaTime);
        Rotate(deltaTime);
        UpdateCameraFollow(deltaTime);
    }

    void Move(float deltaTime)
    {
        if (player.IsSuppressed) return;

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * deltaTime;

        if (player.IsCharmed) move = -move;

        transform.position += move;
    }

    void Rotate(float deltaTime)
    {
        if (player.IsSuppressed) return;

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

    void UpdateCameraFollow(float deltaTime)
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cameraTransform.position.y));
        Vector3 targetPosition = player.transform.position + cameraOffset + (cursorWorldPosition - player.transform.position) * cursorFollowFactor;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, cameraFollowSpeed * deltaTime);
    }
}
