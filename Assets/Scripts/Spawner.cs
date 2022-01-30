using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;

    public class Spawner : MonoBehaviour
    {
        public GameObject prefab;
        public float lifeTime;
        public float spawnRate;

        private float lastSpawnedTime = -9999;

        private void Update()
        {
            if (Time.time - lastSpawnedTime > spawnRate)
            {
                var go = Instantiate(prefab, transform.position, transform.rotation);
                var dest = go.AddComponent<DelayedDestruction>();
                dest.lifetime = lifeTime;
                lastSpawnedTime = Time.time;
            }
        }
    }