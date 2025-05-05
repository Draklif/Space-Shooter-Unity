using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dropChance = .2f;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject[] dropPrefab;
    [SerializeField] private GameObject deathVFX;

    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioClip shootSFX;

    [SerializeField] private GameObject gunPoint;
    void Start()
    {
        StartCoroutine(Shoot());
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

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
            if (shootSFX != null) AudioSource.PlayClipAtPoint(shootSFX, transform.position, .5f);
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

        if (deathVFX != null)
        {
            GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        if (deathSFX != null) AudioSource.PlayClipAtPoint(deathSFX, transform.position, .5f);

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
