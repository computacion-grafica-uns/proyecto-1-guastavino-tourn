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

    // Start is called before the first frame update
    void Start()
    {
        createPiso();
        createTecho();
        createPared1();
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
        UpdateMesh(piso,vPiso,fPiso);
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
        UpdateMesh(techo,vTecho,fTecho);
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
        UpdateMesh(pared1,vPared1,fPared1);
    }
    private void createCamera(){
        miCamara = new GameObject("Camara");
        miCamara.AddComponent<Camera>();
        miCamara.transform.position = new Vector3(-20,4,-10);
        miCamara.transform.rotation = Quaternion.Euler(0,60,0);
        miCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        miCamara.GetComponent<Camera>().backgroundColor = Color.black;
    }
    
    private void UpdateMesh(GameObject gO,Vector3[] v,int[] f){
        gO.GetComponent<MeshFilter>().mesh.vertices = v;
        gO.GetComponent<MeshFilter>().mesh.triangles = f;
    }

}
