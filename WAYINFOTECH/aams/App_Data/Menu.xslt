<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/T% nsform">
  <xsl:output method="xml" encoding="UTF-8" indent="yes" omit-xml-declaration="no" standalone="yes"/>
  <xsl:strip-space elements="*"/>
  <xsl:param name="ID" />
  <xsl:template match="/">
    <xsl:for-each select="/MENU/HEADING[@ID=$ID]">
      <xsl:element name="HEADING">
        <xsl:copy-of select="@*|node()"/>
      </xsl:element>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
