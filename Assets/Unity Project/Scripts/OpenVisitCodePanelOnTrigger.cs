using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenVisitCodePanelOnTrigger : MonoBehaviour
{
    bool HasPlayerOpenedPanel = false; // ONLY lets you open the panel one time upon entering.

    // + + + + | Collision Handling | + + + + 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !HasPlayerOpenedPanel)
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            ScriptableCameraController scc = pc.m_PlayerCamera;
            scc.ChangeState(new CameraMouseUIState(scc));
            HasPlayerOpenedPanel = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasPlayerOpenedPanel = false;
        }
    }
}
