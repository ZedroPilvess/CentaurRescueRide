using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySwapLogic : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler
{
    [SerializeField] private int slotIndex;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Image itemImage;

    private Canvas canvas;
    private GameObject dragObject;
    private RectTransform dragRect;

    public void Setup(
        int index,
        InventoryManager manager,
        Canvas inventoryCanvas)
    {
        slotIndex = index;
        inventoryManager = manager;
        canvas = inventoryCanvas;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Don't allow dragging empty slots
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not assigned!");
            return;
        }

        if (inventoryManager.IsSlotEmpty(slotIndex))
        {
            return;
        }

        if (canvas == null)
        {
            Debug.LogError("Inventory Canvas is not assigned!");
            return;
        }

        if (itemImage == null)
        {
            Debug.LogError("Item Image is not assigned!");
            return;
        }

        // Create the visual object that follows the mouse
        dragObject = new GameObject("DraggingItem");

        dragObject.transform.SetParent(canvas.transform, false);

        // Get RectTransform immediately
        dragRect = dragObject.AddComponent<RectTransform>();

        Image dragImage = dragObject.AddComponent<Image>();

        dragImage.sprite = itemImage.sprite;
        dragImage.preserveAspect = true;

        dragRect.sizeDelta = itemImage.rectTransform.sizeDelta;

        // Prevent the dragged image from blocking raycasts
        CanvasGroup canvasGroup =
            dragObject.AddComponent<CanvasGroup>();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.8f;

        // Hide original icon while dragging
        itemImage.enabled = false;

        UpdateDragPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragObject == null)
            return;

        if (dragRect == null)
            return;

        UpdateDragPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragObject != null)
        {
            Destroy(dragObject);
        }

        dragObject = null;
        dragRect = null;

        if (itemImage != null)
        {
            itemImage.enabled = true;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (inventoryManager == null)
            return;

        InventorySwapLogic draggedSlot =
            eventData.pointerDrag?.GetComponent<InventorySwapLogic>();

        if (draggedSlot == null)
            return;

        if (draggedSlot == this)
            return;

        inventoryManager.SwapItems(
            draggedSlot.slotIndex,
            slotIndex
        );
    }

    private void UpdateDragPosition(PointerEventData eventData)
    {
        if (dragRect == null)
            return;

        RectTransform canvasRect =
            canvas.transform as RectTransform;

        if (canvasRect == null)
            return;

        Vector2 localPosition;

        Camera eventCamera = eventData.pressEventCamera;

        bool successfullyConverted =
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                eventData.position,
                eventCamera,
                out localPosition
            );

        if (!successfullyConverted)
            return;

        dragRect.localPosition = localPosition;
    }
}