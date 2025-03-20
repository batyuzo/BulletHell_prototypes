using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class weaponLoader : MonoBehaviour
{
    [Header("weapon refs")]
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject nailgun;
    [SerializeField] GameObject cleaver;
    [SerializeField] GameObject knife;

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
        }

        //spawn new weapons
        if (mapname == "ham")
        {
            //weapons get spawned
            //1 - unique
            //2 - rare
            //3 - common
            spawnWeapons(new List<GameObject>() { shotgun }, new List<GameObject>() { nailgun, pistol }, new List<GameObject>() { cleaver, knife }, spawnPositions.ham_unique, spawnPositions.ham_rare, spawnPositions.ham_common1, spawnPositions.ham_common2);
        }
        else if (mapname == "prac")
        {
            //rarities filled
            //weapons get spawned
            //1 - unique
            //2 - rare
            //3 - common
            spawnWeapons(new List<GameObject>() { shotgun, pistol }, new List<GameObject>() { shotgun }, new List<GameObject>() { pistol, nailgun }, spawnPositions.prac_unique, spawnPositions.prac_rare, spawnPositions.prac_common1, spawnPositions.prac_common2);
        }
        else if (mapname == "jap")
        {
            //weapons get spawned
            //1 - unique
            //2 - rare
            //3 - common
            spawnWeapons(new List<GameObject>() { shotgun, pistol }, new List<GameObject>() { shotgun }, new List<GameObject>() { pistol }, spawnPositions.jap_unique, spawnPositions.jap_rare, spawnPositions.jap_common1, spawnPositions.jap_common2);
        }
        Debug.Log(mapname + "loaded");
    }
    private void spawnWeapons(List<GameObject> unique, List<GameObject> rare, List<GameObject> common, Vector2 uniquePos, Vector2 rare1Pos, Vector2 common1Pos, Vector2 common2Pos)
    {
        //UNIQUE
        Instantiate(unique[Random.Range(0, unique.Count)], uniquePos, Quaternion.Euler(0, 0, 0)); weaponsSpawned++;
        //RARE
        mirror(rare[Random.Range(0, rare.Count)], rare1Pos);
        //COMMON
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
