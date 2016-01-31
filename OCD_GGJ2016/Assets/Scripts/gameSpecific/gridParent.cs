using Meshadieme;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gridParent : MonoBehaviour {

    List<gridChild> children = new List<gridChild>();
    List<bool> grid = new List<bool>();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < GM.Get().framework.map.x * GM.Get().framework.map.y; i++)
        {
            grid.Add(false);
        }


	    foreach (Transform child in this.transform)
        {
            if (child.GetComponent<gridChild>())
            {
                //Debug.Log(child);
                children.Add(child.GetComponent<gridChild>());
                Vector2 pos = pointToTile(child.GetComponent<RectTransform>().anchoredPosition);
                if (pos.x < 0) pos.x = 0;
                if (pos.y < 0) pos.y = 0;
                //Debug.Log(pos + " = " + (int)(pos.x + (pos.y * GM.Get().framework.map.x)));
                if (!grid[(int)(pos.x + (pos.y * GM.Get().framework.map.x))])
                {
                    grid[(int)(pos.x + (pos.y * GM.Get().framework.map.x))] = true;
                }
                //Debug.Log(grid[(int)(pos.x + (pos.y * GM.Get().framework.map.x))]);
                if (children[children.Count - 1].gridHeight > 1)
                {
                    for (int i = 2; i < children[children.Count - 1].gridHeight; i++)
                    {
                        if (pos.y > GM.Get().framework.map.y) pos.y = GM.Get().framework.map.y;
                        if (!grid[(int)(pos.x + ((pos.y + i - 1) * GM.Get().framework.map.x))])
                        {
                            grid[(int)(pos.x + ((pos.y + i - 1) * GM.Get().framework.map.x))] = true;
                        }
                    }
                }
                if (children[children.Count - 1].gridWidth > 1)
                {
                    for (int i = 2; i < children[children.Count - 1].gridWidth; i++)
                    {
                        if (pos.x > GM.Get().framework.map.x) pos.x = GM.Get().framework.map.x;
                        if (!grid[(int)((pos.x + i - 1) + (pos.y * GM.Get().framework.map.x))])
                        {
                            grid[(int)((pos.x + i - 1) + (pos.y * GM.Get().framework.map.x))] = true;
                        }
                    }
                }
            }
        }
        printGrid();

	}

    void printGrid()
    {
        int i = 0;
        string str = string.Empty;
        string line = string.Empty;
        for (int j = 0; j < grid.Count; j++)
        {
            if (i < GM.Get().framework.map.x - 1)
            {
                line += (grid[j] ? "1" : "0") + " ";
                i++;
            } else
            {
                line += (grid[j] ? "1" : "0") + " ";
                str = line + "\n" + str;
                line = string.Empty;
                i = 0;
            }
        }
        Debug.Log(str);
    }

    public bool isOccupied(Vector2 pos)
    {
        Debug.Log("Occupied" + pos);
        return grid[(int)(pos.x + pos.y * GM.Get().framework.map.y)] = true;
    }

    public Vector2 pointToTile(Vector2 pos)
    {
        ////grid [ x + (y * col) ]
        return new Vector2(Mathf.Floor(pos.x / GM.Get().framework.tileSize), Mathf.Floor(pos.y / GM.Get().framework.tileSize));
    }
}
