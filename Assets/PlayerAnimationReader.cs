using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationReader : MonoBehaviour
{
    private Animator _playerAnimator;

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        CollideInspector.CollideWithTree += Grap;
    }

    void OnDisable()
    {
        CollideInspector.CollideWithTree -= Grap;
    }

    public void Grap()
    {
        StartCoroutine(GrapAndDisable());
    }

    private IEnumerator GrapAndDisable()
    {
        _playerAnimator.enabled = true;
        Debug.Log("Grap");
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Grap");
        _playerAnimator.enabled = false;
    }

}
