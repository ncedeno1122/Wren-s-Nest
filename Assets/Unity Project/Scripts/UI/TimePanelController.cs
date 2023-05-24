using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimePanelController : MonoBehaviour
{
    private bool m_UpdatingTime = true;
    //private IEnumerator m_TimeCRT;
    private TextMeshProUGUI m_TimeText;

    private void Awake()
    {
        m_TimeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        //m_TimeCRT = UpdateTimeCRT();
        //StartCoroutine(m_TimeCRT);
        if (TimeManager.Exists)
        {
            TimeManager.Instance.OnTickEvent.AddListener(OnTimeManagerTick);
        }
    }

    private void OnDisable()
    {
        //StopCoroutine(m_TimeCRT);
    }

    // + + + + | Functions | + + + +

    /// <summary>
    /// Updates the Time Text every 'timeUpdateFrequency' seconds.
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateTimeCRT()
    {
        float timeHelper = 0f;
        float timeUpdateFrequency = 2f; // In seconds

        while (m_UpdatingTime)
        {
            // Update Time
            if (timeHelper > timeUpdateFrequency)
            {
                //m_TimeText.text = DateTime.Now.ToString("t");
                timeHelper = 0f;
                //Debug.Log("Updating time!");
            }
            timeHelper += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTimeManagerTick()
    {
        m_TimeText.text = TimeManager.Instance.CurrentDT.ToString("h:mm:ss tt") + "\n" +
            TimeManager.Instance.CurrentDT.TimeOfDay.TotalHours;
    }
}
