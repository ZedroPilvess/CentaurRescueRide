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

    [SerializeField] GameObject compass;

    [SerializeField] Transform questTarget; 

    [SerializeField] bool isQuesting = false;

    [SerializeField] public  GameObject piggyBackObj;

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
    private void Update()
    {
        if(isQuesting && questTarget != null)
        {
            compass.transform.rotation = Quaternion.LookRotation(Vector3.forward, questTarget.position - playerObj.transform.position);
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        // Add death logic here (e.g., play animation, disable player controls, etc.)
    }



    public void QuestTarget(Transform target)
    {
        isQuesting = true;
        compass.SetActive(true);
        questTarget = target;

    }

    public void StopQuest()
    {
        compass.SetActive(false);
        isQuesting = false;
    }
}
