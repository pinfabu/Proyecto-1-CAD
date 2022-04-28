Module codigo

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
        conjunto = conjunto_vacio(DOCUMENTO, indef)
        If Not IsNothing(conjunto) Then
            conjunto.Select(AutocadSelect.acSelectionSetAll)
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
        Dim nombreConexion As String = Nothing
        Dim senal As AcadEntity = Nothing
        Dim idSalida As AcadEntity = Nothing
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
                getXdata(entidad, "CONEXION", nombreConexion)
                'seleccionando un ID de salida
                getEntidad(idSalida, "selecciona la salida que irá asociada a esta entrada si es que se asocia a una")
                'Verificando la existencia de la salida, en caso de no seleccionar nada, no se escribe nada como tal
                If Not IsNothing(idSalida) AndAlso idSalida.ObjectName = "AcDbText" Then
                    addXdata(entidad, "CONEXION", idSalida.Handle)
                    addXdata(bloque, "CONEXION-" & nombreConexion, entidad.Handle)
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
            addXdata(entidad, "CONEXION", "")
        End If
        getEntidad(entidad, "Selecciona entrada B")
        If Not IsNothing(entidad) AndAlso entidad.ObjectName = "AcDbLine" Then
            addXdata(entidad, "TIPO", "ENTRADA")
            addXdata(entidad, "NOMBRE", "B")
            addXdata(entidad, "SENAL", "")
            addXdata(entidad, "CONEXION", "")
        End If

        getEntidad(entidad, "Selecciona salida ")
        If Not IsNothing(entidad) AndAlso entidad.ObjectName = "AcDbLine" Then
            addXdata(entidad, "TIPO", "SALIDA")
            addXdata(entidad, "SENAL", "")
            addXdata(entidad, "ID", "")
        End If
    End Sub




    Public Sub asociarSenalSalidaDirecto()
        'asigna un AcDbText a la pata de salida de la compuerta
        'La pata esta indicada como SALIDA

        Dim bloque As AcadBlockReference = Nothing
        Dim entidad As AcadEntity = Nothing
        Dim nombreDeSalida As String = Nothing
        Dim senal As AcadEntity = Nothing
        Dim id As AcadEntity = Nothing
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

                getEntidad(id, "Selecciona el id asociado a la salida")
                If Not IsNothing(id) AndAlso id.ObjectName = "AcDbText" Then
                    addXdata(entidad, "ID", id.Handle)
                    addXdata(bloque, "ID_SALIDA", entidad.Handle)
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

            If InStr(entidad.Name, "AND") Then
                resultado = OperacionAND(binarioA, binarioB, resultado)

            ElseIf InStr(entidad.Name, "OR") Then
                resultado = OperacionOR(binarioA, binarioB, resultado)
            End If

            senalSalida.TextString = resultado
            senalSalida.Update()
        End If
    End Sub

    Public Function IdentificaCompuerta(entidad As Object)
        If InStr(entidad.Name, "AND") Then
            Return "AND"
        ElseIf InStr(entidad.Name, "OR") Then
            Return "OR"
        End If
        Return ""
    End Function

    Public Function getSalidaCorrespondiente(id As String) As List(Of Object)
        Dim Bloques As AcadSelectionSet
        Dim salidaHandle As String
        Dim salidaHandleFinal As String
        Dim sA As AcadEntity
        Dim salidaHandleFinalText As AcadEntity
        Dim idFinal As String
        Dim res As List(Of Object)

        Bloques = getConjunto("ANALISIS")
        For Each elemento In Bloques
            If elemento.EntityName = "AcDbBlockReference" Then
                getXdata(elemento, "SALIDA", salidaHandle)
                sA = DOCUMENTO.HandleToObject(salidaHandle)
                getXdata(sA, "ID", salidaHandleFinal)
                salidaHandleFinalText = DOCUMENTO.HandleToObject(salidaHandleFinal)
                idFinal = salidaHandleFinalText.TextString

                If idFinal = id Then
                    res.Add(idFinal)
                    res.Add(elemento)
                    Return res
                End If
            End If

        Next
        Return Nothing
    End Function

    Public Sub ResuelveCompuertas(compuerta As Object, sA As AcadEntity)
        Dim resultado As String
        Dim idA As String
        Dim idB As String
        Dim idAObject As AcadEntity
        Dim idBObject As AcadEntity
        Dim handEntradaA As String
        Dim handEntradaB As String
        Dim handSalida As String
        Dim eA As AcadEntity
        Dim eB As AcadEntity
        Dim handSenalA As String
        Dim handSenalB As String
        Dim handSenalSalida As String
        Dim senalA As AcadEntity
        Dim senalB As AcadEntity
        Dim senalSalida As AcadEntity
        Dim binarioA As String
        Dim binarioB As String

        getXdata(compuerta, "ENTRADA-A", handEntradaA)
        getXdata(compuerta, "ENTRADA-B", handEntradaB)

        eA = DOCUMENTO.HandleToObject(handEntradaA)
        eB = DOCUMENTO.HandleToObject(handEntradaB)


        getXdata(eA, "CONEXION", idA)
        getXdata(eB, "CONEXION", idB)

        idAObject = DOCUMENTO.HandleToObject(idA)
        idBObject = DOCUMENTO.HandleToObject(idB)

        If idAObject.TextString = "" AndAlso idBObject.TextString = "" Then

            getXdata(eA, "SENAL", handSenalA)
            getXdata(eB, "SENAL", handSenalB)
            getXdata(sA, "SENAL", handSenalSalida)

            senalA = DOCUMENTO.HandleToObject(handSenalA)
            senalB = DOCUMENTO.HandleToObject(handSenalB)
            senalSalida = DOCUMENTO.HandleToObject(handSenalSalida)

            binarioA = senalA.textstring
            binarioB = senalB.textstring
            igualaTamañoSenal(binarioA, binarioB)

            If IdentificaCompuerta(compuerta) = "AND" Then
                resultado = OperacionAND(binarioA, binarioB, resultado)
            ElseIf IdentificaCompuerta(compuerta) = "OR" Then
                resultado = OperacionOR(binarioA, binarioB, resultado)
            End If
            senalSalida.TextString = resultado
            senalSalida.Update()

        ElseIf idAObject.TextString <> "" AndAlso idBObject.TextString = "" Then
            Dim listaRes As List(Of Object)
            Dim handleSalida As AcadEntity
            Dim compuertaRecu As Object

            listaRes = getSalidaCorrespondiente(idAObject.TextString)


        ElseIf idAObject.TextString = "" AndAlso idBObject.TextString <> "" Then

        ElseIf idAObject.TextString <> "" AndAlso idBObject.TextString <> "" Then

        End If
    End Sub

    Public Sub resolverCircuito()

        Dim Bloques As AcadSelectionSet
        Dim eA As AcadEntity
        Dim eB As AcadEntity
        Dim sA As AcadEntity
        Dim senalA As AcadEntity
        Dim senalB As AcadEntity
        Dim senalSalida As AcadEntity
        Dim handleFinalText As AcadEntity
        Dim handleFinal As String
        Dim idFinal As String
        Dim binA As String
        Dim binB As String
        Dim entradaAHandle As String
        Dim entradaBHandle As String
        Dim salidaHandle As String
        Dim Id As String
        Dim operador1Handle As AcadEntity
        Dim operador2Handle As AcadEntity
        Dim resultado As String
        Dim salidaHandleTexto As String
        Dim TextoSalida As AcadEntity

        Bloques = getConjunto("CIRCUITO")

        For Each elemento In Bloques

            If elemento.EntityName = "AcDbBlockReference" Then
                salidaHandle = ""
                entradaAHandle = ""
                entradaBHandle = ""
                handleFinal = ""

                getXdata(elemento, "SALIDA", salidaHandle)
                getXdata(elemento, "ENTRADA-A", entradaAHandle)
                getXdata(elemento, "ENTRADA-B", entradaBHandle)
                eA = DOCUMENTO.HandleToObject(entradaAHandle)
                eB = DOCUMENTO.HandleToObject(entradaBHandle)
                sA = DOCUMENTO.HandleToObject(salidaHandle)

                handleFinal = Nothing

                getXdata(sA, "ID", handleFinal)
                getXdata(sA, "SENAL", salidaHandleTexto)
                TextoSalida = DOCUMENTO.HandleToObject(salidaHandleTexto)

                handleFinalText = DOCUMENTO.HandleToObject(handleFinal)
                idFinal = handleFinalText.textstring
                If idFinal = "Final" Then
                    Debug.Print("Entre 2")
                    Dim Aux As String
                    Dim Aux2 As String
                    Dim handleSenal As String
                    Dim handleSenal2 As String
                    Dim AuxFinal As AcadEntity
                    Dim AuxFinal2 As AcadEntity
                    Dim senalSalidaAux As String
                    Dim senalSalidaAuxFinal As AcadEntity
                    Dim senalSalidaAuxFinalText As String
                    Dim compuerta As String

                    getXdata(eA, "CONEXION", Aux)
                    getXdata(eB, "CONEXION", Aux2)
                    getXdata(eA, "SENAL", handleSenal)
                    getXdata(eB, "SENAL", handleSenal2)

                    senalA = DOCUMENTO.HandleToObject(handleSenal)
                    senalB = DOCUMENTO.HandleToObject(handleSenal2)

                    If Aux <> "" AndAlso Aux2 = "" Then
                        AuxFinal = DOCUMENTO.HandleToObject(Aux)
                    ElseIf Aux = "" AndAlso Aux2 <> "" Then
                        AuxFinal2 = DOCUMENTO.HandleToObject(Aux2)
                    Else
                        AuxFinal = DOCUMENTO.HandleToObject(Aux)
                        AuxFinal2 = DOCUMENTO.HandleToObject(Aux2)
                    End If



                    binA = senalA.textstring
                    binB = senalB.textstring

                    If Aux <> "" AndAlso Aux2 <> "" AndAlso binA = "???" AndAlso binB = "???" Then
                        Debug.Print("Entré prmer caso")
                    ElseIf Aux = "" AndAlso Aux2 <> "" AndAlso binA <> "" AndAlso binB = "???" Then
                        Debug.Print("Entré segundo caso")
                    ElseIf Aux <> "" AndAlso Aux2 = "" AndAlso binA = "???" AndAlso binB <> "" AndAlso binB <> "???" Then
                        Dim listaRes As List(Of Object)
                        Dim compuertaRes As Object
                        listaRes = getSalidaCorrespondiente(AuxFinal.TextString)
                        operador1Handle = listaRes(0)
                        compuertaRes = listaRes(1)

                        getXdata(operador1Handle, "SENAL", senalSalidaAux)
                        senalSalidaAuxFinal = DOCUMENTO.HandleToObject(senalSalidaAux)
                        senalSalidaAuxFinalText = senalSalidaAuxFinal.TextString

                        If senalSalidaAuxFinalText = "???" Then
                            ResuelveCompuertas(compuertaRes, operador1Handle)
                        Else

                            binA = senalSalidaAuxFinalText
                            igualaTamañoSenal(binA, binB)
                            compuerta = IdentificaCompuerta(elemento)

                            If compuerta = "AND" Then
                                resultado = OperacionAND(binA, binB, resultado)
                            ElseIf compuerta = "OR" Then
                                resultado = OperacionOR(binA, binB, resultado)
                            End If
                            TextoSalida.TextString = resultado
                            TextoSalida.Update()
                        End If

                        resolverCompuertaSinAtributos()

                    ElseIf Aux = "" AndAlso Aux2 = "" AndAlso binA <> "" AndAlso binB <> "" AndAlso binA <> "???" AndAlso binB <> "???" Then
                        resolverCompuertaSinAtributos()
                    End If
                End If


            End If
        Next


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

    Public Sub reiniciaSalidas()
        Dim Bloques As AcadSelectionSet
        Dim sA As AcadEntity
        Dim senalSalida As AcadEntity = Nothing
        Dim salidaHandle As String
        Dim handSenalSalida As String

        Bloques = getConjunto("ALL")
        For Each elemento In Bloques
            If elemento.EntityName = "AcDbBlockReference" Then

                getXdata(elemento, "SALIDA", salidaHandle)
                sA = DOCUMENTO.HandleToObject(salidaHandle)
                getXdata(sA, "SENAL", handSenalSalida)
                senalSalida = DOCUMENTO.HandleToObject(handSenalSalida)

                senalSalida.TextString = "???"
                senalSalida.Update()

            End If
            senalSalida = Nothing
            sA = Nothing
            salidaHandle = ""
            handSenalSalida = ""
        Next
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