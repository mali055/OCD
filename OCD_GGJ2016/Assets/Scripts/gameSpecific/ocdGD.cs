﻿//----------------------------------------
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
using UnityEngine;
using System.Collections;

namespace Meshadieme {
	public enum Scenes {
		MAIN = 0,
	}

	public class ocdGD : gameData {

		//Save Game Variables
		public bool isMusicOff;
		public string[] highScoreName;
		public float[] highScore;
        
		public override void initGameData() {
			initSaveGame();
			//Other Game Data = ScriptableObjects / Custom Classes / Character Stats etc.
		}
		
		public override void initSaveGame() {
			Debug.Log ("InitSaveGame");
			if (!PlayerPrefs.HasKey("_isMusicOff") || GM.Get ().deleteSave ) {
				if ( GM.Get ().deleteSave ) GM.Get ().deleteSave = false;
				resetSave ();
			} else {
                isMusicOff = PlayerPrefsX.GetBool("_isMusicOff");
                highScoreName = PlayerPrefsX.GetStringArray("_highScoreName");
                highScore = PlayerPrefsX.GetFloatArray("_highScore");
            }
		}
		
		public override void resetSave () {
			Debug.Log ("resetSave");
			isMusicOff = false;
			GM.Get().framework.gMode = GameMode.Home;
            isMusicOff = false;
            highScoreName = new string[] { "Ali", "Martin", "Elio" };
            highScore = new float[] { 501.0f, 500.0f, 499.0f };

        }
		
		public override void saveGame () {
			Debug.Log ("saveGame");
			PlayerPrefsX.SetBool("_isMusicOff", isMusicOff);
            PlayerPrefsX.SetStringArray("_highScoreName", highScoreName);
            PlayerPrefsX.SetFloatArray("_highScore", highScore);

            PreviewLabs.PlayerPrefs.Flush();
		}

	}
}
