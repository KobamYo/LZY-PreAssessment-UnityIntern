using UnityEngine;
using TMPro;
using Mediapipe.Unity;

public class HandPositionDebug : MonoBehaviour
{
    [Header("MediaPipe")]
    public MultiHandLandmarkListAnnotation handAnnotations;

    [Header("Debug UI")]
    public TextMeshProUGUI debugText;

    [Header("Mapping")]
    public float minX = -3f;
    public float maxX = 3f;

    void Update()
    {
        if (handAnnotations == null || handAnnotations.transform.childCount == 0)
        {
            debugText.text = "No hand detected";
            return;
        }

        var hand = handAnnotations.transform.GetChild(0)
                        .GetComponent<HandLandmarkListAnnotation>();

        if (hand == null)
            return;

        // Wrist
        var wristWorld = hand[0].transform.position;
        var viewport = Camera.main.WorldToViewportPoint(wristWorld);

        float normalizedX = Mathf.Clamp01(viewport.x);
        float mappedX = Mathf.Lerp(minX, maxX, normalizedX);

        debugText.text =
            $"Viewport X: {viewport.x:F2}\n" +
            $"Normalized X: {normalizedX:F2}\n" +
            $"Mapped X: {mappedX:F2}";
    }
}
