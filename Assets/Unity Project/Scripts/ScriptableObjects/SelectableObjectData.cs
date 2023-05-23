using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SelectableObjectData", menuName = "ScriptableObjects/SelectableObjectData")]
public class SelectableObjectData : ScriptableObject
{
    public string ObjectName;
    [TextArea(minLines: 1, maxLines: 3)]
    public string ObjectDescription;
    public string ResourceURL;

    // Vectors for the Camera to move and rotate to upon selection.
    public Vector3 CameraPositionalOffset;
    public Vector3 CameraRotationalOffset;
}
