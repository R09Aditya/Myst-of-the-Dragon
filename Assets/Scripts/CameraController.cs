using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform player; 
    public float triggerX = -11.121f;
    public Vector3 fixedPosition = new Vector3(22.62f, 0f, -10f);
    public Vector3 followOffset = new Vector3(0f, 0f, -10f);
    public float followSmoothTime = 0.15f;
    public bool smoothTransition = true;
    public float moveSpeed = 5f;
    public bool stayFixedAfterTrigger = false;
    private Vector3 velocity = Vector3.zero;
    private bool isFixed = false;
    private float fixedY;

    private void Awake()
    {
        if (player == null)
        {
            var found = GameObject.FindGameObjectWithTag("Player");
            if (found != null) player = found.transform;
            else Debug.LogWarning("CameraController: Player not assigned and no GameObject with tag 'Player' found. Assign player in inspector.");
        }

        fixedY = transform.position.y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, 0f, 65.5f), 
            fixedY, 
            (player != null ? followOffset.z : fixedPosition.z)
        );
    }

    private void LateUpdate()
    {
        if (player == null) return;

        if (player.position.x < triggerX)
            isFixed = true;
        else if (!stayFixedAfterTrigger)
            isFixed = false;

        if (isFixed)
            MoveToFixed();
        else
            FollowPlayer();
    }

    private void MoveToFixed()
    {
        Vector3 target = new Vector3(fixedPosition.x, fixedY, fixedPosition.z);

        target.x = Mathf.Clamp(target.x, 0f, 65.5f);

        if (smoothTransition)
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveSpeed);
        else
            transform.position = target;
    }

    private void FollowPlayer()
    {
        Vector3 target = new Vector3(player.position.x + followOffset.x, fixedY, followOffset.z);

        target.x = Mathf.Clamp(target.x, 0f, 65.5f);

        if (smoothTransition)
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, followSmoothTime);
        else
            transform.position = target;
    }
}
