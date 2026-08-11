using UnityEngine;
using UnityEngine.UI;

public enum QuestState
{
    waiting,    
    go,
    bring,
    
}

public class NpcScript : MonoBehaviour
{
    [SerializeField] public QuestManager questManager;
    [SerializeField] GameObject npcDialogueUI;
    [SerializeField] Button denyBtn;
    [SerializeField] Button acceptBtn;
    [SerializeField] GameObject questInProgressTxt;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] QuestState questState = QuestState.waiting;

    [SerializeField]public  int npcID; 
    [SerializeField] GameObject goTarget;
    [SerializeField] GameObject bringTarget;


    private void Start()
    {
        denyBtn.onClick.AddListener(DenyQuest);
        acceptBtn.onClick.AddListener(AcceptQuest);
         questManager = GameObject.FindWithTag("QuestManager").GetComponent<QuestManager>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(questManager != null && !questManager.isQuesting)
            {
                npcDialogueUI.SetActive(true);
                questInProgressTxt.SetActive(false);

            }
            else
            {
                questInProgressTxt.SetActive(true);
            }

        }
        

    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            npcDialogueUI.SetActive(false);
            questInProgressTxt.SetActive(false);
        }
    }

    void DenyQuest()
    {
        npcDialogueUI.SetActive(false);
    }

    void AcceptQuest()
    {
        npcDialogueUI.SetActive(false);
        goTarget.SetActive(true);   
        questState = QuestState.go; 
        if (questManager != null)
        {
            questManager.isQuesting = true;
        }

        playerStats.QuestTarget(goTarget.transform);    
    }

    public void QuestUpdate()
    {
        if (questState == QuestState.go)
        {
            bringTarget.SetActive(true);
            questState = QuestState.bring;

            playerStats.QuestTarget(bringTarget.transform);
        }
        else if (questState == QuestState.bring)
        {
            questState = QuestState.waiting;
            questManager.isQuesting = false;
            playerStats.StopQuest();

            questManager.isQuesting = false;

            questManager.QuestCompleted(npcID);

            Destroy(goTarget);
            Destroy(gameObject);
        }
    }


}
