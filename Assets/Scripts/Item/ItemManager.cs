using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public Monster[] monsters;

    public static ItemManager inst; // ΩÃ±€≈Ê ¿ŒΩ∫≈œΩ∫
    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



}
