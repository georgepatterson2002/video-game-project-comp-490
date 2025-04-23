using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShootAnimation : MonoBehaviour
{
    private Animator animator;
    public FishSpawner fishSpawner;

    private bool countdownOver = false;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator on the same GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if(fishSpawner.countdownActive == false)
        {
            countdownOver = true;
        }

        if (Input.GetMouseButtonDown(0) && countdownOver == true)
        {
            animator.SetTrigger("Shoot"); // Play gunshot animation
        }
    }
}
