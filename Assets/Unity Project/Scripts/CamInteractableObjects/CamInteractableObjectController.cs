using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CamInteractableObjectController : MonoBehaviour
{
    public abstract void OnHoverEnter();
    public abstract void OnHoverExit();
    public abstract void OnInteract(); // On Primary Input / "Click"

}
