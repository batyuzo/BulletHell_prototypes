using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class menu : MonoBehaviour
{
    [Header("maps")]
    public List<string> maps;//for matchmaking

    [Header("script refs")]
    public passedData passedData;
    public musicPlayer musicPlayer;
    public musicAssets musicAssets;
    public playerAssets playerAssets;
    public tileManager tileManager;

    [Header("gameobject refs")]
    public GameObject uiMenu;
    public GameObject uiCustomize;
    public GameObject uiSettings;
    public GameObject uiLogin;

    [Header("playerbody menu refs")]
    public displaySkin playerbodyP1;
    public displaySkin playerbodyP2;

    [Header("ownership refs")]
    public List<musicKit> p1Kits;
    public List<musicKit> p2Kits;
    public List<string> p1Skins;
    public List<string> p2Skins;

    [Header("login refs")]
    public TMP_InputField field_username;
    public TMP_InputField field_password;

    [Header("button refs menu")]
    public UnityEngine.UI.Button btn_fight;
    public UnityEngine.UI.Button btn_player1;//login + customize
    public UnityEngine.UI.Button btn_player2;//login + customize

    [Header("Customize refs")]
    //middle part
    public displaySkin skinInv;
    public TextMeshProUGUI nameInv;
    public SpriteRenderer kitInv;
    //right part
    public TextMeshProUGUI skinDesc;
    public TextMeshProUGUI kitDesc;

    [Header("some help")]
    public string activePlayer;

    public void init(passedData passedDataRef, musicPlayer musicPlayerRef, musicAssets musicAssetsRef, playerAssets playerAssetsRef)
    {

        passedData = passedDataRef;
        musicPlayer = musicPlayerRef;
        musicAssets = musicAssetsRef;
        playerAssets = playerAssetsRef;
        menuScreen();
        if (Mouse.current != null)
        {
            passedData.p2Device = new List<InputDevice> { Keyboard.current, Mouse.current };
        }
        if (Gamepad.current != null)
        {
            passedData.p1Device = new List<InputDevice> { Gamepad.current };
        }
        else { passedData.p1Device = new List<InputDevice> { Keyboard.current, Mouse.current }; }
    }

    //home screen
    public void menuScreen()//btn_menu
    {
        uiMenu.SetActive(true);
        uiCustomize.SetActive(false);
        uiSettings.SetActive(false);
        uiLogin.SetActive(false);
        menuUpdate();
    }
    public void fight()//btn_fight
    {
        passedData.map = maps[Random.Range(0, maps.Count)];
        SceneManager.LoadScene("fight");
    }
    public void LoadOwnedMusic(APIManager.AssetResponse response)
    {
        if (!response.success)
            return;
        if (response.player == "p1")
        {
            List<musicKit> kits = new List<musicKit>();
            musicKit activeKit = null;
            foreach (KeyValuePair<string, bool> kit in response.ownedAssetName)
            {
                kits.Add(musicAssets.GetAssetByName(kit.Key));
                if (kit.Value)
                {
                    activeKit = musicAssets.GetAssetByName(kit.Key);
                }
            }
            //set owned kits
            if (kits!=null)
            {
                passedData.p1Kits = kits;
            }

            //set active kit
            if (activeKit!=null)
            {
                passedData.p1Kit = activeKit;
            }
        }
        else
        {
            List<musicKit> kits = new List<musicKit>();
            musicKit activeKit = null;
            foreach (KeyValuePair<string, bool> kit in response.ownedAssetName)
            {
                kits.Add(musicAssets.GetAssetByName(kit.Key));
                if (kit.Value)
                    activeKit = musicAssets.GetAssetByName(kit.Key);
            }
            //set owned kits
            if (kits != null)
            {
                passedData.p2Kits = kits;
            }

            //set active kit
            if (activeKit != null)
            {
                passedData.p2Kit = activeKit;
            }
        }
    }
    public void LoadOwnedCharacters(APIManager.AssetResponse response)
    {
        if (!response.success)
            return;
        if (response.player == "p1")
        {
            List<string> skins = new List<string>();
            string activeSkin = null;
            foreach (KeyValuePair<string, bool> skin in response.ownedAssetName)
            {
                skins.Add(skin.Key);
                if (skin.Value)
                {
                    activeSkin = skin.Key;
                }
            }
            if (skins != null)
            {
                passedData.p1Skins = skins;
            }
            else
            {
                passedData.p1Skins = new List<string> { "knight" };
            }

            if (activeSkin != null)
            {
                passedData.p1Skin = activeSkin;
            }
            else
            {
                passedData.p1Skin = "knight";
            }
        }
        else//p2
        {
            List<string> skins = new List<string>();
            string activeSkin = null;
            foreach (KeyValuePair<string, bool> skin in response.ownedAssetName)
            {
                skins.Add(skin.Key);
                if (skin.Value)
                {
                    activeSkin = skin.Key;
                }
            }
            if (skins != null)
            {
                passedData.p2Skins = skins;
            }
            else
            {
                passedData.p2Skins = new List<string> { "knight" };
            }

            if (activeSkin != null)
            {
                passedData.p2Skin = activeSkin;
            }
            else
            {
                passedData.p2Skin = "knight";
            }
        }
    }
    public void loginP1()//btn_player1
    {
        if (passedData.p1Login)//if logged in
        {
            customizeScreen("p1");
        }
        else//first pressed
        {
            loginScreen("p1");
        }
    }
    public void loginP2()//btn_player2
    {
        if (passedData.p2Login)//if logged in
        {
            customizeScreen("p2");
        }
        else//first pressed
        {
            loginScreen("p2");
        }
    }
    public void loginScreen(string player)
    {
        uiMenu.SetActive(false);
        uiCustomize.SetActive(false);
        uiSettings.SetActive(false);
        uiLogin.SetActive(true);
        activePlayer = player;
        field_username.text = null;
        field_password.text = null;
    }
    public void checkLogin()//btn_login on loginScreen
    {
        Debug.Log(field_username.text + " and " + field_password.text);
        loginPlayer(field_username.text, field_password.text);
    }
    public void loginSuccess(LoginResponse response)
    {
        menuScreen();

         if (!response.success)
            return;

        if (response.player == "p1")
            passedData.p1Login = true;
        else
            passedData.p2Login = true;
        if (response.player == "p1")
        {
            passedData.p1Name = response.username;
            passedData.p1Rank = response.points;

            //Loading owned music
            StartCoroutine(APIManager.GetOwnedMusic(response.username, "p1", LoadOwnedMusic));
            //Loading owned characters
            StartCoroutine(APIManager.GetOwnedCharacters(response.username, "p1", LoadOwnedCharacters));
            //"if playerPrefs.p1name==p1Name{} then set the following:
            //CURRENTLY SET PREFS
            passedData.p1Login = true;
            btn_player1.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "edit";
            musicPlayer.changePack(passedData.p1Kit, "p1");
            playerbodyP1.skinSwitch(playerAssets, passedData.p1Skin);//in-menu playerbody
        }
        else
        {
            passedData.p2Name = response.username;
            passedData.p2Rank = response.points;

            //Loading owned music
            StartCoroutine(APIManager.GetOwnedMusic(response.username, "p2", LoadOwnedMusic));
            //Load owned characters
            StartCoroutine(APIManager.GetOwnedCharacters(response.username, "p2", LoadOwnedCharacters));
            //"if playerPrefs.p1name==p1Name{} then set the following:
            //CURRENTLY SET PREFS
            passedData.p2Login = true;
            btn_player2.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "edit";
            musicPlayer.changePack(passedData.p2Kit, "p2");
            playerbodyP2.skinSwitch(playerAssets, passedData.p2Skin);//in-menu playerbody
        }
    }
    public void loginFailure(LoginResponse response)
    {
        loginScreen(activePlayer);
        Debug.Log("Login authentication failed");
    }
    public void loginPlayer(string username, string password)
    {
        if (username != passedData.p1Name && username != passedData.p2Name)
        {
            //Getting hold of username and password
            if (true)
            {
                loginSuccess(new APIManager.LoginResponse(true, username, 50, activePlayer));
                return true;
            }
            else
            {
                //LOGIN REQUEST
                StartCoroutine(APIManager.Login(username, password, activePlayer, loginSuccess, loginFailure));
            }
        }else{
            // Show login error
            Debug.Log("This user is already logged in");
        }
    }
    public void quit()//btn_quit
    {
        Application.Quit();
        Debug.Log("game closed");
    }
    //customize
    public void customizeScreen(string player)//called by logins (when logged in)
    {
        activePlayer = player;
        uiMenu.SetActive(false);
        uiCustomize.SetActive(true);
        uiSettings.SetActive(false);
        uiLogin.SetActive(false);

        customUpdate();//displayed items update
        tileManager.init(musicAssets, passedData, activePlayer);
    }
    public void customUpdate()
    {
        if (activePlayer == "p1")
        {
            //skin update
            skinInv.skinSwitch(playerAssets, passedData.p1Skin);
            setSkinDesc(passedData.p1Skin);
            //name update
            nameInv.text = passedData.p1Name;
            //musicKit update
            kitInv.sprite = passedData.p1Kit.coverart;
            kitDesc.text = passedData.p1Kit.desc;
        }
        else if (activePlayer == "p2")
        {
            //skin update
            skinInv.skinSwitch(playerAssets, passedData.p2Skin);
            setSkinDesc(passedData.p2Skin);
            //name update
            nameInv.text = passedData.p2Name;
            //musicKit update
            kitInv.sprite = passedData.p2Kit.coverart;
            kitDesc.text = passedData.p2Kit.desc;
        }
    }
    public void menuUpdate()
    {

        if (passedData.p2Login && passedData.p1Login)//if both logged in
        {
            btn_fight.interactable = true;
        }
        if (passedData.p1Name != "p1")
        {
            playerbodyP1.skinSwitch(playerAssets, passedData.p1Skin);
        }
        if (passedData.p2Name != "p2")
        {
            playerbodyP2.skinSwitch(playerAssets, passedData.p2Skin);
        }
    }
    public void setSkinDesc(string skin)
    {
        if (skin == "bull")
        {
            skinDesc.text = playerAssets.bull_desc;
        }
        else if (skin == "butcher")
        {
            skinDesc.text = playerAssets.butcher_desc;
        }
        else if (skin == "knight")
        {
            skinDesc.text = playerAssets.knight_desc;
        }
        else if (skin == "entity")
        {
            skinDesc.text = playerAssets.entity_desc;
        }
        else if (skin == "rogue")
        {
            skinDesc.text = playerAssets.rogue_desc;
        }
        else if (skin == "samurai")
        {
            skinDesc.text = playerAssets.samurai_desc;
        }
    }
    //---music selection---
    public void selectBull()
    {
        tileManager.selectSkin(activePlayer, "bull");//passedData update here
        skinDesc.text = playerAssets.bull_desc;
        customUpdate();
    }
    public void selectButcher()
    {
        tileManager.selectSkin(activePlayer, "butcher");
        skinDesc.text = playerAssets.butcher_desc;
        customUpdate();
    }
    public void selectKnight()
    {
        tileManager.selectSkin(activePlayer, "knight");
        skinDesc.text = playerAssets.knight_desc;
        customUpdate();
    }
    public void selectEntity()
    {
        tileManager.selectSkin(activePlayer, "entity");
        skinDesc.text = playerAssets.entity_desc;
        customUpdate();
    }
    public void selectRogue()
    {
        tileManager.selectSkin(activePlayer, "rogue");
        skinDesc.text = playerAssets.rogue_desc;
        customUpdate();
    }
    public void selectSamurai()
    {
        tileManager.selectSkin(activePlayer, "samurai");
        skinDesc.text = playerAssets.samurai_desc;
        customUpdate();
    }
    //--------------------
    //---music selection---
    public void selectCrt1()
    {
        tileManager.selectKit(activePlayer, musicAssets.crt1Kit);
        customUpdate();
    }
    public void selectCrt2()
    {
        tileManager.selectKit(activePlayer, musicAssets.crt2Kit);
        customUpdate();
    }
    public void selectHellstar()
    {
        tileManager.selectKit(activePlayer, musicAssets.hellstarKit);
        customUpdate();
    }
    public void selectMute()
    {
        tileManager.selectKit(activePlayer, musicAssets.muteKit);
        customUpdate();
    }
    //--------------------
    public void reshuffle()//btn_reshuffle
    {
        musicPlayer.playMusic("menu");
    }
    //settings
    public void settingsScreen()//btn_settings
    {
        uiMenu.SetActive(false);
        uiCustomize.SetActive(false);
        uiSettings.SetActive(true);
        //update buttons n things here

        passedData.p1Device = new List<InputDevice> { Keyboard.current, Mouse.current };
        passedData.p2Device = new List<InputDevice> { Gamepad.current };
        ;

    }
}