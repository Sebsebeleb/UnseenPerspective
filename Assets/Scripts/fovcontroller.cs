using UnityEngine;

namespace DefaultNamespace
{
    public class fovcontroller : MonoBehaviour
    {
        public float sensitivity = 0.3f;

        public float maxFov = 90;
        public float minFov = 30;
        void Update()
        {
            var delta = Input.mouseScrollDelta.y;

            Camera.main.fieldOfView -= delta * sensitivity;
            Camera.main.fieldOfView = Mathf.Max(Camera.main.fieldOfView, minFov);
            Camera.main.fieldOfView = Mathf.Min(Camera.main.fieldOfView, maxFov);
        }
    }
}