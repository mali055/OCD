
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
                Node current = new Node();
                current.myTravelCost = 0.0f;
                current.hueristic = 0.0f;
                current.totalTravelCost = 0.0f;
                current.myGrid = from;
                current.parentGrid = from;
                current.occupied = true;

                scanned.Clear();
                processed.Clear();

                while (current.myGrid != pos)
                {
                    scanSurrounding(current);

                }
            }

            void scanSurrounding(Node scan)
            {
                int j = 0;
                int i = 0;
                for (i = -1; i < 2; i = i + 2)
                {

                    //AddPossible(map, Addto, X, Y, TarX, TarY, i, j, Final, Walkable);
                }
                i = 0;
                for (j = -1; j < 2; j = j + 2)
                {
                    //AddPossible(map, Addto, X, Y, TarX, TarY, i, j, Final, Walkable);
                }
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

        }

    }

}
