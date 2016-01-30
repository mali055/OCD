//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.1.0
/// </version>
/// <summary>
/// A Parent Class for most of our scripts,
/// To make it easier to manage debugging code and modify debug output as needed.
/// Aswell as avoiding debug messages is a non-development build.
/// </summary>
/// CHANGELOG: 
///	*	0.1.0: C# Game Template Base
/// TODO: Consolidate Debug Toggles in Editor for easiers access (C#)
/// 

using Meshadieme;
using Meshadieme.Math;
using Meshadieme.Pathfinding;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Meshadieme
{
    /*
    Sequence of events - todo list
    */

    public enum GameMode
    {
        Menu = 0,
        Home = 1,
        MiniGameA = 2,
        MiniGameB = 3,
        MiniGameC = 4,
        MiniGameD = 5,
        MiniGameE = 6,
    }

    public class ocdGF : gameFramework
    {

        public GameMode gMode;
        public GameMode defMode = GameMode.Home;

        public List<string> badThoughts;
        float stress = 0.5f;
        Vector2 sweetSpot = new Vector2(0.4f, 0.6f);
        public Vector2 map = new Vector2(20, 12);
        public int tileSize = 64;
        public GameObject player;
        public GameObject currentCanvas;
        public List<GameObject> canvases;
        public Camera cam;
        public AStar pathing = new AStar(pType.TileBased);


        protected override void Awake()
        {
            //Debug.Log("GF_Awake()");
            currentCanvas.BroadcastMessage("setPlayer");
            cam = Camera.main;

        }
        
        public void loadSelectedGame()
        {
            Debug.Log("GM.framework Loading");
            initGame();
            //callMiniGame(GameMode.MiniGameA);

        }

        //GM.Get().scene.miscRefs[0] = "Welcome to Paper Slots"; <-- Example how to reference stuff
        // miscRef are in editor in _SM game object along with button ref for buttons.

        void callMiniGame(GameMode gm)
        {
            switch (gm)
            {
                case GameMode.MiniGameA:
                    //GM.Get().scene.miscRefs[15].SetActive(false);
                    //GM.Get().scene.miscRefs[16].SetActive(true);
                    break;
            }
            return;
        }

        //call this at the end of your mini game to take the results
        public void endMiniGame()
        {
            //GM.Get().scene.miscRefs[15].SetActive(true);
            //GM.Get().scene.miscRefs[16].SetActive(false);
        }

        void initGame()
        {

        }

        public void procCmds(int buttonIndex)
        {
            switch (buttonIndex)
            {
                case 0: //
                    break;
            }

        }

        public void mouseEvent(int type, Vector2 pos)
        {
            switch (type)
            {
                case 0: //Mouse Down
                    Debug.Log(" Go To " + GM.Get().scene.miscRefs[0].GetComponent<gridParent>().pointToTile(pos));
                    //pathing.goToPos(GM.Get().scene.miscRefs[0].GetComponent<gridParent>().pointToTile(pos));
                    break;
                case 1: //Mouse Drag
                    break;
                case 2: //Mouse Up
                    break;
            }
        }
    }
}
