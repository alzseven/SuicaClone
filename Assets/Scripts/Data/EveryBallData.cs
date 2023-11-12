using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EveryBallData", menuName = "Data/EveryBallData", order = 0)]
    public class EveryBallData : ScriptableObject
    {
        [SerializeField] private BallData[] data;
 
        public BallData GetBallData(int index)
        {
            return 0 <= index && index < data.Length ? data[index] : null;
        }

        public bool TryGetBallData(int index, out BallData res)
        {
            res = GetBallData(index);
            return res != null;
        }
    }
}