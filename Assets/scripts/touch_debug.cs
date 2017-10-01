using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch_debug : MonoBehaviour {
    private Vector2 currPos = new Vector2 ();
    private Vector2 prevPos = new Vector2 ();

    float maxDelay = 0.3f;
    float delayMultiplier = 2;
    private float delay = 0.0f;

    void Start () {
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton (0)) {
            if (delay > 0.0f) {
                // Debug.Log (delay);
                delay -= Time.deltaTime * delayMultiplier;
                return;
            }

            delay = maxDelay;
            Vector2 mousePos = new Vector2 ();
            Vector2 screenPos = new Vector2 ();
            Vector2 centerPos = new Vector2 ();
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Camera.main.pixelHeight - Input.mousePosition.y;
            screenPos = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            // Debug.Log ("screen pos: " + screenPos);

            prevPos = currPos;
            currPos = screenPos;
            // if (prevPos == Vector2.zero) {
            //     prevPos = currPos;
            //     return;
            // }

            var v1 = currPos - centerPos;
            var v2 = prevPos - centerPos;
            // Debug.Log ("v1: [" + v1 + "] && v2: [" + v2 + "]");

            var sign = Mathf.Sign (v1.x * v2.y - v1.y * v2.x);
            Debug.Log ("dot product: " + Vector2.Dot (v1, v2) + " | sign: " + sign);
        }

        if (Input.GetMouseButtonUp (0)) {
            delay = 0.0f;
        }
    }
}