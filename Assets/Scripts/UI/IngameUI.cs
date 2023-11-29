using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public static IngameUI instance;
    public ProgressBar healthBar;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI InteractMessageText;
    private PlayerData player;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = FindObjectOfType<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetProgressValue(player.GetHeathStatus());
        pointsText.text = $"{player.seedPoints}";
    }
    public void DisplayInteractionMessage(string msg)
    {
        InteractMessageText.text = msg;
    }
}
