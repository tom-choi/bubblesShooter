using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSO", menuName = "DataSO/Data")]
public class DataSO : ScriptableObject
{
    public new string name;
    [TextArea(3,10)]
    public string description;
}
