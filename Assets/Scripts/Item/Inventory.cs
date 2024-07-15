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
    public int selectedSlot = 0; // ���õ� ���� ��ȣ
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

    void OnInteract(InputAction.CallbackContext context) // F Ű�� ������ �� ����
    {
        Debug.Log("F ���ͷ�Ʈ ");
        if (inventoryIndex[selectedSlot] != null) // ���õ� ������ ������� ���� ���
        {
            //������ ������
            ClearInventory();//�������� ���ɽ� �ش� �κ��丮 ���� �ʱ�ȭ
        }
        else if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length && inventoryIndex[selectedSlot] == null && inturectItem != null)
        {
            // ��ȿ�� ������ ���õ� ��� �κ��丮�� �������� ����
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
        // �׼��� ���� ������ Ȯ��
        if (context.phase == InputActionPhase.Performed)
        {
            // �Էµ� Ű�� Path ��������
            path = context.control.path;

            // Path ���� ������� switch �� ����
            Debug.Log(path);

            switch (path)
            {
                case "/Keyboard/1":
                    Debug.Log("1�� �Է��߽��ϴ�.");
                    ResetSlot();
                    selectedSlot = 0;
                    SelectSlot();
                    break;
                case "/Keyboard/2":
                    Debug.Log("2�� �Է��߽��ϴ�.");
                    ResetSlot();
                    selectedSlot = 1;
                    SelectSlot();
                    break;
                case "/Keyboard/3":
                    Debug.Log("3�� �Է��߽��ϴ�.");
                    ResetSlot();
                    selectedSlot = 2;
                    SelectSlot();
                    break;
                case "/Keyboard/4":
                    Debug.Log("4�� �Է��߽��ϴ�.");
                    ResetSlot();
                    selectedSlot = 3;
                    SelectSlot();
                    break;
                case "/Keyboard/5":
                    Debug.Log("5�� �Է��߽��ϴ�.");
                    ResetSlot();
                    selectedSlot = 4;
                    SelectSlot();
                    break;
                case "/Keyboard/6":
                    Debug.Log("6�� �Է��߽��ϴ�.");
                    ResetSlot();
                    selectedSlot = 5;
                    SelectSlot();
                    break;
                case "/Keyboard/7":
                    Debug.Log("7�� �Է��߽��ϴ�.");
                    ResetSlot();
                    selectedSlot = 6;
                    SelectSlot();
                    break;
                case "/Keyboard/8":
                    Debug.Log("8�� �Է��߽��ϴ�.");
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
        if (inventoryIndex[selectedSlot] != null)  // ���õ� ���Կ� �������� �ִ��� Ȯ��
        {
            item = inventoryIndex[selectedSlot].GetComponent<ItemS>();  // Item ������Ʈ ��������
            if (item != null)  // Item ������Ʈ�� �ִ��� Ȯ��
            {
                use = item.UseBles;  // UseBles �Ӽ� �� ��������
                Debug.Log("UseBles ��: " + use);  // �� ���
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
        //Ÿ���� ���ȿ��
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
        inventoryImage[selectedSlot].sprite = null; // �ش� ������ �̹����� Null�� ����
        inventoryImage[selectedSlot].enabled = false;//�ش������ �̹��� ��Ȱ��ȭ
        inventoryIndex[selectedSlot] = null; // ���� ����
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
        if (scrollValue.y > 0f) //���� ����
        {
            if (4 > selectedSlot)//���ý����� 5���� ������
            {
                ResetSlot();

                selectedSlot++;
                SelectSlot();
            }
        }
        else if (scrollValue.y < 0f)//���԰���
        {
            if (0 < selectedSlot) //������ ������ 1���� Ŭ��
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
        inventoryImage[selectedSlot].sprite = null; // �ش� ������ �̹����� Null�� ����
        inventoryImage[selectedSlot].enabled = false;
        inventoryIndex[selectedSlot] = null; // ���� ����
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