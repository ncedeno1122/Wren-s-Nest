using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class SelectableObjectController : CamInteractableObjectController
{
    private bool m_IsHoveredOver = false;
    private bool m_IsSelected = false;

    public SelectableObjectData ObjectData;

    // Rendering
    [SerializeField]
    private List<Renderer> m_Renderers;
    private List<Material> m_Materials = new();
    private Color m_HoveredColor = Color.yellow;

    private void Awake()
    {
        // Set Layer to allow for detection via collision!
        //if (gameObject.layer != LayerMask.GetMask("SelectableObject"))
        //{
        //    gameObject.layer = LayerMask.GetMask("SelectableObject");
        //}

        // Get Materials from Inspector-set Renderers.
        foreach (Renderer rend in m_Renderers)
        {
            m_Materials.AddRange(new List<Material>(rend.materials));
        }
    }

    // + + + + | Methods | + + + + 
    #region Methods

    /// <summary>
    /// Sets all inspector-set child materials to being emissive.
    /// </summary>
    /// <param name="on"></param>
    private void TriggerEmissiveMaterials(bool on)
    {
        if (on)
        {
            foreach (Material mat in m_Materials)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", m_HoveredColor);
            }
        }
        else
        {
            foreach (Material mat in m_Materials)
            {
                mat.DisableKeyword("_EMISSION");
            }
        }
    }

    public Vector3 GetCameraOffset()
    {
        if (ObjectData != null)
        {
            return transform.position + ObjectData.CameraPositionalOffset;
        }
        else return Vector3.zero;
    }

    public override void OnHoverEnter()
    {
        m_IsHoveredOver = true;
        TriggerEmissiveMaterials(true);
    }

    public override void OnHoverExit()
    {
        m_IsHoveredOver = false;
        TriggerEmissiveMaterials(false);
    }

    public override void OnInteract()
    {
        //OnSelected();
    }

    public void OnSelected()
    {
        m_IsSelected = true;
        OnHoverExit();
    }

    public void OnDeselected()
    {
        m_IsSelected = false;
    }


    // + + + + | Gizmos | + + + + 

    private void OnDrawGizmosSelected()
    {
        if (ObjectData == null) return;

        Gizmos.color = Color.red;
        Vector3 camOffset = GetCameraOffset();
        Gizmos.DrawLine(transform.position, camOffset);
        Gizmos.DrawSphere(camOffset, 0.125f);

        // Frustrum
        Camera mainCam = Camera.main;
        if (mainCam)
        {
            // Draw Frustrum
            Gizmos.color = Color.yellow;
            Gizmos.matrix = Matrix4x4.TRS
            (
                camOffset, 
                Quaternion.Euler(ObjectData.CameraRotationalOffset),
                mainCam.transform.localScale
                );
            Gizmos.DrawFrustum(Vector3.zero, mainCam.fieldOfView, mainCam.farClipPlane, mainCam.nearClipPlane, mainCam.aspect);
        }
    }


    #endregion Methods
}
