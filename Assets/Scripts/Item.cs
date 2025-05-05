using UnityEngine;
using static Item;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Weapon_Single,
        Weapon_Burst,
        Weapon_Laser,
        Health,
        Speed,
        FireRate
    }

    public ItemType itemType;
    public float speed = 2f;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandleItemPickup();
            Destroy(gameObject);
        }
    }

    private void HandleItemPickup()
    {
        switch (itemType)
        {
            case ItemType.Weapon_Single:
                Debug.Log("Weapon_Single picked up");
                break;

            case ItemType.Weapon_Burst:
                Debug.Log("Weapon_Burst picked up");
                break;

            case ItemType.Weapon_Laser:
                Debug.Log("Weapon_Laser picked up");
                break;

            case ItemType.Health:
                Debug.Log("Health picked up");
                player.ModifyHP(20);
                break;

            case ItemType.Speed:
                Debug.Log("Speed picked up");
                player.ModifySpeed(.25f);
                break;

            case ItemType.FireRate:
                Debug.Log("FireRate picked up");
                player.ModifyFireRate(-.1f);
                break;
        }
    }
}
