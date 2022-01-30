using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Grabber : MonoBehaviour
    {
        public Rigidbody root;

        private Rigidbody grabbedGo;
        //private FixedJoint j;

        public Transform headRoot;
        private Vector3 offset;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (grabbedGo != null)
                {
                    grabbedGo.useGravity = true;
                    grabbedGo.isKinematic = false;
                    //Destroy(j);
                    grabbedGo.velocity = Vector3.zero;
                    grabbedGo.angularVelocity = Vector3.zero;
                    grabbedGo.detectCollisions = true;
                    grabbedGo.transform.SetParent(null);
                    grabbedGo = null;
                }

                else
                {
                    Ray ray = new Ray(transform.position, transform.forward);
                    var hits = Physics.SphereCastAll(ray, 0.3f, 7f);

                    var hit = hits.FirstOrDefault(h => h.rigidbody != null && h.rigidbody != root);

                    if (hit.rigidbody)
                    {
                        Debug.Log("Grabbed: " + hit.rigidbody.gameObject);
                        grabbedGo = hit.rigidbody;
                        grabbedGo.useGravity = false;
                        grabbedGo.isKinematic = true;
                        //j = root.gameObject.AddComponent<FixedJoint>();
                        //j.connectedBody = grabbedGo;
                        grabbedGo.detectCollisions = false;
                        grabbedGo.angularVelocity = Vector3.zero;
                        grabbedGo.velocity = Vector3.zero;
                        offset = headRoot.InverseTransformPoint(headRoot.position - grabbedGo.transform.position);
                        grabbedGo.transform.SetParent(headRoot, true);
                    }
                }
            }

            if (grabbedGo != null)
            {
                //grabbedGo.position = headRoot.position + headRoot.TransformVector(offset);
            }
        }
    }
}