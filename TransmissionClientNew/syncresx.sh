#!/bin/sh
for i in AboutDialog.??-??.resx; do ./resxsync.exe AboutDialog.resx "$i"; done
for i in ErrorLogWindow.??-??.resx; do ./resxsync.exe ErrorLogWindow.resx "$i"; done
for i in FindDialog.??-??.resx; do ./resxsync.exe FindDialog.resx "$i"; done
for i in LocalSettingsDialog.??-??.resx; do ./resxsync.exe LocalSettingsDialog.resx "$i"; done
for i in MainWindow.??-??.resx; do ./resxsync.exe MainWindow.resx "$i"; done
for i in MoveDataPrompt.??-??.resx; do ./resxsync.exe MoveDataPrompt.resx "$i"; done
for i in OtherStrings.??-??.resx; do ./resxsync.exe OtherStrings.resx "$i"; done
for i in RemoteSettingsDialog.??-??.resx; do ./resxsync.exe RemoteSettingsDialog.resx "$i"; done
for i in RssForm.??-??.resx; do ./resxsync.exe RssForm.resx "$i"; done
for i in StatsDialog.??-??.resx; do ./resxsync.exe StatsDialog.resx "$i"; done
for i in TorrentLoadDialog.??-??.resx; do ./resxsync.exe TorrentLoadDialog.resx "$i"; done
for i in TorrentPropertiesDialog.??-??.resx; do ./resxsync.exe TorrentPropertiesDialog.resx "$i"; done
for i in UriPromptWindow.??-??.resx; do ./resxsync.exe UriPromptWindow.resx "$i"; done
for i in *.??-??.resx; do perl prune_resx.pl "$i"; done
