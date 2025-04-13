using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScriptASK : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] sceneObjects;
    public GameObject[] sphereObjects;

    
    [SerializeField] public int currentSceneIndex = 1;

    void Start()
    {
        sceneUpdate(currentSceneIndex);
    }


    private void sceneUpdate(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                pastActivated();
                break;
            case 1:
                presentActivated();
                break;
            case 2:
                futureActivated();
                break;
            default:
                presentActivated();
                break;
        }

    }
    // Update is called once per frame
    // void Update()
    // {


    // }


    public void pastActivated()
    {
        sceneObjects[0].SetActive(true);
        sceneObjects[1].SetActive(false);
        sceneObjects[2].SetActive(false);

        sphereObjects[0].SetActive(false);
        sphereObjects[1].SetActive(true);
        sphereObjects[2].SetActive(true);

    }

    public void presentActivated()
    {
        sceneObjects[0].SetActive(false);
        sceneObjects[1].SetActive(true);
        sceneObjects[2].SetActive(false);

        sphereObjects[0].SetActive(true);
        sphereObjects[1].SetActive(false);
        sphereObjects[2].SetActive(true);

    }

    public void futureActivated()
    {
        sceneObjects[0].SetActive(false);
        sceneObjects[1].SetActive(false);
        sceneObjects[2].SetActive(true);

        sphereObjects[0].SetActive(true);
        sphereObjects[1].SetActive(true);
        sphereObjects[2].SetActive(false);

    }
}
