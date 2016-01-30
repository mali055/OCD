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
                if (!grid[(int)(pos.x + pos.y * GM.Get().framework.map.y)])
                {
                    grid[(int)(pos.x + pos.y * GM.Get().framework.map.y)] = true;
                }
            }
        }
        printGrid();

	}

    void printGrid()
    {
        int i = 0;
        string str = string.Empty;
        for (int j = 0; j < grid.Count; j++)
        {
            if (i < GM.Get().framework.map.x)
            {

                str += grid[j] + " ";
                i++;

            } else
            {
                str += grid[j] + "\n";
                i = 0;
            }


        }
        //Debug.Log(str);
    }

    public Vector2 pointToTile(Vector2 pos)
    {
        ////grid [ x + (y * col) ]
        return new Vector2(Mathf.Floor(pos.x / GM.Get().framework.tileSize), Mathf.Floor(pos.y / GM.Get().framework.tileSize));
    }
}
