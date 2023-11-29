using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    public GameObject inventory;
    public GameObject EscMenu;
    public GameObject EndScreen;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !EscMenu.activeInHierarchy)
        {
            inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(!EscMenu.gameObject.activeInHierarchy);
        }
    }
    public void TogglePause(bool value)
    {
        inventory.gameObject.SetActive(false);
        EscMenu.gameObject.SetActive(value);
        // will need node code to pause all moving objects
    }
    public void ShowEndScreen()
    {
        inventory.SetActive(false);
        EscMenu.SetActive(false);
        EndScreen.SetActive(true);
        EndScreen.GetComponent<Animator>().Play("ShowUp");
        FindObjectOfType<PlayerData>().receiveInput = false;
    }
}
