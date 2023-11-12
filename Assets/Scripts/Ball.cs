using System;
using Data;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour
{
    public int ballIndex;
    public float ballID;

    public event Action<Ball> OnBallCollisionEnter2D;
    public event Action<Ball, Ball> OnBallsCollidedEach;

    private void Awake()
    {
        ballIndex = -1;
        ballID = 0f;
    }

    public void Initialize(int index, float id, BallData newBallData, float mass)
    {
        ballIndex = index;
        ballID = id;
        transform.localScale = newBallData.BallSize;
        GetComponent<SpriteRenderer>().sprite = newBallData.BallSprite;
        GetComponent<Rigidbody2D>().mass = mass;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO? only once?
        OnBallCollisionEnter2D?.Invoke(this);

        var otherGameObject = other.gameObject;
        
        //TODO: Replace literal string
        if (otherGameObject.tag.Equals("Ball"))
        {
            if (otherGameObject.TryGetComponent<Ball>(out var ball))
            {
                OnBallsCollidedEach?.Invoke(this, ball);
            }
        }
    }
}