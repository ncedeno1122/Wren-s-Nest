using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectionController : MonoBehaviour
{
    private int m_SelectableObjectLayer;
    [SerializeField, Range(0.25f, 5f)]
    private float m_SelectionDistance = 1.5f;

    //[SerializeField]
    //private SelectableObjectController m_SelectedObject;
    //public SelectableObjectController SelectedObject { get => m_SelectedObject; }

    [SerializeField]
    private CamInteractableObjectController m_CamInteractableObject;
    public CamInteractableObjectController CamInteractableObject { get => m_CamInteractableObject; }

    private void Awake()
    {
        m_SelectableObjectLayer = LayerMask.GetMask("SelectableObject");
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitObject, m_SelectionDistance, m_SelectableObjectLayer, QueryTriggerInteraction.Collide))
        {
            // Update the SelectableObject reference if null
            if (m_CamInteractableObject == null)
            {
                m_CamInteractableObject = hitObject.collider.gameObject.GetComponent<CamInteractableObjectController>();
                m_CamInteractableObject.OnHoverEnter();
                //Debug.Log($"Didn't have a CamInteractableObject, selected {hitObject.collider.gameObject.name}!");
            }
            else
            {
                // If we DON'T know the new object, update it! 
                if (!hitObject.collider.gameObject.Equals(hitObject.collider.gameObject))
                {
                    // Exit hover on old one,
                    m_CamInteractableObject.OnHoverExit();

                    // Set and ENTER hover on the new one!
                    m_CamInteractableObject = hitObject.collider.gameObject.GetComponent<CamInteractableObjectController>();
                    m_CamInteractableObject.OnHoverEnter();
                }
            }
        }
        else
        {
            // Clear the CamInteractableObject reference, exiting hover.
            if (m_CamInteractableObject != null)
            {
                //Debug.Log($"Deselected {hitObject.collider.gameObject.name}...");
                m_CamInteractableObject.OnHoverExit();
                m_CamInteractableObject = null;
            }
        }
    }

    // + + + + | Functions | + + + + 

    /// <summary>
    /// Tries to provide the currently SelectedObject, returns whether or not one exists.
    /// </summary>
    /// <param name="selectedObject"></param>
    /// <returns></returns>
    //public bool TrySelectObject(out SelectableObjectController selectedObject)
    //{
    //    selectedObject = (m_CamInteractableObject is SelectableObjectController) ? m_CamInteractableObject as SelectableObjectController : null;
    //    return m_CamInteractableObject != null;
    //}

    public bool TryGetCamInteractableObject(out CamInteractableObjectController camInteractable)
    {
        camInteractable = m_CamInteractableObject;
        return m_CamInteractableObject != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(Vector3.forward * m_SelectionDistance));
    }
}
