document.addEventListener('DOMContentLoaded', function () {
    fetchItems();
});

function fetchItems() {
    fetch('/api/items')
        .then(response => response.json())
        .then(data => {
            const itemsList = document.getElementById('itemsList');
            itemsList.innerHTML = '';
            data.forEach(item => {
                const listItem = document.createElement('li');
                listItem.textContent = item.name + ' ' + item.description + ' ' + item.id;
                itemsList.appendChild(listItem);
            });
        })
        .catch(error => console.error('Error fetching items:', error));
}

document.getElementById('createItemForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const itemName = document.getElementById('itemName').value;
    const itemDescription = document.getElementById('itemDescription').value;

    fetch('/api/items', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Id: 1, Name: itemName, Description: itemDescription }),
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            fetchItems();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
});
