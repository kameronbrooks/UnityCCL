using UnityEngine;
using UnityEngine.Events;

public class Thruster : MonoBehaviour
{
    [SerializeField]
    private bool _active = true;

    [SerializeField]
    private Rigidbody2D _body;

    [SerializeField]
    private float _efficiency = 1;

    [SerializeField]
    private float _fuel = 10;

    [SerializeField]
    private float _maxFuel = 10;

    [SerializeField]
    private float _maxThrust = 1;

    [SerializeField]
    private ThrusterEvent _onFixedUpdate;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float _throttle;

    public float fuel
    {
        get
        {
            return _fuel;
        }
    }

    public float maxFuel
    {
        get
        {
            return _maxFuel;
        }
    }

    public void SetThrottle(float throttle)
    {
        _throttle = Mathf.Clamp01(throttle);
    }

    private void FixedUpdate()
    {
        if (_active && _fuel > 0.0f && _throttle > 0)
        {
            _particleSystem.gameObject.SetActive(true);
            float adjustedThrust = _throttle * _maxThrust * Time.fixedDeltaTime;
            _body.AddForce(-transform.up * adjustedThrust);
            _fuel -= _throttle / _efficiency * Time.fixedDeltaTime;
            _onFixedUpdate?.Invoke(this);
        }
        else
        {
            _particleSystem.gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class ThrusterEvent : UnityEvent<Thruster> { }
}