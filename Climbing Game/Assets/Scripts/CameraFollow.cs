using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject thingToFollow;
    void LateUpdate()
    {
        transform.position = thingToFollow.transform.position + new Vector3(0,0,-10);
    }
}
