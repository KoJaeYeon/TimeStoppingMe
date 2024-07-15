using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Item item = GetComponent<Item>();
            if (item != null)
            {
                InteractWithItem(item);
            }
        }
    }

    private void InteractWithItem(Item item)
    {
        if (item is InstantItem)
        {
            item.Use(player);
        }
        else if (item is CollectableItem)
        {
            item.Use(player);
        }
    }
}
