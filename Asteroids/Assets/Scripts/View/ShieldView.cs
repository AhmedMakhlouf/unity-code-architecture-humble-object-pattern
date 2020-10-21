using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldView : MonoBehaviour, IShieldView
{
    
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Collider collider;
    public event EventHandler<CollisionEventArgs> OnCollision;
    public bool Shield
    {
        set
        {
            shield = value;
            if (shield)
            {
                collider.enabled = false;
                if (gameObject.activeSelf)
                    StartCoroutine("Blink");
            }
            else
            {
                if (gameObject.activeSelf)
                    StopCoroutine("Blink");
                renderer.enabled = true;
                collider.enabled = true;
            }
        }
        get
        {
            return shield;
        }
    }
    private bool shield;

    private void OnTriggerEnter(Collider other)
    {
        if (Shield)
            return;

        IShieldView shield = other.GetComponent<IShieldView>();
        if (shield != null && shield.Shield)
            return;

        OnCollision(this, new CollisionEventArgs(other, other.GetComponent<IView>().Type));
    }

    IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);
            renderer.enabled = !renderer.enabled;
        }
    }
}
