using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShootAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator on the same GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Shoot"); // Play gunshot animation
        }
    }
}
