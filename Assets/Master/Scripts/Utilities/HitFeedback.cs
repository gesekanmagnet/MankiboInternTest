using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HitFeedback : MonoBehaviour
{
    [SerializeField] private Volume volume;

        private void OnEnable()
        {
            GameManager.Instance.OnHit += Apply;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnHit -= Apply;
        }

        private void Apply()
        {
            StartCoroutine(ApplyHitFeedback());
        }

        private IEnumerator ApplyHitFeedback()
        {
            volume.weight = 1.0f;
            while (volume.weight > 0)
            {
                volume.weight -= .1f;
                yield return new WaitForSeconds(.1f);
            }

            yield break;
        }
}
