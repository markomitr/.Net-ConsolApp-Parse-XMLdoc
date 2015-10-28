Imports System.IO
Imports System.Xml

Module Module1
    Private Class Osigurenik
        Public _embg As String
        Public _ezbo As String
        Public _ime As String
        Public _prezime As String
        Public _adrsesa As String
        Public _telefon As String
        Public _registerski As String
        Public _datum_raganje As String
        Public _zdrastven_broj As String
        Public Overrides Function ToString() As String
            Dim separator As String = " "
            Return Me._ime & separator & Me._prezime & separator & Me._embg & separator & Me._registerski
        End Function
        Public Overloads Function ToString(ByVal separator As String) As String
            Return Me._ime & separator & Me._prezime & separator & Me._adrsesa & separator & Me._telefon & separator & Me._embg & separator & Me._datum_raganje & separator & Me._zdrastven_broj & separator & Me._registerski
        End Function
        Public Shared Function NaslovKoloni() As String
            Dim separator As String = ";"
            Return "IME" & separator & "PREZIME" & separator & "ADRESA" & separator & "TELEFON" & separator & "EMBG" & separator & "DATUM" & separator & "Z_BROJ" & separator & "REGISTARSKI"
        End Function
    End Class
    Public Enum TipPodatok
        EMBG
        EZBO
        IME
        PREZIME
        ADRESA
        TELEFON
        REGISTERSKI
        Datum_Raganje
        Z_BROJ
    End Enum
    Dim lstOsigur As List(Of Osigurenik)
    Sub Main()
        Dim imeFajl As String = "501778.xml"
        ProcitajXML(imeFajl)
        IspecatiNaEkran()
        ZapisiVoFile()
        Console.Read()
    End Sub
    Private Function ProcitajXML(ByVal Pateka As String) As Boolean
        Try

            Dim xmlDoc As XmlDocument = New XmlDocument() ' // Create an XML document object
            xmlDoc.Load(Pateka) ' // Load the XML document from the specified file

            Dim embg As XmlNodeList = xmlDoc.GetElementsByTagName("EMBG")
            Dim ezbo As XmlNodeList = xmlDoc.GetElementsByTagName("EZBO")
            Dim ime As XmlNodeList = xmlDoc.GetElementsByTagName("IME")
            Dim prezime As XmlNodeList = xmlDoc.GetElementsByTagName("PREZIME")
            Dim adresa As XmlNodeList = xmlDoc.GetElementsByTagName("ADRESA")
            Dim telefon As XmlNodeList = xmlDoc.GetElementsByTagName("TELEFON")
            Dim registerski As XmlNodeList = xmlDoc.GetElementsByTagName("REGISTERSKI")
            Dim datum_raganje As XmlNodeList = xmlDoc.GetElementsByTagName("DATUM_RAGANJE")
            Dim z_broj As XmlNodeList = xmlDoc.GetElementsByTagName("Z_BROJ")

            NapolniObjOdNodeList(embg, TipPodatok.EMBG, lstOsigur)
            NapolniObjOdNodeList(ezbo, TipPodatok.EZBO, lstOsigur)
            NapolniObjOdNodeList(ime, TipPodatok.IME, lstOsigur)
            NapolniObjOdNodeList(prezime, TipPodatok.PREZIME, lstOsigur)
            NapolniObjOdNodeList(adresa, TipPodatok.ADRESA, lstOsigur)
            NapolniObjOdNodeList(telefon, TipPodatok.TELEFON, lstOsigur)
            NapolniObjOdNodeList(registerski, TipPodatok.registerski, lstOsigur)
            NapolniObjOdNodeList(datum_raganje, TipPodatok.Datum_Raganje, lstOsigur)
            NapolniObjOdNodeList(z_broj, TipPodatok.Z_BROJ, lstOsigur)
            Return True
        Catch ex As Exception
            MsgBox("Greska:" & ex.Message)
            Return False
        End Try

    End Function
    Private Function NapolniObjOdNodeList(ByVal nodeList As XmlNodeList, ByVal tip As TipPodatok, ByRef lstOsig As List(Of Osigurenik)) As Boolean
        If lstOsig Is Nothing Then
            lstOsig = New List(Of Osigurenik)
        End If

        If Not nodeList Is Nothing Then
            For index = 0 To nodeList.Count - 1
                If lstOsig.Count < index + 1 Then
                    lstOsig.Add(New Osigurenik)
                End If
                Dim podatok As String = nodeList(index).InnerText.ToString
                Select Case tip
                    Case TipPodatok.IME
                        lstOsig(index)._ime = podatok
                    Case TipPodatok.PREZIME
                        lstOsig(index)._prezime = podatok
                    Case TipPodatok.ADRESA
                        lstOsig(index)._adrsesa = podatok
                    Case TipPodatok.Datum_Raganje
                        lstOsig(index)._datum_raganje = podatok
                    Case TipPodatok.EMBG
                        lstOsig(index)._embg = podatok
                    Case TipPodatok.EZBO
                        lstOsig(index)._ezbo = podatok
                    Case TipPodatok.REGISTERSKI
                        lstOsig(index)._registerski = podatok
                    Case TipPodatok.TELEFON
                        lstOsig(index)._telefon = podatok
                    Case TipPodatok.Z_BROJ
                        lstOsig(index)._zdrastven_broj = podatok
                End Select
            Next
            Return True
        End If
        Return False
    End Function
    Private Sub IspecatiNaEkran()
        If Not lstOsigur Is Nothing Then

            For index = 0 To lstOsigur.Count - 1
                Console.WriteLine((index + 1).ToString & ") " & lstOsigur(index).ToString())
            Next

        End If
    End Sub
    Private Sub ZapisiVoFile()
        Using writer As StreamWriter = New StreamWriter("FondOsigurenici.txt")
            If Not lstOsigur Is Nothing Then
                writer.WriteLine(Osigurenik.NaslovKoloni)
                For index = 0 To lstOsigur.Count - 1
                    If lstOsigur(index)._registerski.StartsWith("519") Then
                        writer.WriteLine(lstOsigur(index).ToString(";"))
                    End If
                Next
                Console.WriteLine("Zapisav FAJL")
            End If
        End Using

    End Sub
End Module
