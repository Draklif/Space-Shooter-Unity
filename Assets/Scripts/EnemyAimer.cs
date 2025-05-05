using System.Collections;
using UnityEngine;

public class EnemyAimer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dropChance = .2f;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject[] dropPrefab;

    [SerializeField] private GameObject gunPoint;
    [SerializeField] private Transform spriteHolder;

    private Transform player;
    void Start()
    {
        StartCoroutine(Shoot());
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (player != null)
        {
            Vector3 direction = player.position - gunPoint.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spriteHolder.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (transform.position.x < -9f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (player != null)
            {
                GameObject laser = Instantiate(laserPrefab, gunPoint.transform.position, Quaternion.LookRotation(Vector3.forward, gunPoint.transform.right));
                Vector3 direction = gunPoint.transform.up;

                Debug.DrawRay(gunPoint.transform.position, direction * 20f, Color.red, 1.5f);

                Laser laserScript = laser.GetComponent<Laser>();
                if (laserScript != null)
                {
                    laserScript.direction = direction;
                    laserScript.speed = 5f;
                }
            }

            yield return new WaitForSeconds(2.5f);
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
