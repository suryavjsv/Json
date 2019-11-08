using System.IO;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;
using System.Net;

public class JsonGetImage : MonoBehaviour
{
    Texture2D myTextureOne;
    Texture2D myTextureTwo;
    public RawImage downloadablePictureOne;
    public RawImage downloadablePictureTwo;
    public string urlOne = "https://trimg.cardekho.com/model/320x212/Fuelsmarrt-3.jpg";
    public string urlTwo = "https://images-na.ssl-images-amazon.com/images/I/71yeJcouLTL._SL1500_.jpg";

    // Button event for creating Folder in runtime

    public void CreateBtn()
    {
        Debug.Log("Working");
        if (!Directory.Exists(Application.dataPath + "/Resources/Textures/"))
        {
            CreateFolders(Application.dataPath + "/Resources/Textures/");
            Debug.Log("Folder Created");
            Debug.Log(Application.dataPath);
            SaveTwoTexFile();
            Debug.Log("next");
            SaveTextureToFile();
        }
        else if (Directory.Exists(Application.dataPath + "/Resources/Textures/"))
        {
            Debug.Log(Application.dataPath);
            Debug.Log("Folder is already there");
            SaveTwoTexFile();
            Debug.Log("next");
            SaveTextureToFile();
        }
        else
        {
            Debug.Log("Folder not created");
        }

    }

    // This method is called in CreateBtn's method for creating folder
    void CreateFolders(string path)
    {
        DirectoryInfo dir = Directory.CreateDirectory(path);
        AssetDatabase.Refresh();
    }

    // Function to save image in resource (directory)
    public void SaveTextureToFile()
    {
        WebClient client = new WebClient();


        client.DownloadFile(urlOne, @Application.dataPath + "/" + "Resources/Textures/tyreOne.png");
        AssetDatabase.Refresh();
        // Load the texture of first image
        myTextureOne = (Texture2D)Resources.Load("Textures/tyreOne"); //Texture2D because it's a rawImage from UI Canavas
        GameObject rawFirst = GameObject.Find("downloadablePicture_1");
        rawFirst.GetComponent<RawImage>().texture = myTextureOne;
        Debug.Log("DoneOne");

    }

    public void SaveTwoTexFile()
    {
        WebClient m_client = new WebClient();

        m_client.DownloadFile(urlTwo, @Application.dataPath + "/" + "Resources/Textures/tyreTwo.png");
        AssetDatabase.Refresh();
        // Load the texture of second image
        myTextureTwo = (Texture2D)Resources.Load("Textures/tyreTwo"); //Texture2D because it's a rawImage from UI Canavas
        GameObject rawSecond = GameObject.Find("downloadablePicture_2");
        rawSecond.GetComponent<RawImage>().texture = myTextureTwo;
        Debug.Log("DoneTwo");
    }

}
