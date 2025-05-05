using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject gunPoint_1;
    [SerializeField] private GameObject gunPoint_2;
    [SerializeField] private float hp = 100f;

    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioClip shootSFX;

    private float timer = .5f;
    private PauseMenu menuThing;
    void Start()
    {
        menuThing = GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenu>();
    }

    void Update()
    {
        Movement();
        ClampMovement();
        Shoot();
    }

    void Movement()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * speed * Time.deltaTime);
    }

    void ClampMovement()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -8.4f, 8.4f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    void Shoot()
    {
        timer += 1 * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && timer > fireRate)
        {
            Instantiate(laserPrefab, gunPoint_1.transform.position, Quaternion.identity);
            Instantiate(laserPrefab, gunPoint_2.transform.position, Quaternion.identity);
            timer = 0;
            if (shootSFX != null) AudioSource.PlayClipAtPoint(shootSFX, transform.position, .5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LaserEnemy"))
        {
            hp -= 20;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp -= 50;
            GameObject vfx = Instantiate(deathVFX, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
            if (deathVFX != null)
            {
                GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
                Destroy(vfx, 2f);
            }

            if (deathSFX != null) AudioSource.PlayClipAtPoint(deathSFX, transform.position, .5f);

            menuThing.GameOver();
            menuThing.audioSource.Stop();
        }
    }

    public void ModifySpeed(float amount)
    {
        speed += amount;
        speed = Mathf.Max(0, speed);
    }

    public void ModifyFireRate(float amount)
    {
        fireRate += amount;
        fireRate = Mathf.Clamp(fireRate, 0.05f, 5f);
    }

    public void ModifyHP(float amount)
    {
        hp += amount;
        hp = Mathf.Clamp(hp, 0, 100);
    }
}
