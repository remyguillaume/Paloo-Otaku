<?php

require 'SabreDAV/vendor/autoload.php';
require 'credentials.php';

use Sabre\DAV\Client;

// Current serie
$serie = $_GET["s"];

// Current chapter
$chapter = $_GET["c"];
$chapter = str_pad($_GET["c"], 4, "0", STR_PAD_LEFT);

// Current page
$page = $_GET["p"];
$page = str_pad($_GET["p"], 2, "0", STR_PAD_LEFT);

// Complete filename
$filename = '/'.$serie.'/'.$chapter.'/'.$chapter.'-'.$page.'.jpg';

// Set credentials
$settings = getCredentials($serie, $chapter);

$client = new Client($settings);
$response = $client->request('GET', $filename); 
$statusCode = $response["statusCode"];
if ($statusCode == 200)
{
	header("Content-Type: image/jpg");
	echo $response["body"];
}
else
{
	echo $statusCode;
}

?>


