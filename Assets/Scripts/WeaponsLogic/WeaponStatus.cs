using System.Collections;
using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    [SerializeField] int damage;

     

    [SerializeField] public float speed;   

    [SerializeField] bool bomb = false;

    [SerializeField] bool bombAOE = false;  

    [SerializeField] bool punch = false;    

    [SerializeField] GameObject explosionEffect;

    [SerializeField] bool projectile =true;

    [SerializeField] int penetration =0;
    [SerializeField] int penetrated =0;

    private void Start()
    {
        if (bomb)
        {
            StartCoroutine(BombExplosion());
        }

        if(projectile)
        {
            Destroy(gameObject, 3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bomb || !bombAOE)
        {
            penetrated++;

            if (penetrated >= penetration)
            {

                Debug.Log("Destroy");
                Destroy(gameObject);
            }
        }

        if(collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
            Debug.Log("HitEnemy");
        }


    }




    IEnumerator BombExplosion()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("Bomb exploded!");
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        explosionEffect.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);    
    }

    
}
