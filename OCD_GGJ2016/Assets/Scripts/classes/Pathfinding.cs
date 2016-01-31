
using Meshadieme;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Meshadieme
{
    namespace Pathfinding
    {
        public enum pType
        {
            TileBased,
            NodeBased,
        }

        public class AStar {

            pType type;
            int tileSize;
            List<Node> scanned = new List<Node>();
            List<Node> processed = new List<Node>();
            List<Vector2> wayBack = new List<Vector2>();


            public AStar (pType ptype)
            {
                type = ptype;
            }

            void setTileSize(int tile)
            {
                tileSize = tile;
            }

            public void goToPos(Vector2 from, Vector2 pos)
            {
                Node current = new Node(0.0f, 0.0f, 0.0f, true, from, from);

                scanned.Clear();
                processed.Clear();
                wayBack.Clear();
                processed.Add(current);
                wayBack.Add(from);

                while (current.myGrid != pos)
                {
                    Debug.Log(from);
                    scanSurrounding(current, pos);
                    chooseNext();
                }
            }

            void chooseNext()
            {
                int index = 0;
                float cost = 9999;
                for (int i = 0; i < scanned.Count; i++)
                {
                    if (scanned[i].totalTravelCost < cost)
                    {
                        index = i;
                    }
                }
                Debug.Log(index);
                wayBack.Add(scanned[index].myGrid);
                processed.Add(scanned[index]);
            }

            void scanSurrounding(Node scan, Vector2 target)
            {
                Vector2 tempGrid = scan.myGrid;
                processed.Add(scan);
                tempGrid.x -= 1;
                tempGrid.y += 1;
                scanNode(tempGrid, target, scan.totalTravelCost, scan.myGrid);
                tempGrid.x++;
                scanNode(tempGrid, target, scan.totalTravelCost, scan.myGrid);
                tempGrid.x++;
                scanNode(tempGrid, target, scan.totalTravelCost, scan.myGrid);
                tempGrid.y--;
                scanNode(tempGrid, target, scan.totalTravelCost, scan.myGrid);
                tempGrid.y--;
                scanNode(tempGrid, target, scan.totalTravelCost, scan.myGrid);
                tempGrid.x--;
                scanNode(tempGrid, target, scan.totalTravelCost, scan.myGrid);
                tempGrid.x--;
                scanNode(tempGrid, target, scan.totalTravelCost, scan.myGrid);
                tempGrid.y++;
                scanNode(tempGrid, target, scan.totalTravelCost, scan.myGrid);

            }

            void scanNode(Vector2 scan, Vector2 target, float travelCost, Vector2 par)
            {
                Node add = null;
                Debug.Log("Z = " + scan);
                if (scan.x >= 0 && scan.y >= 0 && scan.x < GM.Get().framework.map.x && scan.y < GM.Get().framework.map.y)
                {
                    Debug.Log("A = " + GM.Get().scene.miscRefs[0].GetComponent<gridParent>().isOccupied(scan));
                    if (!GM.Get().scene.miscRefs[0].GetComponent<gridParent>().isOccupied(scan))
                    {
                        Debug.Log("B");
                        if (!wayBack.Contains(scan))
                        {
                            Debug.Log("C");
                            if (type == pType.TileBased)
                            {
                                add = new Node(getHueristic(scan, target), travelCost + 1.0f, 1.0f, false, scan, par);
                            }
                            else
                            {
                                //Node add = new Node(getHueristic(scan, target), );
                            }

                            scanned.Add(add);
                        }
                    }
                }
            }

            float getHueristic(Vector2 scan, Vector2 target)
            {
                float result = 0.0f;
                if (type == pType.TileBased)
                {
                    result = Mathf.Abs(scan.y - target.y) + Mathf.Abs(scan.x - target.x);
                } else {

                } //Draw a line and get magnitude for node based later
                return (result);
            }
        }


        class Node
        {
            public float hueristic;
            public Vector2 parentGrid;
            public float totalTravelCost;
            public float myTravelCost;
            public Vector2 myGrid;
            public bool occupied;

            public Node (float h, float total, float cost, bool occ, Vector2 grid, Vector2 parent )
            {
                hueristic = h;
                totalTravelCost = total;
                myTravelCost = cost;
                occupied = occ;
                myGrid = grid;
                parentGrid = parent;
            }

        }

    }

}
