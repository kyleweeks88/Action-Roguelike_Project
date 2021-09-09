using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivateRelic
{
    void OnActivateRelic(Transform interactingEntity, Relic relic);

    void OnDeactivateRelic(Transform _interactingEntity_, Relic _relic_);
}
