using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node
{
    public bool walkable;
    public int gridX;
    public int gridY;
    public int hCost;
    public int gCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
        set
        {
            value = fCost;
        }
    }
    public Node parent;

    public Node(bool walkable, int posX, int posY)
    {
        this.walkable = walkable;
        this.gridX = posX;
        this.gridY = posY;
    }
}
