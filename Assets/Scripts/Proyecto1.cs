using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Projecto1 : MonoBehaviour
{
    private int width = 10;
    private int height = 3;
    private int depth = 10;
    private float wallThickness = 0.15f;
    private float bathroomWidth = 4;
    private float bathroomDepth = 4.5f;
    private Vector3 pos = new Vector3(-20, 0, 20);
    private Vector3 target = new Vector3(0, 0, 0);
    private Vector3 up = new Vector3(0, 1, 0);
    void Start()
    {
        createFloor();
        createWalls();
        createBathroom();
        createCeiling();
        createFurniture();
        //createCamera();
        float fov = 100;
        float aspectRatio = 16 / (float)9;
        float nearClipPlane = 0.1f;
        float farClipPlane = 1000;
        Matrix4x4 proj = CalculatePerspectiveProjectMatrix(fov, aspectRatio, nearClipPlane, farClipPlane);

        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            if (r.material.HasProperty("_ProjectionMatrix"))
                r.material.SetMatrix("_ProjectionMatrix", GL.GetGPUProjectionMatrix(proj, true));
        }
    }

    private void createFurniture()
    {
        createBed();
        createWardrobeSofa();
        createBathroomFurniture();
        createKitchenFurniture();
        createChairsAndTables();
    }

    private void createBed()
    {
        var (bed, bedHalf) = loadObject(
            "muebles/beds/bed1/bed1",
            "Bed1",
            "bed1Texture",
            rgba(59, 53, 92)
        );
        changePosition(
            bed,
            new Vector3(width - wallThickness - bedHalf.x, bedHalf.y, depth - wallThickness - bedHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );        
    }

    private void createWardrobeSofa()
    {    
        var (sofa, sofaHalf) = loadObject(
            "muebles/sofa`s/sofa90degrees/90degreesSofa",
            "sofa",
            "sofa90degreesTextureBlue",
            rgba(92, 93, 54)
        );
        changePosition(
            sofa,
            new Vector3(width - wallThickness - sofaHalf.x, sofaHalf.y,  wallThickness + sofaHalf.z),
            new Vector3(0, -90* Mathf.Deg2Rad, 0),
            new Vector3(1, 1, 1)
        );  

        // var (wardrobe2, wardrobe2Half) = loadObject(
        //     "muebles/Wardrobes/Wardrobe2/Wardrobe2",
        //     "wardrobe2",
        //     "Wardrobe2Texture"
        // );
        // changeposition(
        //     wardrobe2,
        //     new Vector3(width - wallThickness - wardrobe2Half.x, wardrobe2Half.y,  wallThickness + wardrobe2Half.z + sofaHalf.z*2 + 0.5f),
        //     new Vector3(0, 180* Mathf.Deg2Rad, 0),
        //     new Vector3(1, 1, 1)
        // ); 

        var (halfWardrobe, wardrobe2Half) = loadObject(
            "muebles/Wardrobes/HalfWardrobe/HalfWardrobe",
            "HalfWardrobe",
            "HalfWardrobeTexture",
            rgba(44, 30, 22)
        );
        changePosition(
            halfWardrobe,
            new Vector3(width - wallThickness - wardrobe2Half.x, wardrobe2Half.y,  wallThickness + wardrobe2Half.z + sofaHalf.z*2 + 0.5f),
            new Vector3(0, 180* Mathf.Deg2Rad, 0),
            new Vector3(1, 1, 1)
        ); 


        var (wardrobe1, wardrobe1Half) = loadObject(
            "muebles/Wardrobes/Wardrobe1/Wardrobe1",
            "wardrobe1",
            "Wardrobe1Texture",
            rgba(30, 20, 15)  
        );
        changePosition(
            wardrobe1,
            new Vector3(width - wallThickness - wardrobe1Half.x, wardrobe1Half.y,  wallThickness + wardrobe1Half.z + wardrobe2Half.z*2 + sofaHalf.z*2 + 0.8f ),
            new Vector3(0, 180* Mathf.Deg2Rad, 0),
            new Vector3(1, 1, 1)
        );  

        var (littleOne, littleOneHalf) = loadObject(
            "muebles/Wardrobes/littleOne/littleOne",
            "littleOne",
            "littleOneTexture",
            rgba(44, 30, 22)
        );
        changePosition(
            littleOne,
            new Vector3(width - wallThickness - sofaHalf.x *2 - littleOneHalf.x - 0.1f, littleOneHalf.y,  wallThickness + littleOneHalf.z),
            new Vector3(0, -90* Mathf.Deg2Rad, 0),
            new Vector3(1, 1, 1)
        );
    }
    private void createBathroomFurniture()
    {
        var (bath, bathHalf) = loadObject(
            "muebles/Bathroom/bath/bath",
            "bath",
            "BathTexture",
            rgba(103, 133, 171)
        );
        changePosition(
            bath,
            new Vector3(bathHalf.z + 0.2f, bathHalf.y, bathHalf.x + 0.5f),
            new Vector3(0, 90 * Mathf.Deg2Rad, 0),
            new Vector3(1, 1, 1)
        );

        var (toilet, toiletHalf) = loadObject(
            "muebles/Bathroom/toilets/toilet1/toilet1",
            "toilet1",
            "toilet1Texture",
            rgba(141, 164, 192)
        );
        changePosition(
            toilet,
            new Vector3(toiletHalf.x + 0.2f, toiletHalf.y, bathHalf.x * 2 + toiletHalf.z + 1f),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );

        var (sink, sinkHalf) = loadObject(
            "muebles/Bathroom/sink/sink",
            "sink",
            "sinkTexture",
            rgba(141, 164, 192)
        );
        changePosition(
            sink,
            new Vector3(sinkHalf.x + 0.2f, sinkHalf.y, bathHalf.x * 2 + toiletHalf.z * 2 + sinkHalf.z + 1.3f),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );

        var (mirror, mirrorHalf) = loadObject(
            "muebles/Bathroom/mirror/mirror",
            "mirror",
            "mirrorTexture",
            rgba(84, 118, 161)
        );
        changePosition(
            mirror,
            new Vector3(mirrorHalf.x + 0.2f, mirrorHalf.y+1.5f, bathHalf.x * 2 + toiletHalf.z * 2 + sinkHalf.z + 1.3f),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );
    }

    private void createKitchenFurniture()
    {
        var (KitchenCabinetRounded, KitchenCabinetRoundedHalf) = loadObject(
            "muebles/Kitchen/Cabinets/KitchenCabinetRounded/KitchenCabinetRounded",
            "KitchenCabinetRounded",
            "CabinetRoundedTexture",
            rgba(30, 20, 15)  
        );
        changePosition(
            KitchenCabinetRounded,
            new Vector3( wallThickness + KitchenCabinetRoundedHalf.x, KitchenCabinetRoundedHalf.y, depth - wallThickness - KitchenCabinetRoundedHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        ); 

        var (KitchenStove2, KitchenStove2Half) = loadObject(
            "muebles/Kitchen/Cabinets/KitchenStove2/KitchenStove2",
            "KitchenStove2",
            "KitchenStove2Texture",
            rgba(44, 30, 22)
        );
        changePosition(
            KitchenStove2,
            new Vector3( wallThickness + KitchenStove2Half.x, KitchenStove2Half.y, depth - wallThickness - KitchenCabinetRoundedHalf.z * 2 - KitchenStove2Half.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        ); 
        
        var (KitchenCabinetWithOven, KitchenCabinetWithOvenHalf) = loadObject(
            "muebles/Kitchen/Cabinets/KitchenCabinetWithOven/KitchenCabinetWithOven",
            "KitchenCabinetWithOven",
            "KitchenCabinetWithOvenTexture",
            rgba(30, 20, 15)    
        );
        changePosition(
            KitchenCabinetWithOven,
            new Vector3( wallThickness + KitchenCabinetWithOvenHalf.x, KitchenCabinetWithOvenHalf.y, depth - wallThickness - KitchenCabinetRoundedHalf.z * 2 - KitchenStove2Half.z * 2 - KitchenCabinetWithOvenHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        ); 

        var (KitchenCabinet1, KitchenCabinet1Half) = loadObject(
            "muebles/Kitchen/Cabinets/KitchenCabinet1/KitchenCabinet1",
            "KitchenCabinet1",
            "KitchenCabinet1Texture",
            rgba(44, 30, 22)
        );
        changePosition(
            KitchenCabinet1,
            new Vector3( wallThickness + KitchenCabinetWithOvenHalf.x, KitchenCabinetWithOvenHalf.y, depth - wallThickness - KitchenCabinetRoundedHalf.z * 2 - KitchenStove2Half.z * 2 - KitchenCabinetWithOvenHalf.z*2 - KitchenCabinet1Half.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        ); 

        var (UpperCabinet, UpperCabinetHalf) = loadObject(
            "muebles/Kitchen/Cabinets/90DegreesUpperCabinet/90DegreesUpperCabinet",
            "UpperCabinet",
            "90degreesUpperCabinetTexture",
            rgba(67, 46, 34)
        );
        changePosition(
            UpperCabinet,
            new Vector3( wallThickness + UpperCabinetHalf.x, height-UpperCabinetHalf.y-0.05f, depth - wallThickness - UpperCabinetHalf.z ),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        ); 

        var (Fridge, FridgeHalf) = loadObject(
            "muebles/Kitchen/Fridge/Fridge",
            "Fridge",
            "FridgeTexture",
            rgba(122, 149, 182)
        );
        changePosition(
            Fridge,
            new Vector3( wallThickness + KitchenCabinetRoundedHalf.x * 2 + FridgeHalf.x, FridgeHalf.y, depth - wallThickness - FridgeHalf.z ),
            new Vector3(0, 90 * Mathf.Deg2Rad, 0),
            new Vector3(1, 1, 1)
        ); 
    }
    private Color rgba(float r, float g, float b, float a = 255)
    {
        return new Color(r / 255f, g / 255f, b / 255f, 1f);
    }
    private void createChairsAndTables()
    {
        var (table, tableHalf) = loadObject(
            "muebles/tables/table/table",
            "table",
            "tableTexture",
            rgba(30, 20, 15)  
        );
        changePosition(
            table,
            new Vector3(width - wallThickness - tableHalf.x - 3f, tableHalf.y, depth - wallThickness - tableHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        ); 

        var (chair1, chair1Half) = loadObject(
            "muebles/chairs/chair1/chair1",
            "chair1",
            "chair1Texture",
            rgba(44, 30, 22)
        );
        changePosition(
            chair1,
            new Vector3(width - wallThickness - chair1Half.x - 2.7f - tableHalf.x *2, chair1Half.y, depth - wallThickness - tableHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        ); 

        var (chair2, chair2Half) = loadObject(
            "muebles/chairs/chair1/chair1",
            "chair2",
            "chair1Texture",
            rgba(44, 30, 22)
        );
        changePosition(
            chair2,
            new Vector3(width - wallThickness - chair2Half.x - 2.7f, chair2Half.y, depth - wallThickness - tableHalf.z),
            new Vector3(0, 180* Mathf.Deg2Rad, 0),
            new Vector3(1, 1, 1)
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
            new Vector3(bathroomWidth - wallThickness,0,0),
            new Vector3(bathroomWidth - wallThickness,0,depth),
            new Vector3(width,0,0),

            // Triangle 2
            new Vector3(bathroomWidth - wallThickness,0,depth),
            new Vector3(width,0,depth),
            new Vector3(width,0,0),

             // Triangle 3
            new Vector3(0,0,bathroomDepth),
            new Vector3(0,0,depth),
            new Vector3(bathroomWidth,0,bathroomDepth),

            // Triangle 4
            new Vector3(0,0,depth),
            new Vector3(bathroomWidth,0,depth),
            new Vector3(bathroomWidth,0,bathroomDepth),

        };

        floorFaces = new int[floorVertices.Length];
        for (int i = 0; i < floorFaces.Length; i++)
            floorFaces[i] = i;

        Color[] floorColors = new Color[0];
        UpdateMesh(floor, floorVertices, floorFaces, floorColors);

        floor.GetComponent<MeshRenderer>().material = new Material(Shader.Find("FloorShader"));
        Renderer r = floor.GetComponent<Renderer>();
        r.material.SetFloat("_PlankWidth", 0.2f);
        r.material.SetFloat("_PlankLength", 1.0f);
        r.material.SetColor("_WoodLight", new Color(0.76f, 0.60f, 0.42f));
        r.material.SetColor("_WoodDark", new Color(0.45f, 0.30f, 0.18f));
        r.material.SetFloat("_VeinStrength", 0.3f);
        r.material.SetFloat("_GapSize", 0.01f);
    }

    private void createWalls()
    {
        createFrontWall();
        createLeftWall();
        createBackWall();
        createRightWall();
    }

    private void createFrontWall()
    {
        Vector3[] frontVertices;
        int[] frontFaces;
        GameObject frontWall;
        frontWall = new GameObject("FrontWall");
        frontWall.AddComponent<MeshFilter>();
        frontWall.GetComponent<MeshFilter>().mesh = new Mesh();
        frontWall.AddComponent<MeshRenderer>();
        frontWall.AddComponent<CameraManager>();
        frontVertices = new Vector3[]
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

        frontFaces = new int[frontVertices.Length];
        for (int i = 0; i < frontFaces.Length; i++)
            frontFaces[i] = i;

        Color[] frontColors = new Color[frontVertices.Length];
        for (int i = 0; i < frontColors.Length; i++)
        {
            frontColors[i] = frontColors[i] = new Color(243f / 255f, 243f / 255f, 243f / 255f);
        }

        UpdateMesh(frontWall, frontVertices, frontFaces, frontColors);
    }

    private void createBackWall()
    {
        Vector3[] backWallVertices;
        int[] backWallFaces;
        GameObject backWall;
        backWall = new GameObject("BackWall");
        backWall.AddComponent<MeshFilter>();
        backWall.GetComponent<MeshFilter>().mesh = new Mesh();
        backWall.AddComponent<MeshRenderer>();
        backWallVertices = new Vector3[]
        {
            // Exterior
            new Vector3(width,0,depth),
            new Vector3(width,height,depth),
            new Vector3(0,0,depth),

            new Vector3(0,0,depth),
            new Vector3(width,height,depth),
            new Vector3(0,height,depth),

            // Interior
            new Vector3(wallThickness,0,depth-wallThickness),
            new Vector3(wallThickness,height,depth-wallThickness),
            new Vector3(width - wallThickness,0,depth-wallThickness),

            new Vector3(width - wallThickness,0,depth-wallThickness),
            new Vector3(wallThickness,height,depth-wallThickness),
            new Vector3(width - wallThickness,height,depth-wallThickness),
        };

        backWallFaces = new int[backWallVertices.Length];
        for (int i = 0; i < backWallFaces.Length; i++)
            backWallFaces[i] = i;

        Color[] backWallColors = new Color[backWallVertices.Length];
        for (int i = 0; i < backWallColors.Length; i++)
        {
            backWallColors[i] = backWallColors[i] = new Color(243f / 255f, 243f / 255f, 243f / 255f);
        }

        UpdateMesh(backWall, backWallVertices, backWallFaces, backWallColors);
    }

    private void createLeftWall()
    {
        Vector3[] leftWallVertices;
        int[] leftWallFaces;
        GameObject leftWall;
        leftWall = new GameObject("LeftWall");
        leftWall.AddComponent<MeshFilter>();
        leftWall.GetComponent<MeshFilter>().mesh = new Mesh();
        leftWall.AddComponent<MeshRenderer>();
        leftWallVertices = new Vector3[]
        {
            // Exterior
            new Vector3(0,0,depth),
            new Vector3(0,height,depth),
            new Vector3(0,0,0),

            new Vector3(0,0,0),
            new Vector3(0,height,depth),
            new Vector3(0,height,0),

            // Interior
            new Vector3(wallThickness,0,0),
            new Vector3(wallThickness,height,0),
            new Vector3(wallThickness,0,depth-wallThickness),

            new Vector3(wallThickness,0,depth-wallThickness),
            new Vector3(wallThickness,height,0),
            new Vector3(wallThickness,height,depth-wallThickness),
        };

        leftWallFaces = new int[leftWallVertices.Length];
        for (int i = 0; i < leftWallFaces.Length; i++)
            leftWallFaces[i] = i;

        Color[] leftWallColors = new Color[leftWallVertices.Length];
        for (int i = 0; i < leftWallColors.Length; i++)
        {
            leftWallColors[i] = leftWallColors[i] = new Color(243f / 255f, 243f / 255f, 243f / 255f);
        }

        UpdateMesh(leftWall, leftWallVertices, leftWallFaces, leftWallColors);
    }

    private void createRightWall()
    {
        Vector3[] rightWallVertices;
        int[] rightWallFaces;
        GameObject rightWall;
        rightWall = new GameObject("RightWall");
        rightWall.AddComponent<MeshFilter>();
        rightWall.GetComponent<MeshFilter>().mesh = new Mesh();
        rightWall.AddComponent<MeshRenderer>();
        rightWallVertices = new Vector3[]
        {
            // Exterior
            new Vector3(width,0,0),
            new Vector3(width,height,0),
            new Vector3(width,0,depth),

            new Vector3(width,0,depth),
            new Vector3(width,height,0),
            new Vector3(width,height,depth),

            // Interior
            new Vector3(width-wallThickness,0,depth-wallThickness),
            new Vector3(width-wallThickness,height,depth-wallThickness),
            new Vector3(width-wallThickness,0,wallThickness),

            new Vector3(width-wallThickness,0,wallThickness),
            new Vector3(width-wallThickness,height,depth-wallThickness),
            new Vector3(width-wallThickness,height,wallThickness),
        };

        rightWallFaces = new int[rightWallVertices.Length];
        for (int i = 0; i < rightWallFaces.Length; i++)
            rightWallFaces[i] = i;

        Color[] rightWallColors = new Color[rightWallVertices.Length];
        for (int i = 0; i < rightWallColors.Length; i++)
        {
            rightWallColors[i] = rightWallColors[i] = new Color(243f / 255f, 243f / 255f, 243f / 255f);
        }

        UpdateMesh(rightWall, rightWallVertices, rightWallFaces, rightWallColors);
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
        UpdateMesh(ceiling, ceilingVertices, ceilingFaces, ceilingColors);
    }

    private void createBathroom()
    {
        createBathroomFloor();
        createBathroomWalls();
    }

    private void createBathroomFloor()
    {
        Vector3[] bathroomFloorVertices;
        int[] bathroomFloorFaces;
        GameObject bathroomFloor;
        bathroomFloor = new GameObject("BathroomFloor");
        bathroomFloor.AddComponent<MeshFilter>();
        bathroomFloor.GetComponent<MeshFilter>().mesh = new Mesh();
        bathroomFloor.AddComponent<MeshRenderer>();
        bathroomFloorVertices = new Vector3[]
        {
            // Triangle 1
            new Vector3(0,0,0),
            new Vector3(0,0,bathroomDepth),
            new Vector3(bathroomWidth - wallThickness,0,0),

            // Triangle 2
            new Vector3(bathroomWidth - wallThickness,0,0),
            new Vector3(0,0,bathroomDepth),
            new Vector3(bathroomWidth - wallThickness,0,bathroomDepth),

        };

        bathroomFloorFaces = new int[bathroomFloorVertices.Length];
        for (int i = 0; i < bathroomFloorFaces.Length; i++)
            bathroomFloorFaces[i] = i;

        Color[] bathroomFloorColors = new Color[0];
        UpdateMesh(bathroomFloor, bathroomFloorVertices, bathroomFloorFaces, bathroomFloorColors);

        bathroomFloor.GetComponent<MeshRenderer>().material = new Material(Shader.Find("BathroomFloorShader"));
        Renderer r = bathroomFloor.GetComponent<Renderer>();
        r.material.SetFloat("_TileSize", 0.5f);
        r.material.SetFloat("_GapSize", 0.02f);
        r.material.SetColor("_TileLight", new Color(0.92f, 0.92f, 0.92f));
        r.material.SetColor("_TileDark", new Color(0.75f, 0.75f, 0.78f));
        r.material.SetColor("_GroutColor", new Color(0.30f, 0.30f, 0.30f));
        r.material.SetFloat("_Glossiness", 0.85f);
        r.material.SetFloat("_VariationStr", 0.1f);
    }

    private void createBathroomWalls()
    {
        Vector3[] bathroomWallsVertices;
        int[] bathroomWallsFaces;
        GameObject bathroomWalls;
        bathroomWalls = new GameObject("BathroomWalls");
        bathroomWalls.AddComponent<MeshFilter>();
        bathroomWalls.GetComponent<MeshFilter>().mesh = new Mesh();
        bathroomWalls.AddComponent<MeshRenderer>();

        Vector3[] frontWallVertices = createFrontBathroomWall();
        Vector3[] sideWallVertices = createSideBathroomWall();
        Vector3[] doorBordersVertices = createBordersBathroomDoor();
        bathroomWallsVertices = new Vector3[frontWallVertices.Length + sideWallVertices.Length + doorBordersVertices.Length];
        frontWallVertices.CopyTo(bathroomWallsVertices, 0);
        sideWallVertices.CopyTo(bathroomWallsVertices, frontWallVertices.Length);
        doorBordersVertices.CopyTo(bathroomWallsVertices, frontWallVertices.Length + sideWallVertices.Length);

        bathroomWallsFaces = new int[bathroomWallsVertices.Length];
        for (int i = 0; i < bathroomWallsFaces.Length; i++)
            bathroomWallsFaces[i] = i;

        Color[] bathroomWallsColors = new Color[0];

        UpdateMesh(bathroomWalls, bathroomWallsVertices, bathroomWallsFaces, bathroomWallsColors);
    }

    private Vector3[] createFrontBathroomWall()
    {
        Vector3[] frontBathroomWallVertices = new Vector3[]
        {
            // Interior

            new Vector3(bathroomWidth-wallThickness,0,bathroomDepth-wallThickness),
            new Vector3(bathroomWidth-wallThickness,height,bathroomDepth-wallThickness),
            new Vector3(bathroomWidth-wallThickness,0,bathroomDepth-wallThickness-0.2f),

            new Vector3(bathroomWidth-wallThickness,0,bathroomDepth-wallThickness-0.2f),
            new Vector3(bathroomWidth-wallThickness,height,bathroomDepth-wallThickness),
            new Vector3(bathroomWidth-wallThickness,height,bathroomDepth-wallThickness-0.2f),

            new Vector3(bathroomWidth-wallThickness,2,bathroomDepth-wallThickness-0.2f),
            new Vector3(bathroomWidth-wallThickness,height,bathroomDepth-wallThickness-0.2f),
            new Vector3(bathroomWidth-wallThickness,2,bathroomDepth-wallThickness-1),

            new Vector3(bathroomWidth-wallThickness,2,bathroomDepth-wallThickness-1),
            new Vector3(bathroomWidth-wallThickness,height,bathroomDepth-wallThickness-0.2f),
            new Vector3(bathroomWidth-wallThickness,height,bathroomDepth-wallThickness-1),

            new Vector3(bathroomWidth-wallThickness,0,bathroomDepth-wallThickness-1f),
            new Vector3(bathroomWidth-wallThickness,height,bathroomDepth-wallThickness-1f),
            new Vector3(bathroomWidth-wallThickness,0,wallThickness),

            new Vector3(bathroomWidth-wallThickness,0,wallThickness),
            new Vector3(bathroomWidth-wallThickness,height,bathroomDepth-wallThickness-1f),
            new Vector3(bathroomWidth-wallThickness,height,wallThickness),

            // Exterior
            
            new Vector3(bathroomWidth,0,wallThickness),
            new Vector3(bathroomWidth,height,wallThickness),
            new Vector3(bathroomWidth,0,bathroomDepth - wallThickness - 1f),

            new Vector3(bathroomWidth,0,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth,height,wallThickness),
            new Vector3(bathroomWidth,height,bathroomDepth - wallThickness - 1f),

            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth,height,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 0.2f),

            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth,height,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth,height,bathroomDepth - wallThickness - 0.2f),

            new Vector3(bathroomWidth,0,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth,height,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth,0,bathroomDepth),

            new Vector3(bathroomWidth,0,bathroomDepth),
            new Vector3(bathroomWidth,height,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth,height,bathroomDepth),
        };

        return frontBathroomWallVertices;
    }

    private Vector3[] createSideBathroomWall()
    {
        Vector3[] sideBathroomWallVertices = new Vector3[]
        {
            // Interior
            new Vector3(wallThickness,0,bathroomDepth-wallThickness),
            new Vector3(wallThickness,height,bathroomDepth-wallThickness),
            new Vector3(bathroomWidth - wallThickness,0,bathroomDepth-wallThickness),

            new Vector3(bathroomWidth - wallThickness,0,bathroomDepth-wallThickness),
            new Vector3(wallThickness,height,bathroomDepth-wallThickness),
            new Vector3(bathroomWidth - wallThickness,height,bathroomDepth-wallThickness),

            //Exterior
            new Vector3(bathroomWidth,0,bathroomDepth),
            new Vector3(bathroomWidth,height,bathroomDepth),
            new Vector3(wallThickness,0,bathroomDepth),

            new Vector3(wallThickness,0,bathroomDepth),
            new Vector3(bathroomWidth,height,bathroomDepth),
            new Vector3(wallThickness,height,bathroomDepth),
        };

        return sideBathroomWallVertices;
    }

    private Vector3[] createBordersBathroomDoor()
    {
        Vector3[] bordersBathroomDoor = new Vector3[]
        {
            // Puerta
            new Vector3(bathroomWidth,0,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth - wallThickness,0,bathroomDepth - wallThickness - 1f),

            new Vector3(bathroomWidth - wallThickness,0,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth - wallThickness,2,bathroomDepth - wallThickness - 1f),


            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth - wallThickness,2,bathroomDepth - wallThickness - 1f),

            new Vector3(bathroomWidth - wallThickness,2,bathroomDepth - wallThickness - 1f),
            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth - wallThickness,2,bathroomDepth - wallThickness - 0.2f),

            new Vector3(bathroomWidth - wallThickness,0,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth - wallThickness,2,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth,0,bathroomDepth - wallThickness - 0.2f),

            new Vector3(bathroomWidth,0,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth - wallThickness,2,bathroomDepth - wallThickness - 0.2f),
            new Vector3(bathroomWidth,2,bathroomDepth - wallThickness - 0.2f),

            // Ventana
        };

        return bordersBathroomDoor;
    }

    private (GameObject obj, Vector3 halfExtents) loadObject(String path, String name, String texture, Color color)
    {
        FileReader fileReader = new FileReader();
        fileReader.ReadFile(path);

        GameObject obj = new GameObject(name);
        obj.AddComponent<MeshFilter>();
        obj.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.AddComponent<MeshRenderer>();

        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = fileReader.GetVertexes();
        mesh.vertices = vertices;
        mesh.triangles = fileReader.GetFaces();
        mesh.uv = fileReader.GetUVs();

        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }
        mesh.colors = colors;

        obj.GetComponent<MeshRenderer>().material = new Material(Shader.Find("SimpleShader"));

        // Cargar la textura desde Resources
        // Texture2D toiletTexture = Resources.Load<Texture2D>(texture);
        // Debug.Log(toiletTexture != null ? "Textura OK" : "Textura no encontrada");

        // Material mat = new Material(Shader.Find("SimpleShaderTexture")); // ← shader con textura
        // mat.mainTexture = toiletTexture;
        // obj.GetComponent<MeshRenderer>().material = mat;


        Vector3 half = fileReader.GetHalfExtents(); 
        
        return (obj, half);

    }

    private void changePosition(GameObject obj,Vector3 newPosition, Vector3 newRotation, Vector3 newScale){
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

    private Matrix4x4 CalculatePerspectiveProjectMatrix(float fov, float aspect, float n, float f)
    {
        Matrix4x4 perspectiveProjectMatrix = new Matrix4x4(
            new Vector4(1 / (aspect * Mathf.Tan(fov / 2)), 0f, 0f, 0f),
            new Vector4(0f, 1 / Mathf.Tan(fov / 2), 0f, 0f),
            new Vector4(0f, 0f, (f + n) / (n - f), (2 * f * n) / (n - f)),
            new Vector4(0f, 0f, -1f, 0f)
        );

        return perspectiveProjectMatrix.transpose;
    }

    private void Update()
    {
        
    }
}
