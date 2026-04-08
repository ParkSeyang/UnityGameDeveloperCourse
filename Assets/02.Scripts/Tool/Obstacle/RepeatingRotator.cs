using UnityEngine;
using UnityEngine.Serialization;
using Obstacle;

namespace Obstacle
{
    public class RepeatingRotator : MonoBehaviour
    {
        [SerializeField] private RotationInfo rotationInfo;
    
        void Update()
        {
            float deltaAngle = rotationInfo.speed * Time.deltaTime;
            Vector3 deltaEuler = rotationInfo.axis.normalized * deltaAngle;
            Space axisSpace = rotationInfo.isLocal ? Space.Self : Space.World; 
            transform.Rotate(deltaEuler, axisSpace);
        }
    }


}

