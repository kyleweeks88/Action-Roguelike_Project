using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;

    ItemData _item;
    public ItemData item 
    {
        get { return _item; }
        set 
        {
            _item = value;

            if(_item = value)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item.icon;
                image.enabled = true;
            }
        }
    }


    void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
