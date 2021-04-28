using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;

    // THIS IS SOME UNITY MAGIC THAT MAKES THE CLASS REALLY EASY TO USE.
    // JUST CHANGE THE ItemData item AND IT AUTOMAITCALLY UPDATES THE IMAGE.
    ItemData _item;
    public ItemData item 
    {
        get { return _item; }
        set 
        {
            _item = value;

            if(_item == null)
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


    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
