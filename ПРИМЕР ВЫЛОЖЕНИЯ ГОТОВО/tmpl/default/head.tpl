<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title><?php echo $sl_title ?></title>
<meta name="Document-state" content="Dynamic" /><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="Pragma" content="no-cache" />
<link rel="stylesheet" href="./tmpl/<?php echo $theme ?>/style.css" type="text/css" />
</head>
<style type="text/css">
body {
	margin : 0; 
	padding : 0; 
	font : Tahoma, Helvetica, sans-serif; 
	font-size : 13px; 
}

td {
	font : Tahoma, Helvetica, sans-serif; 
	font-size : 13px; 
}
.hash {
	font : Courier New; 
	font-size : 13px; 
}
</style>
<body>
<?php
$id = $_GET[id];
$text = $_GET[text];
$id = htmlspecialchars($id);
$text = htmlspecialchars($text);

?>
<table border=0 cellpadding=0 cellspacing=0 width=400><tr><td align=center nowrap>
<form method="get" action="h.php">
<select name="id"style="width:60px">
<option value="1"<?php if ($id==1) echo ' selected'; ?>>MD5</option>
<option value="2"<?php if ($id==2) echo ' selected'; ?>>Имя</option>
</select>
<input type="text" name="text" style="width:290px" value="<?php echo $text; ?>" />
<input type="submit" name="find" value="Найти" style="width:50px" />
</form>
</td></tr></table>
<br />