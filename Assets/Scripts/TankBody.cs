using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBody : MonoBehaviour
{
    public event TankDeathHandler TankHasDied;
    GameManager gameManager;
    MessageText messageText;
    bool hasDied = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        messageText = FindObjectOfType<MessageText>();
    }

    public void Death()
    {
        if(TankHasDied != null)
        {
            messageText.DisplayMessage("You have died. Press Space to replay.");
            hasDied = true;
            TankHasDied();
        }
    }

    private void Update()
    {
        if(hasDied)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gameManager.ReloadCurrentScene();
            }
        }
    }
}
