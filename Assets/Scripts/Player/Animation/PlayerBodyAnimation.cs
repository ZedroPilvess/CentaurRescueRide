using UnityEngine;

public class PlayerBodyAnimation : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;

    [SerializeField] Rigidbody2D body;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        playerAnimator.speed = body.linearVelocity.magnitude;
        playerAnimator.SetFloat("spd", body.linearVelocity.magnitude);  
    }
}
