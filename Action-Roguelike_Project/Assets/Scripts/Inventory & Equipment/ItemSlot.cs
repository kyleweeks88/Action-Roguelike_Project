using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;

    public event Action<ItemData> OnRightClickEvent; 

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null && OnRightClickEvent != null)
                OnRightClickEvent(item);
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
