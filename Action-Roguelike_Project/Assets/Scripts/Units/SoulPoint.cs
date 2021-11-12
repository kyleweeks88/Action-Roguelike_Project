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

    void OnTriggerStay(Collider col)
    {
        if (!isPickupable) { return; }

        PlayerEventChannel playerEvents = col.gameObject.GetComponent<PlayerEventChannel>();
        if (playerEvents != null)
        {
            playerEvents.SoulGathered(this.currencyItem);
            GameObject.Destroy(this.gameObject);
        }
    }

    private IEnumerator Countdown()
    {
        float duration = 1f; // x seconds you can change this 
                             // to whatever you want
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        isPickupable = true;
    }
}
