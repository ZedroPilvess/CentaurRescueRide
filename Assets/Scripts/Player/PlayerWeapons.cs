using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] PlayerStats ps;

    [SerializeField] InputAction shoot;
    // [SerializeField] InputAction kick;



    [SerializeField] Transform shootPoint;

    [SerializeField] GameObject bulletObj;

    [SerializeField] GameObject bombObj;

    [SerializeField] GameObject shurikenObj;

    [SerializeField] GameObject punchHitbox;


    [SerializeField] bool isPreparing = false;

    [SerializeField] float preparationCount = 0f;

    void Start()
    {
        shoot = InputSystem.actions.FindAction("Shoot");

        shoot.performed += ctx => Shoot();
        
        shoot.canceled += ctx => StopShoot();

    }

    private void StopShoot()
    {
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
        preparationCount = 0f;
        isPreparing = false;
        StartCoroutine(PunchCoroutine());
    }

    IEnumerator PunchCoroutine()
    {

        punchHitbox.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        punchHitbox.SetActive(false);

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



    }


    #endregion

    void ThrowShuriken()
     {
        
     }

    

     void ShootFuzil()
    {
         
    }


}
