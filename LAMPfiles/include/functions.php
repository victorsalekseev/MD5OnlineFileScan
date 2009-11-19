<?php
error_reporting(0);
//--------------------имя таблицы--кодир.--хеш----имя базы--имя хоста--юзер ДБ--пароль ДБ--Поле----Цвет фона--Текущая стр.--Штук на стр.
function get_fileinfo($table_name, $enc,   $hash, $db_n,    $db_hn,   $db_u,    $db_p,     $field, $bg_color, $id_start,    $num_per_page)//Имя таблицы, кодировка, имя словаря
{
$res_obj='Исследуемый объект';
if($field=='Hash')
{
	if(mb_strlen($hash)<>32)
		{
		return array("title" => 'Error formatting Hash', "text" => '<table class="list_stats" border=1 bordercolor=black><tr><td>Введенный текст не распознается как MD5 хеш.<br /><b>Его нужно вводить полностью (32 знака).<br /><br /><i>Примеры:</i><br />Правильно: FA6D8B40A1A730193DFD8780F82C7F27<br /><br />Не правильно: FA6D8B40A1A730193DFD</b></td></tr></table><br />');
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

//$id_start = htmlspecialchars($id_start);//текущая страница, с 1..N
//if($id_start<1)
//{
//   return array("title" => 'Error formatting Page', "text" => '<table class="list_stats" border=1 bordercolor=black><tr><td><font color=red><b>ОШИБКА!</b><br />Страницы с таким номером не существует</font><br /></td></tr></table><br />');
//}
if($field=='Hash')
{
	//$find_id_start=($num_per_page*($id_start-1))+1;//с которой начинать отчет

	$result = mysql_query('SELECT * FROM '.$table_name.' WHERE Hash = \''.$find_w.'\' ORDER BY idFile', $Link_Id);//
	$res_obj='Хеш';
}

if($field=='ShortName')
{

	//$find_id_start=($num_per_page*($id_start-1))+1;//с которой начинать отчет

	$result = mysql_query('SELECT * FROM '.$table_name.' WHERE ShortName = \''.$find_w.'\' ORDER BY idFile', $Link_Id);//
	$res_obj='Имя файла';
}


$num_rows=mysql_num_rows($result);//сколько нашли (на случай конца таблицы)

define("STOP_TIME", microtime(true)-START_TIME);//Конец работы скрипта времени

//$last_num=$find_id_start+$num_per_page-1;
//if(ceil($sum_rows[0]/$num_per_page)==$id_start)//последня страница
//{
//$find_id_start++;
//$last_num=$sum_rows[0];
//}
//$title = 'Страница '.$id_start.'. Записи с '.$find_id_start.' по '.$last_num;
//$links=print_links($sum_rows[0], $id_start, $num_per_page, 10, $id_start);//Печать ссылок на страницы


$w_pr = '';
if($find_w<>'')
{
	$w_pr = '&laquo;'.$find_w.'&raquo;';
}


$out_t=$out_t.'<table class="list_stats"><tr><td align=center nowrap>'.$res_obj.' <b>'.$w_pr.'</b>.<br />'.$title .'<br />Просмотрено:&nbsp;'.$sum_rows[0].', найдено:&nbsp;'.$num_rows.', затрачено '.round(STOP_TIME, 4).' секунд.</td></tr></table><br />';

$no_found = '<table class="list_stats" border=1 bordercolor=black><tr><td>Этого файла пока нет в базе данных.<br /><b>Не забывайте, что имя файла как и MD5 хеш нужно вводить полностью (хеш 32 знака, имя файла вместе с расширением).<br /><br /><i>Примеры:</i><br />Имя файла правильно: explorer.exe<br /><br />Имя файла не правильно: explorer<br /><br />MD5 хеш правильно: FA6D8B40A1A730193DFD8780F82C7F27<br /><br />MD5 хеш не правильно: FA6D8B40A1A730193DFD</b></td></tr></table><br />

'.$links.'<br />';

for($i = 0; $i < $num_rows; $i++)
{
	$no_found='';
	$n_var=$i+$id_start;//было $find_id_start
	$out_t=$out_t.'<table cellpadding=1 cellspacing=0 width=100% border=1 bordercolor=black style="background-color:'.$bg_color.'">';
	$IdFile=htmlspecialchars(mysql_result($result, $i, 'IdFile'));
	$isChecked = htmlspecialchars(mysql_result($result, $i, 'isChecked'));
	if($isChecked==0) {
	$isChecked='Файл безопасен'; }
	else {
	$isChecked='Неизвестно'; }

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
	'<tr><td width=577px colspan=2 nowrap align=left><b>Вариант № '.$n_var.'</b> (<a href="/?id=1&md5='.$Hash.'">постоянная ссылка</a>)</td></tr>
	<tr><td width=577px colspan=2 nowrap align=center>Свойства файла</td></tr>
	<tr><td width=577px colspan=2 nowrap align=center class=hash>MD5: '.$Hash.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Номер</td><td  nowrap>'.$IdFile.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Доверие</td><td  nowrap>'.$isChecked.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Дата проверки</td><td  nowrap>'.$CheckedDate.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Имя файла</td><td  nowrap>'.$ShortName.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Расширение</td><td  nowrap>'.$Extension.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Полное имя файла</td><td>'.str_replace('WindowsXPSP3_IMG', 'WindowsXP', $FullName).'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Размер, байт</td><td  nowrap>'.$Length.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Каталог хранения</td><td>'.str_replace('WindowsXPSP3_IMG', 'WindowsXP', $Directory).'&nbsp;</td></tr>'
//	<tr><td width=200 nowrap>Имя каталога хранения</td><td  nowrap>'.$DirectoryName.'&nbsp;</td></tr>
	.'<tr><td width=200 nowrap>Дата создания</td><td  nowrap>'.$CreationTime.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Дата создания в UTC</td><td  nowrap>'.$CreationTimeUtc.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Только для чтения</td><td  nowrap>'.ret_txt_bool($IsReadOnly).'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Последнее время доступа</td><td  nowrap>'.$LastAccessTime.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Последнее время доступа UTC</td><td  nowrap>'.$LastAccessTimeUtc.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Последнее время записи</td><td  nowrap>'.$LastWriteTime.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Последнее время записи UTC</td><td  nowrap>'.$LastWriteTimeUtc.'&nbsp;</td></tr>
	<tr><td width=200 nowrap>Атрибуты</td><td  nowrap>'.$Attributes.'&nbsp;</td></tr>';
	
	//<!-- <tr><td width=200 nowrap>FileName</td><td  nowrap>'.$FileName.'&nbsp;</td></tr> -->
	//<tr><td width=200 nowrap>Оригинальное имя</td><td  nowrap>'.$OriginalFilename.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>Внутреннее имя (InternalName)</td><td  nowrap>'.$InternalName.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>Язык</td><td  nowrap>'.$Language.'&nbsp;</td></tr>

	$out_temp='';
	if($ProductName<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Название продукта</td><td>'.$ProductName.'&nbsp;</td></tr>';
	}

	if($CompanyName<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Компания-разработчик</td><td>'.$CompanyName.'&nbsp;</td></tr>';
	}

	if($LegalCopyright<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Копирайт</td><td>'.$LegalCopyright.'&nbsp;</td></tr>';
	}

	if($LegalTrademarks<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Торгавая марка</td><td>'.$LegalTrademarks.'&nbsp;</td></tr>';
	}
	
	//<tr><td width=200 nowrap>FileMajorPart</td><td  nowrap>'.$FileMajorPart.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>FileMinorPart</td><td  nowrap>'.$FileMinorPart.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>FilePrivatePart</td><td  nowrap>'.$FilePrivatePart.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>PrivateBuild</td><td  nowrap>'.$PrivateBuild.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>ProductBuildPart</td><td  nowrap>'.$ProductBuildPart.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>ProductMajorPart</td><td  nowrap>'.$ProductMajorPart.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>ProductMinorPart</td><td  nowrap>'.$ProductMinorPart.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>ProductPrivatePart</td><td  nowrap>'.$ProductPrivatePart.'&nbsp;</td></tr>

	if($FileBuildPart<>'' && $FileBuildPart<>0) {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Версия</td><td>'.$FileBuildPart.'&nbsp;</td></tr>';
	}

	if($FileVersion<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Версия файла</td><td>'.$FileVersion.'&nbsp;</td></tr>';
	}
		
	if($ProductVersion<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Версия продукта</td><td>'.$ProductVersion.'&nbsp;</td></tr>';
	}
	
	//<tr><td width=200 nowrap>SpecialBuild</td><td  nowrap>'.$SpecialBuild.'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>Отладочная (IsDebug)</td><td  nowrap>'.ret_txt_bool($IsDebug).'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>IsPatched</td><td  nowrap>'.ret_txt_bool($IsPatched).'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>IsPreRelease</td><td  nowrap>'.ret_txt_bool($IsPreRelease).'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>IsPrivateBuild</td><td  nowrap>'.ret_txt_bool($IsPrivateBuild).'&nbsp;</td></tr>
	//<tr><td width=200 nowrap>IsSpecialBuild</td><td  nowrap>'.ret_txt_bool($IsSpecialBuild).'&nbsp;</td></tr>
	
	if($FileDescription<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Описание</td><td>'.$FileDescription.'&nbsp;</td></tr>';
	}
	
	if($Comments<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Комментарий</td><td>'.$Comments.'&nbsp;</td></tr>';
	}

	if($out_temp<>'') {
	$out_temp='<tr><td width=577px colspan=2 nowrap align=center>Дополнительная информация из других источников</td></tr>'.$out_temp;
	}

//Дополнительная инфо кончилась, ищем на других сайтах
	$out_temp=$out_temp.'<tr><td width=577px colspan=2 nowrap align=center>Поиск информации на других сайтах</td></tr>';

	if(mb_strtolower($Extension)=='.exe') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Процесс на ThreatExpert</td><td><noindex><a href="http://www.threatexpert.com/report.aspx?md5='.$Hash.'" title="Автоматическая система анализа приложений">ThreatExpert Report</a> &#151; англ. яз.</noindex>&nbsp;</td></tr>';
	}

	if(mb_strtolower($Extension)=='.exe') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Статистика на ThreatExpert</td><td><noindex><a href="http://www.threatexpert.com/files/'.$ShortName.'.html" title="Сводная статистика">ThreatExpert Statistics</a> &#151; англ. яз.</noindex>&nbsp;</td></tr>';
	}

	if($ShortName<>'') {
	$out_temp=$out_temp.'<tr><td width=200 nowrap>Статистика по имени</td><td><noindex><a href="http://file.ikaka.com/filenameinfo/filenameinfo.aspx?fn='.$ShortName.'" title="Аналогичная поисковая система">Ikaka Report</a> &#151; китайск. яз.</noindex>&nbsp;</td></tr>';
	}



	$out_t=$out_t.$out_temp.'</table><br />';
}

$out_t=$out_t.$no_found;
mysql_close($Link_Id);//Завершаем работу с БД
$array_ret = array("title" => $w_pr, "text" => $out_t);
return $array_ret;
}

