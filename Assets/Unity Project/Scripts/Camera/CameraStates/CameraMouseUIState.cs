using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseUIState : CameraState
{
    public CameraMouseUIState(ScriptableCameraController context) : base(context)
    {
    }

    public override void OnEnter()
    {
        m_Context.VisitCodePanel.Show();
        VisitCodePanelController.OnValidVisitCodeSubmitted.AddListener(HandleValidCodeSubmitted);

        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void OnExit()
    {
        m_Context.VisitCodePanel.Hide();
        VisitCodePanelController.OnValidVisitCodeSubmitted.RemoveListener(HandleValidCodeSubmitted);
    }

    protected override void PreUpdate()
    {

    }

    protected override void MidUpdate()
    {

    }

    protected override void PostUpdate()
    {

    }

    // + + + + | Functions | + + + +

    private void HandleValidCodeSubmitted(int value)
    {
        // Change back
        // TODO: SHOULD wait for some debounce time
        m_Context.ChangeState(new CameraPlayerState(m_Context));
    }
}
