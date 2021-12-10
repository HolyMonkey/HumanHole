using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    [SerializeField] private Floating _floating;
    [SerializeField] private float _jointY = 0.5f;
    [SerializeField] private ParticleSystem _splashEffectPrefab;
    [SerializeField] private ParticleSystem _tinySplashEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BodyPart part))
        {
            Vector3 position = new Vector3(part.transform.position.x, 0, part.transform.position.z);
            if (part is Spine)
            {
                _floating.transform.position = position + Vector3.up * _jointY;
                _floating.gameObject.SetActive(true);
                Instantiate(_splashEffectPrefab, position, Quaternion.identity);
            }
            else
            {
                Instantiate(_tinySplashEffectPrefab, position, Quaternion.identity);
            }
        }

        if (other.TryGetComponent(out Hand hand))
            hand.PrepareForSwim();
    }
}
