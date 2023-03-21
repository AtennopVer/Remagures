﻿using System.Collections.Generic;
using System.Linq;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class DialogsListFactory : SerializedMonoBehaviour
    {
        [SerializeField] private int _idOfStartDialog;
        [SerializeField] private string _characterName;

        [Space] [SerializeField] private List<SpeakerInfoFactory> _speakerInfoFactories;
        [SerializeField] private List<DialogChoiceFactory> _choiceFactories;

        [Space] [SerializeField] private List<DialogLineFactory> _dialogLineFactories;
        [SerializeField] private List<DialogFactory> _dialogFactories;

        private Dialogs _builtDialog;

        public IDialogs Create()
        {
            if (_builtDialog != null)
                return _builtDialog;
            
            _speakerInfoFactories.ForEach(factory => factory.Build()); //TODO check can i delete it and just create list
            _choiceFactories.ForEach(factory => factory.Create());
            _dialogLineFactories.ForEach(factory => factory.Create());
            _dialogFactories.ForEach(factory => factory.Create());

            _builtDialog = new Dialogs(_dialogFactories.Select(builder => builder.Create()).ToArray(), _characterName);
            
            if (_builtDialog.CurrentDialog == null)
                _builtDialog.SwitchCurrent(_dialogFactories[_idOfStartDialog].Create().Name);

            return _builtDialog;
        }
    }
}