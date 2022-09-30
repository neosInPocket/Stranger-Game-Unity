using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/New InventoryItemInfo")]
public class InventoryItemInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _spriteIcon;
    [SerializeField] private InventoryItemType _type;

    public string id => _id;
    public string title => _title;
    public string description => _description;
    public Sprite spriteIcon => _spriteIcon;
    public InventoryItemType type => _type;
}
