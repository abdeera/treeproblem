Public Class Form2
  
    Public nb As Integer = Form3.nb2 * Form3.nb2
    Public ligne(4, 2 * Form3.nb2 - 1) As Integer
    Public ligne2(4, 2 * Form3.nb2 - 1) As Integer
    Public joueur(nb) As PictureBox
    Public ord(nb) As PictureBox
    Public tabjou(nb) As Integer
    Public tabord(nb) As Integer
    Public phase As Integer = 2 ' 2=1+1 la phase est normalement la premiere on en ajoute 1 par necessité
    Public fini As Integer = 0

    Private Sub rafraichir(ByVal a As Integer, ByVal b As Integer)


        Dim com1, com2, com3, com4, x As Integer
        com1 = 0
        com2 = 0
        com3 = 0
        com4 = 0


        If tabjou(a * Form3.nb2 + b) = -1 Then
            MsgBox("operation interdite", MsgBoxStyle.Critical, "erreur")
            Return
        End If




        For x = 0 To nb - 1
            If (x \ Form3.nb2 + x Mod Form3.nb2 = a + b) Then
                If tabjou(x) = -1 Then
                    com1 = com1 + 1
                End If

            ElseIf (x Mod Form3.nb2 - x \ Form3.nb2 = b - a) Then
                If tabjou(x) = -1 Then
                    com2 = com2 + 1
                End If
            ElseIf x Mod Form3.nb2 = b Then
                If tabjou(x) = -1 Then
                    com3 = com3 + 1
                End If
            ElseIf x \ Form3.nb2 = a Then
                If tabjou(x) = -1 Then
                    com4 = com4 + 1
                End If

                'tabjou(x) += 1

            End If







        Next

        If com1 >= Form3.nb_p Or com2 >= Form3.nb_p Or com3 >= Form3.nb_p Or com4 >= Form3.nb_p Then
            MsgBox("operation interdite", MsgBoxStyle.Critical, "erreur")
            Return
        Else
            tabjou(a * Form3.nb2 + b) = -1
            joueur(a * Form3.nb2 + b).Image = My.Resources.Sans_titre2


            'ref2()
            For x = 0 To nb - 1
                If com1 = Form3.nb_p - 1 And (x \ Form3.nb2 + x Mod Form3.nb2 = a + b) And tabjou(x) <> -1 Then
                    tabjou(x) = -2
                ElseIf com2 = Form3.nb_p - 1 And (x Mod Form3.nb2 - x \ Form3.nb2 = b - a) And tabjou(x) <> -1 Then
                    tabjou(x) = -2

                ElseIf com3 = Form3.nb_p - 1 And x Mod Form3.nb2 = b And tabjou(x) <> -1 Then
                    tabjou(x) = -2
                ElseIf com4 = Form3.nb_p - 1 And x \ Form3.nb2 = a And tabjou(x) <> -1 Then
                    tabjou(x) = -2
                End If
            Next
            ordinateur()
            If CheckBox1.Checked = True Then
                CheckBox1.Checked = False
                CheckBox1.Checked = True
            Else

                CheckBox1.Checked = True
                CheckBox1.Checked = False
            End If

        End If




    End Sub
    Sub ordinateur()
        Dim x, y, i, exist As Integer
        Dim table(nb) As Integer
        Dim choix As New ArrayList  '-----------------------l'ensemble des noeuds condidats prenant h1 comme heuristique
        Dim choix2 As New ArrayList '-----------------------l'ensemble des noeuds condidats prenant h2 comme heuristique
        Dim choix3 As New ArrayList '-----------------------l'ensemble des noeuds condidats prenant h3 comme heuristique
        Dim compt As Integer '----------------------------l'heuristique h2 = cases restées non tracées d'une intersection de moindre de lignes 
        Dim chois As Integer '----------------------------un nombre qui reprente le moins d'intersections des lignes qu'on a deja tombé sur
        choix.Insert(0, -1)
        choix.Insert(1, -1)
        choix.Insert(2, 100000)
        choix2.Insert(0, -1)
        choix2.Insert(1, -1)
        choix3.Insert(0, -1)
        choix3.Insert(1, -1)
        Dim compt2 As Integer


        ''''''ref2(tabord)

        'MsgBox(ligne(1, 1, 1))
