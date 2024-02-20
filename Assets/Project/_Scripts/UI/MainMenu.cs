using Game.Extension;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyTransition;
using DG.Tweening;
using NOOD;

namespace Game
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Main menu")]
        [SerializeField] private CustomButton _playBtn, _howToPlay, _exit;
        [SerializeField] private Image _chooseImage;
        [SerializeField] private TransitionSettings _transitionSetting;

        [Header("How to play")]
        [SerializeField] private CanvasGroup _howToPlayPanel;
        [SerializeField] private CustomButton _closeBtn;

        void Start()
        {
            _playBtn.OnHover += (() =>
            {
                _chooseImage.transform.position = _chooseImage.transform.position.ChangeY(_playBtn.transform.position.y); 
            });
            _howToPlay.OnHover += (() =>
            {
                _chooseImage.transform.position =_chooseImage.transform.position.ChangeY(_howToPlay.transform.position.y); 
            });
            _exit.OnHover += (() =>
            {
                _chooseImage.transform.position =_chooseImage.transform.position.ChangeY(_exit.transform.position.y); 
            });

            _playBtn.OnClick += LoadGameScene;
            _howToPlay.OnClick += OpenHowToPlay;
            _closeBtn.OnClick += CloseHowToPlay;
            CloseHowToPlay();
        }        
        void OnDisable()
        {
            _howToPlayPanel.transform.DOKill();
        }

        private void LoadGameScene()
        {
            TransitionManager.Instance().Transition("GameScene", _transitionSetting, 0.3f);
        }

        private void OpenHowToPlay()
        {
            _howToPlayPanel.alpha = 1;
            _howToPlayPanel.transform.localScale = Vector3.zero;
            _howToPlayPanel.transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBounce);
        }
        private void CloseHowToPlay()
        {
            _howToPlayPanel.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutExpo);
            NoodyCustomCode.FadeCanvasGroup(_howToPlayPanel, _howToPlayPanel, 0, 220f);
        }
    }
}
