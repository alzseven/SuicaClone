using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallDropper : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip Dropclip;
    public AudioClip MergeClip;
    public GameObject[] balls;
    public float speed;
    private Vector2 inputVec;
    private GameObject currentBall;
    private GameObject nextBall;
    public static float gameTime;

    public TMP_Text text;
    public Image nextBallSprite;

    private LineRenderer _lineRenderer;
    
    public static int BallCount;
    
    private void Awake()
    {
        BallCount = 0;
        inputVec = Vector2.zero;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        SetNextBall();
        GetRandomBall();
    }

    void SetNextBall()
    {
        nextBall = balls[Random.Range(0, balls.Length)];
    }
    
    void GetRandomBall()
    {
        currentBall = Instantiate(nextBall);
        currentBall.GetComponent<Rigidbody2D>().gravityScale = 0;
        currentBall.GetComponent<Rigidbody2D>().simulated = false;
        currentBall.GetComponent<Ball>().OnBallCollisionEnter2D += OnBallCollisionEnter2D;
        BallCount++;
        SetNextBall();
    }
    
    // Update is called once per frame
    void Update()
    {
        text.text = BallCount.ToString();
        gameTime += Time.deltaTime;
        transform.position += (Vector3)inputVec * (speed * Time.deltaTime);
        if (currentBall != null)
        {
            currentBall.transform.position = transform.position;

            _lineRenderer.enabled = true;
            RaycastHit2D ray = Physics2D.Raycast(transform.position,
                Vector3.down,
                float.MaxValue,
                1<<6);
            if (ray)
            {
                _lineRenderer.SetPosition(1, Vector3.down * ray.distance);
            }
        }
        else
        {
            _lineRenderer.enabled = false;
        }
        if (nextBall != null)
        {
            nextBallSprite.sprite = nextBall.GetComponent<SpriteRenderer>().sprite;
            nextBallSprite.color = nextBall.GetComponent<SpriteRenderer>().color;
        }
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
        AudioSource.PlayOneShot(Dropclip);
    }

    private void OnBallCollisionEnter2D(Ball ball)
    {
        ball.OnBallCollisionEnter2D -= OnBallCollisionEnter2D;
        AudioSource.PlayOneShot(MergeClip);
        GetRandomBall();
    }

}
