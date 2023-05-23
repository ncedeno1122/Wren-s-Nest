using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInfoUIState : CameraState
{
    private SelectableObjectController m_SelectedObject;

    public CameraInfoUIState(ScriptableCameraController context, SelectableObjectController selectedObject) : base(context)
    {
        m_SelectedObject = selectedObject;
    }

    public override void OnEnter()
    {
        //Debug.Log("Entering CameraInfoUIState!");
        // Button Listeners
        m_Context.ObjectInfoUI.LinkButton.onClick.AddListener(OnLinkButtonPressed);
        m_Context.ObjectInfoUI.BackButton.onClick.AddListener(OnBackButtonPressed);

        m_Context.ObjectInfoUI.PopulateDataForObject(m_SelectedObject);
        m_Context.ObjectInfoUI.Show();

        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void OnExit()
    {
        //Debug.Log("Exiting CameraInfoUIState!");

        // Button Listeners
        m_Context.ObjectInfoUI.LinkButton.onClick.RemoveListener(OnLinkButtonPressed);
        m_Context.ObjectInfoUI.BackButton.onClick.RemoveListener(OnBackButtonPressed);

        m_Context.ObjectInfoUI.Hide();
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

    public void OnLinkButtonPressed()
    {
        Application.OpenURL(m_SelectedObject.ObjectData.ResourceURL);
    }

    public void OnBackButtonPressed()
    {
        // Leave State
        m_Context.ChangeState(new CameraReturnLerpState(m_Context, m_SelectedObject));
    }
}
