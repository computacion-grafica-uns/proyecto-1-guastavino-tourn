using System;
using UnityEngine;

public class Projecto1 : MonoBehaviour
{
    private int width = 10;
    private int height = 3;
    private int depth = 10;
    private float wallThickness = 0.15f;
    private float bathroomWidth = 4;
    private float bathroomDepth = 4.5f;
    void Start()
    {
        createFloor();
        createWalls();
        createBathroom();
        createCeiling();
        createFurniture();
        GameObject cameraGO = new GameObject("CameraController");
        CameraManager cam = cameraGO.AddComponent<CameraManager>();
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
        Color wallColor = rgba(243, 243, 243);
        createFrontWall(wallColor);
        createLeftWall(wallColor);
        createBackWall(wallColor);
        createRightWall(wallColor);
    }

    private void createFrontWall(Color wallColor)
    {
        var (frontWall, frontWallHalf) = loadObject(
            "walls/house/frontWall",
            "frontWall",
            "frontWallTexture",
            wallColor
        );
        changePosition(
            frontWall,
            new Vector3(frontWallHalf.x, frontWallHalf.y, frontWallHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );
    }

    private void createBackWall(Color wallColor)
    {
        var (backWall, backWallHalf) = loadObject(
            "walls/house/backWall",
            "backWall",
            "backWallTexture",
            wallColor
        );
        changePosition(
            backWall,
            new Vector3(backWallHalf.x, backWallHalf.y, backWallHalf.z + depth - wallThickness),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );
    }

    private void createLeftWall(Color wallColor)
    {
        var (leftWall, leftWallHalf) = loadObject(
            "walls/house/leftWall",
            "leftWall",
            "leftWallTexture",
            wallColor
        );
        changePosition(
            leftWall,
            new Vector3(leftWallHalf.x, leftWallHalf.y, leftWallHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );
    }

    private void createRightWall(Color wallColor)
    {
        var (rightWall, rightWallHalf) = loadObject(
            "walls/house/rightWall",
            "rightWall",
            "rightWallTexture",
            wallColor
        );
        changePosition(
            rightWall,
            new Vector3(rightWallHalf.x + width - wallThickness, rightWallHalf.y, rightWallHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );
    }

    private void createCeiling()
    {
        Color ceilingColor = rgba(243, 243, 243);
        var (ceiling, ceilingHalf) = loadObject(
           "walls/house/ceiling",
           "ceiling",
           "ceilingTexture",
           ceilingColor
       );
        changePosition(
            ceiling,
            new Vector3(ceilingHalf.x, ceilingHalf.y + height, ceilingHalf.z),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );
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
        Color wallColor = rgba(243, 243, 243);

        createBathroomFrontWall(wallColor);

        createBathroomSideWall(wallColor);
    }

    private void createBathroomFrontWall(Color wallColor)
    {
        var (bathroomFrontWall, bathroomFrontWallHalf) = loadObject(
            "walls/bathroom/bathroomFrontWall",
            "bathroomFrontWall",
            "bathroomFrontWallTexture",
            wallColor
        );
        changePosition(
            bathroomFrontWall,
            new Vector3(bathroomFrontWallHalf.x + 3.85f, bathroomFrontWallHalf.y, bathroomFrontWallHalf.z + wallThickness),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );
    }

    private void createBathroomSideWall(Color wallColor)
    {
        var (bathroomSideWall, bathroomSideWallHalf) = loadObject(
            "walls/bathroom/bathroomSideWall",
            "bathroomSideWall",
            "bathroomSideWallTexture",
            wallColor
        );
        changePosition(
            bathroomSideWall,
            new Vector3(bathroomSideWallHalf.x + wallThickness, bathroomSideWallHalf.y, bathroomSideWallHalf.z + 4.35f),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1)
        );
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

        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }
        mesh.colors = colors;

        obj.GetComponent<MeshRenderer>().material = new Material(Shader.Find("SimpleShader"));

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

    private void Update()
    {
        
    }
}
