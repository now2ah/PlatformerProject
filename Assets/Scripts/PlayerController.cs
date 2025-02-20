using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public enum eAnimState
    {
        IDLE,
        MOVE,
        JUMP,
        DIE,
        CLEAR
    }
    
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    Rigidbody2D _rigidbody;
    Collider2D _collider;
    LayerMask _groundLayer;
    float _axisH = 0f;
    eAnimState _curAnim;
    eAnimState _prevAnim;
    
    bool _isJumping = false;
    bool _isOnGround = false;
    bool _isDead = false;
    bool _isClear = false;

    public float Speed = 3f;
    public float _jumpForce = 5f;


    void _GetInput()
    {
        if (_isDead)
            return;

        _axisH = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _Jump();
        }
    }

    void _SetDirection()
    {
        if (_axisH > 0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (_axisH < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        //need to fix
        //if (_axisH > 0f)
        //{
        //    _spriteRenderer.flipX = !_spriteRenderer.flipX;
        //}
        //else if (_axisH < 0f)
        //{
        //    _spriteRenderer.flipX = !_spriteRenderer.flipX;
        //}
    }

    void _Jump()
    {
        _isJumping = true;
    }

    void _CheckOnGround()
    {
        _isOnGround = Physics2D.Linecast(transform.position, transform.position + (transform.up * -0.1f), _groundLayer);
    }

    void _JumpProcess()
    {
        if (_isOnGround && _isJumping)
        {
            Vector2 force = new Vector2(0, _jumpForce);
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            _isJumping = false;
        }
    }

    void _CalculateVelocity()
    {
        if (_isDead)
        {
            _rigidbody.linearVelocity = Vector2.zero;
        } else if (_isOnGround || _axisH != 0)
        {
            _rigidbody.linearVelocity = new Vector2(Speed * _axisH, _rigidbody.linearVelocityY);
        }
    }

    void _CheckAnimationState()
    {
        if (_isDead || _isClear)
            return;
        
        if (_isOnGround)
        {
            if (_axisH == 0)
            {
                _curAnim = eAnimState.IDLE;
            }
            else
            {
                _curAnim = eAnimState.MOVE;
            }
        }
        else
        {
            _curAnim = eAnimState.JUMP;
        }

        if (_curAnim != _prevAnim)
        {
            _prevAnim = _curAnim;
            _animator.Play(Enum.GetName(typeof(eAnimState), _curAnim));
        }
    }

    void _Die()
    {
        StartCoroutine(DieCoroutine());
        _rigidbody.AddForce(Vector2.up * 2.0f, ForceMode2D.Impulse);
        _collider.enabled = false;
    }

    IEnumerator DieCoroutine()
    {
        _isDead = true;
        _curAnim = eAnimState.DIE;
        _animator.Play(Enum.GetName(typeof(eAnimState), _curAnim));
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Enemy"))
        {
            _Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Goal") && !_isClear)
        {
            _isClear = true;
            _curAnim = eAnimState.CLEAR;
            _animator.Play(Enum.GetName(typeof(eAnimState), _curAnim));
            GameManager.Instance.GameClear();
        }
        else if (collision.tag.Equals("DeadZone"))
        {
            _Die();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _groundLayer = LayerMask.GetMask("Ground");
        GameManager.Instance.Player = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsRunning)
            return;

        _GetInput();
        _SetDirection();
        _CheckAnimationState();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsRunning)
            return;

        _CheckOnGround();
        _CalculateVelocity();
        _JumpProcess();
    }
}
