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
        int nextBirdIndex = mergedItem.number + 1;

        // Check if next bird exists in the list
        if (nextBirdIndex < allItems.Count && allItems[nextBirdIndex] != null)
        {
            return allItems[nextBirdIndex];
        }

        // Return null if there's no next bird (max level reached)
        Debug.LogWarning($"Cannot merge bird at level {mergedItem.number} - max level reached or next bird not configured.");
        return null;
    }
}
