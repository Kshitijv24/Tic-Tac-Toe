using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedOpponentAI : MonoBehaviour
{
    [SerializeField] GameObject opponentAIIcon;
    [SerializeField] GameObject opponentAIGameObjectHolder;
    [SerializeField] float nextMoveWaitTime = 0.5f;

    bool isCoroutineRunning = false;
    GridBlock[,] board = new GridBlock[3, 3];
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
        if (!isCoroutineRunning && !TurnManager.Instance.isPlayerTurn)
        {
            StartCoroutine(OpponentAIMove());
        }
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

        FindBestMove();
        AudioManager.Instance.PlayClickSound(1f);

        isCoroutineRunning = false;
    }

    private void FindBestMove()
    {
        int bestScore = int.MinValue;
        GridBlock move = null;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j].currentBlockState == BlockState.Empty)
                {
                    board[i, j].currentBlockState = BlockState.X;
                    int score = MiniMax(board, 0, false);
                    board[i, j].currentBlockState = BlockState.Empty;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = board[i, j];
                    }
                }
            }
        }

        Instantiate(
            opponentAIIcon,
            move.transform.position,
            Quaternion.identity, opponentAIGameObjectHolder.transform);

        move.currentBlockState = BlockState.O;
        GridArea.Instance.gridBlockList.Remove(move);
        TurnManager.Instance.ChangeTurn();
    }

    private int MiniMax(GridBlock[,] board, int depth, bool isMaximizing)
    {
        int score = WinCondition.Instance.HandleWinAndLoseCondition();

        if (score == 10)
            return score;

        if (score == -10)
            return score;

        if (!isMovesLeft(board))
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
                        bestScore = (int)MathF.Max(score, bestScore);
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
                        bestScore = (int)MathF.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }

    private Boolean isMovesLeft(GridBlock[,] board)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j].currentBlockState == BlockState.Empty)
                    return true;
        return false;
    }
}
