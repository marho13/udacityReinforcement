using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class leftCapture : MonoBehaviour
{
    public static string folder = "D://Screenshots/Image/Left/";
    public string imgNum = "";
    private bool takeScreenshotOnNextFrame;
    private static leftCapture instance;
    public Camera myCamera;
    public static Dictionary<string, Texture2D> ImagesList = new Dictionary<string, Texture2D>();

    private void Awake()
    {
        instance = this;
        //myCamera = gameObject.GetComponent<Camera>();
    }
    public void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;

            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            ImagesList.Add(DateTime.Now.Ticks.ToString(), renderResult);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    public static void SaveImages()
    {
        string mmdd = System.DateTime.Now.ToString("MM/dd");
        foreach (var texture2D in ImagesList)
        {
            byte[] byteArray = texture2D.Value.EncodeToPNG();
            System.IO.File.WriteAllBytes(folder + "/" + mmdd + "/" + texture2D.Key + ".png", byteArray);
        }
        ImagesList.Clear();
    }

    private void TakeScreenshot(int width, int height, string time)
    {
        imgNum = time;
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height, string time)
    {
        instance.TakeScreenshot(width, height, time);
    }
}
