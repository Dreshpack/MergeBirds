using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Bird bird;

    public ItemInfo currentItem;
    private ItemInfo tempDraggedItem; // Store temporarily during drag

    private void OnEnable()
    {
        if (IsFree())
        {
            bird.TurnOffImage();
        }
        else
        {
            bird.SetBird(currentItem.number);
        }
    }

    public bool IsFree()
    {
        return currentItem == null;
    }

    public void Clear()
    {
        currentItem = null;
        bird.TurnOffImage();
    }

    public void SetNewItem(ItemInfo newItem)
    {
        currentItem = newItem;
        bird.SetBird(newItem.number);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsFree())
            return;

        // Store item temporarily and clear the cell visually
        tempDraggedItem = currentItem;
        DragManager.Instance.StartDrag(currentItem, this);

        // Clear the cell
        currentItem = null;
        bird.TurnOffImage();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (tempDraggedItem == null)
            return;

        DragManager.Instance.Move(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (tempDraggedItem == null)
            return;

        // Check if drop was successful
        if (!DragManager.Instance.WasDropSuccessful())
        {
            // Drop failed or dropped on invalid target, restore the item
            currentItem = tempDraggedItem;
            bird.SetBird(currentItem.number);
        }

        // Clear temporary storage and end drag
        tempDraggedItem = null;
        DragManager.Instance.EndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Get the item being dragged
        ItemInfo droppedItem = DragManager.Instance.GetDraggedItem();

        if (droppedItem == null)
            return;

        // Check if this cell is free
        if (IsFree())
        {
            // Accept the drop
            SetNewItem(droppedItem);
            DragManager.Instance.MarkDropSuccessful();
        }
        // If cell is not free, do nothing (drop will fail and item returns to source)
    }
}
