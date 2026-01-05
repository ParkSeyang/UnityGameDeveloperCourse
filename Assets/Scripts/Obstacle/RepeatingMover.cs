using System;
using UnityEngine;

namespace Obstacle
{
    public class RepeatingMover : MonoBehaviour
    {
        [SerializeField] private MoveInfo moveInfo;
    
        private Vector3 startLocalPosition;
        private Vector3 endLocalPosition;

        private bool targetToggle = true; // true : endLocalPosition, false : startLocalPosition 
        private float waitTime;

        private void Start()
        {
            startLocalPosition = transform.localPosition;
            endLocalPosition = startLocalPosition + moveInfo.localDirection * moveInfo.distance;
        }

        private void Update()
        {
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                return;
            }

            Vector3 target = targetToggle ? endLocalPosition : startLocalPosition;
            transform.localPosition =
                Vector3.MoveTowards(
                    transform.localPosition, target,
                    moveInfo.speed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, target) <= 0.01f)
            {
                targetToggle = !targetToggle;
                waitTime = moveInfo.waitTime;
            }
        }
    
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Vector3 currentStart = Application.isPlaying ? startLocalPosition : transform.localPosition;
            Vector3 currentEnd = currentStart + moveInfo.localDirection.normalized * moveInfo.distance;

            Vector3 worldStart = transform.parent != null ? transform.parent.TransformPoint(currentStart) : currentStart;
            Vector3 worldEnd = transform.parent != null ? transform.parent.TransformPoint(currentEnd) : currentEnd;

            Gizmos.DrawLine(worldStart, worldEnd);
            Gizmos.DrawSphere(worldStart, 0.1f);
            Gizmos.DrawSphere(worldEnd, 0.1f);
        }
    }

}

