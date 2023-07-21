using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : GenericSingleton<TimeManager>
{
    private bool m_IsTicking = true;
    public bool IsTicking { get => m_IsTicking; }

    public static int HourOffset = 0;

    public static int MinuteOffset = 0;

    public DateTime CurrentDT { get => DateTime.Now.ToLocalTime(); }

    private IEnumerator m_TimeTickCRT;


    private UnityEvent m_OnTickEvent = new();
    public UnityEvent OnTickEvent { get => m_OnTickEvent; }

    private LightingManager m_LightingManager;

    private new void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else if (m_Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        m_TimeTickCRT = TimeTickCRT();
        StartCoroutine(m_TimeTickCRT);
    }

    private void OnDisable()
    {
        if (m_TimeTickCRT != null)
        {
            StopCoroutine(m_TimeTickCRT);
        }
        // Remove Event Subscribers
        m_OnTickEvent.RemoveAllListeners();
    }

    private void Start()
    {
        m_LightingManager = GetComponent<LightingManager>();

    }

    private void LateUpdate()
    {
        m_LightingManager.UpdateLighting((float)CurrentDT.AddHours(HourOffset).AddMinutes(MinuteOffset).TimeOfDay.TotalHours / 24f);
        
        // TODO: Remove this, just for demonstration
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Hours
            if (Input.GetKey(KeyCode.H))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    HourOffset++;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    HourOffset--;
                }
            }
            // Minutes
            else if (Input.GetKey(KeyCode.M))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MinuteOffset++;
                    if (MinuteOffset % 60 == 0)
                    {
                        MinuteOffset %= 60;
                        HourOffset++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MinuteOffset--;
                    if (MinuteOffset % -60 == 0)
                    {
                        MinuteOffset %= -60;
                        HourOffset--;
                    }
                }
            }
        }
    }

    // + + + + | Functions | + + + + 

    private IEnumerator TimeTickCRT()
    {
        //float timeHelper = 0f;

        while (m_IsTicking)
        {
            //Debug.Log($"Ticked to indicate {(float) CurrentDT.TimeOfDay.TotalHours / 24f}% of the day completed!");
            m_OnTickEvent?.Invoke();
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
