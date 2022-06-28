using UnityEngine;

public class PatrollingLog : EnemyWithTarget
{
    [field: SerializeField, Header("Patrolling Stuff")] public Transform[] Path { get; private set; }

    [HideInInspector] public Transform CurrentGoal;
    [HideInInspector] public int CurrentPoint = 0;

    public override void Start()
    {
        base.Start();
        EnemyAnimations.Anim.SetBool("wakeUp", true);
    }

    public void ChangeGoal()
    {
        if (CurrentPoint == Path.Length - 1)
            CurrentPoint = 0;
        else
            CurrentPoint++;

        CurrentGoal = Path[CurrentPoint];
    }
    
    public override void Move(Vector3 moveTo)
    {
        base.Move(moveTo);

        if (moveTo == Path[CurrentPoint].position && Pathfinding.Path.Count == 0)
            ChangeGoal();
    }
}
