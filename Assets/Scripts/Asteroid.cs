using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private AudioSource _explosionSound;
    private float _speed = 0.5f;
    private float _rotationSpeed = 12f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    private GameManager _gameManager;

    void Start()
    {
        _explosionSound = GameObject.Find("Explosion").GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed);
        transform.Translate(Vector3.down * Time.deltaTime * _speed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _spawnManager.StartSpawn();
            Destroy(other.gameObject);
            GameObject explosion = Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            _explosionSound.Play();
            Destroy(gameObject);
        }
        if (other.tag == "Player")
        {
            _spawnManager.StartSpawn();
            other.GetComponent<Player>().Damage();
            GameObject explosion = Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            _explosionSound.Play();
            Destroy(gameObject);
        }
    }
}
