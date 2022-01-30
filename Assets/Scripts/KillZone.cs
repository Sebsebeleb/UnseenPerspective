using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class KillZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.position = new Vector3(0, 2, 0);
            }
        }
    }
}