using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class FileReader
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> faces = new List<int>();
    private List<Color> faceColors = new List<Color>();
    private List<Vector2> uvList = new List<Vector2>();
    private List<Vector2> rawUVs = new List<Vector2>();

    private Dictionary<string, Color> materials = new Dictionary<string, Color>();
    private Color currentColor = Color.white;

    private Vector3 centro;
    private float minx, miny, minz;
    private float maxx, maxy, maxz;

    public void ReadFile(string fileName)
    {
        string path = "Assets/Objetos/" + fileName + ".obj";
        StreamReader reader = new StreamReader(path);
        string fileData = reader.ReadToEnd();
        reader.Close();

        string folder = Path.GetDirectoryName(path).Replace("\\", "/") + "/";
        Debug.Log("Folder: " + folder);
        ReadEachLine(fileData, folder);
    }

    private void ReadMtlFile(string mtlPath)
    {
        if (!File.Exists(mtlPath))
        {
            Debug.LogWarning("No se encontró el .mtl en: " + mtlPath);
            return;
        }

        string[] lines = File.ReadAllLines(mtlPath);
        string currentMaterial = "";

        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (trimmed.StartsWith("newmtl "))
            {
                currentMaterial = trimmed.Substring(7).Trim();
            }
            else if (trimmed.StartsWith("Kd ") && currentMaterial != "")
            {
                string[] parts = trimmed.Split(' ');
                float r = float.Parse(parts[1], CultureInfo.InvariantCulture);
                float g = float.Parse(parts[2], CultureInfo.InvariantCulture);
                float b = float.Parse(parts[3], CultureInfo.InvariantCulture);
                materials[currentMaterial] = new Color(r, g, b);
            }
        }
    }

    private void ReadEachLine(string fileData, string folder)
    {
        List<Vector3> rawVertices = new List<Vector3>();
        string[] lines = fileData.Split('\n');
        bool boundsInitialized = false;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (line.StartsWith("mtllib "))
            {
                string mtlFile = line.Substring(7).Trim();
                ReadMtlFile(folder + mtlFile);
            }
            else if (line.StartsWith("usemtl "))
            {
                string matName = line.Substring(7).Trim();
                currentColor = materials.ContainsKey(matName) ? materials[matName] : Color.white;
            }
            else if (line.StartsWith("vt "))
            {
                string[] parts = line.Split(' ');
                float u = float.Parse(parts[1], CultureInfo.InvariantCulture);
                float v = float.Parse(parts[2], CultureInfo.InvariantCulture);
                rawUVs.Add(new Vector2(u, v));
            }
            else if (line.StartsWith("v "))
            {
                string[] parts = line.Split(' ');
                float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                float z = float.Parse(parts[3], CultureInfo.InvariantCulture);

                if (!boundsInitialized)
                {
                    minx = maxx = x; miny = maxy = y; minz = maxz = z;
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
                List<int> uvIndices = new List<int>();

                for (int k = 1; k < parts.Length; k++)
                {
                    string[] split = parts[k].Split('/');
                    if (int.TryParse(split[0], out int vIdx))
                        vertIndices.Add(vIdx - 1);
                    if (split.Length > 1 && int.TryParse(split[1], out int uvIdx))
                        uvIndices.Add(uvIdx - 1);
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

                    if (uvIndices.Count > 0)
                    {
                        uvList.Add(rawUVs[uvIndices[0]]);
                        uvList.Add(rawUVs[uvIndices[k]]);
                        uvList.Add(rawUVs[uvIndices[k + 1]]);
                    }

                    faceColors.Add(currentColor);
                    faceColors.Add(currentColor);
                    faceColors.Add(currentColor);
                }
            }
        }

        centro = new Vector3(
            (maxx + minx) * 0.5f,
            (maxy + miny) * 0.5f,
            (maxz + minz) * 0.5f
        );

        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 v = vertices[i];
            vertices[i] = new Vector3(v.x - centro.x, v.y - centro.y, v.z - centro.z);
        }
    }

    public Vector3[] GetVertexes() => vertices.ToArray();
    public int[] GetFaces() => faces.ToArray();
    public Color[] GetColors() => faceColors.ToArray();
    public Vector2[] GetUVs() => uvList.ToArray();
    public Vector3 GetCenter() => centro;
    public Vector3 GetSize() => new Vector3(maxx - minx, maxy - miny, maxz - minz);
    public Vector3 GetHalfExtents() => GetSize() * 0.5f;
}