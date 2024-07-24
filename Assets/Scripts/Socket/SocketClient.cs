using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PersistentSocketClient : MonoBehaviour
{
    private bool running = true;
    private string serverUrl = "http://3.35.214.173:8501/get-string";

    private InputSignLang inputSignLang;

    void Start()
    {
        inputSignLang = GetComponent<InputSignLang>();
        StartCoroutine(GetDataFromServer());
    }

    IEnumerator GetDataFromServer()
    {
        while (running)
        {
            UnityWebRequest request = UnityWebRequest.Get(serverUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string receivedMessage = request.downloadHandler.text;
                Debug.Log("Received from server: " + receivedMessage);

                // Parse the received JSON to get the prediction string
                string prediction = JsonUtility.FromJson<PredictionResponse>(receivedMessage).prediction;
                inputSignLang.inputSign = prediction;
            }

            yield return new WaitForSeconds(1f);  // Slight delay to reduce CPU usage
        }
    }

    void OnApplicationQuit()
    {
        running = false;
    }

    void OnDestroy()
    {
        running = false;
    }

    [Serializable]
    private class PredictionResponse
    {
        public string prediction;
    }
}
