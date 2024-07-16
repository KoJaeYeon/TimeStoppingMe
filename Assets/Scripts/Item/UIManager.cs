using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager inst; // 싱글톤 인스턴스

    public TextMeshProUGUI inturectMessage;
    private bool isCoroutineRunning = false; // 코루틴 상태를 추적하기 위한 변수

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

        if (!isCoroutineRunning) // 코루틴이 실행 중이 아닐 때만 시작
        {
            StartCoroutine(ClearMessage());
        }
    }

    private IEnumerator ClearMessage()
    {
        isCoroutineRunning = true; // 코루틴 시작
        yield return new WaitForSecondsRealtime(3f); // 3초 뒤 메시지를 숨김
        inturectMessage.text = null;
        isCoroutineRunning = false; // 코루틴 종료
    }
}
