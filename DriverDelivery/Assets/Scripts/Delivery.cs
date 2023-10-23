using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    
    
    private string PACKAGE_TAG = "Package";
    private string CUSTOMER_TAG = "Customer";
    private bool hasPackage;
    private SpriteRenderer spriteRenderer;
    
    
    [SerializeField] private float DelayBeforeThePackageDisappears = 0.5f;
    [SerializeField] private Color32 hasPackageColor = new Color32(1,1,1,1);
    [SerializeField] private Color32 noPackageColor = new Color32(1,1,1,1);

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == PACKAGE_TAG && !hasPackage)
        {
            Debug.Log("Marchandise récup");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(trigger.GameObject(), DelayBeforeThePackageDisappears);
        }
        else if (trigger.tag == CUSTOMER_TAG && hasPackage)
        {
            Debug.Log("C'est livré");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }      
    }
}
