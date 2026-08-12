using UnityEngine;

public enum ItemType
{
    Gun,
    Bomb,
    Shuriken,
    Punch,
    Empty
}

[CreateAssetMenu(fileName = "NovoItem",menuName ="items")]
public class ItemSO : ScriptableObject {

    public ItemType Type;

    public Sprite itemSprite;

    public string itemName;
    [TextArea(3, 6)]
    public string description;
}
