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
    [SerializeField] private Canvas canvas;
    private RectTransform iconRectTransform;
    private ItemInfo draggedItem;
    private Cell sourceCell;
    private bool dropSuccessful;

    public void StartDrag(ItemInfo item, Cell source)
    {
        draggedItem = item;
        sourceCell = source;
        dropSuccessful = false;

        if (iconRectTransform == null)
            iconRectTransform = icon.GetComponent<RectTransform>();

        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
    }

    public void Move(Vector2 screenPosition)
    {
        if (canvas == null || iconRectTransform == null)
            return;

        // Convert screen position to canvas position
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPosition,
            canvas.worldCamera,
            out localPoint
        );

        iconRectTransform.localPosition = localPoint;
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
