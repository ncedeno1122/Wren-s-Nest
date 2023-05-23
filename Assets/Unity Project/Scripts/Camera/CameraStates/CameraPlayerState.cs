using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerState : CameraState
{
    private Vector3 m_CurrentRotationEulers = Vector3.zero;

    public CameraPlayerState(ScriptableCameraController context) : base(context)
    {
        m_CurrentRotationEulers = m_Context.transform.localRotation.eulerAngles;
    }

    public override void OnEnter()
    {
        //Debug.Log("Entered CameraPlayerState!");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnExit()
    {
        //Debug.Log("Exiting CameraPlayerState!");
    }

    public override void HandleSelectInput()
    {
        // Switch state to SelectionState!
        m_Context.ChangeState(new CameraSelectionState(m_Context));
    }

    protected override void PreUpdate()
    {
        m_CurrentRotationEulers += m_LookInputVector * (m_Context.LookSpeed * Time.deltaTime);
        m_CurrentRotationEulers[0] = Mathf.Clamp(m_CurrentRotationEulers[0], m_Context.LowerLookLimit, m_Context.UpperLookLimit); // Clamp look angle
    }

    protected override void MidUpdate()
    {
        // Look
        m_Context.transform.localEulerAngles = m_CurrentRotationEulers;
    }

    protected override void PostUpdate()
    {
        // Move with Player
        m_Context.transform.position = Vector3.Lerp(m_Context.transform.position, m_Context.RegisteredPlayerController.transform.position, 0.35f);
    }
}