br:
        exist = 0
        For i = 0 To nb - 1
            If (tabord(i) > 0) Then
                exist = 1
                '_________________________________________//table = une dupliqu de la table tabord pour simuler notre coup joué_________________________________________

                For x = 0 To nb - 1
                    table(x) = tabord(x)
                Next
                '_________________________________________//ligne2 = une dupliqu de la table ligne associé a la table dupliqué___________________________________________

                For x = 1 To 4
                    For y = 0 To 2 * Form3.nb2 - 2
                        ligne2(x, y) = ligne(x, y)
                    Next
                Next
                '______________________________________________________________________________________________________________________________________________________


                '____tracer(table, i)___________________//augmenter le poid de tte les lignes passant par la cellule___________________________________________________


                tracer(table, ligne2, phase, i)
                '_______________________________________________________________________________________________________________________________
                'ref2(table)


                compt = 0                   '--------heuristique h2
                compt2 = 0                  '--------heuristique h3
                chois = 10000



                For x = 0 To nb - 1
                    If table(x) > 0 Then '-----------scruter les cases valides >0 et compter l'heuristique du coup joué
                        compt2 += 1

                        If (table(x) <= chois) Then
                            If (table(x) < chois) Then
                                chois = table(x)
                                compt = 0
                            End If
                            compt += 1

                        End If



                    End If
                Next
                Dim compteur As Integer = 0  '-----------------------------------------------------heuristique h3
