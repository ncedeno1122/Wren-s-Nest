using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnVisitCodeListenerEvent : MonoBehaviour
{
    public int VisitCodeResponseEventArg;
    public UnityEvent OnThisValidCode;

    private void OnEnable()
    {
        VisitCodePanelController.OnValidVisitCodeSubmitted.AddListener(OnValidVisitCode);
    }
    private void OnDisable()
    {
        VisitCodePanelController.OnValidVisitCodeSubmitted.RemoveListener(OnValidVisitCode);
    }

    public void OnValidVisitCode(int code)
    {
        if (VisitCodeResponseEventArg == code)
        {
            OnThisValidCode?.Invoke();
        }
    }
}
