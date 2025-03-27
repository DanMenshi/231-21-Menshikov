<?php
session_start();
// Включение отображения ошибок
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

if (!isset($_SESSION['user'])) {
    echo "Пользователь не авторизован!";
    exit();
}

include 'db.php';

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Мой профиль</title>
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
        <button class="logout-button" onclick="window.location.href='logout.php'">Выход</button>
    </div>
</header>
<main>
    <section class="profile-container">
        <h2>Мой профиль</h2>
            <?php if(isset($_SESSION['profile_errors'])): ?>
                <div class="alert alert-danger">
                    <?php foreach($_SESSION['profile_errors'] as $error): ?>
                        <p><?= $error ?></p>
                    <?php endforeach; ?>
                    <?php unset($_SESSION['profile_errors']); ?>
                </div>
            <?php endif; ?>
        <div class="profile-info">
            <div class="form-group">
                <strong>Телефон:</strong> <?php echo $user['phone']; ?>
            </div>
            <div class="form-group">
            <strong>Имя:</strong> 
                <?php echo !empty($user['name']) ? htmlspecialchars($user['name']) : 'Не указано'; ?>
            <div class="form-group">
                <strong>Дата регистрации:</strong> <?php echo $user['registration_date']; ?>
            </div>
        </div>
        <button class="edit-profile-button" onclick="openModal('edit-profile-modal')">Изменить профиль</button>
    </section>
    <section class="bookmarks-container">
        <h2>Мои закладки</h2>
        <div class="bookmarks-list">
            <?php if (empty($bookmarks)): ?>
                <p>У вас пока нет закладок.</p>
            <?php else: ?>
                <?php foreach ($bookmarks as $bookmark): ?>
                    <div class="bookmark-item">
                        <h3><?php echo htmlspecialchars($bookmark['event_name']); ?></h3>
                        <p><strong>Дата:</strong> <?php echo htmlspecialchars($bookmark['event_date']); ?></p>
                        <p><strong>Место:</strong> <?php echo htmlspecialchars($bookmark['event_location']); ?></p>
                        <button class="bookmark-button" data-event-id="<?php echo $bookmark['event_id']; ?>" data-event-type="<?php echo $bookmark['event_type']; ?>" data-action="remove">Убрать из закладок</button>
                    </div>
                <?php endforeach; ?>
            <?php endif; ?>
        </div>
    </section>
</main>
<!-- Модальное окно изменения профиля -->
<div class="modal" id="edit-profile-modal">
    <div class="modal-content">
        <span class="close" onclick="closeModal('edit-profile-modal')">x</span>
        <h2>Изменить профиль</h2>
        <form action="profile.php" method="POST" class="profile-form">
            <div class="form-group">
                <label for="name">Изменить имя:</label>
                <input type="text" name="name" id="name" value="<?php echo $user['name']; ?>" required>
            </div>
            <div class="form-group">
                <label for="current_password">Текущий пароль:</label>
                <input type="password" name="current_password" id="current_password" required>
            </div>
            <div class="form-group">
                <label for="new_password">Новый пароль:</label>
                <input type="password" name="new_password" id="new_password">
            </div>
            <div class="form-group">
                <label for="confirm_password">Подтвердите пароль:</label>
                <input type="password" name="confirm_password" id="confirm_password">
            </div>
            <button type="submit" name="change_profile">Сохранить изменения</button>
        </form>
    </div>
</div>
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
