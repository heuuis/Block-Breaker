using UnityEngine;

public class Ball : MonoBehaviour {
    // config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] float collisionSpeedIncreaseFactor = 0.1f;

    // state
    Vector2 paddleToBallVector;
    bool inPlay = false;

    // Cached component references
    Vector2 startingVelocity;
    Vector2 directionOfTravel;
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start() {
        paddleToBallVector = transform.position - paddle1.transform.position;
        startingVelocity = new Vector2(xPush, yPush);
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (!inPlay) {
            StickToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick() {
        if (Input.GetMouseButton(0)) {
            inPlay = true;
            myRigidbody2D.velocity = startingVelocity;
        }
    }

    private void StickToPaddle() {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 velocityTweak = new Vector2(Random.Range(0, randomFactor), Random.Range(0, randomFactor));
        if (inPlay) {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody2D.velocity += velocityTweak;
            PushInDirectionOfTravel(collisionSpeedIncreaseFactor);
        }
    }

    private void PushInDirectionOfTravel(float pushForce) {
        directionOfTravel = myRigidbody2D.velocity.normalized;
        myRigidbody2D.AddForce(pushForce * directionOfTravel, ForceMode2D.Impulse);
    }
}
