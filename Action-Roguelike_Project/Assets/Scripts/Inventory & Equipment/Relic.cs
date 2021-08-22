using UnityEngine;

public class Relic : MonoBehaviour
{
    public RelicData relicData;
    public StatModifier statMod;

    IActivateRelic relicActivation;

    private void Awake()
    {
        statMod = new StatModifier(relicData.modValue, StatModType.PercentMulti);
        relicActivation = GetComponent<IActivateRelic>();
    }
    public virtual void ActivateRelic(Transform _interactingEntity_)
    {
        relicActivation.OnActivateRelic(_interactingEntity_, this);
        
        Debug.Log("Relic Activated!");
        gameObject.SetActive(false);
    }

    public virtual void DeactivateRelic(Transform _interactingEntity_)
    {
        relicActivation.OnDeactivateRelic(_interactingEntity_, this);

        Debug.Log("Relic Deactivated!");
    }
}

