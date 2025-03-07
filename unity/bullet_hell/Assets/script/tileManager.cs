using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tileManager : MonoBehaviour
{
    [Header("sprite refs")]
    public Sprite crt1Base;
    public Sprite crt1Selected;
    public Sprite crt2Base;
    public Sprite crt2Selected;
    public Sprite hellstarBase;
    public Sprite hellstarSelected;
    public Sprite muteBase;
    public Sprite muteSelected;

    [Header("asset refs")]
    musicAssets musicAssets;
    passedData passedData;

    [Header("button refs")]
    public Button btn_crt1;
    public Button btn_crt2;
    public Button btn_hellstar;
    public Button btn_mute;

    public void init(musicAssets musicAssetsRef, passedData passedDataRef, List<musicKit> kits, List<string> skins, string player)//called by menu
    {
        Debug.Log("i reach this," + player);
        musicAssets = musicAssetsRef;
        passedData = passedDataRef;
        //disable everything first
        btn_crt1.interactable = false;
        btn_crt2.interactable = false;
        btn_hellstar.interactable = false;
        btn_mute.interactable = false;
        //deselect tiles
        setTiles();
        //enable owned
        enableTiles(kits);
        //set active

        if (player == "p1")
        {
            selectKit("p1", passedData.p1Kit);
        }
        else
        {
            selectKit("p2", passedData.p2Kit);
        }
    }

    public void enableTiles(List<musicKit> kits)//called by menu
    {
        if (kits.Contains(musicAssets.crt1Kit))
        {
            btn_crt1.interactable = true;
        }
        if (kits.Contains(musicAssets.crt2Kit))
        {
            btn_crt2.interactable = true;
        }
        if (kits.Contains(musicAssets.hellstarKit))
        {
            btn_hellstar.interactable = true;
        }
        if (kits.Contains(musicAssets.muteKit))
        {
            btn_mute.interactable = true;
        }
    }

    public void setTiles()
    {
        btn_crt1.image.sprite = crt1Base;
        btn_crt2.image.sprite = crt2Base;
        btn_hellstar.image.sprite = hellstarBase;
        btn_mute.image.sprite = muteBase;
    }

    public void selectKit(string player, musicKit kit)//called by menu
    {
        setTiles();
        if (player == "p1")
        {
            passedData.p1Kit = kit;//passedData update
            if (kit == musicAssets.crt1Kit)
            {
                btn_crt1.image.sprite = crt1Selected;
            }
            else if (kit == musicAssets.crt2Kit)
            {
                btn_crt2.image.sprite = crt2Selected;
            }
            else if (kit == musicAssets.hellstarKit)
            {
                btn_hellstar.image.sprite = hellstarSelected;
            }
            else if (kit == musicAssets.muteKit)
            {
                btn_mute.image.sprite = muteSelected;
            }
        }
        else
        {
            passedData.p2Kit = kit;//passedData update
            if (kit == musicAssets.crt1Kit)
            {
                btn_crt1.image.sprite = crt1Selected;
            }
            else if (kit == musicAssets.crt2Kit)
            {
                btn_crt2.image.sprite = crt2Selected;
            }
            else if (kit == musicAssets.hellstarKit)
            {
                btn_hellstar.image.sprite = hellstarSelected;
            }
            else if (kit == musicAssets.muteKit)
            {
                btn_mute.image.sprite = muteSelected;
            }
        }

    }
}
