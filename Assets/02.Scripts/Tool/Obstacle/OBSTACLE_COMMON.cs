using UnityEngine;

namespace Obstacle
{
    [System.Serializable]
    public struct RotationInfo
    {
        public Vector3 axis;
        public float speed;
        public bool isLocal;
    }
    
    [System.Serializable]
    public struct MoveInfo
    {
        public Vector3 localDirection;
        public float distance;
        public float speed;
        public float waitTime;
    }
}