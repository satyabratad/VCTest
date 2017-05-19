Imports B2P.Reports

Public Class DynamicControls

    Public Shared Function LoadControls(ByVal controls As List(Of ReportParameter)) As System.Web.UI.WebControls.Table

        'eventually there will be a Report class with all information includign the list of parameters.
        Dim ReportType As String = "A"   'for testing only, this will be an enum from a report class. R = realtime, A = archive

        Using ControlTable As New System.Web.UI.WebControls.Table()
            Dim TextBoxControl As PeterBlum.DES.Web.WebControls.TextBox
            Dim DropDownControl As DropDownList
            Dim DateControl As PeterBlum.DES.Web.WebControls.DateTextBox
            Dim DateValidator As PeterBlum.DES.Web.WebControls.DataTypeCheckValidator
            Dim RequiredTextValidator As PeterBlum.DES.Web.WebControls.RequiredTextValidator
            Dim IntegerControl As PeterBlum.DES.Web.WebControls.IntegerTextBox
            Dim CurrencyControl As PeterBlum.DES.Web.WebControls.CurrencyTextBox
            Dim BooleanControl As CheckBox
            Dim RegexControl As PeterBlum.DES.Web.WebControls.RegexValidator

            ControlTable.CellSpacing = 10
            Dim TableRow As New TableRow

            'test for no parameters
            If controls.Count > 0 Then
                Dim RowHold As Integer = controls(0).DisplayRow

                For Each ci2 As B2P.Reports.ReportParameter In controls

                    If RowHold <> ci2.DisplayRow Then
                        ControlTable.Rows.Add(TableRow)
                        TableRow = New TableRow
                        RowHold = ci2.DisplayRow
                    End If

                    Dim tc As New TableCell

                    If ci2.DataType <> ReportParameter.DataTypes.Session Then
                        tc.Wrap = False

                        tc.Text = String.Format("{0}&nbsp;&nbsp;", ci2.Label)
                        TableRow.Cells.Add(tc)
                    End If

                    tc = New TableCell
                    tc.Wrap = False

                    Select Case ci2.DataType
                        Case B2P.Reports.ReportParameter.DataTypes.String
                            TextBoxControl = New PeterBlum.DES.Web.WebControls.TextBox
                            TextBoxControl.ID = String.Format("DC_{0}", ci2.ParameterName)
                            TextBoxControl.MaxLength = ci2.MaximumLength
                            tc.Controls.Add(TextBoxControl)
                            If ci2.Required Then
                                RequiredTextValidator = New PeterBlum.DES.Web.WebControls.RequiredTextValidator
                                RequiredTextValidator.ID = String.Format("DCR_{0}", ci2.ParameterName)
                                RequiredTextValidator.ControlIDToEvaluate = TextBoxControl.ID
                                RequiredTextValidator.ErrorMessage = "&nbsp;*"
                                RequiredTextValidator.SummaryErrorMessage = String.Format("{0} is required", ci2.Label)
                                tc.Controls.Add(RequiredTextValidator)
                            End If


                        Case B2P.Reports.ReportParameter.DataTypes.DropDown
                            DropDownControl = New DropDownList
                            DropDownControl.ID = String.Format("DC_{0}", ci2.ParameterName)
                            Dim item As New System.Web.UI.WebControls.ListItem

                            For Each ddi As B2P.Reports.ReportParameter.DropDownItem In ci2.DropDownItems
                                item = New System.Web.UI.WebControls.ListItem
                                item.Text = ddi.Label
                                item.Value = ddi.Value
                                DropDownControl.Items.Add(item)
                            Next

                            tc.Controls.Add(DropDownControl)

                        Case B2P.Reports.ReportParameter.DataTypes.DynamicDropDown
                            DropDownControl = New DropDownList
                            DropDownControl.ID = String.Format("DC_{0}", ci2.ParameterName)
                            'DropDownControl.EnableViewState = False
                            Dim item As New System.Web.UI.WebControls.ListItem

                            For Each ddi As B2P.Reports.ReportParameter.DropDownItem In ci2.DropDownItems
                                item = New System.Web.UI.WebControls.ListItem
                                item.Text = ddi.Label
                                item.Value = ddi.Value
                                DropDownControl.Items.Add(item)
                            Next

                            tc.Controls.Add(DropDownControl)

                        Case B2P.Reports.ReportParameter.DataTypes.DateTime

                            If ReportType = "A" Then
                                DateControl = New PeterBlum.DES.Web.WebControls.DateTextBox
                                DateControl.ID = String.Format("DC_{0}", ci2.ParameterName)
                                DateControl.PopupCalendar.Calendar.MultiMonthColumnCount = 1

                                tc.Controls.Add(DateControl)
                                DateValidator = New PeterBlum.DES.Web.WebControls.DataTypeCheckValidator
                                DateValidator.ID = String.Format("DCV_{0}", ci2.ParameterName)
                                DateValidator.ControlIDToEvaluate = DateControl.ID ' String.Format("DC_{0}", ci2.Name)
                                DateValidator.ErrorMessage = "&nbsp;*"
                                DateValidator.SummaryErrorMessage = String.Format("{0} requires a valid date ", ci2.Label)
                                tc.Controls.Add(DateValidator)
                                If ci2.Required Then
                                    RequiredTextValidator = New PeterBlum.DES.Web.WebControls.RequiredTextValidator
                                    RequiredTextValidator.ID = String.Format("DCR_{0}", ci2.ParameterName)
                                    RequiredTextValidator.ControlIDToEvaluate = DateControl.ID
                                    RequiredTextValidator.ErrorMessage = "&nbsp;*"
                                    RequiredTextValidator.SummaryErrorMessage = String.Format("{0} is required", ci2.Label)
                                    tc.Controls.Add(RequiredTextValidator)
                                End If

                            Else
                                DropDownControl = New DropDownList
                                DropDownControl.ID = String.Format("DC_{0}", ci2.ParameterName)
                                For i As Integer = 0 To 7
                                    DropDownControl.Items.Add(Now.AddDays(-i).ToString("MM/dd/yyyy"))
                                Next
                                tc.Controls.Add(DropDownControl)

                            End If
                        Case B2P.Reports.ReportParameter.DataTypes.Boolean

                            BooleanControl = New CheckBox
                            BooleanControl.ID = String.Format("DC_{0}", ci2.ParameterName)
                            tc.Controls.Add(BooleanControl)

                        Case B2P.Reports.ReportParameter.DataTypes.Decimal
                            CurrencyControl = New PeterBlum.DES.Web.WebControls.CurrencyTextBox
                            CurrencyControl.ID = String.Format("DC_{0}", ci2.ParameterName)
                            CurrencyControl.AllowExtraDecimalDigits = False
                            CurrencyControl.MaxValue = "999999.99"

                            tc.Controls.Add(CurrencyControl)

                            RegexControl = New PeterBlum.DES.Web.WebControls.RegexValidator
                            RegexControl.ID = String.Format("DCX_{0}", ci2.ParameterName)
                            RegexControl.ControlIDToEvaluate = CurrencyControl.ID
                            RegexControl.Expression = "(?n:(^\$?(?!0,?\d)\d{1,3}(?=(?<1>,)|(?<1>))(\k<1>\d{3})*(\.\d\d)?)$)"
                            If ci2.Required = True Then
                                RegexControl.IgnoreBlankText = False
                            End If
                            RegexControl.ErrorMessage = "&nbsp;*"
                            RegexControl.SummaryErrorMessage = String.Format("Amount is invalid", ci2.Label)
                            tc.Controls.Add(RegexControl)



                        Case B2P.Reports.ReportParameter.DataTypes.Integer
                            IntegerControl = New PeterBlum.DES.Web.WebControls.IntegerTextBox
                            IntegerControl.AllowNegatives = False
                            IntegerControl.ShowThousandsSeparator = False
                            IntegerControl.ID = String.Format("DC_{0}", ci2.ParameterName)
                            tc.Controls.Add(IntegerControl)


                    End Select

                    TableRow.Cells.Add(tc)

                Next

                ControlTable.Rows.Add(TableRow)
            Else
                Dim tc As New TableCell
                tc.Wrap = False

                tc.Text = String.Format("{0}&nbsp;&nbsp;", "No additional Parameters")
                TableRow.Cells.Add(tc)
                ControlTable.Rows.Add(TableRow)
            End If

            Return ControlTable

        End Using

    End Function
End Class
