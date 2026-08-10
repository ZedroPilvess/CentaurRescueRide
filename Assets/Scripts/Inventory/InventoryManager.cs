using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<ItemSO> itemsList = new List<ItemSO>();


    [SerializeField] List<Image> imagesList = new List<Image>();    
    public void AddItem(ItemSO item)
    {
         itemsList.Add(item);


        UpdateUI();
    }


    public void RemoveItem(int index)
    {


    }

    void UpdateUI()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            imagesList[i].sprite = itemsList[i].itemSprite;    

        }
    }
    
}
