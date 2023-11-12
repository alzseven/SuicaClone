using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private EveryBallData everyBallData;
        [SerializeField] private int droppableMinIndex;
        [SerializeField] private int droppableMaxIndex;
        [SerializeField] private float ballMass;

        public EveryBallData EveryBallData => everyBallData;

        public int DroppableMinIndex => droppableMinIndex;
        public int DroppableMaxIndex => droppableMaxIndex;
        public float BallMass => ballMass;
    }
}