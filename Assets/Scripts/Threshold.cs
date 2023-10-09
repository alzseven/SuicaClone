using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threshold : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Ball>(out var b))
        {
            if (b.isReady)
            {
                Debug.Log("GameOver");
                Time.timeScale = 0;
            }
            else
            {
                b.isReady = true;                
            }
        }
    }
}
