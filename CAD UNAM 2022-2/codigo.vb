Module codigo

    'Public Function DesplazamientoLinea(ByVal x1 As Double)
    '    Dim aux As Double
    '    aux = x1 + 1500
    '    Return aux
    'End Function
    'Public Function CalcularCentroLinea(c1 As Double, c2 As Double)
    '    Dim centro As Double
    '    centro = (c1 + c2) / 2
    '    Return centro
    'End Function

    'Public Function esVertical(linea As AcadLine) As Boolean
    '    Dim status As Boolean
    '    Dim angulo As Integer = 0
    '    Dim p1(0 To 2) As Double
    '    Dim p2(0 To 2) As Double
    '    p1 = linea.StartPoint
    '    p2 = linea.EndPoint
    '    If p1(0) = p2(0) AndAlso p1(1) = p2(1) AndAlso p1(2) = p2(2) Then
    '        status = False
    '    ElseIf p1(0) = p2(0) And p1(2) = p2(2) And p1(1) <> p2(1) Then
    '        status = True
    '    End If
    '    Return status
    'End Function

    'Public Sub clasificaContraLineaVertical()
    '    'Clasificacion de circulos y lineas
    '    'Los círculos quedan en la parte izquierda y las lineas en la derecha
    '    'Su punto de union queda en la mitad de la linea
    '    'Los circulos no deben intersectarse
    '    'Si se intersectan, deben moverse hacia arriba hasta que no haya interseccion
    '    'los circulos deben ser tangentes a la linea vertical
    '    Dim linea As AcadLine
    '    Dim objetosAnalizar As AcadSelectionSet
    '    Dim objParticular As AcadEntity
    '    Dim line As AcadLine
    '    Dim endPnt(0 To 2) As Double
    '    Dim startPnt(0 To 2) As Double
    '    Dim middlePnt(0 To 2) As Double
    '    Dim objeto As AcadEntity
    '    Dim objeto2 As AcadEntity
    '    Dim lineaVertical As AcadEntity
    '    Dim pBase() As Double
    '    Dim circuloAnalizado As AcadCircle
    '    Dim pCentroCirculo() As Double

    '    objeto2 = getEntidad(objeto, "selecciona una linea")
    '    If Not IsNothing(objeto2) AndAlso objeto2.ObjectName = "AcDbLine" AndAlso esVertical(objeto2) Then
    '        'Obtencion de puntos de la linea vertical en coordenadas X y Y
    '        linea = objeto2
    '        lineaVertical = objeto2
    '        middlePnt = DOCUMENTO.Utility.PolarPoint(linea.StartPoint, linea.Angle, linea.Length / 2.0)
    '        'seleccionar los elementos que voy a analizar
    '        objetosAnalizar = getConjunto("Selecciona Conjunto de objetos")
    '        For Each objParticular In objetosAnalizar
    '            If objParticular.Handle <> lineaVertical.Handle Then
    '                Select Case objParticular.EntityName
    '                    Case "AcDbLine"
    '                        line = objParticular
    '                        objParticular = Nothing
    '                        startPnt = line.StartPoint
    '                        endPnt = line.EndPoint
    '                        If startPnt(0) < endPnt(0) Then
    '                            pBase = startPnt
    '                        Else
    '                            pBase = endPnt
    '                        End If
    '                        line.Move(pBase, middlePnt)
    '                        line.Update()
    '                    Case "AcDbCircle"
    '                        circuloAnalizado = objParticular
    '                        pCentroCirculo = circuloAnalizado.Center
    '                        pCentroCirculo(0) = middlePnt(0) - circuloAnalizado.Radius
    '                        circuloAnalizado.Center = pCentroCirculo
    '                        circuloAnalizado.Update()
    '                        librarInterferencias(circuloAnalizado)
    '                End Select
    '            End If
    '        Next
    '    End If
    'End Sub

    'Public Sub librarInterferencias(ByRef circulo As AcadCircle)
    '    'Buscar Intersecciones del circulo
    '    'Si hay interseciones entonces mover el circulo hacia arriba en su radio\
    '    'Repetir este ciclo hasta que no haya intersecciones
    '    Dim objetos As AcadSelectionSet
    '    Dim p1() As Double = Nothing
    '    Dim p2() As Double = Nothing
    '    Dim pDestino(0 To 2) As Double
    '    Dim objParticular As AcadEntity
    '    Dim intersecciones As Boolean = True
    '    Dim meMovi As Boolean = True
    '    Dim PickedPoint(0 To 2) As Double
    '    Dim TransMatrix(0 To 3, 0 To 3) As Double
    '    Dim Objeto As AcadObject = Nothing
    '    Dim ContextData As Object = Nothing
    '    Dim esquinas(11) As Double
    '    Dim coordCirculo() As Double


    '    While meMovi
    '        circulo.GetBoundingBox(p1, p2)
    '        objetos = getObjetosEnRectangulo(p1, p2)
    '        If Not IsNothing(objetos) AndAlso objetos.Count = 1 Then
    '            'Esta salida solo se da cuando el circulo esta solo y no esta tocando a la linea. Lo cual es una solucion parcial porque hay lineas infinitas 
    '            Exit While
    '        ElseIf Not IsNothing(objetos) AndAlso objetos.Count > 1 Then
    '            meMovi = False
    '            For Each elemento In objetos
    '                If elemento.objectname = "AcDbCircle" And elemento.handle <> circulo.handle Then
    '                    pDestino = circulo.Center
    '                    pDestino(1) = pDestino(1) + circulo.Radius
    '                    circulo.Move(circulo.Center, pDestino)
    '                    circulo.Update()
    '                    meMovi = True
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '    End While
    'End Sub

    'Public Function getObjetosEnRectangulo(p1() As Double, p2() As Double) As AcadSelectionSet
    '    Dim Nconjunto As AcadSelectionSet
    '    Dim esquinas(0 To 11) As Double
    '    'cada 3 indices representan una coordenada XYZ
    '    esquinas(0) = p1(0) : esquinas(1) = p1(1) : esquinas(2) = 0 'coord1
    '    esquinas(3) = p2(0) : esquinas(4) = p1(1) : esquinas(5) = 0 'coord2
    '    esquinas(6) = p2(0) : esquinas(7) = p2(1) : esquinas(8) = 0 'coord3
    '    esquinas(9) = p1(0) : esquinas(10) = p2(1) : esquinas(11) = 0 'coord4
    '    Nconjunto = conjunto_vacio(DOCUMENTO, "IDLE")
    '    If Not IsNothing(Nconjunto) Then
    '        Nconjunto.SelectByPolygon(AcSelect.acSelectionSetCrossingPolygon, esquinas)
    '    End If
    '    Return Nconjunto
    'End Function

    'Public Sub DimensionarLote()
    '    Dim poligono As AcadEntity
    '    Dim poligonal As AcadLWPolyline
    '    Dim coord() As Double
    '    Dim b As Integer = 0
    '    Dim Texto As AcadText
    '    Dim p As Double

    '    appActivateAutoCAD()
    '    poligono = selectEntidad("selecciona un poligono")

    '    If Not IsNothing(poligono) AndAlso poligono.ObjectName = "AcDbPolyline" AndAlso poligono.closed Then
    '        Dim i As Integer = 0
    '        Dim j As Integer = 0
    '        Dim x As Double = 0 'Terminos x dentro de la raíz
    '        Dim y As Double = 0 'Terminos y dentro de la raíz
    '        poligonal = poligono
    '        coord = poligono.Coordinates()
    '        Dim Xs(0 To (coord.Length / 2) - 1) As Double
    '        Dim Ys(0 To (coord.Length / 2) - 1) As Double
    '        Dim distancias(0 To (coord.Length / 2) - 1) As Double
    '        Dim centrosX(0 To (coord.Length / 2) - 1) As Double
    '        Dim centrosY(0 To (coord.Length / 2) - 1) As Double
    '        Dim location(0 To 2) As Double

    '        For Each p In coord
    '            If b = 0 Then
    '                Xs(i) = p
    '                b = 1
    '                i += 1

    '            ElseIf b = 1 Then
    '                Ys(j) = p
    '                b = 0
    '                j += 1

    '            End If

    '        Next



    '        For i = 0 To Xs.Length - 2
    '            x = Math.Pow(Xs(i + 1) - Xs(i), 2)
    '            y = Math.Pow(Ys(i + 1) - Ys(i), 2)
    '            distancias(i) = Math.Sqrt(x + y)
    '        Next
    '        'Ultimo elemento
    '        x = Math.Pow(Xs(0) - Xs(Xs.Length - 1), 2)
    '        y = Math.Pow(Ys(0) - Ys(Ys.Length - 1), 2)
    '        distancias(distancias.Length - 1) = Math.Sqrt(x + y)

    '        'Obteniendo los centros
    '        For i = 0 To distancias.Length - 2
    '            centrosX(i) = CalcularCentroLinea(Xs(i), Xs(i + 1))
    '            centrosY(i) = CalcularCentroLinea(Ys(i), Ys(i + 1))
    '        Next
    '        centrosX(centrosX.Length - 1) = CalcularCentroLinea(Xs(0), Xs(Xs.Length - 1))
    '        centrosY(centrosY.Length - 1) = CalcularCentroLinea(Ys(0), Ys(Ys.Length - 1))

    '        Dim point As AcadPoint
    '        Dim textAux As String
    '        Dim alineaciones(Xs.Length) As Double
    '        Dim location1(0 To 2) As Double

    '        For i = 0 To centrosX.Length - 1
    '            location = {centrosX(i), centrosY(i), 0}
    '            textAux = Math.Round(distancias(i), 2).ToString()
    '            Texto = DOCUMENTO.ModelSpace.AddText(textAux, location, 15)
    '        Next

    '    End If


    'End Sub

    'Public Sub agregarDatos(manzana As String, lote As String, uso As String)
    '    'agregar la informacion empleando DICCIONARIOS a una polilinea seleccionada dentro de autocad'
    '    'la polilinea debe estar cerrada y no puede intersectarse a sí misma'

    '    Dim poligono As AcadEntity
    '    Dim poligonal As AcadLWPolyline

    '    appActivateAutoCAD()
    '    poligono = selectEntidad("Selecciona una poligonal")
    '    If Not IsNothing(poligono) AndAlso poligono.ObjectName = "AcDbPolyline" AndAlso poligono.closed Then
    '        poligonal = poligono
    '        addXdata(poligonal, "MANZANA", manzana)
    '        addXdata(poligonal, "LOTE", lote)
    '        addXdata(poligonal, "USO", uso)

    '    Else
    '        DOCUMENTO.Utility.Prompt("No seleccionaste una polilinea o la polilinea está abierta")
    '    End If

    'End Sub

    'Public Sub recuperarDatos(ByRef manzana As String, ByRef lote As String, ByRef uso As String)

    '    Dim poligono As AcadEntity
    '    appActivateAutoCAD()
    '    poligono = selectEntidad("selecciona un poligono")

    '    If Not IsNothing(poligono) AndAlso poligono.ObjectName = "AcDbPolyline" AndAlso poligono.closed Then
    '        getXdata(poligono, "MANZANA", manzana)
    '        getXdata(poligono, "LOTE", lote)
    '        getXdata(poligono, "USO", uso)
    '    End If
    'End Sub

    Public Sub inicializa_conexion(ByVal version As String)
        Dim R As String
        Select Case version
            Case "2017"
                R = "AUTOCAD.Application.21"
            Case "2022"
                R = "AUTOCAD.Application.24"
            Case Else
                R = ""
        End Select
        Try
            DOCUMENTO = Nothing
            AUTOCADAPP = GetObject(, R)
            DOCUMENTO = AUTOCADAPP.ActiveDocument
            UTILITY = DOCUMENTO.Utility
            AUTOCADAPP.Visible = True
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Information, "CAD")
        End Try
    End Sub

    Public Function getConjunto(ByVal indef As String) As AcadSelectionSet
        Dim conjunto As AcadSelectionSet = Nothing
        conjunto = conjunto_vacio(DOCUMENTO, "ANALISIS")
        If Not IsNothing(conjunto) Then
            conjunto.SelectOnScreen()
        End If
        Return conjunto
    End Function

    Public Function getEntidad(ByRef objeto As AcadEntity, mensaje As String) As AcadEntity
        Dim c(0 To 2) As Double
        objeto = Nothing
        Try
            DOCUMENTO.Utility.GetEntity(objeto, c, mensaje)
        Catch ex As Exception

        End Try
        Return objeto
    End Function


    Public Function conjunto_vacio(ByRef documento As AcadDocument, ByVal nombre As String) As AcadSelectionSet
        Dim indice As Integer
        nombre = nombre.Trim.ToUpper
        Try
            For indice = 0 To documento.SelectionSets.Count - 1
                If documento.SelectionSets.Item(indice).Name = nombre Then
                    documento.SelectionSets.Item(indice).Delete()
                    Exit For
                End If
            Next
            conjunto_vacio = documento.SelectionSets.Add(nombre)
            Return conjunto_vacio
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "CAD")
        End Try

    End Function

    Public Function solicitarCoordenada(mensaje As String, Optional pb() As Double = Nothing) As Double()
        Dim p() As Double = Nothing

        Try
            If IsNothing(pb) Then
                p = DOCUMENTO.Utility.GetPoint(, mensaje)
            Else
                p = DOCUMENTO.Utility.GetPoint(pb, mensaje)
            End If
        Catch e As Exception

        End Try
        Return p
    End Function

    Public Sub appActivateAutoCAD()
        Dim AUTOCADWINDNAME As String
        Dim acadProcess() As Process = Process.GetProcessesByName("ACAD")

        Try
            AUTOCADWINDNAME = acadProcess(0).MainWindowTitle
            AppActivate(AUTOCADWINDNAME)
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Public Sub seleccionDeObjetos(metodo As String)
        Dim conjunto As AcadSelectionSet
        Dim entidad As AcadEntity = Nothing
        Dim p(0 To 2) As Double
        Dim p1(0 To 2) As Double
        Dim esquinas(0 To 11) As Double
        'appActivateAutoCAD()
        Select Case metodo
            Case "A"
                'Seleccion selectiva'
                conjunto = conjunto_vacio(DOCUMENTO, "IDLE")
                If Not IsNothing(conjunto) Then
                    conjunto.SelectOnScreen()

                End If
            Case "D"
                'Seleccion de un elemento'
                Try
                    DOCUMENTO.Utility.GetEntity(entidad, p, "Selecciona un elemento")
                Catch ex As Exception

                End Try
            Case "B"
                p = solicitarCoordenada("Esquina 1")
                If Not IsNothing(p) Then
                    p1 = solicitarCoordenada("Esquina opuesta", p)
                    If Not IsNothing(p1) Then
                        'cada 3 indices representan una coordenada XYZ
                        esquinas(0) = p(0) : esquinas(1) = p(1) : esquinas(2) = 0 'coord1
                        esquinas(3) = p1(0) : esquinas(4) = p(1) : esquinas(5) = 0 'coord2
                        esquinas(6) = p1(0) : esquinas(7) = p1(1) : esquinas(8) = 0 'coord3
                        esquinas(9) = p(0) : esquinas(10) = p1(1) : esquinas(11) = 0 'coord4
                        conjunto = conjunto_vacio(DOCUMENTO, "IDLE")
                        If Not IsNothing(conjunto) Then
                            conjunto.SelectByPolygon(AcSelect.acSelectionSetCrossingPolygon, esquinas)
                            MsgBox(conjunto.Count)
                        End If
                    End If
                End If

            Case "F"
                Dim PickedPoint(0 To 2) As Double, TransMatrix(0 To 3, 0 To 3) As Double
                Dim ContextData As Object = Nothing
                Dim HasContextData As String
                Dim Objeto As AcadObject = Nothing
                Dim objetoAsociado As AcadObject = Nothing
                Dim atrObj As AcadAttribute

                Try
                    DOCUMENTO.Utility.GetSubEntity(Objeto, PickedPoint, TransMatrix, ContextData)

                Catch
                    'no se selecciono un objeto
                End Try
                If Not IsNothing(Objeto) Then
                    HasContextData = IIf(VarType(ContextData) = vbEmpty, "does not", "does")
                    MsgBox("The object you chose was a:" & Objeto.ObjectName & vbCrLf &
                           "You point of selection was: " & PickedPoint(0) & ", " &
                                                            PickedPoint(1) & ", " &
                                                            PickedPoint(1) & vbCrLf &
                            "The container" & HasContextData & "have nested objects.")
                    If Not IsNothing(ContextData) Then
                        objetoAsociado = DOCUMENTO.ObjectIdToObject(ContextData(0))
                        MsgBox("Objeto Asociado es " & objetoAsociado.ObjectName)
                    End If
                End If
                If Not IsNothing(Objeto) AndAlso Objeto.ObjectName = "AcDbAttribute" Then
                    MsgBox(Objeto.Handle & "Tipo= " & Objeto.tagstring & " " & Objeto.textstring)
                    atrObj = Objeto
                End If

            Case Else
                'Sin seleccion de metodo

        End Select
    End Sub

    'INICIA PROYECTO 
    Public Sub asociaSenalEntradaDirecto()
        Dim bloque As AcadBlockReference = Nothing
        Dim entidad As AcadEntity = Nothing
        Dim nombreDeEntrada As String = Nothing
        Dim senal As AcadEntity = Nothing
        Dim tipo As String = Nothing
        Dim p(0 To 2) As Double

        appActivateAutoCAD()
        getSubEntidad(entidad, "Selecciona la entrada de una compuerta ", p, bloque)
        If Not IsNothing(entidad) Then
            getXdata(entidad, "TIPO", tipo)
            If tipo = "ENTRADA" Then
                getXdata(entidad, "NOMBRE", nombreDeEntrada)
                'seleccionando una senal para asignar a la salida
                getEntidad(senal, "Selecciona una señal")
                If Not IsNothing(senal) AndAlso senal.ObjectName = "AcDbText" Then
                    'asignando al atributo el apuntador a la señal
                    addXdata(entidad, "SENAL", senal.Handle)
                    'actualizando a la compuerta agregando una capa de informacion que nos indique 
                    'quien es la ENTRADA
                    addXdata(bloque, "ENTRADA" & "-" & nombreDeEntrada, entidad.Handle)
                End If
            End If
        End If
    End Sub

    Public Sub getSubEntidad(ByRef entidad As AcadEntity, mensaje As String, ByRef p() As Double, Optional ByRef objContenedor As AcadEntity = Nothing)
        Dim TransMatrix(0 To 3, 0 To 3) As Double
        Dim ContextData As Object = Nothing

        DOCUMENTO.Utility.Prompt(mensaje)
        Try
            DOCUMENTO.Utility.GetSubEntity(entidad, p, TransMatrix, ContextData)
            'revisando el objeto al que pertenece el objeto seleccionado
            If Not IsNothing(ContextData(0)) Then
                objContenedor = DOCUMENTO.ObjectIdToObject(ContextData(0))
            End If
        Catch
            'no se selecciono un objeto 
            entidad = Nothing
            p = Nothing
        End Try
    End Sub

    Public Sub PrepararElDiccionarioDeUnaCompuerta()
        'va a asignar a cada pata de una compuerta (no encapsulada) el diccionario
        'con XRECORD siguientes
        'TIPO
        'SENAL
        'Tipo tiene los valores ENTRADA o SALIDA
        'Senal contiene un apuntor al HANDLER del texto que representa la senal

        Dim entidad As AcadEntity = Nothing
        Dim tipo As String
        Dim p(0 To 2) As Double

        appActivateAutoCAD()
        getEntidad(entidad, "Selecciona entrada A")
        If Not IsNothing(entidad) AndAlso entidad.ObjectName = "AcDbLine" Then
            addXdata(entidad, "TIPO", "ENTRADA")
            addXdata(entidad, "NOMBRE", "A")
            addXdata(entidad, "SENAL", "")
        End If
        getEntidad(entidad, "Selecciona entrada B")
        If Not IsNothing(entidad) AndAlso entidad.ObjectName = "AcDbLine" Then
            addXdata(entidad, "TIPO", "ENTRADA")
            addXdata(entidad, "NOMBRE", "B")
            addXdata(entidad, "SENAL", "")
        End If

        getEntidad(entidad, "Selecciona salida ")
        If Not IsNothing(entidad) AndAlso entidad.ObjectName = "AcDbLine" Then
            addXdata(entidad, "TIPO", "SALIDA")
            addXdata(entidad, "SENAL", "")
        End If
    End Sub




    Public Sub asociarSenalSalidaDirecto()
        'asigna un AcDbText a la pata de salida de la compuerta
        'La pata esta indicada como SALIDA

        Dim bloque As AcadBlockReference = Nothing
        Dim entidad As AcadEntity = Nothing
        Dim nombreDeEntrada As String = Nothing
        Dim senal As AcadEntity = Nothing
        Dim tipo As String = Nothing
        Dim p(0 To 2) As Double

        appActivateAutoCAD()
        getSubEntidad(entidad, "Selecciona la salida de una compuerta ", p, bloque)
        If Not IsNothing(entidad) Then
            getXdata(entidad, "TIPO", tipo)
            If tipo = "SALIDA" Then
                'seleccionando una senal par asignar a la salida
                getEntidad(senal, "Selecciona el texto donde se representara la salida ")
                If Not IsNothing(senal) AndAlso senal.ObjectName = "AcDbText" Then
                    'asignando el apuntador a la senal
                    addXdata(entidad, "SENAL", senal.Handle)
                    'actualizando a la compuerta agregando una capa de informacion que nos indique 
                    'quien es la SALIDA
                    addXdata(bloque, "SALIDA", entidad.Handle)
                End If
            End If
        End If
    End Sub

    Public Sub resolverCompuertaSinAtributos()
        'al seleccionar una compuerta la resuelve y muestra el resultado en el texto hacia adonde apunta la SALIDA
        'El bloque AND tiene un nivel de informacion en su directorio donde se indica el handler
        'de las ENTRADAS (A y B) y la SALIDA 
        'El resultado se reflejara en el texto al cual se apunta via la SALIDA
        'Los textos se representan con patrones de 0 y 1


        Dim entidad As AcadEntity = Nothing
        Dim p() As Double = Nothing
        Dim resultado As String
        Dim handEntradaA As String
        Dim handEntradaB As String
        Dim handSalida As String
        Dim eA As AcadEntity
        Dim eB As AcadEntity
        Dim sA As AcadEntity
        Dim handSenalA As String
        Dim handSenalB As String
        Dim handSenalSalida As String
        Dim senalA As AcadEntity
        Dim senalB As AcadEntity
        Dim senalSalida As AcadEntity
        Dim binarioA As String
        Dim binarioB As String

        appActivateAutoCAD()
        getEntidad(entidad, "Selecciona una compuerta")
        If Not IsNothing(entidad) AndAlso entidad.ObjectName = "AcDbBlockReference" Then

            'seleccionando la capa de informacion del bloque que nos indica los handler de sus componentes
            'que representan a las lineas de entrada y salida
            getXdata(entidad, "ENTRADA-A", handEntradaA)
            getXdata(entidad, "ENTRADA-B", handEntradaB)
            getXdata(entidad, "SALIDA", handSalida)

            'accediendo mediante los handlers a los objetos lineas correspondientes
            eA = DOCUMENTO.HandleToObject(handEntradaA)
            eB = DOCUMENTO.HandleToObject(handEntradaB)
            sA = DOCUMENTO.HandleToObject(handSalida)

            'obteniendo de las lineas los handlers a los textos que representan la senal
            getXdata(eA, "SENAL", handSenalA)
            getXdata(eB, "SENAL", handSenalB)
            getXdata(sA, "SENAL", handSenalSalida)

            'accediendo desde su handler a los textos que representan las senales 
            senalA = DOCUMENTO.HandleToObject(handSenalA)
            senalB = DOCUMENTO.HandleToObject(handSenalB)
            senalSalida = DOCUMENTO.HandleToObject(handSenalSalida)

            binarioA = senalA.textstring
            binarioB = senalB.textstring

            igualaTamañoSenal(binarioA, binarioB)
            resultado = ""

            If entidad.Name = "AND" Then
                resultado = OperacionAND(binarioA, binarioB, resultado)

            ElseIf entidad.Name = "OR" Then
                resultado = OperacionOR(binarioA, binarioB, resultado)
            End If

            senalSalida.TextString = resultado
            senalSalida.Update()
        End If
    End Sub

    Public Function OperacionAND(binarioA As String, binarioB As String, resultado As String) As String

        Dim combinacion As String
        For i = 0 To binarioA.Length - 1
            combinacion = binarioA.Substring(i, 1) & binarioB.Substring(i, 1)
            Select Case combinacion
                Case "00", "01", "10"
                    resultado = resultado & "0"
                Case "11"
                    resultado = resultado & "1"
            End Select
        Next
        Return resultado
    End Function

    Public Function OperacionOR(binarioA As String, binarioB As String, resultado As String) As String

        Dim combinacion As String
        For i = 0 To binarioA.Length - 1
            combinacion = binarioA.Substring(i, 1) & binarioB.Substring(i, 1)
            Select Case combinacion
                Case "11", "01", "10"
                    resultado = resultado & "1"
                Case "00"
                    resultado = resultado & "0"
            End Select
        Next
        Return resultado
    End Function

    Private Sub igualaTamañoSenal(ByRef senal1 As String, ByRef senal2 As String)
        'recibe dos secuencia en teoria  del tipo 10010101 y las igual en tamano
        'la forma de igualar es con 0s 
        Dim i As Integer
        Dim dif As Integer
        Dim l1 As Integer
        Dim l2 As Integer

        l1 = senal1.Length
        l2 = senal2.Length
        dif = Math.Abs(l1 - l2)
        If l1 <> l2 Then
            If l1 > l2 Then
                For i = 1 To dif
                    senal2 &= "0"
                Next
            Else
                For i = 1 To dif
                    senal1 &= "0"
                Next
            End If
        End If
    End Sub

    'DICCIONARIOS

    Public Sub addXdata(ByRef entidad As AcadEntity, nameXrecord As String, valor As String)
        'Agrega un Xrecord y un solo valor
        Dim dictASTI As AcadDictionary
        Dim astiXRec As AcadXRecord
        Dim keyCode() As Short  'OBLIGATORIO QUE SEA SHORT
        Dim cptData() As Object 'OBLIGATORIO QUE SEA OBJECT

        ReDim keyCode(0)
        ReDim cptData(0)

        dictASTI = entidad.GetExtensionDictionary 'dictASTI crea un diccionario si el objeto no lo tiene ya
        astiXRec = dictASTI.AddXRecord(nameXrecord.ToUpper.Trim)
        keyCode(0) = 100 : cptData(0) = valor
        astiXRec.SetXRecordData(keyCode, cptData)
    End Sub

    Public Sub getXdata(entidad As AcadEntity, nameXrecord As String, ByRef valor As String)
        Dim astiXRec As AcadXRecord = Nothing
        Dim dictASTI As AcadDictionary
        Dim getKey As Object = Nothing
        Dim getData As Object = Nothing

        valor = Nothing
        dictASTI = entidad.GetExtensionDictionary
        Try
            astiXRec = dictASTI.Item(nameXrecord.ToUpper.Trim)

        Catch ex As System.Runtime.InteropServices.COMException
            'no existe el Xrecord con la entrada nameXrecord por lo cual se causa la excepción
            'Esta forma de manejar una excepción si funciona y debe complementarse al momento de
            'recibir el emnsaje de excepciones que seporduzcan y palomear el nombre de la aplicacion
            'para que nose detenga la ejecución del programa
        End Try
        If Not IsNothing(astiXRec) Then
            astiXRec.GetXRecordData(getKey, getData)
            If Not IsNothing(getData) Then
                valor = getData(0) 'recuperando el valor del XRecord
            End If

        End If


    End Sub


    Public Function selectEntidad(mensaje As String) As AcadEntity
        'permite seleccionar una entidad y si no se selecciona nada regresa nothing
        Dim entidad As AcadEntity
        Dim p() As Double

        Try
            DOCUMENTO.Utility.GetEntity(entidad, p, mensaje)
        Catch ex As Exception

        End Try
        Return entidad
    End Function

End Module