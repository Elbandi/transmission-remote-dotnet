!include "MUI2.nsh"
!include "FileAssociation.nsh"
!include "ProtocolAssociation.nsh"
!include "x64.nsh"

;Rebuild Release
!system "%Windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe /nologo /verbosity:m /t:Rebuild /p:Configuration=Release"

;--------------------------------

; The name of the installer
Name "Transmission Remote"

; The file to write
OutFile "transmission-remote-dotnet-3.12-installer.exe"

; The default installation directory
!define ProgramFilesDir "Transmission Remote"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\TransmissionRemote" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

XPStyle on

Var StartMenuFolder

!define MUI_ICON "TransmissionClientNew\transmission_large.ico"
!define MUI_UNICON "TransmissionClientNew\transmission_large.ico"
!define MUI_HEADERIMAGE
;!define MUI_HEADERIMAGE_BITMAP "logo.bmp"
;!define MUI_WELCOMEFINISHPAGE_BITMAP "nsis_wizard.bmp"
;!define MUI_UNWELCOMEFINISHPAGE_BITMAP "nsis_wizard.bmp"
;!define MUI_COMPONENTSPAGE_CHECKBITMAP "${NSISDIR}\Contrib\Graphics\Checks\colorful.bmp"
!define MUI_COMPONENTSPAGE_SMALLDESC
!define MUI_ABORTWARNING

!define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\README.txt"
!define MUI_FINISHPAGE_SHOWREADME_TEXT "Show ReadMe"
!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED

; Pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "LICENCE.txt"
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
;!define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU"
;!define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\TransmissionRemote"
;!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

;--------------------------------

!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_RESERVEFILE_LANGDLL

; English
LangString NAME_SecTransmissionRemote ${LANG_ENGLISH} "Transmission Remote (required)"
LangString DESC_SecTransmissionRemote ${LANG_ENGLISH} "If set, a shortcut for Transmission Remote will be created on the desktop."
LangString NAME_SecFiletypeAssociations ${LANG_ENGLISH} "Register Filetype Associations"
LangString DESC_SecFiletypeAssociations ${LANG_ENGLISH} "Register Associations to Transmission Remote"
LangString NAME_SecRegiterTorrent ${LANG_ENGLISH} "Register .torrent"
LangString DESC_SecRegiterTorrent ${LANG_ENGLISH} "Register .torrent to Transmission Remote"
LangString NAME_SecRegiterMagnet ${LANG_ENGLISH} "Register Magnet URI"
LangString DESC_SecRegiterMagnet ${LANG_ENGLISH} "Register Magnet URI to Transmission Remote"
LangString NAME_SecDesktopIcon ${LANG_ENGLISH} "Create icon on desktop"
LangString DESC_SecDesktopIcon ${LANG_ENGLISH} "If set, a shortcut for Transmission Remote will be created on the desktop."
LangString DESC_SecGeoIPDatabase ${LANG_ENGLISH} "GeoIP database"
LangString NAME_SecLanguages ${LANG_ENGLISH} "Languages"
LangString DESC_SecLanguages ${LANG_ENGLISH} "Languages for Transmission Remote"

;--------------------------------

; The stuff to install
Section $(NAME_SecTransmissionRemote) SecTransmissionRemote
  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "TransmissionClientNew\bin\Release\Transmission Remote.exe"
  File "TransmissionClientNew\Jayrock\Release\Jayrock.dll"
  File "TransmissionClientNew\Jayrock\Release\Jayrock.Json.dll"
  File "README.txt"
  File "LICENCE.txt"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM "SOFTWARE\TransmissionRemote" "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Transmission Remote" "DisplayName" "Transmission Remote"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Transmission Remote" "Publisher" "Alan F"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Transmission Remote" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Transmission Remote" "DisplayIcon" "$INSTDIR\Transmission Remote.exe,0"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Transmission Remote" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Transmission Remote" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    SetShellVarContext current
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Transmission Remote.lnk" "$INSTDIR\Transmission Remote.exe" "" "$INSTDIR\Transmission Remote.exe" 0 
    SetShellVarContext all
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
;    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Transmission Remote.lnk" "$INSTDIR\Transmission Remote.exe" "" "$INSTDIR\Transmission Remote.exe" 0 
  !insertmacro MUI_STARTMENU_WRITE_END

