
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject[] objects;
    private int currentIndex = 1;

    void Start()
    {
        UpdateObjects();
    }

    private void UpdateObjects()
    {
        // Disable all objects first
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        // Enable specific ones based on current index
        switch (currentIndex)
        {
            case 0: // Past
            objects[0].SetActive(true);
                objects[4].SetActive(true);
                objects[5].SetActive(true);
                break;
            case 1: // Present
                objects[1].SetActive(true);
                objects[3].SetActive(true);
                objects[5].SetActive(true);
                break;
            case 2: // Future
                objects[2].SetActive(true);
                objects[3].SetActive(true);
                objects[4].SetActive(true);
                break;
        }

        Debug.Log("Switched to: " + currentIndex);
    }

    public void SwitchPast()
    {
        currentIndex = 0;
        UpdateObjects();
    }

    public void SwitchPresent()
    {
        currentIndex = 1;
        UpdateObjects();
    }

    public void SwitchFuture()
    {
        currentIndex = 2;
        UpdateObjects();
    }
}


// using UnityEngine;
// using System.Collections.Generic;


// public class ObjectSwitcher : MonoBehaviour
// {
//     // Create a custom class to store a row of GameObjects
    
//     public GameObject[] objects; // List of ObjectRows, each containing a List of GameObjects
//     private int currentIndex = 1;

//     void Start()
//     {
//          objects[1].SetActive(true);
//         // Ensure only the first object is active
//         UpdateObjects();
//     }

//     private void UpdateObjects()
//     {
//         if (currentIndex==1){
           
//             objects[3].SetActive(true);
//             objects[5].SetActive(true);
//             objects[0].SetActive(false);
//             objects[2].SetActive(false);
//             objects[4].SetActive(false);
//         }
//         if (currentIndex==0){
            
//             objects[4].SetActive(true);
//             objects[5].SetActive(true);
//             objects[1].SetActive(false);
//             objects[2].SetActive(false);
//             objects[3].SetActive(false);
//         }
//         if (currentIndex==2){
//             // objects[2].SetActive(true);
//             objects[3].SetActive(true);
//             objects[4].SetActive(true);
//             objects[1].SetActive(false);
//             objects[0].SetActive(false);
//             objects[5].SetActive(false);
//         }
        
//     }

//     public void SwitchPresent()
//     {
//         // Loop through the columns and update the index for each rowifcurrentIndex = (currentIndex + 1) % objects.Length; // Assumes all rows have the same length
//         Debug.Log("Switching to index: " + currentIndex);
//          objects[currentIndex].SetActive(false);
//          objects[1].SetActive(true);
//         currentIndex=1;
//         UpdateObjects();
//     }


// public void SwitchPast()
//     {
       
// currentIndex=0;
//         UpdateObjects();
//     }

//     public void SwitchFuture()
//     {
//         objects[currentIndex].SetActive(false);
// objects[2].SetActive(true);
//        currentIndex=2;
//         UpdateObjects();
//     }


//     // void OnMouseDown()  // Detects mouse click on the 3D object
//     // {
//     //     Debug.Log("3D Button Clicked!");
//     //     SwitchObject();
//     // }


// }



// //using UnityEngine;
// //using System.Collections;
// //using System.Collections.Generic;
// //
// //public class ObjectSwitcher : MonoBehaviour
// //{
// //    [System.Serializable]
// //    public class ObjectRow
// //    {
// //        public List<GameObject> objectsInRow;
// //    }
// //
// //    public List<ObjectRow> objects;
// //    private int currentIndex = 0;
// //
// //    public float slideDuration = 0.5f; // How long the slide animation lasts
// //    public Vector3 slideOffset = new Vector3(2f, 0, 0); // Slide direction/offset
// //
// //    private bool isSwitching = false;
// //
// //    void Start()
// //    {
// //        UpdateObjects();
// //    }
// //
// //    public void SwitchObject()
// //    {
// //        if (isSwitching) return;
// //
// //        int nextIndex = (currentIndex + 1) % objects[0].objectsInRow.Count;
// //        StartCoroutine(SlideTransition(currentIndex, nextIndex));
// //        currentIndex = nextIndex;
// //    }
// //
// //    IEnumerator SlideTransition(int fromIndex, int toIndex)
// //    {
// //        isSwitching = true;
// //
// //        for (int row = 0; row < objects.Count; row++)
// //        {
// //            GameObject fromObj = objects[row].objectsInRow[fromIndex];
// //            GameObject toObj = objects[row].objectsInRow[toIndex];
// //
// //            Vector3 originalFromPos = fromObj.transform.position;
// //            Vector3 targetFromPos = originalFromPos + slideOffset;
// //
// //            Vector3 originalToPos = originalFromPos - slideOffset;
// //            Vector3 targetToPos = originalFromPos;
// //
// //            toObj.transform.position = originalToPos;
// //            toObj.SetActive(true);
// //
// //            float t = 0;
// //            while (t < slideDuration)
// //            {
// //                t += Time.deltaTime;
// //                float normalized = Mathf.Clamp01(t / slideDuration);
// //
// //                fromObj.transform.position = Vector3.Lerp(originalFromPos, targetFromPos, normalized);
// //                toObj.transform.position = Vector3.Lerp(originalToPos, targetToPos, normalized);
// //                yield return null;
// //            }
// //
// //            fromObj.SetActive(false);
// //            toObj.transform.position = targetToPos;
// //        }
// //
// //        isSwitching = false;
// //    }
// //
// //    private void UpdateObjects(bool initial = false)
// //    {
// //        for (int i = 0; i < objects.Count; i++)
// //        {
// //            for (int j = 0; j < objects[i].objectsInRow.Count; j++)
// //            {
// //                GameObject obj = objects[i].objectsInRow[j];
// //                obj.SetActive(j == currentIndex || initial);
// //            }
// //        }
// //    }
// //}
