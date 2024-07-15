using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBox : MonoBehaviour
{
    public int Price;
    public int U_Num;
    public Animator Ani;
    public GameObject player;
    public GameObject playerPos;
    void Start()
    {
        Price = 0;
        //GameManager.Instance.price = 0;
        //U_Num= GameManager.Instance.U_Num;
        //게임시작시 금액을 리셋
    }

    public void InsertBox(int price)
    {
        Price += price;
    }
    public void InsertGameManager(ItemS Name)
    {
        //GameManager.Instance.items.Add(Name);
        //GameManager.Instance.names.Add(Name.DataName);
        //GameManager.Instance.itemPrice.Add(Name.Price);
    }

    public void ExitSuccess(GameObject player)
    {
        Ani.SetTrigger("Exit");
        //GameManager.Instance.price = Price;
        //DBManager.Instance.UpdateMoney(U_Num, Price);
        //player = GameObject.FindWithTag("Player");
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.SetParent(playerPos.transform);
        player.transform.position = playerPos.transform.position;
    }
    public void ExitFail()
    {
        //GameManager.Instance.price = 0;

    }
    public void Update()
    {

    }
}
