using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Transform target;
    
    public void ChaseTarget(Transform targetTransform)
    {
        target = targetTransform;

    }


    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
