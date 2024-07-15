using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject[] inventoryIndex = new GameObject[5];
    public UnityEngine.InputSystem.PlayerInput playerInput;

    public string path;
    [SerializeField]
    public int selectedSlot = 0; // 선택된 슬롯 번호
    public Transform inventory;
    public float throwSpeed;
    private bool inBox;

    public Image[] inventoryImage;
    private Vector3 rayStart;
    private Vector3 rayEnd;

    public InBox inBoxScript;
    public void Awake()
    {
        inventoryIndex = new GameObject[5];
        inventoryImage = new Image[5];
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
    }

    public void Start()
    {
        inventory=transform.GetChild(2);
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

    void Update()
    {
        // 매 프레임마다 레이캐스트를 업데이트
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
    }

    void OnInteract(InputValue inputValue) // F 키를 눌렀을 때 실행
    {

        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);// 카메라의 뷰포인트로 레이캐스트 실행

        // LayerMask 설정 - Player 레이어를 제외한 모든 레이어
        int layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        Debug.Log("OnInteract");

        // RaycastAll을 사용하여 모든 히트 결과를 가져옵니다.
        RaycastHit[] hits = Physics.RaycastAll(ray, range, layerMask);

        // 첫 번째로 히트한 "Item" 태그를 가진 오브젝트를 찾습니다.
        GameObject hitItem = null;
        GameObject Box=null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == "Item")
            {
                hitItem = hit.transform.gameObject;
                break;
            }
        }

        if (inventoryIndex[selectedSlot] != null) // 선택된 슬롯이 비어있지 않은 경우
        {
            RaycastHit[] hits2 = Physics.RaycastAll(ray, range, layerMask);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "Box")
                {
                    Box = hit.transform.gameObject;
                    
                    int price = inventoryIndex[selectedSlot].GetComponent<ItemS>().Price;
                    Box.GetComponent<InBox>().InsertBox(price);
                    Box.GetComponent<InBox>().InsertGameManager(inventoryIndex[selectedSlot].GetComponent<ItemS>());
                    inBox = true;
                    break;
                }
            }

            if(inBox)
            {
                GameObject item = inventoryIndex[selectedSlot];
                //아이템을 박스의 자식으로 둠
                item.transform.SetParent(Box.transform);
                item.transform.position= Box.transform.position;
                item.SetActive(true);
                item.tag = "InBox";
                item.layer = 10;
                //item.transform.localScale = item.transform.localScale * 0.5f; //크기를 반으로 줄인다
                inventoryImage[selectedSlot].sprite = null; // 해당 슬롯의 이미지를 Null로 만듦
                inventoryImage[selectedSlot].enabled = false;
                inventoryIndex[selectedSlot] = null; // 슬롯 비우기
                inBox=false;
            }
            else
            {
                GameObject item = inventoryIndex[selectedSlot];

                // 아이템의 부모를 해제하고 독립 객체로 만듦
                //GameObject item = inventoryIndex[selectedSlot];
                item.transform.SetParent(null);
                item.SetActive(true);
                inventoryImage[selectedSlot].sprite = null; // 해당 슬롯의 이미지를 Null로 만듦
                inventoryImage[selectedSlot].enabled = false;

                // Ray를 쏜 지점의 트랜스폼에 해당 물건을 놓음
                //if (hitItem != null)
                //{
                //    Vector3 point = (hit.point - item.transform.position).normalized;
                //    Rigidbody rd = item.GetComponent<Rigidbody>();
                //    rd.AddForce(point * throwSpeed, ForceMode.Impulse);
                //    Debug.Log($"Item placed at {hit.point}");
                //    Debug.Log($"Item placed at {hitItem.name}");
                //}
                //else
                //{
                Vector3 point = ray.direction.normalized;
                Rigidbody rd = item.GetComponent<Rigidbody>();
                rd.velocity = Vector3.zero;
                rd.AddForce(point * throwSpeed, ForceMode.Impulse);
                //}
                inventoryIndex[selectedSlot] = null; // 슬롯 비우기
            }
           
        }
        else // 선택된 슬롯이 비어있는 경우
        {
            if (hitItem != null) // 히트한 "Item" 태그를 가진 오브젝트가 있을 경우에 실행
            {
                if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length) // 유효한 슬롯이 선택된 경우
                {
                    if (inventoryIndex[selectedSlot] == null) // 선택된 슬롯이 비어있는 경우
                    {
                        inventoryIndex[selectedSlot] = hitItem;
                        hitItem.transform.SetParent(inventory);
                        hitItem.transform.position = inventory.position;
                        hitItem.transform.rotation= Quaternion.Euler(0f, 0f, 0f);
                        hitItem.SetActive(false);
                        Debug.Log($"Item added to slot {selectedSlot + 1}");
                        inventoryImage[selectedSlot].sprite = hitItem.GetComponent<ItemS>().Image;
                        inventoryImage[selectedSlot].enabled = true;
                    }
                    else
                    {
                        Debug.Log("Selected slot is already occupied.");
                    }
                }
                else
                {
                    Debug.Log("No valid slot selected.");
                }
            }
            else
            {
                Debug.Log("Nothing hit.");
            }
        }
    }

    private void OnSelect(InputValue value)
    {
        // 현재 액션맵과 액션을 가져옴
        var action = playerInput.actions["Select"];

        // 액션이 실행 중인지 확인
        if (action.phase == InputActionPhase.Performed)
        {
            // 입력된 키의 Path 가져오기
            path = action.activeControl.path;

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
                default:
                    break;
            }
        }
    }
    private void OnUse(InputValue value)
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

                    default :
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
    private void ResetSlot ()
    {
        inventoryImage[selectedSlot].transform.parent.transform.localScale = Vector3.one;
    }

    void OnDrawGizmos()
    {
        // 레이캐스트의 시작점과 끝점을 계속해서 그립니다.
        if (ray.origin != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * range);
        }
    }
    void OnSelectWheel(InputValue inputValue)
    {
        Vector2 scrollValue = inputValue.Get<Vector2>();
        if (scrollValue.y > 0f) //슬롯 증가
        {
            if(4>selectedSlot)//선택슬롯이 5보다 작을시
            {
                ResetSlot();
                
                selectedSlot++;
                SelectSlot();
            }
        }
        else if (scrollValue.y < 0f)//슬롯감소
        {
            if(0<selectedSlot) //선택한 슬롯이 1보다 클시
            {
                ResetSlot();
                Debug.Log(selectedSlot);
                selectedSlot--;
                SelectSlot();

            }
        }
        Debug.Log(inputValue.ToString());
        Debug.Log(scrollValue);
        Debug.Log(scrollValue.sqrMagnitude);
        Debug.Log(scrollValue.ToString());
    }
}


