echo off
REM Для начала Вы должны установить MySQL сервер 
REM и импортировать структуру базы данных из файла Install.sql
REM Далее поправить строчку вызова, где 192.168.1.2 - адрес сервера,
REM aver - имя базы данных, user - имя пользователя, pass - пароль.

start Netcode.Scan.exe online 192.168.1.2 aver root adminadmin