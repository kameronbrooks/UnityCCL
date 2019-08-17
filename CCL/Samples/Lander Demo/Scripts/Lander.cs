using UnityEngine;
using UnityEngine.Events;

public class Lander : MonoBehaviour
{
    private Rigidbody2D _body;

    [SerializeField]
    private float _crashTolerance = 7;

    [SerializeField]
    private Explosion _explosion;

    private UnityEvent _onDestroy;

    private void Explode()
    {
        _explosion.gameObject.transform.SetParent(null, true);
        _explosion.Explode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > _crashTolerance)
        {
            Explode();
            _onDestroy?.Invoke();
            gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}