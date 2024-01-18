using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public class BulletinController : MonoBehaviour
{
    [Range(0, 10)]
    public int BulletinEventsToPrint = 5;

    private List<BulletinEvent> m_CachedBulletinEvents = new();

    public TMP_Text BulletinText;

    //

    private void Awake()
    {
        // Get & Initialize BulletinText
        BulletinText = GetComponentInChildren<TMP_Text>();
        BulletinText.text = String.Empty;

        // Try find the JSON paths...
        string path = Application.dataPath + "/Documents/TestBulletinEvents.json";
        string path2 = Application.dataPath + "/Documents/TestBulletinEventsAdditional.json";

        // Populate List of ALL processed JSON Strings, per path
        List<string> jsonStringList = GetJSONStringListFrom(path);
        jsonStringList.AddRange(GetJSONStringListFrom(path2));

        // Populate Bulletin
        PopulateBulletinEventList(jsonStringList);

        // Filter List for Recent Events
        DisplayFilteredBulletinEvents();

        //foreach (BulletinEvent be in bulletinEvents) Debug.Log(be);
    }

    //

    [ContextMenu("Awake")]
    public void TestAwakeWrapper()
    {
        Awake();
    }

    // + + + + | Functions | + + + +

    private List<string> GetJSONStringListFrom(string path)
    {
        // Try and load / print data?
        string[] rawJSONLines = System.IO.File.ReadAllLines(path);
        List<string> jsonBlocks = new(rawJSONLines.Length);
        int currBlockStartIndex = -1;
        int currBlockEndIndex = -1;
        StringBuilder sb = new();

        for (int i = 0; i < rawJSONLines.Length; i++)
        {
            string currLine = rawJSONLines[i];
            currLine = currLine.Trim();

            if (i > 0) // Exclude first delimiter
            {
                // Are we at the start of a block?
                if (currLine.EndsWith('{')) // Find outer opening braces
                {
                    //Debug.Log("<color=green>" + currLine + "</color>");
                    currBlockStartIndex = i;
                    currLine = "{";
                    //sb.Append('{');
                }
                else if (currLine.StartsWith('}') && i + 1 < rawJSONLines.Length) // Find outer closing braces, exclude last delimiter
                {
                    //Debug.Log("<color=red>" + currLine + "</color>");
                    currBlockEndIndex = i;
                    currLine = currLine.TrimEnd(',');
                }
                sb.Append(currLine);
            }

            // Finally, add the line to the current JSON Block!
            if (currBlockStartIndex < currBlockEndIndex && (currBlockStartIndex != -1 && currBlockEndIndex != -1))
            {
                jsonBlocks.Add(sb.ToString());
                sb.Clear();
                currBlockStartIndex = -1;
                currBlockEndIndex = -1;
            }
        }

        //StringBuilder sb2 = new();
        ////for (int i = 0; i < rawJSONText.Length; i++) sb.Append(rawJSONText[i] + " | ");
        //foreach (string line in jsonBlocks) { sb2.Append(line + "\n"); }
        //Debug.Log(sb2.ToString());

        return jsonBlocks;
    }

    private void PopulateBulletinEventList(List<string> jsonStringList)
    {
        // Populate List of ALL BulletinEvents
        foreach (string jsonString in jsonStringList)
        {
            m_CachedBulletinEvents.Add(BulletinEvent.CreateFromJSON(jsonString));
        }
    }

    private void DisplayFilteredBulletinEvents()
    {
        DateTime currDT = DateTime.Now;
        List<BulletinEvent> filteredEvents = m_CachedBulletinEvents.Where(
            be => be.year >= currDT.Year &&
            be.month >= currDT.Month &&
            be.day >= currDT.Day &&
            be.time >= ((currDT.Hour * 100) + currDT.Minute)
        ).ToList<BulletinEvent>();
        filteredEvents.Sort();

        // Print first few Filtered List BulletinEvents to BulletinText
        for (int i = 0; i < BulletinEventsToPrint; i++)
        {
            if (i < filteredEvents.Count)
            {
                BulletinText.text += filteredEvents[i].ToString() + "\n";
            }
        }
    }
}

/// <summary>
/// Represents a singular event's data on the Bulletin Board.
/// </summary>
[Serializable]
public class BulletinEvent : IComparable
{
    public string name;
    public int month;
    public int day;
    public int year;
    public int time; // Taken in 24-hr time with minutes appended at the end, like military time (ex: 2:30pm is 1430)

    public override string ToString()
    {
        return $"[EVENT: {name}, {month}/{day}/{year} @ {time}]";
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        if (obj is BulletinEvent be)
        {
            return CompareTo(be);
        }
        else return 0;
    }

    /// <summary>
    /// Compares two BulletinEvents by their years, months, dates, then time.
    /// The one that is greater happens later, unless they occur at the exact same time.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(BulletinEvent other)
    {
        if (other == null) return 1;

        // Year
        if (this.year == other.year)
        {
            // Month
            if (this.month == other.month)
            {
                // Day
                if (this.day == other.day)
                {
                    // Time
                    if (this.time == other.time) return 0;
                    else return this.time.CompareTo(other.time);
                }
                else return this.day.CompareTo(other.day);
            }
            else return this.month.CompareTo(other.month);
        }
        else return this.year.CompareTo(other.year);
    }

    public static BulletinEvent CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<BulletinEvent>(jsonString);
    }
}