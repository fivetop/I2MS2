<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<?xml-stylesheet type="text/xsl" href="is.xsl" ?>
<!DOCTYPE msi [
   <!ELEMENT msi   (summary,table*)>
   <!ATTLIST msi version    CDATA #REQUIRED>
   <!ATTLIST msi xmlns:dt   CDATA #IMPLIED
                 codepage   CDATA #IMPLIED
                 compression (MSZIP|LZX|none) "LZX">
   
   <!ELEMENT summary       (codepage?,title?,subject?,author?,keywords?,comments?,
                            template,lastauthor?,revnumber,lastprinted?,
                            createdtm?,lastsavedtm?,pagecount,wordcount,
                            charcount?,appname?,security?)>
                            
   <!ELEMENT codepage      (#PCDATA)>
   <!ELEMENT title         (#PCDATA)>
   <!ELEMENT subject       (#PCDATA)>
   <!ELEMENT author        (#PCDATA)>
   <!ELEMENT keywords      (#PCDATA)>
   <!ELEMENT comments      (#PCDATA)>
   <!ELEMENT template      (#PCDATA)>
   <!ELEMENT lastauthor    (#PCDATA)>
   <!ELEMENT revnumber     (#PCDATA)>
   <!ELEMENT lastprinted   (#PCDATA)>
   <!ELEMENT createdtm     (#PCDATA)>
   <!ELEMENT lastsavedtm   (#PCDATA)>
   <!ELEMENT pagecount     (#PCDATA)>
   <!ELEMENT wordcount     (#PCDATA)>
   <!ELEMENT charcount     (#PCDATA)>
   <!ELEMENT appname       (#PCDATA)>
   <!ELEMENT security      (#PCDATA)>                            
                                
   <!ELEMENT table         (col+,row*)>
   <!ATTLIST table
                name        CDATA #REQUIRED>

   <!ELEMENT col           (#PCDATA)>
   <!ATTLIST col
                 key       (yes|no) #IMPLIED
                 def       CDATA #IMPLIED>
                 
   <!ELEMENT row            (td+)>
   
   <!ELEMENT td             (#PCDATA)>
   <!ATTLIST td
                 href       CDATA #IMPLIED
                 dt:dt     (string|bin.base64) #IMPLIED
                 md5        CDATA #IMPLIED>
]>
<msi version="2.0" xmlns:dt="urn:schemas-microsoft-com:datatypes" codepage="65001">
	
	<summary>
		<codepage>1252</codepage>
		<title>Installation Database</title>
		<subject>SimpleWin IEMS</subject>
		<author>##ID_STRING2##</author>
		<keywords>Installer,MSI,Database</keywords>
		<comments>Contact:  Your local administrator</comments>
		<template>Intel;1033</template>
		<lastauthor>Administrator</lastauthor>
		<revnumber>{CF3B0FEE-FF2A-40E1-B147-B9E3E869B254}</revnumber>
		<lastprinted/>
		<createdtm>06/21/1999 22:00</createdtm>
		<lastsavedtm>07/15/2000 01:50</lastsavedtm>
		<pagecount>200</pagecount>
		<wordcount>0</wordcount>
		<charcount/>
		<appname>InstallShield Express</appname>
		<security>1</security>
	</summary>
	
	<table name="ActionText">
		<col key="yes" def="s72">Action</col>
		<col def="L64">Description</col>
		<col def="L128">Template</col>
		<row><td>Advertise</td><td>##IDS_ACTIONTEXT_Advertising##</td><td/></row>
		<row><td>AllocateRegistrySpace</td><td>##IDS_ACTIONTEXT_AllocatingRegistry##</td><td>##IDS_ACTIONTEXT_FreeSpace##</td></row>
		<row><td>AppSearch</td><td>##IDS_ACTIONTEXT_SearchInstalled##</td><td>##IDS_ACTIONTEXT_PropertySignature##</td></row>
		<row><td>BindImage</td><td>##IDS_ACTIONTEXT_BindingExes##</td><td>##IDS_ACTIONTEXT_File##</td></row>
		<row><td>CCPSearch</td><td>##IDS_ACTIONTEXT_UnregisterModules##</td><td/></row>
		<row><td>CostFinalize</td><td>##IDS_ACTIONTEXT_ComputingSpace3##</td><td/></row>
		<row><td>CostInitialize</td><td>##IDS_ACTIONTEXT_ComputingSpace##</td><td/></row>
		<row><td>CreateFolders</td><td>##IDS_ACTIONTEXT_CreatingFolders##</td><td>##IDS_ACTIONTEXT_Folder##</td></row>
		<row><td>CreateShortcuts</td><td>##IDS_ACTIONTEXT_CreatingShortcuts##</td><td>##IDS_ACTIONTEXT_Shortcut##</td></row>
		<row><td>DeleteServices</td><td>##IDS_ACTIONTEXT_DeletingServices##</td><td>##IDS_ACTIONTEXT_Service##</td></row>
		<row><td>DuplicateFiles</td><td>##IDS_ACTIONTEXT_CreatingDuplicate##</td><td>##IDS_ACTIONTEXT_FileDirectorySize##</td></row>
		<row><td>FileCost</td><td>##IDS_ACTIONTEXT_ComputingSpace2##</td><td/></row>
		<row><td>FindRelatedProducts</td><td>##IDS_ACTIONTEXT_SearchForRelated##</td><td>##IDS_ACTIONTEXT_FoundApp##</td></row>
		<row><td>GenerateScript</td><td>##IDS_ACTIONTEXT_GeneratingScript##</td><td>##IDS_ACTIONTEXT_1##</td></row>
		<row><td>ISLockPermissionsCost</td><td>##IDS_ACTIONTEXT_ISLockPermissionsCost##</td><td/></row>
		<row><td>ISLockPermissionsInstall</td><td>##IDS_ACTIONTEXT_ISLockPermissionsInstall##</td><td/></row>
		<row><td>InstallAdminPackage</td><td>##IDS_ACTIONTEXT_CopyingNetworkFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize##</td></row>
		<row><td>InstallFiles</td><td>##IDS_ACTIONTEXT_CopyingNewFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize2##</td></row>
		<row><td>InstallODBC</td><td>##IDS_ACTIONTEXT_InstallODBC##</td><td/></row>
		<row><td>InstallSFPCatalogFile</td><td>##IDS_ACTIONTEXT_InstallingSystemCatalog##</td><td>##IDS_ACTIONTEXT_FileDependencies##</td></row>
		<row><td>InstallServices</td><td>##IDS_ACTIONTEXT_InstallServices##</td><td>##IDS_ACTIONTEXT_Service2##</td></row>
		<row><td>InstallValidate</td><td>##IDS_ACTIONTEXT_Validating##</td><td/></row>
		<row><td>LaunchConditions</td><td>##IDS_ACTIONTEXT_EvaluateLaunchConditions##</td><td/></row>
		<row><td>MigrateFeatureStates</td><td>##IDS_ACTIONTEXT_MigratingFeatureStates##</td><td>##IDS_ACTIONTEXT_Application##</td></row>
		<row><td>MoveFiles</td><td>##IDS_ACTIONTEXT_MovingFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize3##</td></row>
		<row><td>PatchFiles</td><td>##IDS_ACTIONTEXT_PatchingFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize4##</td></row>
		<row><td>ProcessComponents</td><td>##IDS_ACTIONTEXT_UpdateComponentRegistration##</td><td/></row>
		<row><td>PublishComponents</td><td>##IDS_ACTIONTEXT_PublishingQualifiedComponents##</td><td>##IDS_ACTIONTEXT_ComponentIDQualifier##</td></row>
		<row><td>PublishFeatures</td><td>##IDS_ACTIONTEXT_PublishProductFeatures##</td><td>##IDS_ACTIONTEXT_FeatureColon##</td></row>
		<row><td>PublishProduct</td><td>##IDS_ACTIONTEXT_PublishProductInfo##</td><td/></row>
		<row><td>RMCCPSearch</td><td>##IDS_ACTIONTEXT_SearchingQualifyingProducts##</td><td/></row>
		<row><td>RegisterClassInfo</td><td>##IDS_ACTIONTEXT_RegisterClassServer##</td><td>##IDS_ACTIONTEXT_ClassId##</td></row>
		<row><td>RegisterComPlus</td><td>##IDS_ACTIONTEXT_RegisteringComPlus##</td><td>##IDS_ACTIONTEXT_AppIdAppTypeRSN##</td></row>
		<row><td>RegisterExtensionInfo</td><td>##IDS_ACTIONTEXT_RegisterExtensionServers##</td><td>##IDS_ACTIONTEXT_Extension2##</td></row>
		<row><td>RegisterFonts</td><td>##IDS_ACTIONTEXT_RegisterFonts##</td><td>##IDS_ACTIONTEXT_Font##</td></row>
		<row><td>RegisterMIMEInfo</td><td>##IDS_ACTIONTEXT_RegisterMimeInfo##</td><td>##IDS_ACTIONTEXT_ContentTypeExtension##</td></row>
		<row><td>RegisterProduct</td><td>##IDS_ACTIONTEXT_RegisteringProduct##</td><td>##IDS_ACTIONTEXT_1b##</td></row>
		<row><td>RegisterProgIdInfo</td><td>##IDS_ACTIONTEXT_RegisteringProgIdentifiers##</td><td>##IDS_ACTIONTEXT_ProgID2##</td></row>
		<row><td>RegisterTypeLibraries</td><td>##IDS_ACTIONTEXT_RegisterTypeLibs##</td><td>##IDS_ACTIONTEXT_LibId##</td></row>
		<row><td>RegisterUser</td><td>##IDS_ACTIONTEXT_RegUser##</td><td>##IDS_ACTIONTEXT_1c##</td></row>
		<row><td>RemoveDuplicateFiles</td><td>##IDS_ACTIONTEXT_RemovingDuplicates##</td><td>##IDS_ACTIONTEXT_FileDir##</td></row>
		<row><td>RemoveEnvironmentStrings</td><td>##IDS_ACTIONTEXT_UpdateEnvironmentStrings##</td><td>##IDS_ACTIONTEXT_NameValueAction2##</td></row>
		<row><td>RemoveExistingProducts</td><td>##IDS_ACTIONTEXT_RemoveApps##</td><td>##IDS_ACTIONTEXT_AppCommandLine##</td></row>
		<row><td>RemoveFiles</td><td>##IDS_ACTIONTEXT_RemovingFiles##</td><td>##IDS_ACTIONTEXT_FileDir2##</td></row>
		<row><td>RemoveFolders</td><td>##IDS_ACTIONTEXT_RemovingFolders##</td><td>##IDS_ACTIONTEXT_Folder1##</td></row>
		<row><td>RemoveIniValues</td><td>##IDS_ACTIONTEXT_RemovingIni##</td><td>##IDS_ACTIONTEXT_FileSectionKeyValue##</td></row>
		<row><td>RemoveODBC</td><td>##IDS_ACTIONTEXT_RemovingODBC##</td><td/></row>
		<row><td>RemoveRegistryValues</td><td>##IDS_ACTIONTEXT_RemovingRegistry##</td><td>##IDS_ACTIONTEXT_KeyName##</td></row>
		<row><td>RemoveShortcuts</td><td>##IDS_ACTIONTEXT_RemovingShortcuts##</td><td>##IDS_ACTIONTEXT_Shortcut1##</td></row>
		<row><td>Rollback</td><td>##IDS_ACTIONTEXT_RollingBack##</td><td>##IDS_ACTIONTEXT_1d##</td></row>
		<row><td>RollbackCleanup</td><td>##IDS_ACTIONTEXT_RemovingBackup##</td><td>##IDS_ACTIONTEXT_File2##</td></row>
		<row><td>SelfRegModules</td><td>##IDS_ACTIONTEXT_RegisteringModules##</td><td>##IDS_ACTIONTEXT_FileFolder##</td></row>
		<row><td>SelfUnregModules</td><td>##IDS_ACTIONTEXT_UnregisterModules##</td><td>##IDS_ACTIONTEXT_FileFolder2##</td></row>
		<row><td>SetODBCFolders</td><td>##IDS_ACTIONTEXT_InitializeODBCDirs##</td><td/></row>
		<row><td>StartServices</td><td>##IDS_ACTIONTEXT_StartingServices##</td><td>##IDS_ACTIONTEXT_Service3##</td></row>
		<row><td>StopServices</td><td>##IDS_ACTIONTEXT_StoppingServices##</td><td>##IDS_ACTIONTEXT_Service4##</td></row>
		<row><td>UnmoveFiles</td><td>##IDS_ACTIONTEXT_RemovingMoved##</td><td>##IDS_ACTIONTEXT_FileDir3##</td></row>
		<row><td>UnpublishComponents</td><td>##IDS_ACTIONTEXT_UnpublishQualified##</td><td>##IDS_ACTIONTEXT_ComponentIdQualifier2##</td></row>
		<row><td>UnpublishFeatures</td><td>##IDS_ACTIONTEXT_UnpublishProductFeatures##</td><td>##IDS_ACTIONTEXT_Feature##</td></row>
		<row><td>UnpublishProduct</td><td>##IDS_ACTIONTEXT_UnpublishingProductInfo##</td><td/></row>
		<row><td>UnregisterClassInfo</td><td>##IDS_ACTIONTEXT_UnregisterClassServers##</td><td>##IDS_ACTIONTEXT_ClsID##</td></row>
		<row><td>UnregisterComPlus</td><td>##IDS_ACTIONTEXT_UnregisteringComPlus##</td><td>##IDS_ACTIONTEXT_AppId##</td></row>
		<row><td>UnregisterExtensionInfo</td><td>##IDS_ACTIONTEXT_UnregisterExtensionServers##</td><td>##IDS_ACTIONTEXT_Extension##</td></row>
		<row><td>UnregisterFonts</td><td>##IDS_ACTIONTEXT_UnregisteringFonts##</td><td>##IDS_ACTIONTEXT_Font2##</td></row>
		<row><td>UnregisterMIMEInfo</td><td>##IDS_ACTIONTEXT_UnregisteringMimeInfo##</td><td>##IDS_ACTIONTEXT_ContentTypeExtension2##</td></row>
		<row><td>UnregisterProgIdInfo</td><td>##IDS_ACTIONTEXT_UnregisteringProgramIds##</td><td>##IDS_ACTIONTEXT_ProgID##</td></row>
		<row><td>UnregisterTypeLibraries</td><td>##IDS_ACTIONTEXT_UnregTypeLibs##</td><td>##IDS_ACTIONTEXT_Libid2##</td></row>
		<row><td>WriteEnvironmentStrings</td><td>##IDS_ACTIONTEXT_EnvironmentStrings##</td><td>##IDS_ACTIONTEXT_NameValueAction##</td></row>
		<row><td>WriteIniValues</td><td>##IDS_ACTIONTEXT_WritingINI##</td><td>##IDS_ACTIONTEXT_FileSectionKeyValue2##</td></row>
		<row><td>WriteRegistryValues</td><td>##IDS_ACTIONTEXT_WritingRegistry##</td><td>##IDS_ACTIONTEXT_KeyNameValue##</td></row>
	</table>

	<table name="AdminExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>InstallAdminPackage</td><td/><td>3900</td><td>InstallAdminPackage</td><td/></row>
		<row><td>InstallFiles</td><td/><td>4000</td><td>InstallFiles</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1500</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>4010</td><td>ScheduleReboot</td><td/></row>
	</table>

	<table name="AdminUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AdminWelcome</td><td/><td>1010</td><td>AdminWelcome</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>ExecuteAction</td><td/><td>1300</td><td>ExecuteAction</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>SetupCompleteError</td><td/><td>-3</td><td>SetupCompleteError</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td/><td>-1</td><td>SetupCompleteSuccess</td><td/></row>
		<row><td>SetupInitialization</td><td/><td>50</td><td>SetupInitialization</td><td/></row>
		<row><td>SetupInterrupted</td><td/><td>-2</td><td>SetupInterrupted</td><td/></row>
		<row><td>SetupProgress</td><td/><td>1020</td><td>SetupProgress</td><td/></row>
	</table>

	<table name="AdvtExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>CreateShortcuts</td><td/><td>4500</td><td>CreateShortcuts</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1500</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>MsiPublishAssemblies</td><td/><td>6250</td><td>MsiPublishAssemblies</td><td/></row>
		<row><td>PublishComponents</td><td/><td>6200</td><td>PublishComponents</td><td/></row>
		<row><td>PublishFeatures</td><td/><td>6300</td><td>PublishFeatures</td><td/></row>
		<row><td>PublishProduct</td><td/><td>6400</td><td>PublishProduct</td><td/></row>
		<row><td>RegisterClassInfo</td><td/><td>4600</td><td>RegisterClassInfo</td><td/></row>
		<row><td>RegisterExtensionInfo</td><td/><td>4700</td><td>RegisterExtensionInfo</td><td/></row>
		<row><td>RegisterMIMEInfo</td><td/><td>4900</td><td>RegisterMIMEInfo</td><td/></row>
		<row><td>RegisterProgIdInfo</td><td/><td>4800</td><td>RegisterProgIdInfo</td><td/></row>
		<row><td>RegisterTypeLibraries</td><td/><td>4910</td><td>RegisterTypeLibraries</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>6410</td><td>ScheduleReboot</td><td/></row>
	</table>

	<table name="AdvtUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="AppId">
		<col key="yes" def="s38">AppId</col>
		<col def="S255">RemoteServerName</col>
		<col def="S255">LocalService</col>
		<col def="S255">ServiceParameters</col>
		<col def="S255">DllSurrogate</col>
		<col def="I2">ActivateAtStorage</col>
		<col def="I2">RunAsInteractiveUser</col>
	</table>

	<table name="AppSearch">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="s72">Signature_</col>
		<row><td>DOTNETVERSION45FULL</td><td>DotNet45Full</td></row>
	</table>

	<table name="BBControl">
		<col key="yes" def="s50">Billboard_</col>
		<col key="yes" def="s50">BBControl</col>
		<col def="s50">Type</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="L50">Text</col>
	</table>

	<table name="Billboard">
		<col key="yes" def="s50">Billboard</col>
		<col def="s38">Feature_</col>
		<col def="S50">Action</col>
		<col def="I2">Ordering</col>
	</table>

	<table name="Binary">
		<col key="yes" def="s72">Name</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
		<row><td>ISExpHlp.dll</td><td/><td>&lt;ISRedistPlatformDependentFolder&gt;\ISExpHlp.dll</td></row>
		<row><td>ISSELFREG.DLL</td><td/><td>&lt;ISRedistPlatformDependentFolder&gt;\isregsvr.dll</td></row>
		<row><td>NewBinary1</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\banner.jpg</td></row>
		<row><td>NewBinary10</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CompleteSetupIco.ibd</td></row>
		<row><td>NewBinary11</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CustomSetupIco.ibd</td></row>
		<row><td>NewBinary12</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\DestIcon.ibd</td></row>
		<row><td>NewBinary13</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\NetworkInstall.ico</td></row>
		<row><td>NewBinary14</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\DontInstall.ico</td></row>
		<row><td>NewBinary15</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\Install.ico</td></row>
		<row><td>NewBinary16</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallFirstUse.ico</td></row>
		<row><td>NewBinary17</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallPartial.ico</td></row>
		<row><td>NewBinary18</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallStateMenu.ico</td></row>
		<row><td>NewBinary2</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\New.ibd</td></row>
		<row><td>NewBinary3</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\Up.ibd</td></row>
		<row><td>NewBinary4</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\WarningIcon.ibd</td></row>
		<row><td>NewBinary5</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\welcome.jpg</td></row>
		<row><td>NewBinary6</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CustomSetupIco.ibd</td></row>
		<row><td>NewBinary7</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\ReinstIco.ibd</td></row>
		<row><td>NewBinary8</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\RemoveIco.ibd</td></row>
		<row><td>NewBinary9</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\SetupIcon.ibd</td></row>
		<row><td>SetAllUsers.dll</td><td/><td>&lt;ISRedistPlatformDependentFolder&gt;\SetAllUsers.dll</td></row>
	</table>

	<table name="BindImage">
		<col key="yes" def="s72">File_</col>
		<col def="S255">Path</col>
	</table>

	<table name="CCPSearch">
		<col key="yes" def="s72">Signature_</col>
	</table>

	<table name="CheckBox">
		<col key="yes" def="s72">Property</col>
		<col def="S64">Value</col>
		<row><td>ISCHECKFORPRODUCTUPDATES</td><td>1</td></row>
		<row><td>LAUNCHPROGRAM</td><td>1</td></row>
		<row><td>LAUNCHREADME</td><td>1</td></row>
	</table>

	<table name="Class">
		<col key="yes" def="s38">CLSID</col>
		<col key="yes" def="s32">Context</col>
		<col key="yes" def="s72">Component_</col>
		<col def="S255">ProgId_Default</col>
		<col def="L255">Description</col>
		<col def="S38">AppId_</col>
		<col def="S255">FileTypeMask</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="S32">DefInprocHandler</col>
		<col def="S255">Argument</col>
		<col def="s38">Feature_</col>
		<col def="I2">Attributes</col>
	</table>

	<table name="ComboBox">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
	</table>

	<table name="CompLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="s38">ComponentId</col>
		<col def="I2">Type</col>
	</table>

	<table name="Complus">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="I2">ExpType</col>
	</table>

	<table name="Component">
		<col key="yes" def="s72">Component</col>
		<col def="S38">ComponentId</col>
		<col def="s72">Directory_</col>
		<col def="i2">Attributes</col>
		<col def="S255">Condition</col>
		<col def="S72">KeyPath</col>
		<col def="I4">ISAttributes</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISScanAtBuildFile</col>
		<col def="S255">ISRegFileToMergeAtBuild</col>
		<col def="S0">ISDotNetInstallerArgsInstall</col>
		<col def="S0">ISDotNetInstallerArgsCommit</col>
		<col def="S0">ISDotNetInstallerArgsUninstall</col>
		<col def="S0">ISDotNetInstallerArgsRollback</col>
		<row><td>ConfigS.EXE</td><td>{3A667EE6-2ECE-4BF2-BC56-F698038E0F6C}</td><td>INSTALLDIR</td><td>2</td><td/><td>configs.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>EntityFramework.SqlServer.dll</td><td>{5134283F-3C29-4B1C-A41F-6EC9A2487D80}</td><td>INSTALLDIR</td><td>2</td><td/><td>entityframework.sqlserver.dl</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>EntityFramework.dll</td><td>{5F9334B4-F1CE-48DE-B270-71AEFEB63B99}</td><td>INSTALLDIR</td><td>2</td><td/><td>entityframework.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>EntityFramework.resources.dll</td><td>{E022E4EB-E81E-4C30-8BE2-3F281F0D3D93}</td><td>KO</td><td>2</td><td/><td>entityframework.resources.dl</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>I2MS2.exe</td><td>{84F6D948-DDB0-4429-88B2-619112A86A66}</td><td>INSTALLDIR</td><td>2</td><td/><td>i2ms2.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>I2MS2.vshost.exe</td><td>{8EB7E180-CA78-45C8-B908-79FD8C0D90F4}</td><td>INSTALLDIR</td><td>2</td><td/><td>i2ms2.vshost.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>I2MS2RS.EXE</td><td>{923F1787-BCCB-4D83-AF08-0C564CBE0FA0}</td><td>INSTALLDIR</td><td>2</td><td/><td>i2ms2rs.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>I2MSR.dll</td><td>{3EADBDCD-E1E5-45A7-9034-3D57FCA12F1F}</td><td>INSTALLDIR</td><td>2</td><td/><td>i2msr.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>I2MSR.resources.dll</td><td>{9494C0F2-22D4-42C9-834D-F78AC501594B}</td><td>KO_KR</td><td>2</td><td/><td>i2msr.resources.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>I2MSR.resources.dll1</td><td>{020E8C91-7AAA-4E5A-B66C-37AF628B24E9}</td><td>ZH_CHS</td><td>2</td><td/><td>i2msr.resources.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>IEMS.EXE</td><td>{D2EA0931-E897-46F8-93B9-EB1EEE982DDE}</td><td>INSTALLDIR</td><td>2</td><td/><td>iems.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT</td><td>{DA9C2AA6-AF0A-409D-8E89-32CB307EC398}</td><td>INSTALLDIR</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT1</td><td>{88114FD1-9F43-407E-BEB6-963725F6C462}</td><td>SIMPLEWINIEMS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT10</td><td>{73941DA9-BC2A-4835-ADAF-5F84BC738514}</td><td>KO</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT11</td><td>{37185FE6-AFC7-47B0-B24E-BD777C2BB568}</td><td>KO_KR</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT13</td><td>{9F6BDB51-8B7E-4F93-ABBD-F6A8D4BCFCA2}</td><td>RU</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT14</td><td>{A0BEFD03-5C7D-4809-AF5A-EA0D0BADB44E}</td><td>LSCABLE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT15</td><td>{59C3A4E7-1E0C-4F80-8F21-BBE0D8D06773}</td><td>SIMPLEWIN1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT16</td><td>{761F8188-0F86-4613-BD9A-08938D3953F5}</td><td>EXCELTEMPLATES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT17</td><td>{C97025F1-3956-4FA2-9895-6ABD41152218}</td><td>IMAGES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT18</td><td>{D2071939-0AE2-4BDF-AE1C-B6AF40D95AA3}</td><td>BUILDING</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT19</td><td>{BE80C789-771E-4C70-924D-8F9A77689811}</td><td>CATALOG</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT2</td><td>{3282527B-E060-45F2-A3C7-E2E411588F11}</td><td>DE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT20</td><td>{EB38E2CA-44C0-444D-88D5-2897177A3CBB}</td><td>DRAWING</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT21</td><td>{D1CFA6F5-77DA-4F57-B41B-41D9CE531584}</td><td>DRAWING_3D</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT22</td><td>{F6A352EF-3ED8-4278-BA58-09313D2726E7}</td><td>ETC</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT23</td><td>{B2890C02-DAD9-4B1E-974E-36715BD65849}</td><td>ICON_16</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT24</td><td>{512869FC-3776-4BF9-AD54-2C36270DE119}</td><td>LINK</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT25</td><td>{08A2D919-EF1A-47BA-83DB-6F76455A66D7}</td><td>MAP</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT26</td><td>{5E55439F-A064-4E33-9FE3-0F6E8FDCB935}</td><td>RACK_220</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT27</td><td>{AF2B6BB5-AFC3-4936-8A0A-0E4CC56C9ADD}</td><td>RACK_440</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT28</td><td>{FAB5EBF4-24CD-4557-B4D3-A18F92E9A3E0}</td><td>RACK_880</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT29</td><td>{3E3B8837-9C00-460E-BFD5-33B0ACCCC202}</td><td>SITE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT3</td><td>{835FDF6A-DD0C-484B-8F91-5D2E76CCA77A}</td><td>FORM</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT30</td><td>{30E45161-EB85-4ABB-8C43-7DE4798DF97D}</td><td>PROPERTIES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT31</td><td>{42D129DC-62DC-46F7-B80E-1DCFA84A2293}</td><td>ZH_CHS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT32</td><td>{F0D9F066-AEC6-4A32-B3C7-DC616B294950}</td><td>ZH_HANS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT33</td><td>{6628E743-F949-4120-AA02-46225938DFEB}</td><td>ZH_HANT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT5</td><td>{0E9A9414-3306-4E0D-8B4F-A019A751D6FF}</td><td>EN</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT6</td><td>{0F853FCB-E059-45A3-A26B-BE47AEC19E18}</td><td>ES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT7</td><td>{49E08DE4-AE23-4BAF-958B-373391F3E022}</td><td>FR</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT8</td><td>{61069985-4F09-4ABB-AA83-74604348C0F8}</td><td>IT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT9</td><td>{6C45442A-FE9B-48B2-A046-A0C95C07ED5A}</td><td>JA</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>MahApps.Metro.Resources.dll</td><td>{62BC887F-C111-4D0F-9000-026C9E83B961}</td><td>INSTALLDIR</td><td>2</td><td/><td>mahapps.metro.resources.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>MahApps.Metro.dll</td><td>{81B3986D-B074-48AE-B968-F7F2FFF13074}</td><td>INSTALLDIR</td><td>2</td><td/><td>mahapps.metro.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>MetroChart.dll</td><td>{C0685D7B-6536-4BCE-97F2-775F32628722}</td><td>INSTALLDIR</td><td>2</td><td/><td>metrochart.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.AspNet.SignalR.Client.dll</td><td>{1A24FF6C-3EB5-476A-AC51-F920C54AC93F}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.aspnet.signalr.cli</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.AspNet.SignalR.Core.dll</td><td>{8723460F-8F72-48DB-9881-45AFAC1D199C}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.aspnet.signalr.cor</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.AspNet.SignalR.SystemWeb.dll</td><td>{8C8849EE-96F3-4576-98D4-482E97BE42C7}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.aspnet.signalr.sys</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.dll</td><td>{6015CFAD-E4C1-4554-B200-7B46848FBFE4}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.expression.effects</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll</td><td>{DE557175-DD17-44F2-A07A-68B9B25FD392}</td><td>DE</td><td>2</td><td/><td>microsoft.expression.effects1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll2</td><td>{874C2453-E6B5-43CD-B9F1-5AF1E63A81C0}</td><td>ES</td><td>2</td><td/><td>microsoft.expression.effects3</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll3</td><td>{2FB68266-8DB5-4D18-9DC2-2C06CF3ADA41}</td><td>FR</td><td>2</td><td/><td>microsoft.expression.effects4</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll4</td><td>{16378222-FCFB-43CE-B5FF-82428E19F5BB}</td><td>IT</td><td>2</td><td/><td>microsoft.expression.effects5</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll5</td><td>{990C6B30-44FF-4ADF-8583-F2C7211A2D53}</td><td>JA</td><td>2</td><td/><td>microsoft.expression.effects6</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll6</td><td>{510381AF-6985-4645-991E-E5462E2D943D}</td><td>KO</td><td>2</td><td/><td>microsoft.expression.effects7</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll7</td><td>{613111B7-BDA7-42CA-BE35-2509412C3531}</td><td>RU</td><td>2</td><td/><td>microsoft.expression.effects9</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll8</td><td>{6E741090-2D12-4C8B-99BA-D6AA5BE7DC6C}</td><td>ZH_HANS</td><td>2</td><td/><td>microsoft.expression.effects10</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Effects.resources.dll9</td><td>{730CF57D-9177-4F05-AAA3-2A6B74BC59D9}</td><td>ZH_HANT</td><td>2</td><td/><td>microsoft.expression.effects11</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.dll</td><td>{7D2AA06B-B44C-4B46-B714-407255DC4EC1}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.expression.interac</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll</td><td>{22AACBBA-8E88-46FF-AD74-51F06E0AA87D}</td><td>DE</td><td>2</td><td/><td>microsoft.expression.interac1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll10</td><td>{9C5CC0DD-D1B8-486D-A7BE-096F17D916DF}</td><td>ZH_HANT</td><td>2</td><td/><td>microsoft.expression.interac12</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll2</td><td>{EB2395EA-7C4D-4F8B-B739-CC82A9DA8974}</td><td>EN</td><td>2</td><td/><td>microsoft.expression.interac3</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll3</td><td>{8576FBF2-59D0-473D-ABCE-8C16D8B02691}</td><td>ES</td><td>2</td><td/><td>microsoft.expression.interac4</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll4</td><td>{276E12B0-4355-488B-AC12-1EE619D1CFBD}</td><td>FR</td><td>2</td><td/><td>microsoft.expression.interac5</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll5</td><td>{EE9695DE-0A61-4B5E-9C09-24DF820D6A66}</td><td>IT</td><td>2</td><td/><td>microsoft.expression.interac6</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll6</td><td>{9FA505B4-5C05-409C-85F2-1F6241528506}</td><td>JA</td><td>2</td><td/><td>microsoft.expression.interac7</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll7</td><td>{53C75E31-3F04-4326-9CDE-AABC69800D5A}</td><td>KO</td><td>2</td><td/><td>microsoft.expression.interac8</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll8</td><td>{AE3B2EEB-81B0-48EE-91B0-B9A17A2E762A}</td><td>RU</td><td>2</td><td/><td>microsoft.expression.interac10</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll9</td><td>{36752450-6603-4BA4-B49F-88FD31A260A7}</td><td>ZH_HANS</td><td>2</td><td/><td>microsoft.expression.interac11</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Owin.Cors.dll</td><td>{322C5764-186D-4CAA-97D5-D96D6AD827DA}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.owin.cors.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Owin.Host.HttpListener.dll</td><td>{A37DDBD6-CAC4-4F30-95B5-3B584786BE6D}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.owin.host.httplist</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Owin.Host.SystemWeb.dll</td><td>{7B73237B-F277-47B1-BB1E-987A5746FC51}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.owin.host.systemwe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Owin.Hosting.dll</td><td>{D84985E3-93DF-4088-AD09-ACEF2A0BCC7E}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.owin.hosting.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Owin.Security.dll</td><td>{138EBE8B-0B2A-44EE-A060-71DD8B624A30}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.owin.security.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Microsoft.Owin.dll</td><td>{97A2DB6D-7909-402D-AC84-215A53254892}</td><td>INSTALLDIR</td><td>2</td><td/><td>microsoft.owin.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Newtonsoft.Json.dll</td><td>{817E2D1F-FD5E-45B6-A91B-8043FB952579}</td><td>INSTALLDIR</td><td>2</td><td/><td>newtonsoft.json.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Owin.dll</td><td>{E082BB35-17D9-4CF9-AD1F-5DD131782425}</td><td>INSTALLDIR</td><td>2</td><td/><td>owin.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Net.Http.Formatting.dll</td><td>{75BEF225-050E-4021-8974-A09A0D9BEC25}</td><td>INSTALLDIR</td><td>2</td><td/><td>system.net.http.formatting.d</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Web.Cors.dll</td><td>{600E31B7-7F7B-4B3F-BB69-F468A672DB45}</td><td>INSTALLDIR</td><td>2</td><td/><td>system.web.cors.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Web.Http.SelfHost.dll</td><td>{65A0B121-FADF-4933-9327-B142484CA18B}</td><td>INSTALLDIR</td><td>2</td><td/><td>system.web.http.selfhost.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Web.Http.dll</td><td>{2F7F42C6-4E1B-4DDA-86C9-4BACCE6A691D}</td><td>INSTALLDIR</td><td>2</td><td/><td>system.web.http.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Web.Http.resources.dll</td><td>{BFA11911-192E-43A5-81F5-75CDB8B84D1E}</td><td>KO</td><td>2</td><td/><td>system.web.http.resources.dl</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Controls.Input.Toolkit.dll</td><td>{35351DCD-FD01-4907-A9B6-88CC6948D5D3}</td><td>INSTALLDIR</td><td>2</td><td/><td>system.windows.controls.inpu</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Controls.Layout.Toolkit.dll</td><td>{BFB97C5D-4810-4252-8B13-3B1BF03C2210}</td><td>INSTALLDIR</td><td>2</td><td/><td>system.windows.controls.layo</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.dll</td><td>{6E6369AA-CBBE-486F-9F51-D4426E6167A8}</td><td>INSTALLDIR</td><td>2</td><td/><td>system.windows.interactivity</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll</td><td>{58B5DEAD-C5E8-4746-9106-1DC7372A54B4}</td><td>DE</td><td>2</td><td/><td>system.windows.interactivity1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll10</td><td>{7097F43D-2D6D-42B4-B286-D26724E18D41}</td><td>ZH_HANT</td><td>2</td><td/><td>system.windows.interactivity12</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll2</td><td>{18A5A61D-47EA-4EC8-988D-35CDEB7A25DF}</td><td>EN</td><td>2</td><td/><td>system.windows.interactivity3</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll3</td><td>{1DD1852E-A476-4ECA-895E-590E907F8AD5}</td><td>ES</td><td>2</td><td/><td>system.windows.interactivity4</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll4</td><td>{628D48B3-8CFE-4DFD-89D3-28B5B69650DC}</td><td>FR</td><td>2</td><td/><td>system.windows.interactivity5</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll5</td><td>{6BFDF6D4-6F70-4733-8E6C-95A6FA04DCAB}</td><td>IT</td><td>2</td><td/><td>system.windows.interactivity6</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll6</td><td>{E1FF5D9C-23DD-4036-AD80-D49F2C4F1E02}</td><td>JA</td><td>2</td><td/><td>system.windows.interactivity7</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll7</td><td>{7242B6E3-2047-4BBF-B939-53BC31A2FD30}</td><td>KO</td><td>2</td><td/><td>system.windows.interactivity8</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll8</td><td>{D26A7CF0-A582-4FC2-8BB7-2E75E2CBF454}</td><td>RU</td><td>2</td><td/><td>system.windows.interactivity9</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>System.Windows.Interactivity.resources.dll9</td><td>{1CE53EC4-B6C8-4043-BC32-24CD5B514BD3}</td><td>ZH_HANS</td><td>2</td><td/><td>system.windows.interactivity11</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>WPFToolkit.dll</td><td>{9D0F8274-5350-402D-AC8D-D96AFA3B85FF}</td><td>INSTALLDIR</td><td>2</td><td/><td>wpftoolkit.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>WebApi.exe</td><td>{1E56222B-4237-4C17-9CDB-2243B878A22E}</td><td>INSTALLDIR</td><td>2</td><td/><td>webapi.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>WebApi.vshost.exe</td><td>{4A0CF29C-A046-4065-926B-E25286CEE25F}</td><td>INSTALLDIR</td><td>2</td><td/><td>webapi.vshost.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>WebApiClient.exe</td><td>{67CADD5D-F4E7-41C3-B49F-1A9F48A9A16A}</td><td>INSTALLDIR</td><td>2</td><td/><td>webapiclient.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>_DTools.dll</td><td>{85C8C158-0CAF-4E5A-82C1-C09356FE38B4}</td><td>INSTALLDIR</td><td>2</td><td/><td>_dtools.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>log4net.dll</td><td>{D2CF42BA-D0BC-4D7A-8448-78285986EE6A}</td><td>INSTALLDIR</td><td>2</td><td/><td>log4net.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>svlDNMVAPI.dll</td><td>{CE0DA096-FFA0-4F30-81FB-81BFCC996F16}</td><td>INSTALLDIR</td><td>2</td><td/><td>svldnmvapi.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
	</table>

	<table name="Condition">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="i2">Level</col>
		<col def="S255">Condition</col>
	</table>

	<table name="Control">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control</col>
		<col def="s20">Type</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="S72">Property</col>
		<col def="L0">Text</col>
		<col def="S50">Control_Next</col>
		<col def="L50">Help</col>
		<col def="I4">ISWindowStyle</col>
		<col def="I4">ISControlId</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="S72">Binary_</col>
		<row><td>AdminChangeFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>AdminChangeFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ComboText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Combo</td><td>DirectoryCombo</td><td>21</td><td>64</td><td>277</td><td>80</td><td>458755</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_4##</td><td>Up</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>ComboText</td><td>Text</td><td>21</td><td>50</td><td>99</td><td>14</td><td>3</td><td/><td>##IDS__IsAdminInstallBrowse_LookIn##</td><td>Combo</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallBrowse_BrowseDestination##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallBrowse_ChangeDestination##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>List</td><td>DirectoryList</td><td>21</td><td>90</td><td>332</td><td>97</td><td>7</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_8##</td><td>TailText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>NewFolder</td><td>PushButton</td><td>335</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>List</td><td>##IDS__IsAdminInstallBrowse_CreateFolder##</td><td>0</td><td/><td/><td>NewBinary2</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Tail</td><td>PathEdit</td><td>21</td><td>207</td><td>332</td><td>17</td><td>3</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_11##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>TailText</td><td>Text</td><td>21</td><td>193</td><td>99</td><td>13</td><td>3</td><td/><td>##IDS__IsAdminInstallBrowse_FolderName##</td><td>Tail</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Up</td><td>PushButton</td><td>310</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>NewFolder</td><td>##IDS__IsAdminInstallBrowse_UpOneLevel##</td><td>0</td><td/><td/><td>NewBinary3</td></row>
		<row><td>AdminNetworkLocation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>InstallNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>AdminNetworkLocation</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Browse</td><td>PushButton</td><td>286</td><td>124</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_Change##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>SetupPathEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallPoint_SpecifyNetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>40</td><td>131075</td><td/><td>##IDS__IsAdminInstallPoint_EnterNetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallPoint_NetworkLocationFormatted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_Install##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>LBBrowse</td><td>Text</td><td>21</td><td>90</td><td>100</td><td>10</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_NetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>SetupPathEdit</td><td>PathEdit</td><td>21</td><td>102</td><td>330</td><td>17</td><td>3</td><td>TARGETDIR</td><td/><td>Browse</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>AdminWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsAdminInstallPointWelcome_Wizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsAdminInstallPointWelcome_ServerImage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Icon</td><td>Icon</td><td>15</td><td>15</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary4</td></row>
		<row><td>CancelSetup</td><td>No</td><td>PushButton</td><td>135</td><td>57</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCancelDlg_No##</td><td>Yes</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Text</td><td>Text</td><td>48</td><td>15</td><td>194</td><td>30</td><td>131075</td><td/><td>##IDS__IsCancelDlg_ConfirmCancel##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Yes</td><td>PushButton</td><td>62</td><td>57</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCancelDlg_Yes##</td><td>No</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>CustomSetup</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Tree</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>203</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Change##</td><td>Help</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Details</td><td>PushButton</td><td>93</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Space##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgDesc</td><td>Text</td><td>17</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsCustomSelectionDlg_SelectFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgText</td><td>Text</td><td>9</td><td>51</td><td>360</td><td>10</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_ClickFeatureIcon##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgTitle</td><td>Text</td><td>9</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsCustomSelectionDlg_CustomSetup##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>FeatureGroup</td><td>GroupBox</td><td>235</td><td>67</td><td>131</td><td>120</td><td>1</td><td/><td>##IDS__IsCustomSelectionDlg_FeatureDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Help</td><td>PushButton</td><td>22</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Help##</td><td>Details</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>InstallLabel</td><td>Text</td><td>8</td><td>190</td><td>360</td><td>10</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_InstallTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>ItemDescription</td><td>Text</td><td>241</td><td>80</td><td>120</td><td>50</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_MultilineDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Location</td><td>Text</td><td>8</td><td>203</td><td>291</td><td>20</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_FeaturePath##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Size</td><td>Text</td><td>241</td><td>133</td><td>120</td><td>50</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_FeatureSize##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Tree</td><td>SelectionTree</td><td>8</td><td>70</td><td>220</td><td>118</td><td>7</td><td>_BrowseProperty</td><td/><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>CustomSetupTips</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS_SetupTips_CustomSetupDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS_SetupTips_CustomSetup##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DontInstall</td><td>Icon</td><td>21</td><td>155</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary14</td></row>
		<row><td>CustomSetupTips</td><td>DontInstallText</td><td>Text</td><td>60</td><td>155</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_WillNotBeInstalled##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>FirstInstallText</td><td>Text</td><td>60</td><td>180</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_Advertise##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Install</td><td>Icon</td><td>21</td><td>105</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary15</td></row>
		<row><td>CustomSetupTips</td><td>InstallFirstUse</td><td>Icon</td><td>21</td><td>180</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary16</td></row>
		<row><td>CustomSetupTips</td><td>InstallPartial</td><td>Icon</td><td>21</td><td>130</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary17</td></row>
		<row><td>CustomSetupTips</td><td>InstallStateMenu</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary18</td></row>
		<row><td>CustomSetupTips</td><td>InstallStateText</td><td>Text</td><td>21</td><td>91</td><td>300</td><td>10</td><td>3</td><td/><td>##IDS_SetupTips_InstallState##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>CustomSetupTips</td><td>InstallText</td><td>Text</td><td>60</td><td>105</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_AllInstalledLocal##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>MenuText</td><td>Text</td><td>50</td><td>52</td><td>300</td><td>36</td><td>3</td><td/><td>##IDS_SetupTips_IconInstallState##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>NetworkInstall</td><td>Icon</td><td>21</td><td>205</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary13</td></row>
		<row><td>CustomSetupTips</td><td>NetworkInstallText</td><td>Text</td><td>60</td><td>205</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_Network##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>OK</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_SetupTips_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>PartialText</td><td>Text</td><td>60</td><td>130</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_SubFeaturesInstalledLocal##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>CustomerInformation</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>NameLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>CompanyEdit</td><td>Edit</td><td>21</td><td>100</td><td>237</td><td>17</td><td>3</td><td>COMPANYNAME</td><td>##IDS__IsRegisterUserDlg_Tahoma80##</td><td>SerialLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>CompanyLabel</td><td>Text</td><td>21</td><td>89</td><td>75</td><td>10</td><td>3</td><td/><td>##IDS__IsRegisterUserDlg_Organization##</td><td>CompanyEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsRegisterUserDlg_PleaseEnterInfo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Text</td><td>21</td><td>161</td><td>300</td><td>14</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_InstallFor##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsRegisterUserDlg_CustomerInformation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>NameEdit</td><td>Edit</td><td>21</td><td>63</td><td>237</td><td>17</td><td>3</td><td>USERNAME</td><td>##IDS__IsRegisterUserDlg_Tahoma50##</td><td>CompanyLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>NameLabel</td><td>Text</td><td>21</td><td>52</td><td>75</td><td>10</td><td>3</td><td/><td>##IDS__IsRegisterUserDlg_UserName##</td><td>NameEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>63</td><td>170</td><td>300</td><td>50</td><td>2</td><td>ApplicationUsers</td><td>##IDS__IsRegisterUserDlg_16##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>SerialLabel</td><td>Text</td><td>21</td><td>127</td><td>109</td><td>10</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_SerialNumber##</td><td>SerialNumber</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>SerialNumber</td><td>MaskedEdit</td><td>21</td><td>138</td><td>237</td><td>17</td><td>2</td><td>ISX_SERIALNUM</td><td/><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>DatabaseFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>65</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CHANGE##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DatabaseFolder</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary12</td></row>
		<row><td>DatabaseFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DatabaseFolder_ChangeFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DatabaseFolder_DatabaseFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>LocLabel</td><td>Text</td><td>57</td><td>52</td><td>290</td><td>10</td><td>131075</td><td/><td>##IDS_DatabaseFolder_InstallDatabaseTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Location</td><td>Text</td><td>57</td><td>65</td><td>240</td><td>40</td><td>3</td><td>_BrowseProperty</td><td>##IDS__DatabaseFolder_DatabaseDir##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>DestinationFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>65</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__DestinationFolder_Change##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DestFolder</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary12</td></row>
		<row><td>DestinationFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DestinationFolder_ChangeFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DestinationFolder_DestinationFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>LocLabel</td><td>Text</td><td>57</td><td>52</td><td>290</td><td>10</td><td>131075</td><td/><td>##IDS__DestinationFolder_InstallTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Location</td><td>Text</td><td>57</td><td>65</td><td>240</td><td>40</td><td>3</td><td>_BrowseProperty</td><td>##IDS_INSTALLDIR##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>DiskSpaceRequirements</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgDesc</td><td>Text</td><td>17</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFeatureDetailsDlg_SpaceRequired##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgText</td><td>Text</td><td>10</td><td>185</td><td>358</td><td>41</td><td>3</td><td/><td>##IDS__IsFeatureDetailsDlg_VolumesTooSmall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgTitle</td><td>Text</td><td>9</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFeatureDetailsDlg_DiskSpaceRequirements##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>List</td><td>VolumeCostList</td><td>8</td><td>55</td><td>358</td><td>125</td><td>393223</td><td/><td>##IDS__IsFeatureDetailsDlg_Numbers##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>OK</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFeatureDetailsDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>FilesInUse</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUseMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>348</td><td>33</td><td>3</td><td/><td>##IDS__IsFilesInUse_ApplicationsUsingFiles##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUse##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Exit</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Exit##</td><td>List</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Ignore</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Ignore##</td><td>Exit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>List</td><td>ListBox</td><td>21</td><td>87</td><td>331</td><td>135</td><td>7</td><td>FileInUseProcess</td><td/><td>Retry</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Retry</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Retry##</td><td>Ignore</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>InstallChangeFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ComboText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Combo</td><td>DirectoryCombo</td><td>21</td><td>64</td><td>277</td><td>80</td><td>4128779</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_4##</td><td>Up</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>ComboText</td><td>Text</td><td>21</td><td>50</td><td>99</td><td>14</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_LookIn##</td><td>Combo</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsBrowseFolderDlg_BrowseDestFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsBrowseFolderDlg_ChangeCurrentFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>List</td><td>DirectoryList</td><td>21</td><td>90</td><td>332</td><td>97</td><td>15</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_8##</td><td>TailText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>NewFolder</td><td>PushButton</td><td>335</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>List</td><td>##IDS__IsBrowseFolderDlg_CreateFolder##</td><td>0</td><td/><td/><td>NewBinary2</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Tail</td><td>PathEdit</td><td>21</td><td>207</td><td>332</td><td>17</td><td>15</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_11##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>TailText</td><td>Text</td><td>21</td><td>193</td><td>99</td><td>13</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_FolderName##</td><td>Tail</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Up</td><td>PushButton</td><td>310</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>NewFolder</td><td>##IDS__IsBrowseFolderDlg_UpOneLevel##</td><td>0</td><td/><td/><td>NewBinary3</td></row>
		<row><td>InstallWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Copyright</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Text</td><td>135</td><td>144</td><td>228</td><td>73</td><td>65539</td><td/><td>##IDS__IsWelcomeDlg_WarningCopyright##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>InstallWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsWelcomeDlg_WelcomeProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsWelcomeDlg_InstallProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Agree</td><td>RadioButtonGroup</td><td>8</td><td>190</td><td>291</td><td>40</td><td>3</td><td>AgreeToLicense</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>LicenseAgreement</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ISPrintButton</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsLicenseDlg_ReadLicenseAgreement##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsLicenseDlg_LicenseAgreement##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>ISPrintButton</td><td>PushButton</td><td>301</td><td>188</td><td>65</td><td>17</td><td>3</td><td/><td>##IDS_PRINT_BUTTON##</td><td>Agree</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Memo</td><td>ScrollableText</td><td>8</td><td>55</td><td>358</td><td>130</td><td>7</td><td/><td/><td/><td/><td>0</td><td/><td>&lt;ISProductFolder&gt;\Redist\0409\Eula.rtf</td><td/></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>MaintenanceType</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsMaintenanceDlg_MaitenanceOptions##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsMaintenanceDlg_ProgramMaintenance##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Ico1</td><td>Icon</td><td>35</td><td>75</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary6</td></row>
		<row><td>MaintenanceType</td><td>Ico2</td><td>Icon</td><td>35</td><td>135</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary7</td></row>
		<row><td>MaintenanceType</td><td>Ico3</td><td>Icon</td><td>35</td><td>195</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary8</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>21</td><td>55</td><td>290</td><td>170</td><td>3</td><td>_IsMaintenance</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text1</td><td>Text</td><td>80</td><td>72</td><td>260</td><td>35</td><td>3</td><td/><td>##IDS__IsMaintenanceDlg_ChangeFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text2</td><td>Text</td><td>80</td><td>135</td><td>260</td><td>35</td><td>3</td><td/><td>##IDS__IsMaintenanceDlg_RepairMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text3</td><td>Text</td><td>80</td><td>192</td><td>260</td><td>35</td><td>131075</td><td/><td>##IDS__IsMaintenanceDlg_RemoveProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>MaintenanceWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsMaintenanceWelcome_WizardWelcome##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>50</td><td>196611</td><td/><td>##IDS__IsMaintenanceWelcome_MaintenanceOptionsDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>MsiRMFilesInUse</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Restart</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUseMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>348</td><td>14</td><td>3</td><td/><td>##IDS__IsMsiRMFilesInUse_ApplicationsUsingFiles##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUse##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>List</td><td>ListBox</td><td>21</td><td>66</td><td>331</td><td>130</td><td>3</td><td>FileInUseProcess</td><td/><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Restart</td><td>RadioButtonGroup</td><td>19</td><td>187</td><td>343</td><td>40</td><td>3</td><td>RestartManagerOption</td><td/><td>List</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>OutOfSpace</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsDiskSpaceDlg_DiskSpace##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>43</td><td>3</td><td/><td>##IDS__IsDiskSpaceDlg_HighlightedVolumes##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsDiskSpaceDlg_OutOfDiskSpace##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>List</td><td>VolumeCostList</td><td>21</td><td>95</td><td>332</td><td>120</td><td>393223</td><td/><td>##IDS__IsDiskSpaceDlg_Numbers##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsDiskSpaceDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsPatchDlg_Update##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsPatchDlg_WelcomePatchWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>54</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsPatchDlg_PatchClickUpdate##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>3</td><td/><td/><td>DlgTitle</td><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>ReadmeInformation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS__IsReadmeDlg_Cancel##</td><td>Readme</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>232</td><td>16</td><td>65539</td><td/><td>##IDS__IsReadmeDlg_PleaseReadInfo##</td><td>Back</td><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>3</td><td/><td/><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>193</td><td>13</td><td>65539</td><td/><td>##IDS__IsReadmeDlg_ReadMeInfo##</td><td>DlgDesc</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Readme</td><td>ScrollableText</td><td>10</td><td>55</td><td>353</td><td>166</td><td>3</td><td/><td/><td>Banner</td><td/><td>0</td><td/><td>&lt;ISProductFolder&gt;\Redist\0409\Readme.rtf</td><td/></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>GroupBox1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>ReadyToInstall</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>CompanyNameText</td><td>Text</td><td>38</td><td>198</td><td>211</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Company##</td><td>SerialNumberText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>CurrentSettingsText</td><td>Text</td><td>19</td><td>80</td><td>81</td><td>10</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_CurrentSettings##</td><td>InstallNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyReadyDlg_WizardReady##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgText1</td><td>Text</td><td>21</td><td>54</td><td>330</td><td>24</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_BackOrCancel##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Text</td><td>21</td><td>99</td><td>330</td><td>20</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_InstallFor##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ModifyReady##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle2</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ReadyRepair##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle3</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ReadyInstall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>GroupBox1</td><td>Text</td><td>19</td><td>92</td><td>330</td><td>133</td><td>65541</td><td/><td/><td>SetupTypeText1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>8388611</td><td/><td>##IDS__IsVerifyReadyDlg_Install##</td><td>InstallPerMachine</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>PushButton</td><td>63</td><td>123</td><td>248</td><td>17</td><td>8388610</td><td/><td>##IDS__IsRegisterUserDlg_Anyone##</td><td>InstallPerUser</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>PushButton</td><td>63</td><td>143</td><td>248</td><td>17</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_OnlyMe##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Text</td><td>38</td><td>211</td><td>306</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Serial##</td><td>CurrentSettingsText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText1</td><td>Text</td><td>23</td><td>97</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_SetupType##</td><td>SetupTypeText2</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText2</td><td>Text</td><td>37</td><td>114</td><td>306</td><td>14</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_SelectedSetupType##</td><td>TargetFolderText1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText1</td><td>Text</td><td>24</td><td>136</td><td>306</td><td>11</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_DestFolder##</td><td>TargetFolderText2</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText2</td><td>Text</td><td>37</td><td>151</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Installdir##</td><td>UserInformationText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>UserInformationText</td><td>Text</td><td>23</td><td>171</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_UserInfo##</td><td>UserNameText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>UserNameText</td><td>Text</td><td>38</td><td>184</td><td>306</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_UserName##</td><td>CompanyNameText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>RemoveNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>ReadyToRemove</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ChoseRemoveProgram##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>24</td><td>131075</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ClickRemove##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText1</td><td>Text</td><td>21</td><td>79</td><td>330</td><td>23</td><td>3</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ClickBack##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText2</td><td>Text</td><td>21</td><td>102</td><td>330</td><td>24</td><td>3</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyRemoveAllDlg_RemoveProgram##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>8388611</td><td/><td>##IDS__IsVerifyRemoveAllDlg_Remove##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Finish</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>172</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFatalError_Finish##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsFatalError_NotModified##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsFatalError_ClickFinish##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsFatalError_KeepOrRestore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsFatalError_RestoreOrContinueLater##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>172</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsFatalError_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>25</td><td>196611</td><td/><td>##IDS__IsFatalError_WizardInterrupted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckBoxUpdates</td><td>CheckBox</td><td>135</td><td>164</td><td>10</td><td>9</td><td>2</td><td>ISCHECKFORPRODUCTUPDATES</td><td>CheckBox1</td><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckForUpdatesText</td><td>Text</td><td>152</td><td>162</td><td>190</td><td>30</td><td>65538</td><td/><td>##IDS__IsExitDialog_Update_YesCheckForUpdates##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchProgram</td><td>CheckBox</td><td>151</td><td>114</td><td>10</td><td>9</td><td>2</td><td>LAUNCHPROGRAM</td><td/><td>CheckLaunchReadme</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchReadme</td><td>CheckBox</td><td>151</td><td>148</td><td>10</td><td>9</td><td>2</td><td>LAUNCHREADME</td><td/><td>CheckBoxUpdates</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>182</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckLaunchProgram</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchProgramText</td><td>Text</td><td>164</td><td>112</td><td>98</td><td>15</td><td>65538</td><td/><td>##IDS__IsExitDialog_LaunchProgram##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchReadmeText</td><td>Text</td><td>164</td><td>148</td><td>120</td><td>13</td><td>65538</td><td/><td>##IDS__IsExitDialog_ShowReadMe##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsExitDialog_Finish##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>182</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsExitDialog_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_InstallSuccess##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine3</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_UninstallSuccess##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine1</td><td>Text</td><td>135</td><td>30</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_Update_SetupFinished##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine2</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_Update_PossibleUpdates##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine3</td><td>Text</td><td>135</td><td>120</td><td>228</td><td>45</td><td>65538</td><td/><td>##IDS__IsExitDialog_Update_InternetConnection##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>A</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Abort##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>C</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>ErrorIcon</td><td>Icon</td><td>15</td><td>15</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary4</td></row>
		<row><td>SetupError</td><td>ErrorText</td><td>Text</td><td>50</td><td>15</td><td>200</td><td>50</td><td>131075</td><td/><td>##IDS__IsErrorDlg_ErrorText##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>I</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Ignore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>N</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_NO##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>O</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>R</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Retry##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>Y</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Yes##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>ActionData</td><td>Text</td><td>135</td><td>125</td><td>228</td><td>12</td><td>65539</td><td/><td>##IDS__IsInitDlg_1##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>ActionText</td><td>Text</td><td>135</td><td>109</td><td>220</td><td>36</td><td>65539</td><td/><td>##IDS__IsInitDlg_2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupInitialization</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_NEXT##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsInitDlg_WelcomeWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>30</td><td>196611</td><td/><td>##IDS__IsInitDlg_PreparingWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Finish</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>172</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsUserExit_Finish##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsUserExit_NotModified##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsUserExit_ClickFinish##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsUserExit_KeepOrRestore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsUserExit_RestoreOrContinue##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>172</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsUserExit_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>25</td><td>196611</td><td/><td>##IDS__IsUserExit_WizardInterrupted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>ProgressBar</td><td>59</td><td>113</td><td>275</td><td>12</td><td>65537</td><td/><td>##IDS__IsProgressDlg_ProgressDone##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>ActionText</td><td>Text</td><td>59</td><td>100</td><td>275</td><td>12</td><td>3</td><td/><td>##IDS__IsProgressDlg_2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>SetupProgress</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsProgressDlg_UninstallingFeatures2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgDesc2</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsProgressDlg_UninstallingFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgText</td><td>Text</td><td>59</td><td>51</td><td>275</td><td>30</td><td>196610</td><td/><td>##IDS__IsProgressDlg_WaitUninstall2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgText2</td><td>Text</td><td>59</td><td>51</td><td>275</td><td>30</td><td>196610</td><td/><td>##IDS__IsProgressDlg_WaitUninstall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>196610</td><td/><td>##IDS__IsProgressDlg_InstallingProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgTitle2</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>196610</td><td/><td>##IDS__IsProgressDlg_Uninstalling##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>LbSec</td><td>Text</td><td>192</td><td>139</td><td>32</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_SecHidden##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>LbStatus</td><td>Text</td><td>59</td><td>85</td><td>70</td><td>12</td><td>3</td><td/><td>##IDS__IsProgressDlg_Status##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>SetupIcon</td><td>Icon</td><td>21</td><td>51</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary9</td></row>
		<row><td>SetupProgress</td><td>ShowTime</td><td>Text</td><td>170</td><td>139</td><td>17</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_Hidden##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>TextTime</td><td>Text</td><td>59</td><td>139</td><td>110</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_HiddenTimeRemaining##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupResume</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_WizardResume##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Text</td><td>135</td><td>46</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_ResumeSuspended##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_Resuming##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>SetupType</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CompText</td><td>Text</td><td>80</td><td>80</td><td>246</td><td>30</td><td>3</td><td/><td>##IDS__IsSetupTypeMinDlg_AllFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CompleteIco</td><td>Icon</td><td>34</td><td>80</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary10</td></row>
		<row><td>SetupType</td><td>CustText</td><td>Text</td><td>80</td><td>171</td><td>246</td><td>30</td><td>2</td><td/><td>##IDS__IsSetupTypeMinDlg_ChooseFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CustomIco</td><td>Icon</td><td>34</td><td>171</td><td>24</td><td>24</td><td>5242880</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary11</td></row>
		<row><td>SetupType</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsSetupTypeMinDlg_ChooseSetupType##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>DlgText</td><td>Text</td><td>22</td><td>49</td><td>326</td><td>10</td><td>3</td><td/><td>##IDS__IsSetupTypeMinDlg_SelectSetupType##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupType</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsSetupTypeMinDlg_SetupType##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>MinIco</td><td>Icon</td><td>34</td><td>125</td><td>24</td><td>24</td><td>5242880</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary11</td></row>
		<row><td>SetupType</td><td>MinText</td><td>Text</td><td>80</td><td>125</td><td>246</td><td>30</td><td>2</td><td/><td>##IDS__IsSetupTypeMinDlg_MinimumFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>20</td><td>59</td><td>264</td><td>139</td><td>1048579</td><td>_IsSetupTypeMin</td><td/><td>Back</td><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SplashBitmap</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Image</td><td>Bitmap</td><td>13</td><td>12</td><td>349</td><td>211</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SplashBitmap</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
	</table>

	<table name="ControlCondition">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Action</col>
		<col key="yes" def="s255">Condition</col>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomSetup</td><td>Details</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomSetup</td><td>InstallLabel</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>NOT Privileged</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>ProductState &gt; 0</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>Version9X</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>NOT Privileged</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>ProductState &gt; 0</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>Version9X</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>CustomerInformation</td><td>SerialLabel</td><td>Show</td><td>SERIALNUMSHOW</td></row>
		<row><td>CustomerInformation</td><td>SerialNumber</td><td>Show</td><td>SERIALNUMSHOW</td></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Hide</td><td>SHOWCOPYRIGHT="No"</td></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Show</td><td>SHOWCOPYRIGHT="Yes"</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>Disable</td><td>AgreeToLicense &lt;&gt; "Yes"</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>Enable</td><td>AgreeToLicense = "Yes"</td></row>
		<row><td>ReadyToInstall</td><td>CompanyNameText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>CurrentSettingsText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle</td><td>Show</td><td>ProgressType0="Modify"</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle2</td><td>Show</td><td>ProgressType0="Repair"</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle3</td><td>Show</td><td>ProgressType0="install"</td></row>
		<row><td>ReadyToInstall</td><td>GroupBox1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>Disable</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>Enable</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Hide</td><td>NOT SERIALNUMSHOW</td></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText2</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText2</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>UserInformationText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>UserNameText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Default</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>Default</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckBoxUpdates</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckForUpdatesText</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchProgram</td><td>Show</td><td>SHOWLAUNCHPROGRAM="-1" And PROGRAMFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchReadme</td><td>Show</td><td>SHOWLAUNCHREADME="-1"  And READMEFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchProgramText</td><td>Show</td><td>SHOWLAUNCHPROGRAM="-1" And PROGRAMFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchReadmeText</td><td>Show</td><td>SHOWLAUNCHREADME="-1"  And READMEFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine2</td><td>Show</td><td>ProgressType2="installed" And ((ACTION&lt;&gt;"INSTALL") OR (NOT ISENABLEDWUSFINISHDIALOG) OR (ISENABLEDWUSFINISHDIALOG And Installed))</td></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine3</td><td>Show</td><td>ProgressType2="uninstalled" And ((ACTION&lt;&gt;"INSTALL") OR (NOT ISENABLEDWUSFINISHDIALOG) OR (ISENABLEDWUSFINISHDIALOG And Installed))</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine1</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine2</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine3</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Default</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>Default</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupProgress</td><td>DlgDesc</td><td>Show</td><td>ProgressType2="installed"</td></row>
		<row><td>SetupProgress</td><td>DlgDesc2</td><td>Show</td><td>ProgressType2="uninstalled"</td></row>
		<row><td>SetupProgress</td><td>DlgText</td><td>Show</td><td>ProgressType3="installs"</td></row>
		<row><td>SetupProgress</td><td>DlgText2</td><td>Show</td><td>ProgressType3="uninstalls"</td></row>
		<row><td>SetupProgress</td><td>DlgTitle</td><td>Show</td><td>ProgressType1="Installing"</td></row>
		<row><td>SetupProgress</td><td>DlgTitle2</td><td>Show</td><td>ProgressType1="Uninstalling"</td></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Hide</td><td>RESUME</td></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Show</td><td>NOT RESUME</td></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Hide</td><td>NOT RESUME</td></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Show</td><td>RESUME</td></row>
	</table>

	<table name="ControlEvent">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Event</col>
		<col key="yes" def="s255">Argument</col>
		<col key="yes" def="S255">Condition</col>
		<col def="I2">Ordering</col>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>Reset</td><td>0</td><td>1</td><td>1</td></row>
		<row><td>AdminChangeFolder</td><td>NewFolder</td><td>DirectoryListNew</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>SetTargetPath</td><td>TARGETDIR</td><td>1</td><td>1</td></row>
		<row><td>AdminChangeFolder</td><td>Up</td><td>DirectoryListUp</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Back</td><td>NewDialog</td><td>AdminWelcome</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Browse</td><td>SpawnDialog</td><td>AdminChangeFolder</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>3</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>2</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>SetTargetPath</td><td>TARGETDIR</td><td>1</td><td>1</td></row>
		<row><td>AdminWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>AdminWelcome</td><td>Next</td><td>NewDialog</td><td>AdminNetworkLocation</td><td>1</td><td>0</td></row>
		<row><td>CancelSetup</td><td>No</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>CancelSetup</td><td>Yes</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>CancelSetup</td><td>Yes</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>CustomSetup</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>NOT Installed</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>Installed</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>SelectionBrowse</td><td>InstallChangeFolder</td><td>1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Details</td><td>SelectionBrowse</td><td>DiskSpaceRequirements</td><td>1</td><td>1</td></row>
		<row><td>CustomSetup</td><td>Help</td><td>SpawnDialog</td><td>CustomSetupTips</td><td>1</td><td>1</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>[_IsSetupTypeMin]</td><td>Custom</td><td>1</td><td>0</td></row>
		<row><td>CustomSetupTips</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Back</td><td>NewDialog</td><td>InstallWelcome</td><td>NOT Installed</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>EndDialog</td><td>Exit</td><td>(SERIALNUMVALRETRYLIMIT) And (SERIALNUMVALRETRYLIMIT&lt;0) And (SERIALNUMVALRETURN&lt;&gt;SERIALNUMVALSUCCESSRETVAL)</td><td>2</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>(Not SERIALNUMVALRETURN) OR (SERIALNUMVALRETURN=SERIALNUMVALSUCCESSRETVAL)</td><td>3</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>[ALLUSERS]</td><td>1</td><td>ApplicationUsers = "AllUsers" And Privileged</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>[ALLUSERS]</td><td>{}</td><td>ApplicationUsers = "OnlyCurrentUser" And Privileged</td><td>2</td></row>
		<row><td>DatabaseFolder</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>SpawnDialog</td><td>InstallChangeFolder</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>[_BrowseProperty]</td><td>DATABASEDIR</td><td>1</td><td>2</td></row>
		<row><td>DatabaseFolder</td><td>Next</td><td>NewDialog</td><td>SetupType</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>0</td></row>
		<row><td>DestinationFolder</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>SpawnDialog</td><td>InstallChangeFolder</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>[_BrowseProperty]</td><td>INSTALLDIR</td><td>1</td><td>2</td></row>
		<row><td>DestinationFolder</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>1</td><td>0</td></row>
		<row><td>DiskSpaceRequirements</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Exit</td><td>EndDialog</td><td>Exit</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Ignore</td><td>EndDialog</td><td>Ignore</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Retry</td><td>EndDialog</td><td>Retry</td><td>1</td><td>0</td></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>Reset</td><td>0</td><td>1</td><td>1</td></row>
		<row><td>InstallChangeFolder</td><td>NewFolder</td><td>DirectoryListNew</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>3</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>SetTargetPath</td><td>[_BrowseProperty]</td><td>1</td><td>2</td></row>
		<row><td>InstallChangeFolder</td><td>Up</td><td>DirectoryListUp</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Back</td><td>NewDialog</td><td>SplashBitmap</td><td>Display_IsBitmapDlg</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Back</td><td>NewDialog</td><td>InstallWelcome</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>ISPrintButton</td><td>DoAction</td><td>ISPrint</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>NewDialog</td><td>CustomerInformation</td><td>AgreeToLicense = "Yes"</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Back</td><td>NewDialog</td><td>MaintenanceWelcome</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>CustomSetup</td><td>_IsMaintenance = "Change"</td><td>12</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>_IsMaintenance = "Reinstall"</td><td>13</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>ReadyToRemove</td><td>_IsMaintenance = "Remove"</td><td>11</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>Reinstall</td><td>ALL</td><td>_IsMaintenance = "Reinstall"</td><td>10</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>ReinstallMode</td><td>[ReinstallModeText]</td><td>_IsMaintenance = "Reinstall"</td><td>9</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType0]</td><td>Modify</td><td>_IsMaintenance = "Change"</td><td>2</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType0]</td><td>Repair</td><td>_IsMaintenance = "Reinstall"</td><td>1</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType1]</td><td>Modifying</td><td>_IsMaintenance = "Change"</td><td>3</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType1]</td><td>Repairing</td><td>_IsMaintenance = "Reinstall"</td><td>4</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType2]</td><td>modified</td><td>_IsMaintenance = "Change"</td><td>6</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType2]</td><td>repairs</td><td>_IsMaintenance = "Reinstall"</td><td>5</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType3]</td><td>modifies</td><td>_IsMaintenance = "Change"</td><td>7</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType3]</td><td>repairs</td><td>_IsMaintenance = "Reinstall"</td><td>8</td></row>
		<row><td>MaintenanceWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceWelcome</td><td>Next</td><td>NewDialog</td><td>MaintenanceType</td><td>1</td><td>0</td></row>
		<row><td>MsiRMFilesInUse</td><td>Cancel</td><td>EndDialog</td><td>Exit</td><td>1</td><td>1</td></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>1</td></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>RMShutdownAndRestart</td><td>0</td><td>RestartManagerOption="CloseRestart"</td><td>2</td></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>NewDialog</td><td>AdminNetworkLocation</td><td>ACTION = "ADMIN"</td><td>0</td></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>NewDialog</td><td>DestinationFolder</td><td>ACTION &lt;&gt; "ADMIN"</td><td>0</td></row>
		<row><td>PatchWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>EndDialog</td><td>Return</td><td>1</td><td>3</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>Reinstall</td><td>ALL</td><td>PATCH And REINSTALL=""</td><td>1</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>ReinstallMode</td><td>omus</td><td>PATCH And REINSTALLMODE=""</td><td>2</td></row>
		<row><td>ReadmeInformation</td><td>Back</td><td>NewDialog</td><td>LicenseAgreement</td><td>1</td><td>1</td></row>
		<row><td>ReadmeInformation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>ReadmeInformation</td><td>Next</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>CustomSetup</td><td>Installed OR _IsSetupTypeMin = "Custom"</td><td>2</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>InstallWelcome</td><td>NOT Installed</td><td>1</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>Installed AND _IsMaintenance = "Reinstall"</td><td>3</td></row>
		<row><td>ReadyToInstall</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ALLUSERS]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[MSIINSTALLPERUSER]</td><td>{}</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ALLUSERS]</td><td>2</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[MSIINSTALLPERUSER]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>2</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>2</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>Remove</td><td>ALL</td><td>1</td><td>1</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType1]</td><td>Uninstalling</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType2]</td><td>uninstalled</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType3]</td><td>uninstalls</td><td>1</td><td>0</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>[Suspend]</td><td>{}</td><td>1</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>[Suspend]</td><td>1</td><td>1</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1")</td><td>3</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1") And NOT ISENABLEDWUSFINISHDIALOG</td><td>6</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupError</td><td>A</td><td>EndDialog</td><td>ErrorAbort</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>C</td><td>EndDialog</td><td>ErrorCancel</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>I</td><td>EndDialog</td><td>ErrorIgnore</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>N</td><td>EndDialog</td><td>ErrorNo</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>O</td><td>EndDialog</td><td>ErrorOk</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>R</td><td>EndDialog</td><td>ErrorRetry</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>Y</td><td>EndDialog</td><td>ErrorYes</td><td>1</td><td>0</td></row>
		<row><td>SetupInitialization</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>[Suspend]</td><td>{}</td><td>1</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>[Suspend]</td><td>1</td><td>1</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1")</td><td>3</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupProgress</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Next</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Next</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>SetupType</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>SetupType</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>NewDialog</td><td>CustomSetup</td><td>_IsSetupTypeMin = "Custom"</td><td>2</td></row>
		<row><td>SetupType</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>_IsSetupTypeMin &lt;&gt; "Custom"</td><td>1</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>100</td><td>_IsSetupTypeMin="Minimal"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>200</td><td>_IsSetupTypeMin="Typical"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>300</td><td>_IsSetupTypeMin="Custom"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[ISRUNSETUPTYPEADDLOCALEVENT]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameCustom]</td><td>_IsSetupTypeMin = "Custom"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameMinimal]</td><td>_IsSetupTypeMin = "Minimal"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameTypical]</td><td>_IsSetupTypeMin = "Typical"</td><td>0</td></row>
		<row><td>SplashBitmap</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SplashBitmap</td><td>Next</td><td>NewDialog</td><td>InstallWelcome</td><td>1</td><td>0</td></row>
	</table>

	<table name="CreateFolder">
		<col key="yes" def="s72">Directory_</col>
		<col key="yes" def="s72">Component_</col>
		<row><td>DE</td><td>ISX_DEFAULTCOMPONENT2</td></row>
		<row><td>EN</td><td>ISX_DEFAULTCOMPONENT5</td></row>
		<row><td>ES</td><td>ISX_DEFAULTCOMPONENT6</td></row>
		<row><td>FR</td><td>ISX_DEFAULTCOMPONENT7</td></row>
		<row><td>IT</td><td>ISX_DEFAULTCOMPONENT8</td></row>
		<row><td>JA</td><td>ISX_DEFAULTCOMPONENT9</td></row>
		<row><td>KO</td><td>ISX_DEFAULTCOMPONENT10</td></row>
		<row><td>KO_KR</td><td>ISX_DEFAULTCOMPONENT11</td></row>
		<row><td>LSCABLE</td><td>ISX_DEFAULTCOMPONENT14</td></row>
		<row><td>RU</td><td>ISX_DEFAULTCOMPONENT13</td></row>
		<row><td>SIMPLEWIN1</td><td>ISX_DEFAULTCOMPONENT15</td></row>
		<row><td>SIMPLEWINIEMS</td><td>ISX_DEFAULTCOMPONENT1</td></row>
		<row><td>ZH_CHS</td><td>ISX_DEFAULTCOMPONENT31</td></row>
		<row><td>ZH_HANS</td><td>ISX_DEFAULTCOMPONENT32</td></row>
		<row><td>ZH_HANT</td><td>ISX_DEFAULTCOMPONENT33</td></row>
	</table>

	<table name="CustomAction">
		<col key="yes" def="s72">Action</col>
		<col def="i2">Type</col>
		<col def="S64">Source</col>
		<col def="S0">Target</col>
		<col def="I4">ExtendedType</col>
		<col def="S255">ISComments</col>
		<row><td>ISPreventDowngrade</td><td>19</td><td/><td>[IS_PREVENT_DOWNGRADE_EXIT]</td><td/><td>Exits install when a newer version of this product is found</td></row>
		<row><td>ISPrint</td><td>1</td><td>SetAllUsers.dll</td><td>PrintScrollableText</td><td/><td>Prints the contents of a ScrollableText control on a dialog.</td></row>
		<row><td>ISRunSetupTypeAddLocalEvent</td><td>1</td><td>ISExpHlp.dll</td><td>RunSetupTypeAddLocalEvent</td><td/><td>Run the AddLocal events associated with the Next button on the Setup Type dialog.</td></row>
		<row><td>ISSelfRegisterCosting</td><td>1</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterCosting</td><td/><td/></row>
		<row><td>ISSelfRegisterFiles</td><td>3073</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterFiles</td><td/><td/></row>
		<row><td>ISSelfRegisterFinalize</td><td>1</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterFinalize</td><td/><td/></row>
		<row><td>ISUnSelfRegisterFiles</td><td>3073</td><td>ISSELFREG.DLL</td><td>ISUnSelfRegisterFiles</td><td/><td/></row>
		<row><td>SetARPINSTALLLOCATION</td><td>51</td><td>ARPINSTALLLOCATION</td><td>[INSTALLDIR]</td><td/><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>51</td><td>ALLUSERSPROFILE</td><td>[%SystemRoot]\Profiles\All Users</td><td/><td/></row>
		<row><td>ShowMsiLog</td><td>226</td><td>SystemFolder</td><td>[SystemFolder]notepad.exe "[MsiLogFileLocation]"</td><td/><td>Shows Property-driven MSI Log</td></row>
		<row><td>setAllUsersProfile2K</td><td>51</td><td>ALLUSERSPROFILE</td><td>[%ALLUSERSPROFILE]</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>51</td><td>USERPROFILE</td><td>[%USERPROFILE]</td><td/><td/></row>
	</table>

	<table name="Dialog">
		<col key="yes" def="s72">Dialog</col>
		<col def="i2">HCentering</col>
		<col def="i2">VCentering</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="L128">Title</col>
		<col def="s50">Control_First</col>
		<col def="S50">Control_Default</col>
		<col def="S50">Control_Cancel</col>
		<col def="S255">ISComments</col>
		<col def="S72">TextStyle_</col>
		<col def="I4">ISWindowStyle</col>
		<col def="I4">ISResourceId</col>
		<row><td>AdminChangeFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tail</td><td>OK</td><td>Cancel</td><td>Install Point Browse</td><td/><td>0</td><td/></row>
		<row><td>AdminNetworkLocation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>InstallNow</td><td>InstallNow</td><td>Cancel</td><td>Network Location</td><td/><td>0</td><td/></row>
		<row><td>AdminWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Administration Welcome</td><td/><td>0</td><td/></row>
		<row><td>CancelSetup</td><td>50</td><td>50</td><td>260</td><td>85</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>No</td><td>No</td><td>No</td><td>Cancel</td><td/><td>0</td><td/></row>
		<row><td>CustomSetup</td><td>50</td><td>50</td><td>374</td><td>266</td><td>35</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tree</td><td>Next</td><td>Cancel</td><td>Custom Selection</td><td/><td>0</td><td/></row>
		<row><td>CustomSetupTips</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Custom Setup Tips</td><td/><td>0</td><td/></row>
		<row><td>CustomerInformation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>NameEdit</td><td>Next</td><td>Cancel</td><td>Identification</td><td/><td>0</td><td/></row>
		<row><td>DatabaseFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Database Folder</td><td/><td>0</td><td/></row>
		<row><td>DestinationFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Destination Folder</td><td/><td>0</td><td/></row>
		<row><td>DiskSpaceRequirements</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Feature Details</td><td/><td>0</td><td/></row>
		<row><td>FilesInUse</td><td>50</td><td>50</td><td>374</td><td>266</td><td>19</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Retry</td><td>Retry</td><td>Exit</td><td>Files in Use</td><td/><td>0</td><td/></row>
		<row><td>InstallChangeFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tail</td><td>OK</td><td>Cancel</td><td>Browse</td><td/><td>0</td><td/></row>
		<row><td>InstallWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Welcome Panel</td><td/><td>0</td><td/></row>
		<row><td>LicenseAgreement</td><td>50</td><td>50</td><td>374</td><td>266</td><td>2</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Agree</td><td>Next</td><td>Cancel</td><td>License Agreement</td><td/><td>0</td><td/></row>
		<row><td>MaintenanceType</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RadioGroup</td><td>Next</td><td>Cancel</td><td>Change, Reinstall, Remove</td><td/><td>0</td><td/></row>
		<row><td>MaintenanceWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Maintenance Welcome</td><td/><td>0</td><td/></row>
		<row><td>MsiRMFilesInUse</td><td>50</td><td>50</td><td>374</td><td>266</td><td>19</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>Cancel</td><td>RestartManager Files in Use</td><td/><td>0</td><td/></row>
		<row><td>OutOfSpace</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Resume</td><td>Resume</td><td>Resume</td><td>Out Of Disk Space</td><td/><td>0</td><td/></row>
		<row><td>PatchWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS__IsPatchDlg_PatchWizard##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Patch Panel</td><td/><td>0</td><td/></row>
		<row><td>ReadmeInformation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>7</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Readme Information</td><td/><td>0</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>50</td><td>50</td><td>374</td><td>266</td><td>35</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>InstallNow</td><td>InstallNow</td><td>Cancel</td><td>Ready to Install</td><td/><td>0</td><td/></row>
		<row><td>ReadyToRemove</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RemoveNow</td><td>RemoveNow</td><td>Cancel</td><td>Verify Remove</td><td/><td>0</td><td/></row>
		<row><td>SetupCompleteError</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Finish</td><td>Finish</td><td>Finish</td><td>Fatal Error</td><td/><td>0</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Exit</td><td/><td>0</td><td/></row>
		<row><td>SetupError</td><td>50</td><td>50</td><td>270</td><td>110</td><td>65543</td><td>##IDS__IsErrorDlg_InstallerInfo##</td><td>ErrorText</td><td>O</td><td>C</td><td>Error</td><td/><td>0</td><td/></row>
		<row><td>SetupInitialization</td><td>50</td><td>50</td><td>374</td><td>266</td><td>5</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Cancel</td><td>Cancel</td><td>Cancel</td><td>Setup Initialization</td><td/><td>0</td><td/></row>
		<row><td>SetupInterrupted</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Finish</td><td>Finish</td><td>Finish</td><td>User Exit</td><td/><td>0</td><td/></row>
		<row><td>SetupProgress</td><td>50</td><td>50</td><td>374</td><td>266</td><td>5</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Cancel</td><td>Cancel</td><td>Cancel</td><td>Progress</td><td/><td>0</td><td/></row>
		<row><td>SetupResume</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Resume</td><td/><td>0</td><td/></row>
		<row><td>SetupType</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RadioGroup</td><td>Next</td><td>Cancel</td><td>Setup Type</td><td/><td>0</td><td/></row>
		<row><td>SplashBitmap</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Welcome Bitmap</td><td/><td>0</td><td/></row>
	</table>

	<table name="Directory">
		<col key="yes" def="s72">Directory</col>
		<col def="S72">Directory_Parent</col>
		<col def="l255">DefaultDir</col>
		<col def="S255">ISDescription</col>
		<col def="I4">ISAttributes</col>
		<col def="S255">ISFolderName</col>
		<row><td>ALLUSERSPROFILE</td><td>TARGETDIR</td><td>.:ALLUSE~1|All Users</td><td/><td>0</td><td/></row>
		<row><td>AdminToolsFolder</td><td>TARGETDIR</td><td>.:Admint~1|AdminTools</td><td/><td>0</td><td/></row>
		<row><td>AppDataFolder</td><td>TARGETDIR</td><td>.:APPLIC~1|Application Data</td><td/><td>0</td><td/></row>
		<row><td>BUILDING</td><td>IMAGES</td><td>building</td><td/><td>0</td><td/></row>
		<row><td>CATALOG</td><td>IMAGES</td><td>catalog</td><td/><td>0</td><td/></row>
		<row><td>CommonAppDataFolder</td><td>TARGETDIR</td><td>.:Common~1|CommonAppData</td><td/><td>0</td><td/></row>
		<row><td>CommonFiles64Folder</td><td>TARGETDIR</td><td>.:Common64</td><td/><td>0</td><td/></row>
		<row><td>CommonFilesFolder</td><td>TARGETDIR</td><td>.:Common</td><td/><td>0</td><td/></row>
		<row><td>DATABASEDIR</td><td>ISYourDataBaseDir</td><td>.</td><td/><td>0</td><td/></row>
		<row><td>DE</td><td>INSTALLDIR</td><td>de</td><td/><td>0</td><td/></row>
		<row><td>DRAWING</td><td>IMAGES</td><td>drawing</td><td/><td>0</td><td/></row>
		<row><td>DRAWING_3D</td><td>IMAGES</td><td>DRAWIN~1|drawing_3d</td><td/><td>0</td><td/></row>
		<row><td>DesktopFolder</td><td>TARGETDIR</td><td>.:Desktop</td><td/><td>3</td><td/></row>
		<row><td>EN</td><td>INSTALLDIR</td><td>en</td><td/><td>0</td><td/></row>
		<row><td>ES</td><td>INSTALLDIR</td><td>es</td><td/><td>0</td><td/></row>
		<row><td>ETC</td><td>IMAGES</td><td>etc</td><td/><td>0</td><td/></row>
		<row><td>EXCELTEMPLATES</td><td>SIMPLEWIN1</td><td>EXCELT~1|ExcelTemplates</td><td/><td>0</td><td/></row>
		<row><td>FORM</td><td>IMAGES</td><td>form</td><td/><td>0</td><td/></row>
		<row><td>FR</td><td>INSTALLDIR</td><td>fr</td><td/><td>0</td><td/></row>
		<row><td>FavoritesFolder</td><td>TARGETDIR</td><td>.:FAVORI~1|Favorites</td><td/><td>0</td><td/></row>
		<row><td>FontsFolder</td><td>TARGETDIR</td><td>.:Fonts</td><td/><td>0</td><td/></row>
		<row><td>GlobalAssemblyCache</td><td>TARGETDIR</td><td>.:Global~1|GlobalAssemblyCache</td><td/><td>0</td><td/></row>
		<row><td>ICON_16</td><td>IMAGES</td><td>icon_16</td><td/><td>0</td><td/></row>
		<row><td>IMAGES</td><td>SIMPLEWIN1</td><td>Images</td><td/><td>0</td><td/></row>
		<row><td>INSTALLDIR</td><td>SIMPLEWIN_CLIENT</td><td>.</td><td/><td>0</td><td/></row>
		<row><td>ISCommonFilesFolder</td><td>CommonFilesFolder</td><td>Instal~1|InstallShield</td><td/><td>0</td><td/></row>
		<row><td>ISMyCompanyDir</td><td>ProgramFilesFolder</td><td>MYCOMP~1|My Company Name</td><td/><td>0</td><td/></row>
		<row><td>ISMyProductDir</td><td>ISMyCompanyDir</td><td>MYPROD~1|My Product Name</td><td/><td>0</td><td/></row>
		<row><td>ISYourDataBaseDir</td><td>INSTALLDIR</td><td>Database</td><td/><td>0</td><td/></row>
		<row><td>IT</td><td>INSTALLDIR</td><td>it</td><td/><td>0</td><td/></row>
		<row><td>JA</td><td>INSTALLDIR</td><td>ja</td><td/><td>0</td><td/></row>
		<row><td>KO</td><td>INSTALLDIR</td><td>ko</td><td/><td>0</td><td/></row>
		<row><td>KO_KR</td><td>INSTALLDIR</td><td>ko-KR</td><td/><td>0</td><td/></row>
		<row><td>LINK</td><td>IMAGES</td><td>link</td><td/><td>0</td><td/></row>
		<row><td>LSCABLE</td><td>AppDataFolder</td><td>LSCable</td><td/><td>0</td><td/></row>
		<row><td>LS_CABLE___SYSTEM</td><td>ProgramFilesFolder</td><td>LSCABL~1|LS Cable &amp; System</td><td/><td>0</td><td/></row>
		<row><td>LocalAppDataFolder</td><td>TARGETDIR</td><td>.:LocalA~1|LocalAppData</td><td/><td>0</td><td/></row>
		<row><td>MAP</td><td>IMAGES</td><td>map</td><td/><td>0</td><td/></row>
		<row><td>MY_PRODUCT_NAME</td><td>LS_CABLE___SYSTEM</td><td>MYPROD~1|My Product Name</td><td/><td>0</td><td/></row>
		<row><td>MyPicturesFolder</td><td>TARGETDIR</td><td>.:MyPict~1|MyPictures</td><td/><td>0</td><td/></row>
		<row><td>NetHoodFolder</td><td>TARGETDIR</td><td>.:NetHood</td><td/><td>0</td><td/></row>
		<row><td>PROPERTIES</td><td>INSTALLDIR</td><td>PROPER~1|Properties</td><td/><td>0</td><td/></row>
		<row><td>PersonalFolder</td><td>TARGETDIR</td><td>.:Personal</td><td/><td>0</td><td/></row>
		<row><td>PrimaryVolumePath</td><td>TARGETDIR</td><td>.:Primar~1|PrimaryVolumePath</td><td/><td>0</td><td/></row>
		<row><td>PrintHoodFolder</td><td>TARGETDIR</td><td>.:PRINTH~1|PrintHood</td><td/><td>0</td><td/></row>
		<row><td>ProgramFiles64Folder</td><td>TARGETDIR</td><td>.:Prog64~1|Program Files 64</td><td/><td>0</td><td/></row>
		<row><td>ProgramFilesFolder</td><td>TARGETDIR</td><td>.:PROGRA~1|program files</td><td/><td>0</td><td/></row>
		<row><td>ProgramMenuFolder</td><td>TARGETDIR</td><td>.:Programs</td><td/><td>3</td><td/></row>
		<row><td>RACK_220</td><td>IMAGES</td><td>rack_220</td><td/><td>0</td><td/></row>
		<row><td>RACK_440</td><td>IMAGES</td><td>rack_440</td><td/><td>0</td><td/></row>
		<row><td>RACK_880</td><td>IMAGES</td><td>rack_880</td><td/><td>0</td><td/></row>
		<row><td>RU</td><td>INSTALLDIR</td><td>ru</td><td/><td>0</td><td/></row>
		<row><td>RecentFolder</td><td>TARGETDIR</td><td>.:Recent</td><td/><td>0</td><td/></row>
		<row><td>SIMPLEWIN1</td><td>LSCABLE</td><td>SIMPLE~1|SimpleWin</td><td/><td>0</td><td/></row>
		<row><td>SIMPLEWINIEMS</td><td>ProgramFilesFolder</td><td>SIMPLE~1|SimpleWinIEMS</td><td/><td>0</td><td/></row>
		<row><td>SIMPLEWIN_CLIENT</td><td>SIMPLEWINIEMS</td><td>SIMPLE~1|SimpleWinIEMS</td><td/><td>0</td><td/></row>
		<row><td>SITE</td><td>IMAGES</td><td>site</td><td/><td>0</td><td/></row>
		<row><td>SendToFolder</td><td>TARGETDIR</td><td>.:SendTo</td><td/><td>3</td><td/></row>
		<row><td>StartMenuFolder</td><td>TARGETDIR</td><td>.:STARTM~1|Start Menu</td><td/><td>3</td><td/></row>
		<row><td>StartupFolder</td><td>TARGETDIR</td><td>.:StartUp</td><td/><td>3</td><td/></row>
		<row><td>System16Folder</td><td>TARGETDIR</td><td>.:System</td><td/><td>0</td><td/></row>
		<row><td>System64Folder</td><td>TARGETDIR</td><td>.:System64</td><td/><td>0</td><td/></row>
		<row><td>SystemFolder</td><td>TARGETDIR</td><td>.:System32</td><td/><td>0</td><td/></row>
		<row><td>TARGETDIR</td><td/><td>SourceDir</td><td/><td>0</td><td/></row>
		<row><td>TempFolder</td><td>TARGETDIR</td><td>.:Temp</td><td/><td>0</td><td/></row>
		<row><td>TemplateFolder</td><td>TARGETDIR</td><td>.:ShellNew</td><td/><td>0</td><td/></row>
		<row><td>USERPROFILE</td><td>TARGETDIR</td><td>.:USERPR~1|UserProfile</td><td/><td>0</td><td/></row>
		<row><td>WindowsFolder</td><td>TARGETDIR</td><td>.:Windows</td><td/><td>0</td><td/></row>
		<row><td>WindowsVolume</td><td>TARGETDIR</td><td>.:WinRoot</td><td/><td>0</td><td/></row>
		<row><td>ZH_CHS</td><td>INSTALLDIR</td><td>zh-CHS</td><td/><td>0</td><td/></row>
		<row><td>ZH_HANS</td><td>INSTALLDIR</td><td>zh-Hans</td><td/><td>0</td><td/></row>
		<row><td>ZH_HANT</td><td>INSTALLDIR</td><td>zh-Hant</td><td/><td>0</td><td/></row>
		<row><td>company_name</td><td>ProgramMenuFolder</td><td>회사명</td><td/><td>1</td><td/></row>
		<row><td>lscabl_1_ls_cable___system</td><td>ProgramMenuFolder</td><td>LSCABL~1|LS Cable &amp; System</td><td/><td>1</td><td/></row>
		<row><td>simple_1_simplewin_client</td><td>lscabl_1_ls_cable___system</td><td>SIMPLE~1|SimpleWin Client</td><td/><td>1</td><td/></row>
		<row><td>simple_1_simplewin_iems</td><td>company_name</td><td>SIMPLE~1|SimpleWin IEMS</td><td/><td>1</td><td/></row>
	</table>

	<table name="DrLocator">
		<col key="yes" def="s72">Signature_</col>
		<col key="yes" def="S72">Parent</col>
		<col key="yes" def="S255">Path</col>
		<col def="I2">Depth</col>
	</table>

	<table name="DuplicateFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="s72">File_</col>
		<col def="L255">DestName</col>
		<col def="S72">DestFolder</col>
	</table>

	<table name="Environment">
		<col key="yes" def="s72">Environment</col>
		<col def="l255">Name</col>
		<col def="L255">Value</col>
		<col def="s72">Component_</col>
		<row><td>NewEnvironment1</td><td>##ID_STRING6##</td><td>##ID_STRING7##</td><td>ISX_DEFAULTCOMPONENT</td></row>
		<row><td>NewEnvironment2</td><td>##ID_STRING8##</td><td>##ID_STRING9##</td><td>ISX_DEFAULTCOMPONENT</td></row>
	</table>

	<table name="Error">
		<col key="yes" def="i2">Error</col>
		<col def="L255">Message</col>
		<row><td>0</td><td>##IDS_ERROR_0##</td></row>
		<row><td>1</td><td>##IDS_ERROR_1##</td></row>
		<row><td>10</td><td>##IDS_ERROR_8##</td></row>
		<row><td>11</td><td>##IDS_ERROR_9##</td></row>
		<row><td>1101</td><td>##IDS_ERROR_22##</td></row>
		<row><td>12</td><td>##IDS_ERROR_10##</td></row>
		<row><td>13</td><td>##IDS_ERROR_11##</td></row>
		<row><td>1301</td><td>##IDS_ERROR_23##</td></row>
		<row><td>1302</td><td>##IDS_ERROR_24##</td></row>
		<row><td>1303</td><td>##IDS_ERROR_25##</td></row>
		<row><td>1304</td><td>##IDS_ERROR_26##</td></row>
		<row><td>1305</td><td>##IDS_ERROR_27##</td></row>
		<row><td>1306</td><td>##IDS_ERROR_28##</td></row>
		<row><td>1307</td><td>##IDS_ERROR_29##</td></row>
		<row><td>1308</td><td>##IDS_ERROR_30##</td></row>
		<row><td>1309</td><td>##IDS_ERROR_31##</td></row>
		<row><td>1310</td><td>##IDS_ERROR_32##</td></row>
		<row><td>1311</td><td>##IDS_ERROR_33##</td></row>
		<row><td>1312</td><td>##IDS_ERROR_34##</td></row>
		<row><td>1313</td><td>##IDS_ERROR_35##</td></row>
		<row><td>1314</td><td>##IDS_ERROR_36##</td></row>
		<row><td>1315</td><td>##IDS_ERROR_37##</td></row>
		<row><td>1316</td><td>##IDS_ERROR_38##</td></row>
		<row><td>1317</td><td>##IDS_ERROR_39##</td></row>
		<row><td>1318</td><td>##IDS_ERROR_40##</td></row>
		<row><td>1319</td><td>##IDS_ERROR_41##</td></row>
		<row><td>1320</td><td>##IDS_ERROR_42##</td></row>
		<row><td>1321</td><td>##IDS_ERROR_43##</td></row>
		<row><td>1322</td><td>##IDS_ERROR_44##</td></row>
		<row><td>1323</td><td>##IDS_ERROR_45##</td></row>
		<row><td>1324</td><td>##IDS_ERROR_46##</td></row>
		<row><td>1325</td><td>##IDS_ERROR_47##</td></row>
		<row><td>1326</td><td>##IDS_ERROR_48##</td></row>
		<row><td>1327</td><td>##IDS_ERROR_49##</td></row>
		<row><td>1328</td><td>##IDS_ERROR_122##</td></row>
		<row><td>1329</td><td>##IDS_ERROR_1329##</td></row>
		<row><td>1330</td><td>##IDS_ERROR_1330##</td></row>
		<row><td>1331</td><td>##IDS_ERROR_1331##</td></row>
		<row><td>1332</td><td>##IDS_ERROR_1332##</td></row>
		<row><td>1333</td><td>##IDS_ERROR_1333##</td></row>
		<row><td>1334</td><td>##IDS_ERROR_1334##</td></row>
		<row><td>1335</td><td>##IDS_ERROR_1335##</td></row>
		<row><td>1336</td><td>##IDS_ERROR_1336##</td></row>
		<row><td>14</td><td>##IDS_ERROR_12##</td></row>
		<row><td>1401</td><td>##IDS_ERROR_50##</td></row>
		<row><td>1402</td><td>##IDS_ERROR_51##</td></row>
		<row><td>1403</td><td>##IDS_ERROR_52##</td></row>
		<row><td>1404</td><td>##IDS_ERROR_53##</td></row>
		<row><td>1405</td><td>##IDS_ERROR_54##</td></row>
		<row><td>1406</td><td>##IDS_ERROR_55##</td></row>
		<row><td>1407</td><td>##IDS_ERROR_56##</td></row>
		<row><td>1408</td><td>##IDS_ERROR_57##</td></row>
		<row><td>1409</td><td>##IDS_ERROR_58##</td></row>
		<row><td>1410</td><td>##IDS_ERROR_59##</td></row>
		<row><td>15</td><td>##IDS_ERROR_13##</td></row>
		<row><td>1500</td><td>##IDS_ERROR_60##</td></row>
		<row><td>1501</td><td>##IDS_ERROR_61##</td></row>
		<row><td>1502</td><td>##IDS_ERROR_62##</td></row>
		<row><td>1503</td><td>##IDS_ERROR_63##</td></row>
		<row><td>16</td><td>##IDS_ERROR_14##</td></row>
		<row><td>1601</td><td>##IDS_ERROR_64##</td></row>
		<row><td>1602</td><td>##IDS_ERROR_65##</td></row>
		<row><td>1603</td><td>##IDS_ERROR_66##</td></row>
		<row><td>1604</td><td>##IDS_ERROR_67##</td></row>
		<row><td>1605</td><td>##IDS_ERROR_68##</td></row>
		<row><td>1606</td><td>##IDS_ERROR_69##</td></row>
		<row><td>1607</td><td>##IDS_ERROR_70##</td></row>
		<row><td>1608</td><td>##IDS_ERROR_71##</td></row>
		<row><td>1609</td><td>##IDS_ERROR_1609##</td></row>
		<row><td>1651</td><td>##IDS_ERROR_1651##</td></row>
		<row><td>17</td><td>##IDS_ERROR_15##</td></row>
		<row><td>1701</td><td>##IDS_ERROR_72##</td></row>
		<row><td>1702</td><td>##IDS_ERROR_73##</td></row>
		<row><td>1703</td><td>##IDS_ERROR_74##</td></row>
		<row><td>1704</td><td>##IDS_ERROR_75##</td></row>
		<row><td>1705</td><td>##IDS_ERROR_76##</td></row>
		<row><td>1706</td><td>##IDS_ERROR_77##</td></row>
		<row><td>1707</td><td>##IDS_ERROR_78##</td></row>
		<row><td>1708</td><td>##IDS_ERROR_79##</td></row>
		<row><td>1709</td><td>##IDS_ERROR_80##</td></row>
		<row><td>1710</td><td>##IDS_ERROR_81##</td></row>
		<row><td>1711</td><td>##IDS_ERROR_82##</td></row>
		<row><td>1712</td><td>##IDS_ERROR_83##</td></row>
		<row><td>1713</td><td>##IDS_ERROR_123##</td></row>
		<row><td>1714</td><td>##IDS_ERROR_124##</td></row>
		<row><td>1715</td><td>##IDS_ERROR_1715##</td></row>
		<row><td>1716</td><td>##IDS_ERROR_1716##</td></row>
		<row><td>1717</td><td>##IDS_ERROR_1717##</td></row>
		<row><td>1718</td><td>##IDS_ERROR_1718##</td></row>
		<row><td>1719</td><td>##IDS_ERROR_1719##</td></row>
		<row><td>1720</td><td>##IDS_ERROR_1720##</td></row>
		<row><td>1721</td><td>##IDS_ERROR_1721##</td></row>
		<row><td>1722</td><td>##IDS_ERROR_1722##</td></row>
		<row><td>1723</td><td>##IDS_ERROR_1723##</td></row>
		<row><td>1724</td><td>##IDS_ERROR_1724##</td></row>
		<row><td>1725</td><td>##IDS_ERROR_1725##</td></row>
		<row><td>1726</td><td>##IDS_ERROR_1726##</td></row>
		<row><td>1727</td><td>##IDS_ERROR_1727##</td></row>
		<row><td>1728</td><td>##IDS_ERROR_1728##</td></row>
		<row><td>1729</td><td>##IDS_ERROR_1729##</td></row>
		<row><td>1730</td><td>##IDS_ERROR_1730##</td></row>
		<row><td>1731</td><td>##IDS_ERROR_1731##</td></row>
		<row><td>1732</td><td>##IDS_ERROR_1732##</td></row>
		<row><td>18</td><td>##IDS_ERROR_16##</td></row>
		<row><td>1801</td><td>##IDS_ERROR_84##</td></row>
		<row><td>1802</td><td>##IDS_ERROR_85##</td></row>
		<row><td>1803</td><td>##IDS_ERROR_86##</td></row>
		<row><td>1804</td><td>##IDS_ERROR_87##</td></row>
		<row><td>1805</td><td>##IDS_ERROR_88##</td></row>
		<row><td>1806</td><td>##IDS_ERROR_89##</td></row>
		<row><td>1807</td><td>##IDS_ERROR_90##</td></row>
		<row><td>19</td><td>##IDS_ERROR_17##</td></row>
		<row><td>1901</td><td>##IDS_ERROR_91##</td></row>
		<row><td>1902</td><td>##IDS_ERROR_92##</td></row>
		<row><td>1903</td><td>##IDS_ERROR_93##</td></row>
		<row><td>1904</td><td>##IDS_ERROR_94##</td></row>
		<row><td>1905</td><td>##IDS_ERROR_95##</td></row>
		<row><td>1906</td><td>##IDS_ERROR_96##</td></row>
		<row><td>1907</td><td>##IDS_ERROR_97##</td></row>
		<row><td>1908</td><td>##IDS_ERROR_98##</td></row>
		<row><td>1909</td><td>##IDS_ERROR_99##</td></row>
		<row><td>1910</td><td>##IDS_ERROR_100##</td></row>
		<row><td>1911</td><td>##IDS_ERROR_101##</td></row>
		<row><td>1912</td><td>##IDS_ERROR_102##</td></row>
		<row><td>1913</td><td>##IDS_ERROR_103##</td></row>
		<row><td>1914</td><td>##IDS_ERROR_104##</td></row>
		<row><td>1915</td><td>##IDS_ERROR_105##</td></row>
		<row><td>1916</td><td>##IDS_ERROR_106##</td></row>
		<row><td>1917</td><td>##IDS_ERROR_107##</td></row>
		<row><td>1918</td><td>##IDS_ERROR_108##</td></row>
		<row><td>1919</td><td>##IDS_ERROR_109##</td></row>
		<row><td>1920</td><td>##IDS_ERROR_110##</td></row>
		<row><td>1921</td><td>##IDS_ERROR_111##</td></row>
		<row><td>1922</td><td>##IDS_ERROR_112##</td></row>
		<row><td>1923</td><td>##IDS_ERROR_113##</td></row>
		<row><td>1924</td><td>##IDS_ERROR_114##</td></row>
		<row><td>1925</td><td>##IDS_ERROR_115##</td></row>
		<row><td>1926</td><td>##IDS_ERROR_116##</td></row>
		<row><td>1927</td><td>##IDS_ERROR_117##</td></row>
		<row><td>1928</td><td>##IDS_ERROR_118##</td></row>
		<row><td>1929</td><td>##IDS_ERROR_119##</td></row>
		<row><td>1930</td><td>##IDS_ERROR_125##</td></row>
		<row><td>1931</td><td>##IDS_ERROR_126##</td></row>
		<row><td>1932</td><td>##IDS_ERROR_127##</td></row>
		<row><td>1933</td><td>##IDS_ERROR_128##</td></row>
		<row><td>1934</td><td>##IDS_ERROR_129##</td></row>
		<row><td>1935</td><td>##IDS_ERROR_1935##</td></row>
		<row><td>1936</td><td>##IDS_ERROR_1936##</td></row>
		<row><td>1937</td><td>##IDS_ERROR_1937##</td></row>
		<row><td>1938</td><td>##IDS_ERROR_1938##</td></row>
		<row><td>2</td><td>##IDS_ERROR_2##</td></row>
		<row><td>20</td><td>##IDS_ERROR_18##</td></row>
		<row><td>21</td><td>##IDS_ERROR_19##</td></row>
		<row><td>2101</td><td>##IDS_ERROR_2101##</td></row>
		<row><td>2102</td><td>##IDS_ERROR_2102##</td></row>
		<row><td>2103</td><td>##IDS_ERROR_2103##</td></row>
		<row><td>2104</td><td>##IDS_ERROR_2104##</td></row>
		<row><td>2105</td><td>##IDS_ERROR_2105##</td></row>
		<row><td>2106</td><td>##IDS_ERROR_2106##</td></row>
		<row><td>2107</td><td>##IDS_ERROR_2107##</td></row>
		<row><td>2108</td><td>##IDS_ERROR_2108##</td></row>
		<row><td>2109</td><td>##IDS_ERROR_2109##</td></row>
		<row><td>2110</td><td>##IDS_ERROR_2110##</td></row>
		<row><td>2111</td><td>##IDS_ERROR_2111##</td></row>
		<row><td>2112</td><td>##IDS_ERROR_2112##</td></row>
		<row><td>2113</td><td>##IDS_ERROR_2113##</td></row>
		<row><td>22</td><td>##IDS_ERROR_120##</td></row>
		<row><td>2200</td><td>##IDS_ERROR_2200##</td></row>
		<row><td>2201</td><td>##IDS_ERROR_2201##</td></row>
		<row><td>2202</td><td>##IDS_ERROR_2202##</td></row>
		<row><td>2203</td><td>##IDS_ERROR_2203##</td></row>
		<row><td>2204</td><td>##IDS_ERROR_2204##</td></row>
		<row><td>2205</td><td>##IDS_ERROR_2205##</td></row>
		<row><td>2206</td><td>##IDS_ERROR_2206##</td></row>
		<row><td>2207</td><td>##IDS_ERROR_2207##</td></row>
		<row><td>2208</td><td>##IDS_ERROR_2208##</td></row>
		<row><td>2209</td><td>##IDS_ERROR_2209##</td></row>
		<row><td>2210</td><td>##IDS_ERROR_2210##</td></row>
		<row><td>2211</td><td>##IDS_ERROR_2211##</td></row>
		<row><td>2212</td><td>##IDS_ERROR_2212##</td></row>
		<row><td>2213</td><td>##IDS_ERROR_2213##</td></row>
		<row><td>2214</td><td>##IDS_ERROR_2214##</td></row>
		<row><td>2215</td><td>##IDS_ERROR_2215##</td></row>
		<row><td>2216</td><td>##IDS_ERROR_2216##</td></row>
		<row><td>2217</td><td>##IDS_ERROR_2217##</td></row>
		<row><td>2218</td><td>##IDS_ERROR_2218##</td></row>
		<row><td>2219</td><td>##IDS_ERROR_2219##</td></row>
		<row><td>2220</td><td>##IDS_ERROR_2220##</td></row>
		<row><td>2221</td><td>##IDS_ERROR_2221##</td></row>
		<row><td>2222</td><td>##IDS_ERROR_2222##</td></row>
		<row><td>2223</td><td>##IDS_ERROR_2223##</td></row>
		<row><td>2224</td><td>##IDS_ERROR_2224##</td></row>
		<row><td>2225</td><td>##IDS_ERROR_2225##</td></row>
		<row><td>2226</td><td>##IDS_ERROR_2226##</td></row>
		<row><td>2227</td><td>##IDS_ERROR_2227##</td></row>
		<row><td>2228</td><td>##IDS_ERROR_2228##</td></row>
		<row><td>2229</td><td>##IDS_ERROR_2229##</td></row>
		<row><td>2230</td><td>##IDS_ERROR_2230##</td></row>
		<row><td>2231</td><td>##IDS_ERROR_2231##</td></row>
		<row><td>2232</td><td>##IDS_ERROR_2232##</td></row>
		<row><td>2233</td><td>##IDS_ERROR_2233##</td></row>
		<row><td>2234</td><td>##IDS_ERROR_2234##</td></row>
		<row><td>2235</td><td>##IDS_ERROR_2235##</td></row>
		<row><td>2236</td><td>##IDS_ERROR_2236##</td></row>
		<row><td>2237</td><td>##IDS_ERROR_2237##</td></row>
		<row><td>2238</td><td>##IDS_ERROR_2238##</td></row>
		<row><td>2239</td><td>##IDS_ERROR_2239##</td></row>
		<row><td>2240</td><td>##IDS_ERROR_2240##</td></row>
		<row><td>2241</td><td>##IDS_ERROR_2241##</td></row>
		<row><td>2242</td><td>##IDS_ERROR_2242##</td></row>
		<row><td>2243</td><td>##IDS_ERROR_2243##</td></row>
		<row><td>2244</td><td>##IDS_ERROR_2244##</td></row>
		<row><td>2245</td><td>##IDS_ERROR_2245##</td></row>
		<row><td>2246</td><td>##IDS_ERROR_2246##</td></row>
		<row><td>2247</td><td>##IDS_ERROR_2247##</td></row>
		<row><td>2248</td><td>##IDS_ERROR_2248##</td></row>
		<row><td>2249</td><td>##IDS_ERROR_2249##</td></row>
		<row><td>2250</td><td>##IDS_ERROR_2250##</td></row>
		<row><td>2251</td><td>##IDS_ERROR_2251##</td></row>
		<row><td>2252</td><td>##IDS_ERROR_2252##</td></row>
		<row><td>2253</td><td>##IDS_ERROR_2253##</td></row>
		<row><td>2254</td><td>##IDS_ERROR_2254##</td></row>
		<row><td>2255</td><td>##IDS_ERROR_2255##</td></row>
		<row><td>2256</td><td>##IDS_ERROR_2256##</td></row>
		<row><td>2257</td><td>##IDS_ERROR_2257##</td></row>
		<row><td>2258</td><td>##IDS_ERROR_2258##</td></row>
		<row><td>2259</td><td>##IDS_ERROR_2259##</td></row>
		<row><td>2260</td><td>##IDS_ERROR_2260##</td></row>
		<row><td>2261</td><td>##IDS_ERROR_2261##</td></row>
		<row><td>2262</td><td>##IDS_ERROR_2262##</td></row>
		<row><td>2263</td><td>##IDS_ERROR_2263##</td></row>
		<row><td>2264</td><td>##IDS_ERROR_2264##</td></row>
		<row><td>2265</td><td>##IDS_ERROR_2265##</td></row>
		<row><td>2266</td><td>##IDS_ERROR_2266##</td></row>
		<row><td>2267</td><td>##IDS_ERROR_2267##</td></row>
		<row><td>2268</td><td>##IDS_ERROR_2268##</td></row>
		<row><td>2269</td><td>##IDS_ERROR_2269##</td></row>
		<row><td>2270</td><td>##IDS_ERROR_2270##</td></row>
		<row><td>2271</td><td>##IDS_ERROR_2271##</td></row>
		<row><td>2272</td><td>##IDS_ERROR_2272##</td></row>
		<row><td>2273</td><td>##IDS_ERROR_2273##</td></row>
		<row><td>2274</td><td>##IDS_ERROR_2274##</td></row>
		<row><td>2275</td><td>##IDS_ERROR_2275##</td></row>
		<row><td>2276</td><td>##IDS_ERROR_2276##</td></row>
		<row><td>2277</td><td>##IDS_ERROR_2277##</td></row>
		<row><td>2278</td><td>##IDS_ERROR_2278##</td></row>
		<row><td>2279</td><td>##IDS_ERROR_2279##</td></row>
		<row><td>2280</td><td>##IDS_ERROR_2280##</td></row>
		<row><td>2281</td><td>##IDS_ERROR_2281##</td></row>
		<row><td>2282</td><td>##IDS_ERROR_2282##</td></row>
		<row><td>23</td><td>##IDS_ERROR_121##</td></row>
		<row><td>2302</td><td>##IDS_ERROR_2302##</td></row>
		<row><td>2303</td><td>##IDS_ERROR_2303##</td></row>
		<row><td>2304</td><td>##IDS_ERROR_2304##</td></row>
		<row><td>2305</td><td>##IDS_ERROR_2305##</td></row>
		<row><td>2306</td><td>##IDS_ERROR_2306##</td></row>
		<row><td>2307</td><td>##IDS_ERROR_2307##</td></row>
		<row><td>2308</td><td>##IDS_ERROR_2308##</td></row>
		<row><td>2309</td><td>##IDS_ERROR_2309##</td></row>
		<row><td>2310</td><td>##IDS_ERROR_2310##</td></row>
		<row><td>2315</td><td>##IDS_ERROR_2315##</td></row>
		<row><td>2318</td><td>##IDS_ERROR_2318##</td></row>
		<row><td>2319</td><td>##IDS_ERROR_2319##</td></row>
		<row><td>2320</td><td>##IDS_ERROR_2320##</td></row>
		<row><td>2321</td><td>##IDS_ERROR_2321##</td></row>
		<row><td>2322</td><td>##IDS_ERROR_2322##</td></row>
		<row><td>2323</td><td>##IDS_ERROR_2323##</td></row>
		<row><td>2324</td><td>##IDS_ERROR_2324##</td></row>
		<row><td>2325</td><td>##IDS_ERROR_2325##</td></row>
		<row><td>2326</td><td>##IDS_ERROR_2326##</td></row>
		<row><td>2327</td><td>##IDS_ERROR_2327##</td></row>
		<row><td>2328</td><td>##IDS_ERROR_2328##</td></row>
		<row><td>2329</td><td>##IDS_ERROR_2329##</td></row>
		<row><td>2330</td><td>##IDS_ERROR_2330##</td></row>
		<row><td>2331</td><td>##IDS_ERROR_2331##</td></row>
		<row><td>2332</td><td>##IDS_ERROR_2332##</td></row>
		<row><td>2333</td><td>##IDS_ERROR_2333##</td></row>
		<row><td>2334</td><td>##IDS_ERROR_2334##</td></row>
		<row><td>2335</td><td>##IDS_ERROR_2335##</td></row>
		<row><td>2336</td><td>##IDS_ERROR_2336##</td></row>
		<row><td>2337</td><td>##IDS_ERROR_2337##</td></row>
		<row><td>2338</td><td>##IDS_ERROR_2338##</td></row>
		<row><td>2339</td><td>##IDS_ERROR_2339##</td></row>
		<row><td>2340</td><td>##IDS_ERROR_2340##</td></row>
		<row><td>2341</td><td>##IDS_ERROR_2341##</td></row>
		<row><td>2342</td><td>##IDS_ERROR_2342##</td></row>
		<row><td>2343</td><td>##IDS_ERROR_2343##</td></row>
		<row><td>2344</td><td>##IDS_ERROR_2344##</td></row>
		<row><td>2345</td><td>##IDS_ERROR_2345##</td></row>
		<row><td>2347</td><td>##IDS_ERROR_2347##</td></row>
		<row><td>2348</td><td>##IDS_ERROR_2348##</td></row>
		<row><td>2349</td><td>##IDS_ERROR_2349##</td></row>
		<row><td>2350</td><td>##IDS_ERROR_2350##</td></row>
		<row><td>2351</td><td>##IDS_ERROR_2351##</td></row>
		<row><td>2352</td><td>##IDS_ERROR_2352##</td></row>
		<row><td>2353</td><td>##IDS_ERROR_2353##</td></row>
		<row><td>2354</td><td>##IDS_ERROR_2354##</td></row>
		<row><td>2355</td><td>##IDS_ERROR_2355##</td></row>
		<row><td>2356</td><td>##IDS_ERROR_2356##</td></row>
		<row><td>2357</td><td>##IDS_ERROR_2357##</td></row>
		<row><td>2358</td><td>##IDS_ERROR_2358##</td></row>
		<row><td>2359</td><td>##IDS_ERROR_2359##</td></row>
		<row><td>2360</td><td>##IDS_ERROR_2360##</td></row>
		<row><td>2361</td><td>##IDS_ERROR_2361##</td></row>
		<row><td>2362</td><td>##IDS_ERROR_2362##</td></row>
		<row><td>2363</td><td>##IDS_ERROR_2363##</td></row>
		<row><td>2364</td><td>##IDS_ERROR_2364##</td></row>
		<row><td>2365</td><td>##IDS_ERROR_2365##</td></row>
		<row><td>2366</td><td>##IDS_ERROR_2366##</td></row>
		<row><td>2367</td><td>##IDS_ERROR_2367##</td></row>
		<row><td>2368</td><td>##IDS_ERROR_2368##</td></row>
		<row><td>2370</td><td>##IDS_ERROR_2370##</td></row>
		<row><td>2371</td><td>##IDS_ERROR_2371##</td></row>
		<row><td>2372</td><td>##IDS_ERROR_2372##</td></row>
		<row><td>2373</td><td>##IDS_ERROR_2373##</td></row>
		<row><td>2374</td><td>##IDS_ERROR_2374##</td></row>
		<row><td>2375</td><td>##IDS_ERROR_2375##</td></row>
		<row><td>2376</td><td>##IDS_ERROR_2376##</td></row>
		<row><td>2379</td><td>##IDS_ERROR_2379##</td></row>
		<row><td>2380</td><td>##IDS_ERROR_2380##</td></row>
		<row><td>2381</td><td>##IDS_ERROR_2381##</td></row>
		<row><td>2382</td><td>##IDS_ERROR_2382##</td></row>
		<row><td>2401</td><td>##IDS_ERROR_2401##</td></row>
		<row><td>2402</td><td>##IDS_ERROR_2402##</td></row>
		<row><td>2501</td><td>##IDS_ERROR_2501##</td></row>
		<row><td>2502</td><td>##IDS_ERROR_2502##</td></row>
		<row><td>2503</td><td>##IDS_ERROR_2503##</td></row>
		<row><td>2601</td><td>##IDS_ERROR_2601##</td></row>
		<row><td>2602</td><td>##IDS_ERROR_2602##</td></row>
		<row><td>2603</td><td>##IDS_ERROR_2603##</td></row>
		<row><td>2604</td><td>##IDS_ERROR_2604##</td></row>
		<row><td>2605</td><td>##IDS_ERROR_2605##</td></row>
		<row><td>2606</td><td>##IDS_ERROR_2606##</td></row>
		<row><td>2607</td><td>##IDS_ERROR_2607##</td></row>
		<row><td>2608</td><td>##IDS_ERROR_2608##</td></row>
		<row><td>2609</td><td>##IDS_ERROR_2609##</td></row>
		<row><td>2611</td><td>##IDS_ERROR_2611##</td></row>
		<row><td>2612</td><td>##IDS_ERROR_2612##</td></row>
		<row><td>2613</td><td>##IDS_ERROR_2613##</td></row>
		<row><td>2614</td><td>##IDS_ERROR_2614##</td></row>
		<row><td>2615</td><td>##IDS_ERROR_2615##</td></row>
		<row><td>2616</td><td>##IDS_ERROR_2616##</td></row>
		<row><td>2617</td><td>##IDS_ERROR_2617##</td></row>
		<row><td>2618</td><td>##IDS_ERROR_2618##</td></row>
		<row><td>2619</td><td>##IDS_ERROR_2619##</td></row>
		<row><td>2620</td><td>##IDS_ERROR_2620##</td></row>
		<row><td>2621</td><td>##IDS_ERROR_2621##</td></row>
		<row><td>2701</td><td>##IDS_ERROR_2701##</td></row>
		<row><td>2702</td><td>##IDS_ERROR_2702##</td></row>
		<row><td>2703</td><td>##IDS_ERROR_2703##</td></row>
		<row><td>2704</td><td>##IDS_ERROR_2704##</td></row>
		<row><td>2705</td><td>##IDS_ERROR_2705##</td></row>
		<row><td>2706</td><td>##IDS_ERROR_2706##</td></row>
		<row><td>2707</td><td>##IDS_ERROR_2707##</td></row>
		<row><td>2708</td><td>##IDS_ERROR_2708##</td></row>
		<row><td>2709</td><td>##IDS_ERROR_2709##</td></row>
		<row><td>2710</td><td>##IDS_ERROR_2710##</td></row>
		<row><td>2711</td><td>##IDS_ERROR_2711##</td></row>
		<row><td>2712</td><td>##IDS_ERROR_2712##</td></row>
		<row><td>2713</td><td>##IDS_ERROR_2713##</td></row>
		<row><td>2714</td><td>##IDS_ERROR_2714##</td></row>
		<row><td>2715</td><td>##IDS_ERROR_2715##</td></row>
		<row><td>2716</td><td>##IDS_ERROR_2716##</td></row>
		<row><td>2717</td><td>##IDS_ERROR_2717##</td></row>
		<row><td>2718</td><td>##IDS_ERROR_2718##</td></row>
		<row><td>2719</td><td>##IDS_ERROR_2719##</td></row>
		<row><td>2720</td><td>##IDS_ERROR_2720##</td></row>
		<row><td>2721</td><td>##IDS_ERROR_2721##</td></row>
		<row><td>2722</td><td>##IDS_ERROR_2722##</td></row>
		<row><td>2723</td><td>##IDS_ERROR_2723##</td></row>
		<row><td>2724</td><td>##IDS_ERROR_2724##</td></row>
		<row><td>2725</td><td>##IDS_ERROR_2725##</td></row>
		<row><td>2726</td><td>##IDS_ERROR_2726##</td></row>
		<row><td>2727</td><td>##IDS_ERROR_2727##</td></row>
		<row><td>2728</td><td>##IDS_ERROR_2728##</td></row>
		<row><td>2729</td><td>##IDS_ERROR_2729##</td></row>
		<row><td>2730</td><td>##IDS_ERROR_2730##</td></row>
		<row><td>2731</td><td>##IDS_ERROR_2731##</td></row>
		<row><td>2732</td><td>##IDS_ERROR_2732##</td></row>
		<row><td>2733</td><td>##IDS_ERROR_2733##</td></row>
		<row><td>2734</td><td>##IDS_ERROR_2734##</td></row>
		<row><td>2735</td><td>##IDS_ERROR_2735##</td></row>
		<row><td>2736</td><td>##IDS_ERROR_2736##</td></row>
		<row><td>2737</td><td>##IDS_ERROR_2737##</td></row>
		<row><td>2738</td><td>##IDS_ERROR_2738##</td></row>
		<row><td>2739</td><td>##IDS_ERROR_2739##</td></row>
		<row><td>2740</td><td>##IDS_ERROR_2740##</td></row>
		<row><td>2741</td><td>##IDS_ERROR_2741##</td></row>
		<row><td>2742</td><td>##IDS_ERROR_2742##</td></row>
		<row><td>2743</td><td>##IDS_ERROR_2743##</td></row>
		<row><td>2744</td><td>##IDS_ERROR_2744##</td></row>
		<row><td>2745</td><td>##IDS_ERROR_2745##</td></row>
		<row><td>2746</td><td>##IDS_ERROR_2746##</td></row>
		<row><td>2747</td><td>##IDS_ERROR_2747##</td></row>
		<row><td>2748</td><td>##IDS_ERROR_2748##</td></row>
		<row><td>2749</td><td>##IDS_ERROR_2749##</td></row>
		<row><td>2750</td><td>##IDS_ERROR_2750##</td></row>
		<row><td>27500</td><td>##IDS_ERROR_130##</td></row>
		<row><td>27501</td><td>##IDS_ERROR_131##</td></row>
		<row><td>27502</td><td>##IDS_ERROR_27502##</td></row>
		<row><td>27503</td><td>##IDS_ERROR_27503##</td></row>
		<row><td>27504</td><td>##IDS_ERROR_27504##</td></row>
		<row><td>27505</td><td>##IDS_ERROR_27505##</td></row>
		<row><td>27506</td><td>##IDS_ERROR_27506##</td></row>
		<row><td>27507</td><td>##IDS_ERROR_27507##</td></row>
		<row><td>27508</td><td>##IDS_ERROR_27508##</td></row>
		<row><td>27509</td><td>##IDS_ERROR_27509##</td></row>
		<row><td>2751</td><td>##IDS_ERROR_2751##</td></row>
		<row><td>27510</td><td>##IDS_ERROR_27510##</td></row>
		<row><td>27511</td><td>##IDS_ERROR_27511##</td></row>
		<row><td>27512</td><td>##IDS_ERROR_27512##</td></row>
		<row><td>27513</td><td>##IDS_ERROR_27513##</td></row>
		<row><td>27514</td><td>##IDS_ERROR_27514##</td></row>
		<row><td>27515</td><td>##IDS_ERROR_27515##</td></row>
		<row><td>27516</td><td>##IDS_ERROR_27516##</td></row>
		<row><td>27517</td><td>##IDS_ERROR_27517##</td></row>
		<row><td>27518</td><td>##IDS_ERROR_27518##</td></row>
		<row><td>27519</td><td>##IDS_ERROR_27519##</td></row>
		<row><td>2752</td><td>##IDS_ERROR_2752##</td></row>
		<row><td>27520</td><td>##IDS_ERROR_27520##</td></row>
		<row><td>27521</td><td>##IDS_ERROR_27521##</td></row>
		<row><td>27522</td><td>##IDS_ERROR_27522##</td></row>
		<row><td>27523</td><td>##IDS_ERROR_27523##</td></row>
		<row><td>27524</td><td>##IDS_ERROR_27524##</td></row>
		<row><td>27525</td><td>##IDS_ERROR_27525##</td></row>
		<row><td>27526</td><td>##IDS_ERROR_27526##</td></row>
		<row><td>27527</td><td>##IDS_ERROR_27527##</td></row>
		<row><td>27528</td><td>##IDS_ERROR_27528##</td></row>
		<row><td>27529</td><td>##IDS_ERROR_27529##</td></row>
		<row><td>2753</td><td>##IDS_ERROR_2753##</td></row>
		<row><td>27530</td><td>##IDS_ERROR_27530##</td></row>
		<row><td>27531</td><td>##IDS_ERROR_27531##</td></row>
		<row><td>27532</td><td>##IDS_ERROR_27532##</td></row>
		<row><td>27533</td><td>##IDS_ERROR_27533##</td></row>
		<row><td>27534</td><td>##IDS_ERROR_27534##</td></row>
		<row><td>27535</td><td>##IDS_ERROR_27535##</td></row>
		<row><td>27536</td><td>##IDS_ERROR_27536##</td></row>
		<row><td>27537</td><td>##IDS_ERROR_27537##</td></row>
		<row><td>27538</td><td>##IDS_ERROR_27538##</td></row>
		<row><td>27539</td><td>##IDS_ERROR_27539##</td></row>
		<row><td>2754</td><td>##IDS_ERROR_2754##</td></row>
		<row><td>27540</td><td>##IDS_ERROR_27540##</td></row>
		<row><td>27541</td><td>##IDS_ERROR_27541##</td></row>
		<row><td>27542</td><td>##IDS_ERROR_27542##</td></row>
		<row><td>27543</td><td>##IDS_ERROR_27543##</td></row>
		<row><td>27544</td><td>##IDS_ERROR_27544##</td></row>
		<row><td>27545</td><td>##IDS_ERROR_27545##</td></row>
		<row><td>27546</td><td>##IDS_ERROR_27546##</td></row>
		<row><td>27547</td><td>##IDS_ERROR_27547##</td></row>
		<row><td>27548</td><td>##IDS_ERROR_27548##</td></row>
		<row><td>27549</td><td>##IDS_ERROR_27549##</td></row>
		<row><td>2755</td><td>##IDS_ERROR_2755##</td></row>
		<row><td>27550</td><td>##IDS_ERROR_27550##</td></row>
		<row><td>27551</td><td>##IDS_ERROR_27551##</td></row>
		<row><td>27552</td><td>##IDS_ERROR_27552##</td></row>
		<row><td>27553</td><td>##IDS_ERROR_27553##</td></row>
		<row><td>27554</td><td>##IDS_ERROR_27554##</td></row>
		<row><td>27555</td><td>##IDS_ERROR_27555##</td></row>
		<row><td>2756</td><td>##IDS_ERROR_2756##</td></row>
		<row><td>2757</td><td>##IDS_ERROR_2757##</td></row>
		<row><td>2758</td><td>##IDS_ERROR_2758##</td></row>
		<row><td>2759</td><td>##IDS_ERROR_2759##</td></row>
		<row><td>2760</td><td>##IDS_ERROR_2760##</td></row>
		<row><td>2761</td><td>##IDS_ERROR_2761##</td></row>
		<row><td>2762</td><td>##IDS_ERROR_2762##</td></row>
		<row><td>2763</td><td>##IDS_ERROR_2763##</td></row>
		<row><td>2765</td><td>##IDS_ERROR_2765##</td></row>
		<row><td>2766</td><td>##IDS_ERROR_2766##</td></row>
		<row><td>2767</td><td>##IDS_ERROR_2767##</td></row>
		<row><td>2768</td><td>##IDS_ERROR_2768##</td></row>
		<row><td>2769</td><td>##IDS_ERROR_2769##</td></row>
		<row><td>2770</td><td>##IDS_ERROR_2770##</td></row>
		<row><td>2771</td><td>##IDS_ERROR_2771##</td></row>
		<row><td>2772</td><td>##IDS_ERROR_2772##</td></row>
		<row><td>2801</td><td>##IDS_ERROR_2801##</td></row>
		<row><td>2802</td><td>##IDS_ERROR_2802##</td></row>
		<row><td>2803</td><td>##IDS_ERROR_2803##</td></row>
		<row><td>2804</td><td>##IDS_ERROR_2804##</td></row>
		<row><td>2806</td><td>##IDS_ERROR_2806##</td></row>
		<row><td>2807</td><td>##IDS_ERROR_2807##</td></row>
		<row><td>2808</td><td>##IDS_ERROR_2808##</td></row>
		<row><td>2809</td><td>##IDS_ERROR_2809##</td></row>
		<row><td>2810</td><td>##IDS_ERROR_2810##</td></row>
		<row><td>2811</td><td>##IDS_ERROR_2811##</td></row>
		<row><td>2812</td><td>##IDS_ERROR_2812##</td></row>
		<row><td>2813</td><td>##IDS_ERROR_2813##</td></row>
		<row><td>2814</td><td>##IDS_ERROR_2814##</td></row>
		<row><td>2815</td><td>##IDS_ERROR_2815##</td></row>
		<row><td>2816</td><td>##IDS_ERROR_2816##</td></row>
		<row><td>2817</td><td>##IDS_ERROR_2817##</td></row>
		<row><td>2818</td><td>##IDS_ERROR_2818##</td></row>
		<row><td>2819</td><td>##IDS_ERROR_2819##</td></row>
		<row><td>2820</td><td>##IDS_ERROR_2820##</td></row>
		<row><td>2821</td><td>##IDS_ERROR_2821##</td></row>
		<row><td>2822</td><td>##IDS_ERROR_2822##</td></row>
		<row><td>2823</td><td>##IDS_ERROR_2823##</td></row>
		<row><td>2824</td><td>##IDS_ERROR_2824##</td></row>
		<row><td>2825</td><td>##IDS_ERROR_2825##</td></row>
		<row><td>2826</td><td>##IDS_ERROR_2826##</td></row>
		<row><td>2827</td><td>##IDS_ERROR_2827##</td></row>
		<row><td>2828</td><td>##IDS_ERROR_2828##</td></row>
		<row><td>2829</td><td>##IDS_ERROR_2829##</td></row>
		<row><td>2830</td><td>##IDS_ERROR_2830##</td></row>
		<row><td>2831</td><td>##IDS_ERROR_2831##</td></row>
		<row><td>2832</td><td>##IDS_ERROR_2832##</td></row>
		<row><td>2833</td><td>##IDS_ERROR_2833##</td></row>
		<row><td>2834</td><td>##IDS_ERROR_2834##</td></row>
		<row><td>2835</td><td>##IDS_ERROR_2835##</td></row>
		<row><td>2836</td><td>##IDS_ERROR_2836##</td></row>
		<row><td>2837</td><td>##IDS_ERROR_2837##</td></row>
		<row><td>2838</td><td>##IDS_ERROR_2838##</td></row>
		<row><td>2839</td><td>##IDS_ERROR_2839##</td></row>
		<row><td>2840</td><td>##IDS_ERROR_2840##</td></row>
		<row><td>2841</td><td>##IDS_ERROR_2841##</td></row>
		<row><td>2842</td><td>##IDS_ERROR_2842##</td></row>
		<row><td>2843</td><td>##IDS_ERROR_2843##</td></row>
		<row><td>2844</td><td>##IDS_ERROR_2844##</td></row>
		<row><td>2845</td><td>##IDS_ERROR_2845##</td></row>
		<row><td>2846</td><td>##IDS_ERROR_2846##</td></row>
		<row><td>2847</td><td>##IDS_ERROR_2847##</td></row>
		<row><td>2848</td><td>##IDS_ERROR_2848##</td></row>
		<row><td>2849</td><td>##IDS_ERROR_2849##</td></row>
		<row><td>2850</td><td>##IDS_ERROR_2850##</td></row>
		<row><td>2851</td><td>##IDS_ERROR_2851##</td></row>
		<row><td>2852</td><td>##IDS_ERROR_2852##</td></row>
		<row><td>2853</td><td>##IDS_ERROR_2853##</td></row>
		<row><td>2854</td><td>##IDS_ERROR_2854##</td></row>
		<row><td>2855</td><td>##IDS_ERROR_2855##</td></row>
		<row><td>2856</td><td>##IDS_ERROR_2856##</td></row>
		<row><td>2857</td><td>##IDS_ERROR_2857##</td></row>
		<row><td>2858</td><td>##IDS_ERROR_2858##</td></row>
		<row><td>2859</td><td>##IDS_ERROR_2859##</td></row>
		<row><td>2860</td><td>##IDS_ERROR_2860##</td></row>
		<row><td>2861</td><td>##IDS_ERROR_2861##</td></row>
		<row><td>2862</td><td>##IDS_ERROR_2862##</td></row>
		<row><td>2863</td><td>##IDS_ERROR_2863##</td></row>
		<row><td>2864</td><td>##IDS_ERROR_2864##</td></row>
		<row><td>2865</td><td>##IDS_ERROR_2865##</td></row>
		<row><td>2866</td><td>##IDS_ERROR_2866##</td></row>
		<row><td>2867</td><td>##IDS_ERROR_2867##</td></row>
		<row><td>2868</td><td>##IDS_ERROR_2868##</td></row>
		<row><td>2869</td><td>##IDS_ERROR_2869##</td></row>
		<row><td>2870</td><td>##IDS_ERROR_2870##</td></row>
		<row><td>2871</td><td>##IDS_ERROR_2871##</td></row>
		<row><td>2872</td><td>##IDS_ERROR_2872##</td></row>
		<row><td>2873</td><td>##IDS_ERROR_2873##</td></row>
		<row><td>2874</td><td>##IDS_ERROR_2874##</td></row>
		<row><td>2875</td><td>##IDS_ERROR_2875##</td></row>
		<row><td>2876</td><td>##IDS_ERROR_2876##</td></row>
		<row><td>2877</td><td>##IDS_ERROR_2877##</td></row>
		<row><td>2878</td><td>##IDS_ERROR_2878##</td></row>
		<row><td>2879</td><td>##IDS_ERROR_2879##</td></row>
		<row><td>2880</td><td>##IDS_ERROR_2880##</td></row>
		<row><td>2881</td><td>##IDS_ERROR_2881##</td></row>
		<row><td>2882</td><td>##IDS_ERROR_2882##</td></row>
		<row><td>2883</td><td>##IDS_ERROR_2883##</td></row>
		<row><td>2884</td><td>##IDS_ERROR_2884##</td></row>
		<row><td>2885</td><td>##IDS_ERROR_2885##</td></row>
		<row><td>2886</td><td>##IDS_ERROR_2886##</td></row>
		<row><td>2887</td><td>##IDS_ERROR_2887##</td></row>
		<row><td>2888</td><td>##IDS_ERROR_2888##</td></row>
		<row><td>2889</td><td>##IDS_ERROR_2889##</td></row>
		<row><td>2890</td><td>##IDS_ERROR_2890##</td></row>
		<row><td>2891</td><td>##IDS_ERROR_2891##</td></row>
		<row><td>2892</td><td>##IDS_ERROR_2892##</td></row>
		<row><td>2893</td><td>##IDS_ERROR_2893##</td></row>
		<row><td>2894</td><td>##IDS_ERROR_2894##</td></row>
		<row><td>2895</td><td>##IDS_ERROR_2895##</td></row>
		<row><td>2896</td><td>##IDS_ERROR_2896##</td></row>
		<row><td>2897</td><td>##IDS_ERROR_2897##</td></row>
		<row><td>2898</td><td>##IDS_ERROR_2898##</td></row>
		<row><td>2899</td><td>##IDS_ERROR_2899##</td></row>
		<row><td>2901</td><td>##IDS_ERROR_2901##</td></row>
		<row><td>2902</td><td>##IDS_ERROR_2902##</td></row>
		<row><td>2903</td><td>##IDS_ERROR_2903##</td></row>
		<row><td>2904</td><td>##IDS_ERROR_2904##</td></row>
		<row><td>2905</td><td>##IDS_ERROR_2905##</td></row>
		<row><td>2906</td><td>##IDS_ERROR_2906##</td></row>
		<row><td>2907</td><td>##IDS_ERROR_2907##</td></row>
		<row><td>2908</td><td>##IDS_ERROR_2908##</td></row>
		<row><td>2909</td><td>##IDS_ERROR_2909##</td></row>
		<row><td>2910</td><td>##IDS_ERROR_2910##</td></row>
		<row><td>2911</td><td>##IDS_ERROR_2911##</td></row>
		<row><td>2912</td><td>##IDS_ERROR_2912##</td></row>
		<row><td>2919</td><td>##IDS_ERROR_2919##</td></row>
		<row><td>2920</td><td>##IDS_ERROR_2920##</td></row>
		<row><td>2924</td><td>##IDS_ERROR_2924##</td></row>
		<row><td>2927</td><td>##IDS_ERROR_2927##</td></row>
		<row><td>2928</td><td>##IDS_ERROR_2928##</td></row>
		<row><td>2929</td><td>##IDS_ERROR_2929##</td></row>
		<row><td>2932</td><td>##IDS_ERROR_2932##</td></row>
		<row><td>2933</td><td>##IDS_ERROR_2933##</td></row>
		<row><td>2934</td><td>##IDS_ERROR_2934##</td></row>
		<row><td>2935</td><td>##IDS_ERROR_2935##</td></row>
		<row><td>2936</td><td>##IDS_ERROR_2936##</td></row>
		<row><td>2937</td><td>##IDS_ERROR_2937##</td></row>
		<row><td>2938</td><td>##IDS_ERROR_2938##</td></row>
		<row><td>2939</td><td>##IDS_ERROR_2939##</td></row>
		<row><td>2940</td><td>##IDS_ERROR_2940##</td></row>
		<row><td>2941</td><td>##IDS_ERROR_2941##</td></row>
		<row><td>2942</td><td>##IDS_ERROR_2942##</td></row>
		<row><td>2943</td><td>##IDS_ERROR_2943##</td></row>
		<row><td>2944</td><td>##IDS_ERROR_2944##</td></row>
		<row><td>2945</td><td>##IDS_ERROR_2945##</td></row>
		<row><td>3001</td><td>##IDS_ERROR_3001##</td></row>
		<row><td>3002</td><td>##IDS_ERROR_3002##</td></row>
		<row><td>32</td><td>##IDS_ERROR_20##</td></row>
		<row><td>33</td><td>##IDS_ERROR_21##</td></row>
		<row><td>4</td><td>##IDS_ERROR_3##</td></row>
		<row><td>5</td><td>##IDS_ERROR_4##</td></row>
		<row><td>7</td><td>##IDS_ERROR_5##</td></row>
		<row><td>8</td><td>##IDS_ERROR_6##</td></row>
		<row><td>9</td><td>##IDS_ERROR_7##</td></row>
	</table>

	<table name="EventMapping">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Event</col>
		<col def="s50">Attribute</col>
		<row><td>CustomSetup</td><td>ItemDescription</td><td>SelectionDescription</td><td>Text</td></row>
		<row><td>CustomSetup</td><td>Location</td><td>SelectionPath</td><td>Text</td></row>
		<row><td>CustomSetup</td><td>Size</td><td>SelectionSize</td><td>Text</td></row>
		<row><td>SetupInitialization</td><td>ActionData</td><td>ActionData</td><td>Text</td></row>
		<row><td>SetupInitialization</td><td>ActionText</td><td>ActionText</td><td>Text</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>AdminInstallFinalize</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>InstallFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>MoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>RemoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>RemoveRegistryValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>SetProgress</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>UnmoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>WriteIniValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>WriteRegistryValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionText</td><td>ActionText</td><td>Text</td></row>
	</table>

	<table name="Extension">
		<col key="yes" def="s255">Extension</col>
		<col key="yes" def="s72">Component_</col>
		<col def="S255">ProgId_</col>
		<col def="S64">MIME_</col>
		<col def="s38">Feature_</col>
	</table>

	<table name="Feature">
		<col key="yes" def="s38">Feature</col>
		<col def="S38">Feature_Parent</col>
		<col def="L64">Title</col>
		<col def="L255">Description</col>
		<col def="I2">Display</col>
		<col def="i2">Level</col>
		<col def="S72">Directory_</col>
		<col def="i2">Attributes</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISFeatureCabName</col>
		<col def="S255">ISProFeatureName</col>
		<row><td>AlwaysInstall</td><td/><td>##DN_AlwaysInstall##</td><td>Enter the description for this feature here.</td><td>0</td><td>1</td><td>INSTALLDIR</td><td>16</td><td/><td>Enter comments regarding this feature here.</td><td/><td/></row>
	</table>

	<table name="FeatureComponents">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">Component_</col>
		<row><td>AlwaysInstall</td><td>ConfigS.EXE</td></row>
		<row><td>AlwaysInstall</td><td>EntityFramework.SqlServer.dll</td></row>
		<row><td>AlwaysInstall</td><td>EntityFramework.dll</td></row>
		<row><td>AlwaysInstall</td><td>EntityFramework.resources.dll</td></row>
		<row><td>AlwaysInstall</td><td>I2MS2.exe</td></row>
		<row><td>AlwaysInstall</td><td>I2MS2.vshost.exe</td></row>
		<row><td>AlwaysInstall</td><td>I2MS2RS.EXE</td></row>
		<row><td>AlwaysInstall</td><td>I2MSR.dll</td></row>
		<row><td>AlwaysInstall</td><td>I2MSR.resources.dll</td></row>
		<row><td>AlwaysInstall</td><td>I2MSR.resources.dll1</td></row>
		<row><td>AlwaysInstall</td><td>IEMS.EXE</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT1</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT10</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT11</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT13</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT14</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT15</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT16</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT17</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT18</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT19</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT2</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT20</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT21</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT22</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT23</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT24</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT25</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT26</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT27</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT28</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT29</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT3</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT30</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT31</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT32</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT33</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT5</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT6</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT7</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT8</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT9</td></row>
		<row><td>AlwaysInstall</td><td>MahApps.Metro.Resources.dll</td></row>
		<row><td>AlwaysInstall</td><td>MahApps.Metro.dll</td></row>
		<row><td>AlwaysInstall</td><td>MetroChart.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.AspNet.SignalR.Client.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.AspNet.SignalR.Core.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.AspNet.SignalR.SystemWeb.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll2</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll3</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll4</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll5</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll6</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll7</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll8</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Effects.resources.dll9</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll10</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll2</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll3</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll4</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll5</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll6</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll7</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll8</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Expression.Interactions.resources.dll9</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Owin.Cors.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Owin.Host.HttpListener.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Owin.Host.SystemWeb.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Owin.Hosting.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Owin.Security.dll</td></row>
		<row><td>AlwaysInstall</td><td>Microsoft.Owin.dll</td></row>
		<row><td>AlwaysInstall</td><td>Newtonsoft.Json.dll</td></row>
		<row><td>AlwaysInstall</td><td>Owin.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Net.Http.Formatting.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Web.Cors.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Web.Http.SelfHost.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Web.Http.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Web.Http.resources.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Controls.Input.Toolkit.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Controls.Layout.Toolkit.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll10</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll2</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll3</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll4</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll5</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll6</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll7</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll8</td></row>
		<row><td>AlwaysInstall</td><td>System.Windows.Interactivity.resources.dll9</td></row>
		<row><td>AlwaysInstall</td><td>WPFToolkit.dll</td></row>
		<row><td>AlwaysInstall</td><td>WebApi.exe</td></row>
		<row><td>AlwaysInstall</td><td>WebApi.vshost.exe</td></row>
		<row><td>AlwaysInstall</td><td>WebApiClient.exe</td></row>
		<row><td>AlwaysInstall</td><td>_DTools.dll</td></row>
		<row><td>AlwaysInstall</td><td>log4net.dll</td></row>
		<row><td>AlwaysInstall</td><td>svlDNMVAPI.dll</td></row>
	</table>

	<table name="File">
		<col key="yes" def="s72">File</col>
		<col def="s72">Component_</col>
		<col def="s255">FileName</col>
		<col def="i4">FileSize</col>
		<col def="S72">Version</col>
		<col def="S20">Language</col>
		<col def="I2">Attributes</col>
		<col def="i2">Sequence</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I4">ISAttributes</col>
		<col def="S72">ISComponentSubFolder_</col>
		<row><td>_dtools.dll</td><td>_DTools.dll</td><td>3DTools.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\3DTools.dll</td><td>1</td><td/></row>
		<row><td>abc_site.png</td><td>ISX_DEFAULTCOMPONENT29</td><td>abc_site.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\site\abc_site.png</td><td>1</td><td/></row>
		<row><td>aten_energy_box_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>ATEN_E~1.PNG|ATEN_Energy_Box_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\rack_220\ATEN_Energy_Box_220x20.png</td><td>1</td><td/></row>
		<row><td>aten_energy_box_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>ATEN_E~1.PNG|ATEN_Energy_Box_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\rack_440\ATEN_Energy_Box_440x40.png</td><td>1</td><td/></row>
		<row><td>aten_energybox_440x40.png</td><td>ISX_DEFAULTCOMPONENT</td><td>ATEN_E~1.PNG|ATEN_EnergyBox_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ATEN_EnergyBox_440x40.png</td><td>1</td><td/></row>
		<row><td>aten_energybox_440x40.png1</td><td>ISX_DEFAULTCOMPONENT27</td><td>ATEN_E~1.PNG|ATEN_EnergyBox_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\rack_440\ATEN_EnergyBox_440x40.png</td><td>1</td><td/></row>
		<row><td>building_16.png</td><td>ISX_DEFAULTCOMPONENT23</td><td>BUILDI~1.PNG|building_16.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\icon_16\building_16.png</td><td>1</td><td/></row>
		<row><td>center_16.png</td><td>ISX_DEFAULTCOMPONENT23</td><td>CENTER~1.PNG|center_16.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\icon_16\center_16.png</td><td>1</td><td/></row>
		<row><td>cisco93128tx_3u_220x60.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO9~1.PNG|Cisco93128tx_3U_220x60.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\rack_220\Cisco93128tx_3U_220x60.png</td><td>1</td><td/></row>
		<row><td>cisco93128tx_3u_440x120.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO9~1.PNG|Cisco93128tx_3U_440x120.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\rack_440\Cisco93128tx_3U_440x120.png</td><td>1</td><td/></row>
		<row><td>cisco93128tx_3u_440x120.png1</td><td>ISX_DEFAULTCOMPONENT19</td><td>CISCO9~1.PNG|Cisco93128tx_3U_440x120.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\catalog\Cisco93128tx_3U_440x120.png</td><td>1</td><td/></row>
		<row><td>cisco_4503_7u_220x140.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_4503_7U_220x140.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_4503_7U_220x140.png</td><td>1</td><td/></row>
		<row><td>cisco_4503_7u_440x280.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_4503_7U_440x280.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_4503_7U_440x280.png</td><td>1</td><td/></row>
		<row><td>cisco_4506_11u_220x220.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_4506_11U_220x220.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_4506_11U_220x220.png</td><td>1</td><td/></row>
		<row><td>cisco_4506_11u_440x440.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_4506_11U_440x440.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_4506_11U_440x440.png</td><td>1</td><td/></row>
		<row><td>cisco_4507_12u_220x240.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_4507_12U_220x240.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_4507_12U_220x240.png</td><td>1</td><td/></row>
		<row><td>cisco_4507_12u_440x480.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_4507_12U_440x480.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_4507_12U_440x480.png</td><td>1</td><td/></row>
		<row><td>cisco_4510_15u_220x300.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_4510_15U_220x300.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_4510_15U_220x300.png</td><td>1</td><td/></row>
		<row><td>cisco_4510_15u_440x600.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_4510_15U_440x600.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_4510_15U_440x600.png</td><td>1</td><td/></row>
		<row><td>cisco_6503_4u_220x80.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_6503_4U_220x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_6503_4U_220x80.png</td><td>1</td><td/></row>
		<row><td>cisco_6503_4u_440x160.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_6503_4U_440x160.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_6503_4U_440x160.png</td><td>1</td><td/></row>
		<row><td>cisco_6504_5u_220x100.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_6504_5U_220x100.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_6504_5U_220x100.png</td><td>1</td><td/></row>
		<row><td>cisco_6504_5u_440x200.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_6504_5U_440x200.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_6504_5U_440x200.png</td><td>1</td><td/></row>
		<row><td>cisco_6506_11u_220x220.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_6506_11U_220x220.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_6506_11U_220x220.png</td><td>1</td><td/></row>
		<row><td>cisco_6506_11u_440x440.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_6506_11U_440x440.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_6506_11U_440x440.png</td><td>1</td><td/></row>
		<row><td>cisco_6509_14u_220x280.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_6509_14U_220x280.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_6509_14U_220x280.png</td><td>1</td><td/></row>
		<row><td>cisco_6509_14u_440x560.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_6509_14U_440x560.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_6509_14U_440x560.png</td><td>1</td><td/></row>
		<row><td>cisco_6509v_21u_220x420.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_6509v_21U_220x420.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_6509v_21U_220x420.png</td><td>1</td><td/></row>
		<row><td>cisco_6509v_21u_440x840.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_6509v_21U_440x840.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_6509v_21U_440x840.png</td><td>1</td><td/></row>
		<row><td>cisco_6513_19u_220x380.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>CISCO_~1.PNG|Cisco_6513_19U_220x380.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\Cisco_6513_19U_220x380.png</td><td>1</td><td/></row>
		<row><td>cisco_6513_19u_440x760.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>CISCO_~1.PNG|Cisco_6513_19U_440x760.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\Cisco_6513_19U_440x760.png</td><td>1</td><td/></row>
		<row><td>coax.xsl</td><td>ISX_DEFAULTCOMPONENT3</td><td>coax.xsl</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\coax.xsl</td><td>1</td><td/></row>
		<row><td>configs.exe</td><td>ConfigS.EXE</td><td>ConfigS.EXE</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ConfigS.EXE</td><td>1</td><td/></row>
		<row><td>copper.xsl</td><td>ISX_DEFAULTCOMPONENT3</td><td>copper.xsl</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\copper.xsl</td><td>1</td><td/></row>
		<row><td>data2.3d</td><td>ISX_DEFAULTCOMPONENT</td><td>data2.3d</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\data2.3d</td><td>1</td><td/></row>
		<row><td>demo.xml</td><td>ISX_DEFAULTCOMPONENT17</td><td>demo.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\demo.xml</td><td>1</td><td/></row>
		<row><td>emc_storage_2u_220x40.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>EMC_ST~1.PNG|EMC_storage_2U_220x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\EMC_storage_2U_220x40.png</td><td>1</td><td/></row>
		<row><td>emc_storage_2u_440x80.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>EMC_ST~1.PNG|EMC_storage_2U_440x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\EMC_storage_2U_440x80.png</td><td>1</td><td/></row>
		<row><td>entityframework.dll</td><td>EntityFramework.dll</td><td>ENTITY~1.DLL|EntityFramework.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\EntityFramework.dll</td><td>1</td><td/></row>
		<row><td>entityframework.resources.dl</td><td>EntityFramework.resources.dll</td><td>ENTITY~1.DLL|EntityFramework.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ko\EntityFramework.resources.dll</td><td>1</td><td/></row>
		<row><td>entityframework.sqlserver.dl</td><td>EntityFramework.SqlServer.dll</td><td>ENTITY~1.DLL|EntityFramework.SqlServer.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\EntityFramework.SqlServer.dll</td><td>1</td><td/></row>
		<row><td>entityframework.sqlserver.xm</td><td>ISX_DEFAULTCOMPONENT</td><td>ENTITY~1.XML|EntityFramework.SqlServer.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\EntityFramework.SqlServer.xml</td><td>1</td><td/></row>
		<row><td>entityframework.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>ENTITY~1.XML|EntityFramework.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\EntityFramework.xml</td><td>1</td><td/></row>
		<row><td>export_template_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>EXPORT~1.XLS|export_template_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\export_template_en.xls</td><td>1</td><td/></row>
		<row><td>export_template_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>EXPORT~1.XLS|export_template_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\export_template_ko.xls</td><td>1</td><td/></row>
		<row><td>fiber.xsl</td><td>ISX_DEFAULTCOMPONENT3</td><td>fiber.xsl</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\fiber.xsl</td><td>1</td><td/></row>
		<row><td>floor_16.png</td><td>ISX_DEFAULTCOMPONENT23</td><td>floor_16.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\icon_16\floor_16.png</td><td>1</td><td/></row>
		<row><td>fmt.css</td><td>ISX_DEFAULTCOMPONENT3</td><td>fmt.css</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\fmt.css</td><td>1</td><td/></row>
		<row><td>fnet.bmp</td><td>ISX_DEFAULTCOMPONENT3</td><td>fnet.bmp</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\fnet.bmp</td><td>1</td><td/></row>
		<row><td>general_blank_1u_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Blank_1U_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Blank_1U_220x20.png</td><td>1</td><td/></row>
		<row><td>general_blank_1u_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Blank_1U_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Blank_1U_440x40.png</td><td>1</td><td/></row>
		<row><td>general_blank_2u_220x40.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Blank_2U_220x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Blank_2U_220x40.png</td><td>1</td><td/></row>
		<row><td>general_blank_2u_440x80.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Blank_2U_440x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Blank_2U_440x80.png</td><td>1</td><td/></row>
		<row><td>general_blank_3u_220x60.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Blank_3U_220x60.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Blank_3U_220x60.png</td><td>1</td><td/></row>
		<row><td>general_blank_3u_440x120.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Blank_3U_440x120.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Blank_3U_440x120.png</td><td>1</td><td/></row>
		<row><td>general_blank_4u_220x80.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Blank_4U_220x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Blank_4U_220x80.png</td><td>1</td><td/></row>
		<row><td>general_blank_4u_440x160.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Blank_4U_440x160.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Blank_4U_440x160.png</td><td>1</td><td/></row>
		<row><td>general_entry1_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Entry1_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Entry1_220x20.png</td><td>1</td><td/></row>
		<row><td>general_entry1_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Entry1_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Entry1_440x40.png</td><td>1</td><td/></row>
		<row><td>general_entry2_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Entry2_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Entry2_220x20.png</td><td>1</td><td/></row>
		<row><td>general_entry2_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Entry2_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Entry2_440x40.png</td><td>1</td><td/></row>
		<row><td>general_l3sw24_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_L3SW24_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_L3SW24_220x20.png</td><td>1</td><td/></row>
		<row><td>general_l3sw24_2_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_L3SW24_2_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_L3SW24_2_220x20.png</td><td>1</td><td/></row>
		<row><td>general_l3sw24_2_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_L3SW24_2_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_L3SW24_2_440x40.png</td><td>1</td><td/></row>
		<row><td>general_l3sw24_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_L3SW24_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_L3SW24_440x40.png</td><td>1</td><td/></row>
		<row><td>general_l3sw24_4_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_L3SW24_4_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_L3SW24_4_220x20.png</td><td>1</td><td/></row>
		<row><td>general_l3sw24_4_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_L3SW24_4_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_L3SW24_4_440x40.png</td><td>1</td><td/></row>
		<row><td>general_pp_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_PP_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_PP_220x20.png</td><td>1</td><td/></row>
		<row><td>general_pp_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_PP_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_PP_440x40.png</td><td>1</td><td/></row>
		<row><td>general_ppa_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_PPA_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_PPA_220x20.png</td><td>1</td><td/></row>
		<row><td>general_ppa_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_PPA_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_PPA_440x40.png</td><td>1</td><td/></row>
		<row><td>general_router_1u_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Router_1U_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Router_1U_220x20.png</td><td>1</td><td/></row>
		<row><td>general_router_1u_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Router_1U_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Router_1U_440x40.png</td><td>1</td><td/></row>
		<row><td>general_server_1.5u_220x40.p</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Server_1.5U_220x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Server_1.5U_220x40.png</td><td>1</td><td/></row>
		<row><td>general_server_1.5u_440x80.p</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Server_1.5U_440x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Server_1.5U_440x80.png</td><td>1</td><td/></row>
		<row><td>general_server_1u_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Server_1U_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Server_1U_220x20.png</td><td>1</td><td/></row>
		<row><td>general_server_1u_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Server_1U_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Server_1U_440x40.png</td><td>1</td><td/></row>
		<row><td>general_server_1u_440x80.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Server_1U_440x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Server_1U_440x80.png</td><td>1</td><td/></row>
		<row><td>general_server_2u_220x40.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Server_2U_220x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Server_2U_220x40.png</td><td>1</td><td/></row>
		<row><td>general_server_2u_440x80.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Server_2U_440x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Server_2U_440x80.png</td><td>1</td><td/></row>
		<row><td>general_server_3u_220x60.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Server_3U_220x60.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Server_3U_220x60.png</td><td>1</td><td/></row>
		<row><td>general_server_3u_440x120.pn</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Server_3U_440x120.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Server_3U_440x120.png</td><td>1</td><td/></row>
		<row><td>general_server_4u_220x80.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_Server_4U_220x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_Server_4U_220x80.png</td><td>1</td><td/></row>
		<row><td>general_server_4u_440x160.pn</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_Server_4U_440x160.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_Server_4U_440x160.png</td><td>1</td><td/></row>
		<row><td>general_sw24_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_SW24_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_SW24_220x20.png</td><td>1</td><td/></row>
		<row><td>general_sw24_2_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_SW24_2_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_SW24_2_220x20.png</td><td>1</td><td/></row>
		<row><td>general_sw24_2_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_SW24_2_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_SW24_2_440x40.png</td><td>1</td><td/></row>
		<row><td>general_sw24_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_SW24_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_SW24_440x40.png</td><td>1</td><td/></row>
		<row><td>general_sw24_4_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_SW24_4_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_SW24_4_220x20.png</td><td>1</td><td/></row>
		<row><td>general_sw24_4_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_SW24_4_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_SW24_4_440x40.png</td><td>1</td><td/></row>
		<row><td>general_sw48_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_SW48_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_SW48_220x20.png</td><td>1</td><td/></row>
		<row><td>general_sw48_2_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_SW48_2_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_SW48_2_220x20.png</td><td>1</td><td/></row>
		<row><td>general_sw48_2_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_SW48_2_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_SW48_2_440x40.png</td><td>1</td><td/></row>
		<row><td>general_sw48_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_SW48_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_SW48_440x40.png</td><td>1</td><td/></row>
		<row><td>general_sw48_4_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>GENERA~1.PNG|General_SW48_4_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\General_SW48_4_220x20.png</td><td>1</td><td/></row>
		<row><td>general_sw48_4_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>GENERA~1.PNG|General_SW48_4_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\General_SW48_4_440x40.png</td><td>1</td><td/></row>
		<row><td>hp_blade_server_10u_220x200.</td><td>ISX_DEFAULTCOMPONENT26</td><td>HP_BLA~1.PNG|HP_blade_server_10U_220x200.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\HP_blade_server_10U_220x200.png</td><td>1</td><td/></row>
		<row><td>hp_blade_server_10u_440x400.</td><td>ISX_DEFAULTCOMPONENT27</td><td>HP_BLA~1.PNG|HP_blade_server_10U_440x400.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\HP_blade_server_10U_440x400.png</td><td>1</td><td/></row>
		<row><td>i2ms2.exe</td><td>I2MS2.exe</td><td>I2MS2.exe</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2.exe</td><td>1</td><td/></row>
		<row><td>i2ms2.exe.config</td><td>ISX_DEFAULTCOMPONENT</td><td>I2MS2E~1.CON|I2MS2.exe.config</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2.exe.config</td><td>1</td><td/></row>
		<row><td>i2ms2.pdb</td><td>ISX_DEFAULTCOMPONENT</td><td>I2MS2.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2.pdb</td><td>1</td><td/></row>
		<row><td>i2ms2.vshost.exe</td><td>I2MS2.vshost.exe</td><td>I2MS2V~1.EXE|I2MS2.vshost.exe</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2.vshost.exe</td><td>1</td><td/></row>
		<row><td>i2ms2.vshost.exe.config</td><td>ISX_DEFAULTCOMPONENT</td><td>I2MS2V~1.CON|I2MS2.vshost.exe.config</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2.vshost.exe.config</td><td>1</td><td/></row>
		<row><td>i2ms2.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>I2MS2.XML</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2.XML</td><td>1</td><td/></row>
		<row><td>i2ms2_config.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>I2MS2_~1.XML|i2ms2_config.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\i2ms2_config.xml</td><td>1</td><td/></row>
		<row><td>i2ms2rs.exe</td><td>I2MS2RS.EXE</td><td>I2MS2RS.EXE</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2RS.EXE</td><td>1</td><td/></row>
		<row><td>i2msr.dll</td><td>I2MSR.dll</td><td>I2MSR.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MSR.dll</td><td>1</td><td/></row>
		<row><td>i2msr.dll.codeanalysislog.xm</td><td>ISX_DEFAULTCOMPONENT</td><td>I2MSRD~1.XML|I2MSR.dll.CodeAnalysisLog.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MSR.dll.CodeAnalysisLog.xml</td><td>1</td><td/></row>
		<row><td>i2msr.dll.lastcodeanalysissu</td><td>ISX_DEFAULTCOMPONENT</td><td>I2MSRD~1.LAS|I2MSR.dll.lastcodeanalysissucceeded</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MSR.dll.lastcodeanalysissucceeded</td><td>1</td><td/></row>
		<row><td>i2msr.pdb</td><td>ISX_DEFAULTCOMPONENT</td><td>I2MSR.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MSR.pdb</td><td>1</td><td/></row>
		<row><td>i2msr.resources.dll</td><td>I2MSR.resources.dll</td><td>I2MSRR~1.DLL|I2MSR.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ko-KR\I2MSR.resources.dll</td><td>1</td><td/></row>
		<row><td>i2msr.resources.dll1</td><td>I2MSR.resources.dll1</td><td>I2MSRR~1.DLL|I2MSR.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\zh-CHS\I2MSR.resources.dll</td><td>1</td><td/></row>
		<row><td>ibm_blade_server_4u_220x80.p</td><td>ISX_DEFAULTCOMPONENT26</td><td>IBM_BL~1.PNG|IBM_blade_server_4U_220x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\IBM_blade_server_4U_220x80.png</td><td>1</td><td/></row>
		<row><td>ibm_blade_server_4u_440x160.</td><td>ISX_DEFAULTCOMPONENT27</td><td>IBM_BL~1.PNG|IBM_blade_server_4U_440x160.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\IBM_blade_server_4U_440x160.png</td><td>1</td><td/></row>
		<row><td>ibm_blade_server_6u_220x120.</td><td>ISX_DEFAULTCOMPONENT26</td><td>IBM_BL~1.PNG|IBM_blade_server_6U_220x120.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\IBM_blade_server_6U_220x120.png</td><td>1</td><td/></row>
		<row><td>ibm_blade_server_6u_440x240.</td><td>ISX_DEFAULTCOMPONENT27</td><td>IBM_BL~1.PNG|IBM_blade_server_6U_440x240.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\IBM_blade_server_6U_440x240.png</td><td>1</td><td/></row>
		<row><td>ibm_storage2_2u_220x40.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>IBM_ST~1.PNG|IBM_storage2_2U_220x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\IBM_storage2_2U_220x40.png</td><td>1</td><td/></row>
		<row><td>ibm_storage2_2u_440x80.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>IBM_ST~1.PNG|IBM_storage2_2U_440x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\IBM_storage2_2U_440x80.png</td><td>1</td><td/></row>
		<row><td>ibm_storage_2u_220x40.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>IBM_ST~1.PNG|IBM_storage_2U_220x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\IBM_storage_2U_220x40.png</td><td>1</td><td/></row>
		<row><td>ibm_storage_2u_440x80.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>IBM_ST~1.PNG|IBM_storage_2U_440x80.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\IBM_storage_2U_440x80.png</td><td>1</td><td/></row>
		<row><td>ic_16.png</td><td>ISX_DEFAULTCOMPONENT23</td><td>ic_16.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\icon_16\ic_16.png</td><td>1</td><td/></row>
		<row><td>iems.exe</td><td>IEMS.EXE</td><td>IEMS.EXE</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\IEMS.EXE</td><td>1</td><td/></row>
		<row><td>index.xsl</td><td>ISX_DEFAULTCOMPONENT3</td><td>index.xsl</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\index.xsl</td><td>1</td><td/></row>
		<row><td>index2.xsl</td><td>ISX_DEFAULTCOMPONENT3</td><td>index2.xsl</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\index2.xsl</td><td>1</td><td/></row>
		<row><td>ipp_16.png</td><td>ISX_DEFAULTCOMPONENT23</td><td>ipp_16.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\icon_16\ipp_16.png</td><td>1</td><td/></row>
		<row><td>log4net.dll</td><td>log4net.dll</td><td>log4net.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\log4net.dll</td><td>1</td><td/></row>
		<row><td>log4net.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>log4net.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\log4net.xml</td><td>1</td><td/></row>
		<row><td>ls_energybox_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>LS_ENE~1.PNG|LS_EnergyBox_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\LS_EnergyBox_220x20.png</td><td>1</td><td/></row>
		<row><td>ls_energybox_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>LS_ENE~1.PNG|LS_EnergyBox_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\LS_EnergyBox_440x40.png</td><td>1</td><td/></row>
		<row><td>ls_simplewin_ic_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>LS_SIM~1.PNG|LS_SimpleWin_IC_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\LS_SimpleWin_IC_220x20.png</td><td>1</td><td/></row>
		<row><td>ls_simplewin_ic_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>LS_SIM~1.PNG|LS_SimpleWin_IC_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\LS_SimpleWin_IC_440x40.png</td><td>1</td><td/></row>
		<row><td>ls_simplewin_ipp_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>LS_SIM~1.PNG|LS_SimpleWin_IPP_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\LS_SimpleWin_IPP_220x20.png</td><td>1</td><td/></row>
		<row><td>ls_simplewin_ipp_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>LS_SIM~1.PNG|LS_SimpleWin_IPP_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\LS_SimpleWin_IPP_440x40.png</td><td>1</td><td/></row>
		<row><td>ls_simplewin_ippa_220x20.png</td><td>ISX_DEFAULTCOMPONENT26</td><td>LS_SIM~1.PNG|LS_SimpleWin_IPPA_220x20.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_220\LS_SimpleWin_IPPA_220x20.png</td><td>1</td><td/></row>
		<row><td>ls_simplewin_ippa_440x40.png</td><td>ISX_DEFAULTCOMPONENT27</td><td>LS_SIM~1.PNG|LS_SimpleWin_IPPA_440x40.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\rack_440\LS_SimpleWin_IPPA_440x40.png</td><td>1</td><td/></row>
		<row><td>mahapps.metro.dll</td><td>MahApps.Metro.dll</td><td>MAHAPP~1.DLL|MahApps.Metro.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\MahApps.Metro.dll</td><td>1</td><td/></row>
		<row><td>mahapps.metro.pdb</td><td>ISX_DEFAULTCOMPONENT</td><td>MAHAPP~1.PDB|MahApps.Metro.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\MahApps.Metro.pdb</td><td>1</td><td/></row>
		<row><td>mahapps.metro.resources.dll</td><td>MahApps.Metro.Resources.dll</td><td>MAHAPP~1.DLL|MahApps.Metro.Resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\MahApps.Metro.Resources.dll</td><td>1</td><td/></row>
		<row><td>mahapps.metro.resources.pdb</td><td>ISX_DEFAULTCOMPONENT</td><td>MAHAPP~1.PDB|MahApps.Metro.Resources.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\MahApps.Metro.Resources.pdb</td><td>1</td><td/></row>
		<row><td>metrochart.dll</td><td>MetroChart.dll</td><td>METROC~1.DLL|MetroChart.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\MetroChart.dll</td><td>1</td><td/></row>
		<row><td>metrochart.pdb</td><td>ISX_DEFAULTCOMPONENT</td><td>METROC~1.PDB|MetroChart.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\MetroChart.pdb</td><td>1</td><td/></row>
		<row><td>microsoft.aspnet.signalr.cli</td><td>Microsoft.AspNet.SignalR.Client.dll</td><td>MICROS~1.DLL|Microsoft.AspNet.SignalR.Client.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.AspNet.SignalR.Client.dll</td><td>1</td><td/></row>
		<row><td>microsoft.aspnet.signalr.cli1</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.AspNet.SignalR.Client.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.AspNet.SignalR.Client.xml</td><td>1</td><td/></row>
		<row><td>microsoft.aspnet.signalr.cor</td><td>Microsoft.AspNet.SignalR.Core.dll</td><td>MICROS~1.DLL|Microsoft.AspNet.SignalR.Core.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.AspNet.SignalR.Core.dll</td><td>1</td><td/></row>
		<row><td>microsoft.aspnet.signalr.cor1</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.AspNet.SignalR.Core.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.AspNet.SignalR.Core.xml</td><td>1</td><td/></row>
		<row><td>microsoft.aspnet.signalr.sys</td><td>Microsoft.AspNet.SignalR.SystemWeb.dll</td><td>MICROS~1.DLL|Microsoft.AspNet.SignalR.SystemWeb.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.AspNet.SignalR.SystemWeb.dll</td><td>1</td><td/></row>
		<row><td>microsoft.aspnet.signalr.sys1</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.AspNet.SignalR.SystemWeb.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.AspNet.SignalR.SystemWeb.xml</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects</td><td>Microsoft.Expression.Effects.dll</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Expression.Effects.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects1</td><td>Microsoft.Expression.Effects.resources.dll</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\de\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects10</td><td>Microsoft.Expression.Effects.resources.dll8</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\zh-Hans\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects11</td><td>Microsoft.Expression.Effects.resources.dll9</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\zh-Hant\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects3</td><td>Microsoft.Expression.Effects.resources.dll2</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\es\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects4</td><td>Microsoft.Expression.Effects.resources.dll3</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\fr\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects5</td><td>Microsoft.Expression.Effects.resources.dll4</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\it\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects6</td><td>Microsoft.Expression.Effects.resources.dll5</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ja\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects7</td><td>Microsoft.Expression.Effects.resources.dll6</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ko\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.effects9</td><td>Microsoft.Expression.Effects.resources.dll7</td><td>MICROS~1.DLL|Microsoft.Expression.Effects.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ru\Microsoft.Expression.Effects.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac</td><td>Microsoft.Expression.Interactions.dll</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Expression.Interactions.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac1</td><td>Microsoft.Expression.Interactions.resources.dll</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\de\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac10</td><td>Microsoft.Expression.Interactions.resources.dll8</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ru\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac11</td><td>Microsoft.Expression.Interactions.resources.dll9</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\zh-Hans\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac12</td><td>Microsoft.Expression.Interactions.resources.dll10</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\zh-Hant\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac3</td><td>Microsoft.Expression.Interactions.resources.dll2</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\en\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac4</td><td>Microsoft.Expression.Interactions.resources.dll3</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\es\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac5</td><td>Microsoft.Expression.Interactions.resources.dll4</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\fr\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac6</td><td>Microsoft.Expression.Interactions.resources.dll5</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\it\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac7</td><td>Microsoft.Expression.Interactions.resources.dll6</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ja\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.expression.interac8</td><td>Microsoft.Expression.Interactions.resources.dll7</td><td>MICROS~1.DLL|Microsoft.Expression.Interactions.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ko\Microsoft.Expression.Interactions.resources.dll</td><td>1</td><td/></row>
		<row><td>microsoft.owin.cors.dll</td><td>Microsoft.Owin.Cors.dll</td><td>MICROS~1.DLL|Microsoft.Owin.Cors.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Cors.dll</td><td>1</td><td/></row>
		<row><td>microsoft.owin.cors.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.Owin.Cors.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Cors.xml</td><td>1</td><td/></row>
		<row><td>microsoft.owin.dll</td><td>Microsoft.Owin.dll</td><td>MICROS~1.DLL|Microsoft.Owin.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.dll</td><td>1</td><td/></row>
		<row><td>microsoft.owin.host.httplist</td><td>Microsoft.Owin.Host.HttpListener.dll</td><td>MICROS~1.DLL|Microsoft.Owin.Host.HttpListener.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Host.HttpListener.dll</td><td>1</td><td/></row>
		<row><td>microsoft.owin.host.httplist1</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.Owin.Host.HttpListener.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Host.HttpListener.xml</td><td>1</td><td/></row>
		<row><td>microsoft.owin.host.systemwe</td><td>Microsoft.Owin.Host.SystemWeb.dll</td><td>MICROS~1.DLL|Microsoft.Owin.Host.SystemWeb.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Host.SystemWeb.dll</td><td>1</td><td/></row>
		<row><td>microsoft.owin.host.systemwe1</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.Owin.Host.SystemWeb.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Host.SystemWeb.xml</td><td>1</td><td/></row>
		<row><td>microsoft.owin.hosting.dll</td><td>Microsoft.Owin.Hosting.dll</td><td>MICROS~1.DLL|Microsoft.Owin.Hosting.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Hosting.dll</td><td>1</td><td/></row>
		<row><td>microsoft.owin.hosting.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.Owin.Hosting.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Hosting.xml</td><td>1</td><td/></row>
		<row><td>microsoft.owin.security.dll</td><td>Microsoft.Owin.Security.dll</td><td>MICROS~1.DLL|Microsoft.Owin.Security.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Security.dll</td><td>1</td><td/></row>
		<row><td>microsoft.owin.security.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.Owin.Security.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.Security.xml</td><td>1</td><td/></row>
		<row><td>microsoft.owin.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>MICROS~1.XML|Microsoft.Owin.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Microsoft.Owin.xml</td><td>1</td><td/></row>
		<row><td>newtonsoft.json.dll</td><td>Newtonsoft.Json.dll</td><td>NEWTON~1.DLL|Newtonsoft.Json.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Newtonsoft.Json.dll</td><td>1</td><td/></row>
		<row><td>newtonsoft.json.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>NEWTON~1.XML|Newtonsoft.Json.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Newtonsoft.Json.xml</td><td>1</td><td/></row>
		<row><td>no_image.png</td><td>ISX_DEFAULTCOMPONENT17</td><td>No_Image.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\No_Image.png</td><td>1</td><td/></row>
		<row><td>noname.jpg</td><td>ISX_DEFAULTCOMPONENT17</td><td>noname.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\noname.jpg</td><td>1</td><td/></row>
		<row><td>owin.dll</td><td>Owin.dll</td><td>Owin.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Owin.dll</td><td>1</td><td/></row>
		<row><td>pp_16.png</td><td>ISX_DEFAULTCOMPONENT23</td><td>pp_16.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\icon_16\pp_16.png</td><td>1</td><td/></row>
		<row><td>rack_16.png</td><td>ISX_DEFAULTCOMPONENT23</td><td>rack_16.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\icon_16\rack_16.png</td><td>1</td><td/></row>
		<row><td>record.xsl</td><td>ISX_DEFAULTCOMPONENT3</td><td>record.xsl</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\record.xsl</td><td>1</td><td/></row>
		<row><td>reexport.zip</td><td>ISX_DEFAULTCOMPONENT3</td><td>reexport.zip</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\reexport.zip</td><td>1</td><td/></row>
		<row><td>resources.ko_kr.resx</td><td>ISX_DEFAULTCOMPONENT30</td><td>RESOUR~1.RES|Resources.ko-KR.resx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Properties\Resources.ko-KR.resx</td><td>1</td><td/></row>
		<row><td>resources.resx</td><td>ISX_DEFAULTCOMPONENT30</td><td>RESOUR~1.RES|Resources.resx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Properties\Resources.resx</td><td>1</td><td/></row>
		<row><td>resources.zh_chs.resx</td><td>ISX_DEFAULTCOMPONENT30</td><td>RESOUR~1.RES|Resources.zh-CHS.resx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Properties\Resources.zh-CHS.resx</td><td>1</td><td/></row>
		<row><td>resources1.designer.cs</td><td>ISX_DEFAULTCOMPONENT30</td><td>RESOUR~1.CS|Resources1.Designer.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Properties\Resources1.Designer.cs</td><td>1</td><td/></row>
		<row><td>room_16.png</td><td>ISX_DEFAULTCOMPONENT23</td><td>room_16.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\icon_16\room_16.png</td><td>1</td><td/></row>
		<row><td>seoul.jpg</td><td>ISX_DEFAULTCOMPONENT25</td><td>seoul.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\map\seoul.jpg</td><td>1</td><td/></row>
		<row><td>settings.designer.cs</td><td>ISX_DEFAULTCOMPONENT30</td><td>SETTIN~1.CS|Settings.Designer.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Properties\Settings.Designer.cs</td><td>1</td><td/></row>
		<row><td>settings.settings</td><td>ISX_DEFAULTCOMPONENT30</td><td>SETTIN~1.SET|Settings.settings</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Properties\Settings.settings</td><td>1</td><td/></row>
		<row><td>skorea.png</td><td>ISX_DEFAULTCOMPONENT25</td><td>skorea.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\map\skorea.png</td><td>1</td><td/></row>
		<row><td>skorea_1600x1000.png</td><td>ISX_DEFAULTCOMPONENT25</td><td>SKOREA~1.PNG|skorea_1600x1000.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\map\skorea_1600x1000.png</td><td>1</td><td/></row>
		<row><td>stat_eb_day_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_E~1.XLS|stat_eb_day_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_eb_day_en.xls</td><td>1</td><td/></row>
		<row><td>stat_eb_day_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_E~1.XLS|stat_eb_day_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_eb_day_ko.xls</td><td>1</td><td/></row>
		<row><td>stat_eb_hour_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_E~1.XLS|stat_eb_hour_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_eb_hour_en.xls</td><td>1</td><td/></row>
		<row><td>stat_eb_hour_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_E~1.XLS|stat_eb_hour_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_eb_hour_ko.xls</td><td>1</td><td/></row>
		<row><td>stat_eb_month_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_E~1.XLS|stat_eb_month_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_eb_month_en.xls</td><td>1</td><td/></row>
		<row><td>stat_eb_month_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_E~1.XLS|stat_eb_month_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_eb_month_ko.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_day2_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_day2_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_day2_en.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_day2_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_day2_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_day2_ko.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_day_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_day_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_day_en.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_day_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_day_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_day_ko.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_hour2_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_hour2_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_hour2_en.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_hour2_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_hour2_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_hour2_ko.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_hour_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_hour_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_hour_en.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_hour_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_hour_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_hour_ko.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_month2_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_month2_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_month2_en.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_month2_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_month2_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_month2_ko.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_month_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_month_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_month_en.xls</td><td>1</td><td/></row>
		<row><td>stat_terminal_month_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>STAT_T~1.XLS|stat_terminal_month_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\stat_terminal_month_ko.xls</td><td>1</td><td/></row>
		<row><td>svldnmvapi.dll</td><td>svlDNMVAPI.dll</td><td>SVLDNM~1.DLL|svlDNMVAPI.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\svlDNMVAPI.dll</td><td>1</td><td/></row>
		<row><td>system.net.http.formatting.d</td><td>System.Net.Http.Formatting.dll</td><td>SYSTEM~1.DLL|System.Net.Http.Formatting.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Net.Http.Formatting.dll</td><td>1</td><td/></row>
		<row><td>system.net.http.formatting.x</td><td>ISX_DEFAULTCOMPONENT</td><td>SYSTEM~1.XML|System.Net.Http.Formatting.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Net.Http.Formatting.xml</td><td>1</td><td/></row>
		<row><td>system.web.cors.dll</td><td>System.Web.Cors.dll</td><td>SYSTEM~1.DLL|System.Web.Cors.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Web.Cors.dll</td><td>1</td><td/></row>
		<row><td>system.web.http.dll</td><td>System.Web.Http.dll</td><td>SYSTEM~1.DLL|System.Web.Http.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Web.Http.dll</td><td>1</td><td/></row>
		<row><td>system.web.http.resources.dl</td><td>System.Web.Http.resources.dll</td><td>SYSTEM~1.DLL|System.Web.Http.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ko\System.Web.Http.resources.dll</td><td>1</td><td/></row>
		<row><td>system.web.http.selfhost.dll</td><td>System.Web.Http.SelfHost.dll</td><td>SYSTEM~1.DLL|System.Web.Http.SelfHost.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Web.Http.SelfHost.dll</td><td>1</td><td/></row>
		<row><td>system.web.http.selfhost.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>SYSTEM~1.XML|System.Web.Http.SelfHost.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Web.Http.SelfHost.xml</td><td>1</td><td/></row>
		<row><td>system.web.http.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>SYSTEM~1.XML|System.Web.Http.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Web.Http.xml</td><td>1</td><td/></row>
		<row><td>system.windows.controls.inpu</td><td>System.Windows.Controls.Input.Toolkit.dll</td><td>SYSTEM~1.DLL|System.Windows.Controls.Input.Toolkit.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Windows.Controls.Input.Toolkit.dll</td><td>1</td><td/></row>
		<row><td>system.windows.controls.layo</td><td>System.Windows.Controls.Layout.Toolkit.dll</td><td>SYSTEM~1.DLL|System.Windows.Controls.Layout.Toolkit.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Windows.Controls.Layout.Toolkit.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity</td><td>System.Windows.Interactivity.dll</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\System.Windows.Interactivity.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity1</td><td>System.Windows.Interactivity.resources.dll</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\de\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity11</td><td>System.Windows.Interactivity.resources.dll9</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\zh-Hans\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity12</td><td>System.Windows.Interactivity.resources.dll10</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\zh-Hant\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity3</td><td>System.Windows.Interactivity.resources.dll2</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\en\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity4</td><td>System.Windows.Interactivity.resources.dll3</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\es\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity5</td><td>System.Windows.Interactivity.resources.dll4</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\fr\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity6</td><td>System.Windows.Interactivity.resources.dll5</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\it\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity7</td><td>System.Windows.Interactivity.resources.dll6</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ja\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity8</td><td>System.Windows.Interactivity.resources.dll7</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ko\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>system.windows.interactivity9</td><td>System.Windows.Interactivity.resources.dll8</td><td>SYSTEM~1.DLL|System.Windows.Interactivity.resources.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\ru\System.Windows.Interactivity.resources.dll</td><td>1</td><td/></row>
		<row><td>temp.txt</td><td>ISX_DEFAULTCOMPONENT18</td><td>temp.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\building\temp.txt</td><td>1</td><td/></row>
		<row><td>temp.txt1</td><td>ISX_DEFAULTCOMPONENT17</td><td>temp.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\temp.txt</td><td>1</td><td/></row>
		<row><td>temp.txt2</td><td>ISX_DEFAULTCOMPONENT19</td><td>temp.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\building\temp.txt</td><td>1</td><td/></row>
		<row><td>temp.txt3</td><td>ISX_DEFAULTCOMPONENT20</td><td>temp.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\building\temp.txt</td><td>1</td><td/></row>
		<row><td>temp.txt4</td><td>ISX_DEFAULTCOMPONENT21</td><td>temp.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\building\temp.txt</td><td>1</td><td/></row>
		<row><td>temp.txt5</td><td>ISX_DEFAULTCOMPONENT22</td><td>temp.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\building\temp.txt</td><td>1</td><td/></row>
		<row><td>temp.txt6</td><td>ISX_DEFAULTCOMPONENT24</td><td>temp.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\building\temp.txt</td><td>1</td><td/></row>
		<row><td>temp.txt7</td><td>ISX_DEFAULTCOMPONENT28</td><td>temp.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\LSCable\SimpleWin\Images\building\temp.txt</td><td>1</td><td/></row>
		<row><td>template.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>Template.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\Template.xls</td><td>1</td><td/></row>
		<row><td>template_alarm_list_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_alarm_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_alarm_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_alarm_list_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_alarm_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_alarm_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_asset_list_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_asset_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_asset_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_asset_list_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_asset_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_asset_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_cable_list_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_cable_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_cable_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_cable_list_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_cable_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_cable_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_catalog_list_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_catalog_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_catalog_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_catalog_list_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_catalog_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_catalog_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_contact_list_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_contact_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_contact_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_contact_list_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_contact_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_contact_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_environment_list_en</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_environment_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_environment_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_environment_list_ko</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_environment_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_environment_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_location_list_en.xl</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_location_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_location_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_location_list_ko.xl</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_location_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_location_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_logdb_list_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_logdb_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_logdb_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_logdb_list_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_logdb_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_logdb_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_manufacturer_list_e</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_manufacturer_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_manufacturer_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_manufacturer_list_k</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_manufacturer_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_manufacturer_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_print_list_en.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_print_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_print_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_print_list_ko.xls</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_print_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_print_list_ko.xls</td><td>1</td><td/></row>
		<row><td>template_workorder_list_en.x</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_workorder_list_en.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_workorder_list_en.xls</td><td>1</td><td/></row>
		<row><td>template_workorder_list_ko.x</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEMPLA~1.XLS|template_workorder_list_ko.xls</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\ExcelTemplates\template_workorder_list_ko.xls</td><td>1</td><td/></row>
		<row><td>utility.xsl</td><td>ISX_DEFAULTCOMPONENT3</td><td>utility.xsl</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\AppData\Roaming\LSCable\SimpleWin\Images\form\utility.xsl</td><td>1</td><td/></row>
		<row><td>webapi.20170309.log</td><td>ISX_DEFAULTCOMPONENT</td><td>WEBAPI~1.LOG|WebApi.20170309.log</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\WebApi.20170309.log</td><td>1</td><td/></row>
		<row><td>webapi.20170310.log</td><td>ISX_DEFAULTCOMPONENT</td><td>WEBAPI~1.LOG|WebApi.20170310.log</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\WebApi.20170310.log</td><td>1</td><td/></row>
		<row><td>webapi.exe</td><td>WebApi.exe</td><td>WebApi.exe</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\WebApi\WebApi\bin\Debug\WebApi.exe</td><td>1</td><td/></row>
		<row><td>webapi.exe.config</td><td>ISX_DEFAULTCOMPONENT</td><td>WEBAPI~1.CON|WebApi.exe.config</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\WebApi.exe.config</td><td>1</td><td/></row>
		<row><td>webapi.pdb</td><td>ISX_DEFAULTCOMPONENT</td><td>WebApi.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\WebApi\WebApi\bin\Debug\WebApi.pdb</td><td>1</td><td/></row>
		<row><td>webapi.vshost.exe</td><td>WebApi.vshost.exe</td><td>WEBAPI~1.EXE|WebApi.vshost.exe</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\WebApi\WebApi\bin\Debug\WebApi.vshost.exe</td><td>1</td><td/></row>
		<row><td>webapi.vshost.exe.config</td><td>ISX_DEFAULTCOMPONENT</td><td>WEBAPI~1.CON|WebApi.vshost.exe.config</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\WebApi.vshost.exe.config</td><td>1</td><td/></row>
		<row><td>webapi.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>WebApi.XML</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\WebApi\WebApi\bin\Debug\WebApi.XML</td><td>1</td><td/></row>
		<row><td>webapi_config.xml</td><td>ISX_DEFAULTCOMPONENT</td><td>WEBAPI~1.XML|webapi_config.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\webapi_config.xml</td><td>1</td><td/></row>
		<row><td>webapiclient.exe</td><td>WebApiClient.exe</td><td>WEBAPI~1.EXE|WebApiClient.exe</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\WebApiClient.exe</td><td>1</td><td/></row>
		<row><td>webapiclient.pdb</td><td>ISX_DEFAULTCOMPONENT</td><td>WEBAPI~1.PDB|WebApiClient.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\WebApiClient.pdb</td><td>1</td><td/></row>
		<row><td>world_map.png</td><td>ISX_DEFAULTCOMPONENT25</td><td>WORLD_~1.PNG|world_map.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\SimpleWinData\Images\map\world_map.png</td><td>1</td><td/></row>
		<row><td>wpftoolkit.dll</td><td>WPFToolkit.dll</td><td>WPFTOO~1.DLL|WPFToolkit.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\WPFToolkit.dll</td><td>1</td><td/></row>
	</table>

	<table name="FileSFPCatalog">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s255">SFPCatalog_</col>
	</table>

	<table name="Font">
		<col key="yes" def="s72">File_</col>
		<col def="S128">FontTitle</col>
	</table>

	<table name="ISAssistantTag">
		<col key="yes" def="s72">Tag</col>
		<col def="S255">Data</col>
		<row><td>I2MS2.exe</td><td/></row>
		<row><td>PROJECT_ASSISTANT_DEFAULT_FEATURE</td><td>AlwaysInstall</td></row>
		<row><td>PROJECT_ASSISTANT_FEATURES</td><td>NonSelectable</td></row>
	</table>

	<table name="ISBillBoard">
		<col key="yes" def="s72">ISBillboard</col>
		<col def="i2">Duration</col>
		<col def="i2">Origin</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Effect</col>
		<col def="i2">Sequence</col>
		<col def="i2">Target</col>
		<col def="S72">Color</col>
		<col def="S72">Style</col>
		<col def="S72">Font</col>
		<col def="L72">Title</col>
		<col def="S72">DisplayName</col>
	</table>

	<table name="ISChainPackage">
		<col key="yes" def="s72">Package</col>
		<col def="S255">SourcePath</col>
		<col def="S72">ProductCode</col>
		<col def="i2">Order</col>
		<col def="i4">Options</col>
		<col def="S255">InstallCondition</col>
		<col def="S255">RemoveCondition</col>
		<col def="S0">InstallProperties</col>
		<col def="S0">RemoveProperties</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="S72">DisplayName</col>
	</table>

	<table name="ISChainPackageData">
		<col key="yes" def="s72">Package_</col>
		<col key="yes" def="s72">File</col>
		<col def="s50">FilePath</col>
		<col def="I4">Options</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="ISClrWrap">
		<col key="yes" def="s72">Action_</col>
		<col key="yes" def="s72">Name</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISComCatalogAttribute">
		<col key="yes" def="s72">ISComCatalogObject_</col>
		<col key="yes" def="s255">ItemName</col>
		<col def="S0">ItemValue</col>
	</table>

	<table name="ISComCatalogCollection">
		<col key="yes" def="s72">ISComCatalogCollection</col>
		<col def="s72">ISComCatalogObject_</col>
		<col def="s255">CollectionName</col>
	</table>

	<table name="ISComCatalogCollectionObjects">
		<col key="yes" def="s72">ISComCatalogCollection_</col>
		<col key="yes" def="s72">ISComCatalogObject_</col>
	</table>

	<table name="ISComCatalogObject">
		<col key="yes" def="s72">ISComCatalogObject</col>
		<col def="s255">DisplayName</col>
	</table>

	<table name="ISComPlusApplication">
		<col key="yes" def="s72">ISComCatalogObject_</col>
		<col def="S255">ComputerName</col>
		<col def="s72">Component_</col>
		<col def="I2">ISAttributes</col>
		<col def="S0">DepFiles</col>
	</table>

	<table name="ISComPlusApplicationDLL">
		<col key="yes" def="s72">ISComPlusApplicationDLL</col>
		<col def="s72">ISComPlusApplication_</col>
		<col def="s72">ISComCatalogObject_</col>
		<col def="s0">CLSID</col>
		<col def="S0">ProgId</col>
		<col def="S0">DLL</col>
		<col def="S0">AlterDLL</col>
	</table>

	<table name="ISComPlusProxy">
		<col key="yes" def="s72">ISComPlusProxy</col>
		<col def="s72">ISComPlusApplication_</col>
		<col def="S72">Component_</col>
		<col def="I2">ISAttributes</col>
		<col def="S0">DepFiles</col>
	</table>

	<table name="ISComPlusProxyDepFile">
		<col key="yes" def="s72">ISComPlusApplication_</col>
		<col key="yes" def="s72">File_</col>
		<col def="S0">ISPath</col>
	</table>

	<table name="ISComPlusProxyFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">ISComPlusApplicationDLL_</col>
	</table>

	<table name="ISComPlusServerDepFile">
		<col key="yes" def="s72">ISComPlusApplication_</col>
		<col key="yes" def="s72">File_</col>
		<col def="S0">ISPath</col>
	</table>

	<table name="ISComPlusServerFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">ISComPlusApplicationDLL_</col>
	</table>

	<table name="ISComponentExtended">
		<col key="yes" def="s72">Component_</col>
		<col def="I4">OS</col>
		<col def="S0">Language</col>
		<col def="s72">FilterProperty</col>
		<col def="I4">Platforms</col>
		<col def="S0">FTPLocation</col>
		<col def="S0">HTTPLocation</col>
		<col def="S0">Miscellaneous</col>
		<row><td>ConfigS.EXE</td><td/><td/><td>_AF4DEDBF_D4EE_4C39_A0BA_E6E861BF7306_FILTER</td><td/><td/><td/><td/></row>
		<row><td>EntityFramework.SqlServer.dll</td><td/><td/><td>_1FB8445D_B5C8_4918_BF74_12D895F40D66_FILTER</td><td/><td/><td/><td/></row>
		<row><td>EntityFramework.dll</td><td/><td/><td>_761BDF24_66C1_4F7D_84A2_A0C0CA150C82_FILTER</td><td/><td/><td/><td/></row>
		<row><td>EntityFramework.resources.dll</td><td/><td/><td>_F5E32F8F_988C_45E9_AED8_B962F78AC4D5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>I2MS2.exe</td><td/><td/><td>_68FF7EFF_F0B4_4A5A_BBF8_0A4EE8E943EB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>I2MS2.vshost.exe</td><td/><td/><td>_3F931FE6_D8BF_49C1_BD53_6D04F283FE95_FILTER</td><td/><td/><td/><td/></row>
		<row><td>I2MS2RS.EXE</td><td/><td/><td>_7CBEC97E_CE18_49E6_8077_A6624D0F9713_FILTER</td><td/><td/><td/><td/></row>
		<row><td>I2MSR.dll</td><td/><td/><td>_B6C18146_7C65_468E_BF55_0CBF8B7AC48F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>I2MSR.resources.dll</td><td/><td/><td>_6F997242_D1B8_45BF_AD7F_6C2EF250A031_FILTER</td><td/><td/><td/><td/></row>
		<row><td>I2MSR.resources.dll1</td><td/><td/><td>_E559BF0E_E45A_4C34_B364_26AF549E839B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>IEMS.EXE</td><td/><td/><td>_32504DF5_2C0D_49EA_A6D3_5E5F2C010EB4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT</td><td/><td/><td>_FEA1B88E_9D23_4B26_8051_96FA1EB6BED5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT1</td><td/><td/><td>_F5AA6FD1_7F49_4AF1_8B10_E5B4AEDFA10E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT10</td><td/><td/><td>_DFBB195F_8534_4F58_ACCC_FB193D9DA066_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT11</td><td/><td/><td>_BFDFF920_0BFA_4D44_8A89_480BB5DFD814_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT13</td><td/><td/><td>_F05E58C7_2E46_463D_A12D_D53ACAF2BF02_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT14</td><td/><td/><td>_8A80F7D1_94EB_4BDF_BC0B_D332C95E172B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT15</td><td/><td/><td>_9413B99F_8491_4091_8520_0B604E0AD37C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT16</td><td/><td/><td>_365197CA_9851_4840_A8F3_838BF9FC0448_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT17</td><td/><td/><td>_64CF15E0_5561_4B0C_9646_F80A99549BE4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT18</td><td/><td/><td>_20EAC9A7_3AD7_4515_A71B_47179675DBF8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT19</td><td/><td/><td>_DEA36321_5222_4607_9018_610C96422D5C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT2</td><td/><td/><td>_D111FF71_56F4_41E3_89DF_2A1CBF4D7D73_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT20</td><td/><td/><td>_F7B392BE_E46B_49FE_B7D6_1B1FFDBB060E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT21</td><td/><td/><td>_0ED472FB_4AAE_4949_96D7_009129317AF1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT22</td><td/><td/><td>_2D7BFC5C_CF92_4385_8DE0_BE5D6FD19F2C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT23</td><td/><td/><td>_559D8BE3_4C84_47A1_A192_226874D0BAF4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT24</td><td/><td/><td>_E99F41F1_55DC_4C4A_83D1_6AD0DE2242F8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT25</td><td/><td/><td>_D9670376_53BA_4819_B39D_AAED11B3FFF1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT26</td><td/><td/><td>_E74F79AC_AF30_49AC_8170_3B5ECEB32044_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT27</td><td/><td/><td>_19088A42_F6FC_4358_8E43_F91B04816CE1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT28</td><td/><td/><td>_515CA697_E1EB_4B81_8ECE_1E1589285EBD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT29</td><td/><td/><td>_749A5EDB_B35A_4314_8764_65A0BDD3454A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT3</td><td/><td/><td>_27CC3BC6_E1BF_46DC_ACD5_31D874383DD0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT30</td><td/><td/><td>_961E8094_2540_4B6D_913A_FAE3EB9CB494_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT31</td><td/><td/><td>_3EBD4350_9385_4A1C_A3CF_2163747905DA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT32</td><td/><td/><td>_8C757CCE_4936_43BE_B275_468EC0A7F603_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT33</td><td/><td/><td>_84950429_F460_433E_8A81_0E90D111A9D9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT5</td><td/><td/><td>_613BAC63_277B_49DB_A04E_299C9000396C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT6</td><td/><td/><td>_D902BDF0_AE6F_4735_A11D_B5F428B33AAE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT7</td><td/><td/><td>_96906750_110B_4161_AC96_1FD11615317C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT8</td><td/><td/><td>_904B9688_04A5_43A9_BA71_DB8DF279AF67_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT9</td><td/><td/><td>_99F3684F_4278_45CD_90A3_E13FBAC82B58_FILTER</td><td/><td/><td/><td/></row>
		<row><td>MahApps.Metro.Resources.dll</td><td/><td/><td>_9B88971C_E688_4534_958B_39C26BEABE74_FILTER</td><td/><td/><td/><td/></row>
		<row><td>MahApps.Metro.dll</td><td/><td/><td>_D625BFBD_7787_41A4_A3C9_085239FB3879_FILTER</td><td/><td/><td/><td/></row>
		<row><td>MetroChart.dll</td><td/><td/><td>_EEF1C25A_B3B1_4A27_AC91_58DBDE6E6DE2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.AspNet.SignalR.Client.dll</td><td/><td/><td>_E0EFE799_9619_4FE1_B385_E0B3C868A76F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.AspNet.SignalR.Core.dll</td><td/><td/><td>_20BE4E36_6519_4DFA_B831_5CEF78B65A81_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.AspNet.SignalR.SystemWeb.dll</td><td/><td/><td>_60B8BC6D_F01E_44E3_9161_E639FDE868BB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.dll</td><td/><td/><td>_44A522E4_32CE_48F5_93F5_13872F9AB491_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll</td><td/><td/><td>_1C2A0DB4_AE4C_4CC4_BDBC_1F554543B1A2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll2</td><td/><td/><td>_59AA1CA0_09E3_493E_AB82_791EC699CE2A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll3</td><td/><td/><td>_B38C2F89_5D07_4571_B5F1_C3C29C527159_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll4</td><td/><td/><td>_6D5295F1_6415_47C0_97A6_D14E26F75077_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll5</td><td/><td/><td>_70133092_DC90_43AA_A2E3_D57A7D703E8F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll6</td><td/><td/><td>_C47DD735_9AA6_485C_89AE_A1D5E4DF138E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll7</td><td/><td/><td>_3CC44886_29BF_4E2E_9F54_3F67B7325F34_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll8</td><td/><td/><td>_7BB4F0BD_39E3_43AA_AEDC_22DE47919384_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Effects.resources.dll9</td><td/><td/><td>_503D4C50_2941_4064_ACE8_394ABBEE2FBD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.dll</td><td/><td/><td>_94548392_8F96_440F_BF92_C61729569CDF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll</td><td/><td/><td>_99EC95AC_755E_4546_9A49_A2EC85CFA9DD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll10</td><td/><td/><td>_409B5CF4_3B3F_4445_AF2A_34C67B32F985_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll2</td><td/><td/><td>_78FDDBB8_B693_4FB8_A7DF_38373339B7CE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll3</td><td/><td/><td>_E943A6A0_FBB9_44F3_B160_B97D82971214_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll4</td><td/><td/><td>_1F7E6F85_0E7D_4E13_B5DC_E08029B2C7E6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll5</td><td/><td/><td>_50C3F4F8_DB7E_4AF4_9067_D0E805E3A803_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll6</td><td/><td/><td>_249CADBB_5048_4331_B268_DD28442747E6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll7</td><td/><td/><td>_B26F7E0D_5D45_46CF_ACCD_F0B1C3097798_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll8</td><td/><td/><td>_65E7BA44_9468_40A4_8BDF_2D1B6141BA52_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Expression.Interactions.resources.dll9</td><td/><td/><td>_A38F6125_B865_40BA_9101_1F5F033FE9C5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Owin.Cors.dll</td><td/><td/><td>_C65F41A0_BB77_4DEA_AEB7_BE570833DEBD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Owin.Host.HttpListener.dll</td><td/><td/><td>_D6356530_E274_4FB3_B1EF_E60344BB1D0F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Owin.Host.SystemWeb.dll</td><td/><td/><td>_A7590B3D_F582_4388_9D03_6C6A4D3263EA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Owin.Hosting.dll</td><td/><td/><td>_4EDB0CBC_FE96_4C4D_B0E6_D059A92D4CD3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Owin.Security.dll</td><td/><td/><td>_5B6E188A_DED2_4F2B_BB99_7ED036EDEDC1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Microsoft.Owin.dll</td><td/><td/><td>_87B5A6B6_71B3_47F9_9228_7164B4251E27_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Newtonsoft.Json.dll</td><td/><td/><td>_6AA0A7B6_C3E7_4AE2_B08E_9AEB278B63D5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Owin.dll</td><td/><td/><td>_E127C804_0CE0_4C04_8DCE_75633D947E36_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Net.Http.Formatting.dll</td><td/><td/><td>_BA6961C7_7587_4726_9CA5_C0787DCCB506_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Web.Cors.dll</td><td/><td/><td>_CF4404E2_7171_4733_88FD_CEAFD30C43C1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Web.Http.SelfHost.dll</td><td/><td/><td>_B1AF7A75_162C_4C32_A30E_944C397CE2D2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Web.Http.dll</td><td/><td/><td>_D9ADA904_AE77_4777_95A8_40E29AFC5C4A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Web.Http.resources.dll</td><td/><td/><td>_5555FFEA_CD93_4D0C_B637_58495E47F21A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Controls.Input.Toolkit.dll</td><td/><td/><td>_0E25B7E5_E63B_47C3_8FD8_DF4FFF7E471B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Controls.Layout.Toolkit.dll</td><td/><td/><td>_61184BB3_1E3C_4041_88FC_A68A43AF4B45_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.dll</td><td/><td/><td>_FECDFD69_C244_487E_8DBD_3BFD6E8AE0C5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll</td><td/><td/><td>_9B4DBAD1_EE0B_440C_A9EA_67CA32EF98D0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll10</td><td/><td/><td>_21CE9A38_B4DD_44E4_B791_D02923C18293_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll2</td><td/><td/><td>_8C389801_166A_48ED_95DC_A1A75B7EFBF1_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll3</td><td/><td/><td>_996DDFCC_E84F_405E_BEFF_B4F613448838_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll4</td><td/><td/><td>_9B43F577_9694_4141_AD60_C31B4001ED42_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll5</td><td/><td/><td>_4FD5CC4F_25FB_43A8_B44D_1785120AC574_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll6</td><td/><td/><td>_7889D604_F37A_4A17_9514_119895E0EC75_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll7</td><td/><td/><td>_327AFD0C_7B68_4B1B_9642_E7C9A234F5E3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll8</td><td/><td/><td>_1AFE36A8_053E_497A_B810_0EF6635F4F65_FILTER</td><td/><td/><td/><td/></row>
		<row><td>System.Windows.Interactivity.resources.dll9</td><td/><td/><td>_75EB51C7_E86B_417A_B336_9F044DEF0814_FILTER</td><td/><td/><td/><td/></row>
		<row><td>WPFToolkit.dll</td><td/><td/><td>_C763FC84_52DE_4DF8_A92D_E3B076506225_FILTER</td><td/><td/><td/><td/></row>
		<row><td>WebApi.exe</td><td/><td/><td>_96E203B7_9112_4935_A198_7522554C46D7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>WebApi.vshost.exe</td><td/><td/><td>_815699C2_E670_447F_BDEE_23F7E324395E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>WebApiClient.exe</td><td/><td/><td>_4E25E5AA_5402_44F3_8327_30590DCCE2B6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>_DTools.dll</td><td/><td/><td>_13F5D466_D705_47FB_AB95_EB4BA97259EF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>log4net.dll</td><td/><td/><td>_5F80F7EF_9DEA_4E9C_9245_A5DE854FBDF0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>svlDNMVAPI.dll</td><td/><td/><td>_471097F4_87E5_42A1_BD61_73426D6BB8C0_FILTER</td><td/><td/><td/><td/></row>
	</table>

	<table name="ISCustomActionReference">
		<col key="yes" def="s72">Action_</col>
		<col def="S0">Description</col>
		<col def="S255">FileType</col>
		<col def="S255">ISCAReferenceFilePath</col>
	</table>

	<table name="ISDIMDependency">
		<col key="yes" def="s72">ISDIMReference_</col>
		<col def="s255">RequiredUUID</col>
		<col def="S255">RequiredMajorVersion</col>
		<col def="S255">RequiredMinorVersion</col>
		<col def="S255">RequiredBuildVersion</col>
		<col def="S255">RequiredRevisionVersion</col>
	</table>

	<table name="ISDIMReference">
		<col key="yes" def="s72">ISDIMReference</col>
		<col def="S0">ISBuildSourcePath</col>
	</table>

	<table name="ISDIMReferenceDependencies">
		<col key="yes" def="s72">ISDIMReference_Parent</col>
		<col key="yes" def="s72">ISDIMDependency_</col>
	</table>

	<table name="ISDIMVariable">
		<col key="yes" def="s72">ISDIMVariable</col>
		<col def="s72">ISDIMReference_</col>
		<col def="s0">Name</col>
		<col def="S0">NewValue</col>
		<col def="I4">Type</col>
	</table>

	<table name="ISDLLWrapper">
		<col key="yes" def="s72">EntryPoint</col>
		<col def="I4">Type</col>
		<col def="s0">Source</col>
		<col def="s255">Target</col>
	</table>

	<table name="ISDRMFile">
		<col key="yes" def="s72">ISDRMFile</col>
		<col def="S72">File_</col>
		<col def="S72">ISDRMLicense_</col>
		<col def="s255">Shell</col>
	</table>

	<table name="ISDRMFileAttribute">
		<col key="yes" def="s72">ISDRMFile_</col>
		<col key="yes" def="s72">Property</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISDRMLicense">
		<col key="yes" def="s72">ISDRMLicense</col>
		<col def="S255">Description</col>
		<col def="S50">ProjectVersion</col>
		<col def="I4">Attributes</col>
		<col def="S255">LicenseNumber</col>
		<col def="S255">RequestCode</col>
		<col def="S255">ResponseCode</col>
	</table>

	<table name="ISDependency">
		<col key="yes" def="S50">ISDependency</col>
		<col def="I2">Exclude</col>
	</table>

	<table name="ISDisk1File">
		<col key="yes" def="s72">ISDisk1File</col>
		<col def="s255">ISBuildSourcePath</col>
		<col def="I4">Disk</col>
	</table>

	<table name="ISDynamicFile">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="s255">SourceFolder</col>
		<col def="I2">IncludeFlags</col>
		<col def="S0">IncludeFiles</col>
		<col def="S0">ExcludeFiles</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISFeatureDIMReferences">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">ISDIMReference_</col>
	</table>

	<table name="ISFeatureMergeModuleExcludes">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s255">ModuleID</col>
		<col key="yes" def="i2">Language</col>
	</table>

	<table name="ISFeatureMergeModules">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s255">ISMergeModule_</col>
		<col key="yes" def="i2">Language_</col>
	</table>

	<table name="ISFeatureSetupPrerequisites">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">ISSetupPrerequisites_</col>
	</table>

	<table name="ISFileManifests">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">Manifest_</col>
	</table>

	<table name="ISIISItem">
		<col key="yes" def="s72">ISIISItem</col>
		<col def="S72">ISIISItem_Parent</col>
		<col def="L255">DisplayName</col>
		<col def="i4">Type</col>
		<col def="S72">Component_</col>
	</table>

	<table name="ISIISProperty">
		<col key="yes" def="s72">ISIISProperty</col>
		<col key="yes" def="s72">ISIISItem_</col>
		<col def="S0">Schema</col>
		<col def="S255">FriendlyName</col>
		<col def="I4">MetaDataProp</col>
		<col def="I4">MetaDataType</col>
		<col def="I4">MetaDataUserType</col>
		<col def="I4">MetaDataAttributes</col>
		<col def="L0">MetaDataValue</col>
		<col def="I4">Order</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISInstallScriptAction">
		<col key="yes" def="s72">EntryPoint</col>
		<col def="I4">Type</col>
		<col def="s72">Source</col>
		<col def="S255">Target</col>
	</table>

	<table name="ISLanguage">
		<col key="yes" def="s50">ISLanguage</col>
		<col def="I2">Included</col>
		<row><td>1033</td><td>0</td></row>
		<row><td>1042</td><td>1</td></row>
	</table>

	<table name="ISLinkerLibrary">
		<col key="yes" def="s72">ISLinkerLibrary</col>
		<col def="s255">Library</col>
		<col def="i4">Order</col>
		<row><td>isrt.obl</td><td>isrt.obl</td><td>2</td></row>
		<row><td>iswi.obl</td><td>iswi.obl</td><td>1</td></row>
	</table>

	<table name="ISLocalControl">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="I4">Attributes</col>
		<col def="I2">X</col>
		<col def="I2">Y</col>
		<col def="I2">Width</col>
		<col def="I2">Height</col>
		<col def="S72">Binary_</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="ISLocalDialog">
		<col key="yes" def="S50">Dialog_</col>
		<col key="yes" def="S50">ISLanguage_</col>
		<col def="I4">Attributes</col>
		<col def="S72">TextStyle_</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
	</table>

	<table name="ISLocalRadioButton">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
	</table>

	<table name="ISLockPermissions">
		<col key="yes" def="s72">LockObject</col>
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="S255">Domain</col>
		<col key="yes" def="s255">User</col>
		<col def="I4">Permission</col>
		<col def="I4">Attributes</col>
	</table>

	<table name="ISLogicalDisk">
		<col key="yes" def="i2">DiskId</col>
		<col key="yes" def="s255">ISProductConfiguration_</col>
		<col key="yes" def="s255">ISRelease_</col>
		<col def="i2">LastSequence</col>
		<col def="L64">DiskPrompt</col>
		<col def="S255">Cabinet</col>
		<col def="S32">VolumeLabel</col>
		<col def="S32">Source</col>
	</table>

	<table name="ISLogicalDiskFeatures">
		<col key="yes" def="i2">ISLogicalDisk_</col>
		<col key="yes" def="s255">ISProductConfiguration_</col>
		<col key="yes" def="s255">ISRelease_</col>
		<col key="yes" def="S38">Feature_</col>
		<col def="i2">Sequence</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISMergeModule">
		<col key="yes" def="s255">ISMergeModule</col>
		<col key="yes" def="i2">Language</col>
		<col def="s255">Name</col>
		<col def="S255">Destination</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISMergeModuleCfgValues">
		<col key="yes" def="s255">ISMergeModule_</col>
		<col key="yes" def="i2">Language_</col>
		<col key="yes" def="s72">ModuleConfiguration_</col>
		<col def="L0">Value</col>
		<col def="i2">Format</col>
		<col def="L255">Type</col>
		<col def="L255">ContextData</col>
		<col def="L255">DefaultValue</col>
		<col def="I2">Attributes</col>
		<col def="L255">DisplayName</col>
		<col def="L255">Description</col>
		<col def="L255">HelpLocation</col>
		<col def="L255">HelpKeyword</col>
	</table>

	<table name="ISObject">
		<col key="yes" def="s50">ObjectName</col>
		<col def="s15">Language</col>
	</table>

	<table name="ISObjectProperty">
		<col key="yes" def="S50">ObjectName</col>
		<col key="yes" def="S50">Property</col>
		<col def="S0">Value</col>
		<col def="I2">IncludeInBuild</col>
	</table>

	<table name="ISPatchConfigImage">
		<col key="yes" def="S72">PatchConfiguration_</col>
		<col key="yes" def="s72">UpgradedImage_</col>
	</table>

	<table name="ISPatchConfiguration">
		<col key="yes" def="s72">Name</col>
		<col def="i2">CanPCDiffer</col>
		<col def="i2">CanPVDiffer</col>
		<col def="i2">IncludeWholeFiles</col>
		<col def="i2">LeaveDecompressed</col>
		<col def="i2">OptimizeForSize</col>
		<col def="i2">EnablePatchCache</col>
		<col def="S0">PatchCacheDir</col>
		<col def="i4">Flags</col>
		<col def="S0">PatchGuidsToReplace</col>
		<col def="s0">TargetProductCodes</col>
		<col def="s50">PatchGuid</col>
		<col def="s0">OutputPath</col>
		<col def="i2">MinMsiVersion</col>
		<col def="I4">Attributes</col>
	</table>

	<table name="ISPatchConfigurationProperty">
		<col key="yes" def="S72">ISPatchConfiguration_</col>
		<col key="yes" def="S50">Property</col>
		<col def="S50">Value</col>
	</table>

	<table name="ISPatchExternalFile">
		<col key="yes" def="s50">Name</col>
		<col key="yes" def="s13">ISUpgradedImage_</col>
		<col def="s72">FileKey</col>
		<col def="s255">FilePath</col>
	</table>

	<table name="ISPatchWholeFile">
		<col key="yes" def="s50">UpgradedImage</col>
		<col key="yes" def="s72">FileKey</col>
		<col def="S72">Component</col>
	</table>

	<table name="ISPathVariable">
		<col key="yes" def="s72">ISPathVariable</col>
		<col def="S255">Value</col>
		<col def="S255">TestValue</col>
		<col def="i4">Type</col>
		<row><td>CommonFilesFolder</td><td/><td/><td>1</td></row>
		<row><td>I2MS2</td><td>I2MS2\I2MS2.csproj</td><td/><td>2</td></row>
		<row><td>ISPROJECTDIR</td><td/><td/><td>1</td></row>
		<row><td>ISProductFolder</td><td/><td/><td>1</td></row>
		<row><td>ISProjectDataFolder</td><td/><td/><td>1</td></row>
		<row><td>ISProjectFolder</td><td/><td/><td>1</td></row>
		<row><td>ProgramFilesFolder</td><td/><td/><td>1</td></row>
		<row><td>SystemFolder</td><td/><td/><td>1</td></row>
		<row><td>WindowsFolder</td><td/><td/><td>1</td></row>
	</table>

	<table name="ISPowerShellWrap">
		<col key="yes" def="s72">Action_</col>
		<col key="yes" def="s72">Name</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISProductConfiguration">
		<col key="yes" def="s72">ISProductConfiguration</col>
		<col def="S255">ProductConfigurationFlags</col>
		<col def="I4">GeneratePackageCode</col>
		<row><td>Express</td><td/><td>1</td></row>
	</table>

	<table name="ISProductConfigurationInstance">
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="i2">InstanceId</col>
		<col key="yes" def="s72">Property</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISProductConfigurationProperty">
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s72">Property</col>
		<col def="L255">Value</col>
	</table>

	<table name="ISRelease">
		<col key="yes" def="s72">ISRelease</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="s255">BuildLocation</col>
		<col def="s255">PackageName</col>
		<col def="i4">Type</col>
		<col def="s0">SupportedLanguagesUI</col>
		<col def="i4">MsiSourceType</col>
		<col def="i4">ReleaseType</col>
		<col def="s72">Platforms</col>
		<col def="S0">SupportedLanguagesData</col>
		<col def="s6">DefaultLanguage</col>
		<col def="i4">SupportedOSs</col>
		<col def="s50">DiskSize</col>
		<col def="i4">DiskSizeUnit</col>
		<col def="i4">DiskClusterSize</col>
		<col def="S0">ReleaseFlags</col>
		<col def="i4">DiskSpanning</col>
		<col def="S255">SynchMsi</col>
		<col def="s255">MediaLocation</col>
		<col def="S255">URLLocation</col>
		<col def="S255">DigitalURL</col>
		<col def="S255">DigitalPVK</col>
		<col def="S255">DigitalSPC</col>
		<col def="S255">Password</col>
		<col def="S255">VersionCopyright</col>
		<col def="i4">Attributes</col>
		<col def="S255">CDBrowser</col>
		<col def="S255">DotNetBuildConfiguration</col>
		<col def="S255">MsiCommandLine</col>
		<col def="I4">ISSetupPrerequisiteLocation</col>
		<row><td>CD_ROM</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>0</td><td>1042</td><td>0</td><td>2</td><td>Intel</td><td/><td>1042</td><td>0</td><td>650</td><td>0</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>Custom</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>2</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>100</td><td>0</td><td>1024</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-10</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>8.75</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-18</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>15.83</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-5</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1042</td><td>0</td><td>2</td><td>Intel</td><td/><td>1042</td><td>0</td><td>4.38</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-9</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>7.95</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>SingleImage</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>PackageName</td><td>1</td><td>1042</td><td>0</td><td>1</td><td>Intel</td><td/><td>1042</td><td>0</td><td>0</td><td>0</td><td>0</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>108573</td><td/><td/><td/><td>3</td></row>
		<row><td>WebDeployment</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>PackageName</td><td>4</td><td>1033</td><td>2</td><td>1</td><td>Intel</td><td/><td>1033</td><td>0</td><td>0</td><td>0</td><td>0</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>124941</td><td/><td/><td/><td>3</td></row>
	</table>

	<table name="ISReleaseASPublishInfo">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="S0">Property</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISReleaseExtended">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="I4">WebType</col>
		<col def="S255">WebURL</col>
		<col def="I4">WebCabSize</col>
		<col def="S255">OneClickCabName</col>
		<col def="S255">OneClickHtmlName</col>
		<col def="S255">WebLocalCachePath</col>
		<col def="I4">EngineLocation</col>
		<col def="S255">Win9xMsiUrl</col>
		<col def="S255">WinNTMsiUrl</col>
		<col def="I4">ISEngineLocation</col>
		<col def="S255">ISEngineURL</col>
		<col def="I4">OneClickTargetBrowser</col>
		<col def="S255">DigitalCertificateIdNS</col>
		<col def="S255">DigitalCertificateDBaseNS</col>
		<col def="S255">DigitalCertificatePasswordNS</col>
		<col def="I4">DotNetRedistLocation</col>
		<col def="S255">DotNetRedistURL</col>
		<col def="I4">DotNetVersion</col>
		<col def="S255">DotNetBaseLanguage</col>
		<col def="S0">DotNetLangaugePacks</col>
		<col def="S255">DotNetFxCmdLine</col>
		<col def="S255">DotNetLangPackCmdLine</col>
		<col def="S50">JSharpCmdLine</col>
		<col def="I4">Attributes</col>
		<col def="I4">JSharpRedistLocation</col>
		<col def="I4">MsiEngineVersion</col>
		<col def="S255">WinMsi30Url</col>
		<col def="S255">CertPassword</col>
		<row><td>CD_ROM</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>Custom</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-10</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-18</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-5</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-9</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>SingleImage</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>1</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>WebDeployment</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>setup</td><td>Default</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>2</td><td>http://www.Installengine.com/Msiengine20</td><td>http://www.Installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
	</table>

	<table name="ISReleaseProperty">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s0">Value</col>
	</table>

	<table name="ISReleasePublishInfo">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="S255">Repository</col>
		<col def="S255">DisplayName</col>
		<col def="S255">Publisher</col>
		<col def="S255">Description</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISSQLConnection">
		<col key="yes" def="s72">ISSQLConnection</col>
		<col def="s255">Server</col>
		<col def="s255">Database</col>
		<col def="s255">UserName</col>
		<col def="s255">Password</col>
		<col def="s255">Authentication</col>
		<col def="i2">Attributes</col>
		<col def="i2">Order</col>
		<col def="S0">Comments</col>
		<col def="I4">CmdTimeout</col>
		<col def="S0">BatchSeparator</col>
		<col def="S0">ScriptVersion_Table</col>
		<col def="S0">ScriptVersion_Column</col>
	</table>

	<table name="ISSQLConnectionDBServer">
		<col key="yes" def="s72">ISSQLConnectionDBServer</col>
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col key="yes" def="s72">ISSQLDBMetaData_</col>
		<col def="i2">Order</col>
	</table>

	<table name="ISSQLConnectionScript">
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="i2">Order</col>
	</table>

	<table name="ISSQLDBMetaData">
		<col key="yes" def="s72">ISSQLDBMetaData</col>
		<col def="S0">DisplayName</col>
		<col def="S0">AdoDriverName</col>
		<col def="S0">AdoCxnDriver</col>
		<col def="S0">AdoCxnServer</col>
		<col def="S0">AdoCxnDatabase</col>
		<col def="S0">AdoCxnUserID</col>
		<col def="S0">AdoCxnPassword</col>
		<col def="S0">AdoCxnWindowsSecurity</col>
		<col def="S0">AdoCxnNetLibrary</col>
		<col def="S0">TestDatabaseCmd</col>
		<col def="S0">TestTableCmd</col>
		<col def="S0">VersionInfoCmd</col>
		<col def="S0">VersionBeginToken</col>
		<col def="S0">VersionEndToken</col>
		<col def="S0">LocalInstanceNames</col>
		<col def="S0">CreateDbCmd</col>
		<col def="S0">SwitchDbCmd</col>
		<col def="I4">ISAttributes</col>
		<col def="S0">TestTableCmd2</col>
		<col def="S0">WinAuthentUserId</col>
		<col def="S0">DsnODBCName</col>
		<col def="S0">AdoCxnPort</col>
		<col def="S0">AdoCxnAdditional</col>
		<col def="S0">QueryDatabasesCmd</col>
		<col def="S0">CreateTableCmd</col>
		<col def="S0">InsertRecordCmd</col>
		<col def="S0">SelectTableCmd</col>
		<col def="S0">ScriptVersion_Table</col>
		<col def="S0">ScriptVersion_Column</col>
		<col def="S0">ScriptVersion_ColumnType</col>
	</table>

	<table name="ISSQLRequirement">
		<col key="yes" def="s72">ISSQLRequirement</col>
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col def="S15">MajorVersion</col>
		<col def="S25">ServicePackLevel</col>
		<col def="i4">Attributes</col>
		<col def="S72">ISSQLConnectionDBServer_</col>
	</table>

	<table name="ISSQLScriptError">
		<col key="yes" def="i4">ErrNumber</col>
		<col key="yes" def="S72">ISSQLScriptFile_</col>
		<col def="i2">ErrHandling</col>
		<col def="L255">Message</col>
		<col def="i2">Attributes</col>
	</table>

	<table name="ISSQLScriptFile">
		<col key="yes" def="s72">ISSQLScriptFile</col>
		<col def="s72">Component_</col>
		<col def="i2">Scheduling</col>
		<col def="L255">InstallText</col>
		<col def="L255">UninstallText</col>
		<col def="S0">ISBuildSourcePath</col>
		<col def="S0">Comments</col>
		<col def="i2">ErrorHandling</col>
		<col def="i2">Attributes</col>
		<col def="S255">Version</col>
		<col def="S255">Condition</col>
		<col def="S0">DisplayName</col>
	</table>

	<table name="ISSQLScriptImport">
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="S255">Server</col>
		<col def="S255">Database</col>
		<col def="S255">UserName</col>
		<col def="S255">Password</col>
		<col def="i4">Authentication</col>
		<col def="S0">IncludeTables</col>
		<col def="S0">ExcludeTables</col>
		<col def="i4">Attributes</col>
	</table>

	<table name="ISSQLScriptReplace">
		<col key="yes" def="s72">ISSQLScriptReplace</col>
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="S0">Search</col>
		<col def="S0">Replace</col>
		<col def="i4">Attributes</col>
	</table>

	<table name="ISScriptFile">
		<col key="yes" def="s255">ISScriptFile</col>
	</table>

	<table name="ISSelfReg">
		<col key="yes" def="s72">FileKey</col>
		<col def="I2">Cost</col>
		<col def="I2">Order</col>
		<col def="S50">CmdLine</col>
	</table>

	<table name="ISSetupFile">
		<col key="yes" def="s72">ISSetupFile</col>
		<col def="S255">FileName</col>
		<col def="V0">Stream</col>
		<col def="S50">Language</col>
		<col def="I2">Splash</col>
		<col def="S0">Path</col>
	</table>

	<table name="ISSetupPrerequisites">
		<col key="yes" def="s72">ISSetupPrerequisites</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I2">Order</col>
		<col def="I2">ISSetupLocation</col>
		<col def="S255">ISReleaseFlags</col>
		<row><td>_68B71FE8_043C_4344_BC59_E803FBB87150_</td><td>Microsoft .NET Framework 4.5 Full.prq</td><td/><td/><td/></row>
	</table>

	<table name="ISSetupType">
		<col key="yes" def="s38">ISSetupType</col>
		<col def="L255">Description</col>
		<col def="L255">Display_Name</col>
		<col def="i2">Display</col>
		<col def="S255">Comments</col>
		<row><td>Custom</td><td>##IDS__IsSetupTypeMinDlg_ChooseFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Custom##</td><td>3</td><td/></row>
		<row><td>Minimal</td><td>##IDS__IsSetupTypeMinDlg_MinimumFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Minimal##</td><td>2</td><td/></row>
		<row><td>Typical</td><td>##IDS__IsSetupTypeMinDlg_AllFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Typical##</td><td>1</td><td/></row>
	</table>

	<table name="ISSetupTypeFeatures">
		<col key="yes" def="s38">ISSetupType_</col>
		<col key="yes" def="s38">Feature_</col>
		<row><td>Custom</td><td>AlwaysInstall</td></row>
		<row><td>Minimal</td><td>AlwaysInstall</td></row>
		<row><td>Typical</td><td>AlwaysInstall</td></row>
	</table>

	<table name="ISStorages">
		<col key="yes" def="s72">Name</col>
		<col def="S0">ISBuildSourcePath</col>
	</table>

	<table name="ISString">
		<col key="yes" def="s255">ISString</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="S0">Value</col>
		<col def="I2">Encoded</col>
		<col def="S0">Comment</col>
		<col def="I4">TimeStamp</col>
		<row><td>COMPANY_NAME</td><td>1042</td><td>회사명</td><td>0</td><td/><td>1000684363</td></row>
		<row><td>DN_AlwaysInstall</td><td>1042</td><td>항상 설치</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_COLOR</td><td>1042</td><td>시스템의 색상 설정이 [ProductName] 실행에 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_DOTNETVERSION45FULL</td><td>1042</td><td>Microsoft .NET Framework 4.5 Full package or greater needs to be installed for this installation to continue.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_OS</td><td>1042</td><td>운영 체제가 [ProductName] 실행에 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_PROCESSOR</td><td>1042</td><td>프로세서가 [ProductName] 실행에 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_RAM</td><td>1042</td><td>RAM이 [ProductName] 실행에 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_SCREEN</td><td>1042</td><td>화면 해상도가 [ProductName] 실행에 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPACT</td><td>1042</td><td>압축</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPACT_DESC</td><td>1042</td><td>압축 설명</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPLETE</td><td>1042</td><td>전체 설치</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPLETE_DESC</td><td>1042</td><td>전체 설치 설명</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM</td><td>1042</td><td>사용자 정의</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM_DESC</td><td>1042</td><td>사용자 정의 설명</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM_DESC_PRO</td><td>1042</td><td>사용자 정의 설명</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_TYPICAL</td><td>1042</td><td>일반</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDPROP_SETUPTYPE_TYPICAL_DESC</td><td>1042</td><td>일반 설명</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_1</td><td>1042</td><td>[1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_1b</td><td>1042</td><td>[1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_1c</td><td>1042</td><td>[1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_1d</td><td>1042</td><td>[1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Advertising</td><td>1042</td><td>처음 사용 시 설치할 응용 프로그램</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_AllocatingRegistry</td><td>1042</td><td>레지스트리 공간 할당 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_AppCommandLine</td><td>1042</td><td>응용 프로그램: [1], 명령줄: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_AppId</td><td>1042</td><td>AppId: [1]{{, AppType: [2]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_AppIdAppTypeRSN</td><td>1042</td><td>AppId: [1]{{, AppType: [2], Users: [3], RSN: [4]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Application</td><td>1042</td><td>응용 프로그램: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_BindingExes</td><td>1042</td><td>실행 파일을 바인딩하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ClassId</td><td>1042</td><td>Class Id: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ClsID</td><td>1042</td><td>Class Id: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ComponentIDQualifier</td><td>1042</td><td>구성 요소 ID: [1], 제한자: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ComponentIdQualifier2</td><td>1042</td><td>구성 요소 ID: [1], 제한자: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace</td><td>1042</td><td>필요한 공간을 확인하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace2</td><td>1042</td><td>필요한 공간을 확인하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace3</td><td>1042</td><td>필요한 공간을 확인하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ContentTypeExtension</td><td>1042</td><td>MIME 컨텐트 유형: [1], 익스텐션: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ContentTypeExtension2</td><td>1042</td><td>MIME 컨텐트 유형: [1], 익스텐션: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_CopyingNetworkFiles</td><td>1042</td><td>네트워크 설치 파일을 복사하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_CopyingNewFiles</td><td>1042</td><td>새 파일을 복사하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingDuplicate</td><td>1042</td><td>중복 파일을 만드는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingFolders</td><td>1042</td><td>폴더 만드는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingIISRoots</td><td>1042</td><td>IIS 가상 루트를 만드는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingShortcuts</td><td>1042</td><td>바로 가기 만드는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_DeletingServices</td><td>1042</td><td>서비스 삭제 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_EnvironmentStrings</td><td>1042</td><td>환경 문자열을 업데이트하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_EvaluateLaunchConditions</td><td>1042</td><td>실행 조건 확인 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Extension</td><td>1042</td><td>익스텐션: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Extension2</td><td>1042</td><td>익스텐션: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Feature</td><td>1042</td><td>기능: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FeatureColon</td><td>1042</td><td>기능: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_File</td><td>1042</td><td>파일: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_File2</td><td>1042</td><td>파일: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDependencies</td><td>1042</td><td>파일: [1], 의존성: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir</td><td>1042</td><td>파일: [1], 디렉터리: [9]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir2</td><td>1042</td><td>File: [1], Directory: [9]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir3</td><td>1042</td><td>파일: [1], 디렉터리: [9]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize</td><td>1042</td><td>파일: [1], 디렉터리: [9], 크기: [6]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize2</td><td>1042</td><td>File: [1],  Directory: [9],  Size: [6]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize3</td><td>1042</td><td>파일: [1],  디렉터리: [9],  크기: [6]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize4</td><td>1042</td><td>파일: [1], 디렉터리: [2], 크기: [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirectorySize</td><td>1042</td><td>파일: [1],  디렉터리: [9],  크기: [6]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileFolder</td><td>1042</td><td>파일: [1], 폴더: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileFolder2</td><td>1042</td><td>파일: [1], 폴더: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileSectionKeyValue</td><td>1042</td><td>키: [1],  섹션: [2],  키: [3], 값: [4]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FileSectionKeyValue2</td><td>1042</td><td>키: [1],  섹션: [2],  키: [3], 값: [4]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Folder</td><td>1042</td><td>폴더: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Folder1</td><td>1042</td><td>폴더: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Font</td><td>1042</td><td>글꼴: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Font2</td><td>1042</td><td>글꼴: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FoundApp</td><td>1042</td><td>다음 응용 프로그램을 찾았습니다: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_FreeSpace</td><td>1042</td><td>남은 공간: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_GeneratingScript</td><td>1042</td><td>수행할 스크립트 작업을 생성하는 중:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ISLockPermissionsCost</td><td>1042</td><td>개체에 대한 권한 정보를 수집하는 중…</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ISLockPermissionsInstall</td><td>1042</td><td>개체에 대한 권한 정보를 적용하는 중…</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_InitializeODBCDirs</td><td>1042</td><td>ODBC 디렉터리를 초기화하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_InstallODBC</td><td>1042</td><td>ODBC 구성 요소 설치 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_InstallServices</td><td>1042</td><td>새 서비스 설치 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_InstallingSystemCatalog</td><td>1042</td><td>시스템 카탈로그 설치</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_KeyName</td><td>1042</td><td>키: [1], 이름: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_KeyNameValue</td><td>1042</td><td>키: [1], 이름: [2], 값: [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_LibId</td><td>1042</td><td>LibID: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Libid2</td><td>1042</td><td>LibID: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_MigratingFeatureStates</td><td>1042</td><td>관련 응용 프로그램에서 기능을 이동하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_MovingFiles</td><td>1042</td><td>파일을 옮기는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_NameValueAction</td><td>1042</td><td>이름: [1], 값: [2], 수행 [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_NameValueAction2</td><td>1042</td><td>이름: [1], 값: [2], 수행 [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_PatchingFiles</td><td>1042</td><td>파일 패치 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ProgID</td><td>1042</td><td>ProgId: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_ProgID2</td><td>1042</td><td>ProgId: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_PropertySignature</td><td>1042</td><td>속성: [1], 서명: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_PublishProductFeatures</td><td>1042</td><td>제품 기능을 게시하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_PublishProductInfo</td><td>1042</td><td>제품 정보를 게시하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_PublishingQualifiedComponents</td><td>1042</td><td>해당 구성 요소를 게시하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegUser</td><td>1042</td><td>사용자를 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterClassServer</td><td>1042</td><td>Class 서버를 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterExtensionServers</td><td>1042</td><td>익스텐션 서버를 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterFonts</td><td>1042</td><td>글꼴을 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterMimeInfo</td><td>1042</td><td>MIME 정보를 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterTypeLibs</td><td>1042</td><td>형식 라이브러리를 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringComPlus</td><td>1042</td><td>COM+ 응용 프로그램과 구성 요소를 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringModules</td><td>1042</td><td>모듈을 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringProduct</td><td>1042</td><td>제품을 등록하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringProgIdentifiers</td><td>1042</td><td>프로그램 확인자의 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemoveApps</td><td>1042</td><td>응용 프로그램을 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingBackup</td><td>1042</td><td>백업 파일을 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingDuplicates</td><td>1042</td><td>중복 파일을 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingFiles</td><td>1042</td><td>파일을 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingFolders</td><td>1042</td><td>폴더를 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingIISRoots</td><td>1042</td><td>IIS 가상 루트를 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingIni</td><td>1042</td><td>INI 파일 항목을 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingMoved</td><td>1042</td><td>이동한 파일을 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingODBC</td><td>1042</td><td>ODBC 구성 요소 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingRegistry</td><td>1042</td><td>시스템 레지스트리 값을 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingShortcuts</td><td>1042</td><td>바로 가기를 제거하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_RollingBack</td><td>1042</td><td>롤백을 수행하는 중:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_SearchForRelated</td><td>1042</td><td>관련 응용 프로그램을 검색하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_SearchInstalled</td><td>1042</td><td>설치한 응용 프로그램 확인 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_SearchingQualifyingProducts</td><td>1042</td><td>제품을 확인하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_SearchingQualifyingProducts2</td><td>1042</td><td>제품을 확인하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Service</td><td>1042</td><td>서비스: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Service2</td><td>1042</td><td>서비스: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Service3</td><td>1042</td><td>서비스: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Service4</td><td>1042</td><td>서비스: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Shortcut</td><td>1042</td><td>바로 가기: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Shortcut1</td><td>1042</td><td>바로 가기: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_StartingServices</td><td>1042</td><td>서비스를 시작하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_StoppingServices</td><td>1042</td><td>서비스를 중지하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishProductFeatures</td><td>1042</td><td>제품 기능의 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishQualified</td><td>1042</td><td>해당 구성 요소의 게시를 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishingProductInfo</td><td>1042</td><td>제품 정보의 게시를 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnregTypeLibs</td><td>1042</td><td>형식 라이브러리의 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterClassServers</td><td>1042</td><td>Class 서버의 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterExtensionServers</td><td>1042</td><td>extension 서버의 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterModules</td><td>1042</td><td>모듈 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringComPlus</td><td>1042</td><td>COM+ 응용 프로그램과 구성 요소의 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringFonts</td><td>1042</td><td>글꼴 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringMimeInfo</td><td>1042</td><td>MIME 정보의 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringProgramIds</td><td>1042</td><td>프로그램 확인자의 등록을 해제하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UpdateComponentRegistration</td><td>1042</td><td>구성 요소 등록을 업데이트하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_UpdateEnvironmentStrings</td><td>1042</td><td>환경 문자열을 업데이트하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_Validating</td><td>1042</td><td>설치를 유효화하는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_WritingINI</td><td>1042</td><td>INI 파일 값을 쓰는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ACTIONTEXT_WritingRegistry</td><td>1042</td><td>시스템 레지스트리 값을 쓰는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_BACK</td><td>1042</td><td>&lt; 뒤로(&amp;B)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_CANCEL</td><td>1042</td><td>취소</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_CANCEL2</td><td>1042</td><td>{&amp;Tahoma8} 취소</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_CHANGE</td><td>1042</td><td>바꾸기(&amp;C)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_COST</td><td>1042</td><td>COM+ 애플리케이션 비용 산출: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_INSTALL</td><td>1042</td><td>COM+ 애플리케이션 설치: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_UNINSTALL</td><td>1042</td><td>COM+ 애플리케이션 제거: [1]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_DIALOG_TEXT2_DESCRIPTION</td><td>1042</td><td>Dialog 보통 설명</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_DIALOG_TEXT_DESCRIPTION_EXTERIOR</td><td>1042</td><td>{&amp;TahomaBold10}Dialog 굵게 제목</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_DIALOG_TEXT_DESCRIPTION_INTERIOR</td><td>1042</td><td>{&amp;MSSansBold8}Dialog 굵게 제목</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_DIFX_AMD64</td><td>1042</td><td>[ProductName]에는 X64 프로세서가 필요합니다. 마법사를 종료하려면 [확인]을 클릭하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_DIFX_IA64</td><td>1042</td><td>[ProductName]에는 IA64 프로세서가 필요합니다. 마법사를 종료하려면 [확인]을 클릭하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_DIFX_X86</td><td>1042</td><td>[ProductName]에는 X86 프로세서가 필요합니다. 마법사를 종료하려면 [확인]을 클릭하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_DatabaseFolder_InstallDatabaseTo</td><td>1042</td><td>[ProductName]을 다음 위치에 설치하십시오:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_0</td><td>1042</td><td>{{심각한 오류: }}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1</td><td>1042</td><td>{{오류 [1]. }}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_10</td><td>1042</td><td>=== 기록 시작: [Date]  [Time] ===</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_100</td><td>1042</td><td>바로 가기 [2]을(를) 제거하지 못했습니다. 바로 가기 파일이 실제로 있는지, 그리고 그 파일을 액세스할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_101</td><td>1042</td><td>파일 [2]의 형식 라이브러리를 등록하지 못했습니다. 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_102</td><td>1042</td><td>파일 [2]의 형식 라이브러리를 등록하지 못했습니다. 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_103</td><td>1042</td><td>ini 파일 [2][3]을(를) 업데이트하지 못했습니다. 그 파일이 실제로 있는지, 그리고 파일에 액세스할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_104</td><td>1042</td><td>리부팅할 때 파일 [2]을(를) [3](으)로 바꾸도록 설정하지 못했습니다. 파일 [3]에 쓰기 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_105</td><td>1042</td><td>ODBC 드라이버 관리자를 제거하는데 오류가 생겼습니다. ODBC 오류 [2]: [3]. 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_106</td><td>1042</td><td>ODBC 드라이버 관리자를 설치하는 중에 오류가 생겼습니다. ODBC 오류 [2]: [3]. 소속된 부서의 기술 지원 담당자에게 문의하십시외.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_107</td><td>1042</td><td>ODBC 드라이버 제거하는데 오류가 생겼습니다:[4]. ODBC 오류 [2]: [3]. ODBC 드라이버를 제거할 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_108</td><td>1042</td><td>ODBC 드라이버를 설치하는 중에 오류가 생겼습니다: [4]. ODBC 오류 [2]: [3]. [4] 파일이 실제로 있는지, 그리고 그 파일을 액세스할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_109</td><td>1042</td><td>ODBC 데이터 원본을 구성하는 중에 오류가 생겼습니다: [4]. ODBC 오류 [2]: [3]. [4] 파일이 실제로 있는지, 그리고 그 파일에 액세스할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_11</td><td>1042</td><td>=== 기록 시작: [Date]  [Time] ===</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_110</td><td>1042</td><td>서비스 '[2]'([3])을(를) 시작하는데 실패했습니다. 시스템 서비스를 시작할 수 있는 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_111</td><td>1042</td><td>서비스 '[2]'([3])을(를) 중지하지 못했습니다. 시스템 서비스를 중단할 수 있는 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_112</td><td>1042</td><td>서비스 '[2]'([3])을(를) 삭제하지 못했습니다. 시스템 서비스를 삭제할 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_113</td><td>1042</td><td>서비스 '[2]'([3])을(를) 설치하지 못했습니다. 시스템 서비스를 설치할 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_114</td><td>1042</td><td>환경 변수 '[2]'을(를) 업데이트하지 못했습니다. 환경 변수를 수정할 수 있는 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_115</td><td>1042</td><td>모든 사용자가 함께 사용할 수 있는 시스템에 설치 작업을 실행할 수 있는 권한이 없습니다. 관리자 권한으로 로그온해서 설치를 다시 실행하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_116</td><td>1042</td><td>'[3]' 파일에 대한 파일 보안을 설정하지 못했습니다. 오류: [2]. 이 파일에 대한 보안을 수정할 수 있는 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_117</td><td>1042</td><td>구성 요소 서비스(COM+ 1.0)가 이 컴퓨터에 설치되어 있지 않습니다. Component Service가 있어야 설치를 끝낼 수 있습니다. Component Service는 Windows 2000에서 사용할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_118</td><td>1042</td><td>COM+ 응용 프로그램을 등록하는 중에 오류가 생겼습니다. 자세한 내용은 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_119</td><td>1042</td><td>COM+ 응용 프로그램의 등록을 해제하는 중에 오류가 생겼습니다. 자세한 사항은 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_12</td><td>1042</td><td>수행 시작 [Time]: [1].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_120</td><td>1042</td><td>이 응용 프로그램의 이전 버전을 제거하는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_121</td><td>1042</td><td>이 응용 프로그램의 이전 버전을 제거하기 위해 준비하는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_122</td><td>1042</td><td>파일 [2]에 패치를 적용하는 중에 오류가 생겼습니다. 다른 방법으로 업데이트되었기 때문에 이 패치를 사용해서 수정할 수 없습니다. 자세한 사항은 패치를 구입한 대리점에 문의하십시오. {{시스템 오류: [3]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_123</td><td>1042</td><td>[2](이)가 필요한 제품 중 하나를 설치할 수 없습니다. 소속된 부서의 기술 지원 담당자에게 문의하십시오. {{시스템 오류: [3].}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_124</td><td>1042</td><td>이전 버전의 [2]을(를) 제거할 수 없습니다. 소속된 부서의 기술 지원 담당자에게 문의하십시오. {{시스템 오류 [3].}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_125</td><td>1042</td><td>서비스 '[2]'([3])의 설명을 바꾸지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_126</td><td>1042</td><td>시스템 파일 [2]을(를) Windows에서 보호하고 있어서 업데이트할 수 없습니다. 이 파일을 제대로 실행하려면 이 프로그램의 운영 체제를 업데이트해야 합니다. {{패키지 버전: [3], OS 보호 버전: [4]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_127</td><td>1042</td><td>Windows Installer 서비스가 보호된 Windows 파일 [2]을(를) 업데이트하지 못했습니다. {{패키지 버전: [3], OS 보호 버전: [4], SFP 오류: [5]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_128</td><td>1042</td><td>Windows Installer 서비스에서 하나 이상의 보호된 Windows 파일을 업데이트할 수 없습니다. SFP 오류: [2]. 보호된 파일 목록: [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_129</td><td>1042</td><td>시스템 정책에 따라 사용자 설치를 사용할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_13</td><td>1042</td><td>수행 끝 [Time]: [1]. 리턴 값 [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_130</td><td>1042</td><td>이 설치 프로그램이 IIS 가상 루트를 구성하려면 Internet Information Server 4.0 이상이 필요합니다. IIS 4.0 이상이 설치되어 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_131</td><td>1042</td><td>이 설치 프로그램이 IIS 가상 루트를 구성하려면 관리자 권한이 필요합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1329</td><td>1042</td><td>캐비닛 파일 [2]이(가) 디지털 서명되어 있지 않기 때문에 필요한 파일을 설치할 수 없습니다. 캐비닛 파일이 손상되었을 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1330</td><td>1042</td><td>캐비닛 파일 [2]의 디지털 서명이 잘못되었기 때문에 필요한 파일을 설치할 수 없습니다. 캐비닛 파일이 손상된 것 같습니다.{ WinVerifyTrust에서 [3] 오류를 반환했습니다.}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1331</td><td>1042</td><td>[2] 파일을 제대로 복사하지 못했습니다. CRC 오류입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1332</td><td>1042</td><td>[2] 파일을 제대로 패치하지 못했습니다. CRC 오류입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1333</td><td>1042</td><td>[2] 파일을 제대로 패치하지 못했습니다. CRC 오류입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1334</td><td>1042</td><td>캐비닛 파일 '[3]'에서 '[2]' 파일을 찾을 수 없으므로 이 파일을 설치할 수 없습니다. 네트워크 오류이거나 CD-ROM 읽기 오류이거나 이 패키지의 문제일 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1335</td><td>1042</td><td>이 설치에 필요한 캐비닛 파일 '[2]'이(가) 손상되어 사용할 수 없습니다. 네트워크 오류이거나 CD-ROM 읽기 오류이거나 이 패키지의 문제일 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1336</td><td>1042</td><td>이 설치를 완료하는 데 필요한 임시 파일을 작성하는 중 오류가 발생했습니다. 폴더: [3]. 시스템 오류 코드: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_14</td><td>1042</td><td>남은 시간: {[1] 분 }{[2] 초}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_15</td><td>1042</td><td>메모리가 부족합니다. 다른 응용 프로그램을 종료한 후 다시 시도하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_16</td><td>1042</td><td>Windows Installer가 더 이상 응답하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1609</td><td>1042</td><td>보안 설정을 적용하는 동안 오류가 발생했습니다. [2]은(는) 유효한 사용자 또는 그룹이 아닙니다. 패키지에 문제가 있거나 네트워크의 도메인 컨트롤러에 연결하는 데 문제가 있을 수 있습니다. 네트워크 연결을 확인하고 [다시 시도]를 클릭하거나, 설치를 마치려면 [취소]를 클릭하십시오. 사용자의 SID를 찾을 수 없습니다. 시스템 오류 [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1651</td><td>1042</td><td>관리자가 광고 상태의 사용자 단위 관리 또는 컴퓨터 단위 응용 프로그램에 패치를 적용하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_17</td><td>1042</td><td>Windows Installer가 완전히 중지되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1715</td><td>1042</td><td>[2] 설치됨.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1716</td><td>1042</td><td>[2] 구성됨.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1717</td><td>1042</td><td>[2] 제거됨.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1718</td><td>1042</td><td>디지털 서명 정책이 [2] 파일을 거부했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1719</td><td>1042</td><td>Windows Installer 서비스에 액세스할 수 없습니다. 고객 지원 담당자에게 문의하여 Windows Installer 서비스가 제대로 등록되어 사용할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1720</td><td>1042</td><td>Windows Installer 패키지에 문제가 있습니다. 설치를 완료하기 위해 필요한 스크립트를 실행할 수 없습니다. 고객 지원 담당자나 패키지 공급업체에 문의하십시오. 사용자 지정 작업 [2] 스크립트 오류 [3], [4]: [5] 줄 [6], 열 [7], [8]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1721</td><td>1042</td><td>Windows Installer 패키지에 문제가 있습니다. 설치를 완료하기 위해 필요한 프로그램을 실행할 수 없습니다. 고객 지원 담당자나 패키지 공급업체에 문의하십시오. 작업: [2], 위치: [3], 명령: [4]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1722</td><td>1042</td><td>Windows Installer 패키지에 문제가 있습니다. 설치 프로그램의 일부로 실행한 프로그램이 예상대로 완료되지 않았습니다. 고객 지원 담당자나 패키지 공급업체에 문의하십시오. 작업 [2], 위치: [3], 명령: [4]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1723</td><td>1042</td><td>Windows Installer 패키지에 문제가 있습니다. 설치를 완료하기 위해 필요한 DLL을 실행할 수 없습니다. 고객 지원 담당자나 패키지 공급업체에 문의하십시오. 작업 [2], 항목: [3], 라이브러리: [4]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1724</td><td>1042</td><td>제거를 성공적으로 완료했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1725</td><td>1042</td><td>제거하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1726</td><td>1042</td><td>보급이 잘 끝났습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1727</td><td>1042</td><td>보급하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1728</td><td>1042</td><td>구성을 성공적으로 완료했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1729</td><td>1042</td><td>구성하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1730</td><td>1042</td><td>Administrator만이 이 응용 프로그램을 제거할 수 없습니다. 이 응용 프로그램을 제거하려면, Administrator로 로그온하거나, 기술 지원 그룹에 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1731</td><td>1042</td><td>[2] 제품의 원본 설치 패키지가 클라이언트 패키지와 동기화되어 있지 않습니다. '[3]' 설치 패키지의 유효한 사본을 사용하여 다시 설치해 보십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1732</td><td>1042</td><td>[2] 설치를 완료하려면 컴퓨터를 다시 시작해야 합니다. 현재 다른 사용자가 이 컴퓨터에 로그온하고 있어서 컴퓨터를 다시 시작하면 다른 사용자의 작업을 손실할 수도 있습니다. 지금 다시 시작하시겠습니까?</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_18</td><td>1042</td><td>[ProductName]을(를) 구성하고 있습니다. 기다리십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_19</td><td>1042</td><td>필요한 정보를 모으는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1935</td><td>1042</td><td>어셈블리 구성 요소 '[2]'을(를) 설치하는 동안 오류가 발생했습니다. HRESULT: [3]. {{어셈블리 인터페이스: [4], 함수: [5], 어셈블리 이름: [6]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1936</td><td>1042</td><td>어셈블리 '[6]'을(를) 설치하는 동안 오류가 발생했습니다. 어셈블리가 강력한 이름을 가지고 있지 않거나 최소 키 길이로 서명되어 있지 않습니다. HRESULT: [3]. {{어셈블리 인터페이스: [4], 함수: [5], 구성 요소: [2]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1937</td><td>1042</td><td>어셈블리 '[6]'을(를) 설치하는 동안 오류가 발생했습니다. 서명이나 카탈로그를 확인할 수 없거나 유효하지 않습니다. HRESULT: [3]. {{어셈블리 인터페이스: [4], 함수: [5], 구성 요소: [2]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_1938</td><td>1042</td><td>어셈블리 '[6]'을(를) 설치하는 동안 오류가 발생했습니다. 하나 이상의 어셈블리 모듈을 찾을 수 없습니다. HRESULT: [3]. {{어셈블리 인터페이스: [4], 함수: [5], 구성 요소: [2]}}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2</td><td>1042</td><td>경고 [1]. </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_20</td><td>1042</td><td>{[ProductName] }설치가 성공적으로 완료되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_21</td><td>1042</td><td>{[ProductName] }설치를 실패했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2101</td><td>1042</td><td>운영 체제에서 지원하지 않는 바로 가기입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2102</td><td>1042</td><td>잘못된 .ini 작업: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2103</td><td>1042</td><td>셸 폴더 [2]에 대한 경로를 확인할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2104</td><td>1042</td><td>.ini 파일 쓰기: [3]: 시스템 오류: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2105</td><td>1042</td><td>바로 가기 [3]을(를) 만들지 못했습니다. 시스템 오류: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2106</td><td>1042</td><td>바로 가기 [3]을(를) 삭제하지 못했습니다. 시스템 오류: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2107</td><td>1042</td><td>형식 라이브러리 [2]을(를) 등록하는 동안 오류 [3]이(가) 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2108</td><td>1042</td><td>형식 라이브러리 [2]의 등록을 취소하는 동안 오류 [3]이(가) 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2109</td><td>1042</td><td>.ini 작업에 대한 섹션이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2110</td><td>1042</td><td>.ini 작업에 대한 키가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2111</td><td>1042</td><td>실행 중인 응용 프로그램을 검색하지 못했으므로 성능 데이터를 가져올 수 없습니다. 반환된 등록 작업: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2112</td><td>1042</td><td>실행 중인 응용 프로그램을 검색하지 못했으므로 성능 인덱스를 가져올 수 없습니다. 반환된 등록 작업: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2113</td><td>1042</td><td>실행 중인 응용 프로그램을 검색하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_22</td><td>1042</td><td>파일 읽기 오류: [2]. {{ 시스템 오류 [3].}}  파일이 실제로 있는지, 그리고 그 파일에 액세스할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2200</td><td>1042</td><td>데이터베이스: [2]. 데이터베이스 개체를 만들지 못했습니다. 모드 = [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2201</td><td>1042</td><td>데이터베이스: [2]. 메모리 부족으로 초기화되지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2202</td><td>1042</td><td>데이터베이스: [2]. 메모리 부족으로 데이터에 액세스하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2203</td><td>1042</td><td>데이터베이스: [2]. 데이터베이스 파일을 열 수 없습니다. 시스템 오류 [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2204</td><td>1042</td><td>데이터베이스: [2]. 테이블이 이미 있습니다: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2205</td><td>1042</td><td>데이터베이스: [2]. 테이블이 없습니다: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2206</td><td>1042</td><td>데이터베이스: [2]. 테이블을 삭제할 수 없습니다: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2207</td><td>1042</td><td>데이터베이스: [2]. 의도 위반.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2208</td><td>1042</td><td>데이터베이스: [2]. 매개 변수가 부족하여 실행할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2209</td><td>1042</td><td>데이터베이스: [2]. 커서의 상태가 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2210</td><td>1042</td><td>데이터베이스: [2]. 열 [3]의 업데이트 데이터 형식이 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2211</td><td>1042</td><td>데이터베이스: [2]. 테이블 테이블 [3]을(를) 만들 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2212</td><td>1042</td><td>데이터베이스: [2]. 데이터베이스가 쓰기 가능 상태가 아닙니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2213</td><td>1042</td><td>데이터베이스: [2]. 데이터베이스 테이블을 저장하는 중 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2214</td><td>1042</td><td>데이터베이스: [2]. 내보낼 파일을 쓰는 중 오류가 발생했습니다. [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2215</td><td>1042</td><td>데이터베이스: [2]. 가져올 파일을 열 수 없습니다: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2216</td><td>1042</td><td>데이터베이스: [2]. 파일 가져오기 형식 오류: [3], 줄 [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2217</td><td>1042</td><td>데이터베이스: [2]. CreateOutputDatabase [3]의 상태가 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2218</td><td>1042</td><td>데이터베이스: [2]. 테이블 이름을 제공하지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2219</td><td>1042</td><td>데이터베이스: [2]. 설치 관리자 데이터베이스 형식이 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2220</td><td>1042</td><td>데이터베이스: [2]. 행/필드 데이터가 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2221</td><td>1042</td><td>데이터베이스: [2]. 가져올 파일의 코드 페이지가 충돌합니다. [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2222</td><td>1042</td><td>데이터베이스: [2]. 변환 또는 병합 코드 페이지 [3]이(가) 데이터베이스 코드 페이지 [4]과(와) 다릅니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2223</td><td>1042</td><td>데이터베이스: [2]. 데이터베이스가 동일하여 변환이 생성되지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2224</td><td>1042</td><td>데이터베이스: [2]. GenerateTransform: 데이터베이스가 손상되었습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2225</td><td>1042</td><td>데이터베이스: [2]. 변환: 임시 테이블을 변환할 수 없습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2226</td><td>1042</td><td>데이터베이스: [2]. 변환하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2227</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리에 잘못된 '[3]'이(가) 있습니다. [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2228</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리에 알 수 없는 테이블 '[3]'이(가) 있습니다. [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2229</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리의 테이블 '[3]'을(를) 로드할 수 없습니다. [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2230</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리에 반복되는 테이블 '[3]'이(가) 있습니다. [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2231</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리에 ')'가 없습니다. [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2232</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리에 예기치 않은 토큰 '[3]'이(가) 있습니다: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2233</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리의 SELECT 절에 열이 없습니다: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2234</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리의 ORDER BY 절에 열이 없음: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2235</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리에 열 '[3]'이(가) 없거나 모호합니다: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2236</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리에 잘못된 연산자 '[3]'이(가) 있습니다: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2237</td><td>1042</td><td>데이터베이스: [2]. 쿼리 문자열이 잘못되었거나 없습니다: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2238</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리에 FROM 절이 없습니다. [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2239</td><td>1042</td><td>데이터베이스: [2]. INSERT SQL 문에 값이 부족합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2240</td><td>1042</td><td>데이터베이스: [2]. UPDATE SQL 문에 업데이트 열이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2241</td><td>1042</td><td>데이터베이스: [2]. INSER SQL 문에 삽입 열이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2242</td><td>1042</td><td>데이터베이스: [2]. 열 '[3]'이(가) 반복됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2243</td><td>1042</td><td>데이터베이스: [2]. 테이블 생성을 위한 기본 열이 정의되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2244</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리 [4]에 잘못된 유형 지정자 '[3]'이(가) 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2245</td><td>1042</td><td>오류 [3](으)로 인해 IStorage::Stat가 실패했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2246</td><td>1042</td><td>데이터베이스: [2]. 설치 관리자 변환 형식이 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2247</td><td>1042</td><td>데이터베이스: [2] 스트림 읽기/쓰기 변환 실패.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2248</td><td>1042</td><td>데이터베이스: [2]. GenerateTransform/Merge: 기본 테이블의 열 유형이 참조 테이블과 일치하지 않습니다. 테이블: [3] 열 #: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2249</td><td>1042</td><td>데이터베이스: [2] GenerateTransform: 기본 테이블에 참조 테이블보다 더 많은 열이 있습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2250</td><td>1042</td><td>데이터베이스: [2] 변환: 기본 열을 추가할 수 없습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2251</td><td>1042</td><td>데이터베이스: [2] 변환: 존재하지 않는 행을 삭제할 수 없습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2252</td><td>1042</td><td>데이터베이스: [2] 변환: 기본 테이블을 추가할 수 없습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2253</td><td>1042</td><td>데이터베이스: [2] 변환: 존재하지 않는 테이블을 삭제할 수 없습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2254</td><td>1042</td><td>데이터베이스: [2] 변환: 존재하지 않는 행을 업데이트할 수 없습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2255</td><td>1042</td><td>데이터베이스: [2] 변환: 이름이 같은 열이 이미 있습니다. 테이블: [3] 열: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2256</td><td>1042</td><td>데이터베이스: [2]. GenerateTransform/Merge: 기본 테이블의 기본 키 수가 참조 테이블과 일치하지 않습니다. 테이블: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2257</td><td>1042</td><td>데이터베이스: [2]. 읽기 전용 테이블을 수정하려고 했습니다. [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2258</td><td>1042</td><td>데이터베이스: [2]. 매개 변수의 유형이 일치하지 않습니다. [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2259</td><td>1042</td><td>데이터베이스: [2] 테이블을 업데이트하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2260</td><td>1042</td><td>Storage CopyTo를 실패했습니다. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2261</td><td>1042</td><td>스트림 [2]을(를) 제거할 수 없습니다. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2262</td><td>1042</td><td>스트림이 없습니다: [2]. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2263</td><td>1042</td><td>스트림 [2]을(를) 열 수 없습니다. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2264</td><td>1042</td><td>스트림 [2]을(를) 제거할 수 없습니다. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2265</td><td>1042</td><td>저장소를 커밋할 수 없습니다. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2266</td><td>1042</td><td>저장소를 롤백할 수 없습니다. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2267</td><td>1042</td><td>저장소 [2]을(를) 삭제할 수 없습니다. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2268</td><td>1042</td><td>데이터베이스: [2]. 병합: [3] 테이블에서 병합 충돌이 보고되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2269</td><td>1042</td><td>데이터베이스: [2]. 병합: 두 데이터베이스의 '[3]' 테이블에 포함된 열 개수가 다릅니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2270</td><td>1042</td><td>데이터베이스: [2]. GenerateTransform/Merge: 기본 테이블의 열 이름이 참조 테이블과 일치하지 않습니다. 테이블: [3] 열 #: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2271</td><td>1042</td><td>SummaryInformation 쓰기가 실패했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2272</td><td>1042</td><td>데이터베이스: [2]. 데이터베이스가 읽기 전용으로 열려 있어 MergeDatabase가 변경 내용을 쓰지 못합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2273</td><td>1042</td><td>데이터베이스: [2]. MergeDatabase: 기본 데이터베이스에 대한 참조가 참조 데이터베이스로 전달되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2274</td><td>1042</td><td>데이터베이스: [2]. MergeDatabase: 오류 테이블에 오류를 쓸 수 없습니다. 미리 정의된 오류 테이블에 Null을 허용하지 않는 열이 있어 오류 테이블에 오류를 쓸 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2275</td><td>1042</td><td>데이터베이스: [2]. 지정된 [3] 수정 작업이 테이블 조인에 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2276</td><td>1042</td><td>데이터베이스: [2]. 코드 페이지 [3]은(는) 시스템에서 지원되지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2277</td><td>1042</td><td>데이터베이스: [2]. 테이블 [3]을(를) 저장하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2278</td><td>1042</td><td>데이터베이스: [2]. SQL 쿼리의 WHERE 절이 식 개수 제한(32개)을 초과했습니다: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2279</td><td>1042</td><td>데이터베이스: [2] 변환: 기본 테이블 [3]에 너무 많은 열이 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2280</td><td>1042</td><td>데이터베이스: [2]. 테이블 [4]의 열 [3]을(를) 만들 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2281</td><td>1042</td><td>스트림 [2]의 이름을 바꿀 수 없습니다. 시스템 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2282</td><td>1042</td><td>스트림 이름 [2]이(가) 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_23</td><td>1042</td><td>'[2]' 파일을 만들 수 없습니다. 이 이름을 가진 디렉터리가 이미 있습니다. 설치를 취소하고 다른 위치에 다시 설치해 보십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2302</td><td>1042</td><td>패치 알림: [2]바이트가 패치되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2303</td><td>1042</td><td>볼륨 정보 가져오기 오류. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2304</td><td>1042</td><td>사용 가능한 디스크 공간을 가져오는 동안 오류가 발생했습니다. GetLastError: [2]. 볼륨: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2305</td><td>1042</td><td>패치 스레드를 기다리는 동안 오류가 발생했습니다. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2306</td><td>1042</td><td>패치 적용을 위한 스레드를 만들 수 없습니다. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2307</td><td>1042</td><td>원본 파일 키 이름이 Null입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2308</td><td>1042</td><td>대상 파이 이름이 Null입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2309</td><td>1042</td><td>패치가 이미 진행 중인데 파일 [2]을(를) 패치하려고 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2310</td><td>1042</td><td>진행 중인 패치가 없는데 패치를 계속하려고 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2315</td><td>1042</td><td>경로 구분 기호가 없습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2318</td><td>1042</td><td>파일이 없습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2319</td><td>1042</td><td>파일 특성을 설정하는 동안 오류가 발생했습니다. [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2320</td><td>1042</td><td>파일에 쓸 수 없습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2321</td><td>1042</td><td>파일을 만드는 동안 오류가 발생했습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2322</td><td>1042</td><td>사용자가 취소했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2323</td><td>1042</td><td>파일 특성이 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2324</td><td>1042</td><td>파일을 열 수 없습니다: [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2325</td><td>1042</td><td>파일에 대한 파일 시간을 가져올 수 없습니다. [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2326</td><td>1042</td><td>FileToDosDateTime에 오류가 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2327</td><td>1042</td><td>디렉터리를 제거할 수 없습니다: [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2328</td><td>1042</td><td>파일에 대한 파일 버전 정보를 가져오는 동안 오류가 발생했습니다. [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2329</td><td>1042</td><td>파일을 삭제하는 동안 오류가 발생했습니다: [3]. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2330</td><td>1042</td><td>파일 특성을 가져오는 동안 오류가 발생했습니다. [3]. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2331</td><td>1042</td><td>라이브러리를 로드할 수 없거나 [2] 시작 지점을 찾을 수 없습니다 [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2332</td><td>1042</td><td>파일 특성을 가져오는 동안 오류가 발생했습니다. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2333</td><td>1042</td><td>파일 특성을 설정하는 동안 오류가 발생했습니다. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2334</td><td>1042</td><td>파일에 대한 파일 시간을 로컬 시간으로 변환하는 동안 오류가 발생했습니다: [3]. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2335</td><td>1042</td><td>경로 [2]이(가) [3]의 부모 항목이 아닙니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2336</td><td>1042</td><td>경로에 임시 파일을 만드는 동안 오류가 발생했습니다. [3]. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2337</td><td>1042</td><td>파일을 닫을 수 없습니다. [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2338</td><td>1042</td><td>파일에 대한 리소스를 업데이트할 수 없습니다. [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2339</td><td>1042</td><td>파일에 대한 파일 시간을 설정할 수 없습니다. [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2340</td><td>1042</td><td>파일에 대한 리소스를 업데이트할 수 없습니다. [3], 리소스가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2341</td><td>1042</td><td>파일에 대한 리소스를 업데이트할 수 없습니다. [3], 리소스가 너무 큽니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2342</td><td>1042</td><td>파일에 대한 리소스를 업데이트할 수 없습니다. [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2343</td><td>1042</td><td>지정된 경로가 비어 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2344</td><td>1042</td><td>파일의 유효성을 검사하는 데 필요한 IMAGEHLP.DLL 파일을 찾을 수 없습니다:[2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2345</td><td>1042</td><td>[2]: 파일에 유효한 체크섬 값이 들어 있지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2347</td><td>1042</td><td>사용자가 무시했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2348</td><td>1042</td><td>캐비닛 스트림에서 읽는 동안 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2349</td><td>1042</td><td>다른 정보를 사용하여 복사를 다시 시작했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2350</td><td>1042</td><td>FDI 서버 오류</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2351</td><td>1042</td><td>파일 키 '[2]'이(가) 캐비닛 '[3]'에 없습니다. 설치를 계속할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2352</td><td>1042</td><td>캐비닛 파일 서버를 초기화할 수 없습니다. 필요한 파일 'CABINET.DLL'이 없을 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2353</td><td>1042</td><td>캐비닛이 아닙니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2354</td><td>1042</td><td>캐비닛을 처리할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2355</td><td>1042</td><td>캐비닛이 손상되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2356</td><td>1042</td><td>스트림에서 캐비닛을 찾을 수 없습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2357</td><td>1042</td><td>특성을 설정할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2358</td><td>1042</td><td>파일이 사용 중인지 여부를 확인하는 동안 오류가 발생했습니다: [3]. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2359</td><td>1042</td><td>대상 파일을 만들 수 없습니다. 파일이 사용 중일 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2360</td><td>1042</td><td>진행률이 표시됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2361</td><td>1042</td><td>다음 캐비닛이 필요합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2362</td><td>1042</td><td>폴더를 찾을 수 없습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2363</td><td>1042</td><td>폴더의 하위 폴더를 열거할 수 없습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2364</td><td>1042</td><td>CreateCopier 호출에 잘못된 열거 상수가 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2365</td><td>1042</td><td>실행 파일 [2]에 대해 BindImage를 수행할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2366</td><td>1042</td><td>사용자 오류.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2367</td><td>1042</td><td>사용자가 중단했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2368</td><td>1042</td><td>네트워크 리소스 정보를 가져오지 못했습니다. 오류 [2], 네트워크 경로 [3]. 확장 오류: 네트워크 공급자 [5], 오류 코드 [4], 오류 설명 [6].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2370</td><td>1042</td><td>[2] 파일에 대한 CRC 체크섬 값이 잘못되었습니다.{ 헤더에는 체크섬 값이 [3](으)로 나타나며 계산 값은 [4]입니다.}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2371</td><td>1042</td><td>파일 [2]에 패치를 적용할 수 없습니다. GetLastError: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2372</td><td>1042</td><td>패치 파일 [2]이(가) 손상되었거나 형식이 잘못되었습니다. 파일 [3]을(를) 패치하려고 합니다. GetLastError: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2373</td><td>1042</td><td>파일 [2]이(가) 유효한 패치 파일이 아닙니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2374</td><td>1042</td><td>파일 [2]이(가) 패치 파일 [3]의 유효한 대상 파일이 아닙니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2375</td><td>1042</td><td>알 수 없는 패치 오류: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2376</td><td>1042</td><td>캐비닛이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2379</td><td>1042</td><td>읽기 위해 파일을 여는 동안 오류가 발생했습니다. [3] GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2380</td><td>1042</td><td>쓰기 위해 파일을 여는 동안 오류가 발생했습니다. [3]. GetLastError: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2381</td><td>1042</td><td>디렉터리가 없습니다. [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2382</td><td>1042</td><td>드라이브가 준비되지 못했습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_24</td><td>1042</td><td>다음 디스크를 넣으십시오: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2401</td><td>1042</td><td>32비트 운영 체제에서 키 [2]에 대해 64비트 레지스트리 작업이 시도되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2402</td><td>1042</td><td>메모리가 부족합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_25</td><td>1042</td><td>이 디렉터리에 액세스할 수 있는 권한이 충분하지 않습니다:[2]. 설치를 계속할 수 없습니다. 관리자 권한으로 로그온하거나 시스템 관리자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2501</td><td>1042</td><td>롤백 스크립트 열거자를 만들 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2502</td><td>1042</td><td>설치가 진행되고 있지 않을 때 InstallFinalize를 호출했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2503</td><td>1042</td><td>진행 중으로 표시되지 않은 RunScript를 호출했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_26</td><td>1042</td><td>파일 쓰기 오류: [2]. 그 디렉터리에 액세스할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2601</td><td>1042</td><td>속성 [2]의 값이 잘못되었습니다: '[3]'</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2602</td><td>1042</td><td>미디어 테이블에 [2] 테이블 항목 '[3]'과(와) 연결된 항목이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2603</td><td>1042</td><td>테이블 이름 [2]이(가) 중복되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2604</td><td>1042</td><td>[2] 속성이 정의되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2605</td><td>1042</td><td>[3] 또는 [4]에서 서버 [2]을(를) 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2606</td><td>1042</td><td>속성 [2]의 값이 유효한 전체 경로가 아닙니다. '[3]'.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2607</td><td>1042</td><td>파일 설치에 필요한 미디어 테이블이 없거나 비어 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2608</td><td>1042</td><td>개체에 대한 보안 설명자를 만들 수 없습니다. 오류: '[2]'.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2609</td><td>1042</td><td>초기화하기 전에 제품 설정을 마이그레이션하려고 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2611</td><td>1042</td><td>파일 [2]이(가) 압축된 것으로 표시되어 있지만 관련된 미디어 항목이 캐비닛을 지정하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2612</td><td>1042</td><td>'[2]' 열에 스트림이 없습니다. 기본 키: '[3]'.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2613</td><td>1042</td><td>RemoveExistingProducts 작업 순서가 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2614</td><td>1042</td><td>설치 패키지에서 IStorage 개체에 액세스할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2615</td><td>1042</td><td>원본 확인 오류로 인해 모듈 [2]의 등록 취소를 건너뛰었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2616</td><td>1042</td><td>수반되는 파일 [2]에 부모 항목이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2617</td><td>1042</td><td>공유 구성 요소 [2]을(를) 구성 요소 테이블에서 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2618</td><td>1042</td><td>격리된 응용 프로그램 구성 요소 [2]을(를) 구성 요소 테이블에서 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2619</td><td>1042</td><td>격리된 구성 요소 [2], [3]이(가) 동일한 기능에 속하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2620</td><td>1042</td><td>격리된 응용 프로그램 구성 요소 [2]의 키 파일이 파일 테이블에 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2621</td><td>1042</td><td>바로 가기 [2] 집합에 대한 리소스 DLL 또는 리소스 ID 정보가 잘못 설정되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27</td><td>1042</td><td>[2] 파일을 읽다가 오류가 생겼습니다. {{ 시스템 오류 [3].}} 파일이 실제로 있는지, 그리고 그 파일에 액세스할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2701</td><td>1042</td><td>기능의 깊이가 [2] 레벨의 허용 가능한 트리 깊이를 초과합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2702</td><td>1042</td><td>기능 테이블 레코드([2])가 특성 필드에 존재하지 않는 부모 항목을 참조합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2703</td><td>1042</td><td>루트 원본 경로에 대한 속성 이름이 정의되지 않았습니다: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2704</td><td>1042</td><td>루트 디렉터리 속성이 정의되어 있지 않습니다. [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2705</td><td>1042</td><td>잘못된 테이블: [2]. 트리로 연결할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2706</td><td>1042</td><td>원본 경로가 생성되지 않았습니다. 디렉터리 테이블에 항목 [2]에 대한 경로가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2707</td><td>1042</td><td>대상 경로가 생성되지 않았습니다. 디렉터리 테이블에 항목 [2]에 대한 경로가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2708</td><td>1042</td><td>파일 테이블에 항목이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2709</td><td>1042</td><td>지정된 구성 요소 이름('[2]')을 구성 요소 테이블에서 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2710</td><td>1042</td><td>요청된 ‘Select’ 상태는 이 구성 요소에 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2711</td><td>1042</td><td>지정된 기능 이름('[2]')을 기능 테이블에서 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2712</td><td>1042</td><td>작업 [2]에서 모덜리스 대화 상자 [3](으)로부터 잘못된 값이 반환되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2713</td><td>1042</td><td>Null을 허용하지 않는 열에 Null 값이 있습니다('[4]' 테이블, '[3]' 열의 '[2]').</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2714</td><td>1042</td><td>기본 폴더 이름에 대한 값이 잘못되었습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2715</td><td>1042</td><td>지정된 파일 키('[2]')를 파일 테이블에서 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2716</td><td>1042</td><td>구성 요소 '[2]'에 대해 임의 하위 구성 요소 이름을 만들 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2717</td><td>1042</td><td>작업 조건이 잘못되었거나 사용자 지정 작업 '[2]'을(를) 호출하는 동안 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2718</td><td>1042</td><td>제품 코드 '[2]'에 대한 패키지 이름이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2719</td><td>1042</td><td>원본 '[2]'에 UNC와 드라이브 문자 경로가 둘 다 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2720</td><td>1042</td><td>원본 목록 키를 여는 동안 오류가 발생했습니다. 오류: '[2]'</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2721</td><td>1042</td><td>사용자 지정 작업 [2]을(를) 이진 테이블 스트림에서 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2722</td><td>1042</td><td>사용자 지정 작업 [2]을(를) 파일 테이블에서 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2723</td><td>1042</td><td>사용자 지정 작업 [2]이(가) 지원되지 않은 유형을 지정합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2724</td><td>1042</td><td>설치를 실행 중인 미디어의 볼륨 레이블 '[2]'이(가) 미디어 테이블에 지정한 레이블 '[3]'과(와) 일치하지 않습니다. 이러한 상황은 미디어 테이블에 항목이 하나만 있을 때만 허용됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2725</td><td>1042</td><td>데이터베이스 테이블이 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2726</td><td>1042</td><td>작업 찾을 수 없습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2727</td><td>1042</td><td>디렉터리 항목 '[2]'이(가) 디렉터리 테이블에 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2728</td><td>1042</td><td>테이블 정의 오류: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2729</td><td>1042</td><td>설치 엔진이 초기화되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2730</td><td>1042</td><td>데이터베이스에 잘못된 값이 있습니다. 테이블: '[2]', 기본 키: '[3]', 명령: '[4]'</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2731</td><td>1042</td><td>선택 관리자가 초기화되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2732</td><td>1042</td><td>디렉터리 관리자가 초기화되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2733</td><td>1042</td><td>'[4]' 테이블의 '[3]' 열에 잘못된 외래 키('[2]')가 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2734</td><td>1042</td><td>다시 설치 모드 문자가 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2735</td><td>1042</td><td>사용자 지정 작업 '[2]'이(가) 처리되지 않은 예외를 발생시키고 중지되었습니다. 사용자 지정 작업에 액세스 위반과 같은 내부 오류가 발생했기 때문일 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2736</td><td>1042</td><td>사용자 지정 작업 임시 파일을 생성하지 못했습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2737</td><td>1042</td><td>사용자 지정 작업 [2], 항목 [3], 라이브러리 [4]에 액세스할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2738</td><td>1042</td><td>사용자 지정 작업 [2]에 대한 VBScript 런타임에 액세스할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2739</td><td>1042</td><td>사용자 지정 작업 [2]에 대한 JavaScript 런타임에 액세스할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2740</td><td>1042</td><td>사용자 지정 조치 [2] 스크립트 오류 [3], [4]: [5] 라인 [6], 열 [7], [8].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2741</td><td>1042</td><td>제품 [2]에 대한 구성 정보가 손상되었습니다. 잘못된 정보: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2742</td><td>1042</td><td>서버로 마샬링하지 못했습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2743</td><td>1042</td><td>사용자 지정 작업 [2]을(를) 실행할 수 없습니다. 위치: [3], 명령: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2744</td><td>1042</td><td>사용자 지정 작업 [2]에서 EXE를 호출하지 못했습니다. 위치: [3], 명령: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2745</td><td>1042</td><td>변환 [2]이(가) 패키지 [3]에 적합하지 않습니다. 언어가 [4]이어야 하는데 [5]입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2746</td><td>1042</td><td>변환 [2]이(가) 패키지 [3]에 적합하지 않습니다. 제품이 [4]이어야 하는데 [5]입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2747</td><td>1042</td><td>변환 [2]이(가) 패키지 [3]에 적합하지 않습니다. 제품 버전이 &lt; [4]이어야 하는데 [5]입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2748</td><td>1042</td><td>변환 [2]이(가) 패키지 [3]에 적합하지 않습니다. 제품 버전이 &lt;= [4]이어야 하는데 [5]입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2749</td><td>1042</td><td>변환 [2]이(가) 패키지 [3]에 적합하지 않습니다. 제품 버전이 == [4]이어야 하는데 [5]입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2750</td><td>1042</td><td>변환 [2]이(가) 패키지 [3]에 적합하지 않습니다. 제품 버전이 &gt;= [4]이어야 하는데 [5]입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27502</td><td>1042</td><td>[2] 서버에 연결할 수 없습니다 [3]. [4]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27503</td><td>1042</td><td>[2] 서버로부터 버전 스트링을 불러올 수 없습니다. '[3]'. [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27504</td><td>1042</td><td>SQL 버전 요구 사항에 맞지 않습니다: [3]. 이 설치는 [2] [4] 이상이 필요합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27505</td><td>1042</td><td>SQL 스크립트 파일을 열 수 없습니다 [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27506</td><td>1042</td><td>SQL 스크립트를 실행할 수 없습니다 [2]. 라인 [3]. [4]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27507</td><td>1042</td><td>데이터베이스 서버를 찾거나 연결하려면 MDAC를 설치해야 합니다.  설치를 종료합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27508</td><td>1042</td><td>COM+ 응용 프로그램 설치할 수 없습니다. [2]  [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27509</td><td>1042</td><td>COM+ 응용 프로그램 제거할 수 없습니다. [2]  [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2751</td><td>1042</td><td>변환 [2]이(가) 패키지 [3]에 적합하지 않습니다. 제품 버전이 &gt; [4]이어야 하는데 [5]입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27510</td><td>1042</td><td>COM+ 응용 프로그램 설치할 수 없습니다. [2]  System.EnterpriseServices.RegistrationHelper 객체를 만들 수 없습니다.  Microsoft(R) .NET 서비스 구성 요소를 등록하려면 Microsoft(R) .NET Framework를 설치해야 합니다. </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27511</td><td>1042</td><td>SQL 스크립트 파일을 실행할 수 없습니다. [2] 열 수 없는 연결:  [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27512</td><td>1042</td><td>[2] 서버 트랜잭션을 시작할 수 없습니다. [3] 데이터베이스 [4]. [5]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27513</td><td>1042</td><td>[2] 서버 트랜잭션을 확인할 수 없습니다. [3] 데이터베이스 [4]. [5]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27514</td><td>1042</td><td>설치를 마치려면 Microsoft SQL Server가 필요합니다. 지정한 서버 '[3]'은(는) Microsoft SQL Server Desktop Engine 또는 SQL Server Express입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27515</td><td>1042</td><td>[2] 서버로부터 스키마 버전을 불러올 수 없습니다. '[3]' 데이터베이스:  '[4]'. [5]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27516</td><td>1042</td><td>[2] 서버에 스키마 버전을 쓸 수 없습니다. '[3]' 데이터베이스:  '[4]'. [5]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27517</td><td>1042</td><td>이 설치에서는 COM+ 응용 프로그램 설치를 위한 관리자 권한이 필요합니다. administrator로 로그온한 후 다시 이 설치를 시도하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27518</td><td>1042</td><td>COM+ 응용 프로그램 "[2]"은(는) NT 서비스로 실행하도록 구성되어 있으며, 시스템에 COM+ 1.5 이상이 설치되어 있어야 합니다. 현재 시스템에는 COM+ 1.0이 설치되어 있으므로 이 응용 프로그램이 설치되지 않을 것입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27519</td><td>1042</td><td>XML 파일 [2]을(를) 업데이트하는 동안 오류가 발생했습니다. [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2752</td><td>1042</td><td>패키지 [4]의 자식 저장소로 저장된 변환 [2]을(를) 열 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27520</td><td>1042</td><td>XML 파일 [2]을(를) 여는 동안 오류가 발생했습니다. [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27521</td><td>1042</td><td>XML 파일을 구성하려면 MSXML 3.0 이상이 필요합니다. 버전 3.0 이상이 설치되어 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27522</td><td>1042</td><td>XML 파일 [2]을(를) 만드는 동안 오류가 발생했습니다. [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27523</td><td>1042</td><td>서버를 로드하는 동안 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27524</td><td>1042</td><td>NetApi32.DLL을 로드하는 동안 오류가 발생했습니다. ISNetApi.dll을 사용하려면 NetApi32.DLL을 올바르게 로드해야 하며 NT 기반 운영 체제가 있어야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27525</td><td>1042</td><td>서버를 찾을 수 없습니다. 지정한 서버가 있는지 확인하십시오. 서버 이름은 반드시 입력해야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27526</td><td>1042</td><td>ISNetApi.dll에서 알 수 없는 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27527</td><td>1042</td><td>버퍼가 너무 작습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27528</td><td>1042</td><td>액세스가 거부되었습니다. 관리자 권한을 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27529</td><td>1042</td><td>컴퓨터가 올바르지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2753</td><td>1042</td><td>파일 '[2]'이(가) 설치용으로 표시되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27530</td><td>1042</td><td>NetAPI로부터 알 수 없는 오류가 반환되었습니다. 시스템 오류: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27531</td><td>1042</td><td>처리되지 않은 예외입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27532</td><td>1042</td><td>이 서버 또는 도메인의 사용자 이름이 올바르지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27533</td><td>1042</td><td>암호가 일치하지 않습니다. 암호는 대/소문자를 구분합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27534</td><td>1042</td><td>목록이 비어 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27535</td><td>1042</td><td>액세스 위반입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27536</td><td>1042</td><td>그룹을 가져오는 동안 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27537</td><td>1042</td><td>사용자를 그룹에 추가하는 동안 오류가 발생했습니다. 이 도메인 또는 서버에 대한 그룹이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27538</td><td>1042</td><td>사용자를 만드는 동안 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27539</td><td>1042</td><td>ERROR_NETAPI_ERROR_NOT_PRIMARY를 NetAPI에서 반환했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2754</td><td>1042</td><td>파일 '[2]'이(가) 유효한 패치 파일이 아닙니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27540</td><td>1042</td><td>지정한 사용자가 이미 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27541</td><td>1042</td><td>지정한 그룹이 이미 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27542</td><td>1042</td><td>암호가 잘못되었습니다. 암호가 네트워크 암호 정책에 맞는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27543</td><td>1042</td><td>이름이 올바르지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27544</td><td>1042</td><td>그룹이 올바르지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27545</td><td>1042</td><td>사용자 이름은 비워둘 수 없으며 DOMAIN\Username 형식이어야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27546</td><td>1042</td><td>TEMP 디렉터리에서 INI 파일을 로드하거나 만드는 동안 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27547</td><td>1042</td><td>ISNetAPI.dll이 로드되지 않았거나 DLL을 로드하는 동안 오류가 발생했습니다. 이 DLL을 로드해야 작업을 수행할 수 있습니다. DLL이 SUPPORTDIR 디렉터리에 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27548</td><td>1042</td><td>TEMP 디렉터리에서 새 사용자 정보가 포함된 INI 파일을 삭제하는 동안 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27549</td><td>1042</td><td>주 도메인 컨트롤러(PDC)를 가져오는 동안 오류가 발생했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2755</td><td>1042</td><td>패키지 [3]을(를) 설치하려는 중 서버에서 오류 [2]을(를) 반환했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27550</td><td>1042</td><td>사용자를 만들려면 모든 필드에 값이 있어야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27551</td><td>1042</td><td>[2]의 ODBC 드라이버를 찾을 수 없습니다. [2] 데이터베이스 서버에 연결하려면 이 드라이버가 필요합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27552</td><td>1042</td><td>데이터베이스 [4]을(를) 만드는 동안 오류가 발생했습니다. 서버: [2] [3]. [5]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27553</td><td>1042</td><td>데이터베이스 [4]에 연결하는 동안 오류가 발생했습니다. 서버: [2] [3]. [5]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27554</td><td>1042</td><td>연결 [2]을(를) 여는 동안 오류가 발생했습니다. 이 연결과 연결된 유효한 데이터베이스 메타테이터가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_27555</td><td>1042</td><td>'[2]' 개체에 권한을 적용하는 중 오류가 발생했습니다. 시스템 오류: [3] ([4])</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2756</td><td>1042</td><td>속성 '[2]'이(가) 하나 이상의 테이블에서 디렉터리 속성으로 사용되었지만 값이 할당되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2757</td><td>1042</td><td>변환 [2]에 대한 요약 정보를 만들 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2758</td><td>1042</td><td>변환 [2]에 MSI 버전이 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2759</td><td>1042</td><td>변환 [2] 버전 [3]이(가) 엔진과 호환되지 않습니다. 최소: [4], 최대: [5].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2760</td><td>1042</td><td>변환 [2]이(가) 패키지 [3]에 적합하지 않습니다. 업그레이드 코드 [4]이(가) 필요한데 [5]이(가) 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2761</td><td>1042</td><td>트랜잭션을 시작할 수 없습니다. 전역 뮤텍스가 제대로 초기화되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2762</td><td>1042</td><td>스크립트 레코드를 쓸 수 없습니다. 트랜잭션이 시작되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2763</td><td>1042</td><td>스크립트를 실행할 수 없습니다. 트랜잭션이 시작되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2765</td><td>1042</td><td>AssemblyName 테이블에 어셈블리 이름이 없습니다. 구성 요소: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2766</td><td>1042</td><td>파일 [2]은(는) 유효하지 않는 MSI 저장소 파일입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2767</td><td>1042</td><td>추가 데이터가없습니다{ [2] 열거 시}.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2768</td><td>1042</td><td>패키 패키지의 변환이 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2769</td><td>1042</td><td>사용자 지정 작업 [2]이(가) [3]개의 MSIHANDLE을 닫지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2770</td><td>1042</td><td>캐시된 폴더 [2]이(가) 내부 캐시 폴더 테이블에 정의되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2771</td><td>1042</td><td>기능 [2] 업그레이드에 구성 요소가 누락되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2772</td><td>1042</td><td>새로운 업그레디으 기능 [2]은(는) 리프 기능이어야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_28</td><td>1042</td><td>'[2]' 파일을 단독으로 액세스할 수 있는 권한을 다른 응용 프로그램에서 갖고 있습니다. 다른 응용 프로그램을 모두 닫은 후 "다시 시도"를 눌러 다시 시도하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2801</td><td>1042</td><td>알 수 없는 메시지 -- 유형 [2]. 아무 작업도 수행되지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2802</td><td>1042</td><td>이벤트 [2]에 대한 게시자를 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2803</td><td>1042</td><td>대화 상자 뷰가 대화 상자 [2]에 대한 레코드를 찾지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2804</td><td>1042</td><td>대화 상자 [2]에서 컨트롤 [3]의 활성화 시 CMsiDialog가 조건 [3]을(를) 평가하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2806</td><td>1042</td><td>대화 상자 [2]이(가) 조건 [3]을(를) 평가하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2807</td><td>1042</td><td>작업 [2]을(를) 인식할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2808</td><td>1042</td><td>대화 상자 [2]의 기본 단추가 잘못 정의되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2809</td><td>1042</td><td>대화 상자 [2]에서 다음 컨트롤 포인터가 순환을 형성하지 않습니다. [3]에서 [4](으)로의 포인터가 있지만 추가 포인터는 없습니다. </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2810</td><td>1042</td><td>대화 상자 [2]에서 다음 컨트롤 포인터가 순환을 형성하지 않습니다. [3]과(와) [5] 둘 다에서 [4](으)로의 포인터가 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2811</td><td>1042</td><td>대화 상자 [2]에서 컨트롤 [3]에 포커스를 두어야 하는데 가능하지 않습니다..</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2812</td><td>1042</td><td>이벤트 [2]을(를) 인식할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2813</td><td>1042</td><td>인수 [2]을(를) 사용하여 EndDialog 이벤트가 호출되었지만 해당 대화 상자에 부모 항목이 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2814</td><td>1042</td><td>대화 상자 [2]에서 컨트롤 [3]이(가) 존재하지 않는 컨트롤 [4]을(를) 다음 컨트롤로 지정합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2815</td><td>1042</td><td>ControlCondition 테이블에 대화 상자 [2]에 대한 조건 없는 행이 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2816</td><td>1042</td><td>EventMapping 테이블이 이벤트 [3]에 대해 대화 상자 [2]의 잘못된 컨트롤 [4]을(를) 참조합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2817</td><td>1042</td><td>이벤트 [2]이(가) 대화 상자 [3]의 컨트롤 [4]에 대한 특성을 설정하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2818</td><td>1042</td><td>ControlEvent 테이블의 EndDialog에 인식할 수 없는 인수 [2]이(가) 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2819</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에 속성이 연결되어야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2820</td><td>1042</td><td>이미 초기화된 처리기를 초기화하려고 했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2821</td><td>1042</td><td>이미 초기화된 대화 상자를 초기화하려고 했습니다. [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2822</td><td>1042</td><td>모든 컨트롤을 추가할 때까지 대화 상자 [2]에 대해 다른 메서드를 호출할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2823</td><td>1042</td><td>대화 상자 [2]에서 미리 초기화된 컨트롤 [3]을(를) 초기화하려고 했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2824</td><td>1042</td><td>대화 상자 특성 [3]에는 [2]개 이상의 필드 레코드가 필요합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2825</td><td>1042</td><td>컨트롤 특성 [3]에는 [2]개 이상의 필드 레코드가 필요합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2826</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]이(가) 대화 상자 [4]의 경계에서 [5]픽셀만큼 벗어났습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2827</td><td>1042</td><td>대화 상자 [2]의 라디오 단추 그룹 [3]에 포함된 단추 [4]이(가) 그룹 [5]의 경계에서 [6]픽셀만큼 벗어났습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2828</td><td>1042</td><td>대화 상자 [2]에서 컨트롤 [3]을(를) 제거하려고 했으나 해당 컨트롤이 대화 상자에 속하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2829</td><td>1042</td><td>초기화되지 않은 대화 상자를 사용하려고 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2830</td><td>1042</td><td>대화 상자 [2]에서 초기화되지 않은 컨트롤을 사용하려고 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2831</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]이(가) [5] 특성 [4]을(를) 지원하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2832</td><td>1042</td><td>대화 상자 [2]이(가) 특성 [3]을(를) 지원하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2833</td><td>1042</td><td>대화 상자 [3]의 컨트롤 [4]이(가) 메시지 [2]을(를) 무시했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2834</td><td>1042</td><td>대화 상자 [2]의 다음 포인터들이 단일 루프를 형성하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2835</td><td>1042</td><td>컨트롤 [2]이(가) 대화 상자 [3]에 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2836</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에 포커스를 둘 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2837</td><td>1042</td><td>대화 상자 [2]에 있는 컨트롤 [3]의 경우 winproc가 [4]을(를) 반환해야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2838</td><td>1042</td><td>선택 테이블의 항목 [2]에 대한 부모 항목이 자기 자신입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2839</td><td>1042</td><td>속성 [2]을(를) 설정하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2840</td><td>1042</td><td>대화 상자 이름 불일치 오류입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2841</td><td>1042</td><td>오류 대화 상자에 [확인] 단추가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2842</td><td>1042</td><td>오류 대화 상자에 텍스트 필드가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2843</td><td>1042</td><td>표준 대화 상자에는 ErrorString 특성을 사용할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2844</td><td>1042</td><td>Errorstring이 설정되지 않으면 오류 대화 상자를 실행할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2845</td><td>1042</td><td>단추의 총 폭이 오류 대화 상자의 크기를 초과합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2846</td><td>1042</td><td>SetFocus가 오류 대화 상자에서 필요한 컨트롤을 찾지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2847</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에 아이콘과 비트맵 스타일이 모두 설정되어 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2848</td><td>1042</td><td>대화 상자 [2]에서 기본 단추로 설정하려는 컨트롤 [3]이(가) 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2849</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]이(가) 정수 값을 지정할 수 없는 유형입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2850</td><td>1042</td><td>볼륨 유형을 인식할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2851</td><td>1042</td><td>아이콘 [2]의 데이터가 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2852</td><td>1042</td><td>대화 상자 [2]을(를) 사용하려면 먼저 한 개 이상의 컨트롤을 추가해야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2853</td><td>1042</td><td>대화 상자 [2]은(는) 모덜리스 대화 상자이므로 execut 메서드를 호출하면 안 됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2854</td><td>1042</td><td>대화 상자 [2]에 첫 번째 활성 컨트롤로 지정된 컨트롤 [3]이(가) 없습니다. </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2855</td><td>1042</td><td>대화 상자 [2]의 라디오 단추 그룹 [3]에 한 개 이하의 단추가 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2856</td><td>1042</td><td>대화 상자 [2]의 다른 복사본을 만들고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2857</td><td>1042</td><td>선택 테이블에 지정된 디렉터리 [2]을(를) 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2858</td><td>1042</td><td>비트맵 [2]의 데이터가 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2859</td><td>1042</td><td>테스트 오류 메시지.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2860</td><td>1042</td><td>대화 상자 [2]의 [취소] 단추가 잘못 정의되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2861</td><td>1042</td><td>대화 상자 [2] 컨트롤 [3]의 라디오 단추에 대한 다음 포인터들이 순환을 형성하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2862</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에 대한 특성이 유효한 아이콘 크기를 정의하지 않습니다. 크기를 16으로 설정합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2863</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에는 크기가 [5]x[5]인 아이콘 [4]이(가) 필요한데 이 크기를 사용할 수 없습니다. 사용 가능한 첫 번째 크기를 로드합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2864</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]이(가) 찾아보기 이벤트를 받았으나 현재 선택 항목에 대해 구성 가능한 디렉터리가 없습니다. 가능한 원인: 찾아보기 단추가 제대로 작성되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2865</td><td>1042</td><td>빌보드 [2]의 컨트롤 [3]이(가) 빌보드 [4]의 경계에서 [5]픽셀만큼 벗어났습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2866</td><td>1042</td><td>대화 상자 [2]은(는) 인수 [3]을(를) 반환할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2867</td><td>1042</td><td>오류 대화 상자 속성이 설정되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2868</td><td>1042</td><td>오류 대화 상자 [2]에 오류 스타일 비트가 설정되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2869</td><td>1042</td><td>대화 상자 [2]에 오류 스타일 비트가 설정되었으나 오류 대화 상자가 아닙니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2870</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에 대한 도움말 문자열 [4]에 구분 문자가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2871</td><td>1042</td><td>[2] 테이블이 최신이 아닙니다. [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2872</td><td>1042</td><td>대화 상자 [2]에 있는 CheckPath 컨트롤 이벤트의 인수가 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2873</td><td>1042</td><td>대화 상자 [3]의 컨트롤 [2]에 잘못된 문자열 길이 제한이 지정되었습니다: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2874</td><td>1042</td><td>텍스트 글꼴을 [2](으)로 변경하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2875</td><td>1042</td><td>텍스트 색을 [2](으)로 변경하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2876</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에서 문자열을 잘라내야 합니다: [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2877</td><td>1042</td><td>이진 데이터 [2]을(를) 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2878</td><td>1042</td><td>대화 상자 [2]에서 컨트롤 [3]에 다음 값이 지정된 것 같습니다: [4]. 이 값은 잘못되었거나 중복된 값입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2879</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에서는 마스크 문자열을 구문 분석할 수 없습니다. [4].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2880</td><td>1042</td><td>나머지 컨트롤 이벤트는 수행하지 마십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2881</td><td>1042</td><td>CMsiHandler를 초기화하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2882</td><td>1042</td><td>대화 상자 창 클래스를 등록하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2883</td><td>1042</td><td>대화 상자 [2]에 대해 CreateNewDialog를 실패했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2884</td><td>1042</td><td>대화 상자 [2]에서 창을 만들지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2885</td><td>1042</td><td>대화 상자 [2]에서 컨트롤 [3]을(를) 만들지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2886</td><td>1042</td><td>[2] 테이블을 만들지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2887</td><td>1042</td><td>[2] 테이블에 대한 커서를 만들지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2888</td><td>1042</td><td>[2] 뷰를 실행하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2889</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]에 대해 창을 만들지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2890</td><td>1042</td><td>처리기가 초기화된 대화 상자를 만들지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2891</td><td>1042</td><td>대화 상자 [2]에 대해 창을 소멸시키지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2892</td><td>1042</td><td>[2]은(는) 정수 전용 컨트롤인데 [3]이(가) 유효하지 않은 정수 값입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2893</td><td>1042</td><td>대화 상자 [2]의 컨트롤 [3]은(는) 최대 [5]문자인 속성 값을 허용할 수 있습니다. 값 [4]이(가) 이 제한을 초과하여 잘렸습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2894</td><td>1042</td><td>RICHED20.DLL을 로드하지 못했습니다. GetLastError()에서 다음을 반환했습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2895</td><td>1042</td><td>RICHED20.DLL을 해제하지 못했습니다. GetLastError()에서 다음을 반환했습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2896</td><td>1042</td><td>작업 [2]을(를) 실행하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2897</td><td>1042</td><td>이 시스템에서 어떠한 [2] 글꼴도 만들지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2898</td><td>1042</td><td>[2] 텍스트 스타일에 대해 시스템은 [4] 문자 집합에 '[3]' 글꼴을 만들었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2899</td><td>1042</td><td>[2] 텍스트 스타일을 만들지 못했습니다. GetLastError()에서 다음을 반환했습니다: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_29</td><td>1042</td><td>다음 파일을 설치할 수 있는 디스크 공간이 충분하지 않습니다: [2]. 디스크 공간을 늘린 후 "다시 시도"를 누르거나 "취소"를 눌러서 끝내십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2901</td><td>1042</td><td>작업 [2]에 대한 매개 변수가 잘못되었습니다: 매개 변수 [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2902</td><td>1042</td><td>작업 [2]의 호출이 시퀀스를 벗어났습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2903</td><td>1042</td><td>파일 [2]이(가) 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2904</td><td>1042</td><td>파일 [2]에 대해 BindImage를 수행할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2905</td><td>1042</td><td>스크립트 파일 [2]에서 레코드를 읽을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2906</td><td>1042</td><td>스크립트 파일 [2]에 헤더가 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2907</td><td>1042</td><td>안전한 보안 설명자를 만들 수 없습니다. 오류: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2908</td><td>1042</td><td>구성 요소 [2]을(를) 등록할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2909</td><td>1042</td><td>구성 요소 [2]의 등록을 취소할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2910</td><td>1042</td><td>사용자의 보안 ID를 확인할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2911</td><td>1042</td><td>폴더 [2]을(를) 제거할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2912</td><td>1042</td><td>다시 시작 시 파일 [2]을(를) 제거하도록 예약할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2919</td><td>1042</td><td>압축된 파일에 대해 캐비닛이 지정되지 않았습니다. [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2920</td><td>1042</td><td>파일 [2]에 대해 원본 디렉터리가 지정되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2924</td><td>1042</td><td>스크립트 [2] 버전은 지원되지 않습니다. 스크립트 버전: [3], 최소 버전: [4], 최대 버전: [5].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2927</td><td>1042</td><td>ShellFolder id [2]이(가) 잘못되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2928</td><td>1042</td><td>최대 원본 수를 초과했습니다. 원본 '[2]'을(를) 건너뜁니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2929</td><td>1042</td><td>게시 루트를 확인할 수 없습니다. 오류: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2932</td><td>1042</td><td>스크립트 데이터에서 파일 [2]을(를) 만들 수 없습니다. 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2933</td><td>1042</td><td>롤백 스크립트 [2]을(를) 초기화할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2934</td><td>1042</td><td>변환 [2]의 보안을 유지할 수 없습니다. 오류 [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2935</td><td>1042</td><td>변환 [2]의 보안을 해제할 수 없습니다. 오류 [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2936</td><td>1042</td><td>변환 [2]을(를) 찾을 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2937</td><td>1042</td><td>시스템 파일 보호 카탈로그를 설치할 수 없습니다. 카탈로그: [2], 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2938</td><td>1042</td><td>캐시에서 시스템 파일 보호 카탈로그를 검색할 수 없습니다. 카탈로그: [2], 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2939</td><td>1042</td><td>캐시에서 시스템 파일 보호 카탈로그를 삭제할 수 없습니다. 카탈로그: [2], 오류: [3].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2940</td><td>1042</td><td>원본 확인을 위한 디렉터리 관리자가 제공되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2941</td><td>1042</td><td>파일 [2]에 대한 CRC를 계산할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2942</td><td>1042</td><td>[2] 파일에 대해 BindImage 작업이 실행되지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2943</td><td>1042</td><td>이 버전의 Windows는 64비트 패키지의 배포를 지원하지 않습니다. [2]이(가) 64비트 패키지용 스크립트입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2944</td><td>1042</td><td>GetProductAssignmentType을 실패했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_2945</td><td>1042</td><td>오류 [3](으)로 인해 ComPlus 응용 프로그램 [2]을(를) 설치하지 못했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_3</td><td>1042</td><td>Info [1]. </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_30</td><td>1042</td><td>원본 파일이 없습니다: [2]. 파일이 실제로 있는지, 그리고 그 파일에 대한 액세스 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_3001</td><td>1042</td><td>이 목록의 패치에 잘못된 배열 정보가 있습니다: [2][3][4][5][6][7][8][9][10][11][12][13][14][15][16].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_3002</td><td>1042</td><td>패치 [2]에 잘못된 배열 정보가 있습니다. </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_31</td><td>1042</td><td>파일 읽기 오류: [3]. {{ 시스템 오류 [2].}}  파일이 실제로 있는지, 그리고 그 파일에 대한 액세스 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_32</td><td>1042</td><td>파일 쓰기 오류: [3]. {{ 시스템 오류 [2].}}  그 디렉터리에 대한 액세스 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_33</td><td>1042</td><td>원본 파일이 없습니다{{(cabinet)}}: [2]. 그 파일이 실제로 있는지, 그리고 그 파일에 대한 액세스 권한이 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_34</td><td>1042</td><td>'[2]' 디렉터리를 만들 수 없습니다. 이 이름을 가진 파일이 이미 있습니다. 그 파일 이름을 다른 것으로 바꾸거나 제거한 후 "다시 시도"를 누르거나 "취소"를 눌러 끝내십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_35</td><td>1042</td><td>볼륨 [2]을(를) 현재 사용할 수 없습니다. 다른 볼륨을 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_36</td><td>1042</td><td>지정한 경로 '[2]'을(를) 사용할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_37</td><td>1042</td><td>지정한 폴더에 쓸 수 없습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_38</td><td>1042</td><td>파일을 읽을 때 네트워크 오류가 생겼습니다: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_39</td><td>1042</td><td>디렉터리를 만들 때 오류가 생겼습니다: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_4</td><td>1042</td><td>내부 오류 [1]. [2]{, [3]}{, [4]}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_40</td><td>1042</td><td>디렉터리를 만들 때 네트워크 오류가 생겼습니다: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_41</td><td>1042</td><td>원본 파일 캐비넷을 열 때 네트워크 오류가 생겼습니다: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_42</td><td>1042</td><td>지정한 경로가 너무 깁니다: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_43</td><td>1042</td><td>이 파일을 수정할 수 있는 권한이 충분하지 않습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_44</td><td>1042</td><td>폴더 경로 '[2]'이(가) 잘못 되었습니다. 경로로 지정한 문자가 없거나 경로가 너무 깁니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_45</td><td>1042</td><td>폴더 경로 '[2]'에 폴더 경로로 사용할 수 없는 글자가 들어 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_46</td><td>1042</td><td>폴더 경로 '[2]'에 사용할 수 없는 글자가 들어 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_47</td><td>1042</td><td>'[2]'은(는) 파일 이름으로 유효하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_48</td><td>1042</td><td>파일 보안 오류: [3] GetLastError: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_49</td><td>1042</td><td>사용할 수 없는 드라이브: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_5</td><td>1042</td><td>{{디스크 꽉 참: }}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_50</td><td>1042</td><td>키를 만들 수 없습니다: [2]. {{ 시스템 오류 [3].}}  그 키에 대한 액세스 권한이 있는지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_51</td><td>1042</td><td>키를 열 수 없습니다: [2]. {{ 시스템 오류  [3].}}  그 키에 대한 액세스 권한이 있는지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_52</td><td>1042</td><td>키 [3]에서 값 [2]을(를) 삭제할 수 없습니다. {{ 시스템 오류 [4].}}  그 키에 대한 액세스 권한이 있는지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_53</td><td>1042</td><td>키 [2]을(를) 삭제할 수 없습니다. {{ 시스템 오류 [3].}} 그 키에 대한 액세스 권한이 있는지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_54</td><td>1042</td><td>키 [3]에서 값 [2]을(를) 읽을 수 없습니다. {{ 시스템 오류 [4].}}  그 키에 대한 액세스 권한이 있는지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_55</td><td>1042</td><td>키 [3]에 값 [2]을(를) 쓸 수 없습니다. {{ 시스템 오류 [4].}}  그 키에 대한 액세스 권한이 충분한지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_56</td><td>1042</td><td>키 [2]의 값 이름을 구할 수 없습니다. {{ 시스템 오류 [3].}}  그 키에 대한 액세스 권한이 있는지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_57</td><td>1042</td><td>키 [2]의 하위 키 이름을 구할 수 없습니다. {{ 시스템 오류 [3].}}  그 키에 대한 액세스 권한이 있는지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_58</td><td>1042</td><td>키 [2]의 보안 정보를 읽을 수 없습니다. {{ 시스템 오류 [3].}}  그 키에 대한 액세스 권한이 있는지 확인하거나, 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_59</td><td>1042</td><td>사용 가능한 레지스트리 공간을 늘릴 수 없습니다. 이 응용 프로그램을 설치하는데 [2]KB의 레지스트리 공간이 필요합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_6</td><td>1042</td><td>수행 [Time]: [1]. [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_60</td><td>1042</td><td>다른 설치 프로그램이 실행 중입니다. 그 프로그램의 설치를 끝낸 후, 이 작업을 계속하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_61</td><td>1042</td><td>보안 데이터에 액세스하는 중에 오류가 생겼습니다. Windows Installer를 올바르게 구성했는지 확인한 후 다시 설치하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_62</td><td>1042</td><td>사용자 '[2]'이(가) 제품 '[3]'의 설치를 이전에 초기화한 적이 있습니다. 그 사용자가 설치 프로그램을 다시 실행해야 그 제품을 사용할 수 있습니다. 지금 실행하는 설치 작업은 계속됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_63</td><td>1042</td><td>사용자 '[2]'이(가) 제품 '[3]'의 설치를 이전에 초기화한 적이 있습니다. 그 사용자가 설치 프로그램을 다시 실행해야 그 제품을 사용할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_64</td><td>1042</td><td>디스크 공간 부족 - 볼륨: '[2]'; 필요한 공간: [3]KB; 사용할 수 있는 공간: [4]KB. 디스크 공간을 늘린 후 다시 시도하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_65</td><td>1042</td><td>취소하시겠습니까?</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_66</td><td>1042</td><td>파일 [2][3]을(를) 다음 프로세스에서 사용하고 있습니다{이름: [4], Id: [5], 창 제목: '[6]'}.  그 응용 프로그램을 닫은 후 다시 시도하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_67</td><td>1042</td><td>제품 '[2]'이(가) 이미 설치되어 있어서 이 제품을 설치할 수 없습니다. 이 두 제품은 호환되지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_68</td><td>1042</td><td>디스크 공간 부족 - 볼륨: '[2]'; 필요한 공간: [3]KB; 사용할 수 있는 공간: [4]KB.  롤백 기능을 해제하면 디스크 공간을 충분히 사용할 수 있습니다. 끝내시려면 "취소"를 누르고, 사용할 수 있는 디스크 공간을 확인하려면 "다시 시도"를 누르고, 롤백없이 계속하려면 "무시"를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_69</td><td>1042</td><td>네트워크 위치 [2]에 액세스할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_7</td><td>1042</td><td>[ProductName]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_70</td><td>1042</td><td>다음 응용 프로그램을 닫아야 설치를 계속할 수 있습니다:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_71</td><td>1042</td><td>이 제품을 설치하려는 시스템에 해당 제품을 이전에 설치한 적이 없습니다. </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_72</td><td>1042</td><td>키 [2]이(가) 유효하지 않습니다. 정확한 키를 입력했는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_73</td><td>1042</td><td>시스템을 다시 시작한 후에 [2]의 구성을 설정해야 합니다. 지금 다시 시작하려면 "예"를 누르고, 나중에 수동으로 다시 시작하려면 "아니오"를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_74</td><td>1042</td><td>변경된 구성 설정을 [2]에 적용하려면 시스템을 다시 시작해야 합니다. 지금 다시 시작하려면 "예"를 누르고, 나중에 수동으로 다시 시작하려면 "아니오"를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_75</td><td>1042</td><td>[2]의 설치가 현재 중지된 상태입니다. 계속하려면 설치하면서 변경된 사항을 취소해야 합니다. 변경된 사항들을 취소하시겠습니까?</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_76</td><td>1042</td><td>이 제품의 설치 작업이 실행 중입니다. 계속하려면 전에 설치하면서 변경된 사항을 다시 되돌려야 합니다. 변경된 사항을 취소하시겠습니까?</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_77</td><td>1042</td><td>제품 [2]의 설치 패키지를 찾을 수 없습니다. 설치 패키지 '[3]'의 사본을 사용해서 설치를 다시 시도하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_78</td><td>1042</td><td>설치 작업을 성공적으로 완료했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_79</td><td>1042</td><td>설치를 실패했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_8</td><td>1042</td><td>{[2]}{, [3]}{, [4]}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_80</td><td>1042</td><td>제품: [2] - [3]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_81</td><td>1042</td><td>컴퓨터를 이전 상태로 되돌리거나, 나중에 다시 설치해야 합니다. 이전 상태로 되돌리시겠습니까?</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_82</td><td>1042</td><td>디스크에 설치 정보를 쓰는데 오류가 생겼습니다. 디스크 공간이 충분한지 확인하고 "다시 시도"를 누르거나, 설치를 끝내려면 "취소"를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_83</td><td>1042</td><td>사용자의 컴퓨터를 이전 상태로 되돌리는데 필요한 파일 중 일부를 찾을 수 없습니다. 복구할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_84</td><td>1042</td><td>경로 [2]이(가) 유효하지 않습니다. 정확한 경로를 지정하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_85</td><td>1042</td><td>메모리가 부족합니다. 다른 응용 프로그램을 종료한 후 다시 시도하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_86</td><td>1042</td><td>드라이브 [2]에 디스크가 없습니다. 디스크를 넣은 후 "다시 시도"를 누르거나, "취소"를 눌러서 이전에 선택한 볼륨으로 다시 돌아가십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_87</td><td>1042</td><td>드라이브 [2]에 디스크가 없습니다. 디스크를 넣은 후 "다시 시도"를 누르거나, "취소"를 눌러서 "찾아보기" 대화 상자로 이동한 후 다른 볼륨을 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_88</td><td>1042</td><td>폴더 [2]이(가) 존재하지 않습니다. 기존에 있던 폴더의 경로를 입력하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_89</td><td>1042</td><td>이 폴더에 대한 읽기 권한이 충분하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_9</td><td>1042</td><td>메시지 유형: [1], 인수: [2]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_90</td><td>1042</td><td>설치할 대상 폴더를 지정하지 않았습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_91</td><td>1042</td><td>다음 원본 설치 데이터베이스를 읽는 중에 오류가 생겼습니다: [2].</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_92</td><td>1042</td><td>리부팅 작업을 설정하고 있습니다: 파일 [2]의 이름을 [3](으)로 바꾸고 있습니다. 작업을 완료하려면 리부팅해야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_93</td><td>1042</td><td>리부팅 작업을 설정하고 있습니다: 파일 [2]을(를) 삭제하고 있습니다. 작업을 완료하려면 리부팅해야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_94</td><td>1042</td><td>모듈 [2]을(를) 등록하는데 실패했습니다. HRESULT [3]. 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_95</td><td>1042</td><td>모듈 [2]의 등록을 해제하는데 실패했습니다. HRESULT [3]. 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_96</td><td>1042</td><td>패키지 [2]을(를) 캐시하는데 실패했습니다. 오류: [3]. 소속된 부서의 기술 지원 담당자에게 문의하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_97</td><td>1042</td><td>[2] 글꼴을 등록하지 못했습니다. 글꼴을 설치할 수 있는 권한이 충분한지, 그리고 이 글꼴을 시스템에서 지원하는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_98</td><td>1042</td><td>[2] 글꼴의 등록을 해제하지 못했습니다. 글꼴을 제거할 수 있는 권한이 충분히 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ERROR_99</td><td>1042</td><td>바로 가기 [2]을(를) 만들지 못했습니다. 대상 폴더가 실제로 있는지, 그리고 그 폴더에 액세스할 수 있는지 확인하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_INSTALLDIR</td><td>1042</td><td>{&amp;Tahoma8}[INSTALLDIR]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_INSTALLSHIELD</td><td>1042</td><td>InstallShield</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_INSTALLSHIELD_FORMATTED</td><td>1042</td><td>{&amp;MSSWhiteSerif8}InstallShield</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ISSCRIPT_VERSION_MISSING</td><td>1042</td><td>InstallScript 엔진이 현재 컴퓨터에서 누락되었습니다. 가능한 경우, ISScript.msi를 실행하거나 지원 담당자에게 문의하여 도움을 받으십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_ISSCRIPT_VERSION_OLD</td><td>1042</td><td>현재 컴퓨터의 InstallScript 엔진은 이 설치에 필요한 버전보다 오래된 것입니다. 가능한 경우, 최신 버전의 ISScript.msi를 실행하거나 지원 담당자에게 문의하여 도움을 받으십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_NEXT</td><td>1042</td><td>다음(&amp;N) &gt;</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_OK</td><td>1042</td><td>{&amp;Tahoma8} 확인</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_BROWSE</td><td>1042</td><td>[ProductName]의 [SETUPEXENAME] 원본 열기</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_INVALID</td><td>1042</td><td>이 실행 파일은 [ProductName]의 원본 실행 파일이 아닙니다. [SETUPEXENAME] 원본을 사용하지 않고 추가 종속 항목을 설치하면 [ProductName]이(가) 제대로 작동하지 않을 수 있습니다. [SETUPEXENAME] 원본을 찾으시겠습니까?</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_SEARCH</td><td>1042</td><td>이 설치를 위해 추가 종속 항목이 필요할 수 있습니다. 그러한 종속 항목이 없으면 [ProductName]이(가) 제대로 작동하지 않을 수 있습니다. [SETUPEXENAME] 원본을 찾으시겠습니까?</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PREVENT_DOWNGRADE_EXIT</td><td>1042</td><td>이 컴퓨터에 이 응용 프로그램의 새 버전이 이미 설치되어 있습니다. 이 버전을 설치하려면 먼저 새 버전을 제거하십시오. 마법사를 종료하려면 확인을 클릭하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PRINT_BUTTON</td><td>1042</td><td>인쇄(&amp;P)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PRODUCTNAME_INSTALLSHIELD</td><td>1042</td><td>[ProductName] - InstallShield Wizard</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEAPPPOOL</td><td>1042</td><td>응용 프로그램 풀 %s 만들기</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEAPPPOOLS</td><td>1042</td><td>응용 프로그램 풀 만드는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEVROOT</td><td>1042</td><td>IIS 가상 디렉터리 %s(을)를 만들고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEVROOTS</td><td>1042</td><td>IIS 가상 디렉터리를 만드는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSION</td><td>1042</td><td>웹 서비스 확장 만들기</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS</td><td>1042</td><td>웹 서비스 확장 만드는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSITE</td><td>1042</td><td>IIS 웹사이트 %s을(를) 만드는 중</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSITES</td><td>1042</td><td>IIS 웹사이트를 만드는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_EXTRACT</td><td>1042</td><td>IIS 가상 디렉터리 정보를 추출하고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_EXTRACTDONE</td><td>1042</td><td>IIS 가상 디렉터리 정보를 추출했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEAPPPOOL</td><td>1042</td><td>응용 프로그램 풀 제거</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEAPPPOOLS</td><td>1042</td><td>응용 프로그램 풀 제거 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVESITE</td><td>1042</td><td>포트 %d에서 웹 사이트를 제거하고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEVROOT</td><td>1042</td><td>IIS 가상 디렉터리 %s(을)를 제거하고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEVROOTS</td><td>1042</td><td>IIS 가상 디렉터리를 제거하는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION</td><td>1042</td><td>웹 서비스 확장 제거</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS</td><td>1042</td><td>웹 서비스 확장 제거 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSITES</td><td>1042</td><td>IIS 웹사이트를 제거하는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKAPPPOOLS</td><td>1042</td><td>응용 프로그램 풀 롤백 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKVROOTS</td><td>1042</td><td>가상 디렉터리 및 웹 사이트 변경 내용을 롤백하고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS</td><td>1042</td><td>웹 서비스 확장 롤백 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_TEXTFILECHANGS_REPLACE</td><td>1042</td><td>%s을(를) %s(으)로 바꾸는 중(%s에서)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_XML_COSTING</td><td>1042</td><td>XML 파일의 크기를 계산하는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_XML_CREATE_FILE</td><td>1042</td><td>XML 파일 %s(을)를 만드는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_XML_FILES</td><td>1042</td><td>XML 파일 변경 수행 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_XML_REMOVE_FILE</td><td>1042</td><td>XML 파일 %s(을)를 제거하고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_XML_ROLLBACK_FILES</td><td>1042</td><td>XML 파일 변경 내용을 롤백하는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_PROGMSG_XML_UPDATE_FILE</td><td>1042</td><td>XML 파일 %s(을)를 업데이트하는 중...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SETUPEXE_EXPIRE_MSG</td><td>1042</td><td>이 설치 프로그램은 %s까지 작동합니다. 설치 프로그램을 종료합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SETUPEXE_LAUNCH_COND_E</td><td>1042</td><td>이 설치 프로그램은 InstallShield의 평가판으로 만들어졌으며 setup.exe에서만 시작할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME1</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME10</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.vshost.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME11</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME12</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApiClient.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME13</td><td>1042</td><td>LAUNCH~1.EXE|Launch ZetaResourceEditor-Setup.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME14</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME15</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.vshost.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME16</td><td>1042</td><td>LAUNCH~1.EXE|Launch ZetaResourceEditor-Setup.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME17</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME18</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApiClient.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME19</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2RS.EXE</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME2</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.vshost.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME20</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApiClient.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME21</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2RS.EXE</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME22</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.vshost.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME23</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME24</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME25</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApiClient.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME26</td><td>1042</td><td>LAUNCH~1.EXE|Launch ConfigS.EXE</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME27</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME28</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.vshost.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME29</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2RS.EXE</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME3</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME30</td><td>1042</td><td>LAUNCH~1.EXE|Launch IEMS.EXE</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME31</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME32</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApiClient.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME33</td><td>1033</td><td/><td>0</td><td/><td>1235604843</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME33</td><td>1042</td><td>LAUNCH~1.EXE|Launch IEMS.EXE</td><td>0</td><td/><td>1235604843</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME34</td><td>1033</td><td/><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME34</td><td>1042</td><td>LAUNCH~1.EXE|Launch ConfigS.EXE</td><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME35</td><td>1033</td><td/><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME35</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.exe</td><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME36</td><td>1033</td><td/><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME36</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.vshost.exe</td><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME37</td><td>1033</td><td/><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME37</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2RS.EXE</td><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME38</td><td>1033</td><td/><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME38</td><td>1042</td><td>LAUNCH~1.EXE|Launch IEMS.EXE</td><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME39</td><td>1033</td><td/><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME39</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME4</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApiClient.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME40</td><td>1033</td><td/><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME40</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.vshost.exe</td><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME41</td><td>1033</td><td/><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME41</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApiClient.exe</td><td>0</td><td/><td>1369790924</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME42</td><td>1033</td><td/><td>0</td><td/><td>-878347350</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME42</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>-878347350</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME43</td><td>1033</td><td/><td>0</td><td/><td>-878347350</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME43</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.vshost.exe</td><td>0</td><td/><td>-878347350</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME5</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME6</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.vshost.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME7</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME8</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApiClient.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME9</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLBROWSE_INTRO</td><td>1042</td><td>아래 서버 목록에서 대상 데이터베이스 서버를 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLBROWSE_INTRO_DB</td><td>1042</td><td>아래 카탈로그 이름 목록에서 지정하려는 데이터베이스 카탈로그를 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLBROWSE_INTRO_TEMPLATE</td><td>1042</td><td>[IS_SQLBROWSE_INTRO]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_BROWSE</td><td>1042</td><td>검색(&amp;R)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_BROWSE_DB</td><td>1042</td><td>검색(&amp;O)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_CATALOG</td><td>1042</td><td>데이터베이스 카탈로그 이름(&amp;N):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_CONNECT</td><td>1042</td><td>연결 방식:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_DESC</td><td>1042</td><td>데이터베이스 서버 및 인증 방법을 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_ID</td><td>1042</td><td>로그인 ID(&amp;L):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_INTRO</td><td>1042</td><td>아래 목록에서 설치할 데이터베이스 서버를 선택하거나 [찾아보기]를 클릭하여 모든 데이터베이스 서버의 목록을 살펴보십시오. 또한 현재 자격 증명 또는 SQL 로그인 ID 및 암호를 사용하여 로그인 인증 방식을 지정할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_PSWD</td><td>1042</td><td>비밀번호(&amp;P):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_SERVER</td><td>1042</td><td>데이터베이스 서버(&amp;S):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_SERVER2</td><td>1042</td><td>설치하려는 데이터 서버(&amp;D):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_SQL</td><td>1042</td><td>다음 로그인 ID 및 암호를 사용하여 서버 인증(&amp;Q)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_TITLE</td><td>1042</td><td>{MSSansBold8}데이터베이스 서버(&amp;M)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLLOGIN_WIN</td><td>1042</td><td>현재 사용자의 Windows 인증 자격 증명(&amp;W)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLSCRIPT_INSTALLING</td><td>1042</td><td>기존의 SQL 설치 스크립트...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SQLSCRIPT_UNINSTALLING</td><td>1042</td><td>SQL 제거 스크립트 실행...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_STANDARD_USE_SETUPEXE</td><td>1042</td><td>MSI 패키지를 직접 실행하여 설치할 수 없습니다. setup.exe를 실행해야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_Advertise</td><td>1042</td><td>{&amp;Tahoma8} 처음 사용 시 구성요소가 설치됩니다 (해당 구성요소가 이 옵션을 지원하는 경우에만 적용됨).</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_AllInstalledLocal</td><td>1042</td><td>{&amp;Tahoma8} 로컬 하드 드라이브에 전체 구성요소가 설치됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_CustomSetup</td><td>1042</td><td>{&amp;MSSansBold8} 사용자 정의 설치 참고</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_CustomSetupDescription</td><td>1042</td><td>{&amp;Tahoma8} 사용자 정의 설치는 프로그램 구성요소를 선택하여 설치할 수 있도록 해줍니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_IconInstallState</td><td>1042</td><td>{&amp;Tahoma8} 구성요소 이름 옆의 아이콘은 해당 구성요소의 설치 상태를 나타냅니다. 아이콘을 눌러 각 구성요소의 설치 상태 메뉴를 드롭다운하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_InstallState</td><td>1042</td><td>{&amp;Tahoma8} 설치 상태는 각각 다음을 의미합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_Network</td><td>1042</td><td>{&amp;Tahoma8} 네트워크로부터 구성요소를 실행하도록 설치합니다 (해당 구성요소가 이 옵션을 지원하는 경우에만 적용됨).</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_OK</td><td>1042</td><td>{&amp;Tahoma8} 확인</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_SubFeaturesInstalledLocal</td><td>1042</td><td>{&amp;Tahoma8} 로컬 하드 드라이브에 일부 하위 구성요소가 설치됩니다. (해당 구성요소에 하위 구성요소가 있는 경우에만 적용됨).</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_SetupTips_WillNotBeInstalled</td><td>1042</td><td>{&amp;Tahoma8} 구성요소가 설치되지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_Available</td><td>1042</td><td> 사용 가능</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_Bytes</td><td>1042</td><td> 바이트</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_CompilingFeaturesCost</td><td>1042</td><td> 이 구성요소의 비용을 수집하고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_Differences</td><td>1042</td><td> 차이</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_DiskSize</td><td>1042</td><td> 디스크 크기</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureCompletelyRemoved</td><td>1042</td><td> 이 구성요소를 완전히 제거합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureContinueNetwork</td><td>1042</td><td> 이 구성요소를 계속 네트워크에서 실행하도록 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureFreeSpace</td><td>1042</td><td> 이 구성요소는 하드 드라이브에 [1]의 여유 공간을 확보합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledCD</td><td>1042</td><td> 이 구성요소 및 모든 하위 구성요소를 CD로부터 실행하도록 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledCD2</td><td>1042</td><td> 이 구성요소를 CD로부터 실행하도록 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledLocal</td><td>1042</td><td> 이 구성요소 및 모든 하위 구성요소를 로컬 하드 드라이브에 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledLocal2</td><td>1042</td><td> 이 구성요소를 로컬 하드 드라이브에 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledNetwork</td><td>1042</td><td> 이 구성요소 및 모든 하위 구성요소를 네트워크로부터 실행하도록 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledNetwork2</td><td>1042</td><td> 이 구성요소를 네트워크로부터 실행하도록 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledRequired</td><td>1042</td><td> 요구시 이 구성요소를 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledWhenRequired</td><td>1042</td><td> 요구시 이 구성요소를 설치하도록 설정합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledWhenRequired2</td><td>1042</td><td> 요구시 이 구성요소를 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureLocal</td><td>1042</td><td> 이 구성요소를 로컬 하드 드라이브에 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureLocal2</td><td>1042</td><td> 이 구성요소를 사용자의 로컬 하드 드라이브에 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureNetwork</td><td>1042</td><td> 이 구성요소를 네트워크로부터 실행하도록 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureNetwork2</td><td>1042</td><td> 이 구성요소를 네트워크에서 실행할 수 있도록 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureNotAvailable</td><td>1042</td><td> 이 구성요소를 사용할 수 없게 됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureOnCD</td><td>1042</td><td> 이 구성요소를 CD로부터 실행하도록 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureOnCD2</td><td>1042</td><td> 이 구성요소를 CD로부터 실행할 수 있도록 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureRemainLocal</td><td>1042</td><td> 이 구성요소를 사용자의 로컬 하드 드라이브에 남겨둡니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureRemoveNetwork</td><td>1042</td><td> 이 구성요소를 사용자의 로컬 하드 드라이브에서 제거하지만 네트워크로부터 여전히 실행할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureRemovedCD</td><td>1042</td><td> 이 구성요소를 사용자의 로컬 하드 드라이브에서 제거하지만 CD에서는 여전히 실행할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureRemovedUnlessRequired</td><td>1042</td><td> 이 구성요소를 사용자의 로컬 하드 드라이브에서 제거하지만 요구시 설치되도록 설정합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureRequiredSpace</td><td>1042</td><td> 이 구성요소는 하드 드라이브에 [1]를 필요로 합니다.  </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureRunFromCD</td><td>1042</td><td> 이 구성요소를 계속 CD로부터 실행하도록 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree</td><td>1042</td><td> 이 구성요소는 하드 드라이브에 [1]의 여유 공간을 확보합니다. [3] 중 [2]의 하위 구성요소가 선택되었습니다. 이 하위 구성요소는 하드 드라이브에 [4]의 여유 공간을 확보합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree2</td><td>1042</td><td> 이 구성요소는 하드 드라이브에 [1]의 여유 공간을 확보합니다. [3] 중 [2]의 하위 구성요소가 선택되었습니다. 이 하위 구성요소는 하드 드라이브에 [4]를 필요로 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree3</td><td>1042</td><td> 이 구성요소는 하드 드라이브의 [1]를 필요로 합니다. [3] 중 [2]의 하위 구성요소가 선택되었습니다. 이 하위 구성요소는 하드 드라이브에 [4]의 여유 공간을 확보합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree4</td><td>1042</td><td> 이 구성요소는 하드 드라이브의 [1]를 필요로 합니다. [3] 중 [2]의 하위 구성요소가 선택되었습니다. 이 하위 구성요소는 하드 드라이브에 [4]를 필요로 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureUnavailable</td><td>1042</td><td> 이 구성요소를 설치 제거합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureUninstallNoNetwork</td><td>1042</td><td> 이 구성요소는 완전히 설치 제거되며 네트워크로부터 이 구성요소를 실행할 수 없게 됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureWasCD</td><td>1042</td><td> 이 구성요소는 CD로부터 실행되었지만 요구시 설치되도록 설정합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureWasCDLocal</td><td>1042</td><td> 이 구성요소는 CD로부터 실행되었지만 로컬 하드 드라이브에 설치됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureWasOnNetworkInstalled</td><td>1042</td><td> 이 구성요소는 네트워크로부터 실행되었지만 요구시 설치됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureWasOnNetworkLocal</td><td>1042</td><td> 이 구성요소는 네트워크로부터 실행되었지만 로컬 하드 드라이브에 설치됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_FeatureWillBeUninstalled</td><td>1042</td><td> 이 구성요소는 완전히 설치 제거되며 CD로부터 이 구성요소를 실행할 수 없게 됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_Folder</td><td>1042</td><td> 폴더|새 폴더</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_GB</td><td>1042</td><td> GB</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_KB</td><td>1042</td><td> KB</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_MB</td><td>1042</td><td> MB</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_Required</td><td>1042</td><td> 필수</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_TimeRemaining</td><td>1042</td><td> 남은 시간: {[1]분 }{[2]초}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS_UITEXT_Volume</td><td>1042</td><td> 볼륨</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__AgreeToLicense_0</td><td>1042</td><td>{&amp;Tahoma8} 사용권 계약서의 조건에 동의하지 않음(&amp;D)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__AgreeToLicense_1</td><td>1042</td><td>{&amp;Tahoma8} 사용권 계약서의 조건에 동의함(&amp;A)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DatabaseFolder_ChangeFolder</td><td>1042</td><td>이 폴더에 설치하려면 [다음]을 클릭하고, 또는 다른 폴더로 설치하려면 [바꾸기]를 클릭하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DatabaseFolder_DatabaseDir</td><td>1042</td><td>[DATABASEDIR]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DatabaseFolder_DatabaseFolder</td><td>1042</td><td>{&amp;MSSansBold8}데이터베이스 폴더</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DestinationFolder_Change</td><td>1042</td><td>{&amp;Tahoma8} 변경(&amp;C)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DestinationFolder_ChangeFolder</td><td>1042</td><td>{&amp;Tahoma8} 이 폴더에 설치하려면 "다음" 단추를 누르고, 다른 위치에 설치하려면 "변경" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DestinationFolder_DestinationFolder</td><td>1042</td><td>{&amp;MSSansBold8} 대상 폴더</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DestinationFolder_InstallTo</td><td>1042</td><td>{&amp;Tahoma8} [ProductName] 설치 위치:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DisplayName_Custom</td><td>1042</td><td>사용자 정의</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DisplayName_Minimal</td><td>1042</td><td>최소</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__DisplayName_Typical</td><td>1042</td><td>일반</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_11</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_4</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_8</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_BrowseDestination</td><td>1042</td><td>{&amp;Tahoma8} 대상 폴더를 찾습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_ChangeDestination</td><td>1042</td><td>{&amp;MSSansBold8} 현재 대상 폴더 변경</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_CreateFolder</td><td>1042</td><td> 새 폴더 작성||</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_FolderName</td><td>1042</td><td>{&amp;Tahoma8} 폴더 이름(&amp;F):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_LookIn</td><td>1042</td><td>{&amp;Tahoma8} 찾는 위치(&amp;L)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallBrowse_UpOneLevel</td><td>1042</td><td> 상위 폴더|</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallPointWelcome_ServerImage</td><td>1042</td><td>{&amp;Tahoma8} InstallShield(R) 설치 마법사는 지정된 네트워크 위치에 [ProductName]의 서버 이미지를 작성합니다. 계속하려면 "다음" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallPointWelcome_Wizard</td><td>1042</td><td>{&amp;TahomaBold10} [ProductName] - InstallShield Wizard 마법사를 시작합니다</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallPoint_Change</td><td>1042</td><td>{&amp;Tahoma8} 변경(&amp;C)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallPoint_EnterNetworkLocation</td><td>1042</td><td>{&amp;Tahoma8} 네트워크 위치를 입력하거나 "변경" 단추를 눌러 위치를 찾으십시오. 네트워크 위치를 입력하거나 "변경" 단추를 눌러 위치를 찾으십시오. 지정한 네트워크 위치에 [ProductName]의 서버 이미지를 작성하려면 "설치" 단추를 누르고, 설치 마법사를 종료하려면 "취소" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallPoint_Install</td><td>1042</td><td>{&amp;Tahoma8} 설치(&amp;I)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallPoint_NetworkLocation</td><td>1042</td><td>{&amp;Tahoma8} 네트워크 위치(&amp;N):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallPoint_NetworkLocationFormatted</td><td>1042</td><td>{&amp;MSSansBold8} 네트워크 위치</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsAdminInstallPoint_SpecifyNetworkLocation</td><td>1042</td><td>{&amp;Tahoma8} 제품의 서버 이미지를 위한 네트워크 위치를 지정합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseButton</td><td>1042</td><td>검색(&amp;R)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_11</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_4</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_8</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_BrowseDestFolder</td><td>1042</td><td>{&amp;Tahoma8} 대상 폴더를 찾습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_ChangeCurrentFolder</td><td>1042</td><td>{&amp;MSSansBold8} 현재 대상 폴더 변경</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_CreateFolder</td><td>1042</td><td> 새 폴더 작성||</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_FolderName</td><td>1042</td><td>{&amp;Tahoma8} 폴더 이름(&amp;F):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_LookIn</td><td>1042</td><td>{&amp;Tahoma8} 찾는 위치(&amp;L)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_OK</td><td>1042</td><td>{&amp;Tahoma8} 확인</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseFolderDlg_UpOneLevel</td><td>1042</td><td> 상위 폴더||</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseForAccount</td><td>1042</td><td>사용자 계정 찾아보기</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseGroup</td><td>1042</td><td>사용자 그룹 선택</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsBrowseUsernameTitle</td><td>1042</td><td>사용자 이름 선택</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCancelDlg_ConfirmCancel</td><td>1042</td><td>{&amp;Tahoma8} [ProductName] - InstallShield Wizard를 취소하시겠습니까?</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCancelDlg_No</td><td>1042</td><td>{&amp;Tahoma8} 아니오(&amp;N)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCancelDlg_Yes</td><td>1042</td><td>{&amp;Tahoma8} 예(&amp;Y)  </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsConfirmPassword</td><td>1042</td><td>암호 확인(&amp;F):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCreateNewUserTitle</td><td>1042</td><td>새 사용자 정보</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCreateUserBrowse</td><td>1042</td><td>새 사용자 정보(&amp;E)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Change</td><td>1042</td><td>{&amp;Tahoma8} 변경(&amp;C)...</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_ClickFeatureIcon</td><td>1042</td><td>{&amp;Tahoma8} 구성요소의 설치 방법을 변경하려면 아래 목록에 있는 아이콘을 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_CustomSetup</td><td>1042</td><td>{&amp;MSSansBold8} 사용자 정의 설치</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeatureDescription</td><td>1042</td><td>{&amp;Tahoma8} 구성요소 설명</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeaturePath</td><td>1042</td><td>{&amp;Tahoma8} &lt;selected feature path&gt;  </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeatureSize</td><td>1042</td><td>{&amp;Tahoma8} 구성요소 크기</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Help</td><td>1042</td><td>{&amp;Tahoma8} 도움말(&amp;H)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_InstallTo</td><td>1042</td><td>{&amp;Tahoma8} 설치 위치:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_MultilineDescription</td><td>1042</td><td>{&amp;Tahoma8} 현재 선택한 항목을 여러 행으로 설명합니다</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_SelectFeatures</td><td>1042</td><td>{&amp;Tahoma8} 설치하려는 프로그램 구성요소를 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Space</td><td>1042</td><td>{&amp;Tahoma8} 공간(&amp;S)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsDiskSpaceDlg_DiskSpace</td><td>1042</td><td>{&amp;Tahoma8} 설치에 필요한 디스크 공간이 사용 가능한 디스크 공간을 초과합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsDiskSpaceDlg_HighlightedVolumes</td><td>1042</td><td>{&amp;Tahoma8} 강조 표시된 드라이브는 현재 선택한 구성요소 설치에 필요한 디스크 공간이 부족합니다. 강조 표시된 드라이브에서 파일을 제거하거나, 구성요소를 적게 선택하여 설치하거나, 다른 대상 드라이브를 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsDiskSpaceDlg_Numbers</td><td>1042</td><td>{&amp;Tahoma8}{120}{70}{70}{70}{70}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsDiskSpaceDlg_OK</td><td>1042</td><td>{&amp;Tahoma8} 확인(&amp;O)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsDiskSpaceDlg_OutOfDiskSpace</td><td>1042</td><td>{&amp;MSSansBold8} 디스크 공간 부족</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsDomainOrServer</td><td>1042</td><td>도메인 및 서버(&amp;D):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsErrorDlg_Abort</td><td>1042</td><td>{&amp;Tahoma8} 중단(&amp;A)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsErrorDlg_ErrorText</td><td>1042</td><td>{&amp;Tahoma8} &lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsErrorDlg_Ignore</td><td>1042</td><td>{&amp;Tahoma8} 무시(&amp;I)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsErrorDlg_InstallerInfo</td><td>1042</td><td>[ProductName] - InstallShield Wizard 정보</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsErrorDlg_NO</td><td>1042</td><td>{&amp;Tahoma8} 아니오(&amp;N)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsErrorDlg_OK</td><td>1042</td><td>{&amp;Tahoma8} 확인(&amp;O)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsErrorDlg_Retry</td><td>1042</td><td>{&amp;Tahoma8} 재시도(&amp;R)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsErrorDlg_Yes</td><td>1042</td><td>{&amp;Tahoma8} 예(&amp;Y)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_Finish</td><td>1042</td><td>{&amp;Tahoma8} 마침(&amp;F)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_InstallSuccess</td><td>1042</td><td>{&amp;Tahoma8} InstallShield Wizard가 [ProductName]을(를) 성공적으로 설치했습니다. 마법사를 종료하려면 "마침" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_LaunchProgram</td><td>1042</td><td>프로그램을 실행합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_ShowReadMe</td><td>1042</td><td>readme 파일을 표시합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_UninstallSuccess</td><td>1042</td><td>{&amp;Tahoma8} InstallShield Wizard[ProductName]을(를) 성공적으로 제거했습니다. 마법사를 종료하려면 "마침" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_Update_InternetConnection</td><td>1042</td><td>인터넷 접속은 최신 갱신본이 있는지 확인하는데 사용될 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_Update_PossibleUpdates</td><td>1042</td><td>일부 프로그램 파일은 [ProductName] 사본을 구입한 후 갱신되었을 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_Update_SetupFinished</td><td>1042</td><td>설치 프로그램은 [ProductName] 설치를 완료하였습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_Update_YesCheckForUpdates</td><td>1042</td><td>예, 설치가 완료된 후 프로그램 갱신 내용을 확인합니다(권장사항)(&amp;Y).</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsExitDialog_WizardCompleted</td><td>1042</td><td>{&amp;TahomaBold10} InstallShield 완료</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFatalError_ClickFinish</td><td>1042</td><td>{&amp;Tahoma8} 마법사를 종료하려면 "마침" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFatalError_Finish</td><td>1042</td><td>{&amp;Tahoma8} 마침(&amp;F)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFatalError_KeepOrRestore</td><td>1042</td><td>{&amp;Tahoma8} 시스템에 설치된 기존 요소를 보관하여 다음에 설치를 계속하거나, 설치 이전의 원래 상태로 시스템을 복원할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFatalError_NotModified</td><td>1042</td><td>{&amp;Tahoma8} 시스템이 변경되지 않았습니다. 다음에 설치를 완료하려면 설치 프로그램을 다시 실행하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFatalError_RestoreOrContinueLater</td><td>1042</td><td>{&amp;Tahoma8} 마법사를 종료하려면 "복원" 또는 "다음에 계속" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFatalError_WizardCompleted</td><td>1042</td><td>{&amp;TahomaBold10}[ProductName] - InstallShield Wizard 마법사를 완료했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFatalError_WizardInterrupted</td><td>1042</td><td>{&amp;Tahoma8} [ProductName] - InstallShield Wizard가 완료되기 전에 설치 마법사가 중단되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_DiskSpaceRequirements</td><td>1042</td><td>{&amp;MSSansBold8} 디스크 공간 요구 사항</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_Numbers</td><td>1042</td><td>{&amp;Tahoma8}{120}{70}{70}{70}{70}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_OK</td><td>1042</td><td>{&amp;Tahoma8} 확인</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_SpaceRequired</td><td>1042</td><td>{&amp;Tahoma8} 선택한 구성요소를 설치하기 위한 디스크 공간.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_VolumesTooSmall</td><td>1042</td><td>{&amp;Tahoma8} 강조 표시된 드라이브는 현재 선택한 구성요소 설치에 필요한 디스크 공간이 부족합니다. 강조 표시된 드라이브에서 파일을 제거하거나, 구성요소를 적게 선택하여 설치하거나, 다른 대상 드라이브를 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFilesInUse_ApplicationsUsingFiles</td><td>1042</td><td>{&amp;Tahoma8} 이 프로그램 설치시 업데이트해야 할 파일을 다음의 응용 프로그램에서 사용하고 있습니다. 계속하려면 해당 응용 프로그램을 닫은 후 "재시도" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFilesInUse_Exit</td><td>1042</td><td>{&amp;Tahoma8} 종료(&amp;E)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFilesInUse_FilesInUse</td><td>1042</td><td>{&amp;MSSansBold8} 사용 중인 파일</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFilesInUse_FilesInUseMessage</td><td>1042</td><td>{&amp;Tahoma8} 업데이트해야 할 일부 파일이 현재 사용 중입니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFilesInUse_Ignore</td><td>1042</td><td>{&amp;Tahoma8} 무시(&amp;I)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsFilesInUse_Retry</td><td>1042</td><td>{&amp;Tahoma8} 재시도(&amp;R)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsGroup</td><td>1042</td><td>사용자 그룹(&amp;U):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsGroupLabel</td><td>1042</td><td>사용자 그룹(&amp;O):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsInitDlg_1</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsInitDlg_2</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsInitDlg_PreparingWizard</td><td>1042</td><td>{&amp;Tahoma8} InstallShield Wizard가 설치 과정으로 귀하를 안내할 준비를 하는 동안 잠시 기다려 주십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsInitDlg_WelcomeWizard</td><td>1042</td><td>{&amp;TahomaBold10} [ProductName]InstallShield Wizard를 시작합니다</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsLicenseDlg_LicenseAgreement</td><td>1042</td><td>{&amp;MSSansBold8} 사용권 계약서</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsLicenseDlg_ReadLicenseAgreement</td><td>1042</td><td>{&amp;Tahoma8} 다음의 사용권 계약서를 자세히 읽으십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsLogonInfoDescription</td><td>1042</td><td>이 응용 프로그램에서 사용할 사용자 계정을 지정하십시오. 사용자 계정은 도메인\사용자 이름 형식이어야 합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsLogonInfoTitle</td><td>1042</td><td>{&amp;MSSansBold8}로그온 정보</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsLogonInfoTitleDescription</td><td>1042</td><td>사용자 이름 및 암호를 지정하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsLogonNewUserDescription</td><td>1042</td><td>아래 버튼을 선택하여 설치하는 동안 만들 새 사용자에 대한 정보를 지정하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceDlg_ChangeFeatures</td><td>1042</td><td>{&amp;Tahoma8} 설치할 프로그램 구성요소를 변경합니다. 이 옵션을 선택하면 구성요소 설치 방법을 변경할 수 있는 사용자 선택 대화 상자가 나타납니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceDlg_MaitenanceOptions</td><td>1042</td><td>{&amp;Tahoma8} 프로그램을 변경, 복구 또는 제거합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceDlg_Modify</td><td>1042</td><td>{&amp;MSSansBold8} 변경(&amp;M)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceDlg_ProgramMaintenance</td><td>1042</td><td>{&amp;MSSansBold8} 프로그램 유지 보수</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceDlg_Remove</td><td>1042</td><td>{&amp;MSSansBold8} 제거(&amp;R)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceDlg_RemoveProductName</td><td>1042</td><td>{&amp;Tahoma8} 컴퓨터에서 [ProductName]을(를) 제거합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceDlg_Repair</td><td>1042</td><td>{&amp;MSSansBold8} 복구(&amp;P)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceDlg_RepairMessage</td><td>1042</td><td>{&amp;Tahoma8} 프로그램의 오류를 복구합니다. 이 옵션을 선택하면 누락되거나 손상된 파일, 바로 가기 및 레지스트리 항목을 수정합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceWelcome_MaintenanceOptionsDescription</td><td>1042</td><td>{&amp;Tahoma8} InstallShield(R) Wizard가 [ProductName]의 변경, 복구 또는 제거를 도와줍니다. 계속하려면 "다음" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMaintenanceWelcome_WizardWelcome</td><td>1042</td><td>{&amp;TahomaBold10}[ProductName] - InstallShield Wizard를 시작합니다</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_ApplicationsUsingFiles</td><td>1042</td><td>다음 응용 프로그램에서 이 설치 프로그램으로 업데이트할 파일을 사용하고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_CloseRestart</td><td>1042</td><td>자동 종료 후 응용 프로그램을 다시 시작합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_RebootAfter</td><td>1042</td><td>응용 프로그램이 종료되지 않습니다. (다시 부팅해야 합니다.)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsPatchDlg_PatchClickUpdate</td><td>1042</td><td>InstallShield(R) Wizard는 사용자의 컴퓨터에 [ProductName]를 위한 패치를 설치합니다.계속하려면 업데이트를 클릭하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsPatchDlg_PatchWizard</td><td>1042</td><td>[ProductName] - InstallShield Wizard</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsPatchDlg_Update</td><td>1042</td><td>업데이트(&amp;U) &gt;</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsPatchDlg_WelcomePatchWizard</td><td>1042</td><td>{&amp;TahomaBold10}[ProductName]의 패치에 오신 것을 환영합니다</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_2</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_Hidden</td><td>1042</td><td>{&amp;Tahoma8} (Hidden for now)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_HiddenTimeRemaining</td><td>1042</td><td>{&amp;Tahoma8} (Hidden for now) 예상 남은 시간:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_InstallingProductName</td><td>1042</td><td>{&amp;MSSansBold8} [ProductName]을(를) 설치합니다.  </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_ProgressDone</td><td>1042</td><td> 진행률</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_SecHidden</td><td>1042</td><td>{&amp;Tahoma8} (Hidden for now)초</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_Status</td><td>1042</td><td>{&amp;Tahoma8} 상태:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_Uninstalling</td><td>1042</td><td>{&amp;MSSansBold8} [ProductName]을(를) 삭제합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_UninstallingFeatures</td><td>1042</td><td>{&amp;Tahoma8} 선택한 프로그램 구성요소가 제거됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_UninstallingFeatures2</td><td>1042</td><td>{&amp;Tahoma8} 선택한 프로그램 구성요소가 설치됩니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_WaitUninstall</td><td>1042</td><td>{&amp;Tahoma8} InstallShield Wizard가 [ProductName]을(를) 제거하는 동안 잠시 기다려 주십시오. 이 작업은 몇 분 정도 걸립니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsProgressDlg_WaitUninstall2</td><td>1042</td><td>{&amp;Tahoma8} InstallShield Wizard가 [ProductName]을(를) 설치하는 동안 잠시 기다려 주십시오. 이 작업은 몇 분 정도 걸립니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsReadmeDlg_Cancel</td><td>1042</td><td>취소(&amp;C)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsReadmeDlg_PleaseReadInfo</td><td>1042</td><td>다음 readme 정보를 자세히 읽으십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsReadmeDlg_ReadMeInfo</td><td>1042</td><td>{&amp;MSSansBold8}Readme 정보</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_16</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_Anyone</td><td>1042</td><td>{&amp;Tahoma8} 컴퓨터를 사용하는 모든 사용자(&amp;A)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_CustomerInformation</td><td>1042</td><td>{&amp;MSSansBold8} 사용자 정보</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_InstallFor</td><td>1042</td><td>{&amp;Tahoma8} 다음 사용자용으로 프로그램을 설치합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_OnlyMe</td><td>1042</td><td>{&amp;Tahoma8} 사용자([USERNAME]) 전용(&amp;M)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_Organization</td><td>1042</td><td>{&amp;Tahoma8} 조직(&amp;O):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_PleaseEnterInfo</td><td>1042</td><td>{&amp;Tahoma8} 사용자 정보를 입력하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_SerialNumber</td><td>1042</td><td>일련번호(&amp;S)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_Tahoma50</td><td>1042</td><td>{50}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_Tahoma80</td><td>1042</td><td>{80}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsRegisterUserDlg_UserName</td><td>1042</td><td>{&amp;Tahoma8} 사용자 이름(&amp;U):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsResumeDlg_ResumeSuspended</td><td>1042</td><td>{&amp;Tahoma8} InstallShield(R) Wizard가 귀하의 컴퓨터에 중단된 [ProductName] - InstallShield Wizard를 완료합니다. 계속하려면 "다음" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsResumeDlg_Resuming</td><td>1042</td><td>{&amp;TahomaBold10}[ProductName] - InstallShield Wizard를 다시 시작합니다</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsResumeDlg_WizardResume</td><td>1042</td><td>{&amp;Tahoma8} InstallShield(R) Wizard가 귀하의 컴퓨터에 [ProductName]을(를) 설치했습니다. 계속하려면 "다음" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSelectDomainOrServer</td><td>1042</td><td>도메인 또는 서버 선택</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSelectDomainUserInstructions</td><td>1042</td><td>아래 버튼을 선택하여 설치하는 동안 만들 새 사용자에 대한 정보를 지정하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupComplete_ShowMsiLog</td><td>1042</td><td>Windows Installer 로그 표시</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_13</td><td>1042</td><td>{&amp;Tahoma8}</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_AllFeatures</td><td>1042</td><td>{&amp;Tahoma8} 모든 프로그램 구성요소를 설치합니다. (디스크 공간을 가장 많이 필요로 함).</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_ChooseFeatures</td><td>1042</td><td>{&amp;Tahoma8} 설치하려는 프로그램 구성요소 및 설치할 위치를 선택하십시오. 고급 사용자일 경우에 권장합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_ChooseSetupType</td><td>1042</td><td>{&amp;Tahoma8} 사용자의 필요에 가장 적합한 설치 유형을 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Complete</td><td>1042</td><td>{&amp;MSSansBold8} 전체 설치(&amp;C)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Custom</td><td>1042</td><td>{&amp;MSSansBold8} 사용자 정의 설치(&amp;S)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Minimal</td><td>1042</td><td>{&amp;MSSansBold8}최소(&amp;M)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_MinimumFeatures</td><td>1042</td><td>{&amp;Tahoma8}최소 필수 기능들이 설치될 것입니다</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_SelectSetupType</td><td>1042</td><td>{&amp;Tahoma8} 설치 유형을 선택하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_SetupType</td><td>1042</td><td>{&amp;MSSansBold8} 설치 유형</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Typical</td><td>1042</td><td>{&amp;MSSansBold8}일반(&amp;T)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsUserExit_ClickFinish</td><td>1042</td><td>{&amp;Tahoma8} 마법사를 종료하려면 "마침" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsUserExit_Finish</td><td>1042</td><td>{&amp;Tahoma8} 마침(&amp;F)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsUserExit_KeepOrRestore</td><td>1042</td><td>{&amp;Tahoma8} 시스템에 설치된 기존 요소를 보관하여 다음에 설치를 계속하거나, 설치 이전의 원래 상태로 시스템을 복원할 수 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsUserExit_NotModified</td><td>1042</td><td>{&amp;Tahoma8} 시스템이 변경되지 않았습니다. 나중에 이 프로그램을 설치하려면 설치 프로그램을 다시 실행하십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsUserExit_RestoreOrContinue</td><td>1042</td><td>{&amp;Tahoma8} 마법사를 종료하려면 "복원" 또는 "다음에 계속" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsUserExit_WizardCompleted</td><td>1042</td><td>{&amp;TahomaBold10} InstallShield Wizard를 완료했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsUserExit_WizardInterrupted</td><td>1042</td><td>{&amp;Tahoma8} [ProductName] - InstallShield Wizard가 완료되기 전에 설치 마법사가 중단되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsUserNameLabel</td><td>1042</td><td>사용자 이름(&amp;U):</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_BackOrCancel</td><td>1042</td><td>{&amp;Tahoma8} 설치 설정 사항을 검토하거나 변경하려면 "뒤로" 단추를 누르십시오. 마법사를 종료하려면 "취소" 단추를 누르십시오.  </td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ClickInstall</td><td>1042</td><td>{&amp;Tahoma8} 설치를 시작하려면 "설치" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Company</td><td>1042</td><td>회사: [COMPANYNAME]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_CurrentSettings</td><td>1042</td><td>현재 설정:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_DestFolder</td><td>1042</td><td>설치 폴더:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Install</td><td>1042</td><td>{&amp;Tahoma8} 설치(&amp;I)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Installdir</td><td>1042</td><td>[INSTALLDIR]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ModifyReady</td><td>1042</td><td>{&amp;MSSansBold8} 프로그램 변경 준비 완료</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ReadyInstall</td><td>1042</td><td>{&amp;MSSansBold8} 프로그램 설치 준비 완료</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ReadyRepair</td><td>1042</td><td>{&amp;MSSansBold8} 프로그램 복구 준비 완료</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_SelectedSetupType</td><td>1042</td><td>[SelectedSetupType]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Serial</td><td>1042</td><td>일련번호: [ISX_SERIALNUM]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_SetupType</td><td>1042</td><td>설치 종류:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_UserInfo</td><td>1042</td><td>사용자 정보:</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_UserName</td><td>1042</td><td>성명: [USERNAME]</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyReadyDlg_WizardReady</td><td>1042</td><td>{&amp;Tahoma8} 마법사는 설치를 시작할 준비가 되었습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ChoseRemoveProgram</td><td>1042</td><td>{&amp;Tahoma8} 사용자 시스템에서 프로그램을 제거하도록 선택했습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ClickBack</td><td>1042</td><td>{&amp;Tahoma8} 설정 사항을 검토하거나 변경하려면 "뒤로" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ClickRemove</td><td>1042</td><td>{&amp;Tahoma8} 컴퓨터에서 '[ProductName]'을(를) 제거하려면 "제거" 단추를 누르십시오. 제거 후에는 더 이상 이 프로그램을 사용할 수 없습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_Remove</td><td>1042</td><td>{&amp;Tahoma8} 제거(&amp;R)</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_RemoveProgram</td><td>1042</td><td>{&amp;MSSansBold8} 프로그램 제거</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsWelcomeDlg_InstallProductName</td><td>1042</td><td>{&amp;Tahoma8} InstallShield(R) Wizard가 귀하의 컴퓨터에 [ProductName]을(를) 설치합니다. 계속하려면 "다음" 단추를 누르십시오.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsWelcomeDlg_WarningCopyright</td><td>1042</td><td>경고: 이 프로그램은 저작권법과 국제 협약에 의해 보호되고 있습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__IsWelcomeDlg_WelcomeProductName</td><td>1042</td><td>{&amp;TahomaBold10}[ProductName] - InstallShield Wizard를 시작합니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__TargetReq_DESC_COLOR</td><td>1042</td><td>시스템의 색상 설정은 [ProductName] 을(를) 가동하기 위하여 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__TargetReq_DESC_OS</td><td>1042</td><td>운영 체제는 [ProductName] 을(를) 가동하기 위하여 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__TargetReq_DESC_PROCESSOR</td><td>1042</td><td>프로세서는 [ProductName] 을(를) 가동하기 위하여 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__TargetReq_DESC_RAM</td><td>1042</td><td>RAM 양은 [ProductName] 을(를) 가동하기 위하여 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>IDS__TargetReq_DESC_RESOLUTION</td><td>1042</td><td>화면 해상도는 [ProductName] 을(를) 가동하기 위하여 적합하지 않습니다.</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>ID_STRING1</td><td>1042</td><td>http://www.회사명.com</td><td>0</td><td/><td>1000684363</td></row>
		<row><td>ID_STRING2</td><td>1042</td><td>회사명</td><td>0</td><td/><td>1000684363</td></row>
		<row><td>ID_STRING3</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>ID_STRING4</td><td>1042</td><td>LAUNCH~1.EXE|Launch I2MS2.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>ID_STRING5</td><td>1042</td><td>LAUNCH~1.EXE|Launch WebApi.exe</td><td>0</td><td/><td>1235571499</td></row>
		<row><td>ID_STRING6</td><td>1033</td><td>=-NewEnvironment1</td><td>0</td><td/><td>1000717291</td></row>
		<row><td>ID_STRING6</td><td>1042</td><td>=-Path</td><td>0</td><td/><td>1000678667</td></row>
		<row><td>ID_STRING7</td><td>1033</td><td>[~];[ProgramFilesFolder]simplewin</td><td>0</td><td/><td>1000701195</td></row>
		<row><td>ID_STRING7</td><td>1042</td><td>[~];[ProgramFilesFolder]simplewinIEMS\bin</td><td>0</td><td/><td>1000676651</td></row>
		<row><td>ID_STRING8</td><td>1033</td><td>=-NewEnvironment2</td><td>0</td><td/><td>1000686891</td></row>
		<row><td>ID_STRING8</td><td>1042</td><td>=-SIMPLEVIEW_ROOT</td><td>0</td><td/><td>1000701227</td></row>
		<row><td>ID_STRING9</td><td>1033</td><td>[~];[ProgramFilesFolder]simplewinIEMS</td><td>0</td><td/><td>1000715563</td></row>
		<row><td>ID_STRING9</td><td>1042</td><td>[~];[ProgramFilesFolder]simplewinIEMS</td><td>0</td><td/><td>1000715563</td></row>
		<row><td>IIDS_UITEXT_FeatureUninstalled</td><td>1042</td><td> 이 구성요소는 설치 안된 상태로 유지됩니다.</td><td>0</td><td/><td>1235571499</td></row>
	</table>

	<table name="ISSwidtagProperty">
		<col key="yes" def="s72">Name</col>
		<col def="s0">Value</col>
		<row><td>UniqueId</td><td>DBE1B4AE-B3E2-4297-B0FC-16A325E861F5</td></row>
	</table>

	<table name="ISTargetImage">
		<col key="yes" def="s13">UpgradedImage_</col>
		<col key="yes" def="s13">Name</col>
		<col def="s0">MsiPath</col>
		<col def="i2">Order</col>
		<col def="I4">Flags</col>
		<col def="i2">IgnoreMissingFiles</col>
	</table>

	<table name="ISUpgradeMsiItem">
		<col key="yes" def="s72">UpgradeItem</col>
		<col def="s0">ObjectSetupPath</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="i2">ISAttributes</col>
	</table>

	<table name="ISUpgradedImage">
		<col key="yes" def="s13">Name</col>
		<col def="s0">MsiPath</col>
		<col def="s8">Family</col>
	</table>

	<table name="ISVirtualDirectory">
		<col key="yes" def="s72">Directory_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualPackage">
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualRegistry">
		<col key="yes" def="s72">Registry_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualRelease">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s255">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualShortcut">
		<col key="yes" def="s72">Shortcut_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISXmlElement">
		<col key="yes" def="s72">ISXmlElement</col>
		<col def="s72">ISXmlFile_</col>
		<col def="S72">ISXmlElement_Parent</col>
		<col def="L0">XPath</col>
		<col def="L0">Content</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISXmlElementAttrib">
		<col key="yes" def="s72">ISXmlElementAttrib</col>
		<col key="yes" def="s72">ISXmlElement_</col>
		<col def="L255">Name</col>
		<col def="L0">Value</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISXmlFile">
		<col key="yes" def="s72">ISXmlFile</col>
		<col def="l255">FileName</col>
		<col def="s72">Component_</col>
		<col def="s72">Directory</col>
		<col def="I4">ISAttributes</col>
		<col def="S0">SelectionNamespaces</col>
		<col def="S255">Encoding</col>
	</table>

	<table name="ISXmlLocator">
		<col key="yes" def="s72">Signature_</col>
		<col key="yes" def="S72">Parent</col>
		<col def="S255">Element</col>
		<col def="S255">Attribute</col>
		<col def="I2">ISAttributes</col>
	</table>

	<table name="Icon">
		<col key="yes" def="s72">Name</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I2">ISIconIndex</col>
		<row><td>ARPPRODUCTICON.exe</td><td/><td>C:\SimpleWinData\Images\Icon_msi.ico</td><td>0</td></row>
		<row><td>I2MS2.exe1_953550AF96DC4FBF927051DE69354985.exe</td><td/><td>C:\SimpleWinData\Images\logo.ico</td><td>0</td></row>
		<row><td>I2MS2.exe1_B6DF1EA3ADE142FA8C3ECAAF4CE6FA73.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>I2MS2.exe2_CE0E349D6C444304AE12282201F7DE0E.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2.exe</td><td>0</td></row>
		<row><td>I2MS2.exe_3F85E4279261405CA95B3F0C5362C287.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>I2MS2.exe_4BC6563ECD934CDE9E81BF7D009A931B.exe</td><td/><td>C:\SimpleWinData\Images\logo.ico</td><td>0</td></row>
		<row><td>I2MS2.exe_754F8D467BEA4B8B8387B15799CD7496.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2.exe</td><td>0</td></row>
		<row><td>I2MS2.vshost.exe1_CFA8CE4B7F7E4123923E616FF0556359.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>I2MS2.vshost.exe_0386A0E8F77540EDBA57912EDA47FADF.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>I2MS2.vshost.exe_3A99D837867F454295830BE517B7990E.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>I2MS2.vshost.exe_3BF1098C154D4DEEA67B4D30F086BF08.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>I2MS2RS.EXE_A70298B6A68643B5ACDBC6B0BA15569B.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2RS.EXE</td><td>0</td></row>
		<row><td>I2MS2RS.EXE_FE9290B8CDCB4DE69F56F610B8A7A938.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\I2MS2RS.EXE</td><td>0</td></row>
		<row><td>IEMS.EXE1_E9B64271D59F4F899817368ECCFFAEB1.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\IEMS.EXE</td><td>0</td></row>
		<row><td>IEMS.EXE_A5F5863877CF42FF8FEB46EDB80A6B12.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\IEMS.EXE</td><td>0</td></row>
		<row><td>IEMS.EXE_D0FEA74673FB47AAB4EAC5A068AA299B.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\IEMS.EXE</td><td>0</td></row>
		<row><td>WebApi.exe1_8B1F21AF667C4B98BE43E7E41951F136.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApi.exe_2966594750D946D385C432D8BB6062DC.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApi.exe_45C3DE69E14E48B6B84A9AB31282CE93.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApi.exe_6689B91246FA45308E149BECA793217E.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApi.exe_6D6E416B5A9C4E4BB349783A7CB91B3C.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApi.exe_A761677C3C2E4D20B0EFEF8E16D05268.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApi.vshost.exe_85DA5322084C45008FCF225677ABC0D3.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApiClient.exe1_75A68BC93EBE4EC0A230274D04564FA2.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApiClient.exe_4792D5155F7F42AE8DC30D80DBC1A078.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApiClient.exe_7BAE560BC6F54E65B7BD843D5EC62499.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApiClient.exe_8CFACF8A140441A099C36AE14200C369.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>WebApiClient.exe_EF703B0A367D4C3193DCE76085ED3369.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>ZetaResourceEditor_764C4E42BA4A4D158CC97FF321955EA4.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Properties\ZetaResourceEditor-Setup.exe</td><td>0</td></row>
		<row><td>ZetaResourceEditor_EADD008B75F0414697FE88E6ADA06A69.exe</td><td/><td>C:\Users\jake\Source\Workspaces\I2MS2\I2MS2\I2MS2\bin\Debug\Properties\ZetaResourceEditor-Setup.exe</td><td>0</td></row>
	</table>

	<table name="IniFile">
		<col key="yes" def="s72">IniFile</col>
		<col def="l255">FileName</col>
		<col def="S72">DirProperty</col>
		<col def="l255">Section</col>
		<col def="l128">Key</col>
		<col def="s255">Value</col>
		<col def="i2">Action</col>
		<col def="s72">Component_</col>
	</table>

	<table name="IniLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="s255">FileName</col>
		<col def="s96">Section</col>
		<col def="s128">Key</col>
		<col def="I2">Field</col>
		<col def="I2">Type</col>
	</table>

	<table name="InstallExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AllocateRegistrySpace</td><td>NOT Installed</td><td>1550</td><td>AllocateRegistrySpace</td><td/></row>
		<row><td>AppSearch</td><td/><td>400</td><td>AppSearch</td><td/></row>
		<row><td>BindImage</td><td/><td>4300</td><td>BindImage</td><td/></row>
		<row><td>CCPSearch</td><td>CCP_TEST</td><td>500</td><td>CCPSearch</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>CreateFolders</td><td/><td>3700</td><td>CreateFolders</td><td/></row>
		<row><td>CreateShortcuts</td><td/><td>4500</td><td>CreateShortcuts</td><td/></row>
		<row><td>DeleteServices</td><td>VersionNT</td><td>2000</td><td>DeleteServices</td><td/></row>
		<row><td>DuplicateFiles</td><td/><td>4210</td><td>DuplicateFiles</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>FindRelatedProducts</td><td>NOT ISSETUPDRIVEN</td><td>420</td><td>FindRelatedProducts</td><td/></row>
		<row><td>ISPreventDowngrade</td><td>ISFOUNDNEWERPRODUCTVERSION</td><td>450</td><td>ISPreventDowngrade</td><td/></row>
		<row><td>ISRunSetupTypeAddLocalEvent</td><td>Not Installed And Not ISRUNSETUPTYPEADDLOCALEVENT</td><td>1050</td><td>ISRunSetupTypeAddLocalEvent</td><td/></row>
		<row><td>ISSelfRegisterCosting</td><td/><td>2201</td><td/><td/></row>
		<row><td>ISSelfRegisterFiles</td><td/><td>5601</td><td/><td/></row>
		<row><td>ISSelfRegisterFinalize</td><td/><td>6601</td><td/><td/></row>
		<row><td>ISUnSelfRegisterFiles</td><td/><td>2202</td><td/><td/></row>
		<row><td>InstallFiles</td><td/><td>4000</td><td>InstallFiles</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1501</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallODBC</td><td/><td>5400</td><td>InstallODBC</td><td/></row>
		<row><td>InstallServices</td><td>VersionNT</td><td>5800</td><td>InstallServices</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>IsolateComponents</td><td/><td>950</td><td>IsolateComponents</td><td/></row>
		<row><td>LaunchConditions</td><td>Not Installed</td><td>410</td><td>LaunchConditions</td><td/></row>
		<row><td>MigrateFeatureStates</td><td/><td>1010</td><td>MigrateFeatureStates</td><td/></row>
		<row><td>MoveFiles</td><td/><td>3800</td><td>MoveFiles</td><td/></row>
		<row><td>MsiConfigureServices</td><td>VersionMsi &gt;= "5.00"</td><td>5850</td><td>MSI5 MsiConfigureServices</td><td/></row>
		<row><td>MsiPublishAssemblies</td><td/><td>6250</td><td>MsiPublishAssemblies</td><td/></row>
		<row><td>MsiUnpublishAssemblies</td><td/><td>1750</td><td>MsiUnpublishAssemblies</td><td/></row>
		<row><td>PatchFiles</td><td/><td>4090</td><td>PatchFiles</td><td/></row>
		<row><td>ProcessComponents</td><td/><td>1600</td><td>ProcessComponents</td><td/></row>
		<row><td>PublishComponents</td><td/><td>6200</td><td>PublishComponents</td><td/></row>
		<row><td>PublishFeatures</td><td/><td>6300</td><td>PublishFeatures</td><td/></row>
		<row><td>PublishProduct</td><td/><td>6400</td><td>PublishProduct</td><td/></row>
		<row><td>RMCCPSearch</td><td>Not CCP_SUCCESS And CCP_TEST</td><td>600</td><td>RMCCPSearch</td><td/></row>
		<row><td>RegisterClassInfo</td><td/><td>4600</td><td>RegisterClassInfo</td><td/></row>
		<row><td>RegisterComPlus</td><td/><td>5700</td><td>RegisterComPlus</td><td/></row>
		<row><td>RegisterExtensionInfo</td><td/><td>4700</td><td>RegisterExtensionInfo</td><td/></row>
		<row><td>RegisterFonts</td><td/><td>5300</td><td>RegisterFonts</td><td/></row>
		<row><td>RegisterMIMEInfo</td><td/><td>4900</td><td>RegisterMIMEInfo</td><td/></row>
		<row><td>RegisterProduct</td><td/><td>6100</td><td>RegisterProduct</td><td/></row>
		<row><td>RegisterProgIdInfo</td><td/><td>4800</td><td>RegisterProgIdInfo</td><td/></row>
		<row><td>RegisterTypeLibraries</td><td/><td>5500</td><td>RegisterTypeLibraries</td><td/></row>
		<row><td>RegisterUser</td><td/><td>6000</td><td>RegisterUser</td><td/></row>
		<row><td>RemoveDuplicateFiles</td><td/><td>3400</td><td>RemoveDuplicateFiles</td><td/></row>
		<row><td>RemoveEnvironmentStrings</td><td/><td>3300</td><td>RemoveEnvironmentStrings</td><td/></row>
		<row><td>RemoveExistingProducts</td><td/><td>1410</td><td>RemoveExistingProducts</td><td/></row>
		<row><td>RemoveFiles</td><td/><td>3500</td><td>RemoveFiles</td><td/></row>
		<row><td>RemoveFolders</td><td/><td>3600</td><td>RemoveFolders</td><td/></row>
		<row><td>RemoveIniValues</td><td/><td>3100</td><td>RemoveIniValues</td><td/></row>
		<row><td>RemoveODBC</td><td/><td>2400</td><td>RemoveODBC</td><td/></row>
		<row><td>RemoveRegistryValues</td><td/><td>2600</td><td>RemoveRegistryValues</td><td/></row>
		<row><td>RemoveShortcuts</td><td/><td>3200</td><td>RemoveShortcuts</td><td/></row>
		<row><td>ResolveSource</td><td>Not Installed</td><td>850</td><td>ResolveSource</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>6410</td><td>ScheduleReboot</td><td/></row>
		<row><td>SelfRegModules</td><td/><td>5600</td><td>SelfRegModules</td><td/></row>
		<row><td>SelfUnregModules</td><td/><td>2200</td><td>SelfUnregModules</td><td/></row>
		<row><td>SetARPINSTALLLOCATION</td><td/><td>1100</td><td>SetARPINSTALLLOCATION</td><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>VersionNT = 400</td><td>970</td><td/><td/></row>
		<row><td>SetODBCFolders</td><td/><td>1200</td><td>SetODBCFolders</td><td/></row>
		<row><td>StartServices</td><td>VersionNT</td><td>5900</td><td>StartServices</td><td/></row>
		<row><td>StopServices</td><td>VersionNT</td><td>1900</td><td>StopServices</td><td/></row>
		<row><td>UnpublishComponents</td><td/><td>1700</td><td>UnpublishComponents</td><td/></row>
		<row><td>UnpublishFeatures</td><td/><td>1800</td><td>UnpublishFeatures</td><td/></row>
		<row><td>UnregisterClassInfo</td><td/><td>2700</td><td>UnregisterClassInfo</td><td/></row>
		<row><td>UnregisterComPlus</td><td/><td>2100</td><td>UnregisterComPlus</td><td/></row>
		<row><td>UnregisterExtensionInfo</td><td/><td>2800</td><td>UnregisterExtensionInfo</td><td/></row>
		<row><td>UnregisterFonts</td><td/><td>2500</td><td>UnregisterFonts</td><td/></row>
		<row><td>UnregisterMIMEInfo</td><td/><td>3000</td><td>UnregisterMIMEInfo</td><td/></row>
		<row><td>UnregisterProgIdInfo</td><td/><td>2900</td><td>UnregisterProgIdInfo</td><td/></row>
		<row><td>UnregisterTypeLibraries</td><td/><td>2300</td><td>UnregisterTypeLibraries</td><td/></row>
		<row><td>ValidateProductID</td><td/><td>700</td><td>ValidateProductID</td><td/></row>
		<row><td>WriteEnvironmentStrings</td><td/><td>5200</td><td>WriteEnvironmentStrings</td><td/></row>
		<row><td>WriteIniValues</td><td/><td>5100</td><td>WriteIniValues</td><td/></row>
		<row><td>WriteRegistryValues</td><td/><td>5000</td><td>WriteRegistryValues</td><td/></row>
		<row><td>setAllUsersProfile2K</td><td>VersionNT &gt;= 500</td><td>980</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>VersionNT</td><td>960</td><td/><td/></row>
	</table>

	<table name="InstallShield">
		<col key="yes" def="s72">Property</col>
		<col def="S0">Value</col>
		<row><td>ActiveLanguage</td><td>1042</td></row>
		<row><td>Comments</td><td/></row>
		<row><td>CurrentMedia</td><td dt:dt="bin.base64" md5="de9f554a3bc05c12be9c31b998217995">
UwBpAG4AZwBsAGUASQBtAGEAZwBlAAEARQB4AHAAcgBlAHMAcwA=
			</td></row>
		<row><td>DefaultProductConfiguration</td><td>Express</td></row>
		<row><td>EnableSwidtag</td><td>0</td></row>
		<row><td>ISCompilerOption_CompileBeforeBuild</td><td>1</td></row>
		<row><td>ISCompilerOption_Debug</td><td>0</td></row>
		<row><td>ISCompilerOption_IncludePath</td><td/></row>
		<row><td>ISCompilerOption_LibraryPath</td><td/></row>
		<row><td>ISCompilerOption_MaxErrors</td><td>50</td></row>
		<row><td>ISCompilerOption_MaxWarnings</td><td>50</td></row>
		<row><td>ISCompilerOption_OutputPath</td><td>&lt;ISProjectDataFolder&gt;\Script Files</td></row>
		<row><td>ISCompilerOption_PreProcessor</td><td>_ISSCRIPT_NEW_STYLE_DLG_DEFS</td></row>
		<row><td>ISCompilerOption_WarningLevel</td><td>3</td></row>
		<row><td>ISCompilerOption_WarningsAsErrors</td><td>1</td></row>
		<row><td>ISTheme</td><td>InstallShield Blue.theme</td></row>
		<row><td>ISUSLock</td><td>{6E6C314F-F391-4B68-9A18-C020FE54176A}</td></row>
		<row><td>ISUSSignature</td><td>{E530B400-D35E-4E1E-B79E-52718C175101}</td></row>
		<row><td>ISVisitedViews</td><td>viewAssistant,viewAppFiles,viewAppV,viewObjects,viewShortcuts,viewSetupTypes,viewUI,viewISToday,viewProject,viewRealSetupDesign,viewSetupDesign,viewUpgradePaths,viewUpdateService,viewFeatureFiles,viewFileExtensions,viewRegistry,viewSystemSearch,viewEnvironmentVariables,viewVRoots,viewIniFiles,viewTextFiles,viewXMLConfig,viewCustomActions</td></row>
		<row><td>Limited</td><td>1</td></row>
		<row><td>LockPermissionMode</td><td>1</td></row>
		<row><td>MsiExecCmdLineOptions</td><td/></row>
		<row><td>MsiLogFile</td><td/></row>
		<row><td>OnUpgrade</td><td>0</td></row>
		<row><td>Owner</td><td/></row>
		<row><td>PatchFamily</td><td>MyPatchFamily1</td></row>
		<row><td>PatchSequence</td><td>1.0.0</td></row>
		<row><td>SaveAsSchema</td><td/></row>
		<row><td>SccEnabled</td><td>0</td></row>
		<row><td>SccPath</td><td>SAK</td></row>
		<row><td>SchemaVersion</td><td>774</td></row>
		<row><td>Type</td><td>MSIE</td></row>
		<row><td>VSSccAuxPath</td><td>SAK</td></row>
		<row><td>VSSccLocalPath</td><td>SAK</td></row>
		<row><td>VSSccProvider</td><td>SAK</td></row>
	</table>

	<table name="InstallUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AppSearch</td><td/><td>400</td><td>AppSearch</td><td/></row>
		<row><td>CCPSearch</td><td>CCP_TEST</td><td>500</td><td>CCPSearch</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>ExecuteAction</td><td/><td>1300</td><td>ExecuteAction</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>FindRelatedProducts</td><td/><td>430</td><td>FindRelatedProducts</td><td/></row>
		<row><td>ISPreventDowngrade</td><td>ISFOUNDNEWERPRODUCTVERSION</td><td>450</td><td>ISPreventDowngrade</td><td/></row>
		<row><td>InstallWelcome</td><td>Not Installed</td><td>1210</td><td>InstallWelcome</td><td/></row>
		<row><td>IsolateComponents</td><td/><td>950</td><td>IsolateComponents</td><td/></row>
		<row><td>LaunchConditions</td><td>Not Installed</td><td>410</td><td>LaunchConditions</td><td/></row>
		<row><td>MaintenanceWelcome</td><td>Installed And Not RESUME And Not Preselected And Not PATCH</td><td>1230</td><td>MaintenanceWelcome</td><td/></row>
		<row><td>MigrateFeatureStates</td><td/><td>1200</td><td>MigrateFeatureStates</td><td/></row>
		<row><td>PatchWelcome</td><td>Installed And PATCH And Not IS_MAJOR_UPGRADE</td><td>1205</td><td>Patch Panel</td><td/></row>
		<row><td>RMCCPSearch</td><td>Not CCP_SUCCESS And CCP_TEST</td><td>600</td><td>RMCCPSearch</td><td/></row>
		<row><td>ResolveSource</td><td>Not Installed</td><td>990</td><td>ResolveSource</td><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>VersionNT = 400</td><td>970</td><td/><td/></row>
		<row><td>SetupCompleteError</td><td/><td>-3</td><td>SetupCompleteError</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td/><td>-1</td><td>SetupCompleteSuccess</td><td/></row>
		<row><td>SetupInitialization</td><td/><td>420</td><td>SetupInitialization</td><td/></row>
		<row><td>SetupInterrupted</td><td/><td>-2</td><td>SetupInterrupted</td><td/></row>
		<row><td>SetupProgress</td><td/><td>1240</td><td>SetupProgress</td><td/></row>
		<row><td>SetupResume</td><td>Installed And (RESUME Or Preselected) And Not PATCH</td><td>1220</td><td>SetupResume</td><td/></row>
		<row><td>ValidateProductID</td><td/><td>700</td><td>ValidateProductID</td><td/></row>
		<row><td>setAllUsersProfile2K</td><td>VersionNT &gt;= 500</td><td>980</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>VersionNT</td><td>960</td><td/><td/></row>
	</table>

	<table name="IsolatedComponent">
		<col key="yes" def="s72">Component_Shared</col>
		<col key="yes" def="s72">Component_Application</col>
	</table>

	<table name="LaunchCondition">
		<col key="yes" def="s255">Condition</col>
		<col def="l255">Description</col>
		<row><td>DOTNETVERSION45FULL&gt;="#1"</td><td>##IDPROP_EXPRESS_LAUNCH_CONDITION_DOTNETVERSION45FULL##</td></row>
	</table>

	<table name="ListBox">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
	</table>

	<table name="ListView">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
		<col def="S72">Binary_</col>
	</table>

	<table name="LockPermissions">
		<col key="yes" def="s72">LockObject</col>
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="S255">Domain</col>
		<col key="yes" def="s255">User</col>
		<col def="I4">Permission</col>
	</table>

	<table name="MIME">
		<col key="yes" def="s64">ContentType</col>
		<col def="s255">Extension_</col>
		<col def="S38">CLSID</col>
	</table>

	<table name="Media">
		<col key="yes" def="i2">DiskId</col>
		<col def="i2">LastSequence</col>
		<col def="L64">DiskPrompt</col>
		<col def="S255">Cabinet</col>
		<col def="S32">VolumeLabel</col>
		<col def="S32">Source</col>
	</table>

	<table name="MoveFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="L255">SourceName</col>
		<col def="L255">DestName</col>
		<col def="S72">SourceFolder</col>
		<col def="s72">DestFolder</col>
		<col def="i2">Options</col>
	</table>

	<table name="MsiAssembly">
		<col key="yes" def="s72">Component_</col>
		<col def="s38">Feature_</col>
		<col def="S72">File_Manifest</col>
		<col def="S72">File_Application</col>
		<col def="I2">Attributes</col>
	</table>

	<table name="MsiAssemblyName">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="s255">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="MsiDigitalCertificate">
		<col key="yes" def="s72">DigitalCertificate</col>
		<col def="v0">CertData</col>
	</table>

	<table name="MsiDigitalSignature">
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="s72">SignObject</col>
		<col def="s72">DigitalCertificate_</col>
		<col def="V0">Hash</col>
	</table>

	<table name="MsiDriverPackages">
		<col key="yes" def="s72">Component</col>
		<col def="i4">Flags</col>
		<col def="I4">Sequence</col>
		<col def="S0">ReferenceComponents</col>
	</table>

	<table name="MsiEmbeddedChainer">
		<col key="yes" def="s72">MsiEmbeddedChainer</col>
		<col def="S255">Condition</col>
		<col def="S255">CommandLine</col>
		<col def="s72">Source</col>
		<col def="I4">Type</col>
	</table>

	<table name="MsiEmbeddedUI">
		<col key="yes" def="s72">MsiEmbeddedUI</col>
		<col def="s255">FileName</col>
		<col def="i2">Attributes</col>
		<col def="I4">MessageFilter</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="MsiFileHash">
		<col key="yes" def="s72">File_</col>
		<col def="i2">Options</col>
		<col def="i4">HashPart1</col>
		<col def="i4">HashPart2</col>
		<col def="i4">HashPart3</col>
		<col def="i4">HashPart4</col>
	</table>

	<table name="MsiLockPermissionsEx">
		<col key="yes" def="s72">MsiLockPermissionsEx</col>
		<col def="s72">LockObject</col>
		<col def="s32">Table</col>
		<col def="s0">SDDLText</col>
		<col def="S255">Condition</col>
	</table>

	<table name="MsiPackageCertificate">
		<col key="yes" def="s72">PackageCertificate</col>
		<col def="s72">DigitalCertificate_</col>
	</table>

	<table name="MsiPatchCertificate">
		<col key="yes" def="s72">PatchCertificate</col>
		<col def="s72">DigitalCertificate_</col>
	</table>

	<table name="MsiPatchMetadata">
		<col key="yes" def="s72">PatchConfiguration_</col>
		<col key="yes" def="S72">Company</col>
		<col key="yes" def="s72">Property</col>
		<col def="S0">Value</col>
	</table>

	<table name="MsiPatchOldAssemblyFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="S72">Assembly_</col>
	</table>

	<table name="MsiPatchOldAssemblyName">
		<col key="yes" def="s72">Assembly</col>
		<col key="yes" def="s255">Name</col>
		<col def="S255">Value</col>
	</table>

	<table name="MsiPatchSequence">
		<col key="yes" def="s72">PatchConfiguration_</col>
		<col key="yes" def="s0">PatchFamily</col>
		<col key="yes" def="S0">Target</col>
		<col def="s0">Sequence</col>
		<col def="i2">Supersede</col>
	</table>

	<table name="MsiServiceConfig">
		<col key="yes" def="s72">MsiServiceConfig</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="i4">ConfigType</col>
		<col def="S0">Argument</col>
		<col def="s72">Component_</col>
	</table>

	<table name="MsiServiceConfigFailureActions">
		<col key="yes" def="s72">MsiServiceConfigFailureActions</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="I4">ResetPeriod</col>
		<col def="L255">RebootMessage</col>
		<col def="L255">Command</col>
		<col def="S0">Actions</col>
		<col def="S0">DelayActions</col>
		<col def="s72">Component_</col>
	</table>

	<table name="MsiShortcutProperty">
		<col key="yes" def="s72">MsiShortcutProperty</col>
		<col def="s72">Shortcut_</col>
		<col def="s0">PropertyKey</col>
		<col def="s0">PropVariantValue</col>
	</table>

	<table name="ODBCAttribute">
		<col key="yes" def="s72">Driver_</col>
		<col key="yes" def="s40">Attribute</col>
		<col def="S255">Value</col>
	</table>

	<table name="ODBCDataSource">
		<col key="yes" def="s72">DataSource</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s255">DriverDescription</col>
		<col def="i2">Registration</col>
	</table>

	<table name="ODBCDriver">
		<col key="yes" def="s72">Driver</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s72">File_</col>
		<col def="S72">File_Setup</col>
	</table>

	<table name="ODBCSourceAttribute">
		<col key="yes" def="s72">DataSource_</col>
		<col key="yes" def="s32">Attribute</col>
		<col def="S255">Value</col>
	</table>

	<table name="ODBCTranslator">
		<col key="yes" def="s72">Translator</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s72">File_</col>
		<col def="S72">File_Setup</col>
	</table>

	<table name="Patch">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="i2">Sequence</col>
		<col def="i4">PatchSize</col>
		<col def="i2">Attributes</col>
		<col def="V0">Header</col>
		<col def="S38">StreamRef_</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="PatchPackage">
		<col key="yes" def="s38">PatchId</col>
		<col def="i2">Media_</col>
	</table>

	<table name="ProgId">
		<col key="yes" def="s255">ProgId</col>
		<col def="S255">ProgId_Parent</col>
		<col def="S38">Class_</col>
		<col def="L255">Description</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="Property">
		<col key="yes" def="s72">Property</col>
		<col def="L0">Value</col>
		<col def="S255">ISComments</col>
		<row><td>ALLUSERS</td><td>1</td><td/></row>
		<row><td>ARPINSTALLLOCATION</td><td/><td/></row>
		<row><td>ARPPRODUCTICON</td><td>ARPPRODUCTICON.exe</td><td/></row>
		<row><td>ARPSIZE</td><td/><td/></row>
		<row><td>ARPURLINFOABOUT</td><td>##ID_STRING1##</td><td/></row>
		<row><td>AgreeToLicense</td><td>No</td><td/></row>
		<row><td>ApplicationUsers</td><td>AllUsers</td><td/></row>
		<row><td>DWUSINTERVAL</td><td>30</td><td/></row>
		<row><td>DWUSLINK</td><td>CE9B90DFF9BBC7CFA9ACB78F0EEB978FBEDC408F79CB1758CECBB7DFDE4BF7CFFEECF7EFC9AC</td><td/></row>
		<row><td>DefaultUIFont</td><td>ExpressDefault</td><td/></row>
		<row><td>DialogCaption</td><td>InstallShield for Windows Installer</td><td/></row>
		<row><td>DiskPrompt</td><td>[1]</td><td/></row>
		<row><td>DiskSerial</td><td>1234-5678</td><td/></row>
		<row><td>DisplayNameCustom</td><td>##IDS__DisplayName_Custom##</td><td/></row>
		<row><td>DisplayNameMinimal</td><td>##IDS__DisplayName_Minimal##</td><td/></row>
		<row><td>DisplayNameTypical</td><td>##IDS__DisplayName_Typical##</td><td/></row>
		<row><td>Display_IsBitmapDlg</td><td>1</td><td/></row>
		<row><td>ErrorDialog</td><td>SetupError</td><td/></row>
		<row><td>INSTALLLEVEL</td><td>200</td><td/></row>
		<row><td>ISCHECKFORPRODUCTUPDATES</td><td>1</td><td/></row>
		<row><td>ISENABLEDWUSFINISHDIALOG</td><td/><td/></row>
		<row><td>ISSHOWMSILOG</td><td/><td/></row>
		<row><td>ISVROOT_PORT_NO</td><td>0</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_COST</td><td>##IDS_COMPLUS_PROGRESSTEXT_COST##</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_INSTALL</td><td>##IDS_COMPLUS_PROGRESSTEXT_INSTALL##</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_UNINSTALL</td><td>##IDS_COMPLUS_PROGRESSTEXT_UNINSTALL##</td><td/></row>
		<row><td>IS_PREVENT_DOWNGRADE_EXIT</td><td>##IDS_PREVENT_DOWNGRADE_EXIT##</td><td/></row>
		<row><td>IS_PROGMSG_TEXTFILECHANGS_REPLACE</td><td>##IDS_PROGMSG_TEXTFILECHANGS_REPLACE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_COSTING</td><td>##IDS_PROGMSG_XML_COSTING##</td><td/></row>
		<row><td>IS_PROGMSG_XML_CREATE_FILE</td><td>##IDS_PROGMSG_XML_CREATE_FILE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_FILES</td><td>##IDS_PROGMSG_XML_FILES##</td><td/></row>
		<row><td>IS_PROGMSG_XML_REMOVE_FILE</td><td>##IDS_PROGMSG_XML_REMOVE_FILE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_ROLLBACK_FILES</td><td>##IDS_PROGMSG_XML_ROLLBACK_FILES##</td><td/></row>
		<row><td>IS_PROGMSG_XML_UPDATE_FILE</td><td>##IDS_PROGMSG_XML_UPDATE_FILE##</td><td/></row>
		<row><td>IS_SQLSERVER_AUTHENTICATION</td><td>0</td><td/></row>
		<row><td>IS_SQLSERVER_DATABASE</td><td/><td/></row>
		<row><td>IS_SQLSERVER_PASSWORD</td><td/><td/></row>
		<row><td>IS_SQLSERVER_SERVER</td><td/><td/></row>
		<row><td>IS_SQLSERVER_USERNAME</td><td>sa</td><td/></row>
		<row><td>InstallChoice</td><td>AR</td><td/></row>
		<row><td>LAUNCHPROGRAM</td><td>1</td><td/></row>
		<row><td>LAUNCHREADME</td><td>1</td><td/></row>
		<row><td>Manufacturer</td><td>##COMPANY_NAME##</td><td/></row>
		<row><td>PIDKEY</td><td/><td/></row>
		<row><td>PIDTemplate</td><td>12345&lt;###-%%%%%%%&gt;@@@@@</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEAPPPOOL</td><td>##IDS_PROGMSG_IIS_CREATEAPPPOOL##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEAPPPOOLS</td><td>##IDS_PROGMSG_IIS_CREATEAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEVROOT</td><td>##IDS_PROGMSG_IIS_CREATEVROOT##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEVROOTS</td><td>##IDS_PROGMSG_IIS_CREATEVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSERVICEEXTENSION</td><td>##IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSION##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSITE</td><td>##IDS_PROGMSG_IIS_CREATEWEBSITE##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSITES</td><td>##IDS_PROGMSG_IIS_CREATEWEBSITES##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACT</td><td>##IDS_PROGMSG_IIS_EXTRACT##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTDONE</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTDONEz</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTzDONE</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEAPPPOOL</td><td>##IDS_PROGMSG_IIS_REMOVEAPPPOOL##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEAPPPOOLS</td><td>##IDS_PROGMSG_IIS_REMOVEAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVESITE</td><td>##IDS_PROGMSG_IIS_REMOVESITE##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEVROOT</td><td>##IDS_PROGMSG_IIS_REMOVEVROOT##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEVROOTS</td><td>##IDS_PROGMSG_IIS_REMOVEVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSITES</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSITES##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKAPPPOOLS</td><td>##IDS_PROGMSG_IIS_ROLLBACKAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKVROOTS</td><td>##IDS_PROGMSG_IIS_ROLLBACKVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>ProductCode</td><td>{E6A245FD-7672-47E2-AE3A-8C94AAFCF319}</td><td/></row>
		<row><td>ProductName</td><td>SimpleWin IEMS</td><td/></row>
		<row><td>ProductVersion</td><td>1.00.0000</td><td/></row>
		<row><td>ProgressType0</td><td>install</td><td/></row>
		<row><td>ProgressType1</td><td>Installing</td><td/></row>
		<row><td>ProgressType2</td><td>installed</td><td/></row>
		<row><td>ProgressType3</td><td>installs</td><td/></row>
		<row><td>RebootYesNo</td><td>Yes</td><td/></row>
		<row><td>ReinstallFileVersion</td><td>o</td><td/></row>
		<row><td>ReinstallModeText</td><td>omus</td><td/></row>
		<row><td>ReinstallRepair</td><td>r</td><td/></row>
		<row><td>RestartManagerOption</td><td>CloseRestart</td><td/></row>
		<row><td>SERIALNUMBER</td><td/><td/></row>
		<row><td>SERIALNUMVALSUCCESSRETVAL</td><td>1</td><td/></row>
		<row><td>SecureCustomProperties</td><td>ISFOUNDNEWERPRODUCTVERSION;USERNAME;COMPANYNAME;ISX_SERIALNUM;SUPPORTDIR;DOTNETVERSION45FULL</td><td/></row>
		<row><td>SelectedSetupType</td><td>##IDS__DisplayName_Typical##</td><td/></row>
		<row><td>SetupType</td><td>Typical</td><td/></row>
		<row><td>UpgradeCode</td><td>{87FA0EE5-03E1-41B4-A202-286748843CA1}</td><td/></row>
		<row><td>_IsMaintenance</td><td>Change</td><td/></row>
		<row><td>_IsSetupTypeMin</td><td>Typical</td><td/></row>
	</table>

	<table name="PublishComponent">
		<col key="yes" def="s38">ComponentId</col>
		<col key="yes" def="s255">Qualifier</col>
		<col key="yes" def="s72">Component_</col>
		<col def="L0">AppData</col>
		<col def="s38">Feature_</col>
	</table>

	<table name="RadioButton">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="L64">Text</col>
		<col def="L50">Help</col>
		<col def="I4">ISControlId</col>
		<row><td>AgreeToLicense</td><td>1</td><td>No</td><td>0</td><td>15</td><td>291</td><td>15</td><td>##IDS__AgreeToLicense_0##</td><td/><td/></row>
		<row><td>AgreeToLicense</td><td>2</td><td>Yes</td><td>0</td><td>0</td><td>291</td><td>15</td><td>##IDS__AgreeToLicense_1##</td><td/><td/></row>
		<row><td>ApplicationUsers</td><td>1</td><td>AllUsers</td><td>1</td><td>7</td><td>290</td><td>14</td><td>##IDS__IsRegisterUserDlg_Anyone##</td><td/><td/></row>
		<row><td>ApplicationUsers</td><td>2</td><td>OnlyCurrentUser</td><td>1</td><td>23</td><td>290</td><td>14</td><td>##IDS__IsRegisterUserDlg_OnlyMe##</td><td/><td/></row>
		<row><td>RestartManagerOption</td><td>1</td><td>CloseRestart</td><td>6</td><td>9</td><td>331</td><td>14</td><td>##IDS__IsMsiRMFilesInUse_CloseRestart##</td><td/><td/></row>
		<row><td>RestartManagerOption</td><td>2</td><td>Reboot</td><td>6</td><td>21</td><td>331</td><td>14</td><td>##IDS__IsMsiRMFilesInUse_RebootAfter##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>1</td><td>Change</td><td>0</td><td>0</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Modify##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>2</td><td>Reinstall</td><td>0</td><td>60</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Repair##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>3</td><td>Remove</td><td>0</td><td>120</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Remove##</td><td/><td/></row>
		<row><td>_IsSetupTypeMin</td><td>1</td><td>Typical</td><td>1</td><td>6</td><td>264</td><td>14</td><td>##IDS__IsSetupTypeMinDlg_Typical##</td><td/><td/></row>
	</table>

	<table name="RegLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="i2">Root</col>
		<col def="s255">Key</col>
		<col def="S255">Name</col>
		<col def="I2">Type</col>
		<row><td>DotNet45Full</td><td>2</td><td>SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full</td><td>Version</td><td>2</td></row>
	</table>

	<table name="Registry">
		<col key="yes" def="s72">Registry</col>
		<col def="i2">Root</col>
		<col def="s255">Key</col>
		<col def="S255">Name</col>
		<col def="S0">Value</col>
		<col def="s72">Component_</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="RemoveFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="L255">FileName</col>
		<col def="s72">DirProperty</col>
		<col def="i2">InstallMode</col>
		<row><td>IEMS.EXE</td><td>IEMS.EXE</td><td/><td>simple_1_simplewin_iems</td><td>2</td></row>
		<row><td>WebApi.exe</td><td>WebApi.exe</td><td/><td>simple_1_simplewin_iems</td><td>2</td></row>
		<row><td>WebApi.vshost.exe</td><td>WebApi.vshost.exe</td><td/><td>simple_1_simplewin_iems</td><td>2</td></row>
	</table>

	<table name="RemoveIniFile">
		<col key="yes" def="s72">RemoveIniFile</col>
		<col def="l255">FileName</col>
		<col def="S72">DirProperty</col>
		<col def="l96">Section</col>
		<col def="l128">Key</col>
		<col def="L255">Value</col>
		<col def="i2">Action</col>
		<col def="s72">Component_</col>
	</table>

	<table name="RemoveRegistry">
		<col key="yes" def="s72">RemoveRegistry</col>
		<col def="i2">Root</col>
		<col def="l255">Key</col>
		<col def="L255">Name</col>
		<col def="s72">Component_</col>
	</table>

	<table name="ReserveCost">
		<col key="yes" def="s72">ReserveKey</col>
		<col def="s72">Component_</col>
		<col def="S72">ReserveFolder</col>
		<col def="i4">ReserveLocal</col>
		<col def="i4">ReserveSource</col>
	</table>

	<table name="SFPCatalog">
		<col key="yes" def="s255">SFPCatalog</col>
		<col def="V0">Catalog</col>
		<col def="S0">Dependency</col>
	</table>

	<table name="SelfReg">
		<col key="yes" def="s72">File_</col>
		<col def="I2">Cost</col>
	</table>

	<table name="ServiceControl">
		<col key="yes" def="s72">ServiceControl</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="S255">Arguments</col>
		<col def="I2">Wait</col>
		<col def="s72">Component_</col>
	</table>

	<table name="ServiceInstall">
		<col key="yes" def="s72">ServiceInstall</col>
		<col def="s255">Name</col>
		<col def="L255">DisplayName</col>
		<col def="i4">ServiceType</col>
		<col def="i4">StartType</col>
		<col def="i4">ErrorControl</col>
		<col def="S255">LoadOrderGroup</col>
		<col def="S255">Dependencies</col>
		<col def="S255">StartName</col>
		<col def="S255">Password</col>
		<col def="S255">Arguments</col>
		<col def="s72">Component_</col>
		<col def="L255">Description</col>
	</table>

	<table name="Shortcut">
		<col key="yes" def="s72">Shortcut</col>
		<col def="s72">Directory_</col>
		<col def="l128">Name</col>
		<col def="s72">Component_</col>
		<col def="s255">Target</col>
		<col def="S255">Arguments</col>
		<col def="L255">Description</col>
		<col def="I2">Hotkey</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="I2">ShowCmd</col>
		<col def="S72">WkDir</col>
		<col def="S255">DisplayResourceDLL</col>
		<col def="I2">DisplayResourceId</col>
		<col def="S255">DescriptionResourceDLL</col>
		<col def="I2">DescriptionResourceId</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISShortcutName</col>
		<col def="I4">ISAttributes</col>
		<row><td>IEMS.EXE</td><td>simple_1_simplewin_iems</td><td>##IDS_SHORTCUT_DISPLAY_NAME38##</td><td>IEMS.EXE</td><td>AlwaysInstall</td><td/><td/><td/><td>IEMS.EXE_A5F5863877CF42FF8FEB46EDB80A6B12.exe</td><td>1</td><td>1</td><td>INSTALLDIR</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>WebApi.exe</td><td>simple_1_simplewin_iems</td><td>##IDS_SHORTCUT_DISPLAY_NAME42##</td><td>WebApi.exe</td><td>AlwaysInstall</td><td/><td/><td/><td>WebApi.exe_2966594750D946D385C432D8BB6062DC.exe</td><td>0</td><td>1</td><td>INSTALLDIR</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>WebApi.vshost.exe</td><td>simple_1_simplewin_iems</td><td>##IDS_SHORTCUT_DISPLAY_NAME43##</td><td>WebApi.vshost.exe</td><td>AlwaysInstall</td><td/><td/><td/><td>WebApi.vshost.exe_85DA5322084C45008FCF225677ABC0D3.exe</td><td>0</td><td>1</td><td>INSTALLDIR</td><td/><td/><td/><td/><td/><td/><td/></row>
	</table>

	<table name="Signature">
		<col key="yes" def="s72">Signature</col>
		<col def="s255">FileName</col>
		<col def="S20">MinVersion</col>
		<col def="S20">MaxVersion</col>
		<col def="I4">MinSize</col>
		<col def="I4">MaxSize</col>
		<col def="I4">MinDate</col>
		<col def="I4">MaxDate</col>
		<col def="S255">Languages</col>
	</table>

	<table name="TextStyle">
		<col key="yes" def="s72">TextStyle</col>
		<col def="s32">FaceName</col>
		<col def="i2">Size</col>
		<col def="I4">Color</col>
		<col def="I2">StyleBits</col>
		<row><td>Arial8</td><td>Arial</td><td>8</td><td/><td/></row>
		<row><td>Arial9</td><td>Arial</td><td>9</td><td/><td/></row>
		<row><td>ArialBlue10</td><td>Arial</td><td>10</td><td>16711680</td><td/></row>
		<row><td>ArialBlueStrike10</td><td>Arial</td><td>10</td><td>16711680</td><td>8</td></row>
		<row><td>CourierNew8</td><td>Courier New</td><td>8</td><td/><td/></row>
		<row><td>CourierNew9</td><td>Courier New</td><td>9</td><td/><td/></row>
		<row><td>ExpressDefault</td><td>Tahoma</td><td>8</td><td/><td/></row>
		<row><td>MSGothic9</td><td>MS Gothic</td><td>9</td><td/><td/></row>
		<row><td>MSSGreySerif8</td><td>MS Sans Serif</td><td>8</td><td>8421504</td><td/></row>
		<row><td>MSSWhiteSerif8</td><td>Tahoma</td><td>8</td><td>16777215</td><td/></row>
		<row><td>MSSansBold8</td><td>Tahoma</td><td>8</td><td/><td>1</td></row>
		<row><td>MSSansSerif8</td><td>MS Sans Serif</td><td>8</td><td/><td/></row>
		<row><td>MSSansSerif9</td><td>MS Sans Serif</td><td>9</td><td/><td/></row>
		<row><td>Tahoma10</td><td>Tahoma</td><td>10</td><td/><td/></row>
		<row><td>Tahoma8</td><td>Tahoma</td><td>8</td><td/><td/></row>
		<row><td>Tahoma9</td><td>Tahoma</td><td>9</td><td/><td/></row>
		<row><td>TahomaBold10</td><td>Tahoma</td><td>10</td><td/><td>1</td></row>
		<row><td>TahomaBold8</td><td>Tahoma</td><td>8</td><td/><td>1</td></row>
		<row><td>Times8</td><td>Times New Roman</td><td>8</td><td/><td/></row>
		<row><td>Times9</td><td>Times New Roman</td><td>9</td><td/><td/></row>
		<row><td>TimesItalic12</td><td>Times New Roman</td><td>12</td><td/><td>2</td></row>
		<row><td>TimesItalicBlue10</td><td>Times New Roman</td><td>10</td><td>16711680</td><td>2</td></row>
		<row><td>TimesRed16</td><td>Times New Roman</td><td>16</td><td>255</td><td/></row>
		<row><td>VerdanaBold14</td><td>Verdana</td><td>13</td><td/><td>1</td></row>
	</table>

	<table name="TypeLib">
		<col key="yes" def="s38">LibID</col>
		<col key="yes" def="i2">Language</col>
		<col key="yes" def="s72">Component_</col>
		<col def="I4">Version</col>
		<col def="L128">Description</col>
		<col def="S72">Directory_</col>
		<col def="s38">Feature_</col>
		<col def="I4">Cost</col>
	</table>

	<table name="UIText">
		<col key="yes" def="s72">Key</col>
		<col def="L255">Text</col>
		<row><td>AbsentPath</td><td/></row>
		<row><td>GB</td><td>##IDS_UITEXT_GB##</td></row>
		<row><td>KB</td><td>##IDS_UITEXT_KB##</td></row>
		<row><td>MB</td><td>##IDS_UITEXT_MB##</td></row>
		<row><td>MenuAbsent</td><td>##IDS_UITEXT_FeatureNotAvailable##</td></row>
		<row><td>MenuAdvertise</td><td>##IDS_UITEXT_FeatureInstalledWhenRequired2##</td></row>
		<row><td>MenuAllCD</td><td>##IDS_UITEXT_FeatureInstalledCD##</td></row>
		<row><td>MenuAllLocal</td><td>##IDS_UITEXT_FeatureInstalledLocal##</td></row>
		<row><td>MenuAllNetwork</td><td>##IDS_UITEXT_FeatureInstalledNetwork##</td></row>
		<row><td>MenuCD</td><td>##IDS_UITEXT_FeatureInstalledCD2##</td></row>
		<row><td>MenuLocal</td><td>##IDS_UITEXT_FeatureInstalledLocal2##</td></row>
		<row><td>MenuNetwork</td><td>##IDS_UITEXT_FeatureInstalledNetwork2##</td></row>
		<row><td>NewFolder</td><td>##IDS_UITEXT_Folder##</td></row>
		<row><td>SelAbsentAbsent</td><td>##IDS_UITEXT_GB##</td></row>
		<row><td>SelAbsentAdvertise</td><td>##IDS_UITEXT_FeatureInstalledWhenRequired##</td></row>
		<row><td>SelAbsentCD</td><td>##IDS_UITEXT_FeatureOnCD##</td></row>
		<row><td>SelAbsentLocal</td><td>##IDS_UITEXT_FeatureLocal##</td></row>
		<row><td>SelAbsentNetwork</td><td>##IDS_UITEXT_FeatureNetwork##</td></row>
		<row><td>SelAdvertiseAbsent</td><td>##IDS_UITEXT_FeatureUnavailable##</td></row>
		<row><td>SelAdvertiseAdvertise</td><td>##IDS_UITEXT_FeatureInstalledRequired##</td></row>
		<row><td>SelAdvertiseCD</td><td>##IDS_UITEXT_FeatureOnCD2##</td></row>
		<row><td>SelAdvertiseLocal</td><td>##IDS_UITEXT_FeatureLocal2##</td></row>
		<row><td>SelAdvertiseNetwork</td><td>##IDS_UITEXT_FeatureNetwork2##</td></row>
		<row><td>SelCDAbsent</td><td>##IDS_UITEXT_FeatureWillBeUninstalled##</td></row>
		<row><td>SelCDAdvertise</td><td>##IDS_UITEXT_FeatureWasCD##</td></row>
		<row><td>SelCDCD</td><td>##IDS_UITEXT_FeatureRunFromCD##</td></row>
		<row><td>SelCDLocal</td><td>##IDS_UITEXT_FeatureWasCDLocal##</td></row>
		<row><td>SelChildCostNeg</td><td>##IDS_UITEXT_FeatureFreeSpace##</td></row>
		<row><td>SelChildCostPos</td><td>##IDS_UITEXT_FeatureRequiredSpace##</td></row>
		<row><td>SelCostPending</td><td>##IDS_UITEXT_CompilingFeaturesCost##</td></row>
		<row><td>SelLocalAbsent</td><td>##IDS_UITEXT_FeatureCompletelyRemoved##</td></row>
		<row><td>SelLocalAdvertise</td><td>##IDS_UITEXT_FeatureRemovedUnlessRequired##</td></row>
		<row><td>SelLocalCD</td><td>##IDS_UITEXT_FeatureRemovedCD##</td></row>
		<row><td>SelLocalLocal</td><td>##IDS_UITEXT_FeatureRemainLocal##</td></row>
		<row><td>SelLocalNetwork</td><td>##IDS_UITEXT_FeatureRemoveNetwork##</td></row>
		<row><td>SelNetworkAbsent</td><td>##IDS_UITEXT_FeatureUninstallNoNetwork##</td></row>
		<row><td>SelNetworkAdvertise</td><td>##IDS_UITEXT_FeatureWasOnNetworkInstalled##</td></row>
		<row><td>SelNetworkLocal</td><td>##IDS_UITEXT_FeatureWasOnNetworkLocal##</td></row>
		<row><td>SelNetworkNetwork</td><td>##IDS_UITEXT_FeatureContinueNetwork##</td></row>
		<row><td>SelParentCostNegNeg</td><td>##IDS_UITEXT_FeatureSpaceFree##</td></row>
		<row><td>SelParentCostNegPos</td><td>##IDS_UITEXT_FeatureSpaceFree2##</td></row>
		<row><td>SelParentCostPosNeg</td><td>##IDS_UITEXT_FeatureSpaceFree3##</td></row>
		<row><td>SelParentCostPosPos</td><td>##IDS_UITEXT_FeatureSpaceFree4##</td></row>
		<row><td>TimeRemaining</td><td>##IDS_UITEXT_TimeRemaining##</td></row>
		<row><td>VolumeCostAvailable</td><td>##IDS_UITEXT_Available##</td></row>
		<row><td>VolumeCostDifference</td><td>##IDS_UITEXT_Differences##</td></row>
		<row><td>VolumeCostRequired</td><td>##IDS_UITEXT_Required##</td></row>
		<row><td>VolumeCostSize</td><td>##IDS_UITEXT_DiskSize##</td></row>
		<row><td>VolumeCostVolume</td><td>##IDS_UITEXT_Volume##</td></row>
		<row><td>bytes</td><td>##IDS_UITEXT_Bytes##</td></row>
	</table>

	<table name="Upgrade">
		<col key="yes" def="s38">UpgradeCode</col>
		<col key="yes" def="S20">VersionMin</col>
		<col key="yes" def="S20">VersionMax</col>
		<col key="yes" def="S255">Language</col>
		<col key="yes" def="i4">Attributes</col>
		<col def="S255">Remove</col>
		<col def="s72">ActionProperty</col>
		<col def="S72">ISDisplayName</col>
		<row><td>{00000000-0000-0000-0000-000000000000}</td><td>***ALL_VERSIONS***</td><td></td><td></td><td>2</td><td/><td>ISFOUNDNEWERPRODUCTVERSION</td><td>ISPreventDowngrade</td></row>
	</table>

	<table name="Verb">
		<col key="yes" def="s255">Extension_</col>
		<col key="yes" def="s32">Verb</col>
		<col def="I2">Sequence</col>
		<col def="L255">Command</col>
		<col def="L255">Argument</col>
	</table>

	<table name="_Validation">
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="s32">Column</col>
		<col def="s4">Nullable</col>
		<col def="I4">MinValue</col>
		<col def="I4">MaxValue</col>
		<col def="S255">KeyTable</col>
		<col def="I2">KeyColumn</col>
		<col def="S32">Category</col>
		<col def="S255">Set</col>
		<col def="S255">Description</col>
		<row><td>ActionText</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to be described.</td></row>
		<row><td>ActionText</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description displayed in progress dialog and log when action is executing.</td></row>
		<row><td>ActionText</td><td>Template</td><td>Y</td><td/><td/><td/><td/><td>Template</td><td/><td>Optional localized format template used to format action data records for display during action execution.</td></row>
		<row><td>AdminExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdminExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdminExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdminExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdminExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdminUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdminUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdminUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdminUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdminUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdvtExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdvtExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdvtUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdvtUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdvtUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdvtUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdvtUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AppId</td><td>ActivateAtStorage</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td/></row>
		<row><td>AppId</td><td>AppId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td/></row>
		<row><td>AppId</td><td>DllSurrogate</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppId</td><td>LocalService</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppId</td><td>RemoteServerName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>AppId</td><td>RunAsInteractiveUser</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td/></row>
		<row><td>AppId</td><td>ServiceParameters</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppSearch</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The property associated with a Signature</td></row>
		<row><td>AppSearch</td><td>Signature_</td><td>N</td><td/><td/><td>ISXmlLocator;Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>BBControl</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>BBControl</td><td>BBControl</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the control. This name must be unique within a billboard, but can repeat on different billboard.</td></row>
		<row><td>BBControl</td><td>Billboard_</td><td>N</td><td/><td/><td>Billboard</td><td>1</td><td>Identifier</td><td/><td>External key to the Billboard table, name of the billboard.</td></row>
		<row><td>BBControl</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>BBControl</td><td>Type</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The type of the control.</td></row>
		<row><td>BBControl</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>Billboard</td><td>Action</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of an action. The billboard is displayed during the progress messages received from this action.</td></row>
		<row><td>Billboard</td><td>Billboard</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the billboard.</td></row>
		<row><td>Billboard</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>An external key to the Feature Table. The billboard is shown only if this feature is being installed.</td></row>
		<row><td>Billboard</td><td>Ordering</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>A positive integer. If there is more than one billboard corresponding to an action they will be shown in the order defined by this column.</td></row>
		<row><td>Binary</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>Binary</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>Binary</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique key identifying the binary data.</td></row>
		<row><td>BindImage</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>The index into the File table. This must be an executable file.</td></row>
		<row><td>BindImage</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>Paths</td><td/><td>A list of ;  delimited paths that represent the paths to be searched for the import DLLS. The list is usually a list of properties each enclosed within square brackets [] .</td></row>
		<row><td>CCPSearch</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>CheckBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to the item.</td></row>
		<row><td>CheckBox</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with the item.</td></row>
		<row><td>Class</td><td>AppId_</td><td>Y</td><td/><td/><td>AppId</td><td>1</td><td>Guid</td><td/><td>Optional AppID containing DCOM information for associated application (string GUID).</td></row>
		<row><td>Class</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>optional argument for LocalServers.</td></row>
		<row><td>Class</td><td>Attributes</td><td>Y</td><td/><td>32767</td><td/><td/><td/><td/><td>Class registration attributes.</td></row>
		<row><td>Class</td><td>CLSID</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The CLSID of an OLE factory.</td></row>
		<row><td>Class</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>Class</td><td>Context</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The numeric server context for this server. CLSCTX_xxxx</td></row>
		<row><td>Class</td><td>DefInprocHandler</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td>1;2;3</td><td>Optional default inproc handler.  Only optionally provided if Context=CLSCTX_LOCAL_SERVER.  Typically "ole32.dll" or "mapi32.dll"</td></row>
		<row><td>Class</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description for the Class.</td></row>
		<row><td>Class</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the CLSID factory to be operational.</td></row>
		<row><td>Class</td><td>FileTypeMask</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Optional string containing information for the HKCRthis CLSID) key. If multiple patterns exist, they must be delimited by a semicolon, and numeric subkeys will be generated: 0,1,2...</td></row>
		<row><td>Class</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index.</td></row>
		<row><td>Class</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Optional foreign key into the Icon Table, specifying the icon file associated with this CLSID. Will be written under the DefaultIcon key.</td></row>
		<row><td>Class</td><td>ProgId_Default</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>Optional ProgId associated with this CLSID.</td></row>
		<row><td>ComboBox</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list.	The integers do not have to be consecutive.</td></row>
		<row><td>ComboBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same combobox.</td></row>
		<row><td>ComboBox</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ComboBox</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>CompLocator</td><td>ComponentId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID unique to this component, version, and language.</td></row>
		<row><td>CompLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>CompLocator</td><td>Type</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td>A boolean value that determines if the registry value is a filename or a directory location.</td></row>
		<row><td>Complus</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the ComPlus component.</td></row>
		<row><td>Complus</td><td>ExpType</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>ComPlus component attributes.</td></row>
		<row><td>Component</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Remote execution option, one of irsEnum</td></row>
		<row><td>Component</td><td>Component</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>Component</td><td>ComponentId</td><td>Y</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID unique to this component, version, and language.</td></row>
		<row><td>Component</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A conditional statement that will disable this component if the specified condition evaluates to the 'True' state. If a component is disabled, it will not be installed, regardless of the 'Action' state associated with the component.</td></row>
		<row><td>Component</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Required key of a Directory table record. This is actually a property name whose value contains the actual path, set either by the AppSearch action or with the default setting obtained from the Directory table.</td></row>
		<row><td>Component</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a component.</td></row>
		<row><td>Component</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsCommit</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsInstall</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsRollback</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsUninstall</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISRegFileToMergeAtBuild</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path and File name of a .REG file to merge into the component at build time.</td></row>
		<row><td>Component</td><td>ISScanAtBuildFile</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File used by the Dot Net scanner to populate dependant assemblies' File_Application field.</td></row>
		<row><td>Component</td><td>KeyPath</td><td>Y</td><td/><td/><td>File;ODBCDataSource;Registry</td><td>1</td><td>Identifier</td><td/><td>Either the primary key into the File table, Registry table, or ODBCDataSource table. This extract path is stored when the component is installed, and is used to detect the presence of the component and to return the path to it.</td></row>
		<row><td>Condition</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Expression evaluated to determine if Level in the Feature table is to change.</td></row>
		<row><td>Condition</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Reference to a Feature entry in Feature table.</td></row>
		<row><td>Condition</td><td>Level</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>New selection Level to set in Feature table if Condition evaluates to TRUE.</td></row>
		<row><td>Control</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>Control</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>External key to the Binary table.</td></row>
		<row><td>Control</td><td>Control</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the control. This name must be unique within a dialog, but can repeat on different dialogs.</td></row>
		<row><td>Control</td><td>Control_Next</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>The name of an other control on the same dialog. This link defines the tab order of the controls. The links have to form one or more cycles!</td></row>
		<row><td>Control</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>External key to the Dialog table, name of the dialog.</td></row>
		<row><td>Control</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>Help</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The help strings used with the button. The text is optional.</td></row>
		<row><td>Control</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to .rtf file for scrollable text control</td></row>
		<row><td>Control</td><td>ISControlId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A number used to represent the control ID of the Control, Used in Dialog export</td></row>
		<row><td>Control</td><td>ISWindowStyle</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies non-MSI window styles to be applied to this control.</td></row>
		<row><td>Control</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of a defined property to be linked to this control.</td></row>
		<row><td>Control</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>Control</td><td>Type</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The type of the control.</td></row>
		<row><td>Control</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ControlCondition</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>Default;Disable;Enable;Hide;Show</td><td>The desired action to be taken on the specified control.</td></row>
		<row><td>ControlCondition</td><td>Condition</td><td>N</td><td/><td/><td/><td/><td>Condition</td><td/><td>A standard conditional statement that specifies under which conditions the action should be triggered.</td></row>
		<row><td>ControlCondition</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control.</td></row>
		<row><td>ControlCondition</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the dialog.</td></row>
		<row><td>ControlEvent</td><td>Argument</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A value to be used as a modifier when triggering a particular event.</td></row>
		<row><td>ControlEvent</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A standard conditional statement that specifies under which conditions an event should be triggered.</td></row>
		<row><td>ControlEvent</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control</td></row>
		<row><td>ControlEvent</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the dialog.</td></row>
		<row><td>ControlEvent</td><td>Event</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>An identifier that specifies the type of the event that should take place when the user interacts with control specified by the first two entries.</td></row>
		<row><td>ControlEvent</td><td>Ordering</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>An integer used to order several events tied to the same control. Can be left blank.</td></row>
		<row><td>CreateFolder</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table.</td></row>
		<row><td>CreateFolder</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Primary key, could be foreign key into the Directory table.</td></row>
		<row><td>CustomAction</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, name of action, normally appears in sequence table unless private use.</td></row>
		<row><td>CustomAction</td><td>ExtendedType</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The numeric custom action type info flags.</td></row>
		<row><td>CustomAction</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments for this custom action.</td></row>
		<row><td>CustomAction</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>CustomSource</td><td/><td>The table reference of the source of the code.</td></row>
		<row><td>CustomAction</td><td>Target</td><td>Y</td><td/><td/><td>ISDLLWrapper;ISInstallScriptAction</td><td>1</td><td>Formatted</td><td/><td>Excecution parameter, depends on the type of custom action</td></row>
		<row><td>CustomAction</td><td>Type</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>The numeric custom action type, consisting of source location, code type, entry, option flags.</td></row>
		<row><td>Dialog</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this dialog.</td></row>
		<row><td>Dialog</td><td>Control_Cancel</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the cancel control. Hitting escape or clicking on the close icon on the dialog is equivalent to pushing this button.</td></row>
		<row><td>Dialog</td><td>Control_Default</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the default control. Hitting return is equivalent to pushing this button.</td></row>
		<row><td>Dialog</td><td>Control_First</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the control that has the focus when the dialog is created.</td></row>
		<row><td>Dialog</td><td>Dialog</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the dialog.</td></row>
		<row><td>Dialog</td><td>HCentering</td><td>N</td><td>0</td><td>100</td><td/><td/><td/><td/><td>Horizontal position of the dialog on a 0-100 scale. 0 means left end, 100 means right end of the screen, 50 center.</td></row>
		<row><td>Dialog</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the dialog.</td></row>
		<row><td>Dialog</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments for this dialog.</td></row>
		<row><td>Dialog</td><td>ISResourceId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A Number the Specifies the Dialog ID to be used in Dialog Export</td></row>
		<row><td>Dialog</td><td>ISWindowStyle</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies non-MSI window styles to be applied to this control. This is only used in Script Based Setups.</td></row>
		<row><td>Dialog</td><td>TextStyle_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign Key into TextStyle table, only used in Script Based Projects.</td></row>
		<row><td>Dialog</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A text string specifying the title to be displayed in the title bar of the dialog's window.</td></row>
		<row><td>Dialog</td><td>VCentering</td><td>N</td><td>0</td><td>100</td><td/><td/><td/><td/><td>Vertical position of the dialog on a 0-100 scale. 0 means top end, 100 means bottom end of the screen, 50 center.</td></row>
		<row><td>Dialog</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the dialog.</td></row>
		<row><td>Directory</td><td>DefaultDir</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The default sub-path under parent's path.</td></row>
		<row><td>Directory</td><td>Directory</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for directory entry, primary key. If a property by this name is defined, it contains the full path to the directory.</td></row>
		<row><td>Directory</td><td>Directory_Parent</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Reference to the entry in this table specifying the default parent directory. A record parented to itself or with a Null parent represents a root of the install tree.</td></row>
		<row><td>Directory</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3;4;5;6;7</td><td>This is used to store Installshield custom properties of a directory.  Currently the only one is Shortcut.</td></row>
		<row><td>Directory</td><td>ISDescription</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of folder</td></row>
		<row><td>Directory</td><td>ISFolderName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is used in Pro projects because the pro identifier used in the tree wasn't necessarily unique.</td></row>
		<row><td>DrLocator</td><td>Depth</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The depth below the path to which the Signature_ is recursively searched. If absent, the depth is assumed to be 0.</td></row>
		<row><td>DrLocator</td><td>Parent</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The parent file signature. It is also a foreign key in the Signature table. If null and the Path column does not expand to a full path, then all the fixed drives of the user system are searched using the Path.</td></row>
		<row><td>DrLocator</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>AnyPath</td><td/><td>The path on the user system. This is a either a subpath below the value of the Parent or a full path. The path may contain properties enclosed within [ ] that will be expanded.</td></row>
		<row><td>DrLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>DuplicateFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the duplicate file.</td></row>
		<row><td>DuplicateFile</td><td>DestFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full pathname to a destination folder.</td></row>
		<row><td>DuplicateFile</td><td>DestName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Filename to be given to the duplicate file.</td></row>
		<row><td>DuplicateFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular file entry</td></row>
		<row><td>DuplicateFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing the source file to be duplicated.</td></row>
		<row><td>Environment</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the environmental value.</td></row>
		<row><td>Environment</td><td>Environment</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for the environmental variable setting</td></row>
		<row><td>Environment</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the environmental value.</td></row>
		<row><td>Environment</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to set in the environmental settings.</td></row>
		<row><td>Error</td><td>Error</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer error number, obtained from header file IError(...) macros.</td></row>
		<row><td>Error</td><td>Message</td><td>Y</td><td/><td/><td/><td/><td>Template</td><td/><td>Error formatting template, obtained from user ed. or localizers.</td></row>
		<row><td>EventMapping</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of the control attribute, that is set when this event is received.</td></row>
		<row><td>EventMapping</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control.</td></row>
		<row><td>EventMapping</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the Dialog.</td></row>
		<row><td>EventMapping</td><td>Event</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>An identifier that specifies the type of the event that the control subscribes to.</td></row>
		<row><td>Extension</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>Extension</td><td>Extension</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The extension associated with the table row.</td></row>
		<row><td>Extension</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the CLSID factory to be operational.</td></row>
		<row><td>Extension</td><td>MIME_</td><td>Y</td><td/><td/><td>MIME</td><td>1</td><td>Text</td><td/><td>Optional Context identifier, typically "type/format" associated with the extension</td></row>
		<row><td>Extension</td><td>ProgId_</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>Optional ProgId associated with this extension.</td></row>
		<row><td>Feature</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2;4;5;6;8;9;10;16;17;18;20;21;22;24;25;26;32;33;34;36;37;38;48;49;50;52;53;54</td><td>Feature attributes</td></row>
		<row><td>Feature</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Longer descriptive text describing a visible feature item.</td></row>
		<row><td>Feature</td><td>Directory_</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>UpperCase</td><td/><td>The name of the Directory that can be configured by the UI. A non-null value will enable the browse button.</td></row>
		<row><td>Feature</td><td>Display</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Numeric sort order, used to force a specific display ordering.</td></row>
		<row><td>Feature</td><td>Feature</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular feature record.</td></row>
		<row><td>Feature</td><td>Feature_Parent</td><td>Y</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Optional key of a parent record in the same table. If the parent is not selected, then the record will not be installed. Null indicates a root item.</td></row>
		<row><td>Feature</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Comments</td></row>
		<row><td>Feature</td><td>ISFeatureCabName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Name of CAB used when compressing CABs by Feature. Used to override build generated name for CAB file.</td></row>
		<row><td>Feature</td><td>ISProFeatureName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the feature used by pro projects.  This doesn't have to be unique.</td></row>
		<row><td>Feature</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Release Flags that specify whether this  feature will be built in a particular release.</td></row>
		<row><td>Feature</td><td>Level</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The install level at which record will be initially selected. An install level of 0 will disable an item and prevent its display.</td></row>
		<row><td>Feature</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Short text identifying a visible feature item.</td></row>
		<row><td>FeatureComponents</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>FeatureComponents</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>File</td><td>Attributes</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer containing bit flags representing file attributes (with the decimal value of each bit position in parentheses)</td></row>
		<row><td>File</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file.</td></row>
		<row><td>File</td><td>File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token, must match identifier in cabinet.  For uncompressed files, this field is ignored.</td></row>
		<row><td>File</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>File name used for installation.  This may contain a "short name|long name" pair.  It may be just a long name, hence it cannot be of the Filename data type.</td></row>
		<row><td>File</td><td>FileSize</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>File</td><td>ISAttributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>This field contains the following attributes: UseSystemSettings(0x1)</td></row>
		<row><td>File</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>File</td><td>ISComponentSubFolder_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key referencing component subfolder containing this file.  Only for Pro.</td></row>
		<row><td>File</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>List of decimal language Ids, comma-separated if more than one.</td></row>
		<row><td>File</td><td>Sequence</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Sequence with respect to the media images; order must track cabinet order.</td></row>
		<row><td>File</td><td>Version</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Version</td><td/><td>Version string for versioned files;  Blank for unversioned files.</td></row>
		<row><td>FileSFPCatalog</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>File associated with the catalog</td></row>
		<row><td>FileSFPCatalog</td><td>SFPCatalog_</td><td>N</td><td/><td/><td>SFPCatalog</td><td>1</td><td>Text</td><td/><td>Catalog associated with the file</td></row>
		<row><td>Font</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, foreign key into File table referencing font file.</td></row>
		<row><td>Font</td><td>FontTitle</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Font name.</td></row>
		<row><td>ISAssistantTag</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISAssistantTag</td><td>Tag</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Color</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Duration</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Effect</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Font</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>ISBillboard</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Origin</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Sequence</td><td>N</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Style</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Target</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Display name for the chained package. Used only in the IDE.</td></row>
		<row><td>ISChainPackage</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>InstallCondition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>InstallProperties</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Options</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Order</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Package</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>ProductCode</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>RemoveCondition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>RemoveProperties</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>SourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackageData</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>ISChainPackageData</td><td>File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>ISChainPackageData</td><td>FilePath</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackageData</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>ISChainPackageData</td><td>Options</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackageData</td><td>Package_</td><td>N</td><td/><td/><td>ISChainPackage</td><td>1</td><td>Identifier</td><td/><td/></row>
		<row><td>ISClrWrap</td><td>Action_</td><td>N</td><td/><td/><td>CustomAction</td><td>1</td><td>Identifier</td><td/><td>Foreign key into CustomAction table</td></row>
		<row><td>ISClrWrap</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Property associated with this Action</td></row>
		<row><td>ISClrWrap</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value associated with this Property</td></row>
		<row><td>ISComCatalogAttribute</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogAttribute</td><td>ItemName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The named attribute for a catalog object.</td></row>
		<row><td>ISComCatalogAttribute</td><td>ItemValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A value associated with the attribute defined in the ItemName column.</td></row>
		<row><td>ISComCatalogCollection</td><td>CollectionName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>A catalog collection name.</td></row>
		<row><td>ISComCatalogCollection</td><td>ISComCatalogCollection</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComCatalogCollection table.</td></row>
		<row><td>ISComCatalogCollection</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogCollectionObjects</td><td>ISComCatalogCollection_</td><td>N</td><td/><td/><td>ISComCatalogCollection</td><td>1</td><td>Identifier</td><td/><td>A unique key for the ISComCatalogCollection table.</td></row>
		<row><td>ISComCatalogCollectionObjects</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogObject</td><td>DisplayName</td><td>N</td><td/><td/><td/><td/><td/><td/><td>The display name of a catalog object.</td></row>
		<row><td>ISComCatalogObject</td><td>ISComCatalogObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplication</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table that a COM+ application belongs to.</td></row>
		<row><td>ISComPlusApplication</td><td>ComputerName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Computer name that a COM+ application belongs to.</td></row>
		<row><td>ISComPlusApplication</td><td>DepFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List of the dependent files.</td></row>
		<row><td>ISComPlusApplication</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>InstallShield custom attributes associated with a COM+ application.</td></row>
		<row><td>ISComPlusApplication</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>AlterDLL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Alternate filename of the COM+ application component. Will be used for a .NET serviced component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>CLSID</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>CLSID of the COM+ application component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>DLL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Filename of the COM+ application component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComPlusApplicationDLL</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ProgId</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>ProgId of the COM+ application component.</td></row>
		<row><td>ISComPlusProxy</td><td>Component_</td><td>Y</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table that a COM+ application proxy belongs to.</td></row>
		<row><td>ISComPlusProxy</td><td>DepFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List of the dependent files.</td></row>
		<row><td>ISComPlusProxy</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>InstallShield custom attributes associated with a COM+ application proxy.</td></row>
		<row><td>ISComPlusProxy</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table that a COM+ application proxy belongs to.</td></row>
		<row><td>ISComPlusProxy</td><td>ISComPlusProxy</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComPlusProxy table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>ISPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the dependent file.</td></row>
		<row><td>ISComPlusProxyFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusProxyFile</td><td>ISComPlusApplicationDLL_</td><td>N</td><td/><td/><td>ISComPlusApplicationDLL</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>ISPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the dependent file.</td></row>
		<row><td>ISComPlusServerFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusServerFile</td><td>ISComPlusApplicationDLL_</td><td>N</td><td/><td/><td>ISComPlusApplicationDLL</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComponentExtended</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>ISComponentExtended</td><td>FTPLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>FTP Location</td></row>
		<row><td>ISComponentExtended</td><td>FilterProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property to set if you want to filter a component</td></row>
		<row><td>ISComponentExtended</td><td>HTTPLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HTTP Location</td></row>
		<row><td>ISComponentExtended</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Language</td></row>
		<row><td>ISComponentExtended</td><td>Miscellaneous</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Miscellaneous</td></row>
		<row><td>ISComponentExtended</td><td>OS</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>bitwise addition of OSs</td></row>
		<row><td>ISComponentExtended</td><td>Platforms</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>bitwise addition of Platforms.</td></row>
		<row><td>ISCustomActionReference</td><td>Action_</td><td>N</td><td/><td/><td>CustomAction</td><td>1</td><td>Identifier</td><td/><td>Foreign key into theICustomAction table.</td></row>
		<row><td>ISCustomActionReference</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Contents of the file speciifed in ISCAReferenceFilePath. This column is only used by MSI.</td></row>
		<row><td>ISCustomActionReference</td><td>FileType</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>file type of the file specified  ISCAReferenceFilePath. This column is only used by MSI.</td></row>
		<row><td>ISCustomActionReference</td><td>ISCAReferenceFilePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.  This column only exists in ISM.</td></row>
		<row><td>ISDIMDependency</td><td>ISDIMReference_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISDIMDependency table</td></row>
		<row><td>ISDIMDependency</td><td>RequiredBuildVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the build version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredMajorVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the major version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredMinorVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the minor version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredRevisionVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the revision version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredUUID</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>the UUID identifying the required DIM</td></row>
		<row><td>ISDIMReference</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISDIMReference</td><td>ISDIMReference</td><td>N</td><td/><td/><td>ISDIMDependency</td><td>1</td><td>Identifier</td><td/><td>This is the primary key to the ISDIMReference table</td></row>
		<row><td>ISDIMReferenceDependencies</td><td>ISDIMDependency_</td><td>N</td><td/><td/><td>ISDIMDependency</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMDependency table.</td></row>
		<row><td>ISDIMReferenceDependencies</td><td>ISDIMReference_Parent</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISDIMVariable</td><td>ISDIMReference_</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISDIMVariable</td><td>ISDIMVariable</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISDIMVariable table</td></row>
		<row><td>ISDIMVariable</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of a variable defined in the .dim file</td></row>
		<row><td>ISDIMVariable</td><td>NewValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>New value that you want to override with</td></row>
		<row><td>ISDIMVariable</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type of the variable. 0: Build Variable, 1: Runtime Variable</td></row>
		<row><td>ISDLLWrapper</td><td>EntryPoint</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the target column in the CustomAction table</td></row>
		<row><td>ISDLLWrapper</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This is column points to the source file for the DLLWrapper Custom Action</td></row>
		<row><td>ISDLLWrapper</td><td>Target</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The function signature</td></row>
		<row><td>ISDLLWrapper</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type</td></row>
		<row><td>ISDRMFile</td><td>File_</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into File table.  A null value will cause a build warning.</td></row>
		<row><td>ISDRMFile</td><td>ISDRMFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for this item.</td></row>
		<row><td>ISDRMFile</td><td>ISDRMLicense_</td><td>Y</td><td/><td/><td>ISDRMLicense</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing License that packages this file.</td></row>
		<row><td>ISDRMFile</td><td>Shell</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text indicating the activation shell used at runtime.</td></row>
		<row><td>ISDRMFileAttribute</td><td>ISDRMFile_</td><td>N</td><td/><td/><td>ISDRMFile</td><td>1</td><td>Identifier</td><td/><td>Primary foreign key into ISDRMFile table.</td></row>
		<row><td>ISDRMFileAttribute</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the attribute</td></row>
		<row><td>ISDRMFileAttribute</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The value of the attribute</td></row>
		<row><td>ISDRMLicense</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Bitwise field used to specify binary attributes of this license.</td></row>
		<row><td>ISDRMLicense</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>An internal description of this license.</td></row>
		<row><td>ISDRMLicense</td><td>ISDRMLicense</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique key identifying the license record.</td></row>
		<row><td>ISDRMLicense</td><td>LicenseNumber</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The license number.</td></row>
		<row><td>ISDRMLicense</td><td>ProjectVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The version of the project that this license is tied to.</td></row>
		<row><td>ISDRMLicense</td><td>RequestCode</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The request code.</td></row>
		<row><td>ISDRMLicense</td><td>ResponseCode</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The response code.</td></row>
		<row><td>ISDependency</td><td>Exclude</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISDependency</td><td>ISDependency</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISDisk1File</td><td>Disk</td><td>Y</td><td/><td/><td/><td/><td/><td>-1;0;1</td><td>Used to differentiate between disk1(1), last disk(-1), and other(0).</td></row>
		<row><td>ISDisk1File</td><td>ISBuildSourcePath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of file to be copied to Disk1 folder</td></row>
		<row><td>ISDisk1File</td><td>ISDisk1File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for ISDisk1File table</td></row>
		<row><td>ISDynamicFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file.</td></row>
		<row><td>ISDynamicFile</td><td>ExcludeFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Wildcards for excluded files.</td></row>
		<row><td>ISDynamicFile</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15</td><td>This is used to store Installshield custom properties of a dynamic filet.  Currently the only one is SelfRegister.</td></row>
		<row><td>ISDynamicFile</td><td>IncludeFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Wildcards for included files.</td></row>
		<row><td>ISDynamicFile</td><td>IncludeFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Include flags.</td></row>
		<row><td>ISDynamicFile</td><td>SourceFolder</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISFeatureDIMReferences</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureDIMReferences</td><td>ISDIMReference_</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>Feature_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>Language</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>ModuleID</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>ISMergeModule_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>1</td><td>Text</td><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>Language_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>2</td><td/><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureSetupPrerequisites</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureSetupPrerequisites</td><td>ISSetupPrerequisites_</td><td>N</td><td/><td/><td>ISSetupPrerequisites</td><td>1</td><td/><td/><td/></row>
		<row><td>ISFileManifests</td><td>File_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into File table.</td></row>
		<row><td>ISFileManifests</td><td>Manifest_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into File table.</td></row>
		<row><td>ISIISItem</td><td>Component_</td><td>Y</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key to Component table.</td></row>
		<row><td>ISIISItem</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localizable Item Name.</td></row>
		<row><td>ISIISItem</td><td>ISIISItem</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for each item.</td></row>
		<row><td>ISIISItem</td><td>ISIISItem_Parent</td><td>Y</td><td/><td/><td>ISIISItem</td><td>1</td><td>Identifier</td><td/><td>This record's parent record.</td></row>
		<row><td>ISIISItem</td><td>Type</td><td>N</td><td/><td/><td/><td/><td/><td/><td>IIS resource type.</td></row>
		<row><td>ISIISProperty</td><td>FriendlyName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS property name.</td></row>
		<row><td>ISIISProperty</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Flags.</td></row>
		<row><td>ISIISProperty</td><td>ISIISItem_</td><td>N</td><td/><td/><td>ISIISItem</td><td>1</td><td>Identifier</td><td/><td>Primary key for table, foreign key into ISIISItem.</td></row>
		<row><td>ISIISProperty</td><td>ISIISProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for table.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property attributes.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataProp</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property ID.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property data type.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataUserType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property user data type.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS property value.</td></row>
		<row><td>ISIISProperty</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Order sequencing.</td></row>
		<row><td>ISIISProperty</td><td>Schema</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS7 schema information.</td></row>
		<row><td>ISInstallScriptAction</td><td>EntryPoint</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the target column in the CustomAction table</td></row>
		<row><td>ISInstallScriptAction</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This is column points to the source file for the DLLWrapper Custom Action</td></row>
		<row><td>ISInstallScriptAction</td><td>Target</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The function signature</td></row>
		<row><td>ISInstallScriptAction</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type</td></row>
		<row><td>ISLanguage</td><td>ISLanguage</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the language ID.</td></row>
		<row><td>ISLanguage</td><td>Included</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1</td><td>Specify whether this language should be included.</td></row>
		<row><td>ISLinkerLibrary</td><td>ISLinkerLibrary</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for the link library.</td></row>
		<row><td>ISLinkerLibrary</td><td>Library</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the object library (.obl file).</td></row>
		<row><td>ISLinkerLibrary</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Order of the Library</td></row>
		<row><td>ISLocalControl</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>ISLocalControl</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>External key to the Binary table.</td></row>
		<row><td>ISLocalControl</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Name of the control. This name must be unique within a dialog, but can repeat on different dialogs.</td></row>
		<row><td>ISLocalControl</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>External key to the Dialog table, name of the dialog.</td></row>
		<row><td>ISLocalControl</td><td>Height</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to .rtf file for scrollable text control</td></row>
		<row><td>ISLocalControl</td><td>ISLanguage_</td><td>N</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalControl</td><td>Width</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>X</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>Y</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalDialog</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this dialog.</td></row>
		<row><td>ISLocalDialog</td><td>Dialog_</td><td>Y</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>Name of the dialog.</td></row>
		<row><td>ISLocalDialog</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the dialog.</td></row>
		<row><td>ISLocalDialog</td><td>ISLanguage_</td><td>Y</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalDialog</td><td>TextStyle_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign Key into TextStyle table, only used in Script Based Projects.</td></row>
		<row><td>ISLocalDialog</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the dialog.</td></row>
		<row><td>ISLocalRadioButton</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The height of the button.</td></row>
		<row><td>ISLocalRadioButton</td><td>ISLanguage_</td><td>N</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalRadioButton</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td>RadioButton</td><td>2</td><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ISLocalRadioButton</td><td>Property</td><td>N</td><td/><td/><td>RadioButton</td><td>1</td><td>Identifier</td><td/><td>A named property to be tied to this radio button. All the buttons tied to the same property become part of the same group.</td></row>
		<row><td>ISLocalRadioButton</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The width of the button.</td></row>
		<row><td>ISLocalRadioButton</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The horizontal coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>ISLocalRadioButton</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The vertical coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>ISLockPermissions</td><td>Attributes</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permissions attributes mask, 1==Deny access; 2==No inherit, 4==Ignore apply failures, 8==Target object is 64-bit</td></row>
		<row><td>ISLockPermissions</td><td>Domain</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Domain name for user whose permissions are being set.</td></row>
		<row><td>ISLockPermissions</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into CreateFolder, Registry, or File table</td></row>
		<row><td>ISLockPermissions</td><td>Permission</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permission Access mask.</td></row>
		<row><td>ISLockPermissions</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>CreateFolder;File;Registry</td><td>Reference to another table name</td></row>
		<row><td>ISLockPermissions</td><td>User</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>User for permissions to be set. This can be a property, hardcoded named, or SID string</td></row>
		<row><td>ISLogicalDisk</td><td>Cabinet</td><td>Y</td><td/><td/><td/><td/><td>Cabinet</td><td/><td>If some or all of the files stored on the media are compressed in a cabinet, the name of that cabinet.</td></row>
		<row><td>ISLogicalDisk</td><td>DiskId</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Primary key, integer to determine sort order for table.</td></row>
		<row><td>ISLogicalDisk</td><td>DiskPrompt</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk name: the visible text actually printed on the disk.  This will be used to prompt the user when this disk needs to be inserted.</td></row>
		<row><td>ISLogicalDisk</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISLogicalDisk</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISLogicalDisk</td><td>LastSequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the last file for this media.</td></row>
		<row><td>ISLogicalDisk</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>Property</td><td/><td>The property defining the location of the cabinet file.</td></row>
		<row><td>ISLogicalDisk</td><td>VolumeLabel</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The label attributed to the volume.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>Feature_</td><td>Y</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table,</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties, like Compressed, etc.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISLogicalDisk_</td><td>N</td><td>1</td><td>32767</td><td>ISLogicalDisk</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISLogicalDisk table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>Sequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the file for this media.</td></row>
		<row><td>ISMergeModule</td><td>Destination</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Destination.</td></row>
		<row><td>ISMergeModule</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a merge module.</td></row>
		<row><td>ISMergeModule</td><td>ISMergeModule</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The GUID identifying the merge module.</td></row>
		<row><td>ISMergeModule</td><td>Language</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Default decimal language of module.</td></row>
		<row><td>ISMergeModule</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the merge module.</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Attributes (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ContextData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>ContextData  (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>DefaultValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>DefaultValue  (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>DisplayName (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Format</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Format (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>HelpKeyword</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HelpKeyword (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>HelpLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HelpLocation (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ISMergeModule_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>1</td><td>Text</td><td/><td>The module signature, a foreign key into the ISMergeModule table</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Language_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>2</td><td/><td/><td>Default decimal language of module.</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ModuleConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Identifier, foreign key into ModuleConfiguration table (ModuleConfiguration.Name)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Type (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for this item.</td></row>
		<row><td>ISObject</td><td>Language</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObject</td><td>ObjectName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>IncludeInBuild</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Boolean, 0 for false non 0 for true</td></row>
		<row><td>ISObjectProperty</td><td>ObjectName</td><td>Y</td><td/><td/><td>ISObject</td><td>1</td><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISPatchConfigImage</td><td>PatchConfiguration_</td><td>Y</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISPatchConfigurationTable</td></row>
		<row><td>ISPatchConfigImage</td><td>UpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISUpgradedImageTable</td></row>
		<row><td>ISPatchConfiguration</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>PatchConfiguration attributes</td></row>
		<row><td>ISPatchConfiguration</td><td>CanPCDiffer</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether Product Codes may differ</td></row>
		<row><td>ISPatchConfiguration</td><td>CanPVDiffer</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether the Major Product Version may differ</td></row>
		<row><td>ISPatchConfiguration</td><td>EnablePatchCache</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to Enable Patch cacheing</td></row>
		<row><td>ISPatchConfiguration</td><td>Flags</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Patching API Flags</td></row>
		<row><td>ISPatchConfiguration</td><td>IncludeWholeFiles</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to build a binary level patch</td></row>
		<row><td>ISPatchConfiguration</td><td>LeaveDecompressed</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to leave intermediate files devcompressed when finished</td></row>
		<row><td>ISPatchConfiguration</td><td>MinMsiVersion</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Minimum Required MSI Version</td></row>
		<row><td>ISPatchConfiguration</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the Patch Configuration</td></row>
		<row><td>ISPatchConfiguration</td><td>OptimizeForSize</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to Optimize for large files</td></row>
		<row><td>ISPatchConfiguration</td><td>OutputPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Build Location</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchCacheDir</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Directory to recieve the Patch Cache information</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchGuid</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Unique Patch Identifier</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchGuidsToReplace</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List Of Patch Guids to unregister</td></row>
		<row><td>ISPatchConfiguration</td><td>TargetProductCodes</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>List Of target Product Codes</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>ISPatchConfiguration_</td><td>Y</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Name of the Patch Configuration</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the Patch Configuration Property value</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value of the Patch Configuration Property</td></row>
		<row><td>ISPatchExternalFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Filekey</td></row>
		<row><td>ISPatchExternalFile</td><td>FilePath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Filepath</td></row>
		<row><td>ISPatchExternalFile</td><td>ISUpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to the isupgraded image table</td></row>
		<row><td>ISPatchExternalFile</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Uniqu name to identify this record.</td></row>
		<row><td>ISPatchWholeFile</td><td>Component</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Component containing file key</td></row>
		<row><td>ISPatchWholeFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Key of file to be included as whole</td></row>
		<row><td>ISPatchWholeFile</td><td>UpgradedImage</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to ISUpgradedImage Table</td></row>
		<row><td>ISPathVariable</td><td>ISPathVariable</td><td>N</td><td/><td/><td/><td/><td/><td/><td>The name of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>TestValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The test value of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>Type</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;4;8</td><td>The type of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The value of the path variable.</td></row>
		<row><td>ISPowerShellWrap</td><td>Action_</td><td>N</td><td/><td/><td>CustomAction</td><td>1</td><td>Identifier</td><td/><td>Foreign key into CustomAction table</td></row>
		<row><td>ISPowerShellWrap</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Property associated with this Action</td></row>
		<row><td>ISPowerShellWrap</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value associated with this Property</td></row>
		<row><td>ISProductConfiguration</td><td>GeneratePackageCode</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td>0;1</td><td>Indicates whether or not to generate a package code.</td></row>
		<row><td>ISProductConfiguration</td><td>ISProductConfiguration</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the product configuration.</td></row>
		<row><td>ISProductConfiguration</td><td>ProductConfigurationFlags</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Product configuration (release) flags.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>InstanceId</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Identifies the instance number of this instance. This value is stored in the Property InstanceId.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Product Congiuration property name</td></row>
		<row><td>ISProductConfigurationInstance</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>ISProductConfigurationProperty</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISProductConfigurationProperty</td><td>Property</td><td>N</td><td/><td/><td>Property</td><td>1</td><td>Text</td><td/><td>Product Congiuration property name</td></row>
		<row><td>ISProductConfigurationProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property. Never null or empty.</td></row>
		<row><td>ISRelease</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding boolean values for various release attributes.</td></row>
		<row><td>ISRelease</td><td>BuildLocation</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Build location.</td></row>
		<row><td>ISRelease</td><td>CDBrowser</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Demoshield browser location.</td></row>
		<row><td>ISRelease</td><td>DefaultLanguage</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Default language for setup.</td></row>
		<row><td>ISRelease</td><td>DigitalPVK</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing private key (.pvk) file.</td></row>
		<row><td>ISRelease</td><td>DigitalSPC</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing Software Publisher Certificate (.spc) file.</td></row>
		<row><td>ISRelease</td><td>DigitalURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing URL.</td></row>
		<row><td>ISRelease</td><td>DiskClusterSize</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Disk cluster size.</td></row>
		<row><td>ISRelease</td><td>DiskSize</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk size.</td></row>
		<row><td>ISRelease</td><td>DiskSizeUnit</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2</td><td>Disk size units (KB or MB).</td></row>
		<row><td>ISRelease</td><td>DiskSpanning</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2</td><td>Disk spanning (automatic, enforce size, etc.).</td></row>
		<row><td>ISRelease</td><td>DotNetBuildConfiguration</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Build Configuration for .NET solutions.</td></row>
		<row><td>ISRelease</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISRelease</td><td>ISRelease</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISRelease</td><td>ISSetupPrerequisiteLocation</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3</td><td>Location the Setup Prerequisites will be placed in</td></row>
		<row><td>ISRelease</td><td>MediaLocation</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Media location on disk.</td></row>
		<row><td>ISRelease</td><td>MsiCommandLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command line passed to the msi package from setup.exe</td></row>
		<row><td>ISRelease</td><td>MsiSourceType</td><td>N</td><td>-1</td><td>4</td><td/><td/><td/><td/><td>MSI media source type.</td></row>
		<row><td>ISRelease</td><td>PackageName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Package name.</td></row>
		<row><td>ISRelease</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Password.</td></row>
		<row><td>ISRelease</td><td>Platforms</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Platforms supported (Intel, Alpha, etc.).</td></row>
		<row><td>ISRelease</td><td>ReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Release flags.</td></row>
		<row><td>ISRelease</td><td>ReleaseType</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;4</td><td>Release type (single, uncompressed, etc.).</td></row>
		<row><td>ISRelease</td><td>SupportedLanguagesData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Languages supported (for component filtering).</td></row>
		<row><td>ISRelease</td><td>SupportedLanguagesUI</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>UI languages supported.</td></row>
		<row><td>ISRelease</td><td>SupportedOSs</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Indicate which operating systmes are supported.</td></row>
		<row><td>ISRelease</td><td>SynchMsi</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>MSI file to synchronize file keys and other data with (patch-like functionality).</td></row>
		<row><td>ISRelease</td><td>Type</td><td>N</td><td>0</td><td>6</td><td/><td/><td/><td/><td>Release type (CDROM, Network, etc.).</td></row>
		<row><td>ISRelease</td><td>URLLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Media location via URL.</td></row>
		<row><td>ISRelease</td><td>VersionCopyright</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Version stamp information.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>AS Repository property name</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>AS Repository property value</td></row>
		<row><td>ISReleaseExtended</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding boolean values for various release attributes.</td></row>
		<row><td>ISReleaseExtended</td><td>CertPassword</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital certificate password</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificateDBaseNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to cerificate database for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificateIdNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to cerificate ID for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificatePasswordNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Password for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetBaseLanguage</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Base Languge of .NET Redist</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetFxCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to DotNetFx.exe</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetLangPackCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to LangPack.exe</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetLangaugePacks</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>.NET Redist language packs to include</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetRedistLocation</td><td>Y</td><td>0</td><td>3</td><td/><td/><td/><td/><td>Location of .NET framework Redist (Web, SetupExe, Source, None)</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetRedistURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to .NET framework Redist</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetVersion</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Version of .NET framework Redist (1.0, 1.1)</td></row>
		<row><td>ISReleaseExtended</td><td>EngineLocation</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Location of msi engine (Web, SetupExe...)</td></row>
		<row><td>ISReleaseExtended</td><td>ISEngineLocation</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Location of ISScript  engine (Web, SetupExe...)</td></row>
		<row><td>ISReleaseExtended</td><td>ISEngineURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to InstallShield scripting engine</td></row>
		<row><td>ISReleaseExtended</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleaseExtended</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISReleaseExtended</td><td>JSharpCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to vjredist.exe</td></row>
		<row><td>ISReleaseExtended</td><td>JSharpRedistLocation</td><td>Y</td><td>0</td><td>3</td><td/><td/><td/><td/><td>Location of J# framework Redist (Web, SetupExe, Source, None)</td></row>
		<row><td>ISReleaseExtended</td><td>MsiEngineVersion</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding selected MSI engine versions included in this release</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickCabName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File name of generated cabfile</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickHtmlName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File name of generated html page</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickTargetBrowser</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Target browser (IE, Netscape, both...)</td></row>
		<row><td>ISReleaseExtended</td><td>WebCabSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of the cabfile</td></row>
		<row><td>ISReleaseExtended</td><td>WebLocalCachePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Directory to cache downloaded package</td></row>
		<row><td>ISReleaseExtended</td><td>WebType</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Type of web install (One Executable, Downloader...)</td></row>
		<row><td>ISReleaseExtended</td><td>WebURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to .msi package</td></row>
		<row><td>ISReleaseExtended</td><td>Win9xMsiUrl</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to Ansi MSI engine</td></row>
		<row><td>ISReleaseExtended</td><td>WinMsi30Url</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to MSI 3.0 engine</td></row>
		<row><td>ISReleaseExtended</td><td>WinNTMsiUrl</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to Unicode MSI engine</td></row>
		<row><td>ISReleaseProperty</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISProductConfiguration table.</td></row>
		<row><td>ISReleaseProperty</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISRelease table.</td></row>
		<row><td>ISReleaseProperty</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property name</td></row>
		<row><td>ISReleaseProperty</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISReleasePublishInfo</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item description</td></row>
		<row><td>ISReleasePublishInfo</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item display name</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding various attributes</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISReleasePublishInfo</td><td>Publisher</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item publisher</td></row>
		<row><td>ISReleasePublishInfo</td><td>Repository</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository which to  publish the built merge module</td></row>
		<row><td>ISSQLConnection</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Authentication</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>BatchSeparator</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>CmdTimeout</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Database</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ISSQLConnection</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLConnection record.</td></row>
		<row><td>ISSQLConnection</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Password</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ScriptVersion_Column</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ScriptVersion_Table</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Server</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>UserName</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLConnectionDBServer</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLConnectionDBServer record.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLDBMetaData_</td><td>N</td><td/><td/><td>ISSQLDBMetaData</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLDBMetaData table.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnectionScript</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLConnectionScript</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLConnectionScript</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnAdditional</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnDatabase</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnDriver</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnNetLibrary</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnPassword</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnPort</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnServer</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnUserID</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnWindowsSecurity</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoDriverName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>CreateDbCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>CreateTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>DsnODBCName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ISSQLDBMetaData</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLDBMetaData record.</td></row>
		<row><td>ISSQLDBMetaData</td><td>InsertRecordCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>LocalInstanceNames</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>QueryDatabasesCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_Column</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_ColumnType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_Table</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>SelectTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>SwitchDbCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestDatabaseCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestTableCmd2</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionBeginToken</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionEndToken</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionInfoCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>WinAuthentUserId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>ISSQLConnectionDBServer_</td><td>Y</td><td/><td/><td>ISSQLConnectionDBServer</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnectionDBServer table.</td></row>
		<row><td>ISSQLRequirement</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLRequirement</td><td>ISSQLRequirement</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLRequirement record.</td></row>
		<row><td>ISSQLRequirement</td><td>MajorVersion</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>ServicePackLevel</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ErrHandling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ErrNumber</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ISSQLScriptFile_</td><td>Y</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table</td></row>
		<row><td>ISSQLScriptError</td><td>Message</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Custom end-user message. Reserved for future use.</td></row>
		<row><td>ISSQLScriptFile</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Comments</td></row>
		<row><td>ISSQLScriptFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the SQL script.</td></row>
		<row><td>ISSQLScriptFile</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A conditional statement that will disable this script if the specified condition evaluates to the 'False' state. If a script is disabled, it will not be installed regardless of the 'Action' state associated with the component.</td></row>
		<row><td>ISSQLScriptFile</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Display name for the SQL script file.</td></row>
		<row><td>ISSQLScriptFile</td><td>ErrorHandling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISSQLScriptFile</td><td>ISSQLScriptFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISSQLScriptFile table</td></row>
		<row><td>ISSQLScriptFile</td><td>InstallText</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Feedback end-user text at install</td></row>
		<row><td>ISSQLScriptFile</td><td>Scheduling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>UninstallText</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Feedback end-user text at Uninstall</td></row>
		<row><td>ISSQLScriptFile</td><td>Version</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Schema Version (#####.#####.#####.#####)</td></row>
		<row><td>ISSQLScriptImport</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Authentication</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Database</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>ExcludeTables</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLScriptImport</td><td>IncludeTables</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Server</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>UserName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLScriptReplace</td><td>ISSQLScriptReplace</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLScriptReplace record.</td></row>
		<row><td>ISSQLScriptReplace</td><td>Replace</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>Search</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISScriptFile</td><td>ISScriptFile</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the full path of the script file. The path portion may be expressed in path variable form.</td></row>
		<row><td>ISSelfReg</td><td>CmdLine</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSelfReg</td><td>Cost</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSelfReg</td><td>FileKey</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key to the file table</td></row>
		<row><td>ISSelfReg</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupFile</td><td>FileName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the file name to use when streaming the file to the support files location</td></row>
		<row><td>ISSetupFile</td><td>ISSetupFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISSetupFile table</td></row>
		<row><td>ISSetupFile</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Four digit language identifier.  0 for Language Neutral</td></row>
		<row><td>ISSetupFile</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Link to the source file on the build machine</td></row>
		<row><td>ISSetupFile</td><td>Splash</td><td>Y</td><td/><td/><td/><td/><td>Short</td><td/><td>Boolean value indication whether his setup file entry belongs in the Splasc Screen section</td></row>
		<row><td>ISSetupFile</td><td>Stream</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The bits to stream to the support location</td></row>
		<row><td>ISSetupPrerequisites</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupPrerequisites</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Release Flags that specify whether this prereq  will be included in a particular release.</td></row>
		<row><td>ISSetupPrerequisites</td><td>ISSetupLocation</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2</td><td/></row>
		<row><td>ISSetupPrerequisites</td><td>ISSetupPrerequisites</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupPrerequisites</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupType</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>ISSetupType</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Longer descriptive text describing a visible feature item.</td></row>
		<row><td>ISSetupType</td><td>Display</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Numeric sort order, used to force a specific display ordering.</td></row>
		<row><td>ISSetupType</td><td>Display_Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>ISSetupType</td><td>ISSetupType</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular feature record.</td></row>
		<row><td>ISSetupTypeFeatures</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISSetupTypeFeatures</td><td>ISSetupType_</td><td>N</td><td/><td/><td>ISSetupType</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSetupType table.</td></row>
		<row><td>ISStorages</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Path to the file to stream into sub-storage</td></row>
		<row><td>ISStorages</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Name of the sub-storage key</td></row>
		<row><td>ISString</td><td>Comment</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Comment</td></row>
		<row><td>ISString</td><td>Encoded</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Encoding for multi-byte strings.</td></row>
		<row><td>ISString</td><td>ISLanguage_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISString</td><td>ISString</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>String id.</td></row>
		<row><td>ISString</td><td>TimeStamp</td><td>Y</td><td/><td/><td/><td/><td>Time/Date</td><td/><td>Time Stamp. MSI's Time/Date column type is just an int, with bits packed in a certain order.</td></row>
		<row><td>ISString</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>real string value.</td></row>
		<row><td>ISSwidtagProperty</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISSwidtagProperty</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Property value</td></row>
		<row><td>ISTargetImage</td><td>Flags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>relative order of the target image</td></row>
		<row><td>ISTargetImage</td><td>IgnoreMissingFiles</td><td>N</td><td/><td/><td/><td/><td/><td/><td>If true, ignore missing source files when creating patch</td></row>
		<row><td>ISTargetImage</td><td>MsiPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to the target image</td></row>
		<row><td>ISTargetImage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the TargetImage</td></row>
		<row><td>ISTargetImage</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td>relative order of the target image</td></row>
		<row><td>ISTargetImage</td><td>UpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>foreign key to the upgraded Image table</td></row>
		<row><td>ISUpgradeMsiItem</td><td>ISAttributes</td><td>N</td><td/><td/><td/><td/><td/><td>0;1</td><td/></row>
		<row><td>ISUpgradeMsiItem</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISUpgradeMsiItem</td><td>ObjectSetupPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The path to the setup you want to upgrade.</td></row>
		<row><td>ISUpgradeMsiItem</td><td>UpgradeItem</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the Upgrade Item.</td></row>
		<row><td>ISUpgradedImage</td><td>Family</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the image family</td></row>
		<row><td>ISUpgradedImage</td><td>MsiPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to the upgraded image</td></row>
		<row><td>ISUpgradedImage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the UpgradedImage</td></row>
		<row><td>ISVirtualDirectory</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Directory table.</td></row>
		<row><td>ISVirtualDirectory</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualDirectory</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into File  table.</td></row>
		<row><td>ISVirtualFile</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualFile</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualPackage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualPackage</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualRegistry</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualRegistry</td><td>Registry_</td><td>N</td><td/><td/><td>Registry</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Registry table.</td></row>
		<row><td>ISVirtualRegistry</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualRelease</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISProductConfiguration table.</td></row>
		<row><td>ISVirtualRelease</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISRelease table.</td></row>
		<row><td>ISVirtualRelease</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property name</td></row>
		<row><td>ISVirtualRelease</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualShortcut</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualShortcut</td><td>Shortcut_</td><td>N</td><td/><td/><td>Shortcut</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Shortcut table.</td></row>
		<row><td>ISVirtualShortcut</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISXmlElement</td><td>Content</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Element contents</td></row>
		<row><td>ISXmlElement</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML element attributes</td></row>
		<row><td>ISXmlElement</td><td>ISXmlElement</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized, internal token for Xml element</td></row>
		<row><td>ISXmlElement</td><td>ISXmlElement_Parent</td><td>Y</td><td/><td/><td>ISXmlElement</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISXMLElement table.</td></row>
		<row><td>ISXmlElement</td><td>ISXmlFile_</td><td>N</td><td/><td/><td>ISXmlFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into XmlFile table.</td></row>
		<row><td>ISXmlElement</td><td>XPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>XPath fragment including any operators</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML elementattib attributes</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISXmlElementAttrib</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized, internal token for Xml element attribute</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISXmlElement_</td><td>N</td><td/><td/><td>ISXmlElement</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISXMLElement table.</td></row>
		<row><td>ISXmlElementAttrib</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized attribute name</td></row>
		<row><td>ISXmlElementAttrib</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized attribute value</td></row>
		<row><td>ISXmlFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>ISXmlFile</td><td>Directory</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Directory table.</td></row>
		<row><td>ISXmlFile</td><td>Encoding</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>XML File Encoding</td></row>
		<row><td>ISXmlFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized XML file name</td></row>
		<row><td>ISXmlFile</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML file attributes</td></row>
		<row><td>ISXmlFile</td><td>ISXmlFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized,internal token for Xml file</td></row>
		<row><td>ISXmlFile</td><td>SelectionNamespaces</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Selection namespaces</td></row>
		<row><td>ISXmlLocator</td><td>Attribute</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>The name of an attribute within the XML element.</td></row>
		<row><td>ISXmlLocator</td><td>Element</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>XPath query that will locate an element in an XML file.</td></row>
		<row><td>ISXmlLocator</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2</td><td/></row>
		<row><td>ISXmlLocator</td><td>Parent</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The parent file signature. It is also a foreign key in the Signature table.</td></row>
		<row><td>ISXmlLocator</td><td>Signature_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, ISXmlLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>Icon</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>Icon</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>Icon</td><td>ISIconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index to be extracted.</td></row>
		<row><td>Icon</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key. Name of the icon file.</td></row>
		<row><td>IniFile</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;3</td><td>The type of modification to be made, one of iifEnum</td></row>
		<row><td>IniFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the .INI value.</td></row>
		<row><td>IniFile</td><td>DirProperty</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the .INI file is.</td></row>
		<row><td>IniFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name in which to write the information</td></row>
		<row><td>IniFile</td><td>IniFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>IniFile</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file key below Section.</td></row>
		<row><td>IniFile</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file Section.</td></row>
		<row><td>IniFile</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to be written.</td></row>
		<row><td>IniLocator</td><td>Field</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The field in the .INI line. If Field is null or 0 the entire line is read.</td></row>
		<row><td>IniLocator</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name.</td></row>
		<row><td>IniLocator</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Key value (followed by an equals sign in INI file).</td></row>
		<row><td>IniLocator</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Section name within in file (within square brackets in INI file).</td></row>
		<row><td>IniLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>IniLocator</td><td>Type</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>An integer value that determines if the .INI value read is a filename or a directory location or to be used as is w/o interpretation.</td></row>
		<row><td>InstallExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>InstallExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>InstallExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>InstallExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>InstallExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>InstallShield</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property, uppercase if settable by launcher or loader.</td></row>
		<row><td>InstallShield</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>InstallUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>InstallUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>InstallUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>InstallUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>InstallUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>IsolatedComponent</td><td>Component_Application</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Key to Component table item for application</td></row>
		<row><td>IsolatedComponent</td><td>Component_Shared</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Key to Component table item to be isolated</td></row>
		<row><td>LaunchCondition</td><td>Condition</td><td>N</td><td/><td/><td/><td/><td>Condition</td><td/><td>Expression which must evaluate to TRUE in order for install to commence.</td></row>
		<row><td>LaunchCondition</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Localizable text to display when condition fails and install must abort.</td></row>
		<row><td>ListBox</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ListBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same listbox.</td></row>
		<row><td>ListBox</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ListBox</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>ListView</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>The name of the icon to be displayed with the icon. The binary information is looked up from the Binary Table.</td></row>
		<row><td>ListView</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ListView</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same listview.</td></row>
		<row><td>ListView</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ListView</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>LockPermissions</td><td>Domain</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Domain name for user whose permissions are being set. (usually a property)</td></row>
		<row><td>LockPermissions</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Registry or File table</td></row>
		<row><td>LockPermissions</td><td>Permission</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permission Access mask.  Full Control = 268435456 (GENERIC_ALL = 0x10000000)</td></row>
		<row><td>LockPermissions</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>Directory;File;Registry</td><td>Reference to another table name</td></row>
		<row><td>LockPermissions</td><td>User</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>User for permissions to be set.  (usually a property)</td></row>
		<row><td>MIME</td><td>CLSID</td><td>Y</td><td/><td/><td>Class</td><td>1</td><td>Guid</td><td/><td>Optional associated CLSID.</td></row>
		<row><td>MIME</td><td>ContentType</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Primary key. Context identifier, typically "type/format".</td></row>
		<row><td>MIME</td><td>Extension_</td><td>N</td><td/><td/><td>Extension</td><td>1</td><td>Text</td><td/><td>Optional associated extension (without dot)</td></row>
		<row><td>Media</td><td>Cabinet</td><td>Y</td><td/><td/><td/><td/><td>Cabinet</td><td/><td>If some or all of the files stored on the media are compressed in a cabinet, the name of that cabinet.</td></row>
		<row><td>Media</td><td>DiskId</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Primary key, integer to determine sort order for table.</td></row>
		<row><td>Media</td><td>DiskPrompt</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk name: the visible text actually printed on the disk.  This will be used to prompt the user when this disk needs to be inserted.</td></row>
		<row><td>Media</td><td>LastSequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the last file for this media.</td></row>
		<row><td>Media</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>Property</td><td/><td>The property defining the location of the cabinet file.</td></row>
		<row><td>Media</td><td>VolumeLabel</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The label attributed to the volume.</td></row>
		<row><td>MoveFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>If this component is not "selected" for installation or removal, no action will be taken on the associated MoveFile entry</td></row>
		<row><td>MoveFile</td><td>DestFolder</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the destination directory</td></row>
		<row><td>MoveFile</td><td>DestName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name to be given to the original file after it is moved or copied.  If blank, the destination file will be given the same name as the source file</td></row>
		<row><td>MoveFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key that uniquely identifies a particular MoveFile record</td></row>
		<row><td>MoveFile</td><td>Options</td><td>N</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Integer value specifying the MoveFile operating mode, one of imfoEnum</td></row>
		<row><td>MoveFile</td><td>SourceFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the source directory</td></row>
		<row><td>MoveFile</td><td>SourceName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the source file(s) to be moved or copied.  Can contain the '*' or '?' wildcards.</td></row>
		<row><td>MsiAssembly</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Assembly attributes</td></row>
		<row><td>MsiAssembly</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>MsiAssembly</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>MsiAssembly</td><td>File_Application</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into File table, denoting the application context for private assemblies. Null for global assemblies.</td></row>
		<row><td>MsiAssembly</td><td>File_Manifest</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the manifest file for the assembly.</td></row>
		<row><td>MsiAssemblyName</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>MsiAssemblyName</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name part of the name-value pairs for the assembly name.</td></row>
		<row><td>MsiAssemblyName</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The value part of the name-value pairs for the assembly name.</td></row>
		<row><td>MsiDigitalCertificate</td><td>CertData</td><td>N</td><td/><td/><td/><td/><td>Binary</td><td/><td>A certificate context blob for a signer certificate</td></row>
		<row><td>MsiDigitalCertificate</td><td>DigitalCertificate</td><td>N</td><td/><td/><td>MsiPackageCertificate</td><td>2</td><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiDigitalSignature</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td>MsiDigitalCertificate</td><td>1</td><td>Identifier</td><td/><td>Foreign key to MsiDigitalCertificate table identifying the signer certificate</td></row>
		<row><td>MsiDigitalSignature</td><td>Hash</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>The encoded hash blob from the digital signature</td></row>
		<row><td>MsiDigitalSignature</td><td>SignObject</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key to Media table</td></row>
		<row><td>MsiDigitalSignature</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Reference to another table name (only Media table is supported)</td></row>
		<row><td>MsiDriverPackages</td><td>Component</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>MsiDriverPackages</td><td>Flags</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Driver package flags</td></row>
		<row><td>MsiDriverPackages</td><td>ReferenceComponents</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiDriverPackages</td><td>Sequence</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Installation sequence number</td></row>
		<row><td>MsiEmbeddedChainer</td><td>CommandLine</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>MsiEmbeddedChainer</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>CustomSource</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td>Integer</td><td>2;18;50</td><td/></row>
		<row><td>MsiEmbeddedUI</td><td>Attributes</td><td>N</td><td>0</td><td>3</td><td/><td/><td>Integer</td><td/><td>Information about the data in the Data column.</td></row>
		<row><td>MsiEmbeddedUI</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>This column contains binary information.</td></row>
		<row><td>MsiEmbeddedUI</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Filename</td><td/><td>The name of the file that receives the binary information in the Data column.</td></row>
		<row><td>MsiEmbeddedUI</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>MsiEmbeddedUI</td><td>MessageFilter</td><td>Y</td><td>0</td><td>234913791</td><td/><td/><td>Integer</td><td/><td>Specifies the types of messages that are sent to the user interface DLL. This column is only relevant for rows with the msidbEmbeddedUI attribute.</td></row>
		<row><td>MsiEmbeddedUI</td><td>MsiEmbeddedUI</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The primary key for the table.</td></row>
		<row><td>MsiFileHash</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, foreign key into File table referencing file with this hash</td></row>
		<row><td>MsiFileHash</td><td>HashPart1</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart2</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart3</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart4</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>Options</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Various options and attributes for this hash.</td></row>
		<row><td>MsiLockPermissionsEx</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Expression which must evaluate to TRUE in order for this set of permissions to be applied</td></row>
		<row><td>MsiLockPermissionsEx</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Registry, File, CreateFolder, or ServiceInstall table</td></row>
		<row><td>MsiLockPermissionsEx</td><td>MsiLockPermissionsEx</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiLockPermissionsEx</td><td>SDDLText</td><td>N</td><td/><td/><td/><td/><td>FormattedSDDLText</td><td/><td>String to indicate permissions to be applied to the LockObject</td></row>
		<row><td>MsiLockPermissionsEx</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>CreateFolder;File;Registry;ServiceInstall</td><td>Reference to another table name</td></row>
		<row><td>MsiPackageCertificate</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A foreign key to the digital certificate table</td></row>
		<row><td>MsiPackageCertificate</td><td>PackageCertificate</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiPatchCertificate</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td>MsiDigitalCertificate</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the digital certificate table</td></row>
		<row><td>MsiPatchCertificate</td><td>PatchCertificate</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiPatchMetadata</td><td>Company</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Optional company name</td></row>
		<row><td>MsiPatchMetadata</td><td>PatchConfiguration_</td><td>N</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISPatchConfiguration table</td></row>
		<row><td>MsiPatchMetadata</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the metadata</td></row>
		<row><td>MsiPatchMetadata</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value of the metadata</td></row>
		<row><td>MsiPatchOldAssemblyFile</td><td>Assembly_</td><td>Y</td><td/><td/><td>MsiPatchOldAssemblyName</td><td>1</td><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Assembly</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchSequence</td><td>PatchConfiguration_</td><td>N</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the patch configuration table</td></row>
		<row><td>MsiPatchSequence</td><td>PatchFamily</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the family to which this patch belongs</td></row>
		<row><td>MsiPatchSequence</td><td>Sequence</td><td>N</td><td/><td/><td/><td/><td>Version</td><td/><td>The version of this patch in this family</td></row>
		<row><td>MsiPatchSequence</td><td>Supersede</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td>Supersede</td></row>
		<row><td>MsiPatchSequence</td><td>Target</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Target product codes for this patch family</td></row>
		<row><td>MsiServiceConfig</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Argument(s) for service configuration. Value depends on the content of the ConfigType field</td></row>
		<row><td>MsiServiceConfig</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the configuration of the service</td></row>
		<row><td>MsiServiceConfig</td><td>ConfigType</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Service Configuration Option</td></row>
		<row><td>MsiServiceConfig</td><td>Event</td><td>N</td><td>0</td><td>7</td><td/><td/><td/><td/><td>Bit field:   0x1 = Install, 0x2 = Uninstall, 0x4 = Reinstall</td></row>
		<row><td>MsiServiceConfig</td><td>MsiServiceConfig</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>MsiServiceConfig</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Actions</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A list of integer actions separated by [~] delimiters: 0 = SC_ACTION_NONE, 1 = SC_ACTION_RESTART, 2 = SC_ACTION_REBOOT, 3 = SC_ACTION_RUN_COMMAND. Terminate with [~][~]</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Command</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Command line of the process to CreateProcess function to execute</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the configuration of the service</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>DelayActions</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A list of delays (time in milli-seconds), separated by [~] delmiters, to wait before taking the corresponding Action. Terminate with [~][~]</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Event</td><td>N</td><td>0</td><td>7</td><td/><td/><td/><td/><td>Bit field:   0x1 = Install, 0x2 = Uninstall, 0x4 = Reinstall</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>MsiServiceConfigFailureActions</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>RebootMessage</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Message to be broadcast to server users before rebooting</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>ResetPeriod</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Time in seconds after which to reset the failure count to zero. Leave blank if it should never be reset</td></row>
		<row><td>MsiShortcutProperty</td><td>MsiShortcutProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiShortcutProperty</td><td>PropVariantValue</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>String representation of the value in the property</td></row>
		<row><td>MsiShortcutProperty</td><td>PropertyKey</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Canonical string representation of the Property Key being set</td></row>
		<row><td>MsiShortcutProperty</td><td>Shortcut_</td><td>N</td><td/><td/><td>Shortcut</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Shortcut table</td></row>
		<row><td>ODBCAttribute</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of ODBC driver attribute</td></row>
		<row><td>ODBCAttribute</td><td>Driver_</td><td>N</td><td/><td/><td>ODBCDriver</td><td>1</td><td>Identifier</td><td/><td>Reference to ODBC driver in ODBCDriver table</td></row>
		<row><td>ODBCAttribute</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for ODBC driver attribute</td></row>
		<row><td>ODBCDataSource</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCDataSource</td><td>DataSource</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for data source</td></row>
		<row><td>ODBCDataSource</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for data source</td></row>
		<row><td>ODBCDataSource</td><td>DriverDescription</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Reference to driver description, may be existing driver</td></row>
		<row><td>ODBCDataSource</td><td>Registration</td><td>N</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Registration option: 0=machine, 1=user, others t.b.d.</td></row>
		<row><td>ODBCDriver</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCDriver</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for driver, non-localized</td></row>
		<row><td>ODBCDriver</td><td>Driver</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for driver</td></row>
		<row><td>ODBCDriver</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Reference to key driver file</td></row>
		<row><td>ODBCDriver</td><td>File_Setup</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Optional reference to key driver setup DLL</td></row>
		<row><td>ODBCSourceAttribute</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of ODBC data source attribute</td></row>
		<row><td>ODBCSourceAttribute</td><td>DataSource_</td><td>N</td><td/><td/><td>ODBCDataSource</td><td>1</td><td>Identifier</td><td/><td>Reference to ODBC data source in ODBCDataSource table</td></row>
		<row><td>ODBCSourceAttribute</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for ODBC data source attribute</td></row>
		<row><td>ODBCTranslator</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCTranslator</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for translator</td></row>
		<row><td>ODBCTranslator</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Reference to key translator file</td></row>
		<row><td>ODBCTranslator</td><td>File_Setup</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Optional reference to key translator setup DLL</td></row>
		<row><td>ODBCTranslator</td><td>Translator</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for translator</td></row>
		<row><td>Patch</td><td>Attributes</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer containing bit flags representing patch attributes</td></row>
		<row><td>Patch</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, non-localized token, foreign key to File table, must match identifier in cabinet.</td></row>
		<row><td>Patch</td><td>Header</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The patch header, used for patch validation.</td></row>
		<row><td>Patch</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to patch header.</td></row>
		<row><td>Patch</td><td>PatchSize</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of patch in bytes (long integer).</td></row>
		<row><td>Patch</td><td>Sequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Primary key, sequence with respect to the media images; order must track cabinet order.</td></row>
		<row><td>Patch</td><td>StreamRef_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>External key into the MsiPatchHeaders table specifying the row that contains the patch header stream.</td></row>
		<row><td>PatchPackage</td><td>Media_</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Foreign key to DiskId column of Media table. Indicates the disk containing the patch package.</td></row>
		<row><td>PatchPackage</td><td>PatchId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A unique string GUID representing this patch.</td></row>
		<row><td>ProgId</td><td>Class_</td><td>Y</td><td/><td/><td>Class</td><td>1</td><td>Guid</td><td/><td>The CLSID of an OLE factory corresponding to the ProgId.</td></row>
		<row><td>ProgId</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description for the Program identifier.</td></row>
		<row><td>ProgId</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a component, like ExtractIcon, etc.</td></row>
		<row><td>ProgId</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index.</td></row>
		<row><td>ProgId</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Optional foreign key into the Icon Table, specifying the icon file associated with this ProgId. Will be written under the DefaultIcon key.</td></row>
		<row><td>ProgId</td><td>ProgId</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The Program Identifier. Primary key.</td></row>
		<row><td>ProgId</td><td>ProgId_Parent</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>The Parent Program Identifier. If specified, the ProgId column becomes a version independent prog id.</td></row>
		<row><td>Property</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>Property</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property, uppercase if settable by launcher or loader.</td></row>
		<row><td>Property</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>PublishComponent</td><td>AppData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is localisable Application specific data that can be associated with a Qualified Component.</td></row>
		<row><td>PublishComponent</td><td>ComponentId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID that represents the component id that will be requested by the alien product.</td></row>
		<row><td>PublishComponent</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table.</td></row>
		<row><td>PublishComponent</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Feature table.</td></row>
		<row><td>PublishComponent</td><td>Qualifier</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is defined only when the ComponentId column is an Qualified Component Id. This is the Qualifier for ProvideComponentIndirect.</td></row>
		<row><td>RadioButton</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The height of the button.</td></row>
		<row><td>RadioButton</td><td>Help</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The help strings used with the button. The text is optional.</td></row>
		<row><td>RadioButton</td><td>ISControlId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A number used to represent the control ID of the Control, Used in Dialog export</td></row>
		<row><td>RadioButton</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>RadioButton</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this radio button. All the buttons tied to the same property become part of the same group.</td></row>
		<row><td>RadioButton</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible title to be assigned to the radio button.</td></row>
		<row><td>RadioButton</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this button. Selecting the button will set the associated property to this value.</td></row>
		<row><td>RadioButton</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The width of the button.</td></row>
		<row><td>RadioButton</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The horizontal coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>RadioButton</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The vertical coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>RegLocator</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>RegLocator</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>RegLocator</td><td>Root</td><td>N</td><td>0</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum.</td></row>
		<row><td>RegLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table. If the type is 0, the registry values refers a directory, and _Signature is not a foreign key.</td></row>
		<row><td>RegLocator</td><td>Type</td><td>Y</td><td>0</td><td>18</td><td/><td/><td/><td/><td>An integer value that determines if the registry value is a filename or a directory location or to be used as is w/o interpretation.</td></row>
		<row><td>Registry</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the registry value.</td></row>
		<row><td>Registry</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a registry item.  Currently the only one is Automatic.</td></row>
		<row><td>Registry</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>Registry</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>Registry</td><td>Registry</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>Registry</td><td>Root</td><td>N</td><td>-1</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum.</td></row>
		<row><td>Registry</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The registry value.</td></row>
		<row><td>RemoveFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>DirProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full pathname to the folder of the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular file entry</td></row>
		<row><td>RemoveFile</td><td>FileName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>InstallMode</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;3</td><td>Installation option, one of iimEnum.</td></row>
		<row><td>RemoveIniFile</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>2;4</td><td>The type of modification to be made, one of iifEnum.</td></row>
		<row><td>RemoveIniFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the deletion of the .INI value.</td></row>
		<row><td>RemoveIniFile</td><td>DirProperty</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the .INI file is.</td></row>
		<row><td>RemoveIniFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name in which to delete the information</td></row>
		<row><td>RemoveIniFile</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file key below Section.</td></row>
		<row><td>RemoveIniFile</td><td>RemoveIniFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>RemoveIniFile</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file Section.</td></row>
		<row><td>RemoveIniFile</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to be deleted. The value is required when Action is iifIniRemoveTag</td></row>
		<row><td>RemoveRegistry</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the deletion of the registry value.</td></row>
		<row><td>RemoveRegistry</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>RemoveRegistry</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>RemoveRegistry</td><td>RemoveRegistry</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>RemoveRegistry</td><td>Root</td><td>N</td><td>-1</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum</td></row>
		<row><td>ReserveCost</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reserve a specified amount of space if this component is to be installed.</td></row>
		<row><td>ReserveCost</td><td>ReserveFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the destination directory</td></row>
		<row><td>ReserveCost</td><td>ReserveKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key that uniquely identifies a particular ReserveCost record</td></row>
		<row><td>ReserveCost</td><td>ReserveLocal</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Disk space to reserve if linked component is installed locally.</td></row>
		<row><td>ReserveCost</td><td>ReserveSource</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Disk space to reserve if linked component is installed to run from the source location.</td></row>
		<row><td>SFPCatalog</td><td>Catalog</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>SFP Catalog</td></row>
		<row><td>SFPCatalog</td><td>Dependency</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Parent catalog - only used by SFP</td></row>
		<row><td>SFPCatalog</td><td>SFPCatalog</td><td>N</td><td/><td/><td/><td/><td>Filename</td><td/><td>File name for the catalog.</td></row>
		<row><td>SelfReg</td><td>Cost</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The cost of registering the module.</td></row>
		<row><td>SelfReg</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the module that needs to be registered.</td></row>
		<row><td>ServiceControl</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Arguments for the service.  Separate by [~].</td></row>
		<row><td>ServiceControl</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the startup of the service</td></row>
		<row><td>ServiceControl</td><td>Event</td><td>N</td><td>0</td><td>187</td><td/><td/><td/><td/><td>Bit field:  Install:  0x1 = Start, 0x2 = Stop, 0x8 = Delete, Uninstall: 0x10 = Start, 0x20 = Stop, 0x80 = Delete</td></row>
		<row><td>ServiceControl</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>ServiceControl</td><td>ServiceControl</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>ServiceControl</td><td>Wait</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Boolean for whether to wait for the service to fully start</td></row>
		<row><td>ServiceInstall</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Arguments to include in every start of the service, passed to WinMain</td></row>
		<row><td>ServiceInstall</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the startup of the service</td></row>
		<row><td>ServiceInstall</td><td>Dependencies</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Other services this depends on to start.  Separate by [~], and end with [~][~]</td></row>
		<row><td>ServiceInstall</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of service.</td></row>
		<row><td>ServiceInstall</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>External Name of the Service</td></row>
		<row><td>ServiceInstall</td><td>ErrorControl</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Severity of error if service fails to start</td></row>
		<row><td>ServiceInstall</td><td>LoadOrderGroup</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>LoadOrderGroup</td></row>
		<row><td>ServiceInstall</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Internal Name of the Service</td></row>
		<row><td>ServiceInstall</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>password to run service with.  (with StartName)</td></row>
		<row><td>ServiceInstall</td><td>ServiceInstall</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>ServiceInstall</td><td>ServiceType</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Type of the service</td></row>
		<row><td>ServiceInstall</td><td>StartName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>User or object name to run service as</td></row>
		<row><td>ServiceInstall</td><td>StartType</td><td>N</td><td>0</td><td>4</td><td/><td/><td/><td/><td>Type of the service</td></row>
		<row><td>Shortcut</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The command-line arguments for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table denoting the component whose selection gates the the shortcut creation/deletion.</td></row>
		<row><td>Shortcut</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The description for the shortcut.</td></row>
		<row><td>Shortcut</td><td>DescriptionResourceDLL</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This field contains a Formatted string value for the full path to the language neutral file that contains the MUI manifest.</td></row>
		<row><td>Shortcut</td><td>DescriptionResourceId</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The description name index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the shortcut file is created.</td></row>
		<row><td>Shortcut</td><td>DisplayResourceDLL</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This field contains a Formatted string value for the full path to the language neutral file that contains the MUI manifest.</td></row>
		<row><td>Shortcut</td><td>DisplayResourceId</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The display name index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Hotkey</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The hotkey for the shortcut. It has the virtual-key code for the key in the low-order byte, and the modifier flags in the high-order byte.</td></row>
		<row><td>Shortcut</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a shortcut.  Mainly used in pro project types.</td></row>
		<row><td>Shortcut</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Shortcut.</td></row>
		<row><td>Shortcut</td><td>ISShortcutName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A non-unique name for the shortcut.  Mainly used by pro pro project types.</td></row>
		<row><td>Shortcut</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>The icon index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the external icon file for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the shortcut to be created.</td></row>
		<row><td>Shortcut</td><td>Shortcut</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>Shortcut</td><td>ShowCmd</td><td>Y</td><td/><td/><td/><td/><td/><td>1;3;7</td><td>The show command for the application window.The following values may be used.</td></row>
		<row><td>Shortcut</td><td>Target</td><td>N</td><td/><td/><td/><td/><td>Shortcut</td><td/><td>The shortcut target. This is usually a property that is expanded to a file or a folder that the shortcut points to.</td></row>
		<row><td>Shortcut</td><td>WkDir</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property defining location of working directory.</td></row>
		<row><td>Signature</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the file. This may contain a "short name|long name" pair.</td></row>
		<row><td>Signature</td><td>Languages</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>The languages supported by the file.</td></row>
		<row><td>Signature</td><td>MaxDate</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The maximum creation date of the file.</td></row>
		<row><td>Signature</td><td>MaxSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The maximum size of the file.</td></row>
		<row><td>Signature</td><td>MaxVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The maximum version of the file.</td></row>
		<row><td>Signature</td><td>MinDate</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The minimum creation date of the file.</td></row>
		<row><td>Signature</td><td>MinSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The minimum size of the file.</td></row>
		<row><td>Signature</td><td>MinVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The minimum version of the file.</td></row>
		<row><td>Signature</td><td>Signature</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The table key. The Signature represents a unique file signature.</td></row>
		<row><td>TextStyle</td><td>Color</td><td>Y</td><td>0</td><td>16777215</td><td/><td/><td/><td/><td>A long integer indicating the color of the string in the RGB format (Red, Green, Blue each 0-255, RGB = R + 256*G + 256^2*B).</td></row>
		<row><td>TextStyle</td><td>FaceName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>A string indicating the name of the font used. Required. The string must be at most 31 characters long.</td></row>
		<row><td>TextStyle</td><td>Size</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The size of the font used. This size is given in our units (1/12 of the system font height). Assuming that the system font is set to 12 point size, this is equivalent to the point size.</td></row>
		<row><td>TextStyle</td><td>StyleBits</td><td>Y</td><td>0</td><td>15</td><td/><td/><td/><td/><td>A combination of style bits.</td></row>
		<row><td>TextStyle</td><td>TextStyle</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the style. The primary key of this table. This name is embedded in the texts to indicate a style change.</td></row>
		<row><td>TypeLib</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>TypeLib</td><td>Cost</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The cost associated with the registration of the typelib. This column is currently optional.</td></row>
		<row><td>TypeLib</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>TypeLib</td><td>Directory_</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Optional. The foreign key into the Directory table denoting the path to the help file for the type library.</td></row>
		<row><td>TypeLib</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the type library to be operational.</td></row>
		<row><td>TypeLib</td><td>Language</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The language of the library.</td></row>
		<row><td>TypeLib</td><td>LibID</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The GUID that represents the library.</td></row>
		<row><td>TypeLib</td><td>Version</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The version of the library. The major version is in the upper 8 bits of the short integer. The minor version is in the lower 8 bits.</td></row>
		<row><td>UIText</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key that identifies the particular string.</td></row>
		<row><td>UIText</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The localized version of the string.</td></row>
		<row><td>Upgrade</td><td>ActionProperty</td><td>N</td><td/><td/><td/><td/><td>UpperCase</td><td/><td>The property to set when a product in this set is found.</td></row>
		<row><td>Upgrade</td><td>Attributes</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The attributes of this product set.</td></row>
		<row><td>Upgrade</td><td>ISDisplayName</td><td>Y</td><td/><td/><td>ISUpgradeMsiItem</td><td>1</td><td/><td/><td/></row>
		<row><td>Upgrade</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>A comma-separated list of languages for either products in this set or products not in this set.</td></row>
		<row><td>Upgrade</td><td>Remove</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The list of features to remove when uninstalling a product from this set.  The default is "ALL".</td></row>
		<row><td>Upgrade</td><td>UpgradeCode</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The UpgradeCode GUID belonging to the products in this set.</td></row>
		<row><td>Upgrade</td><td>VersionMax</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The maximum ProductVersion of the products in this set.  The set may or may not include products with this particular version.</td></row>
		<row><td>Upgrade</td><td>VersionMin</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The minimum ProductVersion of the products in this set.  The set may or may not include products with this particular version.</td></row>
		<row><td>Verb</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Optional value for the command arguments.</td></row>
		<row><td>Verb</td><td>Command</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The command text.</td></row>
		<row><td>Verb</td><td>Extension_</td><td>N</td><td/><td/><td>Extension</td><td>1</td><td>Text</td><td/><td>The extension associated with the table row.</td></row>
		<row><td>Verb</td><td>Sequence</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Order within the verbs for a particular extension. Also used simply to specify the default verb.</td></row>
		<row><td>Verb</td><td>Verb</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The verb for the command.</td></row>
		<row><td>_Validation</td><td>Category</td><td>Y</td><td/><td/><td/><td/><td/><td>"Text";"Formatted";"Template";"Condition";"Guid";"Path";"Version";"Language";"Identifier";"Binary";"UpperCase";"LowerCase";"Filename";"Paths";"AnyPath";"WildCardFilename";"RegPath";"KeyFormatted";"CustomSource";"Property";"Cabinet";"Shortcut";"URL";"DefaultDir"</td><td>String category</td></row>
		<row><td>_Validation</td><td>Column</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of column</td></row>
		<row><td>_Validation</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of column</td></row>
		<row><td>_Validation</td><td>KeyColumn</td><td>Y</td><td>1</td><td>32</td><td/><td/><td/><td/><td>Column to which foreign key connects</td></row>
		<row><td>_Validation</td><td>KeyTable</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>For foreign key, Name of table to which data must link</td></row>
		<row><td>_Validation</td><td>MaxValue</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Maximum value allowed</td></row>
		<row><td>_Validation</td><td>MinValue</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Minimum value allowed</td></row>
		<row><td>_Validation</td><td>Nullable</td><td>N</td><td/><td/><td/><td/><td/><td>Y;N;@</td><td>Whether the column is nullable</td></row>
		<row><td>_Validation</td><td>Set</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Set of values that are permitted</td></row>
		<row><td>_Validation</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of table</td></row>
	</table>
</msi>
