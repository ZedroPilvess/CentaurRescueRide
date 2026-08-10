using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int HP;
    [SerializeField] int MaxHP;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);  
        }
    }
}
