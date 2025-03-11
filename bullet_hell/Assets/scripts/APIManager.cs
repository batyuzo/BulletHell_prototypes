using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    static public string baseUrl = "http://localhost/bullet_hell/web";

    public class LoginResponse
    {
        public bool success;
        public string username;
    }

    static public IEnumerator Login(string username, string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        SHA512 shaM = new SHA512Managed();
        byte[] hashBytes = shaM.ComputeHash(passwordBytes);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            builder.Append(hashBytes[i].ToString("x2"));
        }
        string hash = builder.ToString();
        string loginEndpoint = $"/login/login_check.php?username={username}&password={hash}";
        UnityWebRequest uwr = UnityWebRequest.Get(baseUrl + loginEndpoint);
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error: " + uwr.error);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
