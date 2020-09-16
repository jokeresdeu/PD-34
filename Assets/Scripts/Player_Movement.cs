using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D _playerRB;
    private SpriteRenderer _playerSprite;
    private Animator _playerAnimator;

    [Header("Horizontal movement")]
    [SerializeField] float _speed;

    [Header("Jump")]
    [SerializeField] float _jumpForce;
    [SerializeField] bool _airControll;
    [SerializeField] float _radius;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _whatIsGround;

    [Header("Crouch")]
    [SerializeField] Transform _cellCheck;
    [SerializeField] Collider2D _headCollider;
    [Range(0, 1)]
    [SerializeField] private float _croachSpeed;

    bool _grounded;
    bool _canStand;
    bool _secondJump = true;
    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<Animator>();
    }

    public void Move(float move, bool jump, bool crouch)
    {
        float modificator = _headCollider.enabled ? 1 : _croachSpeed;

        if (move != 0 && (_grounded || _airControll))
            _playerRB.velocity = new Vector2(_speed * move * modificator, _playerRB.velocity.y);
        else if (move == 0 && _grounded)
            _playerRB.velocity = new Vector2(0, _playerRB.velocity.y);

        if (move > 0 && _playerSprite.flipX)
            _playerSprite.flipX = false;
        else if (move < 0 && !_playerSprite.flipX)
            _playerSprite.flipX = true;

        if (jump )
        {
            if(_grounded)
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, _jumpForce);
            else if(_secondJump)
            {
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, _jumpForce);
                _secondJump = false;
            }
        }

        if (crouch)
            _headCollider.enabled = false;
        else if (!crouch && _canStand)
            _headCollider.enabled = true;

        
        if(Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround))
        {
            _grounded = true;
            _secondJump = true;
        }
        else
        {
            _grounded = false;
        }

        _canStand = !Physics2D.OverlapCircle(_cellCheck.position, _radius, _whatIsGround);

        _playerAnimator.SetFloat("Speed", Mathf.Abs(move));
        _playerAnimator.SetBool("Jump", !_grounded);
        _playerAnimator.SetBool("Crouch", !_headCollider.enabled);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_cellCheck.position, _radius);
    }
}
