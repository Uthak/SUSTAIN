using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
    GameObject UI;
    [SerializeField] AudioSource CameraShutter_Sound;

    private IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../Screenshots/Screenshot.png", bytes);

        //UI wieder sichbar machen
        UI.SetActive(true);
    }

    public void MakeScreenshot()
    {
        //UI ausschalten für Screenshot
        if (CameraShutter_Sound != null)
        {
            CameraShutter_Sound.Play();
        }
        UI = GameObject.Find("SceneObjects");
        UI.SetActive(false);

        StartCoroutine("Screenshot");
        Debug.Log("takedScreenshot");
    }
}
