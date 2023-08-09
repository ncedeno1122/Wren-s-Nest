using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectableResourceUIController : MonoBehaviour
{
    private CanvasGroup m_CanvasGroup;
    [SerializeField] private RectTransform m_ContentTF;
    
    public GameObject SelectableResourceUIPrefab;

    private void Awake()
    {
        m_ContentTF = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Initialize();
        OnDeactivate();
    }

    // + + + + | Functions | + + + + 

    public void OnActivate()
    {
        m_CanvasGroup.alpha = 1f;
        m_CanvasGroup.interactable = true;
    }

    public void OnDeactivate()
    {
        m_CanvasGroup.alpha = 0f;
        m_CanvasGroup.interactable = false;
    }

    private void Initialize()
    {
        // Get all ScriptableObjectData in the scene, make and fill out prefabs for them.
        SelectableObjectController[] sObjectControllers = FindObjectsOfType<SelectableObjectController>();
        SelectableObjectData[] sObjectData = new SelectableObjectData[sObjectControllers.Length];
        
        // Assign SelectableObjectData & Create ResourceUI for them.
        for (int i = 0; i < sObjectControllers.Length; i++)
        {
            sObjectData[i] = sObjectControllers[i].ObjectData;

            GameObject newSRCUIPrefab = Instantiate(SelectableResourceUIPrefab, m_ContentTF) as GameObject;
            InitializeSelectableResourceUIPrefab(newSRCUIPrefab, sObjectData[i]);
        }
    }

    private void InitializeSelectableResourceUIPrefab(GameObject newSRCUIPrefab, SelectableObjectData soData)
    {
        // Set Name Text
        TMP_Text nameText = newSRCUIPrefab.GetComponentInChildren<TMP_Text>();
        if (nameText)
        {
            nameText.text = soData.ObjectName;
        }
    }
}
