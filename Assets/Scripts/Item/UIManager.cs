using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager inst; // �̱��� �ν��Ͻ�

    public TextMeshProUGUI inturectMessage;
    private bool isCoroutineRunning = false; // �ڷ�ƾ ���¸� �����ϱ� ���� ����

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SendinturectMessage(string message)
    {
        if (inturectMessage == null)
        {
            inturectMessage = GameObject.Find("inturectMessage").GetComponent<TextMeshProUGUI>();
        }
        inturectMessage.text = message;

        if (!isCoroutineRunning) // �ڷ�ƾ�� ���� ���� �ƴ� ���� ����
        {
            StartCoroutine(ClearMessage());
        }
    }

    private IEnumerator ClearMessage()
    {
        isCoroutineRunning = true; // �ڷ�ƾ ����
        yield return new WaitForSecondsRealtime(3f); // 3�� �� �޽����� ����
        inturectMessage.text = null;
        isCoroutineRunning = false; // �ڷ�ƾ ����
    }
}
