using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Vector3 direction;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;


        if (transform.position.x < -20f || transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
    }
}
