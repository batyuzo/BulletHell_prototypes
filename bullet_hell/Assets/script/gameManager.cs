using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class gameManager : MonoBehaviour
{
    [Header("fight refs")]//fetched on initFight
    public healthbar healthbarP1;
    public healthbar healthbarP2;
    public GameObject player1;
    public GameObject player2;
    public fightUi fightUi;
    //+mapLoader dependency on init

    [Header("menu refs")]//fetched on menuInit
    public menu menuScript;

    [Header("common refs")]//fetched on both
    public musicPlayer musicPlayer;
    public passedData passedData;

    [Header("permanent refs")]//fetched on awake, or hardcoded from UI
    public mapLoader mapLoader;
    public weaponLoader weaponLoader;
    public spawnPositions spawnPositions;
    public playerAssets playerAssets;
    public musicAssets musicAssets;

    [Header("logs")]
    public bool firstLaunch;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "menu")
        {
            initMenu();
        }
        else if (SceneManager.GetActiveScene().name == "fight")
        {
            initFight();
        }
    }

    public void initMenu()
    {
        setRefs("menu");
        menuScript.init(passedData, musicPlayer, musicAssets, playerAssets);
        musicPlayer.init(passedData.p1Kit, passedData.p2Kit, 0.5f, "menu");
        Debug.Log(passedData.p1Kit.name);
    }

    public void initFight()
    {
        setRefs("fight");
        mapLoader.loadMap(passedData.map);
        weaponLoader.spawnPositions = spawnPositions;
        musicPlayer.init(passedData.p1Kit, passedData.p2Kit, 0.5f, "fight");
        fightUi.init(passedData);
        healthbarP1.init("p1", passedData.map);
        healthbarP2.init("p2", passedData.map);
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;

        if (passedData.map == "prac")
        {
            initPlayers(new string[] { passedData.p1Skin, passedData.p2Skin }, spawnPositions.prac_player, 200);//passeddata
        }
        else if (passedData.map == "ham")
        {
            initPlayers(new string[] { passedData.p1Skin, passedData.p2Skin }, spawnPositions.ham_player, 200);//passeddata
        }
        else if (passedData.map == "jap")
        {
            initPlayers(new string[] { passedData.p1Skin, passedData.p2Skin }, spawnPositions.jap_player, 200);//passeddata
        }
        weaponLoader.init(passedData.map);
    }

    private void initPlayers(string[] playerskins, Vector2 spawnAt, int health)
    {
        player1.GetComponent<playerController>().init(playerskins[0], spawnAt, health, playerAssets);
        player2.GetComponent<playerController>().init(playerskins[1], spawnAt * new Vector3(-1, 1), health, playerAssets);
    }

    private void setRefs(string scene)//find called once
    {
        unset();
        passedData = GameObject.FindGameObjectWithTag("passedData").GetComponent<passedData>();
        if (scene == "menu")//menu
        {
            menuRefs menuRefs = GameObject.FindGameObjectWithTag("refHandler").GetComponent<menuRefs>();
            //initMenu fetch
            menuScript = menuRefs.menuScript;
            musicPlayer = menuRefs.musicPlayer;

        }
        else if(scene=="fight")//fight
        {
            fightRefs fightRefs = GameObject.FindGameObjectWithTag("refHandler").GetComponent<fightRefs>();
            //initFight fetch
            healthbarP1 = fightRefs.healthbarP1;
            healthbarP2 = fightRefs.healthbarP2;
            player1 = fightRefs.player1;
            player2 = fightRefs.player2;
            fightUi = fightRefs.fightUi;
            musicPlayer = fightRefs.musicPlayer;

            mapLoader.init(fightRefs);//junking it for less lines
        }
    }

    private void unset()
    {
        //unset fight refs
        healthbarP1 = null;
        healthbarP2 = null;
        player1 = null;
        player2 = null;

        //unset menu refs
        menuScript = null;

        //musicplayer
        musicPlayer = null;

        //common refs
        //we don't unset common refs
    }
}
