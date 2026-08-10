using UnityEngine;

public enum ItemType
{
    Gun,
    Bomb,
    Shuriken,
    Punch



}

[CreateAssetMenu(fileName = "NovoItem",menuName ="items")]
public class ItemSO : ScriptableObject {

    public ItemType Type;

    public Sprite itemSprite;
}
