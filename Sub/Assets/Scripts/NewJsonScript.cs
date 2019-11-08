using SimpleJSON; 
using System.Collections;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NewJsonScript : MonoBehaviour
{
    Texture2D myTexture; // Texture of the raw image which i gonna apply
    public RawImage rawImage_; // Raw image which I used to show in canvas
    string secondUrl; // Url which will be copied from json text
    string resText; // where the download handler text is stored
    string url = "http://dm-dev.southeastasia.cloudapp.azure.com:8080/task"; // url which conatains json data

    // Button Event
    public void ButtonFunction()
    {
        // Get the json values from url 
        StartCoroutine(SendRequest(url));

        // check whether the directory is there or not
        if (!Directory.Exists(Application.dataPath + "/Resources/Textures"))
        {
            // method which create the folder if it not exists
            CreateFolders(Application.dataPath + "/Resources/Textures");

            // Enumerator method for texture process
            StartCoroutine(saveTexture());
        }
        else if(Directory.Exists(Application.dataPath + "/Resources/Textures"))
        {
            Debug.Log("Folder is there");

            // Enumerator method for texture process
            StartCoroutine(saveTexture());
        }
    }

    // Method will get the json values from url using Unity web request
    private IEnumerator SendRequest(string urlOne)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlOne))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Request Error: " + request.error);
            }
            else
            {
                resText = request.downloadHandler.text;

                //Json Node is component from simpleJSON plugin
                JSONNode json = JSONNode.Parse(resText); //resText is parsed to jsonNode's object
                Debug.Log(json["model_list"][0][2]);
                secondUrl = json["model_list"][0][2]; //String which conatains the url located in json's ["model_list"][0th][2nd] position
                Debug.Log(secondUrl);
            }
        }

      
    }
    // creating directory
    void CreateFolders(string f_path)
    {
        DirectoryInfo dir = Directory.CreateDirectory(f_path);
        AssetDatabase.Refresh();
    }

    // Texture is downloaded and applied from the url 
    IEnumerator saveTexture()
    {
        yield return new WaitForSeconds(2f); //Time is used beccause of method process time. 
        Debug.Log(secondUrl);
        
        //Web client component is used for download the file from url and stores it in particular path
        WebClient client = new WebClient();
        client.DownloadFile(secondUrl, @Application.dataPath + "/" + "Resources/Textures/One.png");
        
        //Refreshing the Asset database is must
        AssetDatabase.Refresh();

        //Load texture of image
        //Texture2D is used because it is been applied in UI canvas raw image
        myTexture = (Texture2D)Resources.Load("Textures/One");
        GameObject rawImage_F = GameObject.Find("ShowImage");
        rawImage_F.GetComponent<RawImage>().texture = myTexture;
        Debug.Log("OVER YES");
    }
}
