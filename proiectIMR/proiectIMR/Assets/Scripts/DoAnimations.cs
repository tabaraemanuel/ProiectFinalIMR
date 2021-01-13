//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARKit;

//[RequireComponent(typeof(ARFace))]
//public class DoAnimations : Singleton<DoAnimations>
//{
//    [SerializeField] private ARKitBlendShapeLocation shapeToTrack = ARKitBlendShapeLocation.EyeBlinkLeft;
//    [SerializeField] private ARKitBlendShapeLocation shapeToTrack2 = ARKitBlendShapeLocation.EyeBlinkRight;

//    private ARKitFaceSubsystem faceSubsystem;

//    private ARFace face;

//    public ARFace Face { get => face; set => face = value; }

//    private void Awake()
//    {
//        face = GetComponent<ARFace>();
//    }

//    private void OnEnable()
//    {
//        var faceManager = FindObjectOfType<ARFaceManager>();

//        if (faceManager != null)
//        {
//            faceSubsystem = (ARKitFaceSubsystem)faceManager.subsystem;
//        }

//        face.updated += OnUpdated;
//    }

//    private void OnDisable()
//    {
//        face.updated -= OnUpdated;
//    }

//    private void OnUpdated(ARFaceUpdatedEventArgs obj)
//    {
//        UpdateFaceFeatures();
//    }

//    private void UpdateFaceFeatures()
//    {
//        using (var blendShapes = faceSubsystem.GetBlendShapeCoefficients(face.trackableId, Unity.Collections.Allocator.Temp))
//        {
//            foreach (var featureCoefficient in blendShapes)
//            {
//                if (featureCoefficient.blendShapeLocation == shapeToTrack)
//                {
//                    Debug.Log("Un ochi a fost inchis!");
//                }
//            }
//        }
//    }
//}
