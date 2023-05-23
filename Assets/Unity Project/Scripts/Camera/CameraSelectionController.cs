using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectionController : MonoBehaviour
{
    private int m_SelectableObjectLayer;
    [SerializeField, Range(0.25f, 5f)]
    private float m_SelectionDistance = 1.5f;

    [SerializeField]
    private SelectableObjectController m_SelectedObject;
    public SelectableObjectController SelectedObject { get => m_SelectedObject; }

    private void Awake()
    {
        m_SelectableObjectLayer = LayerMask.GetMask("SelectableObject");
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitObject, m_SelectionDistance, m_SelectableObjectLayer, QueryTriggerInteraction.Collide))
        {
            // Update the SelectableObject reference if null
            if (m_SelectedObject == null)
            {
                m_SelectedObject = hitObject.collider.gameObject.GetComponent<SelectableObjectController>();
                m_SelectedObject.OnHoverEnter();
                Debug.Log($"Didn't have a selected object, selected {m_SelectedObject.gameObject.name}!");
            }
            else
            {
                // If we DON'T know the new object, update it! 
                if (!m_SelectedObject.gameObject.Equals(hitObject.collider.gameObject))
                {
                    // Exit hover on old one,
                    m_SelectedObject.OnHoverExit();

                    // Set and ENTER hover on the new one!
                    m_SelectedObject = hitObject.collider.gameObject.GetComponent<SelectableObjectController>();
                    m_SelectedObject.OnHoverEnter();
                }
            }
        }
        else
        {
            // Clear the SelectableObject reference, exiting hover.
            if (m_SelectedObject != null)
            {
                Debug.Log($"Deselected {m_SelectedObject.gameObject.name}...");
                m_SelectedObject.OnHoverExit();
                m_SelectedObject = null;
            }
        }
    }

    // + + + + | Functions | + + + + 

    /// <summary>
    /// Tries to provide the currently SelectedObject, returns whether or not one exists.
    /// </summary>
    /// <param name="selectedObject"></param>
    /// <returns></returns>
    public bool TrySelectObject(out SelectableObjectController selectedObject)
    {
        selectedObject = m_SelectedObject;
        return m_SelectedObject != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(Vector3.forward * m_SelectionDistance));
    }
}
