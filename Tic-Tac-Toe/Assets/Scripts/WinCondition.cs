using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public static WinCondition Instance { get; private set; }

    public List<GridBlock> blockList;

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

        //HandleWinAndLoseCondition();
    }

    public int HandleWinAndLoseCondition(GridBlock[,] board)
    {
        int score = 0;

        // Check for horizontal and vertical win/lose conditions
        for (int i = 0; i < 3; i++)
        {
            if (CheckLine(board[i, 0], board[i, 1], board[i, 2]))
            {
                if (board[i, 0].currentBlockState == BlockState.X)
                {
                    //gameWon = true;
                    //Debug.Log("Player won");
                    score = 10;
                }
                else if (board[i, 0].currentBlockState == BlockState.O)
                {
                    //gameLose = true;
                    //Debug.Log("Opponent AI won");
                    score = -10;
                }
            }

            if (CheckLine(board[0, i], board[1, i], board[2, i]))
            {
                if (board[0, i].currentBlockState == BlockState.X)
                {
                    //gameWon = true;
                    //Debug.Log("Player won");
                    score = 10;
                }
                else if (board[0, i].currentBlockState == BlockState.O)
                {
                    //gameLose = true;
                    //Debug.Log("Opponent AI won");
                    score = -10;
                }
            }
        }

        // Check for diagonal win/lose conditions
        if (CheckLine(board[0, 0], board[1, 1], board[2, 2]))
        {
            if (board[0, 0].currentBlockState == BlockState.X)
            {
                //gameWon = true;
                //Debug.Log("Player won");
                score = 10;
            }
            else if (board[0, 0].currentBlockState == BlockState.O)
            {
                //gameLose = true;
                //Debug.Log("Opponent AI won");
                score = -10;
            }
        }

        if (CheckLine(board[0, 2], board[1, 1], board[2, 0]))
        {
            if (board[0, 2].currentBlockState == BlockState.X)
            {
                //gameWon = true;
                //Debug.Log("Player won");
                score = 10;
            }
            else if (board[0, 2].currentBlockState == BlockState.O)
            {
                //gameLose = true;
                //Debug.Log("Opponent AI won");
                score = -10;
            }
        }

        return score;
    }

    private bool CheckLine(GridBlock a, GridBlock b, GridBlock c)
    {
        return a.currentBlockState != BlockState.Empty && a.currentBlockState == b.currentBlockState && b.currentBlockState == c.currentBlockState;
    }
}
