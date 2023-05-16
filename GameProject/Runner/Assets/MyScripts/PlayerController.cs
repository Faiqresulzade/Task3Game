using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] List<GameObject> potatoStairs;

    private float _speed = 2.5f;
    private float _verticalSpeed = 1.5f;
    private float jumpForce = 350;
    private Vector3 _direction;
    private Rigidbody _physics;
    private Animator _animator;
    private float _tomatoCount;
    private float _posYForCollect;
    private GameObject _box;
    private float _timer;
    private int _collectpotatoStairs=0;
    private int i = 0;
    private bool isFinishCollider;
    private float _speedFinish=0.1f;

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
        // FinishStairs();
        if (isFinishCollider)
        {
            transform.position += new Vector3(0, transform.position.y * Time.deltaTime, transform.position.z * Time.deltaTime * _speedFinish) ;
        }

    }

    private void FinishStairs()
    {
        Debug.Log("1");
        for (int layer = 7; layer < 17; layer++)
        {
            Debug.Log("2");

            if (Physics.Raycast(transform.position, -1 * transform.up, 1.5f, layer))
            {
                Debug.Log("3");

                for (int i = _collectpotatoStairs; i < potatoStairs.Count; i++)
                {
                    Debug.Log("4");

                    potatoStairs[i].transform.gameObject.SetActive(true);
                    _collectpotatoStairs++;
                    if (_collectpotatoStairs % 2 == 1) return;
                }

                //foreach (var potato in potatoStairs)
                //{
                //    potato.transform.gameObject.SetActive(true);
                //}
            }
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
            transform.position = new Vector3(0, -0.45f, -48.12f);
            _speed = 1.5f;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            FinishGame(collision);

        }
        if (collision.transform.CompareTag("FinishCollider"))
        {
            isFinishCollider = true;
            collision.transform.gameObject.SetActive(false);
            Debug.Log("hello");
            Debug.Log(i);
            potatoStairs[i].transform.gameObject.SetActive(true);
            potatoStairs[i + 1].transform.gameObject.SetActive(true);
            i += 2;
           
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5), transform.position.y+3, transform.position.z);
            //_physics.MovePosition(transform.position + _direction * _speed * Time.deltaTime);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.CompareTag("FinishCollider"))
    //    {
    //        Debug.Log("hello");
    //        Debug.Log(i);
    //        potatoStairs[i].transform.gameObject.SetActive(true);
    //        potatoStairs[i+1].transform.gameObject.SetActive(true);
    //        i+=2;
    //    }
    //}

    void FixedUpdate()
    {
        PlayerMove();
    }


    private void PlayerJump()
    {
        if (Physics.Raycast(transform.position, -1 * transform.up, 1.5f, layer))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetBool("Jump", true);
                _physics.AddForce(Vector3.up * jumpForce);
            }

        }
        if (Physics.Raycast(transform.position, -1 * transform.up, 0.5f, layer))
        {
            _animator.SetBool("Jump", false);
        }
    }

    private void PlayerMove()
    {
        _speed += 0.004f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5), transform.position.y, transform.position.z);
        _physics.MovePosition(transform.position + _direction * _speed * Time.deltaTime);
    }

    private void FinishGame(Collision collision)
    {
      //  _speed = 0;
        Destroy(collision.gameObject);
        //CollectTomato();
        //_physics.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void PlayerCollect(Collision collision)
    {
        _posYForCollect += .5f;
        collision.gameObject.transform.position = new Vector3(transform.position.x, _posYForCollect + transform.position.y, transform.position.z - 0.5f);
        collision.gameObject.transform.parent = transform;
        _tomatoCount++;
    }

    private void CollectTomato()
    {
        Debug.Log(_tomatoCount);
        StartCoroutine(nameof(Finish));
        for (int i = 0; i < _tomatoCount; i++)
        {
            // Thread.Sleep(1000);
            _box.transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i + 3).gameObject.SetActive(false);
        }
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(23f);
    }

}
