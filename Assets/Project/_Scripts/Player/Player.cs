using System;
using System.Collections;
using System.Collections.Generic;
using NOOD;
using NOOD.Sound;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviorInstance<Player>
    {
        public static Action<int> OnPlayerChangePosition;
        public static Action OnPlayerChangePositionSuccess;
        public static Action OnPlayerPressDeliverBeer;
        private int _index = 0;
        private Vector2 _input;
        private bool _isServePressed;

        private void Update()
        {
            Move();
        }
        
        private void Move()
        {
            GetInput();
            if(_isServePressed)
            {
                BeerServeManager.Instance.ServeBeer();
                OnPlayerPressDeliverBeer?.Invoke();
                SoundManager.PlaySound(SoundEnum.ServeBeer);
            }

            CalculateStandIndex();

            Vector3 standPosition = new Vector3(this.transform.position.x, 0, TableManager.Instance.GetPlayerTablePosition().z);
            this.transform.position = standPosition;

        }

        private int CalculateStandIndex()
        {
            int oldIndex = _index;
            if(_input.y > 0)
            {
                // Move up
                _index++;
            }
            if(_input.y < 0)
            {
                // Move down
                _index--;
            }
            _index = Mathf.Clamp(_index, 0, TableManager.Instance.GetTableList().Count - 1);
            OnPlayerChangePosition?.Invoke(_index);
            if(oldIndex != _index)
            {
                OnPlayerChangePositionSuccess?.Invoke();
            }
            return _index;
        }

        private void GetInput()
        {
            _isServePressed = false;
            float y = 0;
            if(Input.GetKeyDown(KeyCode.W))
            {
                y = 1;
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                y = -1;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _isServePressed = true;
            }

            _input = new Vector2(0, y);
        }
    }
}
