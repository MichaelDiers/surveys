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
        subViews(element);
      }).catch((error) => console.log(error)); // eslint-disable-line no-console
    });
  }
};

subViews(document);
