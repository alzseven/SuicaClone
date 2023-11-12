using System;
using Data.Variables;
using UnityEngine;

public class SimpleTimer : MonoBehaviour
{ 
    public ObservableFloatValue gameTime;

    private void Update() => gameTime.Value += Time.deltaTime;
}