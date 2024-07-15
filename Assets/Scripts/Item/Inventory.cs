using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Camera mainCamera;
    Ray ray;
    RaycastHit hit;
    [SerializeField]
    public float range = 100f;
    [SerializeField]
    public GameObject[] inventoryIndex = new GameObject[8]; // 배열 크기 수정
    public GameObject inturectItem;
    public string path;
    [SerializeField]
    public int selectedSlot = 0; // 선택된 슬롯 번호
    public Transform inventory;
    public float throwSpeed;
    private bool inBox;
    public Image[] inventoryImage = new Image[8]; // 배열 크기 수정
    public PlayerInput playerInput;

    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // 이벤트 등록
        playerInput.actions["Interact"].performed += OnInteract;
        playerInput.actions["ItemIndex"].performed += OnItemIndex;
        playerInput.actions["Use"].performed += OnUse;
        playerInput.actions["SelectWheel"].performed += OnSelectWheel;
    }

    public void Start()
    {
        // inventory의 자식 개수 확인 및 인덱스 오류 방지
        if (transform.childCount > 2)
        {
            inventory = transform.GetChild(2);
        }
        else
        {
            Debug.LogError("Inventory transform not properly set.");
            return;
        }

        GameObject inventoryUI = GameObject.Find("InventoryUi");
        if (inventoryUI != null)
        {
            for (int i = 0; i < inventoryImage.Length; i++)
            {
                if (inventoryUI.transform.childCount > i)
                {
                    inventoryImage[i] = inventoryUI.transform.GetChild(i).GetChild(0).GetComponent<Image>();
                    inventoryImage[i].sprite = null;
                    inventoryImage[i].enabled = false;
                }
                else
                {
                    Debug.LogError($"Inventory UI child {i} not found.");
                }
            }
        }
        else
        {
            Debug.LogError("InventoryUi GameObject not found.");
        }

        selectedSlot = 0;
        SelectSlot();
    }

    void Update()
    {
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("F 인터렉트 ");
        if (inventoryIndex[selectedSlot] != null)
        {
            ClearInventory();
        }
        else if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length && inventoryIndex[selectedSlot] == null && inturectItem != null)
        {
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
        if (context.phase == InputActionPhase.Performed)
        {
            path = context.control.path;
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
        if (inventoryIndex[selectedSlot] != null)
        {
            item = inventoryIndex[selectedSlot].GetComponent<ItemS>();
            if (item != null)
            {
                use = item.UseBles;
                Debug.Log("UseBles 값: " + use);
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
        inventoryImage[selectedSlot].sprite = null;
        inventoryImage[selectedSlot].enabled = false;
        inventoryIndex[selectedSlot] = null;
    }

    void SelectSlot()
    {
        inventoryImage[selectedSlot].transform.parent.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

    private void ResetSlot()
    {
        inventoryImage[selectedSlot].transform.parent.transform.localScale = Vector3.one;
    }

    void OnDrawGizmos()
    {
        if (ray.origin != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * range);
        }
    }

    private void OnSelectWheel(InputAction.CallbackContext context)
    {
        Vector2 scrollValue = context.ReadValue<Vector2>();
        if (scrollValue.y > 0f)
        {
            if (4 > selectedSlot)
            {
                ResetSlot();
                selectedSlot++;
                SelectSlot();
            }
        }
        else if (scrollValue.y < 0f)
        {
            if (0 < selectedSlot)
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
        inventoryImage[selectedSlot].sprite = null;
        inventoryImage[selectedSlot].enabled = false;
        inventoryIndex[selectedSlot] = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            InstantItem item = other.GetComponent<InstantItem>();
            if (item != null)
            {
                inturectItem = other.gameObject;
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
