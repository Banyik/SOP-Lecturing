<?php

include("./database.php");

$request = $_SERVER['REQUEST_METHOD'];

switch($request){

    case "GET":
        echo json_encode(
            array(
                'users' => getUsers()
            )
        );
        break;
    case "POST":
        if(!empty($_POST["username"]) && !empty($_POST["password"])){
            if(!userExists($_POST["username"])){
                addUser($_POST["username"], $_POST["password"]);
            }
            else{
                echo "user already exists.";
            }
        }
        else{
            echo "Bad args.";
        }
        break;
    case "PUT":
        $content = file_get_contents('php://input');
        $data = json_decode($content, true);

        if(!empty($data["id"]) && !empty($data["username"])){
            updateUser($data["id"], $data["username"], $data["password"]);
        }
        else{
            echo "Bad args.";
        }        
        break;
    case "DELETE":
        $content = file_get_contents('php://input');
        $data = json_decode($content, true);

        if(!empty($data["id"]) && !empty($data["username"])){
            if(userExists($data["username"])){
                deleteUser($data["id"]);
            }
            else{
                echo "user doesn't exists.";
            }
        }
        else{
            echo "Bad args.";
        }  
        break;
    default:
        echo "Bad request.";
        break;

}

function getUsers(){
    global $connect;

    $result = $connect -> query("SELECT * FROM users;");

    return $result -> fetch_all(MYSQLI_ASSOC);
}

function addUser($username, $password){
    global $connect;

    $result = $connect -> query("INSERT INTO users (username, password) VALUES ('$username', MD5('$password'));");
}

function updateUser($id, $username, $password){
    global $connect;

    $count = count($connect -> query("SELECT * FROM users WHERE username = '$username' AND ID != '$id';") -> fetch_all(MYSQLI_ASSOC));
    if($count == 0){
        if($password == ""){
            $connect -> query("UPDATE users SET username = '$username' WHERE ID = '$id';");
        }
        else{
            $connect -> query("UPDATE users SET username = '$username', password = MD5('$password') WHERE ID = '$id';");
        }
    }
    else{
        echo "user already exists.";
    }
}

function deleteUser($id){
    global $connect;

    $connect -> query("DELETE FROM users WHERE ID = '$id';");
}

function userExists($username){
    global $connect;

    $result = count($connect -> query("SELECT * FROM users WHERE username = '$username';")->fetch_all(MYSQLI_ASSOC));

    return $result > 0;
}

?>