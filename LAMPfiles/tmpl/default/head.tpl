<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title><?php echo $sl_title ?></title>
<meta name="Document-state" content="Dynamic" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="Pragma" content="no-cache" />

<style>
					.tmenu {height: 30px;}
					.menu2 {margin: 0; padding: 0; width: 190px}
					.menu3 {margin: 0; padding: 0; width: 210px}
					.menu2 div.m3_over, .menu2 div.m3, .menu2 div.item {margin: 0px; background: red;  border-bottom: solid 1px #ffffff;} /* Цвет бекграунда меню и цвет разделительного бордюра*/
					.menu1 .menu2, .menu1_over .menu2 {position: absolute; display: none; z-Index: 100}
					div.m3_over div.menu3, .menu2 div.m3 div.menu3 {position: absolute; padding-left: 1px; margin-top: -20px; left: 187px; display: none; z-Index: 80}
					div.m3 a, div.m3_over a, div.item a {color: #cdcdcd; display: block;	text-decoration: none; padding: 3px 13px 3px 13px} /* Цвет выделенной ссылки */
					div.m3 .menu3 a:hover, div.m3_over a, div.m3:hover a, div.item a:hover {background: #000000;} /* Цвет ссылок и Цвет выделенного бекграунда меню */
					div.m3 .menu3 a, div.m3_over .menu3 a {background: blue} /* Цвет бекграунда 3кр меню */
					
					
					/* Fix IE. Hide from IE Mac */
					* html .menu2 div.item { float: left; }
					* html .menu2 div.item a { height: 1%; }
					* html .menu2 div.m3 { float: left; }
					* html .menu2 div.m3 a{ height: 1%; }
					* html .menu2 div.m3_over { float: left; }
					* html .menu2 div.m3_over a{ height: 1%; }
					/* End */
					
					div.menu1:hover .menu2,  .menu1_over .menu2 {display: block}
					div.m3:hover div.menu3 {display: block}
					div.m3_over div.menu3 {display: block}
					.menu1_over, .menu1 {font-size: 11px; float: left}
					.menu1_over a, .menu1 a {position: relative; color: #000222; text-decoration: none;  display: block; padding: 0px 0px} /* Цвет текста 1ур меню */
					div.menu1:hover, .menu1_over { } 
					.mn2_t {font-size: 1px; line-height: 1px; height: 2px}
					.mn2_b {font-size: 1px; line-height: 1px; height: 2px}
					table {border: none}
					td {vertical-align: top
					font : small Arial, Helvetica, sans-serif; 
					color : black; 
					font-size: 12px
				</style>
<link rel="stylesheet" type="text/css" href="http://rootkits.ru/usr/templates/default.css" />

<script language="javascript" type="text/javascript">
					var agt = navigator.userAgent.toLowerCase ();
					var is_ie      = ((agt.indexOf ("msie") != -1) && (agt.indexOf ("opera") == -1) && (agt.indexOf ("safari") == -1));
					var is_opera = (agt.indexOf ("opera") != -1);
					
					
					startList = function() {
						if (is_ie || is_opera){
							var nodes = document.getElementById("nav").getElementsByTagName("DIV");
							for (var i=0; i < nodes.length; i++) {
								if (nodes[i].className=="menu1" || nodes[i].className=="m3"){
									nodes[i].onmouseover = function() {
										this.className += "_over";
									}
									nodes[i].onmouseout = function() {
										this.className = this.className.replace(new RegExp("_over"), "");
									}
								}
							}
						}
					}


function initw() {
startList(); }
window.onload=initw;
</script>

</head>

<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<?php
$id = htmlspecialchars($_GET[id]);
$text = htmlspecialchars($_GET[text]);

?>

<!--ШАПКА-->
<table width="1000px" border="0" cellpadding="0" cellspacing="0" valign=top align=center>
<tr style="background-color:red">
<td valign="top" Width="162px" Height="58px"><a href="/"><img src="/images/rtk_01.gif" alt="Главная" style="border-width:0px;" /></a></td>
<td valign="top" class="head"></td>
<td valign="top" align="right" Width="14px" Height="58px"><img src="/images/rtk_04.gif" alt="" style="border-width:0px;" /></td>
</tr>
</table>
<!--/ШАПКА-->

<!--МЕНЮ-->
<table width="1000px" height="34" border="0" cellpadding="0" cellspacing="0" align=center background="/images/rtk_05.gif">
<tr height="34" width="1000">
<td valign="top" width="1000">


<div id="nav" class="tmenu" background="/images/rtk_05.gif">

<div class="menu1" style='border-right: solid 1px #FFFFFA;'><table class="pimpa_menu"><tr><td valign=middle align=center><a href="/"><b>Главная</b></a></td></tr></table><div class="menu2"></div></div>


<div class="menu1" style='border-right: solid 1px #FFFFFA;'><table class="pimpa_menu"><tr><td valign=middle align=center><a href="/scan/"><b>Скачать MD5-Scan</b></a></td></tr></table><div class="menu2"></div></div>

<div class="menu1" style='border-right: solid 1px #FFFFFA;'><table class="pimpa_menu"><tr><td valign=middle align=center><a href="/informer/"><b>Информеры</b></a></td></tr></table><div class="menu2"></div></div>

<div class="menu1" style='border-right: solid 1px #FFFFFA;'><table class="pimpa_menu"><tr><td valign=middle align=center><a href="/contacts/"><b>Контакты</b></a></td></tr></table><div class="menu2"></div></div>

<div class="menu1" style='border-right: solid 1px #FFFFFA;'><table class="pimpa_menu"><tr><td valign=middle align=center>&nbsp;</td></tr></table><div class="menu2"></div></div>


<div class="menu1" style='border-right: solid 1px #FFFFFA;'><table class="pimpa_menu"><tr><td valign=middle align=center>&nbsp;</td></tr></table><div class="menu2"></div></div>

<div class="menu1" style='border-right: solid 1px #FFFFFA;'><table class="pimpa_menu"><tr><td valign=middle align=center>&nbsp;</td></tr></table><div class="menu2"></div></div>

<div class="menu1" style='border-right: solid 1px #FFFFFA;'><table class="pimpa_menu"><tr><td valign=middle align=center>&nbsp;</td></tr></table><div class="menu2"></div></div>

</td></tr>
<tr><td class="down_menu"></td></tr>
</table>
<!--/МЕНЮ-->




<table width="1000px" border="0" cellpadding="0" cellspacing="0" align=center valign=top><tr height=7px valign="top"><td></td></tr></table>
<!--ОСН.БЛОК-->
<table width="1000px" border="0" cellpadding="0" cellspacing="0" align=center valign=top>
<tr valign="top">
<td valign="top" width="580px"> <!--Это будет осн.текстом и контентом-->
<h2>MD5-хеши оригинальных «доверенных» файлов</h2>
<font class="news_desc">В данной базе данных собраны хеши оригинальных часто встречающихся «доверенных» файлов. Если Вы думаете, что Ваш компьютер поразил вирус или троян, результатом чего стало изменение файлов программ, то благодаря этой базе можете сравнить хеш оригинального файла с файлом на Вашем компьютере.</font><br />
<font class="news_body">Чтобы получить MD5 хеш, установите программу <noindex><a href="http://beeblebrox.org/HashTab%20Setup.exe">Hashtab</a></noindex>. После инсталляции в свойствах файла добавится вкладка &laquo;Контрольные суммы&raquo;. </font> 

<?php 

   if (!defined('_SAPE_USER')){
        define('_SAPE_USER', 'cb0dfe775744f44e07f561bff6a49ea3'); 
     }
     require_once($_SERVER['DOCUMENT_ROOT'].'/'._SAPE_USER.'/sape.php'); 
     $sape = new SAPE_client();

    echo $sape->return_links(1);

?>

</td>
<td valign="top" width="20px"></td>
<td valign="top" width="400px">
<!--ПОИСК ПО БАЗЕ-->
<table border=0 cellpadding=0 cellspacing=0 width=400px height=115px class="search" align=center><tr>
<td align=center nowrap colspan=3>Поиск в базе данных<br /></td>
</tr>
<tr valign=middle>
<form method="get" action="/h.php">
<td align=center valign=middle style="width:60px;height:35px;" class="in_red" nowrap>Введите<br />Код:</td>
<td align=center valign=middle style="width:175px;height:35px"><img src="/cp/i_show.php?sid=<?php echo md5(uniqid(time())); ?>" style="width:175px;height:35px"></td>
<td align=center valign=middle style="width:162px;height:35px"><input type="text" name="code" style="font-size:24px;width:162px;height:35px;background-color:#cc3333" />
</td>
<tr valign=middle>
<td align=center nowrap colspan=3 class="in_red">Выберите критерий и введите искомый хеш или полное имя файла</td>
</tr>
<tr valign=middle>
<td align=center colspan=3 style="width:400px" nowrap>
<select name="id" style="width:60px;background-color:#cc3333">
<option value="1"<?php if ($id==1) echo ' selected'; ?>>MD5</option>
<option value="2"<?php if ($id==2) echo ' selected'; ?>>Имя</option>
</select>
<input type="text" name="text" style="width:280px;background-color:#cc3333" value="<?php echo $text; ?>" />
<input type="submit" name="find" value="Найти" style="width:50px;background-color:#cc3333" />
</td>
</tr>
<tr valign=middle>
<td align=center nowrap colspan=3 class="in_red">Пример: <a href="http://rootkits.ru/h.php?id=1&text=FA6D8B40A1A730193DFD8780F82C7F27">FA6D8B40A1A730193DFD8780F82C7F27</a> или <a href="http://rootkits.ru/h.php?id=2&text=TrueCrypt.exe">TrueCrypt.exe</a></td>
</form>
</tr></table>
<!--/ПОИСК ПО БАЗЕ-->
</td>

</tr>
</table>
<!--/ОСН.БЛОК-->
<br />

<!--ПОИСК И НОВОСТИ-->
<table width="1000px" border="0" cellpadding="0" cellspacing="0" valign=top align=center>
<tr valign=top>
<td width=580px>
<!--ПОИСК-->
