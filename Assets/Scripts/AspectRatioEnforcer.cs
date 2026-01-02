using UnityEngine;

public class AspectRatioEnforcer : MonoBehaviour
{
    private float targetAspect = 16f / 9f; // 16:9 aspect ratio

    void Start()
    {
        ApplyAspectRatio();
    }

    void ApplyAspectRatio()
    {
        Camera cam = GetComponent<Camera>();
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f) // Letterbox (black bars on top/bottom)
        {
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            cam.rect = rect;
        }
        else // Pillarbox (black bars on left/right)
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        // Reapply in editor when Game view size changes
        ApplyAspectRatio();
    }
#endif
}
