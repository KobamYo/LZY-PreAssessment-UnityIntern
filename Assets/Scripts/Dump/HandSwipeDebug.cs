using UnityEngine;
using TMPro;
using Mediapipe.Unity;

public class HandSwipeDebug : MonoBehaviour
{
    [Header("MediaPipe")]
    public MultiHandLandmarkListAnnotation handAnnotations;

    [Header("Debug UI")]
    public TextMeshProUGUI debugText;

    [Header("Swipe Settings")]
    public float swipeThreshold = 0.15f;
    public float cooldownTime = 0.5f;

    private float startX;
    private bool trackingSwipe = false;
    private float cooldown;

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (handAnnotations == null || handAnnotations.transform.childCount == 0)
        {
            debugText.text = "No hand detected";
            trackingSwipe = false;
            return;
        }

        var hand = handAnnotations.transform.GetChild(0)
                        .GetComponent<HandLandmarkListAnnotation>();

        if (hand == null)
            return;

        var wristWorld = hand[0].transform.position;
        float x = Camera.main.WorldToViewportPoint(wristWorld).x;

        if (cooldown > 0f)
        {
            debugText.text = "Cooldown...";
            return;
        }

        if (!trackingSwipe)
        {
            startX = x;
            trackingSwipe = true;
        }

        float deltaX = x - startX;
        string swipe = "NONE";

        if (deltaX > swipeThreshold)
        {
            swipe = "RIGHT";
            trackingSwipe = false;
            cooldown = cooldownTime;
        }
        else if (deltaX < -swipeThreshold)
        {
            swipe = "LEFT";
            trackingSwipe = false;
            cooldown = cooldownTime;
        }

        debugText.text =
            $"StartX: {startX:F2}\n" +
            $"CurrentX: {x:F2}\n" +
            $"ΔX: {deltaX:F2}\n" +
            $"Swipe: {swipe}";
    }
}
