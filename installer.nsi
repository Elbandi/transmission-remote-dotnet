; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

!include "FileAssociation.nsh"

;Rebuild Release
!system "%Windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe /verbosity:m /t:Rebuild /p:Configuration=Release"

;--------------------------------

; The name of the installer
Name "Transmission Remote"

; The file to write
OutFile "transmission-remote-dotnet-3.12-installer.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\Transmission Remote"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\TransmissionRemote" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

; Pages

Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section "Transmission Remote (required)"
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
  
SectionEnd

; Optional section (can be disabled by the user)

SubSection "Shortcuts" SEC01

  Section "Start Menu Shortcuts"
    CreateDirectory "$SMPROGRAMS\Transmission Remote"
    CreateShortCut "$SMPROGRAMS\Transmission Remote\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
    CreateShortCut "$SMPROGRAMS\Transmission Remote\Transmission Remote.lnk" "$INSTDIR\Transmission Remote.exe" "" "$INSTDIR\Transmission Remote.exe" 0 
  SectionEnd
  
  Section /o "Desktop Shortcut"
    CreateShortCut "$DESKTOP\Transmission Remote.lnk" "$INSTDIR\Transmission Remote.exe" "" "$INSTDIR\Transmission Remote.exe" 0
  SectionEnd

SubSectionEnd
  
Section "GeoIP Database"
  File "GeoIP.dat"
SectionEnd

SubSection "File type associations" SEC02

  Section "Register .torrent to Transmission Remote"
    ${registerExtension} "$INSTDIR\Transmission Remote.exe" ".torrent" "Transmission Remote Torrent"
  SectionEnd

SubSectionEnd

; Translation

SubSection "Translation" SEC03

  Section /o "Chinese translation"
    CreateDirectory "$INSTDIR\zh-CN"
    SetOutPath "$INSTDIR\zh-CN"
    File "TransmissionClientNew\bin\Release\zh-CN\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Czech translation"
    CreateDirectory "$INSTDIR\cs-CZ"
    SetOutPath "$INSTDIR\cs-CZ"
    File "TransmissionClientNew\bin\Release\cs-CZ\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Danish translation"
    CreateDirectory "$INSTDIR\da-DK"
    SetOutPath "$INSTDIR\da-DK"
    File "TransmissionClientNew\bin\Release\da-DK\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "French translation"
    CreateDirectory "$INSTDIR\fr-FR"
    SetOutPath "$INSTDIR\fr-FR"
    File "TransmissionClientNew\bin\Release\fr-FR\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "German translation"
    CreateDirectory "$INSTDIR\de-DE"
    SetOutPath "$INSTDIR\de-DE"
    File "TransmissionClientNew\bin\Release\de-DE\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Hungarian translation"
    CreateDirectory "$INSTDIR\hu-HU"
    SetOutPath "$INSTDIR\hu-HU"
    File "TransmissionClientNew\bin\Release\hu-HU\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Korean translation"
    CreateDirectory "$INSTDIR\ko-KR"
    SetOutPath "$INSTDIR\ko-KR"
    File "TransmissionClientNew\bin\Release\ko-KR\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Polish translation"
    CreateDirectory "$INSTDIR\pl-PL"
    SetOutPath "$INSTDIR\pl-PL"
    File "TransmissionClientNew\bin\Release\pl-PL\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Russian translation"
    CreateDirectory "$INSTDIR\ru-RU"
    SetOutPath "$INSTDIR\ru-RU"
    File "TransmissionClientNew\bin\Release\ru-RU\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Spanish translation"
    CreateDirectory "$INSTDIR\es-ES"
    SetOutPath "$INSTDIR\es-ES"
    File "TransmissionClientNew\bin\Release\es-ES\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Taiwanese translation"
    CreateDirectory "$INSTDIR\zh-TW"
    SetOutPath "$INSTDIR\zh-TW"
    File "TransmissionClientNew\bin\Release\zh-TW\Transmission Remote.resources.dll"
  SectionEnd
  
  Section /o "Turkish translation"
    CreateDirectory "$INSTDIR\tr-TR"
    SetOutPath "$INSTDIR\tr-TR"
    File "TransmissionClientNew\bin\Release\tr-TR\Transmission Remote.resources.dll"
  SectionEnd

SubSectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"

  ; Unregister File Association
  ${unregisterExtension} ".torrent" "Transmission Remote Torrent"
  
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
  Delete "$SMPROGRAMS\Transmission Remote\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\Transmission Remote"
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

SectionEnd
