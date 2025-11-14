using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGameCollection.Games2025.Team10
{
    public class WinScreenController : MonoBehaviour
    {
        public GameObject redWins; // GameObject that holds all the UI stuff for Red Win screen
        public GameObject blueWins; // GameObject that holds all the UI stuff for Blue Win screen

        public float displayTime = 10f; // How long the WinScreen stays on for before going back to title screen

        void Start()
        {
            // Check which player won from WinnerInfo (true is red false is blue)
            if (WinnerInfo.WinnerIsRed)
            {
                // If red won then turn on the RedWin game object
                if (redWins)
                {
                    redWins.SetActive(true);
                }
            }
            else
            {
                // If red did not win then turn on the BLueWin game object
                if (blueWins)
                {
                    blueWins.SetActive(true);
                }
                
            }

            // Start counting for how long to stay on this scene for
            StartCoroutine(ReturnToTitleAfterDelay());
        }

        private System.Collections.IEnumerator ReturnToTitleAfterDelay()
        {
            // Wait for the number of seconds in displayTime
            yield return new WaitForSeconds(displayTime);

            // Then after waiting go back to the TitleScreen scene
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
