<!-- Модальное окно входа -->
<div class="modal" id="login-modal">
    <div class="modal-content">
        <span class="close" onclick="closeModal('login-modal')">&times;</span>
        <h2>Вход в профиль</h2>
        <form action="login.php" method="POST">
            <input type="hidden" name="redirect_url" value="<?= $_SERVER['REQUEST_URI'] ?>">
            
            <div class="form-group">
                <input type="text" 
                       name="phone" 
                       placeholder="Телефон"
                       value="<?= htmlspecialchars($_SESSION['login_form']['phone'] ?? '') ?>"
                       class="<?= isset($_SESSION['login_errors']['phone']) ? 'error-input' : '' ?>">
                <?php if(isset($_SESSION['login_errors']['phone'])): ?>
                    <div class="error-message"><?= $_SESSION['login_errors']['phone'] ?></div>
                    <?php unset($_SESSION['login_errors']['phone']); ?>
                <?php endif; ?>
            </div>
            
            <div class="form-group">
                <input type="password" 
                       name="password" 
                       placeholder="Пароль"
                       class="<?= isset($_SESSION['login_errors']['password']) ? 'error-input' : '' ?>">
                <?php if(isset($_SESSION['login_errors']['password'])): ?>
                    <div class="error-message"><?= $_SESSION['login_errors']['password'] ?></div>
                    <?php unset($_SESSION['login_errors']['password']); ?>
                <?php endif; ?>
            </div>
            
            <button type="submit">Войти</button>
        </form>
        <p>Нет аккаунта? <a href="#" onclick="switchModal('register-modal')">Зарегистрироваться</a></p>
    </div>
</div>

<!-- Модальное окно регистрации -->
<!-- Модальное окно регистрации -->
<div class="modal" id="register-modal">
    <div class="modal-content">
        <span class="close" onclick="closeModal('register-modal')">&times;</span>
        <h2>Регистрация</h2>
        
        <?php if(isset($_SESSION['register_errors']['general'])): ?>
            <div class="alert alert-danger">
                <?= $_SESSION['register_errors']['general'] ?>
            </div>
        <?php endif; ?>

        <form action="register.php" method="POST">
            <input type="hidden" name="redirect_url" value="<?= $_SERVER['REQUEST_URI'] ?>">
            
            <!-- Поле телефона -->
            <div class="form-group">
                <input type="text" 
                       name="phone" 
                       placeholder="Телефон (79123456789)"
                       value="<?= htmlspecialchars($_SESSION['register_form']['phone'] ?? '') ?>"
                       class="<?= isset($_SESSION['register_errors']['phone']) ? 'error-input' : '' ?>">
                <?php if(isset($_SESSION['register_errors']['phone'])): ?>
                    <div class="error-message"><?= $_SESSION['register_errors']['phone'] ?></div>
                <?php endif; ?>
            </div>
            
            <!-- Поле пароля -->
            <div class="form-group">
                <input type="password" 
                       name="password" 
                       placeholder="Пароль (мин. 6 символов)"
                       class="<?= isset($_SESSION['register_errors']['password']) ? 'error-input' : '' ?>">
                <?php if(isset($_SESSION['register_errors']['password'])): ?>
                    <div class="error-message"><?= $_SESSION['register_errors']['password'] ?></div>
                <?php endif; ?>
            </div>
            
            <!-- Подтверждение пароля -->
            <div class="form-group">
                <input type="password" 
                       name="confirm_password" 
                       placeholder="Подтвердите пароль"
                       class="<?= isset($_SESSION['register_errors']['confirm_password']) ? 'error-input' : '' ?>">
                <?php if(isset($_SESSION['register_errors']['confirm_password'])): ?>
                    <div class="error-message"><?= $_SESSION['register_errors']['confirm_password'] ?></div>
                <?php endif; ?>
            </div>
            
            <!-- Чекбокс согласия -->
            <div class="form-group agreement-checkbox">
                <label class="<?= isset($_SESSION['register_errors']['agreement']) ? 'error-label' : '' ?>">
                    <input type="checkbox" 
                           name="agreement" 
                           style="width: 20px;"

                           <?= isset($_SESSION['register_form']['agreement']) ? 'checked' : '' ?>>
                    Согласие с <a href="policy.pdf" target="_blank">политикой обработки данных</a>
                </label>
                <?php if(isset($_SESSION['register_errors']['agreement'])): ?>
                    <div class="error-message"><?= $_SESSION['register_errors']['agreement'] ?></div>
                <?php endif; ?>
            </div>
            
            <button type="submit" class="register-button">Зарегистрироваться</button>
        </form>
        
        <p class="auth-switch">Уже есть аккаунт? <a href="#" onclick="switchModal('login-modal', 'register-modal')">Войти</a></p>
    </div>
</div>

<!-- Скрытый блок для передачи статуса -->
<div id="auth-status"
     data-login-error="<?= isset($_SESSION['login_error']) ? 'true' : 'false' ?>"
     data-register-error="<?= isset($_SESSION['register_error']) ? 'true' : 'false' ?>"
     style="display: none;">
</div>

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