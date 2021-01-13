using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Android;

public class PhoneCamera : MonoBehaviour
{

	private bool camAvailable;
	private WebCamTexture cameraTexture;
	private Texture defaultBackground;

	public RawImage background;
	public AspectRatioFitter fit;
	public bool frontFacing;

	// Use this for initialization
	void Start()
	{
		if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
		{
			Permission.RequestUserPermission(Permission.Camera);
		}

		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length == 0)
			return;

		for (int i = 0; i < devices.Length; i++)
		{
			var curr = devices[i];

			if (!curr.isFrontFacing == frontFacing)
			{
				cameraTexture = new WebCamTexture(curr.name, Screen.width, Screen.height);
				break;
			}
		}

		if (cameraTexture == null)
			return;

		cameraTexture.Play(); // Start the camera
		background.texture = cameraTexture; // Set the texture
		background.transform.Rotate(Vector3.forward, -90f);

		camAvailable = true; // Set the camAvailable for future purposes.
	}

	// Update is called once per frame
	void Update()
	{


        float scaleY = cameraTexture.videoVerticallyMirrored ? -1f : 1f; // Find if the camera is mirrored or not
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f); // Swap the mirrored camera

        int orient = -cameraTexture.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

    }
	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 100), "switch"))
		{
			WebCamDevice[] devices = WebCamTexture.devices;
			cameraTexture.Stop();
			cameraTexture.deviceName = (cameraTexture.deviceName == devices[0].name) ? devices[1].name : devices[0].name;
			cameraTexture.Play();

		}
	}
}