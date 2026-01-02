using UnityEngine;

public class DestroyBehindPlayer : MonoBehaviour
{
    [SerializeField] private float destroyZ = -10f;

    void Update()
    {
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
