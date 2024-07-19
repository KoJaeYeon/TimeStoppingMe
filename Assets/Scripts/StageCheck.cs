using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageCheck : MonoBehaviour
{
    List<Monster> m_MonsterList = new List<Monster>();
    public bool playerEnter = false;

    public GameObject Door;

    private void Update()
    {
        foreach (Monster m in m_MonsterList)
        {
            if(m == null)
            {
                m_MonsterList.Remove(m);
            }
        }

        if(playerEnter)
        {
            if(m_MonsterList.Count == 0)
            {
                Destroy(transform.gameObject);
            }
        }
    }

    public void OnPlayerEntered()
    {
        playerEnter = true;
        Door.SetActive(true);
        foreach (Monster m in m_MonsterList)
        {
            m.playerEnter = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var monster = other.GetComponent<Monster>();
            m_MonsterList.Add(monster);
        }
    }
}
