using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int hp;
    
    [SerializeField] public int maxHP = 3;
    [SerializeField] Sprite spriteFullHP;
    [SerializeField] Sprite spriteHurtHP;
    [SerializeField] Sprite spriteDyingHP;

    [SerializeField] Image hpImg;

    [SerializeField] public int rescuedTargets = 0; 

    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] public GameObject playerObj;

    [SerializeField] public ItemSO equipedItem;

    [SerializeField] GameObject compass;

    [SerializeField] Transform questTarget; 

    [SerializeField] bool isQuesting = false;

    [SerializeField] public  GameObject piggyBackObj;

    [SerializeField] AudioSource source;

    [SerializeField] AudioClip hurtSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (!inventoryManager.IsIventoryFull())
            {
                inventoryManager.AddItem(collision.GetComponent<ItemCollectable>().Item);
               Destroy(collision.gameObject);

            }     
        }
    }



    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
            return;
        }
        if (hp > maxHP) 
        { 
            hp = maxHP; 

        }

        switch (hp)
        {
            case 1:

                hpImg.sprite = spriteDyingHP;


            break;

            case 2:
  
               hpImg.sprite = spriteHurtHP;
            break;

            case 3:
               hpImg.sprite= spriteFullHP;
             break;
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
