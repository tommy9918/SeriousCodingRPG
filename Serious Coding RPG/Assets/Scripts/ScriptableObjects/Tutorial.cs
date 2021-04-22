using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "ScriptableObjects/Tutorial", order = 1)]
public class Tutorial : ScriptableObject
{
    public Sprite sprite;
    public string TutorialID;
}
