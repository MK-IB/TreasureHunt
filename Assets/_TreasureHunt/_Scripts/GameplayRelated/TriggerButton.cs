using _TreasureHunt._Scripts.ControllerRelated;
using DG.Tweening;
using UnityEngine;

namespace _TreasureHunt._Scripts.GameplayRelated
{
    public class TriggerButton : MonoBehaviour
    {

        [SerializeField] private Transform buttonPart;
        [SerializeField] private Transform door;

        private void OnTriggerEnter(Collider other)
        {
            Transform colliderTransform = other.transform;
            PlayerController playerController = colliderTransform.GetComponent<PlayerController>(); 
            if (playerController)
            {
                if(playerController.hasKey)
                    OpenDoor();
                else
                {
                    Transform noKeyWarningPanelTransform = UIController.instance.noKeyWarningPanel.transform;
                    noKeyWarningPanelTransform.gameObject.SetActive(true);
                    noKeyWarningPanelTransform.DOScale(Vector3.one * 0.75f, 0.35f).From();
                }
            }
        }

        public void OpenDoor()
        {
            door.DOLocalMoveY(-1, 1).SetRelative(true);
            Effect();
        }

        void Effect()
        {
            Sequence seq = DOTween.Sequence();
            float origPosY = buttonPart.transform.localPosition.y;
            seq.Append(buttonPart.DOLocalMoveY(origPosY - .55f, 0.5f));
            seq.Append(buttonPart.DOLocalMoveY(origPosY, 0.5f));
        }
    }
}