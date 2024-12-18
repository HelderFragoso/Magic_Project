using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _mousePos;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Aiming();
    }

    private void Aiming()
    {
        _mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = _mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
