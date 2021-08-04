using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random=UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Metronomo : MonoBehaviour
{
    public GameObject objeto;
    public AudioSource[] sonidos;
    private int bpm;
    public Text bpmText;
    private double metrica;
    public Text metricaText;
    private bool subdividir = false;
    public Toggle subdividirToggle;
    private int cont;
    private double interval;
    private int cantidadTiempo = 100;
    private bool unaVez = false;

    void Start() {
        bpm = Int16.Parse(bpmText.text);
        metrica = Int16.Parse(metricaText.text.Substring(0,1));
        subdividir = subdividirToggle.isOn;
        interval = 60.0f / bpm;
    }

    void Update() {
        if (bpm != Int16.Parse(bpmText.text)) {
            interval = 60.0f / Int16.Parse(bpmText.text);
        }
        bpm = Int16.Parse(bpmText.text);
        metrica = Int16.Parse(metricaText.text.Substring(0,1));
        subdividir = subdividirToggle.isOn;
    }

    public void iniciarCoroutine() {
        StartCoroutine(soundRoutine());
    }

    IEnumerator soundRoutine() {
        while (Time.time < cantidadTiempo) {
            if(subdividir && !unaVez) {
                unaVez = true;
                interval = interval / 2;
            } else if (!subdividir && unaVez) {
                unaVez = false;
                interval = 60.0f / bpm;
            }

            cont++;
            if (cont % metrica == 1) {
                sonidos[0].Play();
                objeto.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                if(subdividir) {
                    sonidos[2].Play();
                }
            } else {
                sonidos[1].Play();
                objeto.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                if(subdividir) {
                    sonidos[2].Play();
                }
            }

            if(subdividir) {
                sonidos[2].Play();
                objeto.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            }
            yield return new WaitForSecondsRealtime((float)interval);
        }
    }
}
