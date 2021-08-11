

public class EquipmentSlot : ItemSlot
{
    public ItemType itemType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = itemType.ToString() + " Slot";
    }
}
