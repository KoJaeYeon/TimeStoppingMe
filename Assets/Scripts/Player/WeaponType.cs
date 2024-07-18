using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour
{
    [SerializeField] private string weaponTypeName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            switch (weaponTypeName)
            {
                case "TypeA":
                    player.EquipWeapon<WeaponTypeA>();
                    break;
                case "TypeD":
                    player.EquipWeapon<WeaponTypeD>();
                    break;
                    // 다른 무기 타입 추가
            }
        }
    }
}
