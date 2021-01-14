using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARFaceManager))]
public class FaceController : MonoBehaviour
{
    [SerializeField]
    ARSession session;
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
        GameObject mesh = models[index];
        Debug.Log("HERE " + models.Count);
        manager = GetComponent<ARFaceManager>();
        index = 0;
        left.onClick.AddListener(SwapFaceRight);
        right.onClick.AddListener(SwapFaceLeft);
        for (int i = 0; i < models.Count; i++)
        {
            var model = models[i];
            var image = images[i];
            image.enabled = (i == index);
            if (i == index)
            {
                manager.facePrefab = mesh;
            }

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
        Debug.Log("index= " + index);       
        var current = models[index];
        var currentimage = images[index];
        
        currentimage.enabled = false;
        index--;
        if (index < 0)
        {
            index = models.Count-1;
        }
        var imagetotoggle = images[index];
        var transformToToggle = models[index];
        imagetotoggle.enabled = true;

        GameObject mesh = models[index];    
        manager.facePrefab = mesh;

        session.Reset();

    }

    void SwapFaceRight()
    {
        var current = models[index];
        var currentimage = images[index];
        currentimage.enabled = false;
        index++;
        Debug.Log("index= " + index);
        if (index >= models.Count)
        {
            index = 0;
        }
        var imagetotoggle = images[index];
        var transformToToggle = models[index];

        imagetotoggle.enabled = true;
        GameObject mesh = models[index];
        manager.facePrefab = mesh;

        session.Reset();

    }

}
