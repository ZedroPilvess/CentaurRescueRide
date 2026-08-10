using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHeadMovement : MonoBehaviour
{
    [SerializeField] Camera camera;

    [SerializeField] InputAction aimAction;

    [SerializeField] float offset;

    void Start()
    {
        aimAction = InputSystem.actions.FindAction("Aim");
        camera = Camera.main;   
    }

    // Update is called once per frame
    void Update()
    {
        LockToCamera();
    }

    void LockToCamera()
    {
         Vector2 MouseScreenPos =   aimAction.ReadValue<Vector2>();

        Vector3 mousePos = camera.ScreenToWorldPoint(MouseScreenPos);

        Vector2 dir = transform.position - mousePos;

        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,angle + offset);


    }
}
