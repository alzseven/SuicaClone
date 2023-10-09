using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject nextBall;
    public float id;
    public bool isReady;
    public Action<Ball> OnBallCollisionEnter2D;

    private void Awake()
    {
        isReady = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name.Contains("Circle") ||
           other.gameObject.name.Contains("Square")) OnBallCollisionEnter2D?.Invoke(this);
        
        if (nextBall == null) return;

        
        // TODO: Merge single ball only even collides two or more
        if (other.gameObject.name == this.name)
        {
            if (id > other.gameObject.GetComponent<Ball>().id) return;
            
            var newBall = Instantiate(nextBall, other.transform.position, Quaternion.identity);
            if (newBall.TryGetComponent<Ball>(out var b))
            {
                b.isReady = true;
                b.id = BallDropper.gameTime;
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
            BallDropper.BallCount--;
        }
    }
}