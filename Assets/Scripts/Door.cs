using UnityEngine;

public class Door : MonoBehaviour
{
    public void Open()
    {
        // ���� ���� �ִϸ��̼� �Ǵ� ���� ����
        Debug.Log($"{gameObject.name} is opened.");
        // ���� ���� �� �ݶ��̴��� ��Ȱ��ȭ�Ͽ� ��� �����ϵ��� ����
        GetComponent<Collider>().enabled = false;
    }

    public void Close()
    {
        // ���� �ݴ� �ִϸ��̼� �Ǵ� ���� ����
        Debug.Log($"{gameObject.name} is closed.");
        // ���� ���� �� �ݶ��̴��� Ȱ��ȭ�Ͽ� ��� �Ұ����ϵ��� ����
        GetComponent<Collider>().enabled = true;
    }
}
