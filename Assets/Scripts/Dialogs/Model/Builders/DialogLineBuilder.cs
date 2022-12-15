﻿using System.Collections.Generic;
using System.Linq;
using Remagures.Dialogs.Model.ActionCallbacks;
using Remagures.Dialogs.Model.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Dialogs.Model.Builders
{
    public class DialogLineBuilder : SerializedMonoBehaviour
    {
        [SerializeField] private string _lineText;
        [SerializeField] private SpeakerInfoBuilder _speakerInfoBuilder;
        
        [Space]
        [SerializeField] private List<ChoiceBuilder> _choiceBuilders;
        [SerializeField] private List<IDialogActionCallback> _onLineEndedCallbacks;
        
        public DialogLine BuiltLine { get; private set; }

        public DialogLine Build()
        {
            var builtChoices = _choiceBuilders.Select(builder => builder.BuiltChoice).ToList();
            var result = new DialogLine(_lineText, _speakerInfoBuilder.BuiltInfo, builtChoices);

            foreach (var callback in _onLineEndedCallbacks)
                result.OnLineEndedAction += callback.Callback;

            BuiltLine = result;
            return result;
        }
    }
}