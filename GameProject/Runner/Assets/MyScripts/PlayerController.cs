using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject finishCollider;
    [SerializeField] private CinemachineVirtualCamera awayCam;
    [SerializeField] private CinemachineVirtualCamera mainCam;


    private float _speed = 10f;
    private float _verticalSpeed = .7f;
    private float jumpForce = 350;
    private Vector3 _direction;
    private Rigidbody _physics;
    private Animator _animator;
    private int _tomatoCount;
    private float _posYForCollect;
    private GameObject _box;
    private float _timer;
    private int _collectpotatoStairs = 0;
    private int i = 0;
    private bool isFinishCollider;
    private float _speedFinish = 0.1f;


    void Start()
    {
        _physics = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Walk", true);
        _box = GameObject.FindGameObjectWithTag("Box_1");
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        PlayerJump();

        _direction = new Vector3(16 * Input.GetAxis("Horizontal"), 0, _verticalSpeed);
        if (isFinishCollider)
        {
            transform.position += new Vector3(0, transform.position.y * Time.deltaTime, transform.position.z * Time.deltaTime * _speedFinish);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomato"))
        {
            PlayerCollect(collision);

        }
        if (collision.gameObject.CompareTag("Fence") || collision.gameObject.CompareTag("Rock"))
        {
            transform.position = new Vector3(0, -0.45f, -170.4f);
            _speed = 1.5f;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            FinishGame(collision);

        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("Jump", false);
        }
        if (collision.transform.CompareTag("FinishGround"))
        {
            Debug.Log("hhjklkjhhjkl");
           transform.position= Vector3.Lerp(transform.position, finishCollider.transform.position, 5f * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        PlayerMove();
    }


    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1f, layer))
            {

                _animator.SetBool("Jump", true);
                _physics.AddForce(Vector3.up * jumpForce);
            }
            if (Physics.Raycast(transform.position, -1 * transform.up, 0.4f, layer))
            {
                _animator.SetBool("Jump", false);
            }

        }
        if (Physics.Raycast(transform.position, -1 * transform.up, 0.4f, layer))
        {
            _animator.SetBool("Jump", false);
        }
    }

    private void PlayerMove()
    {
        _speed *= 1.0002f;
        transform.position += _direction * _speed * Time.deltaTime;
        _physics.MovePosition(transform.position + _direction * _speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4, 4), transform.position.y, transform.position.z);
    }

    private void FinishGame(Collision collision)
    {
        Debug.Log("gfsdfghjkl;");
        _animator.SetBool("Walk", false);
        _speed = 0;
        Destroy(collision.gameObject);
        CollectTomato();
        _physics.constraints = RigidbodyConstraints.FreezePositionZ;
       
        awayCam.Priority = 10;

    }

    private void PlayerCollect(Collision collision)
    {
        _posYForCollect += .5f;
        collision.gameObject.transform.position = new Vector3(transform.position.x, _posYForCollect + (transform.position.y+1f), transform.position.z - 0.5f);
        collision.gameObject.transform.parent = transform;
        _tomatoCount++;
        UIManeger.Instance.UpdateCoinValue(_tomatoCount);
    }

    private void CollectTomato()
    {
        for (int i = 0; i < _tomatoCount; i++)
        {
            _box.transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i + 2).gameObject.SetActive(false);
        }
    }
}
