// Управление модальными окнами
function openModal(modalId) {
    document.getElementById(modalId).style.display = 'block';
}

function closeModal(modalId) {
    document.getElementById(modalId).style.display = 'none';
}

function switchModal(newModalId) {
    document.querySelectorAll('.modal').forEach(modal => {
        modal.style.display = 'none';
    });
    openModal(newModalId);
}

// Автоматическое управление модалками
document.addEventListener('DOMContentLoaded', () => {
    const authStatus = document.getElementById('auth-status');
    
    if (authStatus) {
        if (authStatus.dataset.loginError === 'true') {
            openModal('login-modal');
        }
        
        if (authStatus.dataset.registerError === 'true') {
            openModal('register-modal');
        }
    }
});

// Валидация форм
document.querySelectorAll('form').forEach(form => {
    form.addEventListener('submit', function(e) {
        if (this.id === 'login-form') {
            if (!validatePhone(this.phone.value)) {
                e.preventDefault();
                showError(this.phone, 'Неверный формат телефона');
            }
        }
    });
});

// Вспомогательные функции
function validatePhone(phone) {
    const cleaned = phone.replace(/[^0-9+]/g, '');
    return /^(\+7|8)\d{10}$/.test(cleaned);
}
// Автоматическое открытие модалок с ошибками
document.addEventListener('DOMContentLoaded', function() {
    const urlParams = new URLSearchParams(window.location.search);
    
    // Для логина
    if(urlParams.has('login_error')) {
        openModal('login-modal');
        showError(document.querySelector('#login-modal input[name="phone"]'), 
                 urlParams.get('login_error'));
    }
    
    // Для регистрации
    if(urlParams.has('register_error')) {
        openModal('register-modal');
        showError(document.querySelector('#register-modal input[name="phone"]'), 
                 urlParams.get('register_error'));
    }
});

// Функции управления ошибками
function showError(element, message) {
    const errorDiv = document.createElement('div');
    errorDiv.className = 'alert alert-danger';
    errorDiv.textContent = message;
    element.parentNode.insertBefore(errorDiv, element.nextSibling);
    element.classList.add('error-input');
}

function clearErrors(modalId) {
    const modal = document.getElementById(modalId);
    modal.querySelectorAll('.alert-danger').forEach(el => el.remove());
    modal.querySelectorAll('.error-input').forEach(el => el.classList.remove('error-input'));
}