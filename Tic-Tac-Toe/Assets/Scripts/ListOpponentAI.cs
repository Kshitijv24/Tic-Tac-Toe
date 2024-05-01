using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOpponentAI : MonoBehaviour
{
    [SerializeField] GameObject opponentAIIcon;
    [SerializeField] GameObject opponentAIGameObjectHolder;
    [SerializeField] float nextMoveWaitTime = 0.5f;

    bool isCoroutineRunning = false;
    int index = 0;

    private void Update()
    {
        //if (!isCoroutineRunning && !TurnManager.Instance.isPlayerTurn)
        //{
        //    StartCoroutine(OpponentAIMove());
        //}

        if (GridArea.Instance.gridBlockList.Count <= 0)
            return;

        if (!TurnManager.Instance.isPlayerTurn)
            FindBestMove();
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

        foreach(GridBlock gridBlock in GridArea.Instance.gridBlockList)
        {
            if(gridBlock.currentBlockState == BlockState.Empty)
            {
                gridBlock.currentBlockState = BlockState.X;
                int score = MiniMax(GridArea.Instance.gridBlockList, 0, false);
                gridBlock.currentBlockState = BlockState.Empty;

                if (score > bestScore)
                {
                    bestScore = score;
                    move = gridBlock;
                }
            }
        }

        if(move != null)
        {
            Instantiate(
            opponentAIIcon,
            move.transform.position,
            Quaternion.identity, opponentAIGameObjectHolder.transform);

            move.currentBlockState = BlockState.O;
            GridArea.Instance.gridBlockList.Remove(move);
            TurnManager.Instance.ChangeTurn();
        }
    }

    private int MiniMax(List<GridBlock> gridBlockList, int depth, bool isMaximizing)
    {
        int score = ListWinCondition.Instance.HandleWinAndLoseCondition(gridBlockList);

        if (score == 10)
            return score;

        if (score == -10)
            return score;

        if (!isMovesLeft(gridBlockList))
            return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            
            foreach (GridBlock gridBlock in GridArea.Instance.gridBlockList)
            {
                if (gridBlock.currentBlockState == BlockState.Empty)
                {
                    gridBlock.currentBlockState = BlockState.X;
                    score = MiniMax(GridArea.Instance.gridBlockList, depth + 1, !isMaximizing);
                    gridBlock.currentBlockState = BlockState.Empty;
                    bestScore = (int)MathF.Max(score, bestScore);
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;

            foreach (GridBlock gridBlock in GridArea.Instance.gridBlockList)
            {
                if (gridBlock.currentBlockState == BlockState.Empty)
                {
                    gridBlock.currentBlockState = BlockState.O;
                    score = MiniMax(GridArea.Instance.gridBlockList, depth + 1, isMaximizing);
                    gridBlock.currentBlockState = BlockState.Empty;
                    bestScore = (int)MathF.Max(score, bestScore);
                }
            }
            return bestScore;
        }
    }

    private Boolean isMovesLeft(List<GridBlock> gridBlockList)
    {
        if(gridBlockList.Count <= 0)
            return false;
        else
            return true;
    }
}