//-----------------имя таблицы--кодир.--Текущ. стр.--имя базы--имя хоста--юзер ДБ--пароль ДБ--Штук на стр.---Цвет фона
function get_pages($table_name, $enc,   $id_start,   $db_n,    $db_hn,   $db_u,    $db_p,     $num_per_page, $bg_color)//Имя таблицы, кодировка, имя словаря
{

define("START_TIME", microtime(true));//Начало работы скрипта времени
$out_t='';
//----------------------------------------------------------- Подготовка работы с БД нач.
$Link_Id=mysql_connect($db_hn, $db_u, $db_p);
mysql_select_db($db_n, $Link_Id);
mysql_query("Set names '$enc'; ", $Link_Id);
mysql_query ("set character_set_client='$enc'", $Link_Id);
mysql_query ("set character_set_results='$enc'", $Link_Id);
mysql_query ("set collation_connection='".$enc."_general_ci'", $Link_Id);
//----------------------------------------------------------- Подготовка работы с БД кон.


$sum_rows = mysql_fetch_row(mysql_query('SELECT COUNT(idFile) FROM '.$table_name, $Link_Id));//всего записей

$id_start = htmlspecialchars($id_start);//текущая страница, с 1..N
if($id_start<1)
{
   return array("title" => 'Error formatting Page', "text" => '<table class="list_stats" border=1 bordercolor=black><tr><td><font color=red><b>ОШИБКА!</b><br />Страницы с таким номером не существует</font><br /></td></tr></table><br />');
}
$find_id_start=($num_per_page*($id_start-1));//с которой начинать отчет
$result = mysql_query('SELECT * FROM '.$table_name.' ORDER BY idFile DESC LIMIT '.$find_id_start.', '.$num_per_page.'', $Link_Id);//TODO!!! указать только нужные
$find_id_start=$find_id_start+1;
$num_rows=mysql_num_rows($result);//сколько нашли (на случай конца таблицы)

define("STOP_TIME", microtime(true)-START_TIME);//Конец работы скрипта времени

$last_num=$find_id_start+$num_per_page-1;
if(ceil($sum_rows[0]/$num_per_page)==$id_start)
{
$find_id_start++;
$last_num=$sum_rows[0];
}
$title = 'Страница '.$id_start.'. Записи с '.$find_id_start.' по '.$last_num;

$links='<table class="list_stats"><tr><td align=center>'.print_links($sum_rows[0], $id_start, $num_per_page, 14, $id_start).'</td></tr></table>';//Печать ссылок на страницы

$no_found = '<table class="list_stats"><tr><td>Этого файла пока нет в базе данных.<br /><b>Не забывайте, что имя файла как и MD5 хеш нужно вводить полностью (хеш 32 знака, имя файла вместе с расширением).<br /><br />Правильно: explorer.exe</b></td></tr></table>';

for($i = 0; $i < $num_rows; $i++)
{
	$IdFile=htmlspecialchars(mysql_result($result, $i, 'IdFile'));
	$isChecked = htmlspecialchars(mysql_result($result, $i, 'isChecked'));
	if($isChecked==0) {
	$isChecked='&#151; файл безопасен';
	$tb_bg_color='#FDDFBF'; }
	else {
	$isChecked='неизвестно'; }

	$no_found='';
	$n_var=$i+$find_id_start;
	$out_t=$out_t.'<table class="list_files"><tr valign=top><td align=left valign=top>';


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


//логика составления сообщения
if($FileVersion<>'')
{
  $FileVersion='(версия '.$FileVersion.')';
}

if($FileDescription<>'' && !strstr($FileDescription, '???'))
{
  $FileDescription='&#151; '.$FileDescription;
}
else
{
  $FileDescription='';
}

$ProductName=trim(str_replace('?', '', $ProductName));//Если кодировка не подойдет
if($ProductName<>'')
{
  
  $ProductName='В составе продукта <b>'.$ProductName.'</b>';
  if($ProductVersion<>'')
  {
    $ProductName=$ProductName.' <b>(версия '.$ProductVersion.')</b>';
  }
}
if($ProductName<>'')
{
$ProductName=$ProductName.'<br />';
}

$LegalCopyright=trim(str_replace('?', '', $LegalCopyright));//Если кодировка не подойдет
if($LegalCopyright<>'')
{
  if($LegalCopyright=='©  .   .')//такая штука встрачается в виндовс неизвестного языка
  {
    $LegalCopyright='';
  }
  else
  {
    $LegalCopyright='Корпорации <b>'.$LegalCopyright.'</b>';
  }
}

$LegalTrademarks=trim(str_replace('?', '', $LegalTrademarks));//Если кодировка не подойдет
if($LegalTrademarks<>'')
{
  if($LegalCopyright<>'')
  {
    $LegalCopyright=$LegalCopyright.' <b>('.$LegalTrademarks.')</b>';
  }
  else
  {
    $LegalCopyright='Торговая марка: <b>'.$LegalTrademarks.'</b>';
  }
}
if($LegalCopyright<>'')
{
$LegalCopyright=$LegalCopyright.'<br />';
}

	$out_t=$out_t.
'<li type="1" value="'.$n_var.'"><a href="?id=1&md5='.$Hash.'" title="Дополнительная информация">'.$ShortName.' '.$FileVersion.' '.$FileDescription.'</a><br />
MD5: <b>'.$Hash.'</b><br />
Дата проверки: <b>'.$CheckedDate.'</b><br />
'.$ProductName.'
'.$LegalCopyright.'
Доверие к файлу <b>'.$isChecked.'</b>. Номер в базе: '.$IdFile.'<br /></li>';

	$out_t=$out_t.'</td></tr></table><br />';
}

$stat='<table class="list_stats"><tr><td align=center nowrap>'.$res_obj.' '.$title.'.&nbsp;Показано:&nbsp;'.$num_rows.' из '.$sum_rows[0].', затрачено '.round(STOP_TIME, 4).' секунд.<br /></td></tr></table>';

if($out_t<>'')
{
  $out_t=$links.'<ol>'.$out_t.'</ol>'.$links;
}
$out_t=$stat.$out_t.$no_found;

mysql_close($Link_Id);//Завершаем работу с БД
$array_ret = array("title" => $title, "text" => $out_t);
return $array_ret;
}

