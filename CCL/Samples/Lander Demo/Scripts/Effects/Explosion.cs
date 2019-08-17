using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _aux;

    [SerializeField]
    private ParticleSystem _initial;

    public void Explode()
    {
        _initial.Play();
        _aux.Play();
    }
}