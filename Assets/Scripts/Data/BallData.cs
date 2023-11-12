using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "BallData", menuName = "Data/BallData", order = 11)]
    public class BallData : ScriptableObject
    {
        [SerializeField] private Sprite ballSprite;
        [SerializeField] private Vector3 ballSize = Vector3.one;
        [SerializeField] private int mergeScore;

        public Sprite BallSprite => ballSprite;
        public Vector3 BallSize => ballSize;
        public int MergeScore => mergeScore;
    }
}