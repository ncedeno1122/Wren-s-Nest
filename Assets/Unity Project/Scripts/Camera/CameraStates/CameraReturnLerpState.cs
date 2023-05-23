using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReturnLerpState : CameraState
{
    private SelectableObjectController m_SelectedObject;
    private Vector3 m_CurrentPosition = Vector3.zero;
    private Vector3 m_TargetPosition = Vector3.zero;
    private Quaternion m_CurrentRotation = Quaternion.identity;
    private Quaternion m_TargetRotation = Quaternion.identity;

    private float m_LerpTime;
    private bool m_IsCRTFinished = false;
    public IEnumerator LerpCRT;

    public CameraReturnLerpState(ScriptableCameraController context, SelectableObjectController selectedObject, float lerpTime = 1.5f) : base(context)
    {
        m_SelectedObject = selectedObject;

        m_CurrentPosition = m_Context.transform.position;
        m_TargetPosition = m_Context.RegisteredPlayerController.transform.position;

        m_CurrentRotation = m_Context.transform.rotation;
        m_TargetRotation = Quaternion.Euler(m_Context.RegisteredPlayerController.transform.forward);
        m_LerpTime = lerpTime;
    }

    public override void OnEnter()
    {
        //Debug.Log("Entering CameraReturnLerpState!");
        LerpCRT = LerpToFromCRT(
            m_CurrentPosition,
            m_TargetPosition,
            m_CurrentRotation,
            m_TargetRotation,
            m_LerpTime);
        m_Context.StartCoroutine(LerpCRT);
    }

    public override void OnExit()
    {
        //Debug.Log("Exiting CameraReturnLerpState!");
        m_Context.StopCoroutine(LerpCRT);
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

    // + + + + | Coroutine | + + + + 

    private IEnumerator LerpToFromCRT(Vector3 originalPos, Vector3 targetPos, Quaternion originalRot, Quaternion targetRot, float lerpTime)
    {
        m_IsCRTFinished = false;
        for (float t = 0f; t < lerpTime; t += Time.deltaTime)
        {
            // Lerp Position & Rotation
            m_Context.transform.SetPositionAndRotation(
                Vector3.Lerp(originalPos, targetPos, t / lerpTime),
                Quaternion.Lerp(originalRot, targetRot, t / lerpTime));

            yield return new WaitForEndOfFrame();
        }
        m_Context.transform.SetPositionAndRotation(targetPos, targetRot);

        m_IsCRTFinished = true;

        //Debug.Log("Done!");
        m_Context.ChangeState(new CameraPlayerState(m_Context));
    }
}