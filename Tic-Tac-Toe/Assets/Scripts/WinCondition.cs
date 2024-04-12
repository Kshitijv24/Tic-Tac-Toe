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
    [HideInInspector] public GridBlock[,] board = new GridBlock[3, 3];

    [SerializeField] GameObject xWonPopUp;
    [SerializeField] GameObject oWonPopUp;
    [SerializeField] OpponentAI opponentAI;

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

        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                board[row, col] = blockList[index];
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

    public void HandleWinAndLoseCondition()
    {
        // Checking for the rows win or lose condition

        for (int row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0].currentBlockState == board[row, 1].currentBlockState &&
                board[row, 1].currentBlockState == board[row, 2].currentBlockState)
            {
                if (board[row, 0].currentBlockState == BlockState.X)
                {
                    gameWon = true;
                    ShowWinPopUp();
                    //return +10;
                }
                else if (board[row, 0].currentBlockState == BlockState.O)
                {
                    gameLose = true;
                    ShowLosePopUp();
                    //return -10;
                }
            }
        }

        // Checking for the Columns win or lose condition

        for (int col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col].currentBlockState == board[1, col].currentBlockState &&
                board[1, col].currentBlockState == board[2, col].currentBlockState)
            {
                if (board[0, col].currentBlockState == BlockState.X)
                {
                    gameWon = true;
                    ShowWinPopUp();
                    //return +10;
                }
                else if (board[0, col].currentBlockState == BlockState.O)
                {
                    gameLose = true;
                    ShowLosePopUp();
                    //return -10;
                }
            }
        }

        // Checking for Diagonals win or lose condition

        if (board[0, 0].currentBlockState == board[1, 1].currentBlockState && 
            board[1, 1].currentBlockState == board[2, 2].currentBlockState)
        {
            if (board[0, 0].currentBlockState == BlockState.X)
            {
                gameWon = true;
                ShowWinPopUp();
                //return +10;
            }
            else if (board[0, 0].currentBlockState == BlockState.O)
            {
                gameLose = true;
                ShowLosePopUp();
                //return -10;
            }
        }

        // Checking for Diagonals win or lose condition

        if (board[0, 2].currentBlockState == board[1, 1].currentBlockState && 
            board[1, 1].currentBlockState == board[2, 0].currentBlockState)
        {
            if (board[0, 2].currentBlockState == BlockState.X)
            {
                gameWon = true;
                ShowWinPopUp();
                //return +10;
            }
            else if (board[0, 2].currentBlockState == BlockState.O)
            {
                gameLose = true;
                ShowLosePopUp();
                //return -10;
            }
        }
        //return 0;
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
