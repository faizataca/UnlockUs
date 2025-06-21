using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float wallThickness = 1f;
    public float wallLengthBuffer = 2f; // Sedikit buffer biar gak mepet

    private GameObject leftWall, rightWall, topWall, bottomWall;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        leftWall = CreateWall("LeftWall");
        rightWall = CreateWall("RightWall");
        topWall = CreateWall("TopWall");
        bottomWall = CreateWall("BottomWall");
    }

    void LateUpdate()
    {
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = cam.aspect * camHalfHeight;
        Vector3 camPos = cam.transform.position;

        // Posisi dan ukuran wall dinamis
        leftWall.transform.position = new Vector3(
            camPos.x - camHalfWidth - wallThickness / 2f, camPos.y, 0f);
        leftWall.GetComponent<BoxCollider2D>().size = new Vector2(
            wallThickness, camHalfHeight * 2f + wallLengthBuffer);

        rightWall.transform.position = new Vector3(
            camPos.x + camHalfWidth + wallThickness / 2f, camPos.y, 0f);
        rightWall.GetComponent<BoxCollider2D>().size = new Vector2(
            wallThickness, camHalfHeight * 2f + wallLengthBuffer);

        topWall.transform.position = new Vector3(
            camPos.x, camPos.y + camHalfHeight + wallThickness / 2f, 0f);
        topWall.GetComponent<BoxCollider2D>().size = new Vector2(
            camHalfWidth * 2f + wallLengthBuffer, wallThickness);

        bottomWall.transform.position = new Vector3(
            camPos.x, camPos.y - camHalfHeight - wallThickness / 2f, 0f);
        bottomWall.GetComponent<BoxCollider2D>().size = new Vector2(
            camHalfWidth * 2f + wallLengthBuffer, wallThickness);
    }

    GameObject CreateWall(string name)
    {
        GameObject wall = new GameObject(name);
        wall.transform.parent = transform;

        // Tambahkan collider
        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.isTrigger = false;

        // Jadikan static rigidbody
        Rigidbody2D rb = wall.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        // Set layer ke InvisibleWall
        wall.layer = LayerMask.NameToLayer("InvisibleWall");

        return wall;
    }
}
