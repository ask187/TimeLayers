using UnityEngine;
using UnityEditor; // Required for saving prefabs (Editor-only)
using System.IO;
using Meta.XR.MRUtilityKit;

public class ObjectTracker : MonoBehaviour
{
    public string assetsSavePath = "Assets/SavedRooms/"; // Folder to save prefabs
    private MRUKRoom roomInstance;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Find the MRUKRoom instance in the scene
        roomInstance = FindObjectOfType<MRUKRoom>();
        foreach (Vector3 point in roomInstance.GetRoomOutline())
        {
            Debug.Log(point);
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 8; // 8 corners
        lineRenderer.loop = true;       // Connect the last point to the first
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineRenderer.endColor = Color.green;

        UpdateBoundingBox(roomInstance.GetFloorAnchor().VolumeBounds.Value);
    }

    void UpdateBoundingBox(Bounds bounds)
    {
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        Vector3[] corners = new Vector3[]
        {
            center + new Vector3(-extents.x, -extents.y, -extents.z), // 0
            center + new Vector3(extents.x, -extents.y, -extents.z),  // 1
            center + new Vector3(extents.x, -extents.y, extents.z),   // 2
            center + new Vector3(-extents.x, -extents.y, extents.z),  // 3
            center + new Vector3(-extents.x, extents.y, -extents.z),  // 4
            center + new Vector3(extents.x, extents.y, -extents.z),   // 5
            center + new Vector3(extents.x, extents.y, extents.z),    // 6
            center + new Vector3(-extents.x, extents.y, extents.z)    // 7
        };

        lineRenderer.positionCount = 16; // 12 edges + 4 extra to close the box
        lineRenderer.SetPositions(new Vector3[]
        {
            corners[0], corners[1], corners[2], corners[3], corners[0], // Bottom
            corners[4], corners[5], corners[6], corners[7], corners[4], // Top
            corners[0], corners[4], // Side 1
            corners[1], corners[5], // Side 2
            corners[2], corners[6], // Side 3
            corners[3], corners[7]  // Side 4
        });
    }

    void SaveRoomAsPrefab(GameObject roomObject)
    {
#if UNITY_EDITOR
        if (!Directory.Exists(assetsSavePath))
        {
            Directory.CreateDirectory(assetsSavePath);
        }

        string prefabPath = Path.Combine(assetsSavePath, $"{roomObject.name}.prefab");

        GameObject savedPrefab = PrefabUtility.SaveAsPrefabAsset(roomObject, prefabPath);

        if (savedPrefab != null)
        {
            Debug.Log($"✅ MRUKRoom saved as prefab at: {prefabPath}");
        }
        else
        {
            Debug.LogError($"❌ Failed to save MRUKRoom as a prefab!");
        }
#endif
    }
}
