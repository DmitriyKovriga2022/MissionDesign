using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask hitMask;
    public float ButtonDistance = 10f;
    public float BotDistance = 100f;

    private BaseController _controller;

    void Start()
    {
        
    }

    public void GetController(BaseController controller)
    {
        _controller = controller;
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            var ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, ButtonDistance, hitMask))
            {
                var hitObj = hit.collider.gameObject;
                var button = hitObj.GetComponentInChildren<Button>();

                button?.Press();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            var ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, BotDistance, hitMask))
            {
                var hitObj = hit.collider.gameObject;
                var bot = hitObj.GetComponentInChildren<Bot>();

                if (bot != null)
                {
                    bot.Died();
                    Destroy(hitObj);
                }

            }
        }
    }
}
