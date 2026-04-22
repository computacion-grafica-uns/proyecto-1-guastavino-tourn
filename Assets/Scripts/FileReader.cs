using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class FileReader
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> faces = new List<int>();
    private List<Color> faceColors = new List<Color>();

    private Color currentColor = Color.white;
    private float minx, miny, minz;
    private float maxx, maxy, maxz;

    public void ReadFile(string fileName)
    {
        string path = "Assets/Objetos/" + fileName + ".obj";
        StreamReader reader = new StreamReader(path);
        string fileData = reader.ReadToEnd();
        reader.Close();
        ReadEachLine(fileData);
    }

    private void ReadEachLine(string fileData)
    {
        List<Vector3> rawVertices = new List<Vector3>();
        bool boundsInitialized = false;
        string[] lines = fileData.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

             if (line.StartsWith("v "))
            {
                string[] parts = line.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
                float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                float z = float.Parse(parts[3], CultureInfo.InvariantCulture);

                if (!boundsInitialized)
                {
                    minx = maxx = x;
                    miny = maxy = y;
                    minz = maxz = z;
                    boundsInitialized = true;
                }
                else
                {
                    if (x < minx) minx = x; if (x > maxx) maxx = x;
                    if (y < miny) miny = y; if (y > maxy) maxy = y;
                    if (z < minz) minz = z; if (z > maxz) maxz = z;
                }

                rawVertices.Add(new Vector3(x, y, z));
            }
            else if (line.StartsWith("f "))
            {
                string[] parts = line.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
                List<int> vertIndices = new List<int>();

                for (int k = 1; k < parts.Length; k++)
                {
                    string[] split = parts[k].Split('/');
                    if (int.TryParse(split[0], out int vIdx))
                        vertIndices.Add(vIdx - 1);
                }

                for (int k = 1; k < vertIndices.Count - 1; k++)
                {
                    int nextIndex = vertices.Count;

                    vertices.Add(rawVertices[vertIndices[0]]);
                    vertices.Add(rawVertices[vertIndices[k]]);
                    vertices.Add(rawVertices[vertIndices[k + 1]]);

                    faces.Add(nextIndex);
                    faces.Add(nextIndex + 1);
                    faces.Add(nextIndex + 2);

                    faceColors.Add(currentColor);
                    faceColors.Add(currentColor);
                    faceColors.Add(currentColor);
                }
            }
        }

        Vector3 centro = new Vector3(
            (maxx + minx) * 0.5f,
            (maxy + miny) * 0.5f,
            (maxz + minz) * 0.5f
        );
        for (int i = 0; i < vertices.Count; i++)
            vertices[i] -= centro;
    }

    public Vector3[] GetVertexes() => vertices.ToArray();
    public int[] GetFaces() => faces.ToArray();
    public Vector3 GetSize() => new Vector3(maxx - minx, maxy - miny, maxz - minz);
    public Vector3 GetHalfExtents() => GetSize() * 0.5f;
}