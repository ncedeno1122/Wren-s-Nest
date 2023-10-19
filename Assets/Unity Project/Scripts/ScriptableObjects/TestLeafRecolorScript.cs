using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class TestLeafRecolorScript : MonoBehaviour
{
    public static Color[] AutumnColors =
    {
        new Color(255 / 255f, 90 / 255f, 10 / 255f), // Orange
        new Color(148 / 255f, 34 / 255f, 33 / 255f), // Maroon
        new Color(217 / 255f, 171 / 255f, 33 / 255f) // Yellow
    };

    public Texture RedLeavesTexture, OrangeLeavesTexture, YellowLeavesTexture;
    public Texture[] LeafTextures;

    void Awake()
    {
        LeafTextures = new Texture[] { RedLeavesTexture, OrangeLeavesTexture, YellowLeavesTexture };

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        
        foreach (Material mat in renderer.materials)
        {
            //Debug.Log($"Found {mat.name}");
            if (mat.name.Contains("Branch"))
            {
                Debug.Log("Trying to change color!");
                //mat.color = AutumnColors[Random.Range(0, AutumnColors.Length)];
                //mat.mainTexture = LeafTextures[Random.Range(0, LeafTextures.Length)];
                //mat.color = Color.red;
            }
        }
    }
}