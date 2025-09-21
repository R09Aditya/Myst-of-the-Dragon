using UnityEngine;

public class HeartsFollowCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 offset = new Vector3(-1f, -1f, 10f); // adjust to push into top-right

    void LateUpdate()
    {
        if (mainCamera == null) 
            mainCamera = Camera.main;

        // Convert top-right viewport (1,1) to world position
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane + 10f));

        // Apply offset so it doesn't stick exactly at the edge
        transform.position = topRight + offset;
    }
}
