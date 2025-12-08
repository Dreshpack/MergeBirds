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
    private ItemInfo info;


    public void FillInfo(ItemInfo NewInfo)
    {
        info = NewInfo;
        icon.gameObject.SetActive(true);
        icon.sprite = info.icon;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void Off()
    {
        info = null;
        icon.gameObject.SetActive(false);
    }

    public void Move(Vector2 pos)
    {
        
        icon.transform.position = pos;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
       // transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        
    }
}
