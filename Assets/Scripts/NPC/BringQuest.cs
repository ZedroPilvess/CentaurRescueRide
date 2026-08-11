using UnityEngine;

public class BringQuest : MonoBehaviour
{
    [SerializeField] NpcScript npcScript;
    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npcScript.QuestUpdate();
        }
    }
}
