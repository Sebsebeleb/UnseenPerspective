using UnityEngine;

namespace DefaultNamespace
{
    public class DelayedDestruction : MonoBehaviour
    {
        public float lifetime = 9999;

        void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}