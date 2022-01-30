using System;
using System.Collections.Generic;
using UnityEngine;

public class EasyMesh : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    void Init()
    {
        idCounter = 0;
    }

    private int idCounter = 0;
    public class Point
    {
        public int id;
        public float x;
        public float y;
        public float z;

        public Point(float x, float y, float z, int counter)
        {
            this.id = counter;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vector3(Point p) => new Vector3(p.x, p.y, p.z);
    }

    public class Square
    {
        public Point a, b, c, d;

        public Square(Point a, Point b, Point c, Point d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

    }

    private MeshFilter mf;
    private Mesh m;

    private List<Point> points = new List<Point>();
    private List<Square> squares = new List<Square>();

    public Vector3[] debugVertices;
    public int[] debugTriangles;

    public bool DebugViewVertices;

    void Awake()
    {
        mf = GetComponent<MeshFilter>();
        mf.mesh = m = new Mesh();
        
        GetComponent<MeshCollider>().sharedMesh = m;
    }
    public int CreateAndAddPoint(Vector3 position)
    {
        Point p = new Point(position.x, position.y, position.z, idCounter++);
        points.Add(p);

        return p.id;
    }
    
    public void AddSquare(int a, int b, int c, int d)
    {
        Square square = new Square(points[a], points[b], points[c], points[d]);
        squares.Add(square);
    }

    public void Clear()
    {
        m.Clear();
        this.points.Clear();
        this.squares.Clear();
        idCounter = 0;
    }
    public void Build()
    {
        Vector3[] verts = new Vector3[points.Count];
        Debug.Log("Creating for: " + points.Count + " points");
        for (var index = 0; index < points.Count; index++)
        {
            Point point = points[index];
            verts[index] = new Vector3(point.x, point.y, point.z);
        }
        
        // each square has two triangles, which each uses 3 vertices
        int[] triangles = new int[squares.Count * 2 * 3];

        int ti = 0;
        
        Debug.Log("Creating for: " + squares.Count + " squares");
        for (var index = 0; index < squares.Count; index++)
        {
            Square square = squares[index];
            triangles[ti++] = square.a.id;
            triangles[ti++] = square.b.id;
            triangles[ti++] = square.c.id;
            
            triangles[ti++] = square.c.id;
            triangles[ti++] = square.b.id;
            triangles[ti++] = square.d.id;
        }

        debugVertices = verts;
        m.vertices = verts;
        
        debugTriangles = triangles;
        m.triangles = triangles;
        m.Optimize();
        m.MarkModified();

        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshCollider>().enabled = true;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (DebugViewVertices)
        {
            Color c = Gizmos.color;
            Gizmos.color = Color.blue;
            foreach (Point point in points)
            {
                Gizmos.DrawCube(point, Vector3.one*0.15f);
            }

            Gizmos.color = c;
        }
    }
}