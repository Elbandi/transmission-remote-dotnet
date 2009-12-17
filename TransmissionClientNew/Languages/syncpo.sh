#!/bin/sh
for i in *.po; do ./msgmerge.exe --no-wrap -U $i template.pot ; done
