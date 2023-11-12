using System;
using System.Collections;
using System.Collections.Generic;
using Data.Variables;
using UnityEngine;

public class Threshold : MonoBehaviour
{
    [SerializeField] private ObservableFloatValue gameTime;
    
    //TODO:
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Ball>(out var b))
        {
            if (gameTime.Value - b.ballID > 1f)
            {
                Debug.Log("GameOver");

                //TODO: GameOver
                Time.timeScale = 0;
            }
        }
    }
}