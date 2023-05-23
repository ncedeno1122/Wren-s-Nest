using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SelectableObjectController : MonoBehaviour
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

    public void OnHoverEnter()
    {
        m_IsHoveredOver = true;
        TriggerEmissiveMaterials(true);
    }

    public void OnHoverExit()
    {
        m_IsHoveredOver = false;
        TriggerEmissiveMaterials(false);
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



    #endregion Methods
}
