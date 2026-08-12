using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Transform target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool canDamage = true;

    [SerializeField] float hp = 4;

    [SerializeField] Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    public void ChaseTarget(Transform targetTransform)
    {
        target = targetTransform;
        animator.SetTrigger("follow");

    }
    public void TakeDamage(float damage)
    {
               hp -= damage;

        
        if (hp <= 0)
        {
           StartCoroutine(DieAfterDelay());
        }
    }


    IEnumerator DieAfterDelay()
    {
        speed = 0f;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;   
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
           collision.gameObject.GetComponent<PlayerStats>().TakeDamage(1);
            StartCoroutine(DamageCooldown());
        }
    }


    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
}
