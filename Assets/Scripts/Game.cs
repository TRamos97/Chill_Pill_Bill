using Cinemachine;
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
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !EscMenu.activeInHierarchy)
        {
            inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
            if (!inventory.gameObject.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
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
        Camera.main.GetComponentInChildren<CinemachineFreeLook>().enabled = !value;
        // will need node code to pause all moving objects
        if (value == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1; //Resume time
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; //Release cursor
            Time.timeScale = 0; //Stop time
        }
    }
    public void ShowEndScreen()
    {
        Time.timeScale = 1; //Resume time
        Cursor.lockState = CursorLockMode.None; //Release cursor
        inventory.SetActive(false);
        EscMenu.SetActive(false);
        EndScreen.SetActive(true);
        EndScreen.GetComponent<Animator>().Play("ShowUp");
        FindObjectOfType<PlayerData>().receiveInput = false;
        enabled = false; //Disables script and prevents other menus from being opened
    }
}
