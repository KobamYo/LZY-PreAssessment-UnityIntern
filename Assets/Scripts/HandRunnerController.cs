using UnityEngine;
using Mediapipe.Unity;

public class HandRunnerController : MonoBehaviour
{
    [Header("MediaPipe")]
    [SerializeField] private MultiHandLandmarkListAnnotation handAnnotations;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Sensitivity")]
    [SerializeField] private float sensitivity = 60f;   // world units per hand movement

    [Header("Limits")]
    [SerializeField] private float minX = -30f;
    [SerializeField] private float maxX = 20f;

    [Header("Smoothing")]
    [SerializeField] private float smoothSpeed = 10f;
    
    private bool calibrated = false;
    private float centerHandX;
    private float startPlayerX;

    void Update()
    {
        if (handAnnotations == null || handAnnotations.transform.childCount == 0)
        {
            calibrated = false;
            return;
        }

        var hand = handAnnotations.transform.GetChild(0)
                        .GetComponent<HandLandmarkListAnnotation>();

        if (hand == null)
            return;

        float handX = Camera.main
            .WorldToViewportPoint(hand[0].transform.position).x;

        if (!calibrated)
        {
            centerHandX = handX;
            startPlayerX = player.position.x;
            calibrated = true;
            return;
        }

        float offset = handX - centerHandX;
        float targetX = startPlayerX + offset * sensitivity;
        targetX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 pos = player.position;
        pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * smoothSpeed);
        player.position = pos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
