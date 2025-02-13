using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLoader : MonoBehaviour
{

    [Header("ham_factory refs")]
    [SerializeField] Sprite ham_A1;
    [SerializeField] Sprite ham_A2;
    [SerializeField] Sprite ham_B1;
    [SerializeField] Sprite ham_B2;
    [SerializeField] Sprite ham_C1;
    [SerializeField] Sprite ham_C2;
    [SerializeField] Sprite ham_C3;
    [SerializeField] Sprite ham_COLLISION;

    [Header("medieval_japan_refs")]
    [SerializeField] Sprite jap_A1;
    [SerializeField] Sprite jap_A2;
    [SerializeField] Sprite jap_B1;
    [SerializeField] Sprite jap_B2;
    [SerializeField] Sprite jap_C1;
    [SerializeField] Sprite jap_C2;
    [SerializeField] Sprite jap_COLLISION;

    [Header("practice_refs")]
    [SerializeField] Sprite prac_A1;
    [SerializeField] Sprite prac_A2;
    [SerializeField] Sprite prac_B1;
    [SerializeField] Sprite prac_B2;
    [SerializeField] Sprite prac_C1;
    [SerializeField] Sprite prac_C2;
    [SerializeField] Sprite prac_COLLISION;


    //load maps to children, collision is still idkhow
    public bool loadMap(string mapName)
    {
        if (mapName == "ham_factory")
        {
            foreach (Transform child in transform)
            {
                layerUpdate("A1", child, ham_A1);
                layerUpdate("A2", child, ham_A2);
                layerUpdate("B1", child, ham_B1);
                layerUpdate("B2", child, ham_B2);
                layerUpdate("C1", child, ham_C1);
                layerUpdate("C2", child, ham_C2);
                layerUpdate("C3", child, ham_C3);
            }

            return true;
        }
        else if (mapName == "practice")
        {
            //load per layer
            foreach (Transform child in transform)
            {
                layerUpdate("A1", child, prac_A1);
                layerUpdate("A2", child, prac_A2);
                layerUpdate("B1", child, prac_B1);
                layerUpdate("B2", child, prac_B2);
                layerUpdate("C1", child, prac_C1);
                layerUpdate("C2", child, prac_C2);
                layerUpdate("C3", child, null);
            }
            return true;
        }
        else if (mapName == "medieval_japan")
        {

            foreach (Transform child in transform)
            {
                layerUpdate("A1", child, jap_A1);
                layerUpdate("A2", child, jap_A2);
                layerUpdate("B1", child, jap_B1);
                layerUpdate("B2", child, jap_B2);
                layerUpdate("C1", child, jap_C1);
                layerUpdate("C2", child, jap_C2);
                layerUpdate("C3", child, null);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void layerUpdate(string name, Transform child, Sprite toLoad)
    {
        if (child.name == name)
        {
            child.GetComponent<SpriteRenderer>().sprite = toLoad;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
