def obtenerTipo(grado):
    tipo = ""
    print(grado)
    if(grado == 1 or grado == 3 or grado == 6):
        tipo = "TONICA"
    elif(grado == 2 or grado == 4):
        tipo = "SUB-DOMINANTE"
    elif(grado == 5 or grado == 7):
        tipo = "DOMINANTE"
    print(tipo)
    return tipo

notas = ["do", "do#", "re", "re#", "mi", "fa", "fa#", "sol", "sol#", "la","la#", "si", "do", "do#", "re", "re#", "mi", "fa", "fa#", "sol", "sol#", "la","la#", "si"]
sumasEscala = [2, 2, 1, 2, 2, 2, 1]
sumasAcorde = [2, 2]
arrayEscala = []
escala = ""
cont = 0

print('Calculador de escalas y acordes.')
print()
print('''
    -------------------------------------
    | do  | do# | re  | re# | mi  | fa  |
    -------------------------------------
    | fa# | sol | sol#| la  | la# | si  |
    -------------------------------------
''')

grado = input("Ingrese un grado (de 1 a 7): ")
nota = input("Ingrese una de las notas de arriba para saber su escala y su acorde: ")
print()

if (nota in notas):
    indexActual = notas.index(nota)
    for i in sumasEscala:
        escala += notas[indexActual]
        arrayEscala.append(notas[indexActual])
        if(cont != len(sumasEscala)-1):
            escala +=  ", "
        indexActual += i
        cont += 1
    cont = 1
    print("La escala de " + nota + " es: " + escala)
    print()

    cantidad = len(arrayEscala)
    for i in range(cantidad):
        arrayEscala.append(arrayEscala[i])

    for i in range(cantidad):
        arrayAcorde = []
        arrayAcorde.append(arrayEscala[i])
        notaActual = arrayEscala.index(arrayEscala[i])
        for j in sumasAcorde:
            notaActual += j
            arrayAcorde.append(arrayEscala[notaActual])
        index0 = notas.index(arrayAcorde[0])
        index1 = notas.index(arrayAcorde[1])
        index2 = notas.index(arrayAcorde[2])
        salto1 = index1 - index0
        if(index0 > index1):
            salto1 = (index1 + 12) - index0
        salto2 = index2 - index1
        if(index1 > index2):
            salto2 = (index2 + 12) - index1

        tipo = ""
        if(salto1 == 3 and salto2 == 3):
            tipo = "Disminuido"
        elif(salto1 == 4 and salto2 == 4):
            tipo = "Aumentado"
        elif(salto1 == 3 and salto2 == 4):
            tipo = "Menor"
        elif(salto1 == 4 and salto2 == 3):
            tipo = "Mayor"

        if(cont == int(grado)):
            print("El acorde # " + str(cont) + " es: " + str(arrayAcorde) + ". Su tipo es: " + obtenerTipo(int(grado)) + " - " + tipo)

        cont += 1

else:
    print("Ingrese una nota valida!!")

