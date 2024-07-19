using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Elite_2 : Monster
{
    public float overDistance;
    Vector3 previous_Pos;

    public GameObject AttackCollision;
    public GameObject cube;

    public override void Awake()
    {
        base.Awake();

        var sphereCollider = AttackCollision.GetComponent<SphereCollider>();
        var monster_Data_Boss = monster_Data as Monster_Data_Elite_2;
        sphereCollider.radius = monster_Data_Boss.skill_bite_Distance;
        AttackCollision.SetActive(false);

        overDistance = 0f;
        previous_Pos = transform.position;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Attack1();
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            Attack3();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Attack3_End();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            Attack4();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Attack4_End();
        }

        CalculateOverDistance();
    }

    public void CalculateOverDistance()
    {
        float distance = Vector3.Distance(transform.position, previous_Pos);
        overDistance += distance;
        previous_Pos = transform.position;
    }

    public override void Attack1()
    {
        
    }

    public override void Attack2()
    {
        AttackCollision.SetActive(true);
        cube.SetActive(true);
    }
    public override void Attack2_End()
    {
        AttackCollision.SetActive(false);
        cube.SetActive(false);
    }
    //public override void DrawArc()
    //{
    //    if (AttackCollision.activeSelf == true)
    //    {
    //        Handles.color = Color.red;
    //        var monster_Data_Elite2 = monster_Data as Monster_Data_Elite_2;
    //        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, monster_Data.Search_Range / 2, monster_Data_Elite2.skill_bite_Distance);
    //        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -monster_Data.Search_Range / 2, monster_Data_Elite2.skill_bite_Distance);
    //    }
    //}
}
