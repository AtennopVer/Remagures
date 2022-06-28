using UnityEngine;

public class RandomMovableBoundedNPC : MovableNPC
{
    [Header("Times")]
    [SerializeField] private float _minMoveTime;
    [SerializeField] private float _minWaitTime;
    [SerializeField] private float _maxMoveTime;
    [SerializeField] private float _maxWaitTime;

    [Header("Bounds")]
    [SerializeField] private Collider2D _bounds;

    private float _moveTimeSeconds;
    private float _waitTimeSeconds;
    private Vector3 _directionVector;
    
    public override void Start()
    {
        base.Start();
        _waitTimeSeconds = Random.Range(_minWaitTime, _maxWaitTime);
        _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
        ChangeDirection();
    }

    public virtual void FixedUpdate()
    {
        if (!(CurrentState == NPCState.Talk || CurrentState == NPCState.Wait))
        {
            if (CurrentState == NPCState.Walk)
            {
                Animations.animator.SetBool("isStaying", false);
                _moveTimeSeconds -= Time.deltaTime;
                if (_moveTimeSeconds <= 0)
                {
                    _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
                    ChangeState(NPCState.Stay);
                }
                else if (!PlayerInRange)
                    Move();
            }
            else
            {
                Animations.animator.SetBool("isStaying", true);
                _waitTimeSeconds -= Time.deltaTime;
                if (_waitTimeSeconds <= 0)
                {
                    ChooseDifferentDirection();
                    _waitTimeSeconds = Random.Range(_minWaitTime, _maxWaitTime);
                    ChangeState(NPCState.Walk);
                }
            }
        }
        else
        {
            ChangeDirection();
            Vector3 temp = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
            _directionVector = temp - transform.position;
            Animations.UpdateAnim(_directionVector);
        }
    }

    protected override void Move()
    {
        Vector3 temp = transform.position + Speed * Time.deltaTime * _directionVector;
        if (_bounds.bounds.Contains(temp) && Physics2D.Raycast(transform.position, _directionVector, 2f))
            MyRigidbody.MovePosition(temp);
        else
            ChangeDirection();
    }

    protected void ChooseDifferentDirection()
    {
        Vector3 temp = _directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == _directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    protected void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                _directionVector = Vector3.right;
                break;
            case 1:
                _directionVector = Vector3.left;
                break;
            case 2:
                _directionVector = Vector3.up;
                break;
            case 3:
                _directionVector = Vector3.down;
                break;
        }
        Animations.UpdateAnim(_directionVector);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        ChooseDifferentDirection();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && CurrentState != NPCState.Talk)
        {
            base.OnTriggerEnter2D(collision);
            ChangeDirection();
            Vector3 temp = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
            _directionVector = temp - transform.position;
            Animations.UpdateAnim(_directionVector);
        }
    }
}
