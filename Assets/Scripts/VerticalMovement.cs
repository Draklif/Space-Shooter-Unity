using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float range = 3f;

    private Vector3 startPos;
    private bool goingUp = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float moveY = speed * Time.deltaTime * (goingUp ? 1 : -1);
        transform.Translate(Vector3.up * moveY);

        if (Mathf.Abs(transform.position.y - startPos.y) >= range)
        {
            goingUp = !goingUp;
        }
    }
}
