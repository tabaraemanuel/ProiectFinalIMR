using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System;
using UnityEngine.SceneManagement;
public class HideGUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> models;
    [SerializeField]
    private List<Image> images;
    private int index;
    public Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
    
    

    }
    public void LoadPicScene(string path)
    {
        SceneManager.LoadScene(3);
        Sprite sprite = Resources.Load<Sprite>(path);
    }
    public void InactivateGUI()
    {
        index = 0;

        for (int i = 0; i < models.Count; i++)
        {
            var model = models[i];
            var image = images[i];

            image.enabled = false;
            model.SetActive(false);
        }

    }
    public void ActivateGUI()
    {
        index = 0;

        for (int i = 0; i < models.Count; i++)
        {
            var model = models[i];
            var image = images[i];
            image.enabled = (i == index);
            model.SetActive(true);
        }
    }
    public void TakePic()
    {
        bool index = false;
        InactivateGUI();

        //NativeToolkit.SaveScreenshot("MyScreenshot", "MyScreenshotFolder", "jpeg");
        NativeToolkit.SaveImage(texture, "MyImage", "png");
        NativeToolkit.PickImage();


        ActivateGUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
