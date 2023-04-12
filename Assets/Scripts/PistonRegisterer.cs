using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PistonRegisterer : MonoBehaviour
{
    GameObject piston;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.pistons.Add(piston);
    }
}
