using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
    public GameObject[] inventoryIndex = new GameObject[8]; // �迭 ũ�� ����
    public GameObject inturectItem;
    public string path;
    [SerializeField]
    public int selectedSlot = 0; // ���õ� ���� ��ȣ
    public Transform inventory;
    public float throwSpeed;
    private bool inBox;
    public Image[] inventoryImage = new Image[8]; // �迭 ũ�� ����
    public PlayerInput playerInput;
    public Material[] materials;
    public GameObject inven;

    public bool construct;
    private Vector3 constructPosition;
    private bool validPlacement;

    [Header("maxbuild")]
    public float MaxDistance = 5f; //��ġ �ִ�Ÿ�
    
    [Header("RotateSpeed")]
    public float rotatSpeed = 15f;

    public Item item;
    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // �̺�Ʈ ���
        playerInput.actions["Interact"].performed += OnInteract;
        playerInput.actions["ItemIndex"].performed += OnItemIndex;
        playerInput.actions["Use"].performed += OnUse;
        playerInput.actions["SelectWheel"].performed += OnSelectWheel;
        playerInput.actions["Cancel"].performed += OnCancel;
    }

    void Update()
    {
        if (construct)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(hit.point, out navMeshHit, MaxDistance, NavMesh.AllAreas))
                {
                    // ���ø��� ��ġ�� NavMesh ���� �ִ��� Ȯ��
                    if (Vector3.Distance(transform.position, navMeshHit.position) <= MaxDistance)
                    {
                        validPlacement = true;
                        constructPosition = navMeshHit.position;
                        inventoryIndex[selectedSlot].SetActive(true);
                        inventoryIndex[selectedSlot].transform.position = constructPosition;
                        inventoryIndex[selectedSlot].GetComponent<Renderer>().material = materials[0]; // ��ġ ���� ����
                    }
                    else
                    {
                        inventoryIndex[selectedSlot].transform.position = constructPosition;
                        validPlacement = false;
                        inventoryIndex[selectedSlot].GetComponent<Renderer>().material = materials[1]; // ��ġ �Ұ��� ����
                    }
                }
            }
            else
            {
                validPlacement = false;
                inventoryIndex[selectedSlot].GetComponent<Renderer>().material = materials[1]; // ��ġ �Ұ��� ����
            }
        }
    }

    public void Start()
    {
        inven = GameObject.Find("InventoryUi");
        for (int i = 0; i < inventoryImage.Length; i++)
        {
            inventoryImage[i] = inven.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            inventoryImage[i].sprite = null;
            inventoryImage[i].enabled = false;
        }
        selectedSlot = 0;
        SelectSlot();
    }

 

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("F ���ͷ�Ʈ ");

        if (inturectItem != null)
        {
            if (inturectItem.GetComponent<Item>() is PlaceableItem)
            {
                if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length && inturectItem != null)
                {
                    if (inventoryIndex[selectedSlot] != null)
                    {
                        // inventoryIndex�� ��ȸ�Ͽ� null�� �ε����� ã�� inturectItem �Ҵ�
                        bool full = false;
                        for (int i = 0; i < inventoryIndex.Length; i++)
                        {
                            if (inventoryIndex[i] == null)
                            {
                                inventoryIndex[i] = inturectItem;
                                inturectItem.transform.SetParent(inventory);
                                inturectItem.transform.position = inventory.position;
                                inturectItem.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                inturectItem.SetActive(false);
                                Debug.Log($"Item added to slot {i + 1}");
                                inventoryImage[i].sprite = inturectItem.GetComponent<Item>().icon;
                                inventoryImage[i].enabled = true;
                                inturectItem = null; // ���ͷ�Ʈ �������� �Ҵ��� �� null�� �ʱ�ȭ
                                full = true;
                                break;
                            }
                        }

                        if (!full)
                        {
                            Debug.Log("��������"); // ��� �ε����� �� �� �ִ� ���
                            UIManager.inst.SendinturectMessage("�κ��丮�� �� á���ϴ�.");
                        }
                    }
                    else
                    {
                        // ���õ� ������ ��� ������, �� ���Կ� inturectItem �Ҵ�
                        inventoryIndex[selectedSlot] = inturectItem;
                        inturectItem.transform.SetParent(inventory);
                        inturectItem.transform.position = inventory.position;
                        inturectItem.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        inturectItem.SetActive(false);
                        Debug.Log($"Item added to slot {selectedSlot + 1}");
                        inventoryImage[selectedSlot].sprite = inturectItem.GetComponent<Item>().icon;
                        inventoryImage[selectedSlot].enabled = true;
                        inturectItem = null; // ���ͷ�Ʈ �������� �Ҵ��� �� null�� �ʱ�ȭ
                    }
                }
                else
                {
                    Debug.Log("No interactable item or invalid slot selected.");
                }

            }
            else if (inturectItem.GetComponent<Item>() is InstantItem)
            {
                inturectItem.GetComponent<Item>().Use(transform.GetComponent<Player>());
                //�ش罺ũ��Ʈ�� �ִ� Ʈ���������� Player ��ũ��Ʈ���ҷ��� instantitem�� Player���� Use�� �Ѱ���
            }
        }
    }


    private void OnItemIndex(InputAction.CallbackContext context)
    {
        Debug.Log(context.ToString());
        if (context.phase == InputActionPhase.Performed)
        {
            path = context.control.path;
            Debug.Log(path);
            construct = false;
            if (inventoryIndex[selectedSlot] != null)
            {

                inventoryIndex[selectedSlot].gameObject.SetActive(false);
            }
            switch (path)
            {
                case "/Keyboard/1":
                    Debug.Log("1�� �Է��߽��ϴ�.");
                    ResetSlotScale();
                    selectedSlot = 0;
                    SelectSlot();
                    break;
                case "/Keyboard/2":
                    Debug.Log("2�� �Է��߽��ϴ�.");
                    ResetSlotScale();
                    selectedSlot = 1;
                    SelectSlot();
                    break;
                case "/Keyboard/3":
                    Debug.Log("3�� �Է��߽��ϴ�.");
                    ResetSlotScale();
                    selectedSlot = 2;
                    SelectSlot();
                    break;
                case "/Keyboard/4":
                    Debug.Log("4�� �Է��߽��ϴ�.");
                    ResetSlotScale();
                    selectedSlot = 3;
                    SelectSlot();
                    break;
                case "/Keyboard/5":
                    Debug.Log("5�� �Է��߽��ϴ�.");
                    ResetSlotScale();
                    selectedSlot = 4;
                    SelectSlot();
                    break;
                case "/Keyboard/6":
                    Debug.Log("6�� �Է��߽��ϴ�.");
                    ResetSlotScale();
                    selectedSlot = 5;
                    SelectSlot();
                    break;
                case "/Keyboard/7":
                    Debug.Log("7�� �Է��߽��ϴ�.");
                    ResetSlotScale();
                    selectedSlot = 6;
                    SelectSlot();
                    break;
                case "/Keyboard/8":
                    Debug.Log("8�� �Է��߽��ϴ�.");
                    ResetSlotScale();
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
        if (construct && validPlacement)
        {
            inventoryIndex[selectedSlot].transform.position = constructPosition;
            inventoryIndex[selectedSlot].SetActive(true);
            inventoryIndex[selectedSlot].GetComponent<Renderer>().material = inventoryIndex[selectedSlot].GetComponent<PlaceableItem>().originMaterial; // ��Ƽ���� �ʱ�ȭ
            inventoryIndex[selectedSlot].layer = 0;
            inventoryIndex[selectedSlot].transform.SetParent(null);
            inventoryIndex[selectedSlot].GetComponent<PlaceableItem>().Use(transform.GetComponent<Player>());

            ClearInventory();
            construct = false; // construct ���� ����
        }
        else
        {
            if (inventoryIndex[selectedSlot] != null)
            {
                if (inventoryIndex[selectedSlot].GetComponent<Item>() is PlaceableItem)
                {
                    //��ġ�������� ��쿡�� construct�� true�� ������
                    construct = true;
                }
                if (inventoryIndex[selectedSlot].GetComponent<Item>() is InstantItem)
                {
                    inventoryIndex[selectedSlot].GetComponent<Item>().Use(transform.GetComponent<Player>());
                    //�ش罺ũ��Ʈ�� �ִ� Ʈ���������� Player ��ũ��Ʈ���ҷ��� instantitem�� Player���� Use�� �Ѱ���
                }
            }
        }
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

    private void ResetSlotScale()
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
        if (inventoryIndex[selectedSlot] != null)
        {
            if (scrollValue.y > 0f)
            {
                inventoryIndex[selectedSlot].transform.rotation *= Quaternion.Euler(0f, rotatSpeed * Time.unscaledDeltaTime, 0f); // Time.unscaledDeltaTime ���
            }
            else if (scrollValue.y < 0f)
            {
                inventoryIndex[selectedSlot].transform.rotation *= Quaternion.Euler(0f, -rotatSpeed * Time.unscaledDeltaTime, 0f); // Time.unscaledDeltaTime ���
            }
        }

    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        construct = false;
        if (inventoryIndex[selectedSlot] != null)
        {
            inventoryIndex[selectedSlot].gameObject.SetActive(false);
        }
        Debug.Log("Cancel");
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
            item = other.GetComponent<Item>();

            if (item != null)
            {
                inturectItem = other.gameObject;

                bool isfull = true;
                for (int i = 0; i < inventoryIndex.Length; i++)
                {
                    if (inventoryIndex[i] == null)
                    {
                        UIManager.inst.SendinturectMessage("F�� ���� ŉ��");
                        isfull = false;
                        break;
                    }
                }
                if (isfull)
                {
                    UIManager.inst.SendinturectMessage("�κ��丮�� �� á���ϴ�..");
                }
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