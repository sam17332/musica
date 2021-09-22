using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorAcordes : MonoBehaviour
{
    private List<string> arrayNotas = new List<string>() { "DO", "DO#", "RE", "RE#", "MI", "FA", "FA#", "SOL", "SOL#", "LA", "LA#", "SI" };
    private List<string> notas = new List<string>() { "DO", "DO#", "RE", "RE#", "MI", "FA", "FA#", "SOL", "SOL#", "LA", "LA#", "SI", "DO", "DO#", "RE", "RE#", "MI", "FA", "FA#", "SOL", "SOL#", "LA", "LA#", "SI" };
    private List<string> arrayAcorde = new List<string>();
    private List<int> sumasEscala = new List<int>() {2, 2, 1, 2, 2, 2, 1};
    
    private List<int> sumasAcorde = new List<int>() {2, 2};
    public List<string> arrayEscala = new List<string>();
    private List<string> arrayMulti = new List<string>();
    public string[,] arrayArrays = new string[7, 5];
    private int cont = 0;

    public string getRandomNote()
    {
        System.Random random = new System.Random();

        return arrayNotas[random.Next(arrayNotas.Count)];
    }
    
    private string obtenerTipo(int grado)
    {
        string tipo = "";
        if (grado == 1 || grado == 3 || grado == 6)
        {
            tipo = "TONICA";
        }
        else if (grado == 2 || grado == 4)
        {
            tipo = "SUB-DOMINANTE";
        }
        else if(grado == 5 || grado == 7)
        {
            tipo = "DOMINANTE";
        }
        
        return tipo;
    }

    public void GetAcordes(string nota)
    {
        if (arrayNotas.Contains(nota))
        {
            int indexActual;
            indexActual = arrayNotas.IndexOf(nota);
            foreach (int i in sumasEscala)
            {
                arrayEscala.Add(notas[indexActual]);
                indexActual += i;
                cont += 1;
            }
            cont = 1;

            int cantidad;
            cantidad = arrayEscala.Count;
            for (int i = 0; i < cantidad; i++)
            {
                arrayEscala.Add(arrayEscala[i]);    
            }
            
            for (int i = 0; i < cantidad; i++)
            {
                arrayAcorde = new List<string>();
                arrayMulti =  new List<string>();
                int notaActual;
                arrayAcorde.Add(arrayEscala[i]);
                notaActual = arrayEscala.IndexOf(arrayEscala[i]);
                
                foreach (int j in sumasAcorde)
                {
                    notaActual += j;
                    arrayAcorde.Add(arrayEscala[notaActual]);
                }

                int index0;
                int index1;
                int index2;
                int salto1;
                int salto2;
                string tipo = "";
                index0 = notas.IndexOf(arrayAcorde[0]);
                index1 = notas.IndexOf(arrayAcorde[1]);
                index2 = notas.IndexOf(arrayAcorde[2]);
                salto1 = index1 - index0;
                if (index0 > index1)
                {
                    salto1 = (index1 + 12) - index0;
                }
                
                salto2 = index2 - index1;
                if (index1 > index2)
                {
                    salto2 = (index2 + 12) - index1;
                }

                if (salto1 == 3 && salto2 == 3)
                {
                    tipo = "Disminuido";
                }
                else if (salto1 == 4 && salto2 == 4)
                {
                    tipo = "Aumentado";
                }
                else if (salto1 == 3 && salto2 == 4)
                {
                    tipo = "Menor";
                }
                else if (salto1 == 4 && salto2 == 3)
                {
                    tipo = "Mayor";
                }

                //string stringAcordes;
                //stringAcordes = string.Join(",", arrayAcorde);
                //Debug.Log("El acorde # " + cont + " es: " + stringAcordes + ". Su tipo es: " + obtenerTipo(cont) + " - " + tipo);
                arrayMulti.Add(obtenerTipo(cont));
                arrayMulti.Add(tipo);
                foreach (string h in arrayAcorde)
                {
                    arrayMulti.Add(h);
                }
                arrayArrays[cont-1, 0] = tipo;
                arrayArrays[cont-1, 1] = obtenerTipo(cont);
                arrayArrays[cont-1, 2] = arrayAcorde[0];
                arrayArrays[cont-1, 3] = arrayAcorde[1];
                arrayArrays[cont-1, 4] = arrayAcorde[2];
                cont += 1;
            }
        }
    }
}