using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private MeshFilter f;
    private MeshRenderer r;
    private Mesh m;

    void Awake()
    {
        f = GetComponent<MeshFilter>();
        r = GetComponent<MeshRenderer>();

        m = new Mesh();
        m.MarkDynamic();
        f.mesh = m;
    }

    void Start()
    {
        int points = 4;
        Vector3[] vertices = new Vector3[points];
        Vector2[] uvs = new Vector2[points];

        f.mesh.Clear();

        List<Vector3> vPs = new List<Vector3>
        {
            new (0, 0, 0),
            new (1, 0, 0),
            new (1, 1, 0),
            new (0, 1, 0),
        };
        int i = 0;
        foreach (var p in vPs)
        {
            int vi = i;
            vertices[vi] = p;
            i++;
        }


        m.SetVertices(vertices);
        
        // for each 4 vertices use 0,1,2, 0,2,3
        setupTriangles(points);

        for (int u = 0; u < uvs.Length; u++)
        {
            uvs[u] = new Vector2(0, 0);
        }

        m.SetUVs(0, uvs);
        m.MarkModified();
    }

    private void setupTriangles(int points)
    {
        int[] triangles = new int[points * 3];
        for (int t = 0; t < points; t++)
        {
            int ti = t * 3;
            int vi = t;
            triangles[ti + 0] = vi;
            triangles[ti + 1] = vi + 1;
            triangles[ti + 2] = vi + 2;
        }

        m.SetTriangles(triangles, 0);
    }

    private void Update()
    {
        /*if (vine.points.Count > lastNumberOfPoints && vine.points.Count > 1)
        {
            lastNumberOfPoints = vine.points.Count;

            mf.mesh.Clear();
            Vector3[] vertices = new Vector3[vine.points.Count * 2];
            int[] triangles = new int[(vine.points.Count * 2 - 2) * 6];
            Vector2[] uvs = new Vector2[vine.points.Count * 2];
            int i = 0;
            foreach (var p in vine.points)
            {
                int vi = i * 2;
                Vector3 direction;
                if (i == vine.points.Count - 1)
                {
                    direction = vine.points[i - 1].position - p.position;
                }
                else
                {
                    direction = vine.points[i + 1].position - p.position;
                }

                var rotation = Quaternion.AngleAxis(-90, direction);
                vertices[vi] = p.position + p.normal.normalized * 0.06f + (rotation * p.position).normalized * 0.1f;
                //vertices[vi] = p.position;
                //vertices[vi+1] = p.position;

                rotation = Quaternion.AngleAxis(90, direction);
                vertices[vi + 1] = p.position + p.normal.normalized * 0.06f + (rotation * p.position).normalized * 0.1f;

                i++;
            }

            // for each 4 vertices use 0,1,2, 0,2,3
            for (int t = 0; t < vine.points.Count - 1; t++)
            {
                int ti = t * 6;
                int vi = t * 2;
                triangles[ti + 0] = vi;
                triangles[ti + 1] = vi + 1;
                triangles[ti + 2] = vi + 2;

                triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = vi + 3;
                triangles[ti + 5] = vi + 2;
            }

            mf.mesh.SetVertices(vertices);
            mf.mesh.SetTriangles(triangles, 0);

            for (int u = 0; u < uvs.Length; u++)
            {
                uvs[u] = new Vector2(0, 0);
            }

            mf.mesh.SetUVs(0, uvs);

            mf.mesh.MarkModified();
        }*/
    }
}