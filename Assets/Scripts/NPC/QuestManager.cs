using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestPos
{
    public Transform questPosTransform;
    public bool isOccupied;
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] public bool isQuesting = false;

    [SerializeField]  GameObject Npc;

    [SerializeField] InventoryManager inventoryManager;

    [Header("Quest Positions")]
    [SerializeField] private List<QuestPos> questPositions = new List<QuestPos>();

    [SerializeField] public List<Transform> QuestTargets = new List<Transform>();

    [SerializeField] public List<Transform> BringTargets = new List<Transform>();


    private void Start()
    {
        
    }
    public void CreateNewQuests()
    {
        
        inventoryManager.SaveGame();
            
       RollForQuests();

        RollForQuests();



    }

    void RollForQuests()
    {
        List<QuestPos> availablePositions = new List<QuestPos>();

        foreach (QuestPos position in questPositions)
        {
            if (!position.isOccupied)
            {
                availablePositions.Add(position);
            }
        }

        if (availablePositions.Count == 0)
        {
            Debug.Log("No available quest positions!");
            return;
        }

        int roll = Random.Range(0, availablePositions.Count);

        QuestPos selectedPosition = availablePositions[roll];

        selectedPosition.isOccupied = true;

       GameObject npcObj = Instantiate(Npc,selectedPosition.questPosTransform.position,Quaternion.identity);

        npcObj.GetComponent<NpcScript>().npcID = roll;
        npcObj.GetComponent<NpcScript>().questManager = this;
    }


    public void QuestCompleted(int npcID)
    {
        if (npcID >= 0 && npcID < questPositions.Count)
        {
            questPositions[npcID].isOccupied = false;
            Debug.Log($"Quest at position {npcID} completed and marked as unoccupied.");
            CreateNewQuests();  
        }
        else
        {
            Debug.LogWarning($"Invalid NPC ID: {npcID}. Cannot mark quest as completed.");
        }
    }
}
 
