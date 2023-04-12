using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public int maxJumps;
    public GameObject startingPoint;
    public List<GameObject> pistons;

    private static GameManager instance = null;
    public static GameManager Instance => instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController.maxJump = maxJumps;
        playerController.player.transform.position = startingPoint.transform.position;
        startingPoint.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
