using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{

    ARSessionOrigin aRSessionOrigin;

    public Slider scaleSlider;

    private void Awake()
    {
        aRSessionOrigin = GetComponent<ARSessionOrigin>();
    }

    // Start is called before the first frame update
    void Start()
    {
        scaleSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }


    public void OnSliderValueChanged(float value)
    {
        if(scaleSlider != null)
        {
            aRSessionOrigin.transform.localScale = Vector3.one / value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
