using UnityEngine;

public class CameraFollow2Players : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float followSpeed = 5f;

    private Vector3 offset;

    void Start()
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogError("Assign player1 and player2!");
            enabled = false;
            return;
        }

        offset = new Vector3(0, 0, transform.position.z);
    }

    void LateUpdate()
    {
        Vector3 centerPoint = (player1.position + player2.position) / 2f;
        Vector3 targetPosition = new Vector3(centerPoint.x, centerPoint.y, 0f) + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
