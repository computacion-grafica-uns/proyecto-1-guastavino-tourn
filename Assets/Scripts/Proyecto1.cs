using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecto1 : MonoBehaviour
{
    private GameObject myCamera;
    private FileReader fileReader;
    private int width = 10;
    private int height = 3;
    private int depth = 10;
    private float wallThickness = 0.15f;

    void Start()
    {
        createFloor();
        createCeiling();
        createWall1();
        fileReader = new FileReader();
        loadBed();
        createCamera();
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
            // Triangle 1
            new Vector3(0,0,0),
            new Vector3(0,3,0),
            new Vector3(4.6f,3,0),
            
            // Triangle 2
            new Vector3(0,0,0),
            new Vector3(4.6f,3,0),
            new Vector3(4.6f,0,0),

            // Triangle 3
            new Vector3(4.6f,2,0),
            new Vector3(4.6f,3,0),
            new Vector3(5.4f,3,0),

            // Triangle 4
            new Vector3(5.4f,2,0),
            new Vector3(4.6f,2,0),
            new Vector3(5.4f,3,0),
            
            // Triangle 5
            new Vector3(5.4f,3,0),
            new Vector3(5.9f,3,0),
            new Vector3(5.4f,0,0),

            // Triangle 6
            new Vector3(5.9f,0,0),
            new Vector3(5.4f,0,0),
            new Vector3(5.9f,3,0),

            // Triangle 7
            new Vector3(5.9f,3,0),
            new Vector3(7.4f,3,0),
            new Vector3(5.9f,2,0),

            // Triangle 8
            new Vector3(5.9f,2,0),
            new Vector3(7.4f,3,0),
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
            new Vector3(7.4f,3,0),
            new Vector3(10,3,0),

            // Triangle 12
            new Vector3(10,3,0),
            new Vector3(10,0,0),
            new Vector3(7.4f,0,0),

        };
    
        wall1Faces = new int[] {
            0,1,2,
            3,4,5,
            6,7,8,
            9,10,11,
            12,13,14,
            15,16,17,
            18,19,20,
            21,22,23,
            24,25,26,
            27,28,29,
            30,31,32,
            33,34,35
        };

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
    }

    private void loadBed()
    {
        fileReader.ReadFile("muebles/beds/bed1/bed1");

        GameObject bed1 = new GameObject("Bed1");
        bed1.AddComponent<MeshFilter>();
        bed1.GetComponent<MeshFilter>().mesh = new Mesh();
        bed1.AddComponent<MeshRenderer>();

        Vector3[] bedVertices = fileReader.GetVertexes();
        Color[] bedColors = new Color[bedVertices.Length];
        for (int i = 0; i < bedColors.Length; i++)
        {
            bedColors[i] = bedColors[i] = new Color(217f / 255f, 155f / 255f, 233f / 255f);
        }

        UpdateMesh(bed1, bedVertices, fileReader.GetFaces(), bedColors);

        Vector3 halfSizeBed1 = fileReader.GetHalfExtents();

        Vector3 newPosition = new Vector3(width - wallThickness - halfSizeBed1.x, halfSizeBed1.y, depth - wallThickness - halfSizeBed1.z);
        Vector3 newRotation = new Vector3(0f, 0f, 0f);
        Vector3 newScale = new Vector3(1f, 1f, 1f);

        Matrix4x4 modelMatrix = CreateModelMatrix(newPosition, newRotation, newScale);

        bed1.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix);
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
