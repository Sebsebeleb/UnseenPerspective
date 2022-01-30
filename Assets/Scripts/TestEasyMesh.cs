using System;
using System.Collections.Generic;
using UnityEngine;

public class TestEasyMesh : MonoBehaviour
{
    public EasyMesh easy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 a = new Vector3(0, 0, 0);
            Vector3 b = new Vector3(0, 1, 0);
            Vector3 c = new Vector3(1, 1, 0);
            Vector3 d = new Vector3(1, 0, 0);
            
            Vector3 e = new Vector3(0, 0, 1);
            Vector3 f = new Vector3(0, 1, 1);
            Vector3 g = new Vector3(1, 1, 1);
            Vector3 h = new Vector3(1, 0, 1);
            
            easy.CreateAndAddPoint(a);
            easy.CreateAndAddPoint(b);
            easy.CreateAndAddPoint(c);
            easy.CreateAndAddPoint(d);
            easy.CreateAndAddPoint(e);
            easy.CreateAndAddPoint(f);
            easy.CreateAndAddPoint(g);
            easy.CreateAndAddPoint(h);
            easy.AddSquare(0, 1, 2, 3);
            easy.AddSquare(4, 5, 6, 7);
            easy.AddSquare(0, 1, 4, 5);
            easy.AddSquare(1, 2, 5, 6);
            easy.Build();
        }
    }
}