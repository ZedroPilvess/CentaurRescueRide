using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] EnemyScript enemyScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            enemyScript.ChaseTarget(collision.transform);
        }
    }
}
