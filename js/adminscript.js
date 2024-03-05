document.addEventListener('DOMContentLoaded', function () {
    fetchRoles();
    fetchUsers();
});

/*
=============
Roles
=============
*/

function fetchRoles() {
    let selectTag = document.getElementById('userRoles');

    fetch('/api/roles')
        .then(response => response.json())
        .then(data => {
            const rolesList = document.getElementById('rolesList');
            rolesList.innerHTML = '';
            data.forEach(role => {
                const listRole = document.createElement('li');
                let opt = document.createElement("option");
                opt.value = role.id;
                opt.innerHTML = role.title;
                selectTag.append(opt);
                listRole.textContent = role.id + ' ' + role.title;
                rolesList.appendChild(listRole);
            });
        })
        .catch(error => console.error('Error fetching roles:', error));
}

document.getElementById('createRoleForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const roleTitle = document.getElementById('roleTitle').value;

    fetch('/api/roles', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Id: 1, Title: roleTitle }),
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            fetchRoles();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
});

/*
=============
Users
=============
*/

function fetchUsers() {
    fetch('/api/users')
        .then(response => response.json())
        .then(data => {
            const usersList = document.getElementById('usersList');
            usersList.innerHTML = '';
            data.forEach(user => {
                const listUser = document.createElement('li');
                listUser.textContent = user.id + ' ' + user.firstname + ' ' + user.lastname + ' ' + user.telnumber + ' ' + user.email + ' ' + user.role;
                usersList.appendChild(listUser);
            });
        })
        .catch(error => console.error('Error fetching users:', error));
}

document.getElementById('createUserForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const userFirstName = document.getElementById('userFirstName').value;
    const userLastName = document.getElementById('userLastName').value;
    const userPhoneNumber = document.getElementById('userPhoneNumber').value;
    const userEmail = document.getElementById('userEmail').value;
    const userRole = document.getElementById('userRoles').value;

    fetch('/api/users', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Id: 1, FirstName: userFirstName, LastName: userLastName, TelNumber: userPhoneNumber, Email: userEmail }, userRole),
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            fetchRoles();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
});