using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.IO;
using System;

public class RoomCapture : MonoBehaviour
{
    public string savePath;
    private string savePath1;
    private string logFilePath;
    // Start is called before the first frame update
    void Start()
    {
        // savePath = Path.Combine(Application.persistentDataPath, "SceneData.json");
        // savePath1 = Path.Combine(Application.persistentDataPath, "SceneString.json");
        // logFilePath = Application.persistentDataPath + "/ExportLog.txt";
        LoadScene();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     SaveScene();
    // }

    // Save Scene Data to JSON
    // public void SaveScene()
    // {
    //     var sceneData = SerializationHelpers.Serialize(SerializationHelpers.CoordinateSystem.Unity, true, new List<MRUKRoom> {MRUK.Instance.GetCurrentRoom()});
    //     var sceneString = MRUK.Instance.SaveSceneToJsonString(SerializationHelpers.CoordinateSystem.Unity, true, new List<MRUKRoom> {MRUK.Instance.GetCurrentRoom()});
    //     if (!string.IsNullOrEmpty(sceneData))
    //     {
    //         File.WriteAllText(savePath, sceneData);
    //         WriteLog("✅ Scene saved successfully! Path: " + savePath);
    //     }
    //     else
    //     {
    //         WriteLog("❌ Failed to serialize scene data.");
    //     }

    //     if (!string.IsNullOrEmpty(sceneString))
    //     {
    //         File.WriteAllText(savePath1, sceneString);
    //         WriteLog("✅ Scene String saved successfully! Path: " + savePath1);
    //     }
    //     else
    //     {
    //         WriteLog("❌ Failed to serialize scene String.");
    //     }
    // }

    // private void WriteLog(string message)
    // {
    //     string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //     string logEntry = $"{timestamp} - {message}\n";
    //     File.AppendAllText(logFilePath, logEntry);
    // }


    public void LoadScene()
    {
        string json = File.ReadAllText(savePath);
        SerializationHelpers.Deserialize(json); // Load scene from JSON
        Debug.Log("✅ Scene loaded successfully!");
    }

}
