using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyZigZag : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dropChance = .2f;
    [SerializeField] private float frequency = 5f;
    [SerializeField] private float magnitude = 1f;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject[] dropPrefab;

    [SerializeField] private GameObject gunPoint;

    private Vector3 axis;
    private Vector3 pos;
    void Start()
    {
        StartCoroutine(Shoot());

        pos = transform.position;
        axis = transform.up;
    }

    void Update()
    {
        pos += Vector3.left * speed * Time.deltaTime;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot()
    {
        while (true) 
        {
            Instantiate(laserPrefab, gunPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LaserPlayer"))
        {
            Destroy(collision.gameObject);
            Death();
        }
    }

    private void Death()
    {
        TryDropItem();
        Destroy(gameObject);
    }

    private void TryDropItem()
    {
        float roll = Random.Range(0f, 1f);

        if (roll <= dropChance)
        {
            Instantiate(dropPrefab[Random.Range(0, 5)], transform.position, Quaternion.identity);
        }
    }

}
