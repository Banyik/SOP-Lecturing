<?php
include("./database.php");

$request = $_SERVER['REQUEST_METHOD'];

switch($request){

    case "GET":
        if(!empty($_GET["username"]) && !empty($_GET["password"])){
            if(userExists($_GET["username"], $_GET["password"])){
                echo json_encode(
                    array(
                        'error' => 0,
                        'message' => "Successfully logged in!"
                    )
                );
            }
            else{
                echo json_encode(
                    array(
                        'error' => 1,
                        'message' => "User not found!"
                    )
                );
            }
        }
        else{
            echo json_encode(
                array(
                    'error' => 1,
                    'message' => "Bad Args!"
                )
            );
        }
        break;
    default:
    echo json_encode(
        array(
            'error' => 1,
            'message' => "Bad Request!"
        )
    );
    break;
}

function userExists($username, $password){
    global $connect;

    $result = count($connect -> query("SELECT * FROM users WHERE username = '$username' AND password = md5('$password');")->fetch_all(MYSQLI_ASSOC));

    return $result > 0;
}
?>