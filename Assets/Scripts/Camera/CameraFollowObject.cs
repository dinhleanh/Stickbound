using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Cinemachine;


public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.35f;

    private Coroutine _turnCoroutine;
    [SerializeField] private MovementPlayer _player;

    private bool _isFacingRight;

    private void Awake()
    {
        _player = _playerTransform.gameObject.GetComponent<MovementPlayer>();
        
        _isFacingRight = _player.IsFacingRight;
    }

    private void Update()
    {
        //make the cameraFollowObject follow the players position
        transform.position = _player.transform.position;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while(elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            // lerp the y rotation
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f,yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if(_isFacingRight )
        {
            return 0;
        }
        else
        {
            return 180f;
        }
    }

}
