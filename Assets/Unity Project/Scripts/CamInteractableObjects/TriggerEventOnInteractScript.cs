using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventOnInteractScript : CamInteractableObjectController
{
    public UnityEvent OnInteractEvent;

    public override void OnHoverEnter()
    {
        //
    }

    public override void OnHoverExit()
    {
        //
    }

    public override void OnInteract()
    {
        OnInteractEvent?.Invoke();
    }
}
