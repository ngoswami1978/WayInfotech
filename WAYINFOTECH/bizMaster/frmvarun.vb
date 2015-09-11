Imports System.Xml

Public Class frmvarun

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New XmlDocument
        Dim objclass As New bizMaster.bzEmployee
        obj.LoadXml("<MS_UPDATEEMPLOYEESUPERVISORY_INPUT><EmployeeID>28</EmployeeID><Supervisory DomainID='1' DomainName='Yahoo' /></MS_UPDATEEMPLOYEESUPERVISORY_INPUT>")
        obj = objclass.SaveSupervisoryRights(obj)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim obj As New bizMaster.bzDesignation
        Dim objXml As New Xml.XmlDocument
        Dim strScevalue As String = ""
        strScevalue = "<MS_UPDATEDESIGNATION_INPUT><DESIGNATION DesignationID='' Designation='ERERER'> <SECURITY SecurityOptionID='2' Value='2' />  <SECURITY SecurityOptionID='3' Value='2' />   <SECURITY SecurityOptionID='4' Value='2' />   <SECURITY SecurityOptionID='5' Value='2' />   <SECURITY SecurityOptionID='6' Value='2' />   </DESIGNATION>  </MS_UPDATEDESIGNATION_INPUT>"
        objXml.LoadXml(strScevalue)

        objXml = obj.Update(objXml)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim obj As New bizMaster.bzEmployee
        Dim objXml As New Xml.XmlDocument
        Dim strScevalue As String = ""
        strScevalue = "<MS_DEPT_EMPLOYEE_INPUT><DEPT>2</DEPT></MS_DEPT_EMPLOYEE_INPUT>"
        objXml.LoadXml(strScevalue)

        objXml = obj.GetEmployeeListDeptWise(objXml)

    End Sub
End Class