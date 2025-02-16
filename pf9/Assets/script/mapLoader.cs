using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class mapLoader : MonoBehaviour
{
    [Header("misc")]
    public Light2D global;
    public Light2D emission;
    public string activeMap;
    public int current = 0;
    public int i = 0;
    public int divide;
    public GameObject a1, a2, b1, b2, c1, c2, c3;


    [Header("ham_factory refs")]
    [SerializeField] Sprite ham_A1;
    [SerializeField] Sprite ham_A2;
    [SerializeField] Sprite ham_B1;
    [SerializeField] Sprite ham_B2;
    [SerializeField] Sprite ham_C1;
    [SerializeField] Sprite ham_C2;
    [SerializeField] Sprite ham_C3;
    [SerializeField] Sprite ham_COLLISION;
    [SerializeField] Sprite ham_EMISSION;
    public Color ham_ECOLOR;

    [Header("medieval_japan_refs")]
    [SerializeField] Sprite jap_A1;
    [SerializeField] Sprite jap_A2;
    [SerializeField] Sprite jap_B1;
    [SerializeField] Sprite jap_B2;
    [SerializeField] Sprite jap_C1;
    [SerializeField] Sprite jap_C2;
    [SerializeField] Sprite jap_COLLISION;
    [SerializeField] Sprite jap_EMISSION;
    public Color jap_ECOLOR;

    [Header("practice_refs")]
    [SerializeField] Sprite prac_A1;
    [SerializeField] Sprite prac_A2;
    [SerializeField] Sprite prac_B1;
    [SerializeField] Sprite prac_B2;
    [SerializeField] Sprite prac_COLLISION;
    public Color prac_ECOLOR;
    public Sprite[] prac_C1;
    public Sprite[] prac_C2;
    public Sprite[] prac_EMISSION;


    public bool loadMap(string mapName)
    {
        i = 0;
        current = 0;
        if (mapName == "ham_factory")
        {
            //level layers
            layerUpdate("A1", a1, ham_A1);
            layerUpdate("A2", a2, ham_A2);
            layerUpdate("B1", b1, ham_B1);
            layerUpdate("B2", b2, ham_B2);
            layerUpdate("C1", c1, ham_C1);
            layerUpdate("C2", c2, ham_C2);
            layerUpdate("C3", c3, ham_C3);

            //lights
            emission.intensity = 1;
            emission.color = ham_ECOLOR;
            emission.lightCookieSprite = ham_EMISSION;

            //active map
            activeMap = "ham_factory";
            return true;
        }
        else if (mapName == "practice")
        {
            //level layers
            layerUpdate("A1", a1, prac_A1);
            layerUpdate("A2", a2, prac_A2);
            layerUpdate("B1", b1, prac_B1);
            layerUpdate("B2", b2, prac_B2);
            //c1,c2 are managed by a per-frame basis
            layerUpdate("C3", c3, null);

            //lights
            emission.intensity = .85f;
            emission.color = prac_ECOLOR;

            //active map
            activeMap = "practice";
            return true;
        }
        else if (mapName == "medieval_japan")
        {
            //level layers
            layerUpdate("A1", a1, jap_A1);
            layerUpdate("A2", a2, jap_A2);
            layerUpdate("B1", b1, jap_B1);
            layerUpdate("B2", b2, jap_B2);
            layerUpdate("C1", c1, jap_C1);
            layerUpdate("C2", c2, jap_C2);
            layerUpdate("C3", c3, null);

            //lights
            emission.intensity = 1;
            emission.color = jap_ECOLOR;
            emission.lightCookieSprite = jap_EMISSION;

            //active map
            activeMap = "medieval_japan";
            return true;
        }
        else
        {
            return false;
        }
    }

    private void layerUpdate(string name, GameObject obj, Sprite toLoad)
    {
            obj.GetComponent<SpriteRenderer>().sprite = toLoad;
    }

    private void layerAnim(int frame)
    {
        if (frame % 4 == 0 && current < 5)
        {
            current++;
        }
        else if (frame % 4 == 0)
        {
            current = 0;
        }

        if (activeMap == "practice")
        {
            layerUpdate("C1", c1, prac_C1[current]);
            layerUpdate("C2", c2, prac_C2[current]);
            emission.lightCookieSprite = prac_EMISSION[current];
        }
    }

    //for dynamic/animated stuff on any map
    private void FixedUpdate()
    {
        i++;
        layerAnim(i);
    }

}
