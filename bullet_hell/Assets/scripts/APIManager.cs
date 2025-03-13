using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
//using System.Web.Script.Serialization;

public class APIManager : MonoBehaviour
{
    static public string baseUrl = "http://localhost/bullet_hell/web";

    public class LoginResponse
    {
        public bool success;
        public string player; //p1 or p2
        public int points;
        public string username;

        public LoginResponse(bool success, string username, int points, string player)
        {
            this.success = success;
            this.username = username;
            this.points = points;
            this.player = player;
        }
    }

    static public IEnumerator Login(string username, string password, string player, Action<LoginResponse> callback)
    {

        //Hashing the password
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

        //Sending request
        UnityWebRequest uwr = UnityWebRequest.Get(baseUrl + loginEndpoint);
        yield return uwr.SendWebRequest();
        LoginResponse response = new LoginResponse(false, "null", 0,player);
        if (uwr.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response.success = true;
            response.username = username;
        }
        else
        {
            Debug.Log("Error: " + uwr.error);
            response.success = false;
        }
        callback?.Invoke(response);
    }
}
