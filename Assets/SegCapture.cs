using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegCapture : MonoBehaviour
{
    public string folder = "D://Screenshots/SegImage/";
    public string timeNow = "";
    private bool takeScreenshotOnNextFrame;
    private static SegCapture instance;
    public Camera myCamera;
    public List<List<byte>> ImagesList;
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

            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            Debug.Log(byteArray.Length);
            foreach (byte b in byteArray)
            {
                tempList.Add(b);
            }
            ImagesList.Add(tempList);
            Debug.Log(ImagesList.Count);
            //System.IO.File.WriteAllBytes(folder + "/" + timeNow + ".png", byteArray);
            //Debug.Log("Saved CameraScreenshot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
            Debug.Log("Segmentation done");
        }
    }

    IEnumerator saveImages()
    {
        foreach (List<byte> image in imageList)
        {
            foreach (byte il in image)
            {
                Debug.Log(il);
            }
        }
        yield return null;
    }

    private void TakeScreenshot(int width, int height, string time)
    {
        //Debug.Log("Onmyway");
        timeNow = time;
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height, string time)
    {
        //Debug.Log("Segmentation init");
        instance.TakeScreenshot(width, height, time);
    }
}
