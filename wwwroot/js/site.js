const uri = 'api/SingolaAttivitas';
let attivita = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Non funziona il get.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('agg-nome');

    const item = {
        nome: addNameTextbox.value.trim(),
        bCompletata: false
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Non riesco ad aggiungere elementi', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Non riesco a cancellare', error));
}

function closeInput() {
    document.getElementById('formmenu').style.display = 'none';
}

function displayEditForm(id) {
    const item = attivita.find(item => item.id === id);

    document.getElementById('nome').value = item.nome;
    document.getElementById('modId').value = item.id;  
    document.getElementById('stato').checked = item.bCompletata;
    document.getElementById('formmenu').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('modId').value;
    const item = {
        Id: parseInt(itemId, 10),
        bCompletata: document.getElementById('stato').checked,
        nome: document.getElementById('nome').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Non riesco ad aggiornare.', error));

    closeInput();

    return false;
}



function _displayCount(itemCount) {
    const name = (itemCount === 0) ? 'nessuna attività' : 'attività';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('attivitaInserite');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isCompleteCheckbox = document.createElement('input');
        isCompleteCheckbox.type = 'checkbox';
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = item.bCompletata;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Modifica';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Cancella';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isCompleteCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.nome);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    attivita = data;
}