Public Class Form2

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    
        ''view
        ' objInputXml.LoadXml("<MS_VIEWDEPARTMENT_INPUT><DepartmentID></DepartmentID></MS_VIEWDEPARTMENT_INPUT>")
        'Agency view
        Dim obj As New bizMaster.bzDepartment
        Dim strDept_INPUT As String = "<MS_VIEWDEPARTMENT_INPUT><DepartmentID></DepartmentID></MS_VIEWDEPARTMENT_INPUT>"
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml(strDept_INPUT)
        objXml = obj.View(objXml)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Update/Insert
        Dim obj As New bizMaster.bzDepartment
        Dim objXml As New Xml.XmlDocument
        objXml.LoadXml("<MS_UPDATEDEPARTMENT_INPUT><DEPARTMENT Action='I' DepartmentID='' Department_Name='ABCDEFG' ManagerID='37'/></MS_UPDATEDEPARTMENT_INPUT>")
        objXml = obj.Update(objXml)


        '<MS_UPDATEDEPARTMENT_INPUT>
        '<DEPARTMENT Action='' DepartmentID='' Department_Name='' ManagerID='' />
        '</MS_UPDATEDEPARTMENT_INPUT>


    End Sub
End Class