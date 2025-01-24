document.addEventListener('DOMContentLoaded', () => {
    const themeToggle = document.getElementById('themeToggle');
    const currentTheme = localStorage.getItem('theme');

    // Eğer saklanan bir tema varsa, onu uygula
    if (currentTheme === 'dark') {
        document.body.classList.add('dark-mode');
        themeToggle.innerHTML = '<i class="fas fa-moon"></i>'; // Koyu mod simgesi
    }

    themeToggle.addEventListener('click', () => {
        // Koyu mod açıksa kapat, değilse aç
        if (document.body.classList.contains('dark-mode')) {
            document.body.classList.remove('dark-mode');
            themeToggle.innerHTML = '<i class="fas fa-sun"></i>'; // Gündüz simgesi
            localStorage.setItem('theme', 'light');  // Açık tema olarak kaydet
        } else {
            document.body.classList.add('dark-mode');
            themeToggle.innerHTML = '<i class="fas fa-moon"></i>'; // Koyu mod simgesi
            localStorage.setItem('theme', 'dark');   // Koyu tema olarak kaydet
        }
    });
});


