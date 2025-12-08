using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
   // [SerializeField] private Image icon;
    [SerializeField] private Bird bird;
    
    public ItemInfo currentItem;

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
       // icon.gameObject.SetActive(false);
    }

    public void SetNewItem(ItemInfo newItem)
    {
        if (IsFree())
        {
            currentItem = newItem;
            bird.SetBird(newItem.number);
            //icon.gameObject.SetActive(true);
           // icon.sprite = currentItem.icon;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsFree())
            return;
        
        DragManager.Instance.FillInfo(currentItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
       DragManager.Instance.Move(eventData.position);
       Debug.Log("Dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragManager.Instance.Off();
        Debug.Log("End");
       // throw new System.NotImplementedException();
       
    }

    public void OnDrop(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
    }
}
