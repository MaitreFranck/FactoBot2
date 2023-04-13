using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisRegisterer : MonoBehaviour
{
    public GameObject tapis;
    public bool left = true;
    void Start()
    {
        GameManager.Instance.pistons.Add(tapis);
    }

}
