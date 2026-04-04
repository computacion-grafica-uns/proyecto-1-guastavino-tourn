using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyecto1 : MonoBehaviour
{
    private Vector3[] vPiso;
    private int[] fPiso;
    private GameObject piso;
    private Vector3[] vTecho;
    private int[] fTecho;
    private GameObject techo;
    private Vector3[] vPared1;
    private int[] fPared1;
    private GameObject pared1;
    private GameObject miCamara;
    private FileReader fileReader;

    private int anchura = 10;
    private int altura = 3;
    private int profundidad = 10;
    private float grosorPared = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        createPiso();
        createTecho();
        createPared1();
        fileReader = new FileReader();
        cargarCama();
        createCamera();
    }

    private void createPiso()
    {
        piso = new GameObject("Piso");
        piso.AddComponent<MeshFilter>();
        piso.GetComponent<MeshFilter>().mesh = new Mesh();
        piso.AddComponent<MeshRenderer>();
        vPiso = new Vector3[]
        {
            // Triángulo 1
            new Vector3(0,0,0),
            new Vector3(0,0,10),
            new Vector3(10,0,0),
            

            // Triángulo 2
            new Vector3(0,0,10),
            new Vector3(10,0,10),
            new Vector3(10,0,0),

        };
    
        fPiso = new int[] {
            0,1,2,
            3,4,5,
        };

        Color[] cPiso = new Color[vPiso.Length];
        for (int i = 0; i < cPiso.Length; i++)
        {
            cPiso[i] = cPiso[i] = new Color(237f / 255f, 195f / 255f, 133f / 255f);
        }

        UpdateMesh(piso,vPiso,fPiso, cPiso);
    }

    private void createTecho()
    {
        techo = new GameObject("Techo");
        techo.AddComponent<MeshFilter>();
        techo.GetComponent<MeshFilter>().mesh = new Mesh();
        techo.AddComponent<MeshRenderer>();
        vTecho = new Vector3[]
        {
            // Triángulo 1
            new Vector3(0,3,0),
            new Vector3(0,3,10),
            new Vector3(10,3,0),
            

            // Triángulo 2
            new Vector3(0,3,10),
            new Vector3(10,3,10),
            new Vector3(10,3,0),

        };
    
        fTecho = new int[] {
            0,1,2,
            3,4,5,
        };

        Color[] cTecho = new Color[vTecho.Length];
        for (int i = 0; i < cTecho.Length; i++)
        {
            cTecho[i] = cTecho[i] = new Color(243f / 255f, 243f / 255f, 243f / 255f);
        }
        UpdateMesh(techo,vTecho,fTecho, cTecho);
    }
    private void createPared1()
    {
        pared1 = new GameObject("Pared1");
        pared1.AddComponent<MeshFilter>();
        pared1.GetComponent<MeshFilter>().mesh = new Mesh();
        pared1.AddComponent<MeshRenderer>();
        vPared1 = new Vector3[]
        {
            // Triángulo 1
            new Vector3(0,0,0),
            new Vector3(0,3,0),
            new Vector3(4.6f,3,0),
            
            // Triángulo 2
            new Vector3(0,0,0),
            new Vector3(4.6f,3,0),
            new Vector3(4.6f,0,0),

            // Triángulo 3
            new Vector3(4.6f,2,0),
            new Vector3(4.6f,3,0),
            new Vector3(5.4f,3,0),

            // Triángulo 4
            new Vector3(5.4f,2,0),
            new Vector3(4.6f,2,0),
            new Vector3(5.4f,3,0),
            
            // Triángulo 5
            new Vector3(5.4f,3,0),
            new Vector3(5.9f,3,0),
            new Vector3(5.4f,0,0),

            // Triángulo 6
            new Vector3(5.9f,0,0),
            new Vector3(5.4f,0,0),
            new Vector3(5.9f,3,0),

            // Triángulo 7
            new Vector3(5.9f,3,0),
            new Vector3(7.4f,3,0),
            new Vector3(5.9f,2,0),

            // Triángulo 8
            new Vector3(5.9f,2,0),
            new Vector3(7.4f,3,0),
            new Vector3(7.4f,2,0),

            // Triángulo 9
            new Vector3(5.9f,0,0),
            new Vector3(5.9f,1,0),
            new Vector3(7.4f,1,0),

            // Triángulo 10
            new Vector3(5.9f,0,0),
            new Vector3(7.4f,1,0),
            new Vector3(7.4f,0,0),

            // Triángulo 11
            new Vector3(7.4f,0,0),
            new Vector3(7.4f,3,0),
            new Vector3(10,3,0),

            // Triángulo 12
            new Vector3(10,3,0),
            new Vector3(10,0,0),
            new Vector3(7.4f,0,0),

        };
    
        fPared1 = new int[] {
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

        Color[] cPared1 = new Color[vPared1.Length];
        for (int i = 0; i < cPared1.Length; i++)
        {
            cPared1[i] = cPared1[i] = new Color(243f / 255f, 243f / 255f, 243f / 255f);
        }

        UpdateMesh(pared1,vPared1,fPared1, cPared1);
    }
    private void createCamera(){
        miCamara = new GameObject("Camara");
        miCamara.AddComponent<Camera>();
        miCamara.transform.position = new Vector3(-20,4,-10);
        miCamara.transform.rotation = Quaternion.Euler(0,60,0);
        miCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        miCamara.GetComponent<Camera>().backgroundColor = Color.black;
    }

    private void cargarCama()
    {
        fileReader.ReadFile("muebles/beds/bed1/bed1");

        GameObject cama1 = new GameObject("Cama1");
        cama1.AddComponent<MeshFilter>();
        cama1.GetComponent<MeshFilter>().mesh = new Mesh();
        cama1.AddComponent<MeshRenderer>();

        Vector3[] vCama = fileReader.GetVertexes();
        Color[] cCama = new Color[vCama.Length];
        for (int i = 0; i < cCama.Length; i++)
        {
            cCama[i] = cCama[i] = new Color(217f / 255f, 155f / 255f, 233f / 255f);
        }

        UpdateMesh(cama1, vCama, fileReader.GetFaces(), cCama);

        Vector3 mitadTamanoCama1 = fileReader.GetHalfExtents();

        Vector3 newPosition = new Vector3(anchura - grosorPared - mitadTamanoCama1.x, mitadTamanoCama1.y, profundidad - grosorPared - mitadTamanoCama1.z);
        Vector3 newRotation = new Vector3(0f, 0f, 0f);
        Vector3 newScale = new Vector3(1f, 1f, 1f);

        Matrix4x4 modelMatrix = CreateModelMatrix(newPosition, newRotation, newScale);

        cama1.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix);
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
