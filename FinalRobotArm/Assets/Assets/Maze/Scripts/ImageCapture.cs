using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageCapture : MonoBehaviour
{
    private Camera _camera;
    private int Wd = 480, Ht = 240;

    public string filename;
    void Awake()
    {
        _camera = GetComponent<Camera>();
        if (_camera.targetTexture == null)
        {
            _camera.targetTexture = new RenderTexture(Wd, Ht, 24);
        }
        else
        {
            Wd = _camera.targetTexture.width;
            Ht = _camera.targetTexture.height;
        }
        _camera.gameObject.SetActive(false);
    }



    private void LateUpdate()
    {
        if (_camera.gameObject.activeInHierarchy)
        {
            Capture();

        }
    }

    public void Snapshot(string file)
    {
        filename = file;
        _camera.gameObject.SetActive(true);
    }

    public void Capture()
    {

        Texture2D image = new Texture2D(Wd, Ht, TextureFormat.RGB24, false);
        _camera.Render();
        RenderTexture.active = _camera.targetTexture;
        image.ReadPixels(new Rect(0, 0, Wd, Ht), 0, 0);
        image.Apply();


        byte[] bytes = image.EncodeToPNG();
        //filename = SnapshotName();

        //if (filename != prevFile)
        //{
        //    WriteName();
        //    prevFile = filename;
        //}
        System.IO.File.WriteAllBytes(filename, bytes);

        _camera.gameObject.SetActive(false);
    }

    private string SnapshotName()
    {
        return string.Format("C:/Users/Muguro/Desktop/NeckEMG Data/IMG/img_{1}.png",
            Application.dataPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    private void WriteName()
    {
        TextWriter file = new StreamWriter("C:/Users/Muguro/Desktop/unityIMG2/InputData.csv", true);

        file.Write(filename + "," + Input.GetAxis("Horizontal").ToString() + ","
            + Input.GetAxis("Vertical").ToString() + "\n");

        file.Close();
        //return string.Format("C:/Users/Muguro/Desktop/unityIMG/img_{1}.png",
        //    Application.dataPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
