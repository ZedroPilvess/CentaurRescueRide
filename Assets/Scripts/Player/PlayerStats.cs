using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int hp;
    
    [SerializeField] public int maxHP = 3;

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



    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
        if (hp > maxHP) 
        { 
            hp = maxHP; 

        }
    }   


    private void Die()
    {
        Debug.Log("Player Died");
        // Add death logic here (e.g., play animation, disable player controls, etc.)
    }
}
