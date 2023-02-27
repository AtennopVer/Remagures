﻿using System;
using System.Linq;
using Remagures.Root;

namespace Remagures.Model.InventorySystem
{
    public class RunesSelector : IInventoryCellSelector<IRuneItem>, ILateUpdatable
    {
        public bool HasSelected { get; private set; }
        public IReadOnlyCell<IRuneItem> SelectedCell { get; private set;}

        private readonly IInventory<IRuneItem> _runesInventory;

        public RunesSelector(IInventory<IRuneItem> runesInventory)
            => _runesInventory = runesInventory ?? throw new ArgumentNullException(nameof(runesInventory));

        public void LateUpdate()
            => HasSelected = false;
        
        public void Select(IRuneItem item)
        {
            if (_runesInventory.Cells.All(runeCell => runeCell.Item.Rune != item.Rune))
                throw new ArgumentException("This rune doesn't exist");
            
            foreach (var runeCell in _runesInventory.Cells)
                runeCell.Item.Rune.Deactivate();
            
            item.Rune.Activate();
            HasSelected = true;
            SelectedCell = _runesInventory.Cells.ToList().Find(cell => cell.Item.Rune == item.Rune);
        }

        public void UnSelect()
        {
            SelectedCell = null;
            foreach (var runeCell in _runesInventory.Cells)
                runeCell.Item.Rune.Deactivate();
        }
    }
}