using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] GameObject opponentAIIcon;
    [SerializeField] GameObject opponentAIGameObjectHolder;
    [SerializeField] float nextMoveWaitTime = 0.5f;

    private bool isCoroutineRunning = false;

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
        int randomIndex = Random.Range(0, GridArea.Instance.allGridBlock.Count);
        GridBlock randomBlock = GridArea.Instance.allGridBlock[randomIndex];

        Instantiate(
            opponentAIIcon,
            randomBlock.transform.position,
            Quaternion.identity, opponentAIGameObjectHolder.transform);

        randomBlock.currentBlockState = BlockState.O;
        GridArea.Instance.allGridBlock.Remove(randomBlock);
        TurnManager.Instance.ChangeTurn();

        isCoroutineRunning = false;
    }
}
