using UnityEngine;
using System.Collections.Generic;

public class jinhoongak : MonoBehaviour
{

    public void Start()
    {
        FindMonstersUsingMonsterScript();
    }
    public List<GameObject> FindMonstersUsingMonsterScript()
    {
        List<GameObject> monsters = new List<GameObject>();

        // 모든 게임 오브젝트 검색
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        // Monster 스크립트를 참조하고 있는 오브젝트를 모두 찾음
        foreach (var obj in allGameObjects)
        {
            Monster monsterScript = obj.GetComponent<Monster>();
            if (monsterScript != null)
            {
                // Monster 스크립트를 상속받은 스크립트를 찾음
                Monster_Range monsterRangeScript = obj.GetComponent<Monster_Range>();
                if (monsterRangeScript != null)
                {
                    // Monster_Range 스크립트를 가진 오브젝트를 리스트에 추가
                    monsters.Add(obj);
                }
            }
        }

        return monsters;
    }
}
