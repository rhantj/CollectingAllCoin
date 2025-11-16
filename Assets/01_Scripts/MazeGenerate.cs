using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerate : MonoBehaviour
{
    [SerializeField] Transform ground;
    float width;
    float height;

    private void Awake()
    {
        width = 10f * ground.position.x;
        height = 10f * ground.position.z;
    }
}
