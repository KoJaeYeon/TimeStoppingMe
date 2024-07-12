using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemDescription;
    public  GameObject tooltip;
    [SerializeField]
    public TextMeshProUGUI tooltiptext;
    public  string DataClassName;
    public Image image;

    void Start()
    {
        //tooltip = transform.GetChild(0).gameObject;
        //tooltip = GameObject.Find("ToolTip");
        tooltip = transform.parent.Find("ToolTip").gameObject;
        tooltip.SetActive(false);
        tooltiptext =tooltip.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        image = transform.GetChild(0).GetComponent<Image>();
        
        //tooltip.GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
        tooltip.transform.position = Input.mousePosition;
        //DataClassName = gameObject.GetComponent<Image>().name;
        DataClassName = image.sprite.name;
        Debug.Log(DataClassName);
        GetTooltip(DataClassName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }

    public void GetTooltip(string itemClassName)
    {
        //var itemData = DataManager.Inst.GetIteminfo(DataClassName);
        //if (itemData == null)
        //    return;
        //Debug.Log(itemData.Desc);
        //Debug.Log(itemData.Price);
        //Debug.Log(itemData.DataClassName);
        //Debug.Log(itemData.Name);

        //StringBuilder sb = new StringBuilder();
        //sb.AppendLine(itemData.Name);
        //sb.AppendLine(itemData.Price.ToString()+"$");
        ////sb.AppendLine(itemData.DataClassName);
        //sb.AppendLine(itemData.Desc);
        //tooltiptext.text = sb.ToString();
    }
}
