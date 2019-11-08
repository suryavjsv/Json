using SimpleJSON;
using System.Collections;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ThreeD : MonoBehaviour
{
    string url = "http://192.168.1.16:5000/getRoomTyresByName/room1"; // Url string which contain json data
    string secondUrl; // Url string which we're going to get particular data
    string PartUrl= "http://192.168.1.16/img/"; // url constant string it add up with @secondUrl 
    string final_url; // Final url which used to get model or image file
    string ImageUrl, TextureUrl; // Url strings for downloading textures
    public Texture m_Normal;
    MeshRenderer m_renderer;
    
    // co-ordinates which going to use in the transforming of 3d object
    public float x = 0.1f; 
    public float y = 0.1f;
    public float z = 0.1f;
    #region O
   /*public void Button_Tex_clk()
    {
        AssetDatabase.Refresh();
      
        // Make sure to enable keywords
        m_renderer = GetComponentInChildren<Renderer>();
        m_renderer.material.EnableKeyword("_NORMALMAP");
       
        // Set the Normal map using the Texture we assign in inspector window
        m_renderer.material.SetTexture("_BumpMap", m_Normal);
    }*/
    #endregion
    public void Buttn_clk()
    { 
        // check whether the directory is there or not
        if (!Directory.Exists(Application.dataPath + "/Resources/Models")&&(!Directory.Exists(Application.dataPath + "/Resources/Textures")))
        {
            // method which create the folder if it not exists
            CreateFolders(Application.dataPath + "/Resources/Models", Application.dataPath + "/Resources/Textures");

            // Enumerator method for specific json data process
            StartCoroutine(SendRequest(url));
        }
        else if (Directory.Exists(Application.dataPath + "/Resources/Models")&&(Directory.Exists(Application.dataPath + "/Resources/Textures")))
        {
            Debug.Log("Folder is there");

            // Enumerator method for specific json data process
            StartCoroutine(SendRequest(url));
        }
        
        Debug.Log("Next part started");
        // Enumerator method for instantiate 3dmodel from json data 
        StartCoroutine(TimeDelay());        
    }

    // creating directory
    void CreateFolders(string f_path,string t_path)
    {
        DirectoryInfo dir = Directory.CreateDirectory(f_path);
        AssetDatabase.Refresh();

        DirectoryInfo dirr = Directory.CreateDirectory(t_path);
        AssetDatabase.Refresh();
    }

    // Method will get the json values from url using Unity web request
    IEnumerator SendRequest(string urlThree)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Request Error: " + request.error);
            }
            else
            {
               string resText = request.downloadHandler.text;

                //Json Node is component from simpleJSON plugin
                JSONNode json = JSONNode.Parse(resText); //resText is parsed to jsonNode's object
                Debug.Log(json["tyres"][5][6]);
                secondUrl = json["tyres"][5][6]; //String which conatains the url located in json's ["model_list"][0th][2nd] position
                ImageUrl = json["tyres"][5][5];
                Debug.Log(secondUrl); Debug.Log(ImageUrl);
                //concatinating constant url string and current json data string
                final_url = PartUrl + secondUrl;
                TextureUrl = PartUrl + ImageUrl;
            }
        }

    }

    // Enumerator method for instantiate 3dmodel from json data 
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log(final_url);

        //Web client is used for downloading files from url and save it to the specific process
        WebClient client = new WebClient();
        client.DownloadFile(final_url, Application.dataPath + "/" + "Resources/Models/ceatTyre.FBX");
        AssetDatabase.Refresh();
        client.DownloadFile(TextureUrl, Application.dataPath + "/" + "Resources/Textures/baseTxt.png");
        AssetDatabase.Refresh();
        Debug.Log("texture downloaded");

        // If we are loading files in subfolder we should specify the path in Resources.Load("subFolderName/modelName", typeof(GameObject))
        GameObject prefab = Resources.Load("Models/ceatTyre", typeof(GameObject)) as GameObject;
        
        // Scaling gameobject in runtime
        prefab.gameObject.transform.localScale += new Vector3(1, 1, 1);

        // Rotating gameobject in runtime
        prefab.transform.eulerAngles = new Vector3(prefab.transform.eulerAngles.x,prefab.transform.eulerAngles.y + 90,prefab.transform.eulerAngles.z);
        // Instantiating gameobject
       

        AssetDatabase.Refresh();

        // Make sure to enable keywords
        m_renderer = GetComponentInChildren<MeshRenderer>();
        m_renderer.material.EnableKeyword("_NORMALMAP");

        // Set the Normal map using the Texture we assign in inspector window
        m_renderer.material.SetTexture("_BumpMap", m_Normal);
        Instantiate(prefab);

    }
}