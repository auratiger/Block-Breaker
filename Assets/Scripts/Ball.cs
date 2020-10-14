using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] private Paddle paddle1;
    [SerializeField] private float xPush = 0f;
    [SerializeField] private float yPush = 15f;
    [SerializeField] private AudioClip[] ballSounds;
    [SerializeField] private float randomFactor = 0.1f;

    [SerializeField] private float xOffset = 0;
    [SerializeField] private float yOffset = 0;

    // state
    private Vector2 _paddleToBallVector;
    private bool _hasStarted = false;
    
    // Cached component references;
    private AudioSource _myAudioSource;

    private Rigidbody2D _myRigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _paddleToBallVector = transform.position - paddle1.transform.position;
        _myAudioSource = GetComponent<AudioSource>();
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasStarted){
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hasStarted = true;
            _myRigidbody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        var position = paddle1.transform.position;
        Vector2 paddlePos = new Vector2(position.x + xOffset, position.y + yOffset);
        transform.position = paddlePos + _paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var velocityTweak = new Vector2(
            Random.Range(0f, randomFactor), 
            Random.Range(0f, randomFactor));
        
        AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
        _myAudioSource.PlayOneShot(clip);
        _myRigidbody2D.velocity += velocityTweak;
    }
}
