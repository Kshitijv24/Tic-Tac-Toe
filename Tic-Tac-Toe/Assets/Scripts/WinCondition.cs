using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("There are more than one " + this.GetType() + " Instances", this);
            return;
        }
    }

    private void Update()
    {
        if (blockList == null) return;

        if (gameWon) return;
        if (gameLose) return;

        HandleWinCondition();
        HandleLoseCondition();
    }

    private void HandleWinCondition()
    {
        if (blockList[0].currentBlockState == BlockState.X &&
            blockList[1].currentBlockState == BlockState.X &&
            blockList[2].currentBlockState == BlockState.X)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[0].currentBlockState == BlockState.X &&
                 blockList[3].currentBlockState == BlockState.X &&
                 blockList[6].currentBlockState == BlockState.X)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[0].currentBlockState == BlockState.X &&
                 blockList[4].currentBlockState == BlockState.X &&
                 blockList[8].currentBlockState == BlockState.X)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[1].currentBlockState == BlockState.X &&
                 blockList[4].currentBlockState == BlockState.X &&
                 blockList[7].currentBlockState == BlockState.X)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[2].currentBlockState == BlockState.X &&
                 blockList[5].currentBlockState == BlockState.X &&
                 blockList[8].currentBlockState == BlockState.X)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[3].currentBlockState == BlockState.X &&
                 blockList[4].currentBlockState == BlockState.X &&
                 blockList[5].currentBlockState == BlockState.X)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[6].currentBlockState == BlockState.X &&
                 blockList[7].currentBlockState == BlockState.X &&
                 blockList[8].currentBlockState == BlockState.X)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[2].currentBlockState == BlockState.X &&
                 blockList[4].currentBlockState == BlockState.X &&
                 blockList[6].currentBlockState == BlockState.X)
        {
            gameWon = true;
            ShowWinPopUp();
        }
    }

    private void HandleLoseCondition()
    {
        if (blockList[0].currentBlockState == BlockState.O &&
            blockList[1].currentBlockState == BlockState.O &&
            blockList[2].currentBlockState == BlockState.O)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[0].currentBlockState == BlockState.O &&
                 blockList[3].currentBlockState == BlockState.O &&
                 blockList[6].currentBlockState == BlockState.O)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[0].currentBlockState == BlockState.O &&
                 blockList[4].currentBlockState == BlockState.O &&
                 blockList[8].currentBlockState == BlockState.O)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[1].currentBlockState == BlockState.O &&
                 blockList[4].currentBlockState == BlockState.O &&
                 blockList[7].currentBlockState == BlockState.O)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[2].currentBlockState == BlockState.O &&
                 blockList[5].currentBlockState == BlockState.O &&
                 blockList[8].currentBlockState == BlockState.O)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[3].currentBlockState == BlockState.O &&
                 blockList[4].currentBlockState == BlockState.O &&
                 blockList[5].currentBlockState == BlockState.O)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[6].currentBlockState == BlockState.O &&
                 blockList[7].currentBlockState == BlockState.O &&
                 blockList[8].currentBlockState == BlockState.O)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[2].currentBlockState == BlockState.O &&
                 blockList[4].currentBlockState == BlockState.O &&
                 blockList[6].currentBlockState == BlockState.O)
        {
            gameLose = true;
            ShowLosePopUp();
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
            {
                gridBlock.gameObject.SetActive(false);
            }
        }
    }

    private void StopOpponentAIMovesAfterWinOrLose()
    {
        if (gameWon || gameLose)
        {
            opponentAI.gameObject.SetActive(false);
        }
    }
}
