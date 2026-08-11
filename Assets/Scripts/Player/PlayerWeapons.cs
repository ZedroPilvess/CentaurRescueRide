using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private PlayerStats ps;
    [SerializeField] private PlayerHeadMovement phm;

    [Header("Input")]
    [SerializeField] private InputAction shoot;
    //[SerializeField] private InputAction kick;

    [Header("Weapon Points")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject shurikenPoint1;
    [SerializeField] private GameObject shurikenPoint2;
    [SerializeField] private GameObject shurikenPoint3;

    [Header("Weapon Prefabs")]
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private GameObject bombObj;
    [SerializeField] private GameObject shurikenObj;
    [SerializeField] private GameObject punchHitbox;

    [Header("Punch")]
    [SerializeField] private bool isPreparing = false;
    [SerializeField] private float preparationCount = 0f;


    [Header("UI")]
    [SerializeField] bool isOverUI = false; 

    void Start()
    {
        shoot = InputSystem.actions.FindAction("Shoot");

        shoot.performed += ctx => Shoot();
        
        shoot.canceled += ctx => StopShoot();

    }

    private void Update()
    {
        isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    private void StopShoot()
    {
        if (isOverUI) return;
        if (ps.equipedItem == null) return;
        switch (ps.equipedItem.Type)
        {
             
                
            case ItemType.Bomb:
                ThrowBomb();
                break;        
            case ItemType.Punch:
                ReleasePunch();
                break;
        }
    }

    void Shoot()
    {
        if(isOverUI) return; 
        if (!isPreparing && ps.equipedItem != null )
        {
            
            switch (ps.equipedItem.Type)
            {
                case ItemType.Gun:
                    ShootFuzil();
                    break;
                case ItemType.Bomb:
                    PrepareBomb();
                    break;
                case ItemType.Shuriken:
                    ThrowShuriken();
                    break;
                case ItemType.Punch:
                    PreparePunch();
                    break;
                case ItemType.Empty:
                    Debug.Log("No weapon equipped");
                    break;
            }
        }
    }



    IEnumerator PreparingCharge()
    {
        while (isPreparing && preparationCount < 1f)
        {
             
            preparationCount +=   0.01f;
            yield return new WaitForSeconds(0.01f);
            if (preparationCount >= 1f)
            {
                preparationCount = 1f;
            }
        }

    }
    


#region PunchLogic
    private void PreparePunch()
    {
        preparationCount = 0f;
        Debug.Log("Preparing Punch");
        if (!isPreparing)
        {
            isPreparing = true;

            StartCoroutine(PreparingCharge());

        }
    }

    void ReleasePunch()
    {
        
        isPreparing = false;
        StartCoroutine(PunchCoroutine());
    }

    IEnumerator PunchCoroutine()
    {

        punchHitbox.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        punchHitbox.SetActive(false);
        preparationCount = 0f;

    }

    #endregion


#region BombLogic

    private void PrepareBomb()
    {
        preparationCount = 0f;
        Debug.Log("Preparing Bomb");
        if (!isPreparing)
        {
            isPreparing = true;

            StartCoroutine(PreparingCharge());

        }
    }

    void ThrowBomb()
    {
        isPreparing = false;
        
        Vector2 target = phm.mousePos;

        GameObject obj = Instantiate(bombObj, shootPoint.position, Quaternion.identity);

         GoToTarget(obj, target);

       
    }


    #endregion


    #region ShurikenLogic
 void ThrowShuriken()
{
    GameObject shuriken1 = Instantiate(
        shurikenObj,
        shurikenPoint1.transform.position,
        Quaternion.identity
    );

    GameObject shuriken2 = Instantiate(
        shurikenObj,
        shurikenPoint2.transform.position,
        Quaternion.identity
    );

    GameObject shuriken3 = Instantiate(
        shurikenObj,
        shurikenPoint3.transform.position,
        Quaternion.identity
    );

        ThrowShurikenUp(shuriken1, shurikenPoint1.transform);
        ThrowShurikenUp(shuriken2, shurikenPoint2.transform);
        ThrowShurikenUp(shuriken3, shurikenPoint3.transform);
    }
    void ThrowShurikenUp(GameObject shuriken, Transform point)
    {
        Rigidbody2D rb = shuriken.GetComponent<Rigidbody2D>();
        WeaponStatus weaponStatus = shuriken.GetComponent<WeaponStatus>();

        rb.AddForce(point.up * weaponStatus.speed,ForceMode2D.Impulse);
        rb.AddTorque(10f, ForceMode2D.Impulse);
    }

    #endregion

    #region GunLogic
    void ShootFuzil()
    {
        GameObject obj = Instantiate(bulletObj, shootPoint.position, shootPoint.rotation);

        GoToTarget(obj, phm.mousePos);





    }



    #endregion


    void GoToTarget(GameObject obj, Vector2 target)
    {
        Vector2 direction = (target - (Vector2)obj.transform.position).normalized;
        
        obj.GetComponent<Rigidbody2D>().AddForce(direction * obj.GetComponent<WeaponStatus>().speed * ((1 + preparationCount) ) , ForceMode2D.Impulse);
       
        Debug.Log("velocity : " + obj.GetComponent<Rigidbody2D>().linearVelocity);
        Debug.Log("preparationCount : " + preparationCount);

    }
}