//using Mirror.Examples.BilliardsPredicted;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;

//public class Inventory : MonoBehaviour
//{
//    //public List<Item> items;
//    public Camera mainCamera;
//    Ray ray;
//    RaycastHit hit;
//    [SerializeField]
//    public float range = 100f;
//    //public List<GameObject> inventoryIndex = new List<GameObject>();
//    [SerializeField]
//    public GameObject[] inventoryIndex = new GameObject[5];
//    public  UnityEngine.InputSystem.PlayerInput playerInput;

//    public string path;
//    [SerializeField]
//    public int selectedSlot = 0; // 선택된 슬롯 번호
//    public Transform inventory;
//    public float throwSpeed;

//    public Image[] inventoryImage;
//    public void Awake()
//    {
//       // items.Clear(); // 게임이 시작시 아이템 인벤토리를 클리어
//        inventoryIndex = new GameObject[5];
//        //inventoryUIIndex = new GameObject[5];
//        inventoryImage = new Image[5];

//        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
//    }
//    public void Start()
//    {
//        GameObject inventoryUI = GameObject.Find("InventoryUi");
//        for (int i = 0; i < inventoryImage.Length; i++)
//        {
//            inventoryImage[i] = inventoryUI.transform.GetChild(i).GetChild(0).GetComponent<Image>();
//            inventoryImage[i].sprite = null;
//            inventoryImage[i].enabled= false;
//        }
//    }