SectionEnd

; Optional section (can be disabled by the user)

Section /o $(NAME_SecDesktopIcon) SecDesktopIcon
  SetShellVarContext current
  SetOutPath "$INSTDIR\bin"
  CreateShortCut "$DESKTOP\Transmission Remote.lnk" "$INSTDIR\Transmission Remote.exe" "" "$INSTDIR\Transmission Remote.exe" 0
SectionEnd

Section "GeoIP Database" SecGeoIPDatabase
  File "GeoIP.dat"
SectionEnd

SubSection $(NAME_SecFiletypeAssociations) SecFiletypeAssociations

  Section $(NAME_SecRegiterTorrent) SecRegiterTorrent
    ${registerExtension} "$INSTDIR\Transmission Remote.exe" ".torrent" "Transmission Remote Torrent"
  SectionEnd

  Section $(NAME_SecRegiterMagnet) SecRegiterMagnet
    ${registerProtocol} "$INSTDIR\Transmission Remote.exe" "magnet" "Magnet URI"
  SectionEnd

SubSectionEnd

; Translation

SectionGroup $(NAME_SecLanguages) SecLanguages

  Section /o "Chinese" SecLanguagesChinese
    CreateDirectory "$INSTDIR\zh-CN"
    SetOutPath "$INSTDIR\zh-CN"
    File "TransmissionClientNew\bin\Release\zh-CN\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Czech" SecLanguagesCzech
    CreateDirectory "$INSTDIR\cs-CZ"
    SetOutPath "$INSTDIR\cs-CZ"
    File "TransmissionClientNew\bin\Release\cs-CZ\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Danish" SecLanguagesDanish
    CreateDirectory "$INSTDIR\da-DK"
    SetOutPath "$INSTDIR\da-DK"
    File "TransmissionClientNew\bin\Release\da-DK\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "French" SecLanguagesFrench
    CreateDirectory "$INSTDIR\fr-FR"
    SetOutPath "$INSTDIR\fr-FR"
    File "TransmissionClientNew\bin\Release\fr-FR\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "German" SecLanguagesGerman
    CreateDirectory "$INSTDIR\de-DE"
    SetOutPath "$INSTDIR\de-DE"
    File "TransmissionClientNew\bin\Release\de-DE\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Hungarian" SecLanguagesHungarian
    CreateDirectory "$INSTDIR\hu-HU"
    SetOutPath "$INSTDIR\hu-HU"
    File "TransmissionClientNew\bin\Release\hu-HU\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Korean" SecLanguagesKorean
    CreateDirectory "$INSTDIR\ko-KR"
    SetOutPath "$INSTDIR\ko-KR"
    File "TransmissionClientNew\bin\Release\ko-KR\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Polish" SecLanguagesPolish
    CreateDirectory "$INSTDIR\pl-PL"
    SetOutPath "$INSTDIR\pl-PL"
    File "TransmissionClientNew\bin\Release\pl-PL\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Russian" SecLanguagesRussian
    CreateDirectory "$INSTDIR\ru-RU"
    SetOutPath "$INSTDIR\ru-RU"
    File "TransmissionClientNew\bin\Release\ru-RU\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Spanish" SecLanguagesSpanish
    CreateDirectory "$INSTDIR\es-ES"
    SetOutPath "$INSTDIR\es-ES"
    File "TransmissionClientNew\bin\Release\es-ES\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Taiwanese" SecLanguagesTaiwanese
    CreateDirectory "$INSTDIR\zh-TW"
    SetOutPath "$INSTDIR\zh-TW"
    File "TransmissionClientNew\bin\Release\zh-TW\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Turkish" SecLanguagesTurkish
    CreateDirectory "$INSTDIR\tr-TR"
    SetOutPath "$INSTDIR\tr-TR"
    File "TransmissionClientNew\bin\Release\tr-TR\Transmission Remote.resources.dll"
  SectionEnd

SectionGroupEnd

;--------------------------------

; Uninstaller

