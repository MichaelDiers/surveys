const themeHandler = () => {
  document.querySelectorAll('.theme-toggle').forEach((element) => {
    element.addEventListener('click', (e) => {
      e.preventDefault();
      document.querySelector('body').classList.toggle('light');
    });
  });
};

themeHandler();
