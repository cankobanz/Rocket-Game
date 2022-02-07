using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    const float tau = Mathf.PI * 2;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //Periodu 2 seçersen 2 saniyede bir cycle gelir 10 seçersen 10 sn de bir cycle gelir
        float rawSinWave = Mathf.Sin(cycles * tau);//2pi*deðiþen bir sayý
        movementFactor = (rawSinWave + 1f) / 2f;//between 0-1 rastgele sayý
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

    }
}
