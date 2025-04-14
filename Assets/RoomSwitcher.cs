using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour
{
    public GameObject room1Root;
    public GameObject room2Root;
    public GameObject room3Root;
    public float transitionDuration = 2.0f;

    private List<Material> room1Materials = new List<Material>();
    private List<Material> room2Materials = new List<Material>();
    private List<Material> room3Materials = new List<Material>();

    private float transitionTimer = 0.0f;
    private bool isTransitioning = false;
    private int currentRoom = 1;
    private int targetRoom = 1;

    void Start()
    {
        CollectMaterials(room1Root, room1Materials);
        CollectMaterials(room2Root, room2Materials);
        CollectMaterials(room3Root, room3Materials);

        // Start with Room 1 visible, others hidden
        SetFade(room1Materials, 1f); // Visible
        SetFade(room2Materials, 0f); // Hidden
        SetFade(room3Materials, 0f); // Hidden
    }

    void Update()
    {
        if (!isTransitioning)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) StartTransitionTo(1);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) StartTransitionTo(2);
            else if (Input.GetKeyDown(KeyCode.Alpha3)) StartTransitionTo(3);
        }

        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);

            List<Material> fromMaterials = GetMaterialsForRoom(currentRoom);
            List<Material> toMaterials = GetMaterialsForRoom(targetRoom);

            SetFade(fromMaterials, 1 - t);   // Fade out current room (visible → invisible)
            SetFade(toMaterials, t);         // Fade in target room (invisible → visible)

            if (t >= 1f)
            {
                isTransitioning = false;
                currentRoom = targetRoom;
                Debug.Log($"Switched to Room {currentRoom}");
            }
        }
    }

    void StartTransitionTo(int roomNumber)
    {
        if (roomNumber == currentRoom) return;
        targetRoom = roomNumber;
        transitionTimer = 0f;
        isTransitioning = true;
        Debug.Log($"Starting transition from Room {currentRoom} to Room {targetRoom}");
    }

    void CollectMaterials(GameObject root, List<Material> materialList)
    {
        HashSet<Material> uniqueMats = new HashSet<Material>();
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            foreach (Material m in r.materials)
            {
                if (m != null && uniqueMats.Add(m))
                {
                    materialList.Add(m);
                }
            }
        }
    }

    void SetFade(List<Material> materials, float fadeValue)
    {
        foreach (Material mat in materials)
        {
            if (mat.HasProperty("_FadeAmount"))
            {
                mat.SetFloat("_FadeAmount", fadeValue);
            }
        }
    }

    List<Material> GetMaterialsForRoom(int roomNumber)
    {
        switch (roomNumber)
        {
            case 1: return room1Materials;
            case 2: return room2Materials;
            case 3: return room3Materials;
            default: return new List<Material>();
        }
    }
}
