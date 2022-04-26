const subViewForms = (sourceElement) => {
  const forms = sourceElement.querySelectorAll('form');
  if (forms) {
    forms.forEach((form) => {
      form.addEventListener('submit', (e) => {
        e.preventDefault();
        const method = e.target.getAttribute('method');
        const action = e.target.getAttribute('action');
        const formData = new FormData(e.target); // eslint-disable-line no-undef
        sourceElement.classList.add('loader');
        apiCall(action, method, { formData }).then((response) => { // eslint-disable-line no-undef
          const subViews = document.querySelectorAll('.sub-view');
          if (subViews) {
            subViews.forEach((subView) => {
              subView.querySelectorAll('form').forEach(async (subViewForm) => {
                if (subViewForm.id && subViewForm.id === form.id) {
                  await new Promise((r) => setTimeout(r, 2000));
                  sourceElement.classList.remove('loader');
                  subView.innerHTML = response; // eslint-disable-line no-param-reassign
                  const pushStateElement = subView.querySelector('[name=pushStateUrl]');
                  if (pushStateElement) {
                    const pushStateUrl = pushStateElement.getAttribute('value');
                    if (pushStateUrl) {
                      window.history.pushState({}, '', pushStateUrl); // eslint-disable-line no-undef
                    }
                  }
                }
              });
            });
          }
        }).catch((e) => console.log(e)); // eslint-disable-line
      });
    });
  }
};

const subViews = (sourceElement) => {
  const elements = sourceElement.querySelectorAll('[placeholderAjax]');
  if (elements) {
    elements.forEach((element) => {
      const url = element.getAttribute('placeholderAjax');
      // element.removeAttribute('placeholderAjax');
      element.classList.add('loader');
      // eslint-disable-next-line no-undef
      apiCall(url, 'GET').then((result) => {
        const htmlElement = element;
        htmlElement.innerHTML = result;
        htmlElement.classList.remove('loader');
        subViewForms(element);
        subViews(element);
      }).catch((error) => console.log(error)); // eslint-disable-line no-console
    });
  }
};

subViews(document);
