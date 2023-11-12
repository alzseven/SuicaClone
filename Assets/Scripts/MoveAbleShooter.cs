using Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class MoveAbleShooter : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private EdgeCollider2D moveAbleArea;
    private Vector2 _inputVec;

    [SerializeField] private SimpleEventPublisher onFirePublisher;

    private void Awake() => _inputVec = Vector2.zero;

    private void Update()
    {
        var newPos = transform.position + (Vector3)_inputVec * (moveSpeed * Time.deltaTime);
        
        if (newPos.x < moveAbleArea.points[0].x) newPos.x = moveAbleArea.points[1].x;
        if (newPos.x > moveAbleArea.points[1].x) newPos.x = moveAbleArea.points[0].x;

        transform.position = newPos;
    }

    #region PlayerInput
    
    private void OnMove(InputValue value) => _inputVec = value.Get<Vector2>();
    
    private void OnFire() => onFirePublisher.PublishMessage();
    
    #endregion

}
