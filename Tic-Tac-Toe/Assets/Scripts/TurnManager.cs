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
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);

        ChangeTurnUI();
    }

    public void ChangeTurnUI()
    {
        if (WinCondition.Instance.gameWon || WinCondition.Instance.gameLose)
            turnText.gameObject.SetActive(false);

        turnText.text = isPlayerTurn ? "Player Turn" : "Opponent AI Turn";

        if (GridArea.Instance.allGridBlock.Count == 0)
            turnText.text = "It's a draw";
    }

    public void ChangeTurn() => isPlayerTurn = !isPlayerTurn;
}
