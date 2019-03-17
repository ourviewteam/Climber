using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSDeleteObject : MonoBehaviour
{
    public ParticleSystem BloodPS;
    void Start()
    {
        BloodPS = this.gameObject.GetComponent<ParticleSystem>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (BloodPS.isStopped) Destroy(this.gameObject);
    }
}
