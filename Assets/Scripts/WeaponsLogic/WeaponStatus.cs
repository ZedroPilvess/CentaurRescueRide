using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    [SerializeField] int damage;

    [SerializeField] bool bomb = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bomb)
        {
            

            //Implement damage later
            // blind guardian is a good band



        }


    }
}
