<?php
$servername = "localhost";
$username = "root";
$password = ""; // Пароль MySQL (по умолчанию пустой для XAMPP)
$database = "poiskM";
// Подключение к базе данных
$conn = new mysqli($servername, $username, $password, $database);
// Проверка соединения
if ($conn->connect_error) {
    die("Ошибка подключения: " . $conn->connect_error);
}
?>