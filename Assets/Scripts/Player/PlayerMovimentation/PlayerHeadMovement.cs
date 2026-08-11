using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHeadMovement : MonoBehaviour
{
    [SerializeField] Camera camera;

    [SerializeField] InputAction aimAction;

    [SerializeField] float offset;

    [SerializeField] public Vector3 mousePos;

    void Start()
    {
        aimAction = InputSystem.actions.FindAction("Aim");
        camera = Camera.main;   
    }
     
    void Update()
    {
        LockToCamera();
    }

    void LockToCamera()
    {
         Vector2 MouseScreenPos =   aimAction.ReadValue<Vector2>();

         mousePos = camera.ScreenToWorldPoint(MouseScreenPos);

        Vector2 dir = transform.position - mousePos;

        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,angle + offset);


    }
}
