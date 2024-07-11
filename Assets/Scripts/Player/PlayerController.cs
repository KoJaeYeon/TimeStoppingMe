using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 moveInput;

    private void OnEnable()
    {
        // PlayerInput ������Ʈ�� �ִ��� Ȯ���ϰ�, ���ٸ� �߰�
        if (!TryGetComponent(out PlayerInput playerInput))
        {
            playerInput = gameObject.AddComponent<PlayerInput>();
        }

        // Move �̺�Ʈ�� �ݹ� ���
        playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        // Move �̺�Ʈ�� �ݹ� ����
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
