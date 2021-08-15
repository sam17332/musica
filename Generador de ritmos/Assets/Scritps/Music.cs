using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    public AudioSource[] arraySonidos;
    public TextMeshProUGUI metricaTxt;
    public TextMeshProUGUI claveTxt;
    public TextMeshProUGUI fillerTxt;
    public GameObject inputField;
    private double metrica;
    private int BPM;
    private int cantidadTiempo = 100;
    private int contador;
    private List<int> claveInv = new List<int>();
    private bool play = true;
    
    void Start()
    {
        inputField.GetComponent<TMP_InputField>().text = "120";
        BPM = Int16.Parse(inputField.GetComponent<TMP_InputField>().text);
    }
    
    void Update()
    {
        BPM = Int16.Parse(inputField.GetComponent<TMP_InputField>().text);
    }
    
    public void iniciarCoroutine()
    {
        play = true;
        StartCoroutine(StartMusic());
    }

    public void GenerateRythm()
    {
        int[] subdivisionArray = { 3, 4 };
        int[] cantidadEspacios = { 1, 2, 4 };
        System.Random random = new System.Random();
        int cantidadSubdivision = subdivisionArray[random.Next(subdivisionArray.Length)];
        int subdivisionClave = (cantidadEspacios[random.Next(cantidadEspacios.Length)]) * cantidadSubdivision;
        metrica = cantidadSubdivision;
        List<int> subdivisionClaveList = new List<int>();
        int[] posiblesEspacios = { 2, 3 };
        bool iterate = true;
        while (iterate)
        {
            int rand = posiblesEspacios[random.Next(posiblesEspacios.Length)];
            int ope = subdivisionClaveList.Sum() + rand;
            if (ope < subdivisionClave)
            {
                subdivisionClaveList.Add(rand);
            }
            else if (ope > subdivisionClave)
            {
                subdivisionClaveList = new List<int>();
            }
            else if (ope == subdivisionClave)
            {
                subdivisionClaveList.Add(rand);
                iterate = false;
            }
        }

        List<int> clave = getClave(subdivisionClaveList);
        claveInv = getFiller(clave);
        string claveText = "";
        string fillerText = "";
        int contLocal = 0; 
        foreach (int i in clave)
        {
            if (contLocal != 0)
            {
                claveText = claveText + " , " + i.ToString();
            }
            else
            {
                claveText = i.ToString();
            }
            
            contLocal += 1;
        }

        contLocal = 0;
        foreach (int i in claveInv)
        {
            if (contLocal != 0)
            {
                fillerText = fillerText + " , " + i.ToString();
            }
            else
            {
                fillerText = i.ToString();
            }
            
            contLocal += 1;
        }

        metricaTxt.text = cantidadSubdivision.ToString();
        claveTxt.text = claveText;
        fillerTxt.text = fillerText;
    }

    public List<int> getClave(List<int> clave)
    {
        List<int> array = new List<int>();
        foreach (int i in clave)
        {
            if (i == 2)
            {
                array.Add(1);
                array.Add(0);
            }
            else if (i == 3)
            {
                array.Add(1);
                array.Add(0);
                array.Add(0);
            }
        }

        return array;
    }


    public List<int> getFiller(List<int> clave)
    {
        List<int> array = new List<int>();
        foreach (int i in clave)
        {
            if (i == 1)
            {
                array.Add(0);
            }
            else
            {
                array.Add(1);
            }
        }
        return array;
    }

    private int EvalFillerMetric()
    {
        return claveInv[contador % (int)metrica] == 1 ? 1 : 0;
    }

    IEnumerator StartMusic()
    {
        int contStart = 0;
        while (Time.time < cantidadTiempo && play)
        {
            arraySonidos[0].Play();
            contStart++;
            if (contStart % 2 == 0)
            {
                if (EvalFillerMetric() == 0)
                {
                    arraySonidos[1].Play();
                }
                else
                {
                    arraySonidos[2].Play();
                }
                contador++;
            }
            var interval = 60.0f / BPM;
            var subinterval = interval / 2;
            yield return new WaitForSecondsRealtime(subinterval);
        }
    }
}
