<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
		<html>
			<body>
				<table border = "1">
					<tr>
						<th>Name</th>
						<th>Department</th>
						<th>Branch</th>
						<th>Speciality</th>
						<th>Time of the begining</th>
						<th>Time of the ending</th>
						<th>Type</th>
					</tr>
					<xsl:for-each select ="workgroup/group">
						<tr>
							<td>
								<xsl:copy-of select="name"/>
							</td>
							<td>
								<xsl:copy-of select="department"/>
							</td>
							<td>
								<xsl:copy-of select="branch"/>
							</td>
							<td>
								<xsl:copy-of select="speciality"/>
							</td>
							<td>
								<xsl:copy-of select="begining"/>
							</td>
							<td>
								<xsl:copy-of select="ending"/>
							</td>
							<td>
								<xsl:copy-of select="type"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
    </xsl:template>
</xsl:stylesheet>
