﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCapture : MonoBehaviour
{
    public string folder = "D://Screenshots/Image/";
    public string timeNow = "";
    private bool takeScreenshotOnNextFrame;
    private static ImageCapture instance;
    private Camera myCamera;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }
    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            Debug.Log(timeNow);
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(folder + "/" + timeNow + ".png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;

        }
    }

    private void TakeScreenshot(int width, int height, string time)
    {
        timeNow = time;
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
        Debug.Log("Taking Screenshot");
    }

    public static void TakeScreenshot_Static(int width, int height, string time)
    {
        Debug.Log("In static");
        instance.TakeScreenshot(width, height, time);
    }
}
