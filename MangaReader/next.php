<?
require 'SabreDAV/vendor/autoload.php';
require 'credentials.php';

use Sabre\DAV\Client;

// Parameters
$serie = $_GET["s"];
$chapter = str_pad($_GET["c"], 4, "0", STR_PAD_LEFT);
$nextPage = str_pad(intval($_GET["p"]) + 1, 2, "0", STR_PAD_LEFT);

// Complete filename
$filename = '/'.$serie.'/'.$chapter.'/'.$chapter.'-'.$nextPage.'.jpg';

// Set credentials
$settings = getCredentials($serie, $chapter);

$client = new Client($settings);
$response = $client->request('GET', $filename); 
$statusCode = $response["statusCode"];
if ($statusCode != 200)
{
	// We try the next chapter
	$chapter = intval($chapter) + 1;
	$nextPage = 1;
}

$url = 'read.php?s='.$serie.'&c='.intval($chapter).'&p='.intval($nextPage);
echo $url;
?>