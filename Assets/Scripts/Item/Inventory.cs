using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    public Material[] materials;
    public GameObject inven;

    public bool construct;
    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // 이벤트 등록
        playerInput.actions["Interact"].performed += OnInteract;
        playerInput.actions["ItemIndex"].performed += OnItemIndex;
        playerInput.actions["Use"].performed += OnUse;
        playerInput.actions["SelectWheel"].performed += OnSelectWheel;
    }

    void FixedUpdate()
    {
        if (construct)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(hit.point, out navMeshHit, 100f, NavMesh.AllAreas))
                {
                    // 샘플링된 위치가 NavMesh 위에 있는지 확인
                    if (Vector3.Distance(transform.position, navMeshHit.position) <= 5f)
                    {
                        foreach (GameObject obj in inventoryIndex)
                        {
                            if (obj != null)
                            {
                                obj.SetActive(true);
                                obj.transform.position = navMeshHit.position;
                                obj.GetComponent<Renderer>().material = materials[0]; // 설치 가능 영역
                            }
                        }
                    }
                    else
                    {
                        // 조건을 만족하지 않으면 비활성화
                        foreach (GameObject obj in inventoryIndex)
                        {
                            if (obj != null)
                            {
                                //obj.SetActive(false);
                                //obj.transform.position = navMeshHit.position;
                                obj.GetComponent<Renderer>().material = materials[1]; // 설치 불가능 영역
                            }
                        }
                    }
                }
                else
                {
                    // NavMesh 샘플링이 실패하면 비활성화
                    foreach (GameObject obj in inventoryIndex)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                            //obj.transform.position = navMeshHit.position;
                            obj.GetComponent<Renderer>().material = materials[1]; // 설치 불가능 영역
                        }
                    }
                }
            }
        }
    }


    public void Start()
    {
        //GameObject inventoryUI = GameObject.Find("InventoryUi");
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
                    ResetSlotScale();
                    selectedSlot = 0;
                    SelectSlot();
                    break;
                case "/Keyboard/2":
                    Debug.Log("2를 입력했습니다.");
                    ResetSlotScale();
                    selectedSlot = 1;
                    SelectSlot();
                    break;
                case "/Keyboard/3":
                    Debug.Log("3을 입력했습니다.");
                    ResetSlotScale();
                    selectedSlot = 2;
                    SelectSlot();
                    break;
                case "/Keyboard/4":
                    Debug.Log("4을 입력했습니다.");
                    ResetSlotScale();
                    selectedSlot = 3;
                    SelectSlot();
                    break;
                case "/Keyboard/5":
                    Debug.Log("5을 입력했습니다.");
                    ResetSlotScale();
                    selectedSlot = 4;
                    SelectSlot();
                    break;
                case "/Keyboard/6":
                    Debug.Log("6을 입력했습니다.");
                    ResetSlotScale();
                    selectedSlot = 5;
                    SelectSlot();
                    break;
                case "/Keyboard/7":
                    Debug.Log("7을 입력했습니다.");
                    ResetSlotScale();
                    selectedSlot = 6;
                    SelectSlot();
                    break;
                case "/Keyboard/8":
                    Debug.Log("8을 입력했습니다.");
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
        if (construct)
        {

            inventoryIndex[selectedSlot].SetActive(true);
            inventoryIndex[selectedSlot].GetComponent<Renderer>().material = inventoryIndex[selectedSlot].GetComponent<InstantItem>().originMaterial; // 머티리얼 초기화
            inventoryIndex[selectedSlot].transform.SetParent(null);
            ClearInventory();
            construct = false; // construct 상태 종료
        }
        else
        {
            bool use = false;
            InstantItem item = null;
            if (inventoryIndex[selectedSlot] != null)
            {
                construct = true;
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
        if (scrollValue.y > 0f)
        {
            if (7 > selectedSlot)
            {
                ResetSlotScale();
                selectedSlot++;
                SelectSlot();
            }
        }
        else if (scrollValue.y < 0f)
        {
            if (0 < selectedSlot)
            {
                ResetSlotScale();
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
