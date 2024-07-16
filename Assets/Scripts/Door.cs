using UnityEngine;

public class Door : MonoBehaviour
{
    public void Open()
    {
        // 문을 여는 애니메이션 또는 로직 구현
        Debug.Log($"{gameObject.name} is opened.");
        // 문이 열릴 때 콜라이더를 비활성화하여 통과 가능하도록 설정
        GetComponent<Collider>().enabled = false;
    }

    public void Close()
    {
        // 문을 닫는 애니메이션 또는 로직 구현
        Debug.Log($"{gameObject.name} is closed.");
        // 문이 닫힐 때 콜라이더를 활성화하여 통과 불가능하도록 설정
        GetComponent<Collider>().enabled = true;
    }
}
