using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class weaponLoader : MonoBehaviour
{
    [Header("weapon refs")]
    public GameObject shotgun;
    public GameObject pistol;

    [Header("script refs")]
    public spawnPositions spawnPositions;

    [Header("logs")]
    [SerializeField] int weaponsSpawned;

    public void set(string mapname)//called by gameManager
    {
        weaponsSpawned = 0;

        //destroy past weapons
        foreach (GameObject weapon in GameObject.FindGameObjectsWithTag("weapon"))
        {
            Destroy(weapon);
            Debug.Log(weapon.name + " destroyed");
        }

        //spawn new weapons
        if (mapname == "ham")
        {
            //rarities filled
            List<GameObject> unique = new List<GameObject>() { shotgun, shotgun };
            List<GameObject> rare = new List<GameObject>() { shotgun, shotgun };
            List<GameObject> common = new List<GameObject>() { shotgun, shotgun };

            //weapons get spawned
            spawnWeapons(unique, rare, common, spawnPositions.ham_unique, spawnPositions.ham_rare, spawnPositions.ham_common1, spawnPositions.ham_common2);
        }
        else if (mapname == "prac")
        {
            //rarities filled
            List<GameObject> unique = new List<GameObject>() { shotgun, shotgun };
            List<GameObject> rare = new List<GameObject>() { shotgun, shotgun };
            List<GameObject> common = new List<GameObject>() { shotgun, shotgun };

            //weapons get spawned
            spawnWeapons(unique, rare, common, spawnPositions.prac_unique, spawnPositions.prac_rare, spawnPositions.prac_common1, spawnPositions.prac_common2);
        }
        else if (mapname == "jap")
        {
            //rarities filled
            List<GameObject> unique = new List<GameObject>() { shotgun, shotgun };
            List<GameObject> rare = new List<GameObject>() { shotgun, shotgun };
            List<GameObject> common = new List<GameObject>() { shotgun, shotgun };

            //weapons get spawned
            spawnWeapons(unique, rare, common, spawnPositions.jap_unique, spawnPositions.jap_rare, spawnPositions.jap_common1, spawnPositions.jap_common2);
        }
        Debug.Log(mapname + "loaded");
    }

    private void spawnWeapons(List<GameObject> unique, List<GameObject> rare, List<GameObject> common, Vector2 uniquePos, Vector2 rare1Pos, Vector2 common1Pos, Vector2 common2Pos)
    {
        Instantiate(unique[Random.Range(0, unique.Count)], uniquePos, Quaternion.Euler(0, 0, 0)); weaponsSpawned++;
        mirror(rare[Random.Range(0, rare.Count)], rare1Pos);
        mirror(common[Random.Range(0, common.Count)], common1Pos);
        mirror(common[Random.Range(0, common.Count)], common2Pos);
    }

    private void mirror(GameObject weapon, Vector2 spawnAt)//spawn per side
    {
        Instantiate(weapon, spawnAt, Quaternion.Euler(0, 0, 0)).name = weaponsSpawned + weapon.name;
        weaponsSpawned++;
        Instantiate(weapon, new Vector2(-spawnAt.x, spawnAt.y), Quaternion.Euler(0, 180, 0)).name = weaponsSpawned + weapon.name;
        weaponsSpawned++;
    }
}
