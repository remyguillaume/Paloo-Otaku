<?php

function getCredentials($serie, $chapter)
{
	$username = "username";
	$password = "password";

	$settings = array(
		'baseUri' => 'WebDavUrl',
		'userName' => $username,
		'password' => $password,
		'proxy' => '',
	);
	
	return $settings;
}

?>