using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterCheck : MonoBehaviour
{
    public StageCheck StageCheck;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StageCheck.OnPlayerEntered();
        }
    }
}
