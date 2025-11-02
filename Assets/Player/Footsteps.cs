using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource walk, run;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                walk.enabled = false;
                run.enabled = true;
            }
            else
            {
                walk.enabled = true;
                run.enabled = false;
            }
        }
        else
        {
            walk.enabled = false;
            run.enabled = false;
        }
    }
}