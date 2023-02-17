﻿using System.Collections.Generic;
using Remagures.Model.Character;
using Remagures.Model.CutscenesSystem;
using Remagures.Model.DialogSystem;
using Remagures.Model.Interactable;
using Remagures.View;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Root
{
    public sealed class TestCutsceneRoot : CompositeRoot
    {
        [Header("Dialogs Stuff")] 
        [SerializeField] private DialogView _dialogView;
        [SerializeField] private DialogTypeWriter _writer;
        [SerializeField] private Button _dialogWindowButton;
        [SerializeField] private Text _continueText;
        
        [Space]
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private UIActivityChanger _uiActivityChanger;
        private ISystemUpdate _systemUpdate;

        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            var actions = new List<ICutsceneAction>
            {
                new StartAction(_uiActivityChanger),
                new TeleportAction(characterMovement.transform, new Vector2(-6.5f, 5)),
                
                new TimerAction(1.5f),
                new MoveAction(characterMovement, new Vector2(0, 5)),
                new MoveAction(characterMovement, new Vector2(0, 4.99f)),
                
                new TimerAction(1.5f),
                new MoveAction(characterMovement, new Vector2(0, 0)),
                
                new TimerAction(0.5f),
                new DialogAction(_writer, _dialogWindowButton, _continueText, "Где я?"),
                new DialogAction(_writer, _dialogWindowButton, _continueText, "Что это за место?"),
                new DialogAction(_writer, _dialogWindowButton, _continueText, "Что происходит вообще?"),
                
                new EndDialogAction(_dialogView),
                new EndAction(_uiActivityChanger)
            };
            
            var cutscene = new Cutscene(actions);
            _systemUpdate.Add(cutscene);
        }

        private void FixedUpdate() 
            => _systemUpdate.UpdateAll();
    }
}