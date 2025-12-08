using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Scriptable Objects/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public Sprite icon;
    public int income;
    public int number;
    public AudioClip soundOnSpawn;
}
