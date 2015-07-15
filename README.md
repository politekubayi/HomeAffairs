# HomeAffairs
This project generates and validate an ID Number.

1) Please change the below config in webconfig file of HomeAffairs(which is the webAPI project) as per your machine variables:

	<add key="LogFileDirectory" value="P:\homeAffairs\Log_File.txt" />

2)  Please change the below config in webconfig file of HomeAffairsFrontEnd(which is the website project) as per your machine variables:

    <add key="getIDNumberAPILocation" value="api/idnumber" />
    <add key="getIDNumberAPIHost" value="http://localhost:51338/" />
    <add key="GetControlDigitAPILocation" value="api/IDNumber?idNumber=" />
    <add key="GetControlDigitAPIHost" value="http://localhost:51338/" />
	<add key="LogFileDirectory" value="P:\homeAffairs\Log_File.txt" /> 
