using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegCapture : MonoBehaviour
{
    public static string segfolder = "D://Screenshots/SegImage/";
    public string timeNow = "";
    private bool takeScreenshotOnNextFrame;
    private static SegCapture instance;
    
    public Camera mySegCamera;
    public static Dictionary<string, Texture2D> segImagesList = new Dictionary<string, Texture2D>();
    public List<byte> tempList;

    private void Awake()
    {
        instance = this;
        Debug.Log("Hey sexy");
        //myCamera = gameObject.GetComponent<Camera>();
        //myCamera.enabled = true;
    }
    public void OnPostRender()
    {
        if (takeScreenshotOnNextFrame) { 
            takeScreenshotOnNextFrame = false;

            RenderTexture renderTexture = mySegCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            segImagesList.Add(DateTime.Now.Ticks.ToString(), renderResult);

            RenderTexture.ReleaseTemporary(renderTexture);
            mySegCamera.targetTexture = null;
        }
    }

    public static void SaveImages()
    {
        string mmdd = DateTime.Now.ToString("MM/dd");
        foreach (var segtexture2D in segImagesList)
        {
            byte[] segbyteArray = segtexture2D.Value.EncodeToPNG();
            System.IO.File.WriteAllBytes(segfolder + "/" + mmdd + "/" + segtexture2D.Key + ".png", segbyteArray);
        }
        segImagesList.Clear();
    }

    private void TakeScreenshot(int width, int height, string time)
    {
        //Debug.Log("Onmyway");
        timeNow = time;
        mySegCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height, string time)
    {
        //Debug.Log("Segmentation init");
        instance.TakeScreenshot(width, height, time);
    }
}
