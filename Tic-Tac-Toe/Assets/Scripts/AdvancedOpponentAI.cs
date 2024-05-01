using System;
using System.Collections;
using UnityEngine;

public class AdvancedOpponentAI : MonoBehaviour
{
    [SerializeField] GameObject opponentAIIcon;
    [SerializeField] GameObject opponentAIGameObjectHolder;
    [SerializeField] float nextMoveWaitTime = 0.5f;
    
    GridBlock[,] board = new GridBlock[3, 3];
    bool isCoroutineRunning = false;
    int index = 0;

    private void Start()
    {
        // Assigning blockList Data to gridBlockArray

        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                board[row, col] = GridArea.Instance.gridBlockList[index];
                index++;
            }
        }
    }

    private void Update()
    {
        if (GridArea.Instance.gridBlockList.Count <= 0) return;

        if (!isCoroutineRunning && !TurnManager.Instance.isPlayerTurn)
            StartCoroutine(OpponentAIMove());

        //if (!TurnManager.Instance.isPlayerTurn)
        //    FindBestMove(board);
    }

    IEnumerator OpponentAIMove()
    {
        isCoroutineRunning = true;

        if (GridArea.Instance.gridBlockList.Count <= 0)
        {
            isCoroutineRunning = false;
            yield break;
        }

        yield return new WaitForSeconds(nextMoveWaitTime);

        FindBestMove(board);
        AudioManager.Instance.PlayClickSound(1f);

        isCoroutineRunning = false;
    }

    private void FindBestMove(GridBlock[,] board)
    {
        int bestScore = int.MinValue;
        int bestMoveRow = -1;
        int bestMoveCol = -1;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j].currentBlockState == BlockState.Empty)
                {
                    board[i, j].currentBlockState = BlockState.X;
                    int score = MiniMax(board, 0, false);
                    board[i, j].currentBlockState = BlockState.Empty;
                    //Debug.Log("Checking move at [" + i + ", " + j + "] with score: " + score);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMoveRow = i;
                        bestMoveCol = j;
                    }
                }
            }
        }

        if (bestMoveRow != -1 && bestMoveCol != -1)
        {
            GridBlock move = board[bestMoveRow, bestMoveCol];
            Debug.Log("Best Move Found: [" + bestMoveRow + ", " + bestMoveCol + "] with score: " + bestScore);

            Instantiate(
                opponentAIIcon,
                move.transform.position,
                Quaternion.identity, opponentAIGameObjectHolder.transform);

            move.currentBlockState = BlockState.O;
            GridArea.Instance.gridBlockList.Remove(move);
            TurnManager.Instance.ChangeTurn();
        }
        else
        {
            Debug.LogError("No valid move found!");
        }
    }


    private int MiniMax(GridBlock[,] board, int depth, bool isMaximizing)
    {
        int score = Evaluation(board);

        if (score == 10)
            return score;

        if (score == -10)
            return score;

        if (!IsMovesLeft(board))
            return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i,j].currentBlockState == BlockState.Empty)
                    {
                        board[i,j].currentBlockState = BlockState.X;
                        score = MiniMax(board, depth + 1, !isMaximizing);
                        board[i, j].currentBlockState = BlockState.Empty;
                        bestScore = Math.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j].currentBlockState == BlockState.Empty)
                    {
                        board[i, j].currentBlockState = BlockState.O;
                        score = MiniMax(board, depth + 1, isMaximizing);
                        board[i, j].currentBlockState = BlockState.Empty;
                        bestScore = Math.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }

    private int Evaluation(GridBlock[,] board)
    {
        // Check for horizontal win/lose conditions
        for (int row = 0; row < 3; row++)
        {
            if (CheckLine(board[row, 0], board[row, 1], board[row, 2]))
            {
                if (board[row, 0].currentBlockState == BlockState.X)
                    return 10;
                else if (board[row, 0].currentBlockState == BlockState.O)
                    return -10;
            }
        }

        // Check for vertical win/lose conditions

        for (int col = 0; col < 3; col++)
        {
            if (CheckLine(board[0, col], board[1, col], board[2, col]))
            {
                if (board[0, col].currentBlockState == BlockState.X)
                    return 10;
                else if (board[0, col].currentBlockState == BlockState.O)
                    return -10;
            }
        }

        // Check for diagonal win/lose conditions
        if (CheckLine(board[0, 0], board[1, 1], board[2, 2]))
        {
            if (board[0, 0].currentBlockState == BlockState.X)
                return 10;
            else if (board[0, 0].currentBlockState == BlockState.O)
                return -10;
        }

        if (CheckLine(board[0, 2], board[1, 1], board[2, 0]))
        {
            if (board[0, 2].currentBlockState == BlockState.X)
                return 10;
            else if (board[0, 2].currentBlockState == BlockState.O)
                return -10;
        }
        return 0;
    }

    private bool CheckLine(GridBlock a, GridBlock b, GridBlock c)
    {
        return a.currentBlockState != BlockState.Empty && a.currentBlockState == b.currentBlockState && b.currentBlockState == c.currentBlockState;
    }

    private Boolean IsMovesLeft(GridBlock[,] board)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j].currentBlockState == BlockState.Empty)
                    return true;
        return false;
    }
}
