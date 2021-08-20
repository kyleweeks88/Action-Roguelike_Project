

public class EquipmentSlot : ItemSlot
{
    public EquipmentType itemType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = itemType.ToString() + " Slot";
    }
}
