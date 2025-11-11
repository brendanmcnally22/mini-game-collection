using UnityEngine;

public class FrisbeeWarningP2 : MonoBehaviour
{
    public GameObject warningGlow; // assign Player 2’s glowstick
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

        // Only care about LEFT edge for Player 2
        bool offLeft = viewportPos.x < 0f;
        bool backOnScreen = viewportPos.x >= 0f && viewportPos.x <= 1f;

        if (offLeft && !isOffScreen)
        {
            isOffScreen = true;
            if (warningGlow != null)
                warningGlow.SetActive(true);
        }

        if (backOnScreen && isOffScreen)
        {
            isOffScreen = false;
            if (warningGlow != null)
                warningGlow.SetActive(false);
        }
    }
}
