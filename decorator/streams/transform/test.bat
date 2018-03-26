@echo off
set configuration=%1
set key=%2
set anotherKey=42
set basePath="bin\\%configuration%\\"
set exePath="%basePath%transform.exe"

set initFile="random.txt"
set compressed="compressed.txt"
set encrypted="encrypted.txt"
set compressedEncripted="encripted.txt"
set doubleEncrypted="doubleEncrypted.txt"
set compressedDoubleEncrypted="compressedDoubleEncrypted.txt"

set decompressed="decompressed.txt"
set decrypted="decrypted.txt"
set decompressedDecrypted="decompressedDecrypted.txt"
set doubleDecrypted="doubleDecrypted.txt"
set decompressedDoubleDecrypted="decompressedDoubleDecrypted.txt"

set decryptedWithAnotherKey="decryptedWithAnotherKey.txt"

@echo on
call %exePath% -o %initFile%

call %exePath% -i %initFile% -c -o %compressed%
call %exePath% -i %initFile% -e %key% -o %encrypted%
call %exePath% -i %initFile% -c -e %key% -o %compressedEncripted%
call %exePath% -i %initFile% -e %key% %anotherKey% -o %doubleEncrypted%
call %exePath% -i %initFile% -e %key% %anotherKey% -c -o %compressedDoubleEncrypted%

call %exePath% -i %compressed% -u -o %decompressed%
call %exePath% -i %encrypted% -d %key% -o %decrypted%
call %exePath% -i %compressedEncripted% -u -d %key% -o %decompressedDecrypted%
call %exePath% -i %doubleEncrypted% -d %key% %anotherKey% -o %doubleDecrypted%
call %exePath% -i %compressedDoubleEncrypted% -d %key% %anotherKey% -u -o %decompressedDoubleDecrypted%

fc /b %basePath%%initFile% %basePath%%decompressed%
fc /b %basePath%%initFile% %basePath%%decrypted%
fc /b %basePath%%initFile% %basePath%%decompressedDecrypted%
fc /b %basePath%%initFile% %basePath%%doubleDecrypted%
fc /b %basePath%%initFile% %basePath%%decompressedDoubleDecrypted%

call %exePath% -i %encrypted% -d %anotherKey% -o %decryptedWithAnotherKey%
fc /b %basePath%%initFile% %basePath%%decryptedWithAnotherKey% > encriptionKeysDiff.txt
fc /b %basePath%%encrypted% %basePath%%doubleEncrypted% > encriptionCountsDiff.txt