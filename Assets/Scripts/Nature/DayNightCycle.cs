using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.PostProcessing;

public class DayNightCycle : MonoBehaviour {

    public float time;
    public TimeSpan currentTime;
    public Transform SunTransform;
    public Light Sun;
    public Text DayText;
    public int days;

    public int rainIntensity;
    public float intensity;
    public Color fogday = Color.grey;
    public Color fogNight = Color.black;
    public Image Darkness;
    public ParticleSystem rain;

    public int speed;

    public float timeTillNextFall;
    public float rainTime;

    bool isRaining;

    public AudioSource rainSound;
    public float MaxVol;

    bool fadein = true;
    bool fadeout = true;

    float month;
    float year;

    [ColorUsage(true, true)]
    public Color colorFilter;

    public CountryInfo[] countrys;
    public PostProcessVolume ppl;
    public FloatParameter brightness;

    

    void Start ()
    {
       // time = 86400;
        year = 1946;
        timeTillNextFall = UnityEngine.Random.Range(10, 20000);
        rainTime = UnityEngine.Random.Range(10, 5000);

    }

    void Update () {
        ChangeTime();

    }

    public void ChangeTime()
    {
        time += Time.deltaTime * speed;
        if (time > 86400)
        {
            foreach (var country in countrys)
            {
                if (country.isImproving)
                {
                    country.relation += 1;
                }
            }
            days += 1;
            time = 0;

              

                Debug.Log("save");
            
        }

        currentTime = TimeSpan.FromSeconds(time);
        string[] temptime = currentTime.ToString().Split(":"[0]);
        SunTransform.rotation = Quaternion.Euler(new Vector3((time - 21600) / 86400 * 360, 0, 0));
        if(time < 43200)
        {
            intensity = 1 - (43200 - time) / 43200;
        }
        else
        {
            intensity = 1 - ((43200 - time) / 43200 * -1);
        }

        RenderSettings.fogColor = Color32.Lerp(fogNight, fogday, intensity * intensity);

        if(timeTillNextFall <= 0)
        {
            isRaining = true;
        }
        else
        {
            timeTillNextFall -= Time.deltaTime;
            isRaining = false;
        }
        if (isRaining)
        {
            rainTime -= Time.deltaTime;
            rain.Play();
        }
        if(rainTime <= 0)
        {
            isRaining = false;
            rainTime = UnityEngine.Random.Range(10, 5000);
            rain.Stop();

        }
        if (isRaining)
        {
            if (fadein)
            {
                rainSound.volume = MaxVol;
            }
        }
        else
        {
            if (fadeout)
            {
                rainSound.volume = 0.0f;
            }
        }

      
        rain.emissionRate = rainIntensity;
        Sun.intensity = intensity;
        float brightnessfloat = intensity;
        brightness.value = (intensity * 10) - 4;
        ppl.profile.GetSetting<ColorGrading>().brightness.value= brightness;

        colorFilter = new Color(1*(intensity/2)+.5f, 1*(intensity / 2) + .5f, 1*(intensity / 2) + .5f);
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ColorGrading>().colorFilter.value = colorFilter;
        Color c = Darkness.color;
        c.a = (1 - intensity)/2;
        Darkness.color = c;

        if(days == 32 && month == 0)
        {
            month = 1;
            days = 1;
         
        }
        if (days == 29 && month == 1)
        {
            month = 2;
            days = 1;
         
        }
        if (days == 32 && month == 2)
        {
            month = 3;
            days = 1;
        }
        if (days == 31 && month == 3)
        {
            month = 4;
            days = 1;
         
        }
        if (days == 32 && month == 4)
        {
            month = 5;
            days = 1;
         
        }
        if (days == 31 && month == 5)
        {
            month = 6;
            days = 1;
        
        }
        if (days == 32 && month == 6)
        {
            month = 7;
            days = 1;
         
        }
        if (days == 32 && month == 7)
        {
            month = 8;
            days = 1;
        }
        if (days == 31 && month == 8)
        {
            month = 9;
            days = 1;
          
        }
        if (days == 32 && month == 9)
        {
            month = 10;
            days = 1;
            
        }
        if (days == 31 && month == 10)
        {
            month = 11;
            days = 1;
            
        }
        if (days == 32 && month == 11)
        {
            month = 0;
            year += 1;
            days = 1;
            
        }

        if (month >= 0 && month <= 2)
        {
            DayText.text = "Day: " + days + ", Spring" + ",      " + temptime[0] + ":" + temptime[1];

        }
        if (month > 3 && month <= 5)
        {
            DayText.text = "Day: " + days + ", Summer" + ",      " + temptime[0] + ":" + temptime[1] ;

        }
        if (month > 6 && month <= 8)
        {
            DayText.text = "Day: " + days + ", Autumn" + ",      " + temptime[0] + ":" + temptime[1] ;

        }
        if (month > 9 && month <= 11)
        {
            DayText.text = "Day: " + days + ", Winter" + ",      " + temptime[0] + ":" + temptime[1];

        }

        /*
                if (month == 0)
                {
                    DayText.text = year + ", " + "Jan, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 1)
                {
                    DayText.text = year + ", " + "Feb, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 2)
                {
                    DayText.text = year + ", " + "Mar, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 3)
                {
                    DayText.text = year + ", " + "Apr, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 4)
                {
                    DayText.text = year + ", " + "May, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 5)
                {
                    DayText.text = year + ", " + "Jun, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 6)
                {
                    DayText.text = year + ", " + "Jul, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 7)
                {
                    DayText.text = year + ", " + "Aug, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 8)
                {
                    DayText.text = year + ", " + "Sep, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 9)
                {
                    DayText.text = year + ", " + "Oct, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 10)
                {
                    DayText.text = year + ", " + "Nov, " + days + ", " + temptime[0] + ":" + temptime[1];

                }
                if (month == 11)
                {
                    DayText.text = year + ", " + "Dec, " + days + ", " + temptime[0] + ":" + temptime[1];

                }*/
    }

}
