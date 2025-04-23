using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball")) // Make sure your ball is tagged "Ball"
        {
            GameObject.Find("GameManager").GetComponent<ScoreManager>().AddPoint();
        }
    }
}
