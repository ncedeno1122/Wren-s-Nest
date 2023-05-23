using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectInfoUIController : MonoBehaviour
{
    private IEnumerator FadeCRT;

    private CanvasGroup m_CanvasGroup;
    
    private Button m_LinkButton, m_BackButton;
    public Button LinkButton { get => m_LinkButton; }
    public Button BackButton { get => m_BackButton; }
    [SerializeField]
    private TextMeshProUGUI m_TitleText, m_ContentText;

    private void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_BackButton = transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
        m_LinkButton = transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Button>();
        m_TitleText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        m_ContentText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // + + + + | Functions | + + + + 

    public void Show()
    {
        if (FadeCRT != null)
        {
            StopCoroutine(FadeCRT);
        }
        FadeCRT = FadeToAlphaValue(0f, 1f);
        StartCoroutine(FadeCRT);

        LinkButton.interactable = true;
        BackButton.interactable = true;
    }

    public void Hide()
    {
        if (FadeCRT != null)
        {
            StopCoroutine(FadeCRT);
        }
        FadeCRT = FadeToAlphaValue(1f, 0f);
        StartCoroutine(FadeCRT);

        LinkButton.interactable = false;
        BackButton.interactable = false;
    }

    private IEnumerator FadeToAlphaValue(float originalValue, float targetValue, float time = 1f)
    {
        for (float t = 0f; t <= time; t += Time.deltaTime)
        {
            m_CanvasGroup.alpha = Mathf.Lerp(originalValue, targetValue, t / time);
            yield return new WaitForEndOfFrame();
        }
        m_CanvasGroup.alpha = targetValue;
    }

    public void PopulateDataForObject(SelectableObjectController selectedObject)
    {
        m_TitleText.text = selectedObject.ObjectData.ObjectName;
        m_ContentText.text = selectedObject.ObjectData.ObjectDescription;
    }
}
