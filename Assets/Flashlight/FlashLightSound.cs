using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSound : MonoBehaviour
{
    public AudioSource ClickSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Check if the "F" key is pressed
        {
            ClickSound.Play(); // Play the click sound
        }
    }
}
