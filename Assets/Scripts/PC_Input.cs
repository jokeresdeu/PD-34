using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Input : MonoBehaviour
{
    [SerializeField] private Player_Movement _player;
    private float _move;
    private bool _jump;
    private bool _crouch;


    // Update is called once per frame
    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _jump = true;
        }
        _crouch = Input.GetKey(KeyCode.C);
    }

    private void FixedUpdate()
    {
        _player.Move(_move, _jump, _crouch);
        _jump = false;
    }


}