Section "Uninstall"

  ; Unregister File Association
  ${unregisterExtension} ".torrent" "Transmission Remote Torrent"
  ${unregisterProtocol} "magnet" "Magnet URI"
  
  ; Remove registry keys
  DeleteRegKey HKLM SOFTWARE\TransmissionRemote

  ; Remove files and uninstaller
  Delete "$INSTDIR\Transmission Remote.exe"
  Delete "$INSTDIR\Jayrock.dll"
  Delete "$INSTDIR\Jayrock.Json.dll"
  Delete "$INSTDIR\uninstall.exe"
  Delete "$INSTDIR\GeoIP.dat"
  Delete "$INSTDIR\README.txt"
  Delete "$INSTDIR\LICENCE.txt"
  Delete "$INSTDIR\de-DE\Transmission Remote.resources.dll"
  Delete "$INSTDIR\fr-FR\Transmission Remote.resources.dll"
  Delete "$INSTDIR\da-DK\Transmission Remote.resources.dll"
  Delete "$INSTDIR\hu-HU\Transmission Remote.resources.dll"
  Delete "$INSTDIR\zh-CN\Transmission Remote.resources.dll"
  Delete "$INSTDIR\pl-PL\Transmission Remote.resources.dll"
  Delete "$INSTDIR\ru-RU\Transmission Remote.resources.dll"
  Delete "$INSTDIR\tr-TR\Transmission Remote.resources.dll"
  Delete "$INSTDIR\es-ES\Transmission Remote.resources.dll"
  Delete "$INSTDIR\ko-KR\Transmission Remote.resources.dll"
  Delete "$INSTDIR\zh-TW\Transmission Remote.resources.dll"
  Delete "$INSTDIR\cs-CZ\Transmission Remote.resources.dll"

  ; Remove shortcuts, if any
  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
  SetShellVarContext current
  Delete "$SMPROGRAMS\$StartMenuFolder\*.*"
  RMDir "$SMPROGRAMS\$StartMenuFolder"
  SetShellVarContext all
  Delete "$SMPROGRAMS\$StartMenuFolder\*.*"
  RMDir "$SMPROGRAMS\$StartMenuFolder"

  ; Remove directories used
  RMDir "$INSTDIR\da-DK"
  RMDir "$INSTDIR\de-DE"
  RMDir "$INSTDIR\fr-FR"
  RMDir "$INSTDIR\zh-CN"
  RMDir "$INSTDIR\hu-HU"
  RMDir "$INSTDIR\ru-RU"
  RMDir "$INSTDIR\pl-PL"
  RMDir "$INSTDIR\tr-TR"
  RMDir "$INSTDIR\es-ES"
  RMDir "$INSTDIR\ko-KR"
  RMDir "$INSTDIR\zh-TW"
  RMDir "$INSTDIR\cs-CZ"
  RMDir "$INSTDIR"

  DeleteRegKey /ifempty HKCU "Software\TransmissionRemote"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Transmission Remote"

SectionEnd

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SecTransmissionRemote} $(DESC_SecTransmissionRemote)
  !insertmacro MUI_DESCRIPTION_TEXT ${SecDesktopIcon} $(DESC_SecDesktopIcon)
  !insertmacro MUI_DESCRIPTION_TEXT ${SecFiletypeAssociations} $(DESC_SecFiletypeAssociations)
  !insertmacro MUI_DESCRIPTION_TEXT ${SecGeoIPDatabase} $(DESC_SecGeoIPDatabase)
  !insertmacro MUI_DESCRIPTION_TEXT ${SecLanguages} $(DESC_SecLanguages)
  !insertmacro MUI_DESCRIPTION_TEXT ${SecRegiterTorrent} $(DESC_SecRegiterTorrent)
  !insertmacro MUI_DESCRIPTION_TEXT ${SecRegiterMagnet} $(DESC_SecRegiterMagnet)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

Function .onInit
  !insertmacro MUI_LANGDLL_DISPLAY
  ${If} ${RunningX64}
      StrCpy $INSTDIR "$PROGRAMFILES64\${ProgramFilesDir}"
  ${Else}
      StrCpy $INSTDIR "$PROGRAMFILES\${ProgramFilesDir}"
  ${Endif}
FunctionEnd
