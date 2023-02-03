﻿using System;

namespace Remagures.DialogSystem
{
    [Serializable]
    public class DialogChoice : IUsableComponent
    {
        public string ChoiceText { get; }
        public bool IsUsed { get; private set; }

        public void Use()
            => IsUsed = true;

        public DialogChoice(string choiceText)
            => ChoiceText = choiceText ?? throw new ArgumentException("ChoiceText can't be null");
    }
}