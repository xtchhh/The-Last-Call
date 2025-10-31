using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Animator flashLightAnim;
    public bool IsOn { get; private set; } = true; // Default to on, change as needed

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                flashLightAnim.ResetTrigger("Walk");
                flashLightAnim.SetTrigger("Sprint");
            }
            else
            {
                flashLightAnim.ResetTrigger("Sprint");
                flashLightAnim.SetTrigger("Walk");
            }
        }
    }
}
