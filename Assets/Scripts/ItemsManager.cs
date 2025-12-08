using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : Singleton<ItemsManager>
{
    [SerializeField] private List<ItemInfo> allItems;
    protected override void Awake()
    {
        base.Awake();
    }

    public ItemInfo MergeItems(ItemInfo mergedItem)
    {
        return allItems[mergedItem.number + 1];
    }
}
