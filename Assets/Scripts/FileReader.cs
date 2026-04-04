using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class FileReader
{
    // Start is called before the first frame update

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> faces = new List<int>();
    private Vector3 centro;
    private float minx;
    private float miny;
    private float minz;
    private float maxx;
    private float maxy;
    private float maxz;

    public void ReadFile(string fileName)
    {
        string path = "Assets/Models3d/" + fileName + ".obj";
        StreamReader reader = new StreamReader(path);
        string fileData = (reader.ReadToEnd());
        ReadEachLine(fileData);
        reader.Close();
        Debug.Log(fileData);
    }

    private void ReadEachLine(string fileData)
    {
        string[] lines = fileData.Split('\n');
        bool boundsInitialized = false;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (lines[i].StartsWith("v "))
            {
                string[] parts = line.Split(' ');
                float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                float z = float.Parse(parts[3], CultureInfo.InvariantCulture);
                vertices.Add(new Vector3(x, y, z));
                if (!boundsInitialized)
                {
                    minx = maxx = x;
                    miny = maxy = y;
                    minz = maxz = z;
                    boundsInitialized = true;
                }
                else
                {
                    if (x < minx) minx = x;
                    if (y < miny) miny = y;
                    if (z < minz) minz = z;
                    if (x > maxx) maxx = x;
                    if (y > maxy) maxy = y;
                    if (z > maxz) maxz = z;
                }
                
            }
            else if (lines[i].StartsWith("f "))
            {
                string[] parts = line.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

                for (int k = 1; k < parts.Length; k++)
                {
                    Debug.Log(parts[k]);
                    string index = parts[k].Split('/')[0]; // por si viene con textura/normal
                    Debug.Log(index);
                    if (int.TryParse(index, out int value))
                    {
                        faces.Add(value - 1);
                        Debug.Log("valor: "+value);

                    }
                    else
                    {
                        Debug.LogWarning("No se pudo parsear: " + index);
                    }
                }
            }
        }

        // Calcular el centro del bounding box
        centro = new Vector3(
            (maxx + minx) * 0.5f,
            (maxy + miny) * 0.5f,
            (maxz + minz) * 0.5f
        );

        // Restar el centro a cada vértice para centrar el modelo
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 v = vertices[i];
            v = new Vector3(v.x - centro.x, v.y - centro.y, v.z - centro.z);
            vertices[i] = v;
        }
    }




    public Vector3[] GetVertexes()
    {
        return vertices.ToArray();
    }

    public int[] GetFaces()
    {
        return faces.ToArray();
    }

    public void GetColores(){

    }
}