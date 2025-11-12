using UnityEngine;
using System.Collections;

namespace MiniGameCollection.Games2025.Team10
{
    public class FrisbeeInventoryP1 : MonoBehaviour
    {
        public GameObject diskChild;
        public float hideDuration = 2f;

        private bool isHidden = false;
        private bool isOnCooldown = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && !isOnCooldown)
            {
                StartCoroutine(HideDiskTemporarily());
            }
        }

        private IEnumerator HideDiskTemporarily()
        {
            isOnCooldown = true;
            isHidden = true;

            if (diskChild != null)
                diskChild.SetActive(false);

            yield return new WaitForSeconds(hideDuration);

            if (diskChild != null)
                diskChild.SetActive(true);

            isHidden = false;
            isOnCooldown = false;
        }
    }
}