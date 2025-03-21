using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public class AssetResponse
    {
        public Dictionary<string, bool> ownedAssetName;
        public bool success;
        public string player;

        public AssetResponse(Dictionary<string, bool> ownedAssetName, bool success, string player)
        {
            this.ownedAssetName = ownedAssetName;
            this.success = success;
            this.player = player;
        }
    }
    static public IEnumerator GetOwnedCharacters(string username, string player, Action<AssetResponse> callback)
    {
        //Define the api endpoint
        string endpoint = $"/profile/get_player_characters.php?username={username}";
        UnityWebRequest uwr = UnityWebRequest.Get(baseUrl + endpoint);
        yield return uwr.SendWebRequest();
        AssetResponse response = new AssetResponse(new Dictionary<string, bool>(), false, ""); // Initialize dictionary

        if (uwr.result == UnityWebRequest.Result.Success)
        {

            //Parse JSON using SimpleJSON
            JSONNode json = JSON.Parse(uwr.downloadHandler.text);
            if (json != null)
            {
                //Extract data from JSON
                response.success = true;
                response.player = player;

                if (json.IsArray)
                {
                    foreach (JSONNode item in json.AsArray)
                    {
                        // Check for both "name" and "active" keys, and their correct types.
                        if (item.HasKey("name") && item["name"].IsString &&
                            item.HasKey("active") && item["active"].IsNumber)
                        {
                            string name = item["name"];
                            int active = item["active"];
                            Debug.Log("Asset: " + name + "\t Active: " + active);
                            response.ownedAssetName.Add(name, Convert.ToBoolean(active)); // Add both name and active status
                        }
                        else
                        {
                            Debug.LogWarning("Item in array is missing 'name' or 'active' property, or has incorrect types: " + item.ToString());
                        }
                    }
                }
                else
                {
                    Debug.LogError("Expected JSON array, but received: " + json.Tag.ToString());
                    response.success = false;
                }
            }
            else
            {
                Debug.LogError("Failed to parse JSON response.");
                response.success = false;
            }
        }
        else
        {
            Debug.Log("Error: " + uwr.error + " - " + username);
            response.success = false;
        }
        callback(response); // Call the callback
    }
    static public IEnumerator GetOwnedMusic(string username, string player, Action<AssetResponse> callback)
    {
        //Define the api endpoint
        string endpoint = $"/profile/get_player_musics.php?username={username}";
        UnityWebRequest uwr = UnityWebRequest.Get(baseUrl + endpoint);
        yield return uwr.SendWebRequest();
        AssetResponse response = new AssetResponse(new Dictionary<string, bool>(), false, ""); // Initialize dictionary

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log("Received: " + uwr.downloadHandler.text);

            //Parse JSON using SimpleJSON
            JSONNode json = JSON.Parse(uwr.downloadHandler.text);
            if (json != null)
            {
                //Extract data from JSON
                response.success = true;
                response.player = player;

                if (json.IsArray)
                {
                    foreach (JSONNode item in json.AsArray)
                    {
                        // Check for both "name" and "active" keys, and their correct types.
                        if (item.HasKey("name") && item["name"].IsString &&
                            item.HasKey("active") && item["active"].IsNumber)
                        {
                            string name = item["name"];
                            int active = item["active"];
                            Debug.Log("Asset: " + name + "\t Active: " + active);
                            response.ownedAssetName.Add(name, Convert.ToBoolean(active)); // Add both name and active status
                        }
                        else
                        {
                            Debug.LogWarning("Item in array is missing 'name' or 'active' property, or has incorrect types: " + item.ToString());
                        }
                    }
                }
                else
                {
                    Debug.LogError("Expected JSON array, but received: " + json.Tag.ToString());
                    response.success = false;
                }
            }
            else
            {
                Debug.LogError("Failed to parse JSON response.");
                response.success = false;
            }
        }
        else
        {
            Debug.Log("Error: " + uwr.error + " - " + username);
            response.success = false;
        }
        callback(response); // Call the callback
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

        LoginResponse response = new LoginResponse(false, "null", 0, player);

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            //Parse JSON using SimpleJSON
            JSONNode json = JSON.Parse(uwr.downloadHandler.text);

            if (json != null)
            {
                //Extract data from JSON
                response.success = true;
                response.username = json["username"];
                response.points = json["points"].AsInt;
            }
            else
            {
                Debug.LogError("Failed to parse JSON response.");
                response.success = false;
            }
        }
        else
        {
            Debug.Log("Error in login: " + uwr.error);
            response.success = false;
        }
        callback?.Invoke(response);
    }
}