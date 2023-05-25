using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptableCameraController : MonoBehaviour
{
    public float LookSpeed = 5f;
    public float UpperLookLimit = 90f;
    public float LowerLookLimit = -90f;

    [SerializeField]
    private bool m_IsPlayerControllable { get => m_CurrentCameraState is CameraPlayerState; }
    public bool IsPlayerControllable { get => m_IsPlayerControllable; }

    private CameraState m_CurrentCameraState;
    public CameraState CurrentCameraState { get => m_CurrentCameraState; }

    private PlayerController m_RegisteredPlayerController;
    public PlayerController RegisteredPlayerController { get => m_RegisteredPlayerController; }

    private CameraSelectionController m_CameraSelector;
    public CameraSelectionController CameraSelector { get => m_CameraSelector; }

    [SerializeField]
    private ObjectInfoUIController m_ObjectInfoUI; // TODO: Set via UI Service Locater?
    public ObjectInfoUIController ObjectInfoUI { get => m_ObjectInfoUI; }

    private void Awake()
    {
        m_CameraSelector = GetComponent<CameraSelectionController>();
    }

    private void Update()
    {
        m_CurrentCameraState.OnUpdate();
    }

    // + + + + | Functions | + + + + 

    public void ChangeState(CameraState newState)
    {
        m_CurrentCameraState?.OnExit();
        m_CurrentCameraState = newState;
        m_CurrentCameraState.OnEnter();
    }

    public void HandleLookInput(Vector3 lookInput)
    {
        m_CurrentCameraState.HandleLookInput(lookInput);
    }

    public void HandleSelectInput()
    {
        m_CurrentCameraState.HandleSelectInput();
    }

    public void RegisterPlayerController(PlayerController pc)
    {
        m_RegisteredPlayerController = pc;
    }
}
