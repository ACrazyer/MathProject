using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixPlane : MonoBehaviour
{
    private int _size = 5;
    Vector2[][] basePlane;
    Vector2[][] outputPlane;
    Vector2[][] targetPlane;

    //原点的下标
    Vector2 originIndex;
    public Material lineMat;

    // Start is called before the first frame update
    void Awake()
    {
        basePlane = GenerateBasePlane(_size);
        outputPlane = GenerateBasePlane(_size);//ConvertPlane(new Matrix2x2(new Vector2(1, -2), new Vector2(3, 0)), basePlane);
        targetPlane = GenerateBasePlane(_size);
        originIndex = new Vector2(_size / 2, _size / 2);
    }
    private void Start()
    {
        //LerpConvert(new Matrix2x2(new Vector2(1, -2), new Vector2(3, 0)));
    }

    public void LerpConvert(Matrix2x2 matrix)
    {
        for (int i = 0; i < basePlane.Length; i++)
        {
            for (int j = 0; j < basePlane[i].Length; j++)
            {
                targetPlane[i][j] = matrix * basePlane[i][j];
            }
        }
    }


    private Vector2[][] ConvertPlane(Matrix2x2 matrix,Vector2[][] basePlane)
    {
        Vector2[][] output = GenerateBasePlane(basePlane.Length);
        for (int i = 0; i < basePlane.Length; i++)
        {
            for(int j = 0; j < basePlane[i].Length; j++)
            {
                output[i][j] = matrix*basePlane[i][j];
            }
        }
        return output;
    }

    private Vector2[][] GenerateBasePlane(int size)
    {
        Vector2[][] basePlane = new Vector2[size][];
        for (int i = 0; i < size; i++)
        {
            basePlane[i] = new Vector2[size];
            for (int j = 0; j < size; j++)
            {
                basePlane[i][j] = new Vector2(i - size / 2,j - size / 2);
            }
        }
        return basePlane;
    }


    private void DrawPlane(Vector2[][] plane,Color color)
    {
        GL.Begin(GL.LINES);
        GL.Color(color);
        lineMat.SetPass(0);
        for (int i = 0; i < plane.Length; i++)
        {
            GL.Vertex(plane[i][0]);
            GL.Vertex(plane[i][_size-1]);
            GL.Vertex(plane[0][i]);
            GL.Vertex(plane[_size-1][i]);
        }
        GL.End();

    }

    void OnPostRender()
    {
        DrawPlane(basePlane,Color.black);
        DrawPlane(outputPlane, Color.green);
    }

    void Update()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                outputPlane[i][j] = Vector2.Lerp(outputPlane[i][j],targetPlane[i][j],Time.deltaTime*0.5f);
            }
        }
    }
}
