using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<ItemSO> itemsList = new List<ItemSO>();


    [SerializeField] List<Image> imagesList = new List<Image>();

    [SerializeField] InputAction inventoryLeft;
    [SerializeField] InputAction inventoryRight;
    [SerializeField] InputAction inventoryDrop;

    [SerializeField]  int inventorySize = 4;

    [SerializeField] int currentItem = 0;

    [SerializeField] ItemSO emptyItem;

    [SerializeField] bool invTest = true;


    [SerializeField] RectTransform selectedUI;

    [SerializeField] List<RectTransform> itemUIPosition = new List<RectTransform>();        


    private void Start()
    {
        inventoryLeft = InputSystem.actions.FindAction("InventoryLeft");
        inventoryRight = InputSystem.actions.FindAction("InventoryRight");
        inventoryDrop = InputSystem.actions.FindAction("DestroyItem");
        
        UpdateUI();

        inventoryLeft.performed += ctx => MoveLeft();
        inventoryRight.performed += ctx => MoveRight();
        inventoryDrop.performed += ctx => DropItem();
    }


    void MoveLeft()
    {
        currentItem--;
        if (currentItem < 0)
        {
            currentItem = inventorySize - 1;
        }
        Debug.Log("Current item index: " + currentItem);

        UpdateUI();
    }   

    void MoveRight()
    {
        currentItem++;
        if (currentItem >= inventorySize)
        {
            currentItem = 0;
        }
        Debug.Log("Current item index: " + currentItem);

        UpdateUI();
    }

    void DropItem()
    {
               RemoveItem(currentItem);
        Debug.Log("Item dropped from index: " + currentItem);

    
    }

    public void AddItem(ItemSO item)
    {
       for (int i = 0; i < inventorySize; i++)
       {
            if (itemsList[i] == null || itemsList[i].Type == ItemType.Empty)    
            {
               itemsList[i] = item; 
                Debug.Log("Item added: " + item.name);  
                break;
            }  
       }   

        UpdateUI();
    }





    private void Update()
    {
        if (invTest == false)
        {
            RemoveItem(currentItem);  
        }
  
    }

    public void RemoveItem(int index)
    {

        imagesList[index].sprite = emptyItem.itemSprite;
        itemsList[index] = emptyItem;



    }

    void UpdateUI()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            imagesList[i].sprite = itemsList[i].itemSprite;    

            Debug.Log("Item sprite updated: " + itemsList[i].itemSprite.name); 
             

        }




        selectedUI.position = itemUIPosition[currentItem].position + new Vector3(-3.5f, 4, 0); 
    }


    public bool IsIventoryFull()
    {
        foreach (var item in itemsList)
        {
            if (item.Type == ItemType.Empty || item == null )
            {
                return false;
            }
        }   

        return true;
    }
    
}
