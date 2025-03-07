using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
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
    //left part
    public tileManager tiles;
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
    }

    //home screen
    public void menuScreen()//btn_menu
    {
        uiMenu.SetActive(true);
        uiCustomize.SetActive(false);
        uiSettings.SetActive(false);
        //change background
        //update gameobjects
    }
    public void fight()//btn_fight
    {
        passedData.map = maps[Random.Range(0, 3)];
        SceneManager.LoadScene("fight");
    }
    public void loginP1()//btn_player1
    {
        if (passedData.p1Name != "p1")//if logged in
        {

            customizeScreen("p1");
            if (passedData.p2Name != "p2")//if both logged in
            {
                btn_fight.interactable = true;
            }
        }
        //DATABASE NEEDED
        //p1Name=database reference
        //p1Rank=database reference
        //p1Kits=database reference
        //p1Skins=database reference

        //HARDCODED DB REFS FOR NOW
        passedData.p1Name = "batyuzo";
        passedData.p1Rank = 515;
        p1Skins = new List<string> { "entity", "samurai", "butcher" };
        p1Kits = new List<musicKit> { musicAssets.crt1Kit, musicAssets.muteKit };

        //PLAYERPREFS NEEDED
        //"if playerPrefs.p1name==p1Name{} then set the following:
        passedData.p1Skin = "bull";
        passedData.p1SkinDesc = playerAssets.bull_desc;
        passedData.p1Kit = musicAssets.muteKit;

        musicPlayer.changePack(passedData.p1Kit, "p1");
        btn_player1.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "edit";
        playerbodyP1.skinSwitch(playerAssets, passedData.p1Skin);//in-menu playerbody
    }
    public void loginP2()//btn_player2
    {
        if (passedData.p2Name != "p2")//if logged in
        {
            customizeScreen("p2");
            if (passedData.p2Name != "p1")//if both logged in
            {
                btn_fight.interactable = true;
            }
        }
        //DATABASE NEEDED
        //p2Name=database reference
        //p2Rank=database reference
        //p2Kits=database reference
        //p2Skins=database reference

        //HARDCODED DB REFS FOR NOW
        passedData.p2Name = "girmany";
        passedData.p2Rank = 901;
        p2Skins = new List<string> { "entity", "samurai", "butcher" };
        p2Kits = new List<musicKit> { musicAssets.crt1Kit, musicAssets.hellstarKit };

        //PLAYERPREFS NEEDED
        //"if playerPrefs.p1name==p1Name{} then set the following:
        passedData.p2Skin = "butcher";
        passedData.p2SkinDesc = playerAssets.bull_desc;
        passedData.p2Kit = musicAssets.hellstarKit;

        musicPlayer.changePack(passedData.p2Kit, "p2");
        btn_player2.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "edit";
        playerbodyP2.skinSwitch(playerAssets, passedData.p2Skin);//in-menu playerbody
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
            tileManager.init(musicAssets, passedData, p1Kits, p1Skins, activePlayer);
        }
        else if (player == "p2")
        {
            tileManager.init(musicAssets, passedData, p2Kits, p2Skins, activePlayer);
        }


    }

    public void customUpdate()
    {
        if (activePlayer == "p1")
        {
            //skin update
            skinInv.skinSwitch(playerAssets, passedData.p1Skin);
            skinDesc.text = passedData.p1SkinDesc;
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
            kitInv.sprite = passedData.p2Kit.coverart;
            //name update
            nameInv.text = passedData.p2Name;
            //musicKit update
            skinDesc.text = passedData.p2SkinDesc;
            kitDesc.text = passedData.p2Kit.desc;
        }
    }

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

    public string getSkinDesc(string skin)
    {
        if (skin == "bull")
        {
            return playerAssets.bull_desc;
        }
        else if (skin == "butcher")
        {
            return playerAssets.butcher_desc;
        }
        else if (skin == "knight")
        {
            return playerAssets.knight_desc;
        }
        else if (skin == "entity")
        {
            return playerAssets.entity_desc;
        }
        else if (skin == "rogue")
        {
            return playerAssets.rogue_desc;
        }
        else if (skin == "samurai")
        {
            return playerAssets.samurai_desc;
        }
        else return null;
    }
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
