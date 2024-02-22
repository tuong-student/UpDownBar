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
        public Action OnPausePressed;
        public bool IsEndDay;

        void OnEnable()
        {
            if(TimeManager.Instance)
            {
                TimeManager.Instance.OnTimeUp += OnTimeUpHandler;
            }
            if(UIManager.Instance)
            {
                UIManager.Instance.OnNextDayPressed += OnNextDayPressHandler;
            }
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
            TimeManager.TimeScale = 1;
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
