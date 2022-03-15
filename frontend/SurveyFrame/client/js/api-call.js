// eslint-disable-next-line no-unused-vars
function apiCall(url, method, options = {}) {
  return new Promise((resolve, reject) => {
    // eslint-disable-next-line no-undef
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = (event) => {
      if (event.target.readyState === 4) {
        if (event.target.status === 200) {
          let result;
          if (event.target.responseText) {
            const contentType = xhttp.getResponseHeader('content-type');
            if (contentType.startsWith('text/html')) {
              result = event.target.responseText;
            } else {
              result = JSON.parse(event.target.responseText);
            }
          }
          resolve(result);
        } else {
          reject();
        }
      }
    };

    xhttp.open(method, url, true);
    xhttp.withCredentials = true;
    // xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    const { formData } = options;
    if (formData) {
      xhttp.setRequestHeader('CSRF-Token', formData.get('_csrf'));
      xhttp.send(formData);
    } else {
      xhttp.send();
    }
  });
}
