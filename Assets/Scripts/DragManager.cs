using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragManager : Singleton<DragManager>, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    protected override void Awake()
    {
        base.Awake(); // Always call this!
        // Your initialization code
    }

    [SerializeField] private Image icon;
    private ItemInfo draggedItem;
    private Cell sourceCell;
    private bool dropSuccessful;

    public void StartDrag(ItemInfo item, Cell source)
    {
        draggedItem = item;
        sourceCell = source;
        dropSuccessful = false;

        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
    }

    public void Move(Vector2 pos)
    {
        icon.transform.position = pos;
    }

    public void MarkDropSuccessful()
    {
        dropSuccessful = true;
    }

    public bool WasDropSuccessful()
    {
        return dropSuccessful;
    }

    public ItemInfo GetDraggedItem()
    {
        return draggedItem;
    }

    public void EndDrag()
    {
        draggedItem = null;
        sourceCell = null;
        dropSuccessful = false;
        icon.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
       // transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {


    }
}
