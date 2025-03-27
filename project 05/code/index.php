<?php
session_start();
include 'db.php';
require_once 'modals.php';

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Главная страница</title>
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
        <?php if(isset($_SESSION['user'])): ?>
            <button onclick="window.location.href='profile.php'">Профиль</button>
        <?php else: ?>
            <button class="login-button" onclick="openModal('login-modal')">Войти</button>
        <?php endif; ?>
    </div>
</header>
<main>
    <section class="hero">
        <h1>Афиша мероприятий</h1>
        <p>Выберите дату, чтобы увидеть все мероприятия на эту дату:</p>
        <input type="date" id="event-date" min="<?php echo date("Y-m-d"); ?>">
        <button onclick="filterEvents()">Показать мероприятия</button>
    </section>
    <section class="events" id="events-section">
        <h2>Мероприятия на выбранную дату</h2>
        <table id="events-table">
            <thead>
            <tr>
                <th>Название</th>
                <th>Дата</th>
                <th>Место</th>
            </tr>
            </thead>
            <tbody>
            <!-- Мероприятия будут отображаться здесь -->
            </tbody>
        </table>
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
