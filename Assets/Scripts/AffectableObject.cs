using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AffectableObject : MonoBehaviour
{

    public static List<AffectableObject> visibleAffectables = new List<AffectableObject>();
    public Renderer r;
    public MeshFilter f;

    private bool oldVisible = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Init()
    {
        visibleAffectables = new List<AffectableObject>();
    }

    void Awake()
    {
        r = GetComponent<Renderer>();
        f = GetComponent<MeshFilter>();
    }
    void Update()
    {
        bool isVisible = r.isVisible;

        if (isVisible != oldVisible)
        {
            if (isVisible)
            {
                visibleAffectables.Add(this);
            }
            else
            {
                visibleAffectables.Remove(this);
            }
            oldVisible = isVisible;
        }
    }
}