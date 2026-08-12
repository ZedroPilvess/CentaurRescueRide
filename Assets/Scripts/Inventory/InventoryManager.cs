using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using TMPro;

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

    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textDesc;

    [Header("Drag & Drop")]
    [SerializeField] private List<InventorySwapLogic> inventorySlots = new List<InventorySwapLogic>();
    [SerializeField] private Canvas inventoryCanvas;

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
        InitializeInventory();
        SetupInventorySlots();

        LoadGame();

        UpdateUI();

        StartCoroutine(DelayThenSwitch());
    }


    private void SetupInventorySlots()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].Setup(
                i,
                this,
                inventoryCanvas
            );
        }
    }





    private void InitializeInventory()
    {
        while (itemsList.Count < inventorySize)
        {
            itemsList.Add(emptyItem);
        }

        while (itemsList.Count > inventorySize)
        {
            itemsList.RemoveAt(itemsList.Count - 1);
        }
    }



    IEnumerator DelayThenSwitch()
    {
        yield return new WaitForSeconds(0.1f);

        MoveRight();
        yield return new WaitForSeconds(0.1f);
        MoveLeft();


    }
    private void OnEnable()
    {
        inventoryLeft = InputSystem.actions.FindAction("InventoryLeft");
        inventoryRight = InputSystem.actions.FindAction("InventoryRight");
        inventoryDrop = InputSystem.actions.FindAction("DestroyItem");

        inventoryLeft.performed += OnInventoryLeft;
        inventoryRight.performed += OnInventoryRight;
        inventoryDrop.performed += OnInventoryDrop;

        inventoryLeft.Enable();
        inventoryRight.Enable();
        inventoryDrop.Enable();
    }

    private void OnDisable()
    {
        if (inventoryLeft != null)
            inventoryLeft.performed -= OnInventoryLeft;

        if (inventoryRight != null)
            inventoryRight.performed -= OnInventoryRight;

        if (inventoryDrop != null)
            inventoryDrop.performed -= OnInventoryDrop;

        if (inventoryLeft != null)
            inventoryLeft.Disable();

        if (inventoryRight != null)
            inventoryRight.Disable();

        if (inventoryDrop != null)
            inventoryDrop.Disable();
    }

    private void OnInventoryLeft(InputAction.CallbackContext ctx)
    {
        MoveLeft();
    }

    private void OnInventoryRight(InputAction.CallbackContext ctx)
    {
        MoveRight();
    }

    private void OnInventoryDrop(InputAction.CallbackContext ctx)
    {
        DropItem();
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
        ps.TakeDamage(-1);
         
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
        List<string> itemNames = new List<string>();

        foreach (ItemSO item in itemsList)
        {
            if (item == null)
                itemNames.Add(emptyItem.name);
            else
                itemNames.Add(item.name);
        }

        PlayerPrefs.SetString("Inventory",string.Join(",", itemNames));
        PlayerPrefs.SetInt("RescuedTargets", ps.rescuedTargets);

        PlayerPrefs.SetInt("PlayerHP", ps.hp);

        PlayerPrefs.SetInt("EquippedItem", currentItem);

        PlayerPrefs.SetFloat("PlayerX", ps.playerObj.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", ps.playerObj.transform.position.y);


        PlayerPrefs.Save();
        Debug.Log("Game saved successfully!");
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("Inventory"))
        {
            Debug.Log("No save data found. Starting new game.");
            return;
        }

        // Load inventory
        string savedInventory = PlayerPrefs.GetString("Inventory");

        string[] savedItems = savedInventory.Split(',');

        for (int i = 0; i < inventorySize; i++)
        {
            if (i < savedItems.Length)
            {
                ItemSO item = FindItemByName(savedItems[i]);

                if (item != null)
                {
                    itemsList[i] = item;
                }
                else
                {
                    Debug.LogWarning(
                        $"Could not find ItemSO named: {savedItems[i]}"
                    );

                    itemsList[i] = emptyItem;
                }
            }
            else
            {
                itemsList[i] = emptyItem;
            }
        }

        // Load player data
        if (ps != null)
        {
            ps.rescuedTargets =
                PlayerPrefs.GetInt("RescuedTargets", ps.rescuedTargets);

            ps.hp =
                PlayerPrefs.GetInt("PlayerHP", ps.hp);

            float playerX =
                PlayerPrefs.GetFloat("PlayerX", ps.playerObj.transform.position.x);

            float playerY =
                PlayerPrefs.GetFloat("PlayerY", ps.playerObj.transform.position.y);

            ps.playerObj.transform.position =
                new Vector3(playerX, playerY, ps.playerObj.transform.position.z);
        }

        // Load equipped item
        currentItem = PlayerPrefs.GetInt("EquippedItem", 0);

        Debug.Log("Game loaded successfully!");
    }

    private ItemSO FindItemByName(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
            return emptyItem;

        if (fuzilItem != null && fuzilItem.name == itemName)
            return fuzilItem;

        if (bombItem != null && bombItem.name == itemName)
            return bombItem;

        if (shurikenItem != null && shurikenItem.name == itemName)
            return shurikenItem;

        if (punchItem != null && punchItem.name == itemName)
            return punchItem;

        if (emptyItem != null && emptyItem.name == itemName)
            return emptyItem;

        return null;
    }

    void UpdateUI()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            imagesList[i].sprite = itemsList[i].itemSprite;    

            Debug.Log("Item sprite updated: " + itemsList[i].itemSprite.name); 
             

        }
        textDesc.text = itemsList[currentItem].description;
        textName.text = itemsList[currentItem].name;    

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
            if   (item == null || item.Type == ItemType.Empty)  
            {
                return false;
            }
        }   

        return true;
    }

    public bool IsSlotEmpty(int index)
    {
        if (index < 0 || index >= itemsList.Count)
            return true;

        if (itemsList[index] == null)
            return true;

        return itemsList[index].Type == ItemType.Empty;
    }

    public void SwapItems(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || fromIndex >= itemsList.Count)
            return;

        if (toIndex < 0 || toIndex >= itemsList.Count)
            return;

        ItemSO temp = itemsList[fromIndex];

        itemsList[fromIndex] = itemsList[toIndex];
        itemsList[toIndex] = temp;

        Debug.Log(
            $"Swapped inventory slots {fromIndex} and {toIndex}"
        );

        UpdateUI();
    }

}
