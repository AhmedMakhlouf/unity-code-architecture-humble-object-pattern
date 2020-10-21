using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovementSettings", menuName = "Game Settings/Movement", order = 1)]
public class MovementSettings : ScriptableObject
{
    public float maxSpeed;
    public bool drag;
}
