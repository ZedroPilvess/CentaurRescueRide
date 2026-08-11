using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int hp;
    
    [SerializeField] public int maxHP = 100;

    [SerializeField] public int rescuedTargets = 0; 

    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] public GameObject playerObj;

    [SerializeField] public ItemSO equipedItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (!inventoryManager.IsIventoryFull())
            {
                inventoryManager.AddItem(collision.GetComponent<ItemCollectable>().Item);
                collision.gameObject.SetActive(false);

            }     
        }
    }

 
}
