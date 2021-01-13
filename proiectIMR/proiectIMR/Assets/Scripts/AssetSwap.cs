using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class AssetSwap : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> models;
    [SerializeField]
    private List<Image> images;
    private int index;
    ARFaceManager facemanager;
    // Start is called before the first frame update
    void Start()
    {

        index = 0;
        Debug.Log(models.Count);
        facemanager = GameObject.FindObjectOfType(typeof(ARFaceManager)) as ARFaceManager;
        for (int i = 0; i < models.Count; i++)
        {
            var model = models[i];
            var image = images[i];
            if (i == index)
                facemanager.facePrefab = models[i];
            image.enabled = (i == index);
            model.SetActive(i == index);
        }

    }

    public void EnableModelRight()
    {
        var current = models[index];
        var currentimage = images[index];
        current.SetActive(false);
        currentimage.enabled = false;
        index++;
        if (index > 2)
        {
            index = 0;
        }
        var imagetotoggle = images[index];
        var transformToToggle = models[index];
        transformToToggle.SetActive(true);
        imagetotoggle.enabled = true;
        facemanager.facePrefab = models[index];
    }

    public void EnableModelLeft()
    {
        var current = models[index];
        var currentimage = images[index];
        current.SetActive(false);
        currentimage.enabled = false;
        index--;
        if (index < 0)
        {
            index = 2;
        }
        var imagetotoggle = images[index];
        var transformToToggle = models[index];
        imagetotoggle.enabled = true;
        transformToToggle.SetActive(true);
        facemanager.facePrefab = models[index];
    }
    public void InnactivateElemets()
    {
        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetActive(false);
        }
    }
    public void ActivateElements()
    {
        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetActive(true);
        }
    }

}