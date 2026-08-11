using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int hp;
    
    [SerializeField] int maxHP = 100;

    [SerializeField] InventoryManager inventoryManager;

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
