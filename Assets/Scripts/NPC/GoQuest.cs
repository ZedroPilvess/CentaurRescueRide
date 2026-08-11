using UnityEngine;

public class GoQuest : MonoBehaviour
{
    [SerializeField] NpcScript npcScript;
    private void Start()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npcScript.QuestUpdate();
            Transform piggyback = collision.GetComponent<PlayerStats>().piggyBackObj.transform;

            if (piggyback != null)
            {
                transform.SetParent(piggyback);
                transform.localPosition = Vector3.zero;
            }


        }
    }
}
