﻿using Remagures.Model.QuestSystem.GoalListeners;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ShowPopupGoalListenerFactory : MonoBehaviour
    {
        [SerializeField] private GoalFactory _goalFactory;
        [SerializeField] private QuestFactory _questFactory;
        [SerializeField] private QuestPopupsFactory questPopupsFactory;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();

        private void Awake()
        {
            var listener = new ShowPopupListener(_goalFactory.Create(), _questFactory.Create(), questPopupsFactory.Create());
            _systemUpdate.Add(listener);
        }
    }
}