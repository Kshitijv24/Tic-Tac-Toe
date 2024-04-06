using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public static WinCondition Instance { get; private set; }

    public List<GridBlock> blockList;

    [HideInInspector] public bool gameWon = false;
    [HideInInspector] public bool gameLose = false;

    [SerializeField] GameObject xWonPopUp;
    [SerializeField] GameObject oWonPopUp;
    [SerializeField] OpponentAI opponentAI;

    GridBlock[,] gridBlockArray = new GridBlock[3, 3];
    int index = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.Log("There are more than one " + this.GetType() + " Instances", this);
            return;
        }
    }

    private void Start()
    {
        // Assigning blockList Data to gridBlockArray

        for (int row = 0; row < gridBlockArray.GetLength(0); row++)
        {
            for (int col = 0; col < gridBlockArray.GetLength(1); col++)
            {
                gridBlockArray[row, col] = blockList[index];
                index++;
            }
        }
    }

    private void Update()
    {
        if (blockList == null) return;
        if (gameWon || gameLose) return;

        HandleWinAndLoseCondition();
    }

    private void HandleWinAndLoseCondition()
    {
        // Checking for the rows win or lose condition

        for (int row = 0; row < gridBlockArray.GetLength(0); row++)
        {
            if (gridBlockArray[row, 0].currentBlockState == gridBlockArray[row, 1].currentBlockState &&
                gridBlockArray[row, 1].currentBlockState == gridBlockArray[row, 2].currentBlockState)
            {
                if (gridBlockArray[row, 0].currentBlockState == BlockState.X)
                {
                    gameWon = true;
                    ShowWinPopUp();
                }
                else if (gridBlockArray[row, 0].currentBlockState == BlockState.O)
                {
                    gameLose = true;
                    ShowLosePopUp();
                }
            }
        }

        // Checking for the Columns win or lose condition

        for (int col = 0; col < gridBlockArray.GetLength(1); col++)
        {
            if (gridBlockArray[0, col].currentBlockState == gridBlockArray[1, col].currentBlockState &&
                gridBlockArray[1, col].currentBlockState == gridBlockArray[2, col].currentBlockState)
            {
                if (gridBlockArray[0, col].currentBlockState == BlockState.X)
                {
                    gameWon = true;
                    ShowWinPopUp();
                }
                else if (gridBlockArray[0, col].currentBlockState == BlockState.O)
                {
                    gameLose = true;
                    ShowLosePopUp();
                }
            }
        }

        // Checking for Diagonals win or lose condition

        if (gridBlockArray[0, 0].currentBlockState == gridBlockArray[1, 1].currentBlockState && 
            gridBlockArray[1, 1].currentBlockState == gridBlockArray[2, 2].currentBlockState)
        {
            if (gridBlockArray[0, 0].currentBlockState == BlockState.X)
            {
                gameWon = true;
                ShowWinPopUp();
            }
            else if (gridBlockArray[0, 0].currentBlockState == BlockState.O)
            {
                gameLose = true;
                ShowLosePopUp();
            }
        }

        // Checking for Diagonals win or lose condition

        if (gridBlockArray[0, 2].currentBlockState == gridBlockArray[1, 1].currentBlockState && 
            gridBlockArray[1, 1].currentBlockState == gridBlockArray[2, 0].currentBlockState)
        {
            if (gridBlockArray[0, 2].currentBlockState == BlockState.X)
            {
                gameWon = true;
                ShowWinPopUp();
            }
            else if (gridBlockArray[0, 2].currentBlockState == BlockState.O)
            {
                gameLose = true;
                ShowLosePopUp();
            }
        }
    }

    private void ShowWinPopUp()
    {
        if (gameWon)
        {
            StopPlayerMovesAfterWinOrLose();
            StopOpponentAIMovesAfterWinOrLose();

            AudioManager.Instance.PlayWinSound(0.4f);
            Instantiate(xWonPopUp, xWonPopUp.transform.position, Quaternion.identity);
        }
    }

    private void ShowLosePopUp()
    {
        if(gameLose)
        {
            StopPlayerMovesAfterWinOrLose();
            StopOpponentAIMovesAfterWinOrLose();

            AudioManager.Instance.PlayLoseSound(0.4f);
            Instantiate(oWonPopUp, oWonPopUp.transform.position, Quaternion.identity);
        }
    }

    private void StopPlayerMovesAfterWinOrLose()
    {
        if (gameWon || gameLose)
        {
            foreach (GridBlock gridBlock in blockList)
                gridBlock.gameObject.SetActive(false);
        }
    }

    private void StopOpponentAIMovesAfterWinOrLose()
    {
        if (gameWon || gameLose)
            opponentAI.gameObject.SetActive(false);
    }
}
