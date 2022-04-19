using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    public class ViewSkeleton : MonoBehaviour
    {
        public Transform[] childNodes;

        void OnDrawGizmos()
        {
            if (childNodes == null)
            {
                PopulateChildren();
            }


            foreach (Transform child in childNodes)
            {

                if (child == transform)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(child.position, new Vector3(.1f, .1f, .1f));
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(child.position, child.parent.position);
                    Gizmos.DrawCube(child.position, new Vector3(.01f, .01f, .01f));
                }
            }
        }

        public void PopulateChildren()
        {
            childNodes = GetComponentsInChildren<Transform>();
        }
    }
}