using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicAssets : MonoBehaviour
{
    [Header("crt_head1")]
    public musicKit crt1Kit;

    [Header("crt_head2")]//default
    public musicKit crt2Kit;

    [Header("hellstar.plus")]
    public musicKit hellstarKit;

    [Header("mute city")]
    public musicKit muteKit;

    public musicKit GetAssetByName(string name)
    {
        if (name.Contains("crt1"))
        {
            return crt1Kit;
        }
        return null;
    }
}
