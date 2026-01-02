using UnityEngine;

public class WorldScroller : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            return;
            
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