br2:
                Dim lignee As New ArrayList
                Dim compt3 As Integer = 0, compt4 As Integer = 0


                For x = 0 To nb - 1

                    Dim ii, jj, unique As Integer
                    unique = 0
                    compt4 = 0
                    jj = -1
                    'For ii = 0 To vect.Count
                    'If vect(ii).indexof(x) <> -1 Then
                    'jj = vect(ii).indexof(x)
                    '
                    'End If
                    'Next
                    If table(x) > 0 Then


                        If ligne2(1, x Mod Form3.nb2) = phase - 1 Then
                            compt4 = 0
                            Dim v(2 * Form3.nb2) As Integer
                            Dim ind As Integer
                            For ind = 0 To 2 * Form3.nb2
                                v(ind) = -1
                            Next
                            ind = 0
                            For ii = 0 To Form3.nb2 - 1
                                If table(ii * Form3.nb2 + x Mod Form3.nb2) > 0 Then '----------------C a d la case est disponible pr le prochain coup
                                    compt4 += 1   ' -------------------------------------------------augmenter le poid de la ligne
                                    v(ind) = ii * Form3.nb2 + x Mod Form3.nb2
                                    ind = ind + 1
                                End If
                            Next
                            If compt4 >= compt3 Then

                                If compt4 > compt3 Then
                                    lignee.Clear()
                                    lignee.Add(1 & (x Mod Form3.nb2).ToString)
                                    lignee.Add(1)
                                    lignee.Add(x Mod Form3.nb2)
                                    lignee.Add(v)
                                Else
                                    If lignee.IndexOf(1 & (x Mod Form3.nb2).ToString) = -1 Then
                                        lignee.Add(1 & (x Mod Form3.nb2).ToString)
                                        lignee.Add(1)
                                        lignee.Add(x Mod Form3.nb2)
                                        lignee.Add(v)
                                    End If
                                End If
                                compt3 = compt4
                            End If
                            If ind > 1 Then
                                unique += 1
                            End If
                        End If

                        If ligne2(2, x \ Form3.nb2) = phase - 1 Then
                            Dim ind As Integer = 0
                            Dim v(2 * Form3.nb2) As Integer
                            For ind = 0 To 2 * Form3.nb2
                                v(ind) = -1
                            Next
                            compt4 = 0
                            ind = 0
                            For ii = 0 To Form3.nb2 - 1
                                If table((x \ Form3.nb2) * Form3.nb2 + ii) > 0 Then   '-----------(x \ Form3.nb2) * Form3.nb2 + ii ca veut dire les deux indices (x \ Form3.nb2)  et ii si le tableau soit bidimensiennel mais tant quil est uni-dim on calcul (x Div Form3.nb2)=lindice de lignes ,on * fois Form3.nb2 pour transformé en mode uni-dim et on ajoutra apres ii colones
                                    compt4 += 1 ' -------------------------------------------------augmenter le poid de la ligne
                                    v(ind) = (x \ Form3.nb2) * Form3.nb2 + ii
                                    ind = ind + 1
                                End If
                            Next
                            If compt4 >= compt3 Then

                                If compt4 > compt3 Then
                                    lignee.Clear()
                                    lignee.Add(2 & (x \ Form3.nb2).ToString)
                                    lignee.Add(2)
                                    lignee.Add(x \ Form3.nb2)
                                    lignee.Add(v)
                                Else
                                    If lignee.IndexOf(2 & (x \ Form3.nb2).ToString) = -1 Then
                                        lignee.Add(2 & (x \ Form3.nb2).ToString)
                                        lignee.Add(2)
                                        lignee.Add(x \ Form3.nb2)
                                        lignee.Add(v)
                                    End If

                                End If
                                compt3 = compt4
                            End If
                            If ind > 1 Then
                                unique += 1
                            End If
                        End If

                        If ligne2(3, x Mod Form3.nb2 + x \ Form3.nb2) = phase - 1 Then
                            Dim j As Integer
                            Dim v(2 * Form3.nb2) As Integer
                            compt4 = 0
                            Dim ind As Integer = 0
                            For ind = 0 To 2 * Form3.nb2
                                v(ind) = -1
                            Next
                            ind = 0
                            If (x \ Form3.nb2 + x Mod Form3.nb2 >= Form3.nb2 - 1) Then    '----------------------- les diagonales bas
                                j = 0
                                While (x \ Form3.nb2 + x Mod Form3.nb2 - Form3.nb2 + 1 + j <= Form3.nb2 - 1)
                                    If table((x \ Form3.nb2 + x Mod Form3.nb2 - Form3.nb2 + 1 + j) * Form3.nb2 + Form3.nb2 - 1 - j) > 0 Then '------parcours de diagonale
                                        compt4 += 1 ' -------------------------------------------------augmenter le poid de la ligne
                                        v(ind) = (x \ Form3.nb2 + x Mod Form3.nb2 - Form3.nb2 + 1 + j) * Form3.nb2 + Form3.nb2 - 1 - j
                                        ind = ind + 1
                                    End If
                                    j += 1
                                End While

                            Else                                        '----------------------- les diagonales la motié haute
                                j = 0
                                While (x \ Form3.nb2 + x Mod Form3.nb2 - j >= 0)
                                    If table((j) * Form3.nb2 + x \ Form3.nb2 + x Mod Form3.nb2 - j) > 0 Then '------parcours de diagonale indices (j , equation de la diagonale - j) -> j incremente jsq Form3.nb2-1
                                        compt4 += 1 ' -------------------------------------------------augmenter le poid de la ligne
                                        v(ind) = (j) * Form3.nb2 + x \ Form3.nb2 + x Mod Form3.nb2 - j
                                        ind = ind + 1
                                    End If
                                    j += 1
                                End While

                            End If
                            If compt4 >= compt3 Then

                                If compt4 > compt3 Then
                                    lignee.Clear()
                                    lignee.Add(3 & (x \ Form3.nb2 + x Mod Form3.nb2).ToString)
                                    lignee.Add(3)
                                    lignee.Add(x \ Form3.nb2 + x Mod Form3.nb2)
                                    lignee.Add(v)
                                Else
                                    If lignee.IndexOf(3 & (x \ Form3.nb2 + x Mod Form3.nb2).ToString) = -1 Then
                                        lignee.Add(3 & (x \ Form3.nb2 + x Mod Form3.nb2).ToString)
                                        lignee.Add(3)
                                        lignee.Add(x \ Form3.nb2 + x Mod Form3.nb2)
                                        lignee.Add(v)
                                    End If
                                End If
                                compt3 = compt4
                            End If
                            If ind > 1 Then
                                unique += 1
                            End If
                        End If

                        If ligne2(4, x \ Form3.nb2 - x Mod Form3.nb2 + Form3.nb2 - 1) = phase - 1 Then
                            Dim j As Integer
                            Dim ind As Integer = 0
                            Dim v(2 * Form3.nb2) As Integer
                            For ind = 0 To 2 * Form3.nb2
                                v(ind) = -1
                            Next
                            ind = 0
                            compt4 = 0
                            If (x \ Form3.nb2 - x Mod Form3.nb2 >= 0) Then    '----------------------- les diagonales bas
                                j = 0
                                While (x \ Form3.nb2 - x Mod Form3.nb2 + j <= Form3.nb2 - 1)
                                    If table((x \ Form3.nb2 - x Mod Form3.nb2 + j) * Form3.nb2 + j) > 0 Then '------ parcours de diagonale
                                        compt4 += 1 ' -------------------------------------------------augmenter le poid de la ligne
                                        v(ind) = (x \ Form3.nb2 - x Mod Form3.nb2 + j) * Form3.nb2 + j
                                        ind = ind + 1
                                    End If
                                    j += 1

                                End While

                            Else                                        '----------------------- les diagonales la motié haute
                                j = 0
                                While (j + x Mod Form3.nb2 - x \ Form3.nb2 <= Form3.nb2 - 1)
                                    If table(j * Form3.nb2 + (x Mod Form3.nb2 - x \ Form3.nb2 + j)) > 0 Then '---- parcours de diagonale indices (j , equation de la diagonale - j) -> j incremente jsq Form3.nb2-1
                                        compt4 += 1 ' -------------------------------------------------augmenter le poid de la ligne
                                        v(ind) = j * Form3.nb2 + (x Mod Form3.nb2 - x \ Form3.nb2 + j)
                                        ind = ind + 1
                                    End If
                                    j += 1
                                End While

                            End If
                            If compt4 >= compt3 Then

                                If compt4 > compt3 Then
                                    lignee.Clear()
                                    lignee.Add(3 & (x \ Form3.nb2 + x Mod Form3.nb2).ToString)
                                    lignee.Add(3)
                                    lignee.Add(x \ Form3.nb2 + x Mod Form3.nb2)
                                    lignee.Add(v)
                                Else
                                    If lignee.IndexOf((x \ Form3.nb2 + x Mod Form3.nb2).ToString) = -1 Then
                                        lignee.Add(3 & (x \ Form3.nb2 + x Mod Form3.nb2).ToString)
                                        lignee.Add(3)
                                        lignee.Add(x \ Form3.nb2 + x Mod Form3.nb2)
                                        lignee.Add(v)
                                    End If
                                End If
                                compt3 = compt4
                            End If
                            If ind > 1 Then
                                unique += 1
                            End If
                        End If

                        If unique = 1 Then
                            ii = 0
                            While lignee(lignee.Count - 1)(ii) <> -1
                                table(lignee(lignee.Count - 1)(ii)) = -phase
                                ii += 1
                            End While
                            ligne2(lignee(lignee.Count - 3), lignee(lignee.Count - 2)) = phase
                            compteur += 1
                            GoTo br2
                        End If



                    Else

                        Dim compt33, compt44 As Integer

                        'If compt3 > 5 Then
                        ' vect.AddRange(lignee)
                        ' lignee.Clear()
                        'ElseIf compt3 <= 3 Then
                        '
                        'If () then
                        ' End If
                        'End If

                    End If


                Next

                If compt3 <> 0 Then
                    Dim a As Integer = 0
                    Dim j As Integer = 0
                    Dim countt As Integer = 0
                    While a < lignee.Count
                        countt = 0
                        j = 0
                        While lignee(a + 3)(j) <> -1
                            If table(lignee(a + 3)(j)) > 0 Then
                                countt += 1
                            End If
                            j += 1
                        End While
                        If countt = compt3 Then
                            j = 0
                            While lignee(a + 3)(j) <> -1
                                table(lignee(a + 3)(j)) = -phase
                                j += 1
                            End While
                            ligne2(lignee(a + 1), lignee(a + 2)) = phase
                            compteur += 1
                        End If
                        a = a + 4
                    End While
                    GoTo br2
                End If


                If choix(2) >= chois Then

                    If choix(2) = chois Then
                        If choix(1) < compt Then  '------------ajouter a la liste des noeuds avec un poid estimé
                            choix.Clear()
                            choix.Add(i)
                            choix.Add(compt)
                            choix.Add(chois)
                        ElseIf (choix(1) = compt) Then
                            choix.Add(i)
                            choix.Add(compt)
                            choix.Add(chois)
                        End If
                    Else
                        choix.Clear()
                        choix.Add(i)
                        choix.Add(compt)
                        choix.Add(chois)
                    End If
                End If


                If choix2(1) < compteur Then  '------------ajouter a la liste des noeuds avec un poid estimé
                    choix2.Clear()
                    choix2.Add(i)
                    choix2.Add(compteur)

                ElseIf (choix2(1) = compteur) Then
                    choix2.Add(i)
                    choix2.Add(compteur)
                End If
                If choix3(1) < compt2 Then  '------------ajouter a la liste des noeuds avec un poid estimé
                    choix3.Clear()
                    choix3.Add(i)
                    choix3.Add(compt2)

                ElseIf (choix3(1) = compt2) Then
                    choix3.Add(i)
                    choix3.Add(compt2)
                End If
            End If




        Next
        

