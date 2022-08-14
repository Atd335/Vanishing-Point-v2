using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

namespace cakeslice
{
    public class OutlineAnimation : MonoBehaviour
    {

        OutlineEffect outline;
        float fillAmountInit;

        public float speed;

        // Use this for initialization
        void Start()
        {
            outline = GetComponent<OutlineEffect>();
            fillAmountInit = outline.fillAmount;
        }

        // Update is called once per frame
        void Update()
        {
            outline.fillAmount = ((Mathf.Sin(Time.time*speed)/2)+.5f)*fillAmountInit;
        }
    }
}