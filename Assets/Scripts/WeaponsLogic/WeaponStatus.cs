using System.Collections;
using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    [SerializeField] int damage;

    [SerializeField] public float speed;   

    [SerializeField] bool bomb = false;

    [SerializeField] GameObject explosionEffect;

    private void Start()
    {
        if (bomb)
        {
            StartCoroutine(BombExplosion());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bomb)
        {
            

           
          


            Destroy(gameObject);
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
