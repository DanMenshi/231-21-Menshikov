<?php
session_start();
include 'db.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $phone = preg_replace('/[^0-9]/', '', $_POST['phone']);
    $password = $_POST['password'];
    $confirm_password = $_POST['confirm_password'];
    $agreement = isset($_POST['agreement']);
    $redirect_url = $_POST['redirect_url'] ?? 'index.php';

    // Сохраняем введенные данные
    $_SESSION['register_form'] = [
        'phone' => $phone,
        'agreement' => $agreement,
        'redirect' => $redirect_url
    ];

    // Очищаем предыдущие ошибки
    unset($_SESSION['register_errors']);

    // Валидация данных
    $errors = [];

    // Проверка телефона
    if (empty($phone)) {
        $errors['phone'] = 'Введите номер телефона';
    } elseif (!preg_match('/^7\d{10}$/', $phone)) {
        $errors['phone'] = 'Неверный формат телефона (пример: 79123456789)';
    }

    // Проверка пароля
    if (empty($password)) {
        $errors['password'] = 'Введите пароль';
    } elseif (strlen($password) < 6) {
        $errors['password'] = 'Пароль должен быть не короче 6 символов';
    } elseif ($password !== $confirm_password) {
        $errors['confirm_password'] = 'Пароли не совпадают';
    }

    // Проверка согласия
    if (!$agreement) {
        $errors['agreement'] = 'Необходимо согласие с политикой';
    }

    // Если есть ошибки - сохраняем и возвращаем
    if (!empty($errors)) {
        $_SESSION['register_errors'] = $errors;
        header("Location: $redirect_url");
        exit();
    }

    try {
        // Проверка существования пользователя
        $stmt = $conn->prepare("SELECT id FROM users WHERE phone = ?");
        $stmt->bind_param("s", $phone);
        $stmt->execute();
        
        if ($stmt->get_result()->num_rows > 0) {
            $errors['phone'] = 'Пользователь с этим номером уже существует';
            $_SESSION['register_errors'] = $errors;
            header("Location: $redirect_url");
            exit();
        }

        // Хеширование пароля
        $hashed_password = password_hash($password, PASSWORD_DEFAULT);

        // Сохранение пользователя
        $stmt = $conn->prepare("INSERT INTO users (phone, password) VALUES (?, ?)");
        $stmt->bind_param("ss", $phone, $hashed_password);
        $stmt->execute();

        // Автовход после регистрации
        unset($_SESSION['register_form'], $_SESSION['register_errors']);
        $_SESSION['user'] = $phone;
        header("Location: $redirect_url");
        exit();

    } catch(Exception $e) {
        $errors['general'] = 'Ошибка регистрации: ' . $e->getMessage();
        $_SESSION['register_errors'] = $errors;
        header("Location: $redirect_url");
        exit();
    }
}
?>