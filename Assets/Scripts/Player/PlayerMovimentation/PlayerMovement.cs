using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1.5f;

    [SerializeField] float speed = 3f;

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction turnAction;

    [SerializeField] float moveValue;
    [SerializeField] float turnValue;

    [SerializeField] Rigidbody2D rigidbody;




    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Foward");
        turnAction = InputSystem.actions.FindAction("Turn");

        rigidbody = GetComponent<Rigidbody2D>();

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void FixedUpdate()
    {
        Move();
        Rotate();
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
