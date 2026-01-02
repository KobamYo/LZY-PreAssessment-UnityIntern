using UnityEngine;
using System.Collections;

public class CameraPermissionRequester : MonoBehaviour
{
    IEnumerator Start()
    {
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        }

        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.LogError("Webcam permission denied!");
        }
        else
        {
            Debug.Log("Webcam permission granted.");
        }
    }
}
