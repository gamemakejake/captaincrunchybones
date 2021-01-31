using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour
{
    //public int FileCounter = 0;
    public GameObject picture;
    public Texture thing;
    // Start is called before the first frame update

    //private void LateUpdate()
    //{
    //    if (Input.GetKeyDown(KeyCode.F9))
    //    {
    //        CamCapture();
    //    }
    //}

    public void CamCapture()
    {
        Camera Cam = GetComponent<Camera>();

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = Cam.targetTexture;
        //Debug.Log(Cam.targetTexture.width);

        Cam.Render();

        Texture2D Image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height);
        Image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
        Image.Apply();
        RenderTexture.active = currentRT;
        //var Bytes = Image.EncodeToPNG();

        //File.WriteAllBytes(Application.dataPath + "/" + FileCounter + ".png", Bytes);

        picture.GetComponent<CanvasRenderer>().SetTexture(Image);
    }

    public void EraseImage()
    {
        picture.GetComponent<CanvasRenderer>().SetTexture(null);
    }
}
