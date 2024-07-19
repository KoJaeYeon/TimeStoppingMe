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

    public void SendGameClearMessage()
    {
        if (inturectMessage == null)
        {
            inturectMessage = GameObject.Find("inturectMessage").GetComponent<TextMeshProUGUI>();
        }
        inturectMessage.text = "Game Clear!";
        CenterMessage(inturectMessage);

        if (!isCoroutineRunning)
        {
            StartCoroutine(ClearMessage());
        }
    }

    public void SendPlayerDeathMessage()
    {
        if (inturectMessage == null)
        {
            inturectMessage = GameObject.Find("inturectMessage").GetComponent<TextMeshProUGUI>();
        }
        inturectMessage.text = "Player Died!";
        CenterMessage(inturectMessage);
    }

    private void CenterMessage(TextMeshProUGUI message)
    {
        RectTransform rectTransform = message.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    private IEnumerator ClearMessage()
    {
        isCoroutineRunning = true; // �ڷ�ƾ ����
        yield return new WaitForSecondsRealtime(3f); // 3�� �� �޽����� ����
        inturectMessage.text = null;
        isCoroutineRunning = false; // �ڷ�ƾ ����
    }
}
