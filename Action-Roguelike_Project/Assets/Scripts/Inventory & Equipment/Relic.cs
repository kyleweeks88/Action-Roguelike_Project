using UnityEngine;

public class Relic : MonoBehaviour
{
    public RelicData relicData;
    [HideInInspector] public StatModifier statMod;

    IActivateRelic relicActivation;

    private void Awake()
    {
        // Creates a new instance of the StatModifier so it's not a duplicate
        StatModifier statInstance = new StatModifier(relicData.stadModifier.value, relicData.stadModifier.type);
        relicData.stadModifier = statInstance;
        statMod = relicData.stadModifier;
        relicActivation = GetComponent<IActivateRelic>();
    }

    public virtual void ActivateRelic(Transform _interactingEntity_)
    {
        relicActivation.OnActivateRelic(_interactingEntity_, this);
        
        gameObject.SetActive(false);
    }

    public virtual void DeactivateRelic(Transform _interactingEntity_)
    {
        relicActivation.OnDeactivateRelic(_interactingEntity_, this);

        Debug.Log("Relic Deactivated!");
    }
}

