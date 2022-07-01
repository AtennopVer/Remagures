using UnityEngine;

public enum InteractingState
{
    None,
    Ready,
    Interact
}

public class PlayerInteracting : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private DialogView _dialogManager;

    [SerializeField] private Signal _detectInteractSignal;

    [SerializeField] private SpriteRenderer _receivedItemSprite;
    [SerializeField] private GameObject _dialogWindow;

    public bool CanShowContextClue { get; private set; } = true;
    public InteractingState CurrentState { get; private set; }
    public Interactable CurrentInteractable { get; private set; }

    private const string RECEIVING_ANIMATOR_NAME = "receiving";

    public void RaiseItem()
    {
        if (_player.CurrentState != PlayerState.Interact && _player.CurrentItem.Value != null)
        {
            _player.PlayerAnimations.ChangeAnim(RECEIVING_ANIMATOR_NAME, true);
            _player.ChangeState(PlayerState.Interact);
            _receivedItemSprite.sprite = _player.CurrentItem.Value.ItemData.ItemSprite;
        }
    }

    public void Interact()
    {
        CurrentState = InteractingState.Interact;
        CurrentInteractable?.Interact();
        CanShowContextClue = false;
        CurrentInteractable.Context.Raise();
    }

    public void DialogOnTaped()
    {
        if (((CurrentInteractable is InteractableWithTextDisplay && 
        (CurrentInteractable as InteractableWithTextDisplay).CanCloseText) || CurrentInteractable is NPC) && 
        _dialogManager.IsDialogEnded && 
        _dialogManager.CanContinue)
        {
            CurrentInteractable = null;
            CanShowContextClue = true;
            CurrentState = InteractingState.None;
            _detectInteractSignal.Raise();

            _player.ChangeState(PlayerState.Idle);
            _receivedItemSprite.sprite = null;
            _player.CurrentItem.Value = null;
            
            _player.PlayerAnimations.ChangeAnim(RECEIVING_ANIMATOR_NAME, false);
            _dialogWindow.SetActive(false);
        }
    }

    public void SetCurrentInteractable(Interactable interactable)
    {
        if (interactable.PlayerInRange && CurrentInteractable == null)
            CurrentInteractable = interactable;
    }

    public void SetCurrentState(Interactable interactable)
    {
        if (CurrentState != InteractingState.Interact && (CurrentInteractable == null || CurrentInteractable == interactable))
        {
            if (interactable.PlayerInRange)
                CurrentState = InteractingState.Ready;
            else
                CurrentState = InteractingState.None;
        }
    }

    public void ResetCurrentState(Interactable interactable)
    {
        if (!interactable.PlayerInRange && CurrentState == InteractingState.Ready)
            CurrentInteractable = null;
    }
}
