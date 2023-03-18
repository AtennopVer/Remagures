﻿using System;

namespace Remagures.Model.DialogSystem
{
    public sealed class DialogSwitcher
    {
        private readonly string _newDialogName;
        private readonly IDialogs _dialogs;

        public DialogSwitcher(IDialogs dialogs, string newDialogName)
        {
            _newDialogName = newDialogName ?? throw new ArgumentNullException(nameof(newDialogName));
            _dialogs = dialogs ?? throw new ArgumentNullException(nameof(dialogs));
        }

        public void Switch() 
            => _dialogs.SwitchToDialog(_newDialogName);
    }
}