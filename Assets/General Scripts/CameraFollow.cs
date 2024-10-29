using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform character;

    void Update()
    {
        // Follow the character's position but ignore rotation
        transform.position = character.position;
    }
}