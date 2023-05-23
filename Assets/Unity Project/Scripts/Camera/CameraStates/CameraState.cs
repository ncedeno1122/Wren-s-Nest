using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraState
{
    protected ScriptableCameraController m_Context;
    protected Vector3 m_LookInputVector = Vector3.zero;

    public CameraState(ScriptableCameraController context)
    {
        m_Context = context;
    }

    public abstract void OnEnter();
    public abstract void OnExit();

    public virtual void HandleLookInput(Vector3 lookInput)
    {
        m_LookInputVector = lookInput;
    }

    public virtual void HandleSelectInput()
    {

    }

    public virtual void OnUpdate()
    {
        PreUpdate();
        MidUpdate();
        PostUpdate();
    }

    protected abstract void PreUpdate();
    protected abstract void MidUpdate();
    protected abstract void PostUpdate();
}
