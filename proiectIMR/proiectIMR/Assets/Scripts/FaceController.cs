using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARFaceManager))]
public class FaceController : MonoBehaviour
{
    private ARFaceManager manager;
    [SerializeField]
    private Button left;
    [SerializeField]
    private Button right;
    [SerializeField]
    public List<Image> images;
    private int index;
    [SerializeField]
    private List<GameObject> models;
    // Start is called before the first frame update

    private void Awake()
    {
        manager = GetComponent<ARFaceManager>();
        index = 0;
        left.onClick.AddListener(SwapFaceLeft);
        right.onClick.AddListener(SwapFaceRight);
        for (int i = 0; i < models.Count; i++)
        {
            var model = models[i];
            var image = images[i];
            image.enabled = (i == index);
            model.SetActive(i == index);
            if (i == index)
                manager.facePrefab = models[i];
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void SwapFaceLeft()
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
        manager.facePrefab = transformToToggle;
    }

    void SwapFaceRight()
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
        manager.facePrefab = transformToToggle;

    }

}
