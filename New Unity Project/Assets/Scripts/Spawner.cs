using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnOrientation
    {
        Up,
        Down,
        Left,
        Right
    }

    public SpawnOrientation thisOrientation;
}
