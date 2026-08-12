using UnityEngine;

public class PlayerHeadAnimation : MonoBehaviour
{
    // 0 - empty
    // 1 - bomb
    // 2 - gun
    // 3 - punch
    // 4 - shuriken

    [SerializeField] Animator animator;

    [SerializeField] PlayerStats stats;









    public void changeWeaponAnimState()
    {

        switch (stats.equipedItem.Type)
        {
            case ItemType.Gun:
                animator.SetInteger("Equip", 2);
                break;
            case ItemType.Bomb:
                animator.SetInteger("Equip", 1);
                break;
            case ItemType.Shuriken:
                animator.SetInteger("Equip", 4);
                break;
            case ItemType.Punch:
                animator.SetInteger("Equip", 3);
                break;
            case ItemType.Empty:
                animator.SetInteger("Equip", 0);
                break;




        }

  
    }

    public void setClick()
    {
        animator.SetTrigger("Click");
    }
    

    public void setRelease()
    {
        animator.SetTrigger("Release");
    }
}
