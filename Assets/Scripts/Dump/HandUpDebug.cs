using UnityEngine;
using TMPro;
using Mediapipe.Unity;

public class HandUpDebug : MonoBehaviour
{
    [Header("MediaPipe")]
    public MultiHandLandmarkListAnnotation handAnnotations;

    [Header("Debug UI")]
    public TextMeshProUGUI debugText;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float handUpThreshold = 0.4f;

    void Update()
    {
        if (handAnnotations == null)
        {
            debugText.text = "No annotation reference";
            return;
        }

        int handCount = handAnnotations.transform.childCount;

        if (handCount == 0)
        {
            debugText.text = "No hands detected";
            return;
        }

        string result = "";

        for (int i = 0; i < handCount; i++)
        {
            var handGO = handAnnotations.transform.GetChild(i);
            var hand = handGO.GetComponent<HandLandmarkListAnnotation>();

            if (hand == null)
                continue;

            // Wrist landmark
            var wristWorld = hand[0].transform.position;
            var wristViewport = Camera.main.WorldToViewportPoint(wristWorld);

            bool isHandUp = wristViewport.y > handUpThreshold;

            // Determine left/right based on X position
            string side = wristViewport.x < 0.5f ? "LEFT" : "RIGHT";

            result += $"{side} HAND | Y: {wristViewport.y:F2} | UP: {isHandUp}\n";
        }

        debugText.text = result;
    }
}