br3:

        If exist = 1 Then
            Dim choice1, choice2, choice3 As Integer
            choice1 = -1
            choice2 = -1
            choice3 = -1
            Dim l, m, k As Integer
            l = 0
            m = 0
            k = 0
            While l <= choix.Count - 3
                m = 0
                While m <= choix3.Count - 2
                    k = 0
                    While k <= choix2.Count - 2
                        If choix3(m) = choix2(k) = choix(l) Then
                            choice1 = choix2(k)
                            GoTo nb
                        End If
                        k += 2
                    End While
                    If choix3(m) = choix(l) Then
                        choice2 = choix3(m)
                        GoTo nb
                    End If
                    m += 2
                End While
                l += 3
            End While

            k = 0
            l = 0
            m = 0
            If choice1 = choice2 = -1 Then
                k = 0
                While k <= choix2.Count - 2
                    m = 0
                    While m <= choix3.Count - 2
                        If choix3(m) = choix2(k) Then
                            choice3 = choix2(k)
                            GoTo nb
                        End If
                        m += 2
                    End While
                    k += 2
                End While
            End If
nb:
            If choice1 = -1 Then
                If choice2 = -1 Then
                    If choice3 = -1 Then
                        choice1 = choix(0)
                    Else
                        choice1 = choice3
                    End If

                Else
                    choice1 = choice2
                End If

            End If
            tracer(tabord, ligne, phase, choice1)

            'GoTo br

        ElseIf phase < Form3.nb_p + 1 Then

            phase += 1

            For x = 0 To nb - 1 '--------------------------------------------------------passer a la phase suivante et tourner les cases ts >0
                tabord(x) = -tabord(x)
            Next
            GoTo br
        Else
            compt = 0
            chois = 0
            For i = 0 To nb - 1
                If tabord(i) = 0 Then
                    compt += 1
                End If
            Next
            For i = 0 To nb - 1
                If tabjou(i) = -1 Then
                    chois += 1
                End If
            Next
            MsgBox("jeu terminé votre score = " & chois & " score de la machine = " & compt)
            fini = 1
            Exit Sub

        End If

        '''''ref2(tabord)
        For x = 0 To nb - 1
            If tabord(x) = 0 Then
                ord(x).Image = My.Resources.Sans_titre2
            End If
        Next

    End Sub




    Private Sub ref(ByVal a As Integer, ByVal b As Integer)

        If tabjou(a * Form3.nb2 + b) >= Form3.nb_p Or tabjou(a * Form3.nb2 + b) < 0 Then
            MsgBox("operation interdite", MsgBoxStyle.Critical, "erreur")
            Return

        Else
            tabjou(a * Form3.nb2 + b) = Form3.nb_p + 1
            joueur(a * Form3.nb2 + b).Image = My.Resources.Sans_titre2
        End If


        Dim x As Integer

        If tabjou(x) > Form3.nb_p Then
            joueur(x).Image = My.Resources.Sans_titre2
        Else
            joueur(x).Image = My.Resources.vide
        End If



        If tabord(x) > Form3.nb_p Then
            ord(x).Image = My.Resources.Sans_titre2
        Else
            ord(x).Image = My.Resources.vide
        End If

    End Sub
    Private Sub ref2(ByVal tabl() As Integer)
        Dim aa As String = ""

        Dim x As Integer

        For x = 0 To nb - 1
            If tabl(x) < 0 Then
                aa = aa & tabl(x).ToString("00") & vbTab
            Else
                aa = aa & "+" & tabl(x).ToString("00") & vbTab
            End If

            If ((x + 1) Mod Form3.nb2) = 0 Then
                aa = aa & vbCrLf
            End If
        Next

        MsgBox(aa)
        '   aa = ""
        ' For x = 1 To 4                               '--------------------------------initialiser ls lignes par une seul ligne qui croise les cases horisentals, verticals, les 2 diagonales
        'For y = 0 To 2 * Form3.nb2 - 1
        'ligne(x, y) = 1
        ' Next
        ' Next


    End Sub

    Private Sub Form2_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Form3.Enabled = True
    End Sub

    Public Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       

       

        Dim i, x, y As Integer

        For x = 3 To 4                               '--------------------------------initialiser ls lignes par une seul ligne qui croise les cases horisentals, verticals, les 2 diagonales
            For y = 1 To 2 * Form3.nb2 - 3
                ligne(x, y) = 1
            Next
        Next
        For x = 1 To 2                               '--------------------------------initialiser ls lignes par une seul ligne qui croise les cases horisentals, verticals, les 2 diagonales
            For y = 0 To 2 * Form3.nb2 - 2
                ligne(x, y) = 1
            Next
        Next

        For x = 0 To nb - 1                          '--------------------------------initialiser la table de jeu par 1 pour chq case // 0 represente un pion 
            tabord(x) = 1
        Next


        For i = 0 To nb - 1

            Dim T_Label As PictureBox
            T_Label = New PictureBox
            T_Label.Name = "Label_" & Format(i, "000")
            T_Label.Tag = i
            'AddHandler T_Label.DoubleClick, AddressOf DoSomething
            AddHandler T_Label.Click, AddressOf clickk
            'T_Label.Width = 50
            'T_Label.Height = 50
            T_Label.Size = New Size(30, 30)
            'Dim b As System.Drawing.Point
            'b.Y = (i \ 3) * 58 + 26
            'b.X = (i Mod 3) * 58
            T_Label.Location = New Point((i Mod Form3.nb2) * 38 + 10, (i \ Form3.nb2) * 38 + 50)
            Me.Controls.Add(T_Label)
            'T_Label.Show()

        Next
        For i = 0 To nb - 1
            joueur(i) = DirectCast(Me.Controls("Label_" & Format(i, "000")), PictureBox)
            joueur(i).Image = My.Resources.vide
            tabjou(i) = 0
        Next

        Dim ii As Integer
        For ii = 0 To nb - 1

            Dim T_Label2 As PictureBox
            T_Label2 = New PictureBox
            T_Label2.Name = "Labell_" & Format(ii, "000")
            T_Label2.Tag = i
            'AddHandler T_Label.DoubleClick, AddressOf DoSomething
            'AddHandler T_Label.Click, AddressOf DoSomething
            'T_Label.Width = 50
            'T_Label.Height = 50
            T_Label2.Size = New Size(30, 30)
            'Dim b As System.Drawing.Point
            'b.Y = (i \ 3) * 58 + 26
            'b.X = (i Mod 3) * 58
            T_Label2.Location = New Point((ii Mod Form3.nb2) * 38 + (Form3.nb2 + 1) * 38 + 10, (ii \ Form3.nb2) * 38 + 50)
            Me.Controls.Add(T_Label2)
            'T_Label.Show()

        Next
        For ii = 0 To nb - 1
            ord(ii) = DirectCast(Me.Controls("Labell_" & Format(ii, "000")), PictureBox)
            ord(ii).Image = My.Resources.vide

        Next
        CheckBox1.Left = 10
        CheckBox1.Top = 20


        '''''ref2(tabjou)


    End Sub

    Sub clickk(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' si joueur a cliqué sue 1- sa grille 2- case vide
        If fini = 0 Then



            With CType(sender, PictureBox)
                'MessageBox.Show("chaine: " & .Name)
                Dim nmb As Integer = System.Decimal.Parse(.Name.Substring(.Name.Length - 3, 3))
                'MessageBox.Show("index: " & nmb & " " & nmb \ Form3.nb2 & " " & nmb Mod Form3.nb2)
                rafraichir(nmb \ Form3.nb2, nmb Mod Form3.nb2)
            End With



        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Dim x As Integer
        Dim compt = 0
        If CheckBox1.Checked Then
            For x = 0 To nb - 1
                If tabjou(x) = -2 Then

                    joueur(x).Image = My.Resources.vide21
                End If

                If tabjou(x) = -2 Or tabjou(x) = -1 Then
                    compt = compt + 1
                End If

            Next

        Else
            For x = 0 To nb - 1
                If tabjou(x) = -2 Then

                    joueur(x).Image = My.Resources.vide
                End If
                If tabjou(x) = -2 Or tabjou(x) = -1 Then
                    compt = compt + 1
                End If


            Next
        End If


        If compt = nb And fini = 0 Then
            While (fini = 0)
                ordinateur()
            End While

            'MsgBox("fini")
        End If
    End Sub

    Private Sub tracer(ByVal table() As Integer, ByVal lign(,) As Integer, ByVal phase As Integer, ByVal i As Integer)
        Dim j As Integer, y As Integer = phase - 1
        While y > 0                            '---------------------------------detecter les lignes qui croisent cette colones et laugmenter a un niveau
            If lign(1, i Mod Form3.nb2) = y Then '-------------------------------si la ligne est verticale
                lign(1, i Mod Form3.nb2) = y + 1 '-------------------------------augmenter -> ligne de poid y+1 

                If y = phase - 1 Then         '----------------------------------si le poid de la ligne devi1 egale au nivo de la phase on marque tte cette ligne <0 pour ne pa la prcourir prochainement   
                    For j = 0 To Form3.nb2 - 1
                        If table(j * Form3.nb2 + i Mod Form3.nb2) < 0 Then '----------------C a d la case est deja intercedé par une ligne dun poid = phase
                            table(j * Form3.nb2 + i Mod Form3.nb2) -= 1   ' ----------------si le cas dune intersection avec une autre ligne de mm niveau on DECREMENTE
                        ElseIf table(j * Form3.nb2 + i Mod Form3.nb2) <> 0 Then
                            table(j * Form3.nb2 + i Mod Form3.nb2) = -phase '---------------sinon on marque (-phase)
                        End If
                    Next
                ElseIf y = phase - 2 Then  '------------------------------------sinon si le poid de la ligne de vient inferieur exact au nivo de la phase on incremente ttes les cases >0 a 1 car C une untersection avec une autre ligne de meme niveau

                    For j = 0 To Form3.nb2 - 1
                        If table(j * Form3.nb2 + i Mod Form3.nb2) > 0 Then '----------------C a d la case est valide 
                            table(j * Form3.nb2 + i Mod Form3.nb2) += 1   '-----------------si le cas dune intersection avec une autre ligne de mm niveau on INCREMENTE

                        End If
                    Next

                End If
            End If

            If lign(2, i \ Form3.nb2) = y Then '-------------------------------2 le cas dune ligne horisentale -> mm chose
                lign(2, i \ Form3.nb2) = y + 1

                If y = phase - 1 Then
                    For j = 0 To Form3.nb2 - 1
                        If table((i \ Form3.nb2) * Form3.nb2 + j) < 0 Then   '-----------(i \ Form3.nb2) * Form3.nb2 + j ca veut dire les deux indices (i \ Form3.nb2)  et j si le tableau soit bidimensiennel mais tant quil est uni-dim on calcul (i Div Form3.nb2)=lindice de lignes ,on * fois Form3.nb2 pour transformé en mode uni-dim et on ajoutra apres j colones
                            table((i \ Form3.nb2) * Form3.nb2 + j) -= 1      '-----------la ligne croise une autre
                        ElseIf table((i \ Form3.nb2) * Form3.nb2 + j) <> 0 Then
                            table((i \ Form3.nb2) * Form3.nb2 + j) = -phase  '-----------pas de ligne de meme poid a croiser
                        End If
                    Next
                ElseIf y = phase - 2 Then  '------------------------------------sinon si le poid de la ligne devient inferieur exact au nivo de la phase on incremente ttes les cases >0 a 1 car C une untersection avec une autre ligne de meme niveau

                    For j = 0 To Form3.nb2 - 1
                        If table(j * Form3.nb2 + i Mod Form3.nb2) > 0 Then '----------------C a d la case est valide 
                            table(j * Form3.nb2 + i Mod Form3.nb2) += 1   ' ----------------si le cas dune intersection avec une autre ligne de mm niveau on DECREMENTE

                        End If
                    Next


                End If
            End If

            If lign(3, i \ Form3.nb2 + i Mod Form3.nb2) = y Then '-------------------------------3 le cas dune diagonale de droite a gauche -> mm chose i+j est lequation de tte les points que la diagonale droite-haut a gauche-bas croise

                lign(3, i \ Form3.nb2 + i Mod Form3.nb2) = y + 1

                If y = phase - 1 Then

                    If (i \ Form3.nb2 + i Mod Form3.nb2 >= Form3.nb2 - 1) Then    '----------------------- les diagonales bas
                        j = 0
                        While (i \ Form3.nb2 + i Mod Form3.nb2 - Form3.nb2 + 1 + j <= Form3.nb2 - 1)
                            If table((i \ Form3.nb2 + i Mod Form3.nb2 - Form3.nb2 + 1 + j) * Form3.nb2 + Form3.nb2 - 1 - j) < 0 Then '------parcours de diagonale
                                table((i \ Form3.nb2 + i Mod Form3.nb2 - Form3.nb2 + 1 + j) * Form3.nb2 + Form3.nb2 - 1 - j) -= 1
                            ElseIf table((i \ Form3.nb2 + i Mod Form3.nb2 - Form3.nb2 + 1 + j) * Form3.nb2 + Form3.nb2 - 1 - j) <> 0 Then
                                table((i \ Form3.nb2 + i Mod Form3.nb2 - Form3.nb2 + 1 + j) * Form3.nb2 + Form3.nb2 - 1 - j) = -phase
                            End If
                            j += 1
                        End While

                    Else                                        '----------------------- les diagonales la motié haute
                        j = 0
                        While (i \ Form3.nb2 + i Mod Form3.nb2 - j >= 0)
                            If table((j) * Form3.nb2 + i \ Form3.nb2 + i Mod Form3.nb2 - j) < 0 Then '------parcours de diagonale indices (j , equation de la diagonale - j) -> j incremente jsq Form3.nb2-1
                                table((j) * Form3.nb2 + i \ Form3.nb2 + i Mod Form3.nb2 - j) -= 1
                            ElseIf table((j) * Form3.nb2 + i \ Form3.nb2 + i Mod Form3.nb2 - j) <> 0 Then
                                table((j) * Form3.nb2 + i \ Form3.nb2 + i Mod Form3.nb2 - j) = -phase
                            End If
                            j += 1
                        End While

                    End If

                ElseIf y = phase - 2 Then           '------------------------------------sinon si le poid de la ligne devient inferieur exact au nivo de la phase on incremente ttes les cases >0 a 1 car C une untersection avec une autre ligne de meme niveau
                    If (i \ Form3.nb2 + i Mod Form3.nb2 >= Form3.nb2 - 1) Then    '------------------------les diagonales bas
                        j = 0
                        While (i \ Form3.nb2 + i Mod Form3.nb2 - Form3.nb2 + 1 + j <= Form3.nb2 - 1)
                            If table((i \ Form3.nb2 + i Mod Form3.nb2 - Form3.nb2 + 1 + j) * Form3.nb2 + Form3.nb2 - 1 - j) > 0 Then '----------------C a d la case est valide 
                                table((i \ Form3.nb2 + i Mod Form3.nb2 - Form3.nb2 + 1 + j) * Form3.nb2 + Form3.nb2 - 1 - j) += 1   ' ----------------si le cas dune intersection avec une autre ligne de mm niveau on DECREMENTE

                            End If
                            j += 1
                        End While
                    Else '-------------------------------------------------------------- les diagonales la motié haute
                        j = 0
                        While (i \ Form3.nb2 + i Mod Form3.nb2 - j >= 0)

                            If table((j) * Form3.nb2 + i \ Form3.nb2 + i Mod Form3.nb2 - j) > 0 Then '----------------C a d la case est valide 
                                table((j) * Form3.nb2 + i \ Form3.nb2 + i Mod Form3.nb2 - j) += 1   ' ----------------si le cas dune intersection avec une autre ligne de mm niveau on DECREMENTE

                            End If
                            j += 1
                        End While
                    End If
                End If
            End If

            If lign(4, i \ Form3.nb2 - i Mod Form3.nb2 + Form3.nb2 - 1) = y Then '-------------------------------4 le cas dune diagonale de gauche-haut vers droite-bas -> mm chose i-j+Form3.nb2-1 est lequation de tte les points que la diagonale gauche-haut vers droite-bas croise
                lign(4, i \ Form3.nb2 - i Mod Form3.nb2 + Form3.nb2 - 1) = y + 1

                If y = phase - 1 Then
                    If (i \ Form3.nb2 - i Mod Form3.nb2 >= 0) Then    '----------------------- les diagonales bas
                        j = 0
                        While (i \ Form3.nb2 - i Mod Form3.nb2 + j <= Form3.nb2 - 1)
                            If table((i \ Form3.nb2 - i Mod Form3.nb2 + j) * Form3.nb2 + j) < 0 Then '------ parcours de diagonale
                                table((i \ Form3.nb2 - i Mod Form3.nb2 + j) * Form3.nb2 + j) -= 1
                            ElseIf table((i \ Form3.nb2 - i Mod Form3.nb2 + j) * Form3.nb2 + j) <> 0 Then
                                table((i \ Form3.nb2 - i Mod Form3.nb2 + j) * Form3.nb2 + j) = -phase
                            End If
                            j += 1
                        End While

                    Else                                        '----------------------- les diagonales la motié haute
                        j = 0
                        While (j + i Mod Form3.nb2 - i \ Form3.nb2 <= Form3.nb2 - 1)
                            If table(j * Form3.nb2 + (i Mod Form3.nb2 - i \ Form3.nb2 + j)) < 0 Then '---- parcours de diagonale indices (j , equation de la diagonale - j) -> j incremente jsq Form3.nb2-1
                                table(j * Form3.nb2 + (i Mod Form3.nb2 - i \ Form3.nb2 + j)) -= 1
                            ElseIf table(j * Form3.nb2 + (i Mod Form3.nb2 - i \ Form3.nb2 + j)) <> 0 Then
                                table(j * Form3.nb2 + (i Mod Form3.nb2 - i \ Form3.nb2 + j)) = -phase
                            End If
                            j += 1
                        End While

                    End If

                ElseIf y = phase - 2 Then           '------------------------------------sinon si le poid de la ligne devient inferieur exact au nivo de la phase on incremente ttes les cases >0 a 1 car C une untersection avec une autre ligne de meme niveau
                    If (i \ Form3.nb2 - i Mod Form3.nb2 >= 0) Then    '----------------------- les diagonales bas
                        j = 0
                        While (i \ Form3.nb2 - i Mod Form3.nb2 + j <= Form3.nb2 - 1)
                            If table((i \ Form3.nb2 - i Mod Form3.nb2 + j) * Form3.nb2 + j) > 0 Then '----------------C a d la case est valide 
                                table((i \ Form3.nb2 - i Mod Form3.nb2 + j) * Form3.nb2 + j) += 1   ' ----------------si le cas dune intersection avec une autre ligne de mm niveau on DECREMENTE

                            End If

                            j += 1
                        End While
                    Else '-------------------------------------------------------------- les diagonales la motié haute
                        j = 0
                        While (j + i Mod Form3.nb2 - i \ Form3.nb2 <= Form3.nb2 - 1)

                            If table(j * Form3.nb2 + (i Mod Form3.nb2 - i \ Form3.nb2 + j)) > 0 Then '----------------C a d la case est valide 
                                table(j * Form3.nb2 + (i Mod Form3.nb2 - i \ Form3.nb2 + j)) += 1   ' ----------------si le cas dune intersection avec une autre ligne de mm niveau on DECREMENTE

                            End If
                            j += 1
                        End While
                    End If

                End If

            End If
            y -= 1
        End While

        table(i) = 0   '-----------------------------------------------------la case i on met un pion symbolisé par un zero


    End Sub
    Sub afficher(ByVal tabl() As Integer)
        Dim mot As String = "", x As Integer
        For x = 0 To nb - 1
            mot = mot & tabl(x)
        Next
    End Sub



    
End Class