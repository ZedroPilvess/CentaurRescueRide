using UnityEngine;
using UnityEngine.UI;

public class NpcScript : MonoBehaviour
{
    [SerializeField] GameObject npcDialogueUI;
    [SerializeField] Button denyBtn;
    [SerializeField] Button acceptBtn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            npcDialogueUI.SetActive(true);
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            npcDialogueUI.SetActive(false);
        }
    }
}
