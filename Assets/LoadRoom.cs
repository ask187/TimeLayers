using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class SceneData
{
    public string CoordinateSystem;
    public List<Room> Rooms;
}

[System.Serializable]
public class Room
{
    public string UUID;
    public RoomLayout RoomLayout;
    public List<Anchor> Anchors;
}

[System.Serializable]
public class RoomLayout
{
    public string FloorUuid;
    public string CeilingUuid;
    public List<string> WallsUuid;
}

[System.Serializable]
public class Anchor
{
    public string UUID;
    public List<string> SemanticClassifications;
    public TransformData Transform;
    public PlaneBoundsData PlaneBounds;
    public VolumeBoundsData VolumeBounds;
    public List<float[]> PlaneBoundary2D;
}

[System.Serializable]
public class TransformData
{
    public float[] Translation;
    public float[] Rotation;
    public float[] Scale;
}

[System.Serializable]
public class PlaneBoundsData
{
    public float[] Min;
    public float[] Max;
}

[System.Serializable]
public class VolumeBoundsData
{
    public float[] Min;
    public float[] Max;
}

public class LoadRoom : MonoBehaviour
{
    public string jsonFilePath;  // Adjust as needed

    void Start()
    {
        string jsonText = File.ReadAllText(jsonFilePath);
        // SceneData sceneData = JsonUtility.FromJson<SceneData>(jsonText);
        SceneData sceneData = JsonConvert.DeserializeObject<SceneData>(jsonText);
        // string json = JsonConvert.SerializeObject(sceneData); // Pretty-print JSON
        // Debug.Log(json);
        GenerateScene(sceneData);
    }

    void GenerateScene(SceneData data)
    {
        foreach (Room room in data.Rooms)
        {
            GameObject roomObject = new GameObject("Room_" + room.UUID);

            foreach (Anchor anchor in room.Anchors)
            {
                if (anchor.SemanticClassifications.Contains("WALL_FACE") | anchor.SemanticClassifications.Contains("FLOOR"))
                {
                    CreatePlane(anchor);
                }
                else if (anchor.SemanticClassifications.Contains("TABLE"))
                {
                    CreateCube(anchor);
                }
            }
        }
    }

    void CreatePlane(Anchor anchor)
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        SetTransform(plane, anchor.Transform);
        Vector3 size = new Vector3(
            Mathf.Abs(anchor.PlaneBounds.Max[0] - anchor.PlaneBounds.Min[0]),
            1f,
            Mathf.Abs(anchor.PlaneBounds.Max[1] - anchor.PlaneBounds.Min[1])
        );
        plane.transform.localScale = size / 10; // Unity planes are 10x10 by default
    }

    void CreateCube(Anchor anchor)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        SetTransform(cube, anchor.Transform);
        Vector3 size = new Vector3(
            anchor.VolumeBounds.Max[0] - anchor.VolumeBounds.Min[0],
            anchor.VolumeBounds.Max[1] - anchor.VolumeBounds.Min[1],
            anchor.VolumeBounds.Max[2] - anchor.VolumeBounds.Min[2]
        );
        cube.transform.localScale = size;
    }


    void SetTransform(GameObject obj, TransformData transformData)
    {
        obj.transform.position = new Vector3(
            transformData.Translation[0],
            transformData.Translation[1],
            transformData.Translation[2]
        );
        obj.transform.eulerAngles = new Vector3(
            transformData.Rotation[0],
            transformData.Rotation[1],
            transformData.Rotation[2]
        );
        obj.transform.localScale = new Vector3(
            transformData.Scale[0],
            transformData.Rotation[1],
            transformData.Rotation[2]
        );
    }
}
