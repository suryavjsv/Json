using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class JsonPost : MonoBehaviour
{
    //In this instead of Text , Text Mesh Pro is used
    //TMP_Text : textfield 
    //TMP_InputField : textbox (Input Field)
    public TMP_Text sts;
    public TMP_InputField userName;
    public TMP_InputField userPass;

    //Destination url
    public string url = "http://192.168.1.16:5000/login";
    private string user;
    private string pass;

    //Button Event for Login Process
    public void validateLogin()
    {

        if ((userName != null) && (userPass != null))
        {       
            user = userName.text; 
            pass = userPass.text;
            StartCoroutine(callLogin(user, pass));
            Debug.Log("Input Field values are passed and Coroutine");
        }
        else
        {
            Debug.Log("Didn't store the data");
        }

    }

    IEnumerator callLogin(string u_name, string p_word)
    {

        Debug.Log("Processing");

        //Important Note : Format of the json data in url 
        var jsonString = "{\"userName\":\"" + user + "\",\"password\":\"" + pass + "\"}";
        print(jsonString);

        //Converting UTF8 string format to byte array
        byte[] byteData = System.Text.Encoding.UTF8.GetBytes(jsonString.ToCharArray());

        //In this UnityWebRequest component is used 
        //Post method is used
        var request = UnityWebRequest.Post(url, jsonString);
        UploadHandlerRaw jsonUploader = new UploadHandlerRaw(byteData);
        jsonUploader.contentType = "application/json";
        request.uploadHandler = jsonUploader;
        yield return request.SendWebRequest();


        if(request.isNetworkError || request.isHttpError)
        {
            yield return request.error;
            sts.text = "check the internet connection";
        }
        else
        {
            yield return request.downloadHandler.text;
        }
        string responseText = request.downloadHandler.text;

        //By using JSONNode component we can Parse the downloadHandlerText as JSON Node and can access the keys and their values 
        JSONNode json = JSONNode.Parse(responseText);
        Debug.Log(json);

        //We can either use ["Key Name"] or [0-9] Index value
        sts.text = json;
        //if(json["msg"]== "Login successful")
        //    SceneManager.LoadScene("ImageLoad", LoadSceneMode.Single);
    }

}
