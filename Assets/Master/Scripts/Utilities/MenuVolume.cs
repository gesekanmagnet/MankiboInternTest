using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MenuVolume : MonoBehaviour
{
    [SerializeField] private Volume volume;

    private bool increase, decrease;

        private void OnEnable()
        {
            GameManager.Instance.OnGameStart += Apply;
            GameManager.Instance.OnGameOver += Apply;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameStart -= Apply;
            GameManager.Instance.OnGameOver -= Apply;
        }

        private void Apply()
        {
            decrease = true;
        }

        private void Apply(int x)
        {
            increase = true;
        }

        private void Update()
        {
            if(decrease)
            {
                if(volume.weight > 0)
                    volume.weight -= Time.deltaTime;
                else
                    decrease = false;
            }

            if(increase)
            {
                if(volume.weight < 1)
                    volume.weight += Time.deltaTime;
                else
                    increase = false;
            }

            volume.weight = Mathf.Clamp(volume.weight, 0, 1);
        }
}