using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridArea : MonoBehaviour
{
    public static GridArea Instance { get; private set; }

    public List<GridBlock> gridBlockList;

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
}
