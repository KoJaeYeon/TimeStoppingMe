using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    public void Attack1()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack1();
    }

    public void Attack1_End()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack1_End();
    }

    public void Attack2()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack2();
    }

    public void Attack2_End()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack2_End();
    }

    public void Attack3()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack3();
    }

    public void Attack3_End()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack4_End();
    }

    public void Attack4()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack4();
    }

    public void Attack4_End()
    {
        var monster = transform.parent.GetComponent<Monster>();
        monster.Attack4_End();
    }
}
