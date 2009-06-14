<?php
error_reporting(0);
require_once './config.php';
require_once './include/functions.php';

$id = $_GET[id];
$text = $_GET[text];

switch($id)
{
case 1:
{
$arr = get_fileinfo(tbl_fileinfo, 'utf8', $text, DBName, dbHostName, dbUserName, dbPassword, 'Hash');
$sl_title = 'Информация о хеше '.$text;
}
break;

case 2:
{
$arr = get_fileinfo(tbl_fileinfo, 'utf8', $text, DBName, dbHostName, dbUserName, dbPassword, 'ShortName');
$sl_title = 'Информация о имени '.$text;
}
break;

default:
{
$sl_title = 'База данных эталонных файлов';
$arr['text'] ='Чтобы узнать MD5 хеш, можете воспользоваться программой <a href="http://beeblebrox.org/hashtab/hashtab2_setup.exe">Hashtab</a>.<br />После установки в свойствах файла добавится вкладка &laquo;Контрольные суммы&raquo;. <br /> Скопируйте в текстовое поле 32-х значный MD5 хеш в поле ввода.<br />Например, <a href="http://scan.rootkits.ru/h.php?id=1&text=19A25AF0FAF6DA7E571B65A349CD413C">19A25AF0FAF6DA7E571B65A349CD413C</a><br /><br />Если ищете по имени файла, вводите его полностью<br /> (вместе с расширением), например, <a href="http://scan.rootkits.ru/h.php?id=2&text=explorer.exe">explorer.exe</a>';
}

}




require_once './tmpl/'.$theme.'/head.tpl';
echo $arr['text'];
require_once './tmpl/'.$theme.'/down.tpl';

?>