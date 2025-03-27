<?php
session_start();
include 'db.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $phone = preg_replace('/[^0-9]/', '', $_POST['phone']);
    $password = $_POST['password'];
    $redirect_url = $_POST['redirect_url'] ?? 'index.php';

    // Сохраняем введенные данные
    $_SESSION['login_form'] = [
        'phone' => $phone,
        'redirect' => $redirect_url
    ];

    // Очищаем предыдущие ошибки
    unset($_SESSION['login_errors']);

    // Проверка наличия данных
    $errors = [];
    if (empty($phone)) $errors['phone'] = 'Введите номер телефона';
    if (empty($password)) $errors['password'] = 'Введите пароль';

    if (!empty($errors)) {
        $_SESSION['login_errors'] = $errors;
        header("Location: $redirect_url");
        exit();
    }

    // Поиск пользователя
    $stmt = $conn->prepare("SELECT * FROM users WHERE phone = ?");
    $stmt->bind_param("s", $phone);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows === 0) {
        $errors['phone'] = 'Пользователь не найден';
        $_SESSION['login_errors'] = $errors;
        header("Location: $redirect_url");
        exit();
    }

    $user = $result->fetch_assoc();

    // Проверка пароля
    if (!password_verify($password, $user['password'])) {
        $errors['password'] = 'Неверный пароль';
        $_SESSION['login_errors'] = $errors;
        header("Location: $redirect_url");
        exit();
    }

    // Успешный вход
    unset($_SESSION['login_form'], $_SESSION['login_errors']);
    $_SESSION['user'] = $user;
    header("Location: $redirect_url");
    exit();
}
?>