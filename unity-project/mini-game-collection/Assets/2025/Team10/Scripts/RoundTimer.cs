using MiniGameCollection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGameCollection.Games2025.Team10
{
    public class RoundTimer : MonoBehaviour
    {
        [SerializeField] private float roundDuration = 60f; // How long each round lasts in seconds 
        [SerializeField] private CountdownController countdown; // Reference to our countdown controller so that the timer will start after this is finished

        private float timeLeft; // How many seconds left before the round ends
        private bool roundActive = false; // Is the timer currently running or not

        // Starts up at start of level
        void Start()
        {
            // When the level starts start the timer process but in a coroutine
            StartCoroutine(BeginRound());
        }

        // Main coroutine
        private IEnumerator BeginRound()
        {
            // Wait here until the countdown says it is finished
            while (!countdown.countdownFinished)
            {
                // Null here means wait one frame and then check again
                yield return null;
            }

            // When the countdown is done set the countdown to 60 seconds
            timeLeft = roundDuration;

            // Say that the round has started 
            roundActive = true;

            // While there is still time left
            while (timeLeft > 0)
            {
                // Take away a bit of the time every second using deltaTime
                timeLeft -= Time.deltaTime;

                // Pause the loop until the next frame
                yield return null;
            }

            // When timer hits zero stop the round
            roundActive = false;

            // Call the end round function
            EndRound();
        }

        private void EndRound()
        {
            // When time is up send the player back to the title screen
            SceneManager.LoadScene("TitleScreen");
        }
    }
}