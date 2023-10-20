using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class TestLeafRecolorScript : MonoBehaviour
{
    public static string[] LeafMaterialLookupNames = { "Branch", "Leaf", "Bush" }; // All known Leaf material names on trees I use

    public SeasonalLeafTextureSO SeasonalLeafPalette;
    private MeshRenderer m_MeshRenderer;
    [SerializeReference] private Material m_InstanceLeafMaterial;

    void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();

        // Try and find leaf texture!
        foreach (Material mat in m_MeshRenderer.materials)
        {
            for (int i = 0; i < LeafMaterialLookupNames.Length; i++)
            {
                if (mat.name.Contains(LeafMaterialLookupNames[i]))
                {
                    m_InstanceLeafMaterial = mat;
                    break;
                }
            }
        }
    }

    private void Start()
    {
        // Change Materials based on Season (if necessary)
        if (!TimeManager.Exists) return;
        Season currSeason = TimeManager.Instance.GetCurrentSeason();
        switch (currSeason)
        {
            case Season.SPRING:
                break;
            case Season.SUMMER:
                break;
            case Season.AUTUMN:
                m_InstanceLeafMaterial.mainTexture = SeasonalLeafPalette.AutumnLeafTextures[Random.Range(0, SeasonalLeafPalette.AutumnLeafTextures.Count)];
                break;
            case Season.WINTER:
                m_InstanceLeafMaterial.color = Color.clear; // No leaves in winter :D
                break;
        }

    }
}