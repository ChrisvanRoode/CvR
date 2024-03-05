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
    var i, L = selectTag.options.length - 1;
    for(i = L; i >= 0; i--) {
        selectTag.remove(i);
    }

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
                console.log("user", user);
                const listUser = document.createElement('li');
                listUser.textContent = user.id + ' ' + user.firstName + ' ' + user.lastName + ' ' + user.telNumber + ' ' + user.email + ' ' + user.role.title;
                usersList.appendChild(listUser);
            });
        })
        .catch(error => console.error('Error fetching users:', error));
}

document.getElementById('createUserForm').addEventListener('submit', function (e) {
    e.preventDefault();
    var userFirstName = document.getElementById('userFirstName').value
    var userLastName = document.getElementById('userLastName').value
    var userPhoneNumber = document.getElementById('userPhoneNumber').value
    var userEmail = document.getElementById('userEmail').value
    var userRole = document.getElementById('userRoles').value
    var roleId = document.getElementById('userRoles').value
    fetch('/api/users?roleId=' + roleId, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Id: 1, FirstName: userFirstName, LastName: userLastName, TelNumber: userPhoneNumber, Email: userEmail }),
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