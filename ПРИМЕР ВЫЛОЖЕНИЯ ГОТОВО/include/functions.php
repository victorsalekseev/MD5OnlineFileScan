<?php
error_reporting(0);
function get_fileinfo($table_name, $enc, $hash, $db_n, $db_hn, $db_u, $db_p, $field)//Имя таблицы, кодировка, имя словаря
{
$res_obj='Исследуемый объект';
if($field=='Hash')
{
	if(mb_strlen($hash)<>32)
		{
		return array("title" => 'Error formatting Hash', "text" => 'Error formatting Hash');
		}
}

define("START_TIME", microtime(true));
$out_t='';
//----------------------------------------------------------- Подготовка работы с БД нач.
$Link_Id=mysql_connect($db_hn, $db_u, $db_p);
mysql_select_db($db_n, $Link_Id);
mysql_query("Set names '$enc'; ", $Link_Id);
mysql_query ("set character_set_client='$enc'", $Link_Id);
mysql_query ("set character_set_results='$enc'", $Link_Id);
mysql_query ("set collation_connection='".$enc."_general_ci'", $Link_Id);
//----------------------------------------------------------- Подготовка работы с БД кон.


$sum_rows = mysql_fetch_row(mysql_query('SELECT COUNT(idFile) FROM '.$table_name, $Link_Id));

$find_w = htmlspecialchars($hash);

if($field=='Hash')
{
	$result = mysql_query('SELECT * FROM '.$table_name.' WHERE Hash = \''.$find_w.'\'', $Link_Id);//
	$res_obj='Хеш';
}

if($field=='ShortName')
{
	$result = mysql_query('SELECT * FROM '.$table_name.' WHERE ShortName = \''.$find_w.'\'', $Link_Id);//
	$res_obj='Имя файла';
}


$num_rows=mysql_num_rows($result);

define("STOP_TIME", microtime(true)-START_TIME);//Конец работы скрипта времени

$w_pr = '';
if($find_w<>'')
{
	$w_pr = '<b>&laquo;'.$find_w.'&raquo;</b>';
}

$out_t=$out_t.'<table border=0 cellpadding=0 cellspacing=0 width=400><tr><td align=center nowrap>'.$res_obj.' <b>'.$w_pr.'</b>.<br />Просмотрено:&nbsp;'.$sum_rows[0].', найдено:&nbsp;'.$num_rows.', затрачено '.round(STOP_TIME, 4).' секунд.</td></tr></table><br />';

$no_found = "<table cellpadding=1 cellspacing=0 width=400 border=1 bordercolor=black><tr><td width=400 nowrap>Этого файла пока нет в базе данных. Отправьте его на исследование.</td></tr></table><br />";

for($i = 0; $i < $num_rows; $i++)
{
	$no_found='';
	$n_var=$i+1;
	$out_t=$out_t.'<table cellpadding=1 cellspacing=0 width=400 border=1 bordercolor=black>';
	$IdFile=htmlspecialchars(mysql_result($result, $i, 'IdFile'));
	$isChecked = htmlspecialchars(mysql_result($result, $i, 'isChecked'));
	if($isChecked==0) {
	$isChecked='Неизвестно'; }
	else {
	$isChecked='Файл безопасен'; }

	$Hash=htmlspecialchars(mysql_result($result, $i, 'Hash'));
	$CheckedDate=htmlspecialchars(mysql_result($result, $i, 'CheckedDate'));
	$ShortName=htmlspecialchars(mysql_result($result, $i, 'ShortName'));
	$CreationTime=htmlspecialchars(mysql_result($result, $i, 'CreationTime'));
	$CreationTimeUtc=htmlspecialchars(mysql_result($result, $i, 'CreationTimeUtc'));
	$Directory=htmlspecialchars(mysql_result($result, $i, 'Directory'));
	$DirectoryName=htmlspecialchars(mysql_result($result, $i, 'DirectoryName'));
	$Extension=htmlspecialchars(mysql_result($result, $i, 'Extension'));
	$FullName=htmlspecialchars(mysql_result($result, $i, 'FullName'));
	$IsReadOnly=htmlspecialchars(mysql_result($result, $i, 'IsReadOnly'));
	$LastAccessTime=htmlspecialchars(mysql_result($result, $i, 'LastAccessTime'));
	$LastAccessTimeUtc=htmlspecialchars(mysql_result($result, $i, 'LastAccessTimeUtc'));
	$LastWriteTime=htmlspecialchars(mysql_result($result, $i, 'LastWriteTime'));
	$LastWriteTimeUtc=htmlspecialchars(mysql_result($result, $i, 'LastWriteTimeUtc'));
	$Length=htmlspecialchars(mysql_result($result, $i, 'Length'));
	$Attributes=htmlspecialchars(mysql_result($result, $i, 'Attributes'));
	$Comments=htmlspecialchars(mysql_result($result, $i, 'Comments'));
	$CompanyName=htmlspecialchars(mysql_result($result, $i, 'CompanyName'));
	$FileBuildPart=htmlspecialchars(mysql_result($result, $i, 'FileBuildPart'));
	$FileDescription=htmlspecialchars(mysql_result($result, $i, 'FileDescription'));
	$FileMajorPart=htmlspecialchars(mysql_result($result, $i, 'FileMajorPart'));
	$FileMinorPart=htmlspecialchars(mysql_result($result, $i, 'FileMinorPart'));
	$FileName=htmlspecialchars(mysql_result($result, $i, 'FileName'));
	$FilePrivatePart=htmlspecialchars(mysql_result($result, $i, 'FilePrivatePart'));
	$FileVersion=htmlspecialchars(mysql_result($result, $i, 'FileVersion'));
	$InternalName=htmlspecialchars(mysql_result($result, $i, 'InternalName'));
	$IsDebug=htmlspecialchars(mysql_result($result, $i, 'IsDebug'));
	$IsPatched=htmlspecialchars(mysql_result($result, $i, 'IsPatched'));
	$IsPreRelease=htmlspecialchars(mysql_result($result, $i, 'IsPreRelease'));
	$IsPrivateBuild=htmlspecialchars(mysql_result($result, $i, 'IsPrivateBuild'));
	$IsSpecialBuild=htmlspecialchars(mysql_result($result, $i, 'IsSpecialBuild'));
	$Language=htmlspecialchars(mysql_result($result, $i, 'Language'));
	$LegalCopyright=htmlspecialchars(mysql_result($result, $i, 'LegalCopyright'));
	$LegalTrademarks=htmlspecialchars(mysql_result($result, $i, 'LegalTrademarks'));
	$OriginalFilename=htmlspecialchars(mysql_result($result, $i, 'OriginalFilename'));
	$PrivateBuild=htmlspecialchars(mysql_result($result, $i, 'PrivateBuild'));
	$ProductBuildPart=htmlspecialchars(mysql_result($result, $i, 'ProductBuildPart'));
	$ProductMajorPart=htmlspecialchars(mysql_result($result, $i, 'ProductMajorPart'));
	$ProductMinorPart=htmlspecialchars(mysql_result($result, $i, 'ProductMinorPart'));
	$ProductName=htmlspecialchars(mysql_result($result, $i, 'ProductName'));
	$ProductPrivatePart=htmlspecialchars(mysql_result($result, $i, 'ProductPrivatePart'));
	$ProductVersion=htmlspecialchars(mysql_result($result, $i, 'ProductVersion'));
	$SpecialBuild=htmlspecialchars(mysql_result($result, $i, 'SpecialBuild'));

	$out_t=$out_t.
	'<tr><td width=400 colspan=2 nowrap align=left><b>Вариант № '.$n_var.'</b></td></tr>
	<tr><td width=400 colspan=2 nowrap align=center>Общие свойства файла</td></tr>
	<tr><td width=400 colspan=2 nowrap align=center class=hash>MD5: '.$Hash.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Номер</td><td width=200 nowrap>'.$IdFile.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Доверие к файлу</td><td width=200 nowrap>'.$isChecked.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Дата проверки</td><td width=200 nowrap>'.$CheckedDate.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Имя файла</td><td width=200 nowrap>'.$ShortName.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Расширение</td><td width=200 nowrap>'.$Extension.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Полное имя файла</td><td width=200 nowrap>'.$FullName.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Размер, байт</td><td width=200 nowrap>'.$Length.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Каталог хранения</td><td width=200 nowrap>'.$Directory.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Имя каталога хранения</td><td width=200 nowrap>'.$DirectoryName.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Дата создания</td><td width=200 nowrap>'.$CreationTime.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Дата создания в UTC</td><td width=200 nowrap>'.$CreationTimeUtc.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Только для чтения</td><td width=200 nowrap>'.ret_txt_bool($IsReadOnly).'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Последнее время доступа</td><td width=200 nowrap>'.$LastAccessTime.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Последнее время доступа UTC</td><td width=200 nowrap>'.$LastAccessTimeUtc.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Последнее время записи</td><td width=200 nowrap>'.$LastWriteTime.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Последнее время записи UTC</td><td width=200 nowrap>'.$LastWriteTimeUtc.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Атрибуты</td><td width=200 nowrap>'.$Attributes.'&nbsp;</td></tr>
	<tr><td width=400 colspan=2 nowrap align=center>Свойства исполняемого файла</td></tr>
	<!-- <tr><td width=200 nowrap>FileName</td><td width=200 nowrap>'.$FileName.'&nbsp;</td></tr> -->
	<tr><td width=200 nowrap>Оригинальное имя</td><td width=200 nowrap>'.$OriginalFilename.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Внутреннее имя (InternalName)</td><td width=200 nowrap>'.$InternalName.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Язык</td><td width=200 nowrap>'.$Language.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Версия (FileBuildPart)</td><td width=200 nowrap>'.$FileBuildPart.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Компания-разработчик</td><td width=200 nowrap>'.$CompanyName.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>LegalCopyright</td><td width=200 nowrap>'.$LegalCopyright.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>LegalTrademarks</td><td width=200 nowrap>'.$LegalTrademarks.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>FileMajorPart</td><td width=200 nowrap>'.$FileMajorPart.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>FileMinorPart</td><td width=200 nowrap>'.$FileMinorPart.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>FilePrivatePart</td><td width=200 nowrap>'.$FilePrivatePart.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>FileVersion</td><td width=200 nowrap>'.$FileVersion.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>PrivateBuild</td><td width=200 nowrap>'.$PrivateBuild.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>ProductBuildPart</td><td width=200 nowrap>'.$ProductBuildPart.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>ProductMajorPart</td><td width=200 nowrap>'.$ProductMajorPart.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>ProductMinorPart</td><td width=200 nowrap>'.$ProductMinorPart.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>ProductName</td><td width=200 nowrap>'.$ProductName.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>ProductPrivatePart</td><td width=200 nowrap>'.$ProductPrivatePart.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>ProductVersion</td><td width=200 nowrap>'.$ProductVersion.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>SpecialBuild</td><td width=200 nowrap>'.$SpecialBuild.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Отладочная (IsDebug)</td><td width=200 nowrap>'.ret_txt_bool($IsDebug).'&nbsp;</td></tr>
	<tr><td width=200 nowrap>IsPatched</td><td width=200 nowrap>'.ret_txt_bool($IsPatched).'&nbsp;</td></tr>
	<tr><td width=200 nowrap>IsPreRelease</td><td width=200 nowrap>'.ret_txt_bool($IsPreRelease).'&nbsp;</td></tr>
	<tr><td width=200 nowrap>IsPrivateBuild</td><td width=200 nowrap>'.ret_txt_bool($IsPrivateBuild).'&nbsp;</td></tr>
	<tr><td width=200 nowrap>IsSpecialBuild</td><td width=200 nowrap>'.ret_txt_bool($IsSpecialBuild).'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Описание (FileDescription)</td><td width=200 nowrap>'.$FileDescription.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Комментарий</td><td width=200 nowrap>'.$Comments.'&nbsp;</td></tr>

	';
	$out_t=$out_t.'</table><br />';
}

$out_t=$out_t.$no_found;
mysql_close($Link_Id);//Завершаем работу с БД
$array_ret = array("title" => $w_pr, "text" => $out_t);
return $array_ret;
}

function ret_txt_bool($bool_prm)
{
	$ret = '';
	if($bool_prm==0) {
	$ret='Нет'; }
	else {
	$ret='Да'; }
return $ret;
}

?>