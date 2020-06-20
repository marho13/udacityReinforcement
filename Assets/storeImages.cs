using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityScript.Lang;

public class storeImages : MonoBehaviour
{
    public string segFolder = "D://Screenshots/SegImage/";
    public string folder = "D://Screenshots/Image/";
    public string timeNow;
    public string fileName;

    public Camera cam;
    public Camera segCam;

    public List<string> fileNameList;


    public string day = System.DateTime.Now.ToString("MM/dd");

    
    
    public void Start()
    {
        cam.enabled = false;
        segCam.enabled = true;
        createDirectory();
    }

    private void createDirectory() 
    {
        if (!System.IO.Directory.Exists(folder + System.DateTime.Now.ToString("MM/dd")))
        {
            System.IO.Directory.CreateDirectory(folder + "/" + System.DateTime.Now.ToString("MM/dd"));
            System.IO.Directory.CreateDirectory(segFolder + "/" + System.DateTime.Now.ToString("MM/dd"));
        }
    }

    public void AddFileName(string timeName) 
    {
        Debug.Log(timeName);
        fileNameList.Add(timeName);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            cam.enabled = !cam.enabled;
            segCam.enabled = !segCam.enabled;
        }
        if (System.DateTime.Now.ToString("MM/dd") != day) {
            createDirectory();
        }

        //AddFileName(System.DateTime.Now.ToString("hhmmss"));
        timeNow = System.DateTime.Now.ToString("MM/dd/hhmmss");
        AddFileName(timeNow);
        SegCapture.TakeScreenshot_Static(720, 480, timeNow);
        //ImageCapture.TakeScreenshot_Static(720, 480, timeNow);
        
    }
}