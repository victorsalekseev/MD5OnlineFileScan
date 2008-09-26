<?php

require_once './config.php';
require_once './include/functions.php';

$hash = $_GET[hash];
$arr = get_fileinfo(tbl_fileinfo, 'utf8', $hash, DBName, dbHostName, dbUserName, dbPassword);
$sl_title = 'Информация о хеше '.$hash;

require_once './tmpl/'.$theme.'/head.tpl';
echo $arr['text'];
require_once './tmpl/'.$theme.'/down.tpl';

?>