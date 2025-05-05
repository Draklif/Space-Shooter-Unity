using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private Vector3 dir;
    [SerializeField] private float imageWidth;

    private Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float remainder = (vel * Time.time) % imageWidth;
        transform.position = initialPos + remainder * dir;
    }
}
