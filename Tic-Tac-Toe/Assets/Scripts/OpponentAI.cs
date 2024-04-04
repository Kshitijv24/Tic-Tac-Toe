using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] GameObject opponentAIIcon;
    [SerializeField] GameObject opponentAIGameObjectHolder;

    private bool isCoroutineRunning = false;

    private void Update()
    {
        if (!isCoroutineRunning)
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

        if (!TurnManager.Instance.isPlayerTurn)
        {
            yield return new WaitForSeconds(1f);

            AudioManager.Instance.PlayClickSound(1f);
            int randomIndex = Random.Range(0, GridArea.Instance.allGridBlock.Count);
            GridBlock randomBlock = GridArea.Instance.allGridBlock[randomIndex];

            Instantiate(
                opponentAIIcon,
                GridArea.Instance.allGridBlock[randomIndex].transform.position,
                Quaternion.identity, opponentAIGameObjectHolder.transform);

            randomBlock.currentBlockState = BlockState.O;
            GridArea.Instance.allGridBlock.Remove(GridArea.Instance.allGridBlock[randomIndex]);
            TurnManager.Instance.ChangeTurn();
        }

        isCoroutineRunning = false;
    }
}
