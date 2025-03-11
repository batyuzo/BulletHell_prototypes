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

    [Header("playerbody menu refs")]
    public displaySkin playerbodyP1;
    public displaySkin playerbodyP2;

    [Header("ownership refs")]
    public List<musicKit> p1Kits;
    public List<musicKit> p2Kits;
    public List<string> p1Skins;
    public List<string> p2Skins;

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
        menuScreen();
        passedData = passedDataRef;
        musicPlayer = musicPlayerRef;
        musicAssets = musicAssetsRef;
        playerAssets = playerAssetsRef;

        if (Mouse.current != null)
        {
            passedData.p2Device = new InputDevice[] { Keyboard.current, Mouse.current };
        }

        if (Gamepad.current != null)
        {
            passedData.p1Device = new InputDevice[] { Gamepad.current };
        }
    }

    //home screen
    public void menuScreen()//btn_menu
    {
        uiMenu.SetActive(true);
        uiCustomize.SetActive(false);
        uiSettings.SetActive(false);
        //change background
    }
    public void fight()//btn_fight
    {
        passedData.map = maps[Random.Range(0, maps.Count)];
        SceneManager.LoadScene("fight");
    }
    public void loginCallback(APIManager.LoginResponse response)
    {
        if(!response.success)
            return;
        if(response.player == "p1")
        {
            //DATABASE NEEDED
            //passedData.p1Name=database reference
            //passedData.p1Rank=database reference
            //passedData.p1Kits=database reference
            //passedData.p1Skins=database reference

            //HARDCODED DB REFS FOR NOW
            passedData.p1Name = "batyuzo";
            passedData.p1Rank = 515;
            passedData.p1Skins = new List<string> { "bull", "butcher", "knight" };
            passedData.p1Kits = new List<musicKit> { musicAssets.crt1Kit, musicAssets.crt2Kit, musicAssets.muteKit };

            //"if playerPrefs.p1name==p1Name{} then set the following:
            //CURRENTLY SET PREFS
            passedData.p1Skin = "bull";
            passedData.p1Kit = musicAssets.muteKit;
            passedData.p1Login = true;

            btn_player1.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "edit";
            musicPlayer.changePack(passedData.p1Kit, "p1");
            playerbodyP1.skinSwitch(playerAssets, passedData.p1Skin);//in-menu playerbody
        }
        else
        {
            //DATABASE NEEDED
            //passedData.p2Name=database reference
            //passedData.p2Rank=database reference
            //passedData.p2Kits=database reference
            //passedData.p2Skins=database reference

            //HARDCODED DB REFS FOR NOW
            passedData.p2Name = "girmany";
            passedData.p2Rank = 901;
            passedData.p2Skins = new List<string> { "butcher", "rogue", "samurai", };
            passedData.p2Kits = new List<musicKit> { musicAssets.crt1Kit, musicAssets.crt2Kit };

            //"if playerPrefs.p1name==p1Name{} then set the following:
            //CURRENTLY SET PREFS
            passedData.p2Skin = "butcher";
            passedData.p2Kit = musicAssets.crt1Kit;
            passedData.p2Login = true;
            btn_player2.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "edit";
            musicPlayer.changePack(passedData.p2Kit, "p2");
            playerbodyP2.skinSwitch(playerAssets, passedData.p2Skin);//in-menu playerbody
        }
    }
    public void loginP1()//btn_player1
    {
        if (passedData.p2Login)//if both logged in
        {
            btn_fight.interactable = true;
        }
        if (passedData.p1Login)//if logged in
        {
            customizeScreen("p1");
        }
        else//first pressed
        {
            string username = "girmany", password = "baba";
            //LOGIN REQUEST
            StartCoroutine(APIManager.Login(username, password, "p1", loginCallback));
            
        }
    }
    
    public void loginP2()//btn_player2
    {
        if (passedData.p1Login)//if both logged in
        {
            btn_fight.interactable = true;
        }
        if (passedData.p2Login)//if logged in
        {
            customizeScreen("p2");
        }
        else//first pressed
        {
            string username = "girmany", password = "baba";
            //LOGIN REQUEST
            StartCoroutine(APIManager.Login(username, password, "p2", loginCallback));
        }
    }
    public void quit()//btn_quit
    {
        savePrefs();
        Application.Quit();
    }
    public void savePrefs()//on quit
    {
        Debug.Log("preferences saved. well, not yet, but i'm working on it");
    }
    //customize
    public void customizeScreen(string player)//called by logins (when logged in)
    {
        activePlayer = player;
        uiMenu.SetActive(false);
        uiCustomize.SetActive(true);
        uiSettings.SetActive(false);
        customUpdate();//displayed items update
        if (activePlayer == "p1")
        {
            tileManager.init(musicAssets, passedData, activePlayer);
        }
        else if (player == "p2")
        {
            tileManager.init(musicAssets, passedData, activePlayer);
        }
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
    }
}
