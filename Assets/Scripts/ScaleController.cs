using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ScaleController : MonoBehaviour
{

    ARSessionOrigin aRSessionOrigin;

    // Start is called before the first frame update
    void Start()
    {
        aRSessionOrigin = GetComponent<ARSessionOrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