//------------------------имя таблицы--кодир.--MD5----имя базы--имя хоста--юзер ДБ--пароль ДБ--
function get_fileinfo_img($table_name, $enc,   $qhash, $db_n,    $db_hn,   $db_u,    $db_p)//Имя таблицы, кодировка, имя словаря
{
//----------------------------------------------------------- Подготовка работы с БД нач.
$Link_Id=mysql_connect($db_hn, $db_u, $db_p);
mysql_select_db($db_n, $Link_Id);
mysql_query("Set names '$enc'; ", $Link_Id);
mysql_query ("set character_set_client='$enc'", $Link_Id);
mysql_query ("set character_set_results='$enc'", $Link_Id);
mysql_query ("set collation_connection='".$enc."_general_ci'", $Link_Id);
//----------------------------------------------------------- Подготовка работы с БД кон.

if(mb_strlen($qhash)<>32)
{
	return array('Hash' => 'Error Hash', 'ShortName' => 'Error', 'Length' => 'Error');
}

$result = mysql_query('SELECT ShortName, Hash, Length FROM '.$table_name.' WHERE Hash = \''.htmlspecialchars($qhash).'\'', $Link_Id);//
$num_rows=mysql_num_rows($result);//сколько нашли (на случай конца таблицы)

if($num_rows > 0)
{
	$Hash=htmlspecialchars(mysql_result($result, 0, 'Hash'));
	$ShortName=htmlspecialchars(mysql_result($result, 0, 'ShortName'));
	$Length=htmlspecialchars(mysql_result($result, 0, 'Length'));
}

mysql_close($Link_Id);//Завершаем работу с БД
return array("Hash" => $Hash, "ShortName" => $ShortName, "Length" => $Length);
}

