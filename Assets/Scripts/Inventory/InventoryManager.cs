using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private List<ItemSO> itemsList = new List<ItemSO>();
    [SerializeField] private int inventorySize = 4;
    [SerializeField] private int currentItem = 0;



    [Header("Input")]
    [SerializeField] private InputAction inventoryLeft;
    [SerializeField] private InputAction inventoryRight;
    [SerializeField] private InputAction inventoryDrop;

    [Header("UI")]
    [SerializeField] private List<Image> imagesList = new List<Image>();
    [SerializeField] private RectTransform selectedUI;
    [SerializeField] private List<RectTransform> itemUIPosition = new List<RectTransform>();

    [Header("Player")]
    [SerializeField] private PlayerStats ps;
    [SerializeField] private PlayerHeadAnimation headAnimation;

    [Header("Items")]
    [SerializeField] private ItemSO fuzilItem;
    [SerializeField] private ItemSO bombItem;
    [SerializeField] private ItemSO shurikenItem;
    [SerializeField] private ItemSO punchItem;
    [SerializeField] private ItemSO emptyItem;

    //[Header("Testing")]
    //[SerializeField] private bool invTest = true;


    private void Start()
    {
        inventoryLeft = InputSystem.actions.FindAction("InventoryLeft");
        inventoryRight = InputSystem.actions.FindAction("InventoryRight");
        inventoryDrop = InputSystem.actions.FindAction("DestroyItem");
        
      //  UpdateUI();
         
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
        //if (invTest == false)
        //{
        //    RemoveItem(currentItem);  
        //}

        
    }

    public void RemoveItem(int index)
    {

        imagesList[index].sprite = emptyItem.itemSprite;
        itemsList[index] = emptyItem;
        UpdateItem();       


    }

    public void SaveGame()
    {
        PlayerPrefs.SetString("Inventory", string.Join(",", itemsList.ConvertAll(item => item.name)));
        PlayerPrefs.SetInt("RescuedTargets", ps.rescuedTargets);

        PlayerPrefs.SetInt("PlayerHP", ps.hp);

        PlayerPrefs.SetString("EquippedItem", itemsList[currentItem].name);

        PlayerPrefs.SetFloat("PlayerX", ps.playerObj.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", ps.playerObj.transform.position.y);


        PlayerPrefs.Save();
        Debug.Log("Game saved successfully!");
    }

    void UpdateUI()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            imagesList[i].sprite = itemsList[i].itemSprite;    

            Debug.Log("Item sprite updated: " + itemsList[i].itemSprite.name); 
             

        }


      UpdateItem(); 

        selectedUI.position = itemUIPosition[currentItem].position + new Vector3(-3.5f, 4, 0); 
    }

    public void UpdateItem()
    {
        ps.equipedItem = itemsList[currentItem];
        headAnimation.changeWeaponAnimState();
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
