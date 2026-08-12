using System.Collections;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] int damage;

    [SerializeField] public float bonusDmg;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
         

        if(collision.CompareTag("Enemy"))
        {
            
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(damage+bonusDmg);
            Debug.Log("HitEnemy");
        }


    }

     
}
