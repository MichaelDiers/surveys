const subViewForms = (sourceElement) => {
  const forms = sourceElement.querySelectorAll('form');
  if (forms) {
    forms.forEach((form) => {
      form.addEventListener('submit', (e) => {
        e.preventDefault();
        const method = e.target.getAttribute('method');
        const action = e.target.getAttribute('action');
        const formData = new FormData(e.target); // eslint-disable-line no-undef
        apiCall(action, method, { formData }).then((response) => { // eslint-disable-line no-undef
          const subViews = document.querySelectorAll('.sub-view');
          if (subViews) {
            subViews.forEach((subView) => {
              subView.querySelectorAll('form').forEach((subViewForm) => {
                if (subViewForm.id && subViewForm.id === form.id) {
                  subView.innerHTML = response; // eslint-disable-line no-param-reassign
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
  const elements = sourceElement.querySelectorAll('[placeholder]');
  if (elements) {
    elements.forEach((element) => {
      const url = element.getAttribute('placeholder');
      element.removeAttribute('placeholder');
      // eslint-disable-next-line no-undef
      apiCall(url, 'GET').then((result) => {
        const htmlElement = element;
        htmlElement.innerHTML = result;
        subViewForms(element);
        subViews(element);
      }).catch((error) => console.log(error)); // eslint-disable-line no-console
    });
  }
};

subViews(document);
