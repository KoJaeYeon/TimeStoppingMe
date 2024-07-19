using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Air : Monster
{
    public float overDistance;
    Vector3 previous_Pos;

    public GameObject AttackCollision;
    public Transform grapPos;
    public bool isgraped = false;
    

    public override void Awake()
    {
        base.Awake();

        var boxCollider = AttackCollision.GetComponent<BoxCollider>();
        var monster_Data_Air = monster_Data as Monster_Data_Air;
        var col = boxCollider.gameObject;
        col.transform.localScale = new Vector3(monster_Data_Air.Grap_x, monster_Data_Air.Grap_y, monster_Data_Air.Grap_z);
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
        AttackCollision.SetActive(true);        
    }
    public override void Attack1_End()
    {
        AttackCollision.SetActive(false);
    }

    public override void Attack2()
    {
        TargetTrans.SetParent(grapPos);
        TargetTrans.localPosition = Vector3.zero;
        var iattack = TargetTrans.GetComponent<IAttackable>();
        iattack.OnTakeDebuffed(DebuffType.Supress, new Debuff_Suppress());

        TargetTrans.GetComponent<NavMeshAgent>().enabled = false;
    }
    public override void Attack2_End()
    {
        TargetTrans.SetParent(null);
        Vector3 pos = TargetTrans.localPosition;
        pos.y = 1;
        TargetTrans.localPosition =pos;

        var iattack = TargetTrans.GetComponent<IAttackable>();
        iattack.OnTakeDamaged(monster_Data.Damage);
        TargetTrans.GetComponent<NavMeshAgent>().enabled = true;
    }
    public override void DrawArc()
    {
    }
}
