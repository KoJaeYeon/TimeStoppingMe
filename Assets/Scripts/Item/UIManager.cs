using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager inst; // 싱글톤 인스턴스

    public TextMeshProUGUI inturectMessage;
    private bool isCoroutineRunning = false; // 코루틴 상태를 추적하기 위한 변수


    public TextMeshProUGUI AtkToolTip;
    public TextMeshProUGUI RangToolTip;
    public TextMeshProUGUI AtkSpedToolTip;
    public TextMeshProUGUI MoveSpeedToolTip;
    public TextMeshProUGUI projectileSpeedToolTip;
    public TextMeshProUGUI projectileCountToolTip;
    public TextMeshProUGUI maxAmmoSizeToolTip;
    public TextMeshProUGUI reloadTimeToolTip;
    public GameObject OutGameToolTip;
    public TextMeshProUGUI itemToolTip;
    public GameObject itemToolTipObject;
    public TextMeshProUGUI AmmoToolTip;
    public Image timeStop;
    public GameObject heartPrefab;
    private List<GameObject> hearts = new List<GameObject>();
    public Transform panelTransform;

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
        isCoroutineRunning = true; // 코루틴 시작
        yield return new WaitForSecondsRealtime(3f); // 3초 뒤 메시지를 숨김
        inturectMessage.text = null;
        isCoroutineRunning = false; // 코루틴 종료
    }
    public void UpdatePlayerToolTip(float moveSpeed)
    {
        MoveSpeedToolTip.text = moveSpeed.ToString();
    }
    public void UpdateWeaponToolTip(float baseDamage, float baseFireRate,float baseRange, float projectileSpeed, int maxAmmoSize, float reloadTime,int projectileCount)
    {
        AtkToolTip.text = baseDamage.ToString();
        RangToolTip.text = baseRange.ToString();
        AtkSpedToolTip.text = baseFireRate.ToString();
        projectileSpeedToolTip.text = projectileSpeed.ToString();
        projectileCountToolTip.text = projectileCount.ToString();
        maxAmmoSizeToolTip.text = maxAmmoSize.ToString();
        reloadTimeToolTip.text= reloadTime.ToString();
    }
    public void OnOutGameToolTip()
    {
        Debug.Log("OnGameToolTip");
        if (!OutGameToolTip.activeSelf)
        {
            OutGameToolTip.SetActive(true);
        }
        else
        {
            OutGameToolTip.SetActive(false);
        }

    }
    public void ItemToolTip(Item item)
    {  
        if (inturectMessage == null)
        {
            inturectMessage = GameObject.Find("ItemToolTip").GetComponent<TextMeshProUGUI>();
        }
        if (!itemToolTipObject.activeSelf)
        {
            itemToolTipObject.SetActive(true);
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(item.itemName);
        sb.AppendLine(item.itemText);
        //sb.AppendLine(itemData.DataClassName);
        sb.AppendLine();
        itemToolTip.text = sb.ToString();
    }
    public void ClearToolTip()

    {
        itemToolTip.text = null;
        itemToolTipObject.SetActive(false );
    }
    public void UpdateAmmo(int max , int currnt)
    {
        string maxammo = max.ToString();
        string currntammo = currnt.ToString();
        AmmoToolTip.text = (currntammo + " / " + maxammo);
    }
    public void UpdateTimeGauge(int currnttime,int maxtime)
    {
        timeStop.fillAmount = (float)currnttime / maxtime;
    }
    public void UpdateHearts(int currnetHP)
    {
        // 기존 하트 제거
        foreach (var heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        // 현재 HP만큼 하트 생성
        for (int i = 0; i < currnetHP; i++)
        {
            GameObject heart = Instantiate(heartPrefab, panelTransform);
            hearts.Add(heart);
        }
    }
}
