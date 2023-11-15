<?php
    $host = "localhost";
    $port = 3306;
    $socket = "";
    $user = "root";
    $password = "";
    $dbname = "server-db";

    $connect = new mysqli($host, $user, $password, $dbname, $port, $socket)
        or die ("Connection has failed: ".mysqli_connect_error());

    if($connect -> connect_errno > 0){
        printf("Connection has failed %\n:", $connect->connect_error);
        exit();
    }
?>
