using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Elite_2 : Monster
{
    public Transform[] LaunchTrans; // 여러 발사 위치를 가지는 배열

    public float overDistance;
    Vector3 previous_Pos;
    

    public override void Awake()
    {
        base.Awake();
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

    }
    public override void Attack2_End()
    {

    }
    public override void DrawArc()
    {
        //if(AttackCollision.activeSelf == true)
        //{
        //    Handles.color = Color.red;
        //    var monster_Data_Boss = monster_Data as Monster_Data_Boss;
        //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, monster_Data.Search_Range / 2, monster_Data_Boss.skill_bite_Distance);
        //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -monster_Data.Search_Range / 2, monster_Data_Boss.skill_bite_Distance);
        //}        
    }
}
