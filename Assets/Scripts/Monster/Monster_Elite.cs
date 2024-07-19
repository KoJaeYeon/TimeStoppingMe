using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Elite : Monster
{
    public Transform[] LaunchTrans; // 여러 발사 위치를 가지는 배열

    GameObject projectile_Prefab;

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

    public void SpawnProjectile_loveshot()
    {
        var monster_Data_Boss = monster_Data as Monster_Data_Elite;
        projectile_Prefab = monster_Data_Boss.scratch_Prefab;

        // 첫 번째 발사 위치: 타겟을 직접 향함
        SpawnProjectile(LaunchTrans[0], TargetTrans.position, monster_Data_Boss);

        // 두 번째 발사 위치: 타겟을 향해 왼쪽 30도 각도로
        Vector3 leftDirection = Quaternion.Euler(0, -30, 0) * (TargetTrans.position - LaunchTrans[1].position);
        SpawnProjectile(LaunchTrans[1], LaunchTrans[1].position + leftDirection, monster_Data_Boss);

        // 세 번째 발사 위치: 타겟을 향해 오른쪽 30도 각도로
        Vector3 rightDirection = Quaternion.Euler(0, 30, 0) * (TargetTrans.position - LaunchTrans[2].position);
        SpawnProjectile(LaunchTrans[2], LaunchTrans[2].position + rightDirection, monster_Data_Boss);
    }

    private void SpawnProjectile(Transform launchTransform, Vector3 targetPosition, Monster_Data_Elite monsterData)
    {
        var projecile = Instantiate(projectile_Prefab, launchTransform.position, Quaternion.identity);
        var projectile_Loveshot = projecile.GetComponent<Projectile_LoveShot>();

        // 타겟 방향 계산
        Vector3 direction = (targetPosition - launchTransform.position).normalized;

        // 적절한 값으로 초기화
        projectile_Loveshot.Init(launchTransform.position, direction, monsterData.skill_scratch_Distance, monsterData.skill_scratch_LaunchSpeed);
    }

    public override void Attack1()
    {
        SpawnProjectile_loveshot();
    }

    public override void Attack2()
    {

    }
    public override void Attack2_End()
    {

    }
    //public override void DrawArc()
    //{
    //    //if(AttackCollision.activeSelf == true)
    //    //{
    //    //    Handles.color = Color.red;
    //    //    var monster_Data_Boss = monster_Data as Monster_Data_Boss;
    //    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, monster_Data.Search_Range / 2, monster_Data_Boss.skill_bite_Distance);
    //    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -monster_Data.Search_Range / 2, monster_Data_Boss.skill_bite_Distance);
    //    //}        
    //}
}
