using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1.5f;

    [SerializeField] float speed = 3f;

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction turnAction;
    [SerializeField] InputAction breakAction;

    [SerializeField] float breakForce = 5f; 

    [SerializeField] float moveValue;
    [SerializeField] float turnValue;

    [SerializeField] Rigidbody2D rigidbody;



    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Foward");
        turnAction = InputSystem.actions.FindAction("Turn");
        breakAction = InputSystem.actions.FindAction("Break");  
        rigidbody = GetComponent<Rigidbody2D>();

       
    }  
       
    private void FixedUpdate()
    {
        Move();
        Rotate();
        Break();    
    }

    void Break()
    {
        if (breakAction.IsPressed() && rigidbody.linearVelocity.magnitude > 0.01f)
        {
            rigidbody.AddForce(-rigidbody.linearVelocity.normalized * breakForce,ForceMode2D.Force);
        }
    }

    void Move()
    {
        float moveValue = moveAction.ReadValue<float>();
        rigidbody.AddForce(transform.up * speed * moveValue,ForceMode2D.Force);
    }

    void Rotate()
    {
        float turnValue = turnAction.ReadValue<float>();
        rigidbody.AddTorque(turnValue * turnSpeed);
    }
}
