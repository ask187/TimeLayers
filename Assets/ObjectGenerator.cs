using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ObjectGenerator : MonoBehaviour
{
    [System.Serializable]
    public class TransformData
    {
        public Vector3 Translation;
        public Vector3 Rotation;
        public Vector3 Scale;
    }

    [System.Serializable]
    public class BoundsData
    {
        public Vector2 Min;
        public Vector2 Max;
    }

    [System.Serializable]
    public class VolumeBoundsData
    {
        public Vector3 Min;
        public Vector3 Max;
    }

    [System.Serializable]
    public class ObjectData
    {
        public string UUID;
        public List<string> SemanticClassifications;
        public TransformData Transform;
        public BoundsData PlaneBounds;
        public VolumeBoundsData VolumeBounds;
        public List<Vector2> PlaneBoundary2D;
    }

    public string jsonInput; // Assign JSON here

    void Start()
    {
        ObjectData data = JsonConvert.DeserializeObject<ObjectData>(jsonInput);
        Debug.Log(data.UUID);
        GameObject obj = new GameObject(data.UUID);

        // Set Position
        obj.transform.position = new Vector3(data.Transform.Translation.x, data.Transform.Translation.y, data.Transform.Translation.z);

        // Set Rotation
        obj.transform.eulerAngles = new Vector3(data.Transform.Rotation.x, data.Transform.Rotation.y, data.Transform.Rotation.z);

        // Set Scale
        obj.transform.localScale = new Vector3(data.Transform.Scale.x, data.Transform.Scale.y, data.Transform.Scale.z);

        // Create Mesh if needed
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();
        
        List<Vector3> vertices = new List<Vector3>();
        foreach (Vector2 point in data.PlaneBoundary2D)
        {
            vertices.Add(new Vector3(point.x, 0, point.y)); // Convert 2D points to 3D plane
        }
        mesh.vertices = vertices.ToArray();

        int[] triangles = { 0, 1, 2, 2, 3, 0 }; // Basic quad
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        renderer.material = new Material(Shader.Find("Standard"));
    }
}
