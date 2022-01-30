using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Viewer : MonoBehaviour
{
    // Update is called once per frame

    public bool always;

    private List<Vector3> debugPoints = new List<Vector3>();

    public EasyMesh easy, easyRigth;

    public LayerMask layerMask;

    public Animator effectAnimator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || always)
        {
            Doit(easy);
            effectAnimator.SetTrigger("Play");
        }
        
        if (Input.GetMouseButtonDown(1) || always)
        {
            Doit(easyRigth);
            effectAnimator.SetTrigger("Play");
        }
    }

    void Doit(EasyMesh e)
    {
        e.Clear();
        debugPoints.Clear();
        foreach (var affectable in AffectableObject.visibleAffectables)
        {
            Debug.Log("Updating for: " + affectable.gameObject);
            var bounds = affectable.r.bounds;

            var p1 = bounds.min;
            var p2 = bounds.max;


            /*debugPoints.Add(p1);
            debugPoints.Add(p2);*/


            foreach (Vector3 v in affectable.f.mesh.vertices)
            {
                debugPoints.Add(affectable.transform.TransformPoint(v));
            }

            // First find the "interesting" points (the only ones that are not blocked)

            var verts = affectable.f.mesh.vertices.Select(v => affectable.transform.TransformPoint(v));
            var validVerts = new List<Vector3>();

            // Find valid verts
            foreach (Vector3 vert in verts)
            {
                var ray = new Ray(transform.position, vert - transform.position);
                RaycastHit hitInfo;
                bool b = Physics.Raycast(ray, out hitInfo, 9999, layerMask);

                if (b)
                {
                    float TRESHHOLD = 0.04f;
                    if (Vector3.Distance(vert, hitInfo.point) < TRESHHOLD)
                    {
                        b = false;
                    }
                }

                if (!b)
                {
                    validVerts.Add(vert);
                }
            }
            
            Debug.Log("Valid verts: " + verts);

            foreach (Vector3 validVert in validVerts)
            {
                e.CreateAndAddPoint(validVert);
            }

            
            int i = 0;
            int numVert = validVerts.Count;
            foreach (Vector3 v in validVerts)
            {
                Debug.Log($"Raycasting from : {transform.position} to {v - transform.position}");
                RaycastHit[] hits = new RaycastHit[40];
                int numHits = Physics.RaycastNonAlloc(transform.position, v - transform.position, hits, 99999, layerMask);

                Debug.Log("This many hits: " + numHits);
                
                var hit = hits.OrderBy(h => h.distance).First(h => h.transform != null && h.transform != affectable.transform);
                if (hit.transform == null)
                {
                    Debug.Log("No :(");
                }
                else
                {
                    Debug.Log("hit object: " + hit.collider.gameObject, hit.collider);
                }
                Vector3 pos = hit.point;

                if (hit.transform != null)
                {
                    e.CreateAndAddPoint(pos);
                }

                if (i != 0)
                {
                    e.AddSquare(i-1, numVert+i-1, i, numVert+i);
                }

                i++;
            }

        }
        
        //e.AddSquare(0, 1, 2, 3);
        
        e.Build();
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || true)
        {
            foreach (var aff in AffectableObject.visibleAffectables)
            {
                Gizmos.DrawLine(transform.position, aff.transform.position);
            }
        }

        Color color = Gizmos.color;
        foreach (var point in debugPoints)
        {
            //var ray = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f));
            var ray = new Ray(transform.position, point - transform.position);
            RaycastHit hitInfo;
            bool b = Physics.Raycast(ray, out hitInfo);


            if (b)
            {
                float TRESHHOLD = 0.01f;
                Gizmos.DrawLine(point, hitInfo.point - transform.position);
                if (Vector3.Distance(point, hitInfo.point) < TRESHHOLD)
                {
                    b = false;
                }
            }

            if (b)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;

            if (b)
            {
                Gizmos.DrawSphere(hitInfo.point, 0.09f);
            }

            Gizmos.DrawWireCube(point, Vector3.one * 0.2f);
            Gizmos.DrawRay(transform.position, point - transform.position);
        }

        Gizmos.color = color;
    }
}