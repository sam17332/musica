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

    if("#" not in nota):
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
            print("El acorde # " + str(cont) + " es: " + str(arrayAcorde))
            cont += 1

else:
    print("Ingrese una nota valida!!")