using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeImages : MonoBehaviour
{
    public string segFolder = "D://Screenshots/SegImage/";
    public string folder = "D://Screenshots/Image/";
    public int imgNum = 1;
    public string fileName;

    public Camera camFront;
    public Camera camRight;
    public Camera camLeft;
    public Camera camBack;
    public Camera segCamFront;
    public Camera segCamRight;
    public Camera segCamLeft;
    public Camera segCamBack;

    public List<string> fileNameList;

    private bool CaptureImages = true;


    public string day = System.DateTime.Now.ToString("MM/dd");

    
    
    public void Start()
    {
        camFront.enabled = true;
        camRight.enabled = true;
        camLeft.enabled = true;
        camBack.enabled = true;
        segCamFront.enabled = true;
        segCamRight.enabled = true;
        segCamLeft.enabled = true;
        segCamBack.enabled = true;
        createDirectory();

        StartCoroutine(CaptureSegments());
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
        fileNameList.Add(timeName);
    }

    public void Update()
    {
        if (System.DateTime.Now.ToString("MM/dd") != day) {
            createDirectory();
        }

        if (Input.GetKeyDown(KeyCode.N))
            CaptureImages = false;

    }

    private IEnumerator CaptureSegments()
    {
        while (true)
        {
            while (CaptureImages)
            {
                captureImages(720, 480, imgNum.ToString());
                yield return new WaitForSeconds(0.2f);
                Debug.Log("Catupring");
            }

            SaveImages();
            CaptureImages = true;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    private void captureImages(int width, int height, string timeNow)
    {
        backLowCapture.TakeScreenshot_Static(width, height, timeNow);
        frontLowCaputre.TakeScreenshot_Static(width, height, timeNow);
        leftCapture.TakeScreenshot_Static(width, height, timeNow);
        rightCapture.TakeScreenshot_Static(width, height, timeNow);

        segBackLowCapture.TakeScreenshot_Static(width, height, timeNow);
        segFrontLowCapture.TakeScreenshot_Static(width, height, timeNow);
        segLeftCapture.TakeScreenshot_Static(width, height, timeNow);
        segRightCapture.TakeScreenshot_Static(width, height, timeNow);
    }

    private void SaveImages()
    {
        backLowCapture.SaveImages();
        frontLowCaputre.SaveImages();
        leftCapture.SaveImages();
        rightCapture.SaveImages();

        segBackLowCapture.SaveImages();
        segLeftCapture.SaveImages();
        segBackLowCapture.SaveImages();
        segFrontLowCapture.SaveImages();

    }
}