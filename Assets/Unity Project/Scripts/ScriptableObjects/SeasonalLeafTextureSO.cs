using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SeasonalLeafTextureSO", menuName = "New SeasonalLeafTexture Palette")]
public class SeasonalLeafTextureSO : ScriptableObject
{
    public List<Texture2D> SpringLeafTextures;
    public List<Texture2D> AutumnLeafTextures;
}
