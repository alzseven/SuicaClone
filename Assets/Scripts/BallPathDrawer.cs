using System;
using Data;
using Data.Variables;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BallPathDrawer : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Vector3 rayDirection;
    [SerializeField] private ObservableIntValue currentBallIndex;
    
    private LineRenderer _lineRenderer;
    private RaycastHit2D _ray;
    private void Awake() => _lineRenderer = GetComponent<LineRenderer>();

    private void OnEnable() => currentBallIndex.OnValueChanged += OnCurrentBallIndexChanged;

    private void OnDisable() => currentBallIndex.OnValueChanged -= OnCurrentBallIndexChanged;

    private void Start() =>
        _ray = Physics2D.Raycast(transform.position,
            rayDirection,
            Mathf.Infinity,
            targetLayer);
  
    private void Update()
    {
        // RaycastHit2D ray = Physics2D.Raycast(transform.position,
        //     rayDirection,
        //     Mathf.Infinity,
        //     targetLayer);
        if (_ray)
        {
            _lineRenderer.SetPosition(1, Vector3.down * _ray.distance);
        }
    }
        
    private void OnCurrentBallIndexChanged(int index) => _lineRenderer.enabled = index > -1;
}