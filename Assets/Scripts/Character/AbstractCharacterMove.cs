using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AbstractCharacterMove : MonoBehaviour, IMove, IJump
{
    [SerializeField] private float _lineChangeSpeed = 6;
    [SerializeField] private float _lineStep = 0;

    internal Vector3 _targetPos;
    internal Vector3 _targetSpeed;

    internal Rigidbody _rb;
    internal Animator _animator;

    internal int _line = 0;

    internal bool _isPlay;

    [SerializeField] public float _speed { set; get; }

    //////////////////////////

    [SerializeField] private float _jumpForce = 2f;


    private bool _isGround;

   // internal bool _isJump;



    internal void CheckPosition()
    {

        if ((_rb.transform.position.x > _targetPos.x && _targetSpeed.x > 0) || (_rb.transform.position.x < _targetPos.x && _targetSpeed.x < 0))
        {
            _targetSpeed = Vector3.zero;
            _rb.velocity = _targetSpeed;
            //_rb.transform.position = _targetSpeed;
           _rb.position = _targetPos; // ���� ������ = ������� �������� �� ����� ������ 

        }


    }
    public void MoveForward()
    {
      _rb.transform.position += new Vector3(0, 0, _speed * Time.fixedDeltaTime);
    }

    public void Move(ref int line)
    {

        if (Input.GetKeyDown(KeyCode.D)) //&& _isJump == false) ///&& _rb.transform.position.x < -_lineStep)
        {
            MoveRight(ref line);
        }

        if (Input.GetKeyDown(KeyCode.A)) //&& _isJump == false)// && _rb.transform.position.x > _lineStep)
        {
            MoveLeft(ref line);
        }



    }

    public void MoveRight(ref int line)
    {
        switch (line)
        {
            case 0:
                {

                    _targetSpeed = new Vector3(_lineChangeSpeed, 0, 0);
                    _targetPos = new Vector3(_targetPos.x + _lineStep, _rb.transform.position.y, _rb.transform.position.z); //0?
                    _rb.velocity = _targetSpeed;

                    line++;
                    if (_rb.position.y < 0.1)
                    {
                        _animator.Play("StrafeRight");
                    }


                    break;

                }

            case -1:
                {

                    _targetSpeed = new Vector3(_lineChangeSpeed, 0, 0);
                    _targetPos = new Vector3(_targetPos.x + _lineStep, _rb.transform.position.y, _rb.transform.position.z); //0?
                    _rb.velocity = _targetSpeed;

                    line++;
                    if (_rb.position.y < 0.1)
                    {
                        _animator.Play("StrafeRight");
                    }

                    break;

                }

        }

    }

    public void MoveLeft(ref int line)
    {
        switch (line)
        {
            case 0:
                {

                    _targetSpeed = new Vector3(-_lineChangeSpeed, 0, 0);
                    _targetPos = new Vector3(_targetPos.x - _lineStep, _rb.transform.position.y, _rb.transform.position.z);
                    _rb.velocity = _targetSpeed;

                    line--;
                    if (_rb.position.y < 0.1)
                    {
                        _animator.Play("StrafeLeft");
                    }



                    break;
                }

            case 1:
                {

                    _targetSpeed = new Vector3(-_lineChangeSpeed, 0, 0);
                    _targetPos = new Vector3(_targetPos.x - _lineStep, _rb.transform.position.y, _rb.transform.position.z);
                    _rb.velocity = _targetSpeed;

                    line--;
                    if (_rb.position.y < 0.1)
                    {
                        _animator.Play("StrafeLeft");
                    }


                    break;
                }

        }

    }



    public void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
           
            _animator.Play("RunningJump");
            _rb.velocity = Vector3.up * _jumpForce;
            // _rb.velocity = new Vector3(0, 1, 0) * _jumpForce;
            _isGround = false;
          //  _isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _rb.velocity = new Vector3(0, -1, 0) * _jumpForce;
            _animator.Play("Roll");
        }
    }
    internal void FallJump()
    {
        if (_rb.transform.position.y > 5)
        {
            _animator.Play("Fall");
        }


        if ((_rb.transform.position.y > 0.01f && Input.GetKeyDown(KeyCode.A)) || (_rb.transform.position.y > 0.01f && Input.GetKeyDown(KeyCode.D)) ) //out of if 
        {
            _animator.Play("Fall");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGround = true;
       // _isJump = false;
    }
}