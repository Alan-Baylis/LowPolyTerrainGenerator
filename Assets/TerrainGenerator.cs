using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour {
    public int width;
    public float heightScale;
    public float intensity;
    public float Offset;
    public float triangleScale;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.normals = GenerateNormals();
    }
    void Update () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.normals = GenerateNormals();

    }
    Vector3[] GenerateVertices()
    {
        Vector3[] Grid = new Vector3[width*width];
        int z=1;
        int x=1;
        for (int i = 0; i < Grid.Length; i++)
        {
            if (x < width)
            {
            Grid[i] = new Vector3(x*triangleScale, (int)GenerateHeight(x,z), z* triangleScale);
            x++;

            }
            else
            {
                Grid[i] = new Vector3(x* triangleScale, (int)GenerateHeight(x, z), z* triangleScale);
                x = 1;
                z++;
            }

        }
        return Grid;
    }
    Vector3[] GenerateNormals()
    {
      
        Vector3[] norm = new Vector3[width * width];
        int z = 1;
        int x = 1;
        for (int i = 0; i < norm.Length; i++)
        {
            if (x < width)
            {
                norm[i] = Vector3.up;
                x++;

            }
            else
            {
               
                norm[i] = Vector3.up;
                x = 1;
                z++;
            }

        }
        return norm;
    }
    int[] GenerateTriangles()
    {
        int[] triangles = new int[((((width-1) * (width - 1)) * 3) )* 2];
        int b = 0;
        int c = 0;
        int z = 0;
        for (int i = 0; i < (triangles.Length/2); i++)
        {
            if ((z + 1) % width == 0)
            {
                z++;
            }
                if (b == 0)
                {
                    triangles[i] = z;
                    b++;
                }
                else if (b == 1)
                {

                    triangles[i] = z + width;
                    b++;
                }
                else if (b == 2)
                {
                    triangles[i] = z + 1;
                    b = 0;
                    z++;
                }

        }
         b = 0;
         z = 1;
        for (int i = triangles.Length / 2; i < triangles.Length; i++)
        {
            if (z % width == 0)
            {
                z++;
            }
            if (b == 0)
            {
                triangles[i] = (z + width) - 1;


                b++;
            }
            else if (b == 1)
            {

                triangles[i] = z + width;
                b++;
            }
            else if (b == 2)
            {
                triangles[i] = z;
                b = 0;
                z++;
            }
        }
        return triangles;
    }
    float GenerateHeight(int x, int z)
    {
        float xCoord = (float)x / width*intensity+Offset;
        float zCoord = (float)z / width*intensity+Offset*2;
        float y = Mathf.PerlinNoise(xCoord, zCoord)*heightScale;
        return y;
    }
}
