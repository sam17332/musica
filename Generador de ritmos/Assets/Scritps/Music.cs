using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    public AudioSource[] arraySonidos;
    private AudioSource MyAudioSource;
    public TextMeshProUGUI metricaTxt;
    public TextMeshProUGUI claveTxt;
    public TextMeshProUGUI fillerTxt;
    public TextMeshProUGUI notaTxt;
    public TextMeshProUGUI tonoTxt;
    public TextMeshProUGUI tonicaTxt;
    public GameObject inputField;
    public GameObject ritmoObj;
    public GameObject acordesObj;
    public GeneradorAcordes generador;
    public Button acordesBtn;
    public Button ritmoBtn;
    private double metrica;
    private int BPM;
    private int cantidadTiempo = 100;
    private int contador;
    private List<int> claveInv = new List<int>();
    private bool play = true;
    private string escala;
    private List<string> tipo1 = new List<string>() { "BLANCA", "NEGRA", "REDONDA" };
    //private List<string> tipo1 = new List<string>() { "REDONDA" };
    private List<string> tipo2 = new List<string>() { "TONICA", "SUB-DOMINANTE", "DOMINANTE" };
    public List<string> compasTipo1 = new List<string>();
    public List<string> compasTipo2 = new List<string>();
    private bool ritmoBool = true;
    private bool acordesBool = true;
    private bool inicio = true;
    private Dictionary<int, List<string>> acordes = new Dictionary<int, List<string>>();
    private Dictionary<string, int> notas = new Dictionary<string, int>(){
            {"DO", 3},
            {"DO#", 4},
            {"RE", 5},
            {"RE#", 6},
            {"MI", 7},
            {"FA", 8},
            {"FA#", 9},
            {"SOL", 10},
            {"SOL#", 11},
            {"LA", 12},
            {"LA#", 13},
            {"SI", 14}
        };

    void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
        inputField.GetComponent<TMP_InputField>().text = "120";
        BPM = Int16.Parse(inputField.GetComponent<TMP_InputField>().text);
        acordesObj.SetActive(false);
        ritmoObj.SetActive(true);
    }
    
    void Update()
    {
        BPM = Int16.Parse(inputField.GetComponent<TMP_InputField>().text);

        if (acordesBool && !inicio)
        {
            acordesBtn.interactable = true;
            acordesObj.SetActive(true);
        } else if (!acordesBool)
        {
            acordesBtn.interactable = false;
            acordesObj.SetActive(false);
        }
        
        if (ritmoBool && !inicio)
        {
            ritmoBtn.interactable = true;
            ritmoObj.SetActive(true);
        } else if (!ritmoBool)
        {
            ritmoBtn.interactable = false;
            ritmoObj.SetActive(false);
        }

        if (acordesBool && ritmoBool)
        {
            acordesObj.SetActive(false);
        }
    }
    
    public void iniciarCoroutine()
    {
        play = true;
        StartCoroutine(StartMusic());
    }

    public void GenerarAcordes()
    { 
        acordes = new Dictionary<int, List<string>>();
        ritmoBool = false;
        acordesBool = true;
        inicio = false;
        string nota = generador.getRandomNote();
        compasTipo1 = new List<string>();
        compasTipo2 = new List<string>();
        generador.arrayEscala = new List<string>();
        generador.GetAcordes(nota);
        GenerarCompas();
        notaTxt.text = nota;
    }
    
    private void GenerarCompas()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < 8; i++)
        {
            compasTipo1.Add(tipo1[random.Next(tipo1.Count)]);
        }
        
        tonoTxt.text = string.Join(", ", compasTipo1);

        int cantidad = 0;
        foreach (var i in compasTipo1)
        {
            if (i == "BLANCA")
            {
                cantidad += 2;
            }
            else if (i == "NEGRA")
            {
                cantidad += 4;
            }
            else if (i == "REDONDA")
            {
                cantidad += 1;
            }
        }

        for (int i = 0; i < cantidad; i++)
        {
            string tipo = tipo2[random.Next(tipo2.Count)];
            compasTipo2.Add(tipo);
            acordes.Add(i, SearchRandomChord(tipo));
        }

        //foreach (var acorde in acordes)
        //{
        //    print(acorde.Key);
        //    print(string.Join(", ", acorde.Value));
        //}

        tonicaTxt.text = string.Join(", ", compasTipo2);
    }

    private List<string> SearchRandomChord(string tipo)
    {
        System.Random random = new System.Random();
        bool acordeCorrecto = true;
        int numero = 0;
        while (acordeCorrecto)
        {
            numero = random.Next(7);
            string acordeTipo = generador.arrayArrays[numero, 1];
            if (tipo == acordeTipo)
            {
                acordeCorrecto = false;
            }    
        }
        
        List<string> acordeActual = new List<string>();
        acordeActual.Add(generador.arrayArrays[numero, 2]);
        acordeActual.Add(generador.arrayArrays[numero, 3]);
        acordeActual.Add(generador.arrayArrays[numero, 4]);
        //print(string.Join(",", acordeActual) + generador.arrayArrays[numero, 1]);

        return acordeActual;
    }

    public void Parar()
    {
        MyAudioSource.Stop();
        ritmoBool = true;
        acordesBool = true;
    }
    
    public void GenerateRythm()
    {
        ritmoBool = true;
        acordesBool = false;
        inicio = false;
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
        int cont2 = 0;
        int indexActual = 0;
        List<string> array;
        int contBlanca = 0;
        int contNegra = 0;
        string compas;
        while (Time.time < cantidadTiempo && play)
        {
            if (acordesBool)
            {
                if (indexActual < acordes.Count - 1)
                {
                    if (cont2 != 0)
                    {
                        cont2 += 1;
                    }
                    compas = compasTipo1[indexActual];
                    array = acordes[indexActual];
                    if (compas == "BLANCA")
                    {
                        if (cont2 % 6 == 0)
                        {
                            //print(string.Join(",", array));
                            MyAudioSource.Stop();
                            arraySonidos[notas[array[0]]].Play();
                            arraySonidos[notas[array[1]]].Play();
                            arraySonidos[notas[array[2]]].Play();
                            contBlanca += 1;
                            if (contBlanca == 2)
                            {
                                indexActual += 1;
                                contBlanca = 0;
                            }
                            cont2 = 0;
                        }
                    } else if (compas == "NEGRA")
                    {
                        if (cont2 % 3 == 0)
                        {
                            //print(string.Join(",", array));
                            MyAudioSource.Stop();
                            arraySonidos[notas[array[0]]].Play();
                            arraySonidos[notas[array[1]]].Play();
                            arraySonidos[notas[array[2]]].Play();
                            contNegra += 1;
                            if (contNegra == 4)
                            {
                                indexActual += 1;
                                contNegra = 0;
                            }
                            cont2 = 0;
                        }
                    } else if (compas == "REDONDA")
                    {
                        if (cont2 % 12 == 0)
                        {
                            //print(string.Join(",", array));
                            MyAudioSource.Stop();
                            arraySonidos[notas[array[0]]].Play();
                            arraySonidos[notas[array[1]]].Play();
                            arraySonidos[notas[array[2]]].Play();
                            indexActual += 1;
                            cont2 = 0;
                        }
                    }
                }
                else
                {
                    if (cont2 % 12 == 0)
                    {
                        MyAudioSource.Stop();
                        Parar();
                    }
                }

                if (cont2 == 0)
                {
                    cont2 += 1;
                }
            } else if (ritmoBool)
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
            }
            var interval = 60.0f / BPM;
            var subinterval = interval / 2;
            yield return new WaitForSecondsRealtime(subinterval);
        }
    }
}
