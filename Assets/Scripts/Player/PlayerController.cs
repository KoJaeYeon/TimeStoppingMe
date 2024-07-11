using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 moveInput;

    private void OnEnable()
    {
        // PlayerInput 컴포넌트가 있는지 확인하고, 없다면 추가
        if (!TryGetComponent(out PlayerInput playerInput))
        {
            playerInput = gameObject.AddComponent<PlayerInput>();
        }

        // Move 이벤트에 콜백 등록
        playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        // Move 이벤트에 콜백 해제
        if (TryGetComponent(out PlayerInput playerInput))
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }
    }

    public void OnActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "Move")
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
