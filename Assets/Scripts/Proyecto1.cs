using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class Projecto1 : MonoBehaviour
{
    private GameObject myCamera;
    private int width = 10;
    private int height = 3;
    private int depth = 10;
    private float wallThickness = 0.15f;

    void Start()
    {
        createFloor();
        createCeiling();
        createWall1();
        createCamera();
        cargaMuebles();
    }

    private void cargaMuebles()
    {
        var (bed, bedHalf) = loadObject(
            "muebles/beds/bed1/bed1",
            "Bed1",
            "bed1Texture"
        );
        changeposition(
            bed,
            new Vector3(width - wallThickness - bedHalf.x, bedHalf.y, depth - wallThickness - bedHalf.z),
            new Vector3(0,0,0),
            new Vector3(1,1,1)
        );

        Debug.Log($"bedHalf: {bedHalf.x}, {bedHalf.y}, {bedHalf.z}");

        var (toilet, toiletHalf) = loadObject(
            "muebles/Bathroom/toilets/toilet1/toilet1",
            "toilet1",
            "toilet1Texture"
        );
        changeposition(
            toilet,
            new Vector3(toiletHalf.x, toiletHalf.y, toiletHalf.z),
            new Vector3(0,0,0),
            new Vector3(1,1,1)
        );
    }

    private void createFloor()
    {
        Vector3[] floorVertices;
        int[] floorFaces;
        GameObject floor;
        floor = new GameObject("Floor");
        floor.AddComponent<MeshFilter>();
        floor.GetComponent<MeshFilter>().mesh = new Mesh();
        floor.AddComponent<MeshRenderer>();
        floorVertices = new Vector3[]
        {
            // Triangle 1
            new Vector3(0,0,0),
            new Vector3(0,0,depth),
            new Vector3(width,0,0),
            

            // Triangle 2
            new Vector3(0,0,depth),
            new Vector3(width,0,depth),
            new Vector3(width,0,0),

        };
    
        floorFaces = new int[] {
            0,1,2,
            3,4,5,
        };

        

        Color[] floorColors = new Color[floorVertices.Length];
        for (int i = 0; i < floorColors.Length; i++)
        {
            floorColors[i] = floorColors[i] = new Color(237f / 255f, 195f / 255f, 133f / 255f);
        }

        UpdateMesh(floor,floorVertices,floorFaces, floorColors);
        
        floor.GetComponent<MeshRenderer>().material = new Material(Shader.Find("FloorShader"));
        Renderer r = floor.GetComponent<Renderer>();
        r.material.SetFloat("_PlankWidth",   0.2f);
        r.material.SetFloat("_PlankLength",  1.0f);
        r.material.SetColor("_WoodLight",    new Color(0.76f, 0.60f, 0.42f));
        r.material.SetColor("_WoodDark",     new Color(0.45f, 0.30f, 0.18f));
        r.material.SetFloat("_VeinStrength", 0.3f);
        r.material.SetFloat("_GapSize",      0.01f);
    }

    private void createCeiling()
    {
        Vector3[] ceilingVertices;
        int[] ceilingFaces;
        GameObject ceiling;
        ceiling = new GameObject("Ceiling");
        ceiling.AddComponent<MeshFilter>();
        ceiling.GetComponent<MeshFilter>().mesh = new Mesh();
        ceiling.AddComponent<MeshRenderer>();
        ceilingVertices = new Vector3[]
        {
            // Triangle 1
            new Vector3(0,height,0),
            new Vector3(0,height,depth),
            new Vector3(width,height,0),
            

            // Triangle 2
            new Vector3(0,height,depth),
            new Vector3(width,height,depth),
            new Vector3(width,height,0),

        };
    
        ceilingFaces = new int[] {
            0,1,2,
            3,4,5,
        };

        Color[] ceilingColors = new Color[ceilingVertices.Length];
        for (int i = 0; i < ceilingColors.Length; i++)
        {
            ceilingColors[i] = ceilingColors[i] = new Color(243f / 255f, 243f / 255f, 243f / 255f);
        }
        UpdateMesh(ceiling,ceilingVertices,ceilingFaces, ceilingColors);
    }
    private void createWall1()
    {
        Vector3[] wall1Vertices;
        int[] wall1Faces;
        GameObject wall1;
        wall1 = new GameObject("Wall1");
        wall1.AddComponent<MeshFilter>();
        wall1.GetComponent<MeshFilter>().mesh = new Mesh();
        wall1.AddComponent<MeshRenderer>();
        wall1Vertices = new Vector3[]
        {
            //Afuera

            // Triangle 1
            new Vector3(0,0,0),
            new Vector3(0,height,0),
            new Vector3(4.6f,height,0),
            
            // Triangle 2
            new Vector3(0,0,0),
            new Vector3(4.6f,height,0),
            new Vector3(4.6f,0,0),

            // Triangle 3
            new Vector3(4.6f,2,0),
            new Vector3(4.6f,height,0),
            new Vector3(5.4f,height,0),

            // Triangle 4
            new Vector3(5.4f,2,0),
            new Vector3(4.6f,2,0),
            new Vector3(5.4f,height,0),
            
            // Triangle 5
            new Vector3(5.4f,height,0),
            new Vector3(5.9f,height,0),
            new Vector3(5.4f,0,0),

            // Triangle 6
            new Vector3(5.9f,0,0),
            new Vector3(5.4f,0,0),
            new Vector3(5.9f,height,0),

            // Triangle 7
            new Vector3(5.9f,height,0),
            new Vector3(7.4f,height,0),
            new Vector3(5.9f,2,0),

            // Triangle 8
            new Vector3(5.9f,2,0),
            new Vector3(7.4f,height,0),
            new Vector3(7.4f,2,0),

            // Triangle 9
            new Vector3(5.9f,0,0),
            new Vector3(5.9f,1,0),
            new Vector3(7.4f,1,0),

            // Triangle 10
            new Vector3(5.9f,0,0),
            new Vector3(7.4f,1,0),
            new Vector3(7.4f,0,0),

            // Triangle 11
            new Vector3(7.4f,0,0),
            new Vector3(7.4f,height,0),
            new Vector3(width,height,0),

            // Triangle 12
            new Vector3(width,height,0),
            new Vector3(width,0,0),
            new Vector3(7.4f,0,0),

            //Adentro

            // Triangle 
            new Vector3(width-wallThickness,0,wallThickness),
            new Vector3(width-wallThickness,height,wallThickness),
            new Vector3(7.4f,0,wallThickness),

            // Triangle 
            new Vector3(7.4f,0,wallThickness),
            new Vector3(width - wallThickness,height,wallThickness),
            new Vector3(7.4f,height,wallThickness),

            // Triangle 
            new Vector3(7.4f,0,wallThickness),
            new Vector3(7.4f,1,wallThickness),
            new Vector3(5.9f,0,wallThickness),

            // Triangle 
            new Vector3(5.9f,0,wallThickness),
            new Vector3(7.4f,1,wallThickness),
            new Vector3(5.9f,1,wallThickness),

            // Triangle 
            new Vector3(7.4f,2,wallThickness),
            new Vector3(7.4f,height,wallThickness),
            new Vector3(5.9f,2,wallThickness),

            // Triangle 
            new Vector3(5.9f,2,wallThickness),
            new Vector3(7.4f,height,wallThickness),
            new Vector3(5.9f,height,wallThickness),

            // Triangle 
            new Vector3(5.4f,0,wallThickness),
            new Vector3(5.9f,0,wallThickness),
            new Vector3(5.9f,height,wallThickness),

            // Triangle 
            new Vector3(5.4f,0,wallThickness),
            new Vector3(5.9f,height,wallThickness),
            new Vector3(5.4f,height,wallThickness),

            // Triangle 
            new Vector3(4.6f,2,wallThickness),
            new Vector3(5.4f,2,wallThickness),
            new Vector3(5.4f,height,wallThickness),

            // Triangle 
            new Vector3(5.4f,height,wallThickness),
            new Vector3(4.6f,height,wallThickness),
            new Vector3(4.6f,2,wallThickness),

            // Triangle 
            new Vector3(0 + wallThickness,0,wallThickness),
            new Vector3(4.6f,0,wallThickness),
            new Vector3(4.6f,height,wallThickness),

            // Triangle 
            new Vector3(4.6f,height,wallThickness),
            new Vector3(0 + wallThickness,height,wallThickness),
            new Vector3(0 + wallThickness,0,wallThickness),

            // Bordes ventana

            new Vector3(5.9f,1,0),
            new Vector3(5.9f,1,wallThickness),
            new Vector3(7.4f,1,0),

            new Vector3(5.9f,1,wallThickness),
            new Vector3(7.4f,1,wallThickness),
            new Vector3(7.4f,1,0),

            new Vector3(5.9f,1,0),
            new Vector3(5.9f,2,0),
            new Vector3(5.9f,2,wallThickness),

            new Vector3(5.9f,1,0),
            new Vector3(5.9f,2,wallThickness),
            new Vector3(5.9f,1,wallThickness),
            
            new Vector3(5.9f,2,0),
            new Vector3(7.4f,2,wallThickness),
            new Vector3(5.9f,2,wallThickness),

            new Vector3(5.9f,2,0),
            new Vector3(7.4f,2,0),
            new Vector3(7.4f,2,wallThickness),

            new Vector3(7.4f,1,0),
            new Vector3(7.4f,2,wallThickness),
            new Vector3(7.4f,2,0),

            new Vector3(7.4f,1,0),
            new Vector3(7.4f,1,wallThickness),
            new Vector3(7.4f,2,wallThickness),

            //Bordes puerta

            new Vector3(4.6f,0,0),
            new Vector3(4.6f,2,0),
            new Vector3(4.6f,0,wallThickness),

            new Vector3(4.6f,0,wallThickness),
            new Vector3(4.6f,2,0),
            new Vector3(4.6f,2,wallThickness),

            new Vector3(4.6f,2,wallThickness),
            new Vector3(4.6f,2,0),
            new Vector3(5.4f,2,0),

            new Vector3(5.4f,2,0),
            new Vector3(5.4f,2,wallThickness),
            new Vector3(4.6f,2,wallThickness),

            new Vector3(5.4f,0,0),
            new Vector3(5.4f,0,wallThickness),
            new Vector3(5.4f,2,wallThickness),

            new Vector3(5.4f,2,wallThickness),
            new Vector3(5.4f,2,0),
            new Vector3(5.4f,0,0),
        };

        wall1Faces = new int[wall1Vertices.Length];
        for (int i = 0; i < wall1Faces.Length; i++)
            wall1Faces[i] = i;

        Color[] wall1Colors = new Color[wall1Vertices.Length];
        for (int i = 0; i < wall1Colors.Length; i++)
        {
            wall1Colors[i] = wall1Colors[i] = new Color(243f / 255f, 243f / 255f, 243f / 255f);
        }

        UpdateMesh(wall1,wall1Vertices,wall1Faces, wall1Colors);
    }
    private void createCamera(){
        myCamera = new GameObject("Camera");
        myCamera.AddComponent<Camera>();
        myCamera.transform.position = new Vector3(-20,4,-10);
        myCamera.transform.rotation = Quaternion.Euler(0,60,0);
        myCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        myCamera.GetComponent<Camera>().backgroundColor = Color.black;
        myCamera.GetComponent<Camera>().nearClipPlane = 0.01f;
    }

    private (GameObject obj, Vector3 halfExtents) loadObject(String path, String name, String texture)
    {
        FileReader fileReader = new FileReader();
        fileReader.ReadFile(path);

        GameObject obj = new GameObject(name);
        obj.AddComponent<MeshFilter>();
        obj.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.AddComponent<MeshRenderer>();

        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
        mesh.vertices = fileReader.GetVertexes();
        mesh.triangles = fileReader.GetFaces();
        mesh.uv = fileReader.GetUVs();

        // Cargar la textura desde Resources
        Texture2D toiletTexture = Resources.Load<Texture2D>(texture);
        Debug.Log(toiletTexture != null ? "Textura OK" : "Textura no encontrada");

        Material mat = new Material(Shader.Find("SimpleShaderTexture")); // ← shader con textura
        mat.mainTexture = toiletTexture;
        obj.GetComponent<MeshRenderer>().material = mat;

        Vector3 half = fileReader.GetHalfExtents(); 
        
        return (obj, half);

    }

    private void changeposition(GameObject obj,Vector3 newPosition, Vector3 newRotation, Vector3 newScale){
        Matrix4x4 modelMatrix = CreateModelMatrix(newPosition, newRotation, newScale);
        obj.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix);

    }
    private void UpdateMesh(GameObject gO,Vector3[] v,int[] f, Color[] c){
        gO.GetComponent<MeshFilter>().mesh.vertices = v;
        gO.GetComponent<MeshFilter>().mesh.triangles = f;
        gO.GetComponent<MeshRenderer>().material = new Material(Shader.Find("SimpleShader"));
        gO.GetComponent<MeshFilter>().mesh.colors = c;
    }

    private Matrix4x4 CreateModelMatrix(Vector3 newPosition, Vector3 newRotation, Vector3 newScale)
    {
        Matrix4x4 positionMatrix = new Matrix4x4(
            new Vector4(1f, 0f, 0f, newPosition.x),
            new Vector4(0f, 1f, 0f, newPosition.y),
            new Vector4(0f, 0f, 1f, newPosition.z),
            new Vector4(0f, 0f, 0f, 1f)
            );
        positionMatrix = positionMatrix.transpose;

        Matrix4x4 rotationMatrixX = new Matrix4x4(
            new Vector4(1f, 0f, 0f, 0f),
            new Vector4(0f, Mathf.Cos(newRotation.x), -Mathf.Sin(newRotation.x), 0f),
            new Vector4(0f, Mathf.Sin(newRotation.x), Mathf.Cos(newRotation.x), 0f),
            new Vector4(0f, 0f, 0f, 1f)
            );

        Matrix4x4 rotationMatrixY = new Matrix4x4(
            new Vector4(Mathf.Cos(newRotation.y), 0f, Mathf.Sin(newRotation.y), 0f),
            new Vector4(0f, 1f, 0f, 0f),
            new Vector4(-Mathf.Sin(newRotation.y), 0f, Mathf.Cos(newRotation.y), 0f),
            new Vector4(0f, 0f, 0f, 1f)
            );

        Matrix4x4 rotationMatrixZ = new Matrix4x4(
            new Vector4(Mathf.Cos(newRotation.z), -Mathf.Sin(newRotation.z), 0f, 0f),
            new Vector4(Mathf.Sin(newRotation.z), Mathf.Cos(newRotation.z), 0f, 0f),
            new Vector4(0f, 0f, 1f, 0f),
            new Vector4(0f, 0f, 0f, 1f)
            );

        Matrix4x4 rotarionMatrix = rotationMatrixX * rotationMatrixY * rotationMatrixZ;
        rotarionMatrix = rotarionMatrix.transpose;

        Matrix4x4 scaleMatrix = new Matrix4x4(
            new Vector4(newScale.x, 0f, 0f, 0f),
            new Vector4(0f, newScale.y, 0f, 0f),
            new Vector4(0f, 0f, newScale.z, 0f),
            new Vector4(0f, 0f, 0f, 1f)
            );

        scaleMatrix = scaleMatrix.transpose;

        Matrix4x4 finalMatrix = positionMatrix;
        finalMatrix *= rotarionMatrix;
        finalMatrix *= scaleMatrix;

        return finalMatrix;
    }


}
