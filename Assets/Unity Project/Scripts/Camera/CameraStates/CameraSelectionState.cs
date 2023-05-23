using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectionState : CameraState
{

    public CameraSelectionState(ScriptableCameraController context) : base(context)
    {
    }

    public override void OnEnter()
    {
        //Debug.Log("Entered CameraSelectionState!");

        if (m_Context.CameraSelector.TrySelectObject(out SelectableObjectController selectedObject))
        {
            Debug.Log($"WE GOT ONE IT\'S {selectedObject.gameObject.name}!!");

            // Switch state to LerpToObjectState!
            m_Context.ChangeState(new CameraLerpToObjectState(m_Context, m_Context.CameraSelector.SelectedObject));
        }
        else
        {
            // Switch state to PlayerState!
            m_Context.ChangeState(new CameraPlayerState(m_Context));
        }
    }

    public override void OnExit()
    {
        //Debug.Log("Exiting CameraSelectionState!");
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
}
