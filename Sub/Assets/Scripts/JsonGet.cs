using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

public class JsonGet : MonoBehaviour
{

    string urlOne= "http://dm-dev.southeastasia.cloudapp.azure.com:8080/task";
    string jsonData;
    public void btn_clk()
    {
        StartCoroutine(GetValues());
    }
    // Use this for initialization
    IEnumerator GetValues()
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
                string resText = request.downloadHandler.text;

                //Json Node is component from simpleJSON plugin
                JSONNode json = JSONNode.Parse(resText); //resText is parsed to jsonNode's object
                Debug.Log(json);
                Debug.Log("Model List 0 " + json["model_list"][0]);

                // get values at the nodes, to get values at node either use the name directly or the position number of the node
                // here instead of "model_list", you can also write jsonNode[0][0] 

                // get individual values by using their key name or key index position
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][0][1].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][0][2].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][0][3].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][0][4].ToString());

                Debug.Log("JsonNode " + 0 + " " + json["model_list"][1][1].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][1][2].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][1][3].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][1][4].ToString());

                Debug.Log("JsonNode " + 0 + " " + json["model_list"][2][1].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][2][2].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][2][3].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][2][4].ToString());

                Debug.Log("JsonNode " + 0 + " " + json["model_list"][3][1].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][3][2].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][3][3].ToString());
                Debug.Log("JsonNode " + 0 + " " + json["model_list"][3][4].ToString());

            }
        }
        
    }
}