using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickController : MonoBehaviour
{
    public enum TrickState
    {
        Normal,
        InTrick
    }

    private enum Inputs
    {
        Neutral,
        Up,
        Down,
        Left,
        Right
    }

    public enum Trick
    {
        None,
        Kickflip,
        Heelflip,
        Treflip,
        Laserflip
    }

    [SerializeField] private CarStats carProfile;

    [Header("Car State")]
    [SerializeField] private Animator carAnimator;
    [SerializeField] private BoxCollider carHitbox;
    private TrickState currentState;
    private bool isGrounded, isBufferActive;
    private Vector2 moveInput;

    private Queue<Inputs> inputQueue = new Queue<Inputs>();

    private Rigidbody carRB;

    public void SetMoveInput(Vector2 _moveInput) => moveInput = _moveInput;

    void Start()
    {
        carRB = GetComponent<Rigidbody>();
        //carHitbox.enabled = false;
        currentState = TrickState.Normal;

        inputQueue = new Queue<Inputs>();
        Debug.Log("start");
    }

    // Update is called once per frame
    public void UpdateTrickController(bool _isGrounded)
    {
        isGrounded = _isGrounded;

        if (isBufferActive)
        {
            UpdateInputBuffer();
        }
    }

    public void UpdateInputBuffer()
    {
        float inputAngle = Vector2.SignedAngle(Vector2.right, moveInput);

        if (moveInput == Vector2.zero)
            inputQueue.Enqueue(Inputs.Neutral);

        else if (inputAngle > -45.0f && inputAngle < 45.0f)
            inputQueue.Enqueue(Inputs.Right);

        else if (inputAngle >= 45.0f && inputAngle <= 135.0f)
            inputQueue.Enqueue(Inputs.Up);

        else if (inputAngle >= -135.0f && inputAngle <= -45.0f)
            inputQueue.Enqueue(Inputs.Down);

        else if (inputAngle > 135.0f || inputAngle < -135.0f)
            inputQueue.Enqueue(Inputs.Left);
    }

    public void JumpDown()
    {
        if (isGrounded && currentState != TrickState.InTrick)
        {
            currentState = TrickState.InTrick;
            StartCoroutine(BufferInputs());
        }
    }

    private void EvaluateBuffer()
    {
        bool moveInputted = false;

        for (int i = 0; i < inputQueue.Count; i++)
        {
            Inputs currentInput = inputQueue.Dequeue();

            switch (currentInput) // just using first input found for now
            {
                case Inputs.Neutral:
                    break;
                case Inputs.Right:
                    i = inputQueue.Count;
                    moveInputted = true;
                    StartCoroutine(DoTrick("CarHeelflip"));
                    break;
                case Inputs.Left:
                    i = inputQueue.Count;
                    moveInputted = true;
                    StartCoroutine(DoTrick("CarKickflip"));
                    break;
                case Inputs.Up:
                    i = inputQueue.Count;
                    moveInputted = true;
                    StartCoroutine(DoTrick("CarLaserflip"));
                    break;
                case Inputs.Down:
                    i = inputQueue.Count;
                    moveInputted = true;
                    StartCoroutine(DoTrick("CarTreflip"));
                    break;
            }
        }

        // if nothing inputted jump
        if (!moveInputted)
        {
            carRB.AddForce(transform.up * carProfile.jumpForce, ForceMode.Impulse);
            currentState = TrickState.Normal;
        }
    }

    private IEnumerator BufferInputs()
    {
        isBufferActive = true;

        yield return new WaitForSeconds(carProfile.inputBufferLength);

        isBufferActive = false;
        EvaluateBuffer();
        inputQueue.Clear();
    }

    private IEnumerator DoTrick(string trickName)
    {
        currentState = TrickState.InTrick;
        Debug.Log(trickName);

        // Pop car up
        carRB.AddForce(transform.up * carProfile.jumpForce, ForceMode.Impulse);

        // Add trick specific forces
        //if (trickName == "CarKickflip")
        //    carRB.AddForce(-transform.right * 7500, ForceMode.Impulse);
        //if (trickName == "CarHeelflip")
        //    carRB.AddForce(transform.right * 7500, ForceMode.Impulse);

        // Perform trick
        carAnimator.Play(trickName);

        yield return new WaitForSeconds(0.1f);

        //carHitbox.enabled = true;

        yield return new WaitForSeconds(1.0f); //Time adds up to 1.1 which is animation length

        currentState = TrickState.Normal;
    }
}
