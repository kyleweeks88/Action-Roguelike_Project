using System.Collections;
using UnityEngine;

public class SoulPoint : MonoBehaviour
{
    [SerializeField] CurrencyItem currencyItem;
    bool isPickupable;

    private void Awake()
    {
        StartCoroutine(Countdown());
    }

    void OnTriggerEnter(Collider col)
    {
        if (!isPickupable) { return; }

        // Check if the colliding object has an EquipmentManager component
        PlayerEventChannel playerEvents = col.gameObject.GetComponent<PlayerEventChannel>();
        if (playerEvents != null)
        {
            playerEvents.SoulGathered(this.currencyItem);
            GameObject.Destroy(this.gameObject);
        }
    }

    private IEnumerator Countdown()
    {
        float duration = 2f; // 3 seconds you can change this 
                             //to whatever you want
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            //countdownImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        isPickupable = true;
    }
}
