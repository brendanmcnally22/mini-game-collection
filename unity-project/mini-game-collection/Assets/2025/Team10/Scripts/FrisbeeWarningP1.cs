using UnityEngine;

public class FrisbeeWarningP1 : MonoBehaviour
{
    public GameObject warningGlow; // assign Player 1's glowstick
    private Camera mainCam;
    private bool isOffScreen = false;

    void Start()
    {
        mainCam = Camera.main ?? FindObjectOfType<Camera>();

        if (warningGlow != null)
            warningGlow.SetActive(false);
    }

    void Update()
    {
        if (mainCam == null) return;

        Vector3 viewportPos = mainCam.WorldToViewportPoint(transform.position);

        // Only trigger when frisbee fully exits view horizontally
        bool currentlyOffScreen = viewportPos.x < 0f || viewportPos.x > 1f;

        if (currentlyOffScreen && !isOffScreen)
        {
            isOffScreen = true;
            if (warningGlow != null)
                warningGlow.SetActive(true);
        }
        else if (!currentlyOffScreen && isOffScreen)
        {
            isOffScreen = false;
            if (warningGlow != null)
                warningGlow.SetActive(false);
        }
    }
}
