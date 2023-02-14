using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _minPosY = -7f;
    [SerializeField]
    private float _spawnPosY = 11f;
    [SerializeField]
    private int _gamePoints = 10;
    private Animator _anim;
    private AudioSource _explosionSound;
    private UIManager _UIManager;
    [SerializeField]
    private bool _stopFier = false;
    [SerializeField]
    private GameObject _laserPrifab;


    void Start()
    {
        _explosionSound = GameObject.Find("Explosion").GetComponent<AudioSource>();
        _anim = gameObject.GetComponent<Animator>();
        _UIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        StartCoroutine(FierRoutine());
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < _minPosY)
        {
            transform.position = new Vector3(Random.Range(-15.5f, 15.5f), _spawnPosY, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _UIManager.IncreaseScore(_gamePoints);
            _UIManager.CheckForBestScore();
            Destroy(other.gameObject);
            DestroyEnemy();
        }
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Damage();
            DestroyEnemy();
        }
    }
    IEnumerator FierRoutine()
    {
        while (!_stopFier)
        {
            yield return new WaitForSeconds(Random.Range(3f, 8f));
            Instantiate(_laserPrifab, transform.position, Quaternion.identity);
        }
    }
    void DestroyEnemy()
    {
        Destroy(GetComponent<Collider2D>());
        _anim.SetTrigger("OnEnemyDeath");
        _speed /= 3;
        Destroy(gameObject, 1f);
        _explosionSound.Play();
    }
}
