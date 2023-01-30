using UnityEngine;

namespace ScentInTheShadow.Mechanic.Player.Controller
{
    [System.Serializable]
    public class ItemTargetScanner
    {
        public float heightOffset = 0.0f;
        public float detectionRadius = 10;
        [Range(0.0f, 360.0f)]
        public float detectionAngle = 270;
        public float maxHeightDifference = 1.0f;
        public LayerMask viewBlockerLayerMask;


        public GameObject DetectPlayer(Transform detector,GameObject Items, bool useHeightDifference = true)
        {

            Vector3 eyePos = detector.position + Vector3.up * heightOffset;
            Vector3 toPlayer = Items.transform.position - eyePos;
            Vector3 toPlayerTop = Items.transform.position + Vector3.up * 1.5f - eyePos;

            if (useHeightDifference && Mathf.Abs(toPlayer.y + heightOffset) > maxHeightDifference)
            { //if the target is too high or too low no need to try to reach it, just abandon pursuit
                Items.GetComponent<Animator>().Play("idle");
                return null;
            }

            Vector3 toPlayerFlat = toPlayer;
            toPlayerFlat.y = 0;

            if (toPlayerFlat.sqrMagnitude <= detectionRadius * detectionRadius)
            {
                if (Vector3.Dot(toPlayerFlat.normalized, detector.forward) >
                    Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                {

                    Debug.DrawRay(eyePos, toPlayer, Color.blue);
                    Debug.DrawRay(eyePos, toPlayerTop, Color.blue);
                    Items.GetComponent<Animator>().Play("shine");
                    return Items;
                }
            }
            Items.GetComponent<Animator>().Play("idle");
            return null;
        }


#if UNITY_EDITOR

        public void EditorGizmo(Transform transform)
        {
            Color c = new Color(0, 0, 0.7f, 0.4f);

            UnityEditor.Handles.color = c;
            Vector3 rotatedForward = Quaternion.Euler(0, -detectionAngle * 0.5f, 0) * transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, detectionAngle, detectionRadius);

            Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
            Gizmos.DrawWireSphere(transform.position + Vector3.up * heightOffset, 0.2f);
        }

#endif
    }
}

