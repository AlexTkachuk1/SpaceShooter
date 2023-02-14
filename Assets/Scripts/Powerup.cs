using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _powerupID;
    private AudioSource _powerUpSound;
    enum PowerupType
    {
        Triple_Shot_Powerup = 0,
        Speed_Powerup = 1,
        Shield_Powerup = 2,
    }
    void Start()
    {
        _powerUpSound = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -4f) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch ((PowerupType) _powerupID)
                {
                    case PowerupType.Triple_Shot_Powerup:
                        player.ActivateTripleShot();
                        break;
                    case PowerupType.Speed_Powerup:
                        player.ActivateSpeedBoost();
                        break;
                    case PowerupType.Shield_Powerup:
                        player.ActivateShieldBoost();
                        break;
                }
                _powerUpSound.Play();
            }
            Destroy(this.gameObject);
        }
    }
}
