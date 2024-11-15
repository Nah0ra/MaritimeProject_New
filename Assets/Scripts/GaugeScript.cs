using System.Collections;
using UnityEngine;

public class GaugeScript : MonoBehaviour
{

    private SimpleGaugeMaker _simplegaugemaker;
    private float _MaxValue;
    private float _MinValue;
    public bool Forward;
    public float Value;

    public float idealValue;

    public bool Active;

    public bool IdealReached;

    [Tooltip("Input rate of change in seconds, i.e the time it takes for the liquid to heat by one degree")]
    public float RateOfChange;

    private void Start()
    {
        _simplegaugemaker = gameObject.GetComponent<SimpleGaugeMaker>();
        _MaxValue = _simplegaugemaker.gaugeInputs[0].minMaxValue.y;
        _MinValue = _simplegaugemaker.gaugeInputs[0].minMaxValue.x;
        StartCoroutine(GaugeManager(RateOfChange, _MinValue, _MaxValue));
    }


    //Increae value by rate of change per second
    IEnumerator GaugeManager(float Rate, float Min, float Max)
    {
        while (true)
        {
                //While the dial is increasing its value, increase the value
                while (Forward)
                {
                    float count = _simplegaugemaker.gaugeInputs[0].value;
                    while (count != Max)
                    {
                        if (!Forward || !Active)
                        {
                            break;
                        }
                        else
                        {
                            count++;
                            Value = count;
                            _simplegaugemaker.setInputValue("Fuel Pressure", count);

                            if (Value == idealValue)
                            {
                                IdealReached = true;
                            }
                            else
                            {
                                IdealReached = false;
                            }

                            yield return new WaitForSeconds(Rate);
                        }
                    }
                 yield return new WaitForEndOfFrame();
                }

                while (!Forward)
                {
                    float count = _simplegaugemaker.gaugeInputs[0].value;
                    while (count != Min)
                    {
                        if (Forward || !Active)
                        {
                            break;
                        }
                        else
                        {
                            count--;
                            Value = count;
                            _simplegaugemaker.setInputValue("Fuel Pressure", count);

                            if (Value == idealValue)
                            {
                                IdealReached = true;
                            }
                            else
                            {
                                IdealReached = false;
                            }
                            
                            yield return new WaitForSeconds(Rate);
                        }
                    }
                yield return new WaitForEndOfFrame();
            }
          yield return new WaitForEndOfFrame();
        }  
    }

    //Toggles the dial function on or off
    public void ToggleOn()
    {
        if (Active)
        {
            Active = false;
        }
        else
        {
            Active = true;
        }
    }

    //Changes the direction of the dial
    public void ToggleDirection()
    {
        if (Forward)
        {
            Forward = false;
        }
        else
        {
            Forward = true;
        }
    }
}
