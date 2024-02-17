using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressHandler : MonoBehaviour
{
    public virtual void UpdateValues(float current, float max)
    {
        print("Current: " + current + "/" + max);
    }
}
