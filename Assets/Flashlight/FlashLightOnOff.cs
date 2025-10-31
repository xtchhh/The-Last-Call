using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightOnOff : MonoBehaviour
{
    public Light FlashLight; // Reference to the flashlight light source

    // Public property to expose the flashlight's state
    public bool IsOn => FlashLight.enabled;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Check if the "F" key is pressed
        {
            FlashLight.enabled = !FlashLight.enabled; // Toggle the active state of the FlashLight Light Source
        }
    }
}
