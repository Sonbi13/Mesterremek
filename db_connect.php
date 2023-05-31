<?php
    header('Content-Type: text/html; charset=utf-8');

    function db_connect()
	{
		$host = "localhost";
		$user = "root";
		$pwd = "";
		$dbname = "nadfedeles";
        $mysqli = new mysqli($host,$user,$pwd,$dbname);
        if (!$mysqli)
		{
            die("Nem lehet csatlakozni az adatbázishoz!!!".$mysqli->error);
        }
    return $mysqli;
    }
?>