//    void OnInteract(InputValue inputValue) // F 키를 눌렀을 때 실행
//    {
//        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); // 카메라의 뷰포인트로 레이캐스트 실행
//        //ray = mainCamera.ViewportPointToRay(Vector2.one); // 카메라의 뷰포인트로 레이캐스트 실행
//        Debug.Log("OnInteract");
//        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

//        if (inventoryIndex[selectedSlot] != null) // 선택된 슬롯이 비어있지 않은 경우
//        {
//            // 아이템의 부모를 해제하고 독립 객체로 만듦
//            GameObject item = inventoryIndex[selectedSlot];
//            item.transform.SetParent(null);
//            item.SetActive(true);
//            inventoryImage[selectedSlot].sprite=null;//해당슬롯의 이미지를 Null로 만듬
//            inventoryImage[selectedSlot].enabled = false;

//            // Ray를 쏜 지점의 트랜스폼에 해당 물건을 놓음
//            if (Physics.Raycast(ray, out hit, range))
//            {
//                Vector3 point = (hit.point - item.transform.position).normalized;
//                Debug.Log(point.sqrMagnitude);
//                Debug.Log(point);
//                Rigidbody rd = item.GetComponent<Rigidbody>();
//                rd.AddForce(point* throwSpeed, ForceMode.Impulse);
//                //item.transform.position = hit.point;
//                Debug.Log($"Item placed at {hit.point}");
//            }
//            else
//            {
//                Vector3 point = ray.direction.normalized;
//                Rigidbody rd = item.GetComponent<Rigidbody>();
//                rd.AddForce(point * throwSpeed, ForceMode.Impulse);
//            }
//            inventoryIndex[selectedSlot] = null; // 슬롯 비우기
//        }
//        else // 선택된 슬롯이 비어있는 경우
//        {
//            if (Physics.Raycast(ray, out hit, range))
//            {
//                if (hit.transform.tag == "Item") // 히트한 물체의 태그가 Item일 경우에 실행
//                {
//                    if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length) // 유효한 슬롯이 선택된 경우
//                    {
//                        if (inventoryIndex[selectedSlot] == null) // 선택된 슬롯이 비어있는 경우
//                        {

//                            inventoryIndex[selectedSlot] = hit.transform.gameObject;
//                            hit.transform.SetParent(transform);
//                            hit.transform.position = inventory.position;
//                            hit.transform.gameObject.SetActive(false);
//                            Debug.Log($"Item added to slot {selectedSlot + 1}");
//                            inventoryImage[selectedSlot].sprite = hit.transform.GetComponent<Image>().sprite;
//                            inventoryImage[selectedSlot].enabled = true;
//                        }
//                        else
//                        {
//                            Debug.Log("Selected slot is already occupied.");
//                        }
//                    }
//                    else
//                    {
//                        Debug.Log("No valid slot selected.");
//                    }
//                }
//                else
//                {
//                    Debug.Log(hit.transform.tag);
//                }
//            }
//            else
//            {
//                Debug.Log("Nothing hit.");
//            }
//        }

//        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
//    }
//    private void OnSelect(InputValue value)
//    {
//        // 현재 액션맵과 액션을 가져옴
//        var action = playerInput.actions["Select"];

//        // 액션이 실행 중인지 확인
//        if (action.phase == InputActionPhase.Performed)
//        {
//            // 입력된 키의 Path 가져오기
//            path = action.activeControl.path;

//            // Path 값을 기반으로 switch 문 실행
//            Debug.Log(path);

//            switch (path)
//            {
//                case "/Keyboard/1":
//                    Debug.Log("1을 입력했습니다.");
//                    selectedSlot = 0;
//                    break;
//                case "/Keyboard/2":
//                    Debug.Log("2를 입력했습니다.");
//                    selectedSlot = 1;
//                    break;
//                case "/Keyboard/3":
//                    Debug.Log("3을 입력했습니다.");
//                    selectedSlot = 2;
//                    break;
//                case "/Keyboard/4":
//                    Debug.Log("4을 입력했습니다.");
//                    selectedSlot = 3;
//                    break;
//                case "/Keyboard/5":
//                    Debug.Log("5을 입력했습니다.");
//                    selectedSlot = 4;
//                    break;
//                default:
//                    break;
//            }
//        }
//    }


//}



