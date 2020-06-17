using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class storeImages : MonoBehaviour
{
    public string segFolder = "D://Screenshots/SegImage/";
    public string folder = "D://Screenshots/Image/";
    public string timeNow = "";

    public string day = System.DateTime.Now.ToString("MM/dd");

    
    public void Start()
    {
        Debug.Log("Started File");
        Debug.Log(folder + System.DateTime.Now.ToString("MM/dd"));
        Debug.Log(folder + "/" + System.DateTime.Now.ToString("MM/dd"));

        if (!System.IO.Directory.Exists(folder + System.DateTime.Now.ToString("MM/dd")))
        {
            var newFold = System.IO.Directory.CreateDirectory(folder + "/" + System.DateTime.Now.ToString("MM/dd"));
            var newSeg = System.IO.Directory.CreateDirectory(segFolder + "/" + System.DateTime.Now.ToString("MM/dd"));
        }
        Debug.Log(System.IO.Directory.Exists(folder + System.DateTime.Now.ToString("MM/dd")));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Image storage in process");

            timeNow = System.DateTime.Now.ToString("MM/dd/hhmmss");
            SegCapture.TakeScreenshot_Static(1920, 1080, timeNow);
            ImageCapture.TakeScreenshot_Static(1920, 1080, timeNow);
        }

    }
}