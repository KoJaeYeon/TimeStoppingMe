using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public GameObject[] inventoryIndex = new GameObject[5];
    public GameObject inturectItem;
    public string path;
    [SerializeField]
    public int selectedSlot = 0; // 선택된 슬롯 번호
    public Transform inventory;
    public float throwSpeed;
    private bool inBox;

    public Image[] inventoryImage;

    public void Awake()
    {
        inventoryIndex = new GameObject[8];
        inventoryImage = new Image[8];
    }

    public void Start()
    {
        inventory = transform.GetChild(2);
        GameObject inventoryUI = GameObject.Find("InventoryUi");
        for (int i = 0; i < inventoryImage.Length; i++)
        {
            inventoryImage[i] = inventoryUI.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            inventoryImage[i].sprite = null;
            inventoryImage[i].enabled = false;
        }
        selectedSlot = 0;
        SelectSlot();
    }

    void OnInteract(InputAction.CallbackContext context) // F 키를 눌렀을 때 실행
    {
        Debug.Log("F 인터렉트 ");
        if (inventoryIndex[selectedSlot] != null) // 선택된 슬롯이 비어있지 않은 경우
        {
            //아이템 사용로직
            ClearInventory();//아이템이 사용될시 해당 인벤토리 슬롯 초기화
        }
        else if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length && inventoryIndex[selectedSlot] == null && inturectItem != null)
        {
            // 유효한 슬롯이 선택된 경우 인벤토리에 아이템을 삽입
            inventoryIndex[selectedSlot] = inturectItem;
            inturectItem.transform.SetParent(inventory);
            inturectItem.transform.position = inventory.position;
            inturectItem.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            inturectItem.SetActive(false);
            Debug.Log($"Item added to slot {selectedSlot + 1}");
            inventoryImage[selectedSlot].sprite = inturectItem.GetComponent<Item>().icon;
            inventoryImage[selectedSlot].enabled = true;
        }
        else
        {
            Debug.Log("Selected slot is already occupied.");
        }
    }



    private void OnItemIndex(InputAction.CallbackContext context)
    {
        Debug.Log(context.ToString());
        // 액션이 실행 중인지 확인
        if (context.phase == InputActionPhase.Performed)
        {
            // 입력된 키의 Path 가져오기
            path = context.control.path;

            // Path 값을 기반으로 switch 문 실행
            Debug.Log(path);

            switch (path)
            {
                case "/Keyboard/1":
                    Debug.Log("1을 입력했습니다.");
                    ResetSlot();
                    selectedSlot = 0;
                    SelectSlot();
                    break;
                case "/Keyboard/2":
                    Debug.Log("2를 입력했습니다.");
                    ResetSlot();
                    selectedSlot = 1;
                    SelectSlot();
                    break;
                case "/Keyboard/3":
                    Debug.Log("3을 입력했습니다.");
                    ResetSlot();
                    selectedSlot = 2;
                    SelectSlot();
                    break;
                case "/Keyboard/4":
                    Debug.Log("4을 입력했습니다.");
                    ResetSlot();
                    selectedSlot = 3;
                    SelectSlot();
                    break;
                case "/Keyboard/5":
                    Debug.Log("5을 입력했습니다.");
                    ResetSlot();
                    selectedSlot = 4;
                    SelectSlot();
                    break;
                case "/Keyboard/6":
                    Debug.Log("6을 입력했습니다.");
                    ResetSlot();
                    selectedSlot = 5;
                    SelectSlot();
                    break;
                case "/Keyboard/7":
                    Debug.Log("7을 입력했습니다.");
                    ResetSlot();
                    selectedSlot = 6;
                    SelectSlot();
                    break;
                case "/Keyboard/8":
                    Debug.Log("8을 입력했습니다.");
                    ResetSlot();
                    selectedSlot = 7;
                    SelectSlot();
                    break;
                default:
                    break;
            }
        }
    }
    private void OnUse(InputAction.CallbackContext context)
    {
        bool use = false;
        ItemS item = null;
        if (inventoryIndex[selectedSlot] != null)  // 선택된 슬롯에 아이템이 있는지 확인
        {
            item = inventoryIndex[selectedSlot].GetComponent<ItemS>();  // Item 컴포넌트 가져오기
            if (item != null)  // Item 컴포넌트가 있는지 확인
            {
                use = item.UseBles;  // UseBles 속성 값 가져오기
                Debug.Log("UseBles 값: " + use);  // 값 출력
            }
            else
            {
                return;
            }
        }
        if (use)
        {
            switch (item.DataClassName)
            {
                case "Item_Tabaco":
                    UseTabacco();
                    break;
                case "Item_NightVision":
                    UseNightVision();
                    break;

                default:
                    break;
            }
        }
    }

    void UseTabacco()
    {
        UseItem();
        //transform.GetComponent<HP>().PlusHP(20);
        //타바코 사용효과
    }
    void UseNightVision()
    {
        UseItem();
        transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
    }
    void UseItem()
    {
        GameObject item = inventoryIndex[selectedSlot];
        item.transform.SetParent(null);
        item.SetActive(false);
        inventoryImage[selectedSlot].sprite = null; // 해당 슬롯의 이미지를 Null로 만듦
        inventoryImage[selectedSlot].enabled = false;//해당아이템 이미지 비활성화
        inventoryIndex[selectedSlot] = null; // 슬롯 비우기
    }
    void SelectSlot()
    {
        inventoryImage[selectedSlot].transform.parent.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }
    private void ResetSlot()
    {
        inventoryImage[selectedSlot].transform.parent.transform.localScale = Vector3.one;
    }

    void OnSelectWheel(InputAction.CallbackContext context)
    {
        Vector2 scrollValue = new Vector2(0f, 0f);
        if (scrollValue.y > 0f) //슬롯 증가
        {
            if (4 > selectedSlot)//선택슬롯이 5보다 작을시
            {
                ResetSlot();

                selectedSlot++;
                SelectSlot();
            }
        }
        else if (scrollValue.y < 0f)//슬롯감소
        {
            if (0 < selectedSlot) //선택한 슬롯이 1보다 클시
            {
                ResetSlot();
                Debug.Log(selectedSlot);
                selectedSlot--;
                SelectSlot();

            }
        }

    }

    void ClearInventory()
    {
        inventoryImage[selectedSlot].sprite = null; // 해당 슬롯의 이미지를 Null로 만듦
        inventoryImage[selectedSlot].enabled = false;
        inventoryIndex[selectedSlot] = null; // 슬롯 비우기
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            InstantItem item = other.GetComponent<InstantItem>();
            if (item != null)
            {
                inturectItem = other.GameObject();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == inturectItem)
        {
            inturectItem = null;
        }
    }
}