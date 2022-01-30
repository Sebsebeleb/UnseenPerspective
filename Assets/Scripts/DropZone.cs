using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class DropZone : MonoBehaviour
    {
        public string correctTag;
        public Rigidbody attached;
        private float currentGravityForce;
        public float increaseRate, maxForce;
        public UnityEvent onComplete;
        
        private void OnTriggerEnter(Collider other)
        {
            if (attached != null)
            {
                return;
            }
            Debug.Log("Does it have correct tag?: " + other.tag);
            if (other.CompareTag(correctTag))
            {
                onComplete.Invoke();
                attached = other.attachedRigidbody;
                Destroy(attached.GetComponent<DelayedDestruction>());
            }
        }

        void Update()
        {
            if (!attached)
            {
                return;
            }

            Vector3 direction = transform.position - attached.transform.position;
            
            attached.AddForce(direction * currentGravityForce, ForceMode.Acceleration);
            currentGravityForce += Time.deltaTime * increaseRate;
            currentGravityForce = Mathf.Min(currentGravityForce, maxForce);
        }
        
    }
}