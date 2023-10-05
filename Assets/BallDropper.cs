using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class BallDropper : MonoBehaviour
{
    public GameObject[] balls;
    public float speed;
    private Vector2 inputVec;
    private GameObject currentBall;
    private float gameTime;

    public TMP_Text text;
    
    public static int BallCount;
    
    private void Awake()
    {
        BallCount = 0;
        inputVec = Vector2.zero;
    }

    private void Start()
    {
        GetRandomBall();
    }

    void GetRandomBall()
    {
        currentBall = Instantiate(balls[Random.Range(0, balls.Length)]);
        currentBall.GetComponent<Rigidbody2D>().gravityScale = 0;
        currentBall.GetComponent<Rigidbody2D>().simulated = false;
        currentBall.GetComponent<Ball>().OnBallCollisionEnter2D += OnBallCollisionEnter2D;
        BallCount++;
    }
    
    // Update is called once per frame
    void Update()
    {
        text.text = BallCount.ToString();
        gameTime += Time.deltaTime;
        transform.position += (Vector3)inputVec * (speed * Time.deltaTime); 
        if(currentBall != null) currentBall.transform.position = transform.position;
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void OnFire()
    {
        currentBall.GetComponent<Rigidbody2D>().gravityScale = 1;
        currentBall.GetComponent<Rigidbody2D>().simulated = true;
        currentBall.GetComponent<Ball>().id = gameTime;
        currentBall = null;
    }

    private void OnBallCollisionEnter2D(Ball ball)
    {
        ball.OnBallCollisionEnter2D -= OnBallCollisionEnter2D;
        GetRandomBall();
    }

}
