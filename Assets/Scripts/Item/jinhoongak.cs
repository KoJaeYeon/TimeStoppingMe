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

        // ��� ���� ������Ʈ �˻�
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        // Monster ��ũ��Ʈ�� �����ϰ� �ִ� ������Ʈ�� ��� ã��
        foreach (var obj in allGameObjects)
        {
            Monster monsterScript = obj.GetComponent<Monster>();
            if (monsterScript != null)
            {
                // Monster ��ũ��Ʈ�� ��ӹ��� ��ũ��Ʈ�� ã��
                Monster_Range monsterRangeScript = obj.GetComponent<Monster_Range>();
                if (monsterRangeScript != null)
                {
                    // Monster_Range ��ũ��Ʈ�� ���� ������Ʈ�� ����Ʈ�� �߰�
                    monsters.Add(obj);
                }
            }
        }

        return monsters;
    }
}
