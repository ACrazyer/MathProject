using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixPlane : MonoBehaviour
{
    Vector2[][] basePlane;
    Vector2[][] outputPlane;
    Vector2[][] targetPlane;

    //原点的下标
    Vector2Int originIndex;
    public Material lineMat;

    public void InitPlane(int size)
    {
        basePlane = GenerateBasePlane(size);
        outputPlane = GenerateBasePlane(size);//ConvertPlane(new Matrix2x2(new Vector2(1, -2), new Vector2(3, 0)), basePlane);
        targetPlane = GenerateBasePlane(size);
        originIndex = new Vector2Int(size / 2, size / 2);
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
        int size = plane.Length;
        for (int i = 0; i < size; i++)
        {
            GL.Vertex(plane[i][0]);
            GL.Vertex(plane[i][size - 1]);
            GL.Vertex(plane[0][i]);
            GL.Vertex(plane[size - 1][i]);
        }
        GL.Color(Color.green);
        GL.Vertex(plane[originIndex.x][originIndex.y]);
        GL.Vertex(plane[originIndex.x+1][originIndex.y]);
        GL.Color(Color.red);
        GL.Vertex(plane[originIndex.x][originIndex.y]);
        GL.Vertex(plane[originIndex.x][originIndex.y+1]);
        GL.End();
    }

    void OnPostRender()
    {
        DrawPlane(basePlane,Color.white);
        DrawPlane(outputPlane, Color.blue);
    }

    void Update()
    {
        int size = outputPlane.Length;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                outputPlane[i][j] = Vector2.Lerp(outputPlane[i][j],targetPlane[i][j],Time.deltaTime);
            }
        }
    }
}
