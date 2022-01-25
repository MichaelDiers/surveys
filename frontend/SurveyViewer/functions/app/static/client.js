const ajax = (options = {}) => {
  const {
    csrfToken,
    method = 'POST',
    url = '',
  } = options;

  return new Promise((resolve, reject) => {
    const xhttp = new XMLHttpRequest(); // eslint-disable-line no-undef
    xhttp.onreadystatechange = (event) => {
      if (event.target.readyState === 4) {
        if (event.target.status === 200) {
          if (event.target.responseText) {
            resolve(event.target.responseText);
          }
        } else {
          console.log(event);
          reject(
            new Error(`no data: url: ${event.target.responseURL}, status: ${event.target.status}`),
          );
        }
      }
    };

    xhttp.open(method, url, true);
    xhttp.withCredentials = true;
    xhttp.setRequestHeader('CSRF-Token', csrfToken);
    xhttp.send();
  });
};

const createElement = (elementName, innerText, parent, attributes = []) => {
  const element = document.createElement(elementName); // eslint-disable-line no-undef
  element.innerText = innerText;
  attributes.forEach(({ name, value }) => {
    element.setAttribute(name, value);
  });

  parent.appendChild(element);
  return element;
};

const showSurvey = async (survey) => {
  const surveyElement = document.querySelector('#survey'); // eslint-disable-line no-undef
  if (!surveyElement) {
    return;
  }

  createElement('h1', `Hej ${survey.participantName}!`, surveyElement);
  createElement('h2', `Willkommen zur Umfrage ${survey.surveyName}!`, surveyElement);
};

const showClosedSurvey = async (survey) => {
  showSurvey(survey);

  const surveyElement = document.querySelector('#survey'); // eslint-disable-line no-undef
  if (!surveyElement) {
    return;
  }

  createElement('h3', 'Die Umfrage ist bereits geschlossen!', surveyElement);
  const table = createElement('div', null, surveyElement);
  survey.questions.forEach(({ text, choices }) => {
    createElement('div', text, table);
    const choice = choices.find((x) => x.isSelected);
    createElement('div', choice.text, table);
  });
};

const showOpenSurvey = async (survey) => {
  showSurvey(survey);

  const surveyElement = document.querySelector('#survey'); // eslint-disable-line no-undef
  if (!surveyElement) {
    return;
  }

  const form = createElement(
    'form',
    null,
    surveyElement,
    [
      { name: 'method', value: 'POST' },
      { name: 'action', value: './submit' },
    ],
  );

  const csrf = document.querySelector('#csrf'); // eslint-disable-line no-undef
  form.appendChild(csrf.cloneNode(true));
  createElement(
    'input',
    null,
    form,
    [
      { name: 'type', value: 'hidden' },
      { name: 'value', value: survey.participantId },
      { name: 'id', value: 'participantId' },
      { name: 'name', value: 'participantId' },
    ],
  );

  survey.questions.forEach(({ id, text, choices }) => {
    createElement('label', text, form, [{ name: 'for', value: id }]);
    const select = createElement(
      'select',
      null,
      form,
      [
        { name: 'id', value: id },
        { name: 'name', value: id },
        { name: 'required', value: 'required' },
      ],
    );
    choices.forEach(({ isSelected, text: choiceText, value }) => {
      createElement(
        'option',
        choiceText,
        select,
        [
          { name: 'value', value },
          { name: 'selected', value: isSelected },
        ],
      );
    });
  });

  createElement(
    'input',
    null,
    form,
    [
      { name: 'type', value: 'submit' },
      { name: 'value', value: 'Abstimmen' },
    ],
  );
};

const csrfElement = document.getElementById('csrf'); // eslint-disable-line no-undef

if (csrfElement) {
  const csrfToken = csrfElement.getAttribute('value');
  ajax({ csrfToken }).then((result) => {
    const survey = JSON.parse(result);
    if (survey.isClosed) {
      showClosedSurvey(survey).catch((e) => console.log(e));
    } else {
      showOpenSurvey(survey).catch((e) => console.log(e));
    }
  }).catch((error) => {
    console.log('ERROR');
    console.log(error);
  });
}
