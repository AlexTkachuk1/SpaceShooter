using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject[] _areaWithDamage;
    [SerializeField]
    private GameObject _tripleShotLaserPrefab;
    [SerializeField]
    protected AudioSource _fireLaser;
    [SerializeField]
    private float _laserOfSetY;
    [SerializeField]
    private float _fireRate = 0.15f;
    protected float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private UIManager _UIManager;
    private GameManager _gameManager;
    [SerializeField]
    private float _powerUpCoolDownTime = 5f;

    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;

    private Animator _anim;

    private States State
    {
        get { return (States)_anim.GetInteger("State"); }
        set { _anim.SetInteger("State", (int)value); }
    }
    public enum States
    {
        Idle = 0,
        SpaceshipTurnLeft = 1,
        SpaceshipTurnRight = 2,
    }
    void Start()
    {
        _anim = transform.Find("Spaceship").GetComponent<Animator>();
        State = States.Idle;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _UIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _UIManager.increaseNumberOfPlayers();
        if (_gameManager.GetGameMode() == "SinglePlayer") transform.position = new Vector3(0, 0, 0);
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Maanager is NULL");
        }

    }
    protected virtual void CalculateMovement(float horizontalInput, float verticalInput)
    {

        if (horizontalInput < 0)
            State = States.SpaceshipTurnLeft;
        else if (horizontalInput > 0)
            State = States.SpaceshipTurnRight;

        if (State != States.Idle && horizontalInput == 0)
            State = States.Idle;

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -6.6f, 8.6f), 0);

        if (transform.position.x >= 14f)
        {
            transform.position = new Vector3(-15.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -15.5f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }
    }
    protected virtual void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (!_isTripleShotActive) Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserOfSetY, 0), Quaternion.identity);
        else Instantiate(_tripleShotLaserPrefab, transform.position, Quaternion.identity);
        _fireLaser.Play();
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shield.SetActive(false);
            return;
        }
        _lives--;
        _areaWithDamage[Random.Range(1, 7)].SetActive(true);
        if (_lives < 0)
        {
            _lives = 0;
        }
        _UIManager.UpdateLives(_lives);

        if (_lives <= 0)
        {
            _UIManager.decreaseNumberOfPlayers();
            _gameManager.GameOver();
            _spawnManager.OnPlayerDeth();
            _UIManager.SaveBestScore();
            Destroy(gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(_powerUpCoolDownTime);
        _isTripleShotActive = false;
    }

    public void ActivateSpeedBoost()
    {
        _speed *= 2;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(_powerUpCoolDownTime);
        _speed /= 2;
    }

    public void ActivateShieldBoost()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            Destroy(other.gameObject);
            Damage();
        }
    }
}
