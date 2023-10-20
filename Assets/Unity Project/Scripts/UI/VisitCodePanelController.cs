using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class VisitCodePanelController : MonoBehaviour
{
    private CanvasGroup m_CanvasGroup; // TODO: Just make an interface out of this...
    public TMP_InputField m_InputField;
    public TextMeshProUGUI SubmitResultText;

    public UnityEvent<int> OnValidVisitCodeSubmitted = new();

    // TODO: Move this Dictionary somewhere else!
    public Dictionary<string, int> VisitCodeDictionary = new Dictionary<string, int>()
    {
        { "1909", 101 }
    };

    private void Awake()
    {
        m_InputField = GetComponentInChildren<TMP_InputField>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        m_InputField.onValueChanged.AddListener(HandleOnValueChange);
        m_InputField.onEndEdit.AddListener(HandleEndEdit);
    }

    private void OnDisable()
    {
        m_InputField.onValueChanged.RemoveListener(HandleOnValueChange);
        m_InputField.onEndEdit.RemoveListener(HandleEndEdit);
    }

    void Start()
    {
        Hide();
    }

    // + + + + | Functions | + + + +

    private void ClearInputField()
    {
        m_InputField.text = "";
    }

    public void SubmitInput()
    {
        // Handle Invalid Lengths
        if (m_InputField.text.Length < 4)
        {
            SetSubmitResultText("Need a 4-Digit Code!");
        }

        // Handle Valid Code
        if (m_InputField.text.Length == 4)
        {
            // Lookup Code to be Recognized
            if (VisitCodeDictionary.ContainsKey(m_InputField.text))
            {
                int eventKey = VisitCodeDictionary[m_InputField.text];

                OnValidVisitCodeSubmitted?.Invoke(eventKey);
                SetSubmitResultText("Visit Code recognized!");
                Debug.Log($"Recognized Visit Code {m_InputField.text}; sending event with arg {eventKey}!");
            }
        }
    }

    private void SetSubmitResultText(string text)
    {
        SubmitResultText.text = text;
    }

    private void ClearSubmitResultText()
    {
        SubmitResultText.text = "";
    }

    public void AppendInteger(int value)
    {
        if (value >= 0 && value <= 9)
        {
            m_InputField.text += value.ToString();
        }
    }

    public void Show()
    {
        m_CanvasGroup.alpha = 1f;
        m_CanvasGroup.interactable = true;
        m_InputField.ActivateInputField();
        ClearSubmitResultText();
    }

    public void Hide()
    {
        m_CanvasGroup.alpha = 0f;
        m_CanvasGroup.interactable = false;
        m_InputField.DeactivateInputField();
        ClearSubmitResultText();
    }

    // + + + + | InputField | + + + +

    private void HandleOnValueChange(string value)
    {
        //Debug.Log(value);
        int length = value.Length;
        // Handle non-digit letters
        if (length > 0 && !char.IsDigit(value, length - 1))
        {
            value = value.Substring(0, length - 1);
        }

        // Clear if need to type more than 4 inputs
        if (length > 4)
        {
            ClearInputField();
        }
    }

    private void HandleEndEdit(string value)
    {
        //
    }
}
