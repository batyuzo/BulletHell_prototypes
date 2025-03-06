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

    [Header("variables")]
    string player1;
    string player2;
    enum Players
    {
        Player1,
        Player2,
    }
    public List<string> maps;//for matchmaking

    [Header("script refs")]
    public passedData passedData;
    public musicPlayer musicPlayer;
    public musicAssets musicAssets;
    public playerAssets playerAssets;

    [Header("gameobject refs")]
    public GameObject uiMenu;
    public GameObject uiCustomize;
    public GameObject uiSettings;

    [Header("playerbody menu refs")]
    public displaySkin playerbodyP1;
    public displaySkin playerbodyP2;

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


    public void init(passedData passedDataRef, musicPlayer musicPlayerRef, musicAssets musicAssetsRef, playerAssets playerAssetsRef)
    {
        menuScreen();
        passedData = passedDataRef;
        musicPlayer = musicPlayerRef;
        musicAssets = musicAssetsRef;
        playerAssets = playerAssetsRef;
    }

    //------BUTTONS FUNCTIONALITY-------
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

    public void LoginPlayer1()//btn_player1
    {
        if (passedData.p1Name != "p1")//if logged in
        {
            customizeScreen("p1");
        }
        Login(Players.Player1);
        //database here
        passedData.p1Name = "batyuzo";
        passedData.p1Skin = "bull";
        passedData.p1Rank = "200";
        btn_player1.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "edit";
        playerbodyP1.skinSwitch(playerAssets, passedData.p1Skin);

    }

    public void LoginPlayer2()//btn_player1
    {
        if (passedData.p2Name != "p2")//if logged in
        {
            customizeScreen("p2");
        }
        Login(Players.Player2);
        //database here
        passedData.p2Name = "girmany";
        passedData.p2Skin = "rogue";
        passedData.p1Rank = "130";
        btn_player2.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "edit";
        playerbodyP2.skinSwitch(playerAssets, passedData.p2Skin);

    }

    public void quit()//btn_quit
    {
        Application.Quit();
    }

    //customize
    public void customizeScreen(string player)//called by logins (when logged in)
    {
        uiMenu.SetActive(false);
        uiCustomize.SetActive(true);
        uiSettings.SetActive(false);
        //change background
        //enable and update gameobjects
        customUpdate(player);
    }

    public void customUpdate(string player)
    {
        if (player == "p1")
        {
            skinInv.skinSwitch(playerAssets, passedData.p1Skin);
            nameInv.text = passedData.p1Name;
            kitInv.sprite = passedData.p1Kit.coverart;
            skinDesc.text = passedData.p1SkinDesc;
            kitDesc.text = passedData.p1KitDesc;
        }
        else if (player == "p2")
        {
            skinInv.skinSwitch(playerAssets, passedData.p2Skin);
            nameInv.text = passedData.p2Name;
            kitInv.sprite = passedData.p2Kit.coverart;
            skinDesc.text = passedData.p2SkinDesc;
            kitDesc.text = passedData.p2KitDesc;
        }
    }

    public void skinTile(string player, string skin)
    {
        if (player == "p1")
        {
            passedData.p1Skin = skin;
            passedData.p1SkinDesc = getSkinDesc(skin);

        }
        else if (player == "p2")
        {
            passedData.p2Skin = skin;
            passedData.p2SkinDesc = getSkinDesc(skin);
        }
        customUpdate(player);
    }

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

    public void musicTile(string player, string kit)
    {

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

    //------EVERYTHING ELSE-------
    void Login(Players player)
    {
        switch (player)
        {
            case Players.Player1:
                player1 = "batyuzo";
                break;
            case Players.Player2:
                player2 = "gizmo";
                break;

            //will not happen, can't guarrantee in c# that i've exhausted the cases
            default:
                break;
        }

        if (player1 != null && player2 != null)
        {
            btn_fight.interactable = true;
        }
    }


}
