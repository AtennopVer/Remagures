using System.Globalization;
using Remagures.Model.InventorySystem;
using Remagures.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] protected GameObject _inventorySlot;
        [SerializeField] protected GameObject _inventoryPanel;

        [Header("Objects")]
        [SerializeField] private Text _noItemsText;
        [field: SerializeField] public GameObject UseButton { get; private set; }
        [SerializeField] private Text _nameText;
        [field: SerializeField] protected Text DescriptionText { get; private set; }
        [SerializeField] private Text _sharpsCountText;
        
        [Space]
        [SerializeField] private Player _player;
        [SerializeField] private UniqueSetup _uniqueSetup;

        [field: SerializeField, Header("Values")] protected Inventory Inventory { get; private set; }
        [field: SerializeField] protected Item VoidItem { get; private set; }
        [SerializeField] private FloatValue _sharps;

        protected IReadOnlyCell CurrentCell { get; private set; }

        public void Start()
        {
            SetText(new Cell(VoidItem));
            gameObject.SetActive(false);
        }

        public void OnEnable()
        {
            ClearInventory();
            MakeInventorySlots();
            SetText(new Cell(VoidItem));
        
            if(_sharpsCountText)
                _sharpsCountText.text = _sharps.Value.ToString(CultureInfo.InvariantCulture); 
        }

        private void MakeInventorySlots()
        {
            if (_noItemsText != null)
                _noItemsText.gameObject.SetActive(Inventory.Cells.Count == 0);

            foreach (var cell in Inventory.Cells)
            {
                if (cell.Item == null || cell.ItemsCount <= 0) continue;
            
                var slotObject = Instantiate(_inventorySlot, _inventoryPanel.transform.position, Quaternion.identity, _inventoryPanel.transform);
                if (slotObject.TryGetComponent(out CellView newSlot))
                    newSlot.Display(cell, this);
            }
        }

        public void SetText(IReadOnlyCell cell)
        {
            CurrentCell = cell;
            _nameText.text = CurrentCell.Item.Name;
            DescriptionText.text = CurrentCell.Item.Description;
            SetButton();
        }

        protected virtual void SetButton()
        {
            var buttonRect = DescriptionText.gameObject.GetComponent<RectTransform>();
            UseButton.SetActive(CurrentCell.Item is IUsableItem);

            if (CurrentCell.Item is IUsableItem)
            {
                buttonRect.anchoredPosition = new Vector3(-81.58096f, buttonRect.anchoredPosition.y, 0);
                buttonRect.sizeDelta = new Vector2(1941.4f, buttonRect.sizeDelta.y);
            }
            else
            {
                buttonRect.anchoredPosition = new Vector3(6.96f, buttonRect.anchoredPosition.y, 0);
                buttonRect.sizeDelta = new Vector2(2806.157f, buttonRect.sizeDelta.y);
            }    
        }

        private void ClearInventory()
        {
            for (var i = 0; i < _inventoryPanel.transform.childCount; i++)
                Destroy(_inventoryPanel.transform.GetChild(i).gameObject);
        }

        public void UseItem()
        {
            (CurrentCell.Item as IUsableItem)?.Use();
            if (Inventory.GetCell(CurrentCell.Item).ItemCountGreaterOrEqualValue(1))
                Inventory.Decrease(new Cell(CurrentCell.Item));

            if (CurrentCell.ItemsCount <= 0)
            {
                Inventory.Remove(new Cell(CurrentCell.Item, CurrentCell.ItemsCount));
                SetText(new Cell(VoidItem));
            }
        
            ClearInventory();
            MakeInventorySlots();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            SetupPlayer();
        }

        protected virtual void SetupPlayer()
        {
            UnityEngine.Time.timeScale = 1;
            _uniqueSetup.SetupUnique(_player);
        }
    }
}