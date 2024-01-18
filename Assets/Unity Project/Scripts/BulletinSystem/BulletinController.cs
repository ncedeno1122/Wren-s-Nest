using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Collections.Generic;

public class BulletinController : MonoBehaviour
{
    private TextMeshProUGUI m_BulletinText;

    //

    private void Awake()
    {
        m_BulletinText = GetComponentInChildren<TextMeshProUGUI>();

        //Debug.Log(JsonUtility.ToJson(new BulletinEvent() { name = "SideshowEvent", month = 1, day = 4, year = 2024, time = 0258 }));

        // Try find the thing...
        string path = Application.dataPath + "/Documents/TestBulletinEvents.json";
        List<string> jsonStringList = GetJSONStringListFrom(path);
        List<BulletinEvent> bulletinEvents = new();

        foreach (string jsonString in jsonStringList)
        {
            //Debug.Log(jsonString);
            bulletinEvents.Add(BulletinEvent.CreateFromJSON(jsonString));
        }

        foreach (BulletinEvent be in bulletinEvents) Debug.Log(be);
    }

    //

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
}

/// <summary>
/// Represents a singular event's data on the Bulletin Board.
/// </summary>
[Serializable]
public class BulletinEvent
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

    public static BulletinEvent CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<BulletinEvent>(jsonString);
    }
}