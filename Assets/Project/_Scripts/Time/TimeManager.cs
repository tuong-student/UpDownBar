using System;
using NOOD;
using NOOD.Sound;
using UnityEngine;

namespace Game
{
    public class TimeManager : MonoBehaviorInstance<TimeManager>
    {
        #region Events
        public Action OnTimeWarning;
        public Action OnTimeUp;
        public static Action OnTimePause;
        public static Action OnTimeResume;
        #endregion

        #region Property
        public static float DeltaTime => Time.deltaTime * TimeScale;
        public static float UnScaledDeltaTime => Time.unscaledDeltaTime;
        private static float s_timeScale = 1;
        public static float TimeScale { get => s_timeScale; set 
            {
                if (value == 0)
                    OnTimePause?.Invoke();
                else
                    OnTimeResume?.Invoke();
                s_timeScale = value;
            } 
        }
        #endregion

        #region SerializeField
        [SerializeField] private float _timeMultipler = 1;
        [SerializeField] private float _hourInLevel = 12;
        #endregion

        #region Private
        private float _hour;
        private float _minute;
        private float _day = 1;
        private bool _isWarned, _isTimeUp;
        #endregion

        #region Unity functions
        void Awake()
        {
            TimeScale = 1;
            GameplayManager.Instance.OnNextDay += ResetTimeScale;
            GameplayManager.Instance.OnNextDay += ResetTime;
        }
        void OnEnable()
        {
            UIManager.Instance.OnNextDayPressed += NextDay;
        }
        private void Update()
        {
            _minute += DeltaTime * _timeMultipler * TimeScale;  
            if(_minute >= 59)
            {
                _hour++;
                _minute = 0;
            }
            if(_hour == _hourInLevel)
            {
                // Warning
                if(_isWarned == false)
                    Warning();
            }
            if(_hour == _hourInLevel + 1 && GameplayManager.Instance.IsEndDay == false)
            {
                // Stop level
                if(_isTimeUp == false)
                {
                    OnTimeUp?.Invoke();
                    _isTimeUp = true;
                    SoundManager.PlaySound(SoundEnum.EndLevel);
                }
            }
        }
        void OnDisable()
        {
            GameplayManager.Instance.OnNextDay -= ResetTimeScale;
            GameplayManager.Instance.OnNextDay -= ResetTime;
            UIManager.Instance.OnNextDayPressed -= NextDay;
        }
        #endregion

        private void ResetTimeScale()
        {
            TimeScale = 1;
        }
        private void ResetTime()
        {
            _hour = 0;
            _minute = 0;
            _isTimeUp = false;
            _isWarned = false;
        }
        private void NextDay()
        {
            _day += 1;
        }
        public float GetCurrentDay()
        {
            return _day;
        }
        private void Warning()
        {
            OnTimeWarning?.Invoke();
            SoundManager.PlaySound(SoundEnum.Timer);
            _isWarned = true;
        }

        public float GetHour() => _hour;
        public float GetMinute() => _minute;
    }

}