function print_links($total, $page, $number, $links, $id_start) //1 аргумент - всего сообщений, 2 - номер текущей страницы, 3 - число сообщений на странице, 4 - число отображаемых ссылок, 5 - текущая страница
{
  $return = null;
  $start = 0;
  $finish = 0;

  $pages=$total / $number;
  $pages = ceil($pages);//Вычисляем сколько должно получиться страниц 

  $pgali=$page+$links;
  if ($pgali <= $pages)
  {
    $start = $page-$links;
    $finish = $page+$links;
  }
  else
  {
    $start = $pages - ($links - 1);
    $finish = $pages;
  }

  if ($start < 0)
  {
    $start = 1;
  }
  for ($j=$start; $j<=$finish; $j++) //Записываем в переменную ссылки
  {
    if($id_start==$j)
    {
      $return .= ' <b>'.$j.'</b> ';
    }
    else
    {
      $return .= ' <a href="?page_id='.$j.'">'.$j.'</a> ';
    }
  }
  
  $link_to_prev='';
  $first_link = '';
  if($id_start==1)
  {
    $link_to_prev=' < ';
    $first_link=' << ';
  }
  else
  {
    $prev_page=$id_start-1;
    $link_to_prev=' <a href="?page_id='.$prev_page.'" title="Предыдущая страница"><</a> ';
    $first_link=' <a href="?page_id=1" title="Первая страница"><<</a> ';
  }
  
  $link_to_fwd='';
  $last_link='';
  if($id_start==$pages)
  {
    $link_to_fwd=' > ';
    $last_link=' >> ';
  }
  else
  {
    $next_page=$id_start+1;
    $link_to_fwd=' <a href="?page_id='.$next_page.'" title="Следующая страница">></a> ';
    $last_link=' <a href="?page_id='.$pages.'" title="Последняя страница">>></a> ';
  }
  

  return $first_link.$link_to_prev.$return.$link_to_fwd.$last_link;
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