using Remagures.Model.AI.StateMachine;
using UnityEngine;

namespace Remagures.Model.AI.Enemies.TurretEnemies
{
    public sealed class WhilePlayerNotInRange : IState
    {
        private readonly TurretEnemy _turretEnemy;
        private readonly int WAKE_UP_ANIMATOR_NAME = Animator.StringToHash("wakeUp");
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public WhilePlayerNotInRange(TurretEnemy turretEnemy)
            => _turretEnemy = turretEnemy;

        public void OnEnter()
        {
            var enemyAnimator = _turretEnemy.Animations.Animator;
            enemyAnimator.SetBool(WAKE_UP_ANIMATOR_NAME, false);
            enemyAnimator.SetBool(IS_STAYING_ANIMATOR_NAME, true);
        }

        public void OnExit() { }
        public void Update() { }
    }
}