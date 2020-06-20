using System.Collections;
using UnityEngine;

public class SegCapture : MonoBehaviour
{
    public string folder = "D://Screenshots/SegImage/";
    public string timeNow = "";
    private bool takeScreenshotOnNextFrame;
    private static SegCapture instance;
    public Camera myCamera;
    public ArrayList ImagesList;

    private void Awake()
    {
        instance = this;
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
            ImagesList.Add(byteArray);
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
        foreach (byte il in ImagesList)
        {
            Debug.Log(il);
        }
        yield return null;
    }

    private void TakeScreenshot(int width, int height, string time)
    {
        Debug.Log("Onmyway");
        timeNow = time;
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height, string time)
    {
        Debug.Log("Segmentation init");
        instance.TakeScreenshot(width, height, time);
    }
}
