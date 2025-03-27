<?php
session_start();
include 'db.php';
include 'modals.php';

$event_id = $_GET['id'];
$event_type = $_GET['type'];

$stmt = $conn->prepare("SELECT * FROM $event_type WHERE id = ?");
$stmt->bind_param("i", $event_id);
$stmt->execute();
$result = $stmt->get_result();
$event = $result->fetch_assoc();
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Детали мероприятия</title>
    <link rel="stylesheet" href="css/style.css">
    <!-- Подключаем Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
</head>
<body>
<header>
    <div class="logo" onclick="window.location.href='index.php'">ТвойПутеводитель.py</div>
    <nav>
        <a href="concerts.php">КОНЦЕРТЫ</a>
        <a href="theatre.php">ТЕАТР</a>
        <a href="movies.php">ФИЛЬМЫ</a>
        <a href="sports.php">СПОРТ</a>
        <a href="standup.php">СТЕНДАП</a>
    </nav>
    <div class="user-actions">
        <?php if (isset($_SESSION['user'])): ?>
            <button class="profile-button" onclick="window.location.href='profile.php'">Профиль</button>
        <?php else: ?>
            <button class="login-button" onclick="openModal('login-modal')">Войти</button>
        <?php endif; ?>
    </div>
</header>
<main>
    <section class="event-details">
        <h1><?php echo $event['name']; ?></h1>
        <p><strong>Дата:</strong> <?php echo $event['date']; ?></p>
        <p><strong>Место:</strong> <?php echo $event['location']; ?></p>
        <p><strong>Описание:</strong> <?php echo $event['description']; ?></p>
        <?php if (isset($_SESSION['user'])): ?>
            <?php
            $user_id = $_SESSION['user']['id'];
            $stmt_check = $conn->prepare("SELECT * FROM bookmarks WHERE user_id = ? AND event_id = ? AND event_type = ?");
            $stmt_check->bind_param("iis", $user_id, $event_id, $event_type);
            $stmt_check->execute();
            $result_check = $stmt_check->get_result();
            if ($result_check->num_rows > 0) {
                echo "<button class='bookmark-button' data-event-id='{$event['id']}' data-event-type='{$event_type}' data-action='remove'>Убрать из закладок</button>";
            } else {
                echo "<button class='bookmark-button' data-event-id='{$event['id']}' data-event-type='{$event_type}' data-action='add'>Добавить в закладки</button>";
            }
            ?>
        <?php endif; ?>
    </section>
</main>
<footer>
    <div class="footer-content">
        <div class="about-us">
            <p>О нас: Ваш текст о себе или компании.</p>
        </div>
        <div class="social-media">
            <a href="https://t.me/your_telegram" target="_blank"><i class="fab fa-telegram"></i></a>
            <a href="https://vk.com/your_vk" target="_blank"><i class="fab fa-vk"></i></a>
        </div>
        <div class="contact-info">
            <p>Телефон: +123456789</p>
        </div>
        <div class="copyright">
            <p>&copy; 2024 Все права защищены.</p>
        </div>
    </div>
</footer>
<script src="js/script.js"></script>
</body>
</html>
