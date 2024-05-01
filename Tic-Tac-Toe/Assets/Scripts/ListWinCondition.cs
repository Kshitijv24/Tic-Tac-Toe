using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListWinCondition : MonoBehaviour
{
    public static ListWinCondition Instance { get; private set; }

    public List<GridBlock> gridBlockList;

    [HideInInspector] public bool gameWon = false;
    [HideInInspector] public bool gameLose = false;

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

    private void Update()
    {
        if (gridBlockList == null) return;
        if (gameWon || gameLose) return;

        //HandleWinAndLoseCondition();
    }

    public int HandleWinAndLoseCondition(List<GridBlock> gridBlockList)
    {
        if (gridBlockList[0].currentBlockState == BlockState.X && gridBlockList[1].currentBlockState == BlockState.X &&
            gridBlockList[2].currentBlockState == BlockState.X ||

            gridBlockList[0].currentBlockState == BlockState.X && gridBlockList[3].currentBlockState == BlockState.X && 
            gridBlockList[6].currentBlockState == BlockState.X ||

            gridBlockList[0].currentBlockState == BlockState.X && gridBlockList[4].currentBlockState == BlockState.X &&
            gridBlockList[8].currentBlockState == BlockState.X ||

            gridBlockList[1].currentBlockState == BlockState.X && gridBlockList[4].currentBlockState == BlockState.X &&
            gridBlockList[7].currentBlockState == BlockState.X ||

            gridBlockList[2].currentBlockState == BlockState.X && gridBlockList[5].currentBlockState == BlockState.X &&
            gridBlockList[8].currentBlockState == BlockState.X ||

            gridBlockList[3].currentBlockState == BlockState.X && gridBlockList[4].currentBlockState == BlockState.X &&
            gridBlockList[5].currentBlockState == BlockState.X ||

            gridBlockList[6].currentBlockState == BlockState.X && gridBlockList[7].currentBlockState == BlockState.X &&
            gridBlockList[8].currentBlockState == BlockState.X ||

            gridBlockList[2].currentBlockState == BlockState.X && gridBlockList[4].currentBlockState == BlockState.X &&
            gridBlockList[6].currentBlockState == BlockState.X)
        {
            return 10;
        }
        else if (gridBlockList[0].currentBlockState == BlockState.O && gridBlockList[1].currentBlockState == BlockState.O &&
                 gridBlockList[2].currentBlockState == BlockState.O ||

                 gridBlockList[0].currentBlockState == BlockState.O && gridBlockList[3].currentBlockState == BlockState.O &&
                 gridBlockList[6].currentBlockState == BlockState.O ||

                 gridBlockList[0].currentBlockState == BlockState.O && gridBlockList[4].currentBlockState == BlockState.O &&
                 gridBlockList[8].currentBlockState == BlockState.O ||

                 gridBlockList[1].currentBlockState == BlockState.O && gridBlockList[4].currentBlockState == BlockState.O &&
                 gridBlockList[7].currentBlockState == BlockState.O ||

                 gridBlockList[2].currentBlockState == BlockState.O && gridBlockList[5].currentBlockState == BlockState.O &&
                 gridBlockList[8].currentBlockState == BlockState.O ||

                 gridBlockList[3].currentBlockState == BlockState.O && gridBlockList[4].currentBlockState == BlockState.O &&
                 gridBlockList[5].currentBlockState == BlockState.O ||
                 
                 gridBlockList[6].currentBlockState == BlockState.O && gridBlockList[7].currentBlockState == BlockState.O &&
                 gridBlockList[8].currentBlockState == BlockState.O ||

                 gridBlockList[2].currentBlockState == BlockState.O && gridBlockList[4].currentBlockState == BlockState.O &&
                 gridBlockList[6].currentBlockState == BlockState.O)
        {
            return -10;
        }
        return 0;
    }

    private bool CheckLine(GridBlock a, GridBlock b, GridBlock c)
    {
        return a.currentBlockState != BlockState.Empty && a.currentBlockState == b.currentBlockState && b.currentBlockState == c.currentBlockState;
    }
}
