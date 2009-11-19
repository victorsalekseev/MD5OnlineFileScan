<!--/ПОИСК-->
<table class="list_stats"><tr><td align=center><?php echo $sape->return_links(1); ?></td></tr></table>
<br />
</td>
<td valign="top" width="20px"></td>
<td valign="top" width="400px">
<!--Это будет осн.текстом и контентом-->
<h2 style="margin-top: 8px">Новости</h2>
<font class="news_desc"><b>05.01.2010&nbsp;&#151;&nbsp;Изменения в выдаваемой информации.</b><br /></font>
<font class="news_body">В свойствах файла добавлены ссылки на другие аналогичные поисковики.</font>
<br />
<br />
<font class="news_desc"><b>16.11.2009&nbsp;&#151;&nbsp;Доступна для загрузки программа MD5-Scan.</b><br />
Программа похожа на антивирусный сканер с той лишь разницей, что Вы сами можете добавлять &laquo;сигнатуры&raquo; файлов.</font><br />
<font class="news_body">Возможные отрасли применения: поиск изменившихся файлов после обновления, поиск администратором спрятанных файлов (например, музыки), поиск копий важных документов и другое. В базу уже включено 207876 MD5-сумм файлов (всех тех, которые видите на сайте).</font>
<br />
<br />
<font class="news_desc"><b>06.11.2009&nbsp;&#151;&nbsp;Адрес обратной связи.</b><br />
Замечания, пожелания, претензии отправляйте на адрес <img src='/ml.png' border=0>.</font>
<br />

<?php
if(mb_strlen($inf_code)==32 && eregi("^[0-9,a-f]+$", $inf_code))
{
echo '
<h2 style="margin-top: 8px">Информер</h2>
Вы можете поместить его на свой сайт. Изменить цвет можно <a href="http://www.rootkits.ru/informer/?id_file='.$inf_code.'" title="Информеры">на этой странице</a>.
<br />
<center><img src="http://www.rootkits.ru/info/'.$inf_code.'_0.gif"></center>

Ссылка для форума:<br />
<input type=text onclick=\'select()\' name=sel1  style=\'width:350px\' value=\'[URL=http://www.rootkits.ru/?id=1&md5='.$inf_code.'][IMG]http://www.rootkits.ru/info/'.$inf_code.'_0.gif[/IMG][/URL]\'><br />
HTML для блога:<br />
<input type=text onclick=\'select()\' name=sel2  style=\'width:350px\' value=\'<a href="http://www.rootkits.ru/?id=1&md5='.$inf_code.'"><img src="http://www.rootkits.ru/info/'.$inf_code.'_0.gif" border=0>\'></a><br />';
}
?>

</td>

</tr>
</table>
<!--/ПОИСК И НОВОСТИ-->

<table width="1000px" height="34" border="0" cellpadding="0" cellspacing="0" align=center background="/images/rtk_05.gif">
<tr height="34" width="1000">
<td width="700" valign=middle>&nbsp;<?php echo $sape->return_links(); ?></td>
<td width="300" valign=middle align=right>&copy; Rootkits.RU.</td></tr>
</table>


</body>
</html>