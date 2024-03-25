using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public bool isPlayerTurn = true;

    [SerializeField] TextMeshProUGUI turnText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("There are more than one " + this.GetType() + " Instances", this);
            return;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        ChangeTurnUI();
    }

    public void ChangeTurnUI()
    {
        if (isPlayerTurn)
        {
            turnText.text = "Player Turn";
        }
        else
        {
            turnText.text = "Opponent AI Turn";
        }
    }
    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
    }
}
