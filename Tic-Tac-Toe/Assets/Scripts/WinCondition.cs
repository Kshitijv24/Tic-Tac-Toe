using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public static WinCondition Instance { get; private set; }

    public List<GridBlock> blockList;

    [SerializeField] GameObject xWonPopUp;
    [SerializeField] GameObject oWonPopUp;
    [SerializeField] AdvancedOpponentAI advancedOpponentAI;

    [HideInInspector] public bool gameWon = false;
    [HideInInspector] public bool gameLose = false;
    [HideInInspector] public GridBlock[,] board = new GridBlock[3, 3];

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

        CheckForWinOrLoseCondition(board);
    }

    public void CheckForWinOrLoseCondition(GridBlock[,] board)
    {
        // Check for horizontal win/lose conditions
        for (int row = 0; row < 3; row++)
        {
            if (CheckLine(board[row, 0], board[row, 1], board[row, 2]))
            {
                if (board[row, 0].currentBlockState == BlockState.X)
                {
                    gameWon = true;
                    ShowWinOrLosePopUp();
                }
                else if (board[row, 0].currentBlockState == BlockState.O)
                {
                    gameLose = true;
                    ShowWinOrLosePopUp();
                }
            }
        }

        // Check for vertical win/lose conditions

        for (int col = 0; col < 3; col++)
        {
            if (CheckLine(board[0, col], board[1, col], board[2, col]))
            {
                if (board[0, col].currentBlockState == BlockState.X)
                {
                    gameWon = true;
                    ShowWinOrLosePopUp();
                }
                else if (board[0, col].currentBlockState == BlockState.O)
                {
                    gameLose = true;
                    ShowWinOrLosePopUp();
                }
            }
        }

        // Check for diagonal win/lose conditions
        if (CheckLine(board[0, 0], board[1, 1], board[2, 2]))
        {
            if (board[0, 0].currentBlockState == BlockState.X)
            {
                gameWon = true;
                ShowWinOrLosePopUp();
            }
            else if (board[0, 0].currentBlockState == BlockState.O)
            {
                gameLose = true;
                ShowWinOrLosePopUp();
            }
        }

        if (CheckLine(board[0, 2], board[1, 1], board[2, 0]))
        {
            if (board[0, 2].currentBlockState == BlockState.X)
            {
                gameWon = true;
                ShowWinOrLosePopUp();
            }
            else if (board[0, 2].currentBlockState == BlockState.O)
            {
                gameLose = true;
                ShowWinOrLosePopUp();
            }
        }
    }

    private bool CheckLine(GridBlock a, GridBlock b, GridBlock c)
    {
        return a.currentBlockState != BlockState.Empty && a.currentBlockState == b.currentBlockState && b.currentBlockState == c.currentBlockState;
    }

    private void ShowWinOrLosePopUp()
    {
        if (gameWon)
        {
            Debug.Log("Game Won");
            StopPlayerAndOpponentAIMovesAfterWinOrLose();

            AudioManager.Instance.PlayWinSound(0.4f);
            Instantiate(xWonPopUp, xWonPopUp.transform.position, Quaternion.identity);
        }
        else if (gameLose)
        {
            Debug.Log("Game Lose");
            StopPlayerAndOpponentAIMovesAfterWinOrLose();

            AudioManager.Instance.PlayLoseSound(0.4f);
            Instantiate(oWonPopUp, oWonPopUp.transform.position, Quaternion.identity);
        }
    }

    private void StopPlayerAndOpponentAIMovesAfterWinOrLose()
    {
        if (gameWon || gameLose)
        {
            foreach (GridBlock gridBlock in blockList)
                gridBlock.gameObject.SetActive(false);

            advancedOpponentAI.gameObject.SetActive(false);
        }
    }
}
