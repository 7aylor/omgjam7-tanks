using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    MessageText messageText;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        messageText = FindObjectOfType<MessageText>();
        gameManager = FindObjectOfType<GameManager>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        messageText.DisplayMessage("A portal has appeared to the next level");
    }

    public void EnablePortal()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Load next level
            gameManager.LoadNextScene();
        }
    }
}
