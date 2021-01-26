using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deltaTime : MonoBehaviour
{
    private float fixedDeltaTime;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Button pressed");
            if (Time.timeScale == 1.0f)
                Time.timeScale = 5.0f;
            else
                Time.timeScale = 1.0f;
            // Adjust fixed delta time according to timescale
            // The fixed delta time will now be 0.02 frames per real-time second
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
    }
}
