using UnityEngine;

public class FrisbeeWarning : MonoBehaviour
{
    public GameObject warningGlow; // assign your glowstick child here
    private Camera mainCam;
    private bool isOffScreen = false;

    void Start()
    {
        mainCam = Camera.main;
        if (warningGlow != null)
            warningGlow.SetActive(false);
    }

    void Update()
    {
        if (mainCam == null) return;

        // Convert frisbee position to viewport coordinates (0-1 range)
        Vector3 viewportPos = mainCam.WorldToViewportPoint(transform.position);

        // Check if off-screen (to the left or right)
        bool currentlyOffScreen = viewportPos.x < 0f || viewportPos.x > 1f;

        // When frisbee goes off-screen, show glowstick
        if (currentlyOffScreen && !isOffScreen)
        {
            isOffScreen = true;
            if (warningGlow != null)
                warningGlow.SetActive(true);
        }

        // When frisbee comes back on-screen, hide glowstick
        if (!currentlyOffScreen && isOffScreen)
        {
            isOffScreen = false;
            if (warningGlow != null)
                warningGlow.SetActive(false);
        }
    }
}
