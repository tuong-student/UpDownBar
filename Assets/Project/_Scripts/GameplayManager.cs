using System;
using NOOD;
using NOOD.Sound;
using UnityEngine;

namespace Game
{
    public class GameplayManager : MonoBehaviorInstance<GameplayManager>
    {
        public Action OnEndDay;
        public Action OnNextDay;
        public bool IsEndDay;

        void OnEnable()
        {
            TimeManager.Instance.OnTimeUp += OnTimeUpHandler;
            UIManager.Instance.OnNextDayPressed += OnNextDayPressHandler;
            SoundManager.InitSoundManager();
        }
        void OnDisable()
        {
            NoodyCustomCode.UnSubscribeAllEvent<TimeManager>(this);
            NoodyCustomCode.UnSubscribeAllEvent<UIManager>(this);
        }
        void Start()
        {
            SoundManager.PlayMusic(MusicEnum.PianoBGMusic);
            SoundManager.PlayMusic(MusicEnum.CrowdBGSound);
        }

        #region Event functions
        private void OnTimeUpHandler()
        {
            IsEndDay = true;
            OnEndDay?.Invoke();
        }
        private void OnNextDayPressHandler()
        {
            IsEndDay = false;
            OnNextDay?.Invoke();
        }
        #endregion
    }

}
