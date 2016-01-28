<?
	// Redirect to mobile website if necessary
	$iphone = strpos($_SERVER['HTTP_USER_AGENT'],"iPhone");
	$android = strpos($_SERVER['HTTP_USER_AGENT'],"Android");
	$palmpre = strpos($_SERVER['HTTP_USER_AGENT'],"webOS");
	$berry = strpos($_SERVER['HTTP_USER_AGENT'],"BlackBerry");
	$ipod = strpos($_SERVER['HTTP_USER_AGENT'],"iPod");
	$winphone = strpos($_SERVER['HTTP_USER_AGENT'],"Windows Phone");
	
	$isKindle = strpos($_SERVER['HTTP_USER_AGENT'],"Kindle");

	$isMobile = !$isKindle && ($iphone || $android || $palmpre || $ipod || $berry || $winphone);

	$expire=time()+60*60*24*30;
	
	// Current serie
	$serie = $_GET["s"];

	// Current chapter
	$chapter = "0001";
	$cookieName = $serie."-c";
	if (isset($_GET["c"]))
	{
		$chapter = str_pad($_GET["c"], 4, "0", STR_PAD_LEFT);
		setcookie($cookieName, $_GET["c"], $expire);
	}
	else if (isset($_COOKIE[$cookieName]))
	{
		$chapter = str_pad($_COOKIE[$cookieName], 4, "0", STR_PAD_LEFT);
	}

	// Current page
	$page = "01";
	$cookieName = $serie."-p";
	if (isset($_GET["p"]))
	{
		$page = str_pad($_GET["p"], 2, "0", STR_PAD_LEFT);
		setcookie($cookieName, $_GET["p"], $expire);
	}
	else if (isset($_COOKIE[$cookieName]))
	{
		$page = str_pad($_COOKIE[$cookieName], 2, "0", STR_PAD_LEFT);
	}

	// Image Src
	$imageSrc = 'image.php?s='.$serie.'&c='.$chapter.'&p='.$page;
	
	// Next url
	$nextUrl = "http://$_SERVER[HTTP_HOST]$_SERVER[REQUEST_URI]";
	$index = strpos($nextUrl, "read.php");
	$nextUrl = substr($nextUrl, 0, $index).'next.php?s='.$serie.'&c='.$chapter.'&p='.$page;
	$nextUrl = file_get_contents($nextUrl);
	
	// Next image src
	$nextImageSrc = str_replace("read.php", "image.php", $nextUrl);
?> 
<html>

	<head>
<?
	echo '<title>Paloo-Otaku - ['.$serie.'] Chapter '.ltrim($chapter, "0").' Page '.ltrim($page, "0").'</title>';
?>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />

<?
	if ($isMobile)
		echo '<link rel="stylesheet" href="mobile.css" type="text/css" media="screen"/>';
	else if ($isKindle)
		echo '<link rel="stylesheet" href="kindle.css?v=1" type="text/css" media="screen"/>';
	else
		echo '<link rel="stylesheet" href="station.css" type="text/css" media="screen"/>';
		
	echo '<style type="text/css">#preload {background-image:url('.$nextImageSrc.')}</style>';
?>
	</head>

<body>
<div id="content">
<?
	echo '<a href="'.$nextUrl.'"><img src="'.$imageSrc.'" /></a>';
?>
</div>
<div id="preload"></div>

</body>
</html>
