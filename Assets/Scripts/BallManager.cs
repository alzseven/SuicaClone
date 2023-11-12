using Data;
using Data.Variables;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallManager : MonoBehaviour
{
    //TODO:
    [SerializeField] private GameData gameData;
    [SerializeField] private Ball ballPrefab;
    
    [SerializeField] private ObservableIntValue score;
    [SerializeField] private ObservableFloatValue gameTime;
    
    [SerializeField] private Transform currentBallTransform;
    [SerializeField] private SimpleEventPublisher onFirePublisher;
    [Header("Ball Index")]
    [SerializeField] private ObservableIntValue currentBallIndex;
    [SerializeField] private ObservableIntValue nextBallIndex;
    [Header("SfxEvent")]
    [SerializeField] private SfxEventPublisher onThrowBallSfxPublisher;
    [SerializeField] private SfxEventPublisher onMergeBallSfxPublisher;
    

    private void OnEnable() => onFirePublisher.OnEventOccured += ThrowBall;

    private void OnDisable() => onFirePublisher.OnEventOccured -= ThrowBall;

    private void Start()
    {
        //TODO:
        score.Value = 0;
        
        UpdateNextBallIndex();
        AdvanceBallIndex();
    }
    
    private void AdvanceBallIndex()
    {
        currentBallIndex.Value = nextBallIndex.Value;
        UpdateNextBallIndex();
    }
    
    private void UpdateNextBallIndex()
    {
        UpdateNextBallIndex(Random.Range(gameData.DroppableMinIndex, gameData.DroppableMaxIndex));
    }

    private void UpdateNextBallIndex(int value)
    {
        nextBallIndex.Value = value;
    }
    
    private void ThrowBall()
    {
        ThrowBall(currentBallTransform);
    }

    private void ThrowBall(Transform t)
    {
        if (currentBallIndex.Value > -1)
        {
            onThrowBallSfxPublisher.PublishMessage();
            var newBall = CreateBall(currentBallIndex.Value, t.position);
            currentBallIndex.Value = -1;
        }
    }

    // private Ball CreateBall(int ballIndex)
    // {
    //     return CreateBall(ballIndex, Vector3.zero);
    // }
    
    //TODO:
    private Ball CreateBall(int ballIndex, Vector3 pos, bool enableCollision = true)
    {
        if (gameData.EveryBallData.TryGetBallData(ballIndex, out var ballData))
        {
            //TODO: Take out from pool?
            var newBall = Instantiate(ballPrefab,
                pos == Vector3.zero ? transform.position : pos,
                Quaternion.identity,
                transform);

            if (newBall.TryGetComponent<Ball>(out var ball))
            {
                if(enableCollision) ball.OnBallCollisionEnter2D += OnBallCollisionEnter2D;
                ball.OnBallsCollidedEach += Merge;
                ball.Initialize(ballIndex, gameTime.Value, ballData, gameData.BallMass);
            }
        
            return newBall;
        }

        return null;
    }

    //TODO:
    private void OnBallCollisionEnter2D(Ball ball)
    {
        if(currentBallIndex.Value < 0) AdvanceBallIndex();
        ball.OnBallCollisionEnter2D -= OnBallCollisionEnter2D;
    }

    //TODO:
    private void Merge(Ball ball1, Ball ball2)
    {
        if (ball1.ballIndex != ball2.ballIndex) return;
        if (ball1.ballID < ball2.ballID) return;    //case: skip - need review

        var newBallPos = (ball1.transform.position + ball2.transform.position) * 0.5f;
        var newBallType = ball1.ballIndex + 1;

        ball1.OnBallsCollidedEach -= Merge;
        ball2.OnBallsCollidedEach -= Merge;
        
        Debug.Log($"{ball1.ballIndex}({ball1.ballID}) and {ball2.ballIndex}({ball2.ballID}) merged into {newBallType}");
        
        Destroy(ball1.gameObject);
        Destroy(ball2.gameObject);
        
        //TODO: Delayed?(After Animation OR VFX) 

        var newBall = CreateBall(newBallType, newBallPos,false);

        if (gameData.EveryBallData.TryGetBallData(newBallType - 1, out var ball))
        {
            score.Value += ball.MergeScore;
        }
        onMergeBallSfxPublisher.PublishMessage();
    }
}