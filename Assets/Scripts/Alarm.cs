using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Alarm : MonoBehaviour
{
    [HideInInspector] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _audioSource;

    private float _targetVolume;
    private float _fadeVolumeTime = 0.001f;
    private Color _startColor;
    private Coroutine _runCoroutine;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _audioSource.volume += Time.deltaTime * _fadeVolumeTime;
            _targetVolume = 1;
            Work(_targetVolume);
            _spriteRenderer.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _audioSource.volume -= Time.deltaTime * _fadeVolumeTime;
            _targetVolume = 0;
            Work(_targetVolume);
            _spriteRenderer.color = _startColor;
        }
    }

    private void Work(float targetVolume)
    {
        if (_runCoroutine != null)
        {
            StopCoroutine(_runCoroutine);
        }

        _runCoroutine = StartCoroutine(ChangeVolumeSmoothly(targetVolume));
    }

    private IEnumerator ChangeVolumeSmoothly(float targetVolume)
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        while (_audioSource.volume < targetVolume || _audioSource.volume > targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume <= 0 && _audioSource.isPlaying == true)
        {
            _audioSource.Stop();
        }
    }
}
