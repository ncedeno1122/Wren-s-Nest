using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the Object towards the Player while the sprite is visible by the cameras.
/// </summary>
public class BillBoardOnVisible : MonoBehaviour
{
    private bool m_IsVisible = false;

    private Camera m_MainCamera;

    private void Start()
    {
        m_MainCamera = Camera.main;
    }

    private void OnBecameVisible()
    {
        m_IsVisible = true;
    }

    private void OnBecameInvisible()
    {
        m_IsVisible = false;
    }

    private void LateUpdate()
    {
        if (!m_IsVisible) return;

        // If we're VISIBLE,
        transform.LookAt(m_MainCamera.transform);
        transform.Rotate(new Vector3(0f, 180f, 0f));
    }
}