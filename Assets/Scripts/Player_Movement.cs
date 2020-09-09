using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D _playerRB;
    private SpriteRenderer _playerSprite;

    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] bool _airControll;
    [SerializeField] float _radius;
    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _cellCheck;
    [SerializeField] LayerMask _whatIsGround;
    [SerializeField] Collider2D _headCollider;
    bool _grounded;
    bool _canStand;
    bool _crawling;
    float _move;
    bool _jump;
    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(_move!=0 && (_grounded || _airControll))
            _playerRB.velocity = new Vector2(_speed * _move, _playerRB.velocity.y);
        else if(_move == 0 && _grounded)
            _playerRB.velocity = new Vector2(0, _playerRB.velocity.y);

        if (_move > 0 && _playerSprite.flipX)
            _playerSprite.flipX = false;
        else if (_move < 0 && !_playerSprite.flipX)
            _playerSprite.flipX = true;

        if(_jump && _grounded)
        {
            _playerRB.AddForce(Vector2.up * _jumpForce);
            _jump = false;
        }

        if (_crawling)
            _headCollider.enabled = false;
        else if (!_crawling && _canStand)
            _headCollider.enabled = true;

        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround);
        _canStand = !Physics2D.OverlapCircle(_cellCheck.position, _radius, _whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _jump = true;
        }
        _crawling = Input.GetKey(KeyCode.C);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_cellCheck.position, _radius);
    }
}
