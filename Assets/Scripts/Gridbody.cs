using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Gridbody : MonoBehaviour
{

    private List<Gridbody> grids = new List<Gridbody>();
    public Transform fake;
    public bool active;

    public LayerMask fakeLayer;

    private void Start()
    {
        var fakeGo = Instantiate(gameObject);
        Destroy(fakeGo.GetComponent<Gridbody>());

        fake = fakeGo.transform;
        fakeGo.layer = 6;
        GetComponent<Rigidbody>().isKinematic = true;
        Activate();
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    void Update()
    {
        if (active)
        {
            var pos = fake.transform.position;
            transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
        }
    }
}
