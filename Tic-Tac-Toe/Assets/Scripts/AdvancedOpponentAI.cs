using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

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
                board[row, col] = WinCondition.Instance.blockList[index];
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

        if (GridArea.Instance.allGridBlock.Count <= 0)
        {
            isCoroutineRunning = false;
            yield break;
        }

        yield return new WaitForSeconds(nextMoveWaitTime);

        AudioManager.Instance.PlayClickSound(1f);
        BestMove();

        isCoroutineRunning = false;
    }

    private void BestMove()
    {
        float bestScore = Mathf.NegativeInfinity;
        GridBlock move = null;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j].currentBlockState == BlockState.Empty)
                {
                    board[i, j].currentBlockState = BlockState.O;
                    float score = CodingTrainMiniMax(board, 0, false);
                    board[i, j].currentBlockState = BlockState.Empty;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = board[i, j];
                    }
                }
            }
        }

        Debug.Log(bestScore);

        Instantiate(
            opponentAIIcon,
            move.transform.position,
            Quaternion.identity, opponentAIGameObjectHolder.transform);

        move.currentBlockState = BlockState.O;
        GridArea.Instance.allGridBlock.Remove(move);
        TurnManager.Instance.ChangeTurn();
    }

    private float CodingTrainMiniMax(GridBlock[,] board, int depth, bool isMaximizing)
    {
        //float score = WinCondition.Instance.HandleWinAndLoseCondition();
        float score;
        //if (score == 10)
        //    return score;

        //if (score == -10)
        //    return score;

        //if (!isMovesLeft(board))
        //    return 0;

        if (isMaximizing)
        {
            float bestScore = Mathf.NegativeInfinity;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i,j].currentBlockState == BlockState.Empty)
                    {
                        board[i,j].currentBlockState = BlockState.O;
                        score = CodingTrainMiniMax(board, depth + 1, false);
                        board[i, j].currentBlockState = BlockState.Empty;
                        bestScore = MathF.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            float bestScore = Mathf.Infinity;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j].currentBlockState == BlockState.Empty)
                    {
                        board[i, j].currentBlockState = BlockState.X;
                        score = CodingTrainMiniMax(board, depth + 1, true);
                        board[i, j].currentBlockState = BlockState.Empty;
                        bestScore = MathF.Min(score, bestScore);
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

    //private float MiniMax(GridBlock[,] board, int depth, bool isMax)
    //{
    //    Debug.Log("MinMax Run");
    //    float score = WinCondition.Instance.HandleWinAndLoseCondition();

    //    if (score == 10)
    //        return score;

    //    if (score == -10)
    //        return score;

    //    if (!isMovesLeft(board))
    //        return 0;

    //    if (isMax)
    //    {
    //        float bestScore = Mathf.NegativeInfinity;

    //        for (int i = 0; i < 3; i++)
    //        {
    //            for (int j = 0; j < 3; j++)
    //            {
    //                if (board[i, j].currentBlockState == BlockState.Empty)
    //                {
    //                    board[i, j].currentBlockState = BlockState.X;

    //                    score = MiniMax(board, depth + 1, isMax);

    //                    board[i, j].currentBlockState = BlockState.Empty;

    //                    bestScore = Mathf.Max(score, bestScore);
    //                }
    //            }
    //        }
    //        return bestScore;
    //    }
    //    else
    //    {
    //        float bestScore = Mathf.Infinity;

    //        for (int i = 0; i < 3; i++)
    //        {
    //            for (int j = 0; j < 3; j++)
    //            {
    //                if (board[i, j].currentBlockState == BlockState.Empty)
    //                {
    //                    board[i, j].currentBlockState = BlockState.O;

    //                    score = MiniMax(board, depth + 1, !isMax);

    //                    board[i, j].currentBlockState = BlockState.Empty;

    //                    bestScore = Mathf.Min(score, bestScore);
    //                }
    //            }
    //        }
    //        return bestScore;
    //    }
    //}

    //private GridBlock FindBestMove(GridBlock[,] board)
    //{
    //    float bestVal = Mathf.NegativeInfinity;
    //    GridBlock bestMove = null;
    //    bestMove.currentBlockState = BlockState.O;

    //    for (int i = 0; i < 3; i++)
    //    {
    //        for (int j = 0; j < 3; j++)
    //        {
    //            if (board[i,j].currentBlockState == BlockState.Empty)
    //            {
    //                board[i, j].currentBlockState = BlockState.X;

    //                float moveVal = MiniMax(board, 0, false);

    //                board[i, j].currentBlockState = BlockState.Empty;

    //                if(moveVal > bestVal)
    //                {
    //                    bestMove = board[i, j];
    //                    bestVal = moveVal;
    //                }
    //            }
    //        }
    //    }

    //    return bestMove;
    //}
}
