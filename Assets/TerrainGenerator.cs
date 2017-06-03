using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width;
    public float heightScale;
    public float intensity;
    public float xOffset, zOffset;
    public float triangleScale;
    int[] triangles;
    Vector3[] Norm;
    public bool RoundHeight;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = GenerateVerticies();
        mesh.normals = Norm;
        mesh.triangles = triangles;
    }
    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = GenerateVerticies();
        mesh.normals = Norm;
        mesh.triangles = triangles;
    }
    float GenerateHeight(int x, int z)
    {
        float xCoord = (float)x / width * intensity + xOffset;
        float zCoord = (float)z / width * intensity + zOffset;
        float y = Mathf.PerlinNoise(xCoord, zCoord) * heightScale;
        if (RoundHeight)
            return Mathf.RoundToInt(y);
        else
            return y;
    }
    Vector3[] GenerateVerticies()
    {
        triangles = new int[width * width * 6]; //each square has 6 verticies and triangle points
        Vector3[] Grid = new Vector3[triangles.Length];
        Norm = new Vector3[triangles.Length];
        int TriangleIteration = 0;
        int x = 0;
        int y = 0;
        for (int VertexIndex = 0; VertexIndex < triangles.Length / 2; VertexIndex++) //For each iteration sets another point of triangle
        {
            if (TriangleIteration == 0)
            {
                Grid[VertexIndex] = new Vector3(x, GenerateHeight(x, y), y);
                triangles[VertexIndex] = VertexIndex;
                Norm[VertexIndex] = Vector3.up;

            }
            if (TriangleIteration == 1)
            {
                Grid[VertexIndex] = new Vector3(x, GenerateHeight(x, y + 1), y + 1);
                triangles[VertexIndex] = VertexIndex;
                Norm[VertexIndex] = Vector3.up;

            }
            if (TriangleIteration == 2)
            {
                Grid[VertexIndex] = new Vector3(x + 1, GenerateHeight(x + 1, y), y);
                triangles[VertexIndex] = VertexIndex;
                Norm[VertexIndex] = Vector3.up;
                TriangleIteration = 0;
                if (x != width - 1)
                {
                    x++;
                }
                else
                {
                    x = 0;
                    y++;
                }
            }
            else
            {
                TriangleIteration++;
            }

        }
        x = 1;
        y = 0;
        TriangleIteration = 0;
        for (int VertexIndex = triangles.Length / 2; VertexIndex < triangles.Length; VertexIndex++)
        {
            if (TriangleIteration == 0)
            {
                Grid[VertexIndex] = new Vector3(x, GenerateHeight(x, y), y);
                triangles[VertexIndex] = VertexIndex;
                Norm[VertexIndex] = Vector3.up;

            }
            if (TriangleIteration == 1)
            {
                Grid[VertexIndex] = new Vector3(x - 1, GenerateHeight(x - 1, y + 1), y + 1);
                triangles[VertexIndex] = VertexIndex;
                Norm[VertexIndex] = Vector3.up;

            }
            if (TriangleIteration == 2)
            {
                Grid[VertexIndex] = new Vector3(x, GenerateHeight(x, y + 1), y + 1);
                triangles[VertexIndex] = VertexIndex;
                Norm[VertexIndex] = Vector3.up;
                TriangleIteration = 0;
                if (x != width)
                {
                    x++;
                }
                else
                {
                    x = 1;
                    y++;
                }
            }
            else
            {
                TriangleIteration++;
            }
        }
        return Grid;
    }
}
