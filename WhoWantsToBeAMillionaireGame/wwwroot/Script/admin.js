
app.controller('myController', function ($scope, $http) {

    $scope.isUpdate = false;
    $scope.isAdd = true;
    $scope.onPageLoad = function () {
        $http.get('/Admin/User/LoadGrid').then(function (response) {
            $scope.userGrid = response.data;
        });
    };
    $scope.createUser = function () {
        var data = {
            Name: $scope.userName,
            Surname: $scope.userSurname,
            Email: $scope.userEmail
        };
        $http({
            method: 'POST',
            url: '/Admin/User/CreateUser',
            data: data,
            dataType: "json",
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function (response) {
            if (response.data == "already have") {
                alert("Bu istifadəçi artıq qeydiyyatdadır !");
                return;
            }
            else {
                alert("Uğurlu əməliyyat!");
                location.reload();
            }

        }, function (response) {
            console.log(response);
        });
    }

    $scope.deleteUser = function (id) {
        $http.delete('/Admin/User/DeleteUser/' + id).then(function (response) {
            if (response.data == "ok") {
                $scope.userGrid = $scope.userGrid.filter(function (user) {
                    return user.id !== id;
                });
            }
        });
    };
    $scope.readUser = function (id) {
        $scope.clearFields();
        $scope.isUpdate = true;
        $scope.isAdd = false;

        $http.get('/Admin/User/GetUser/' + id).then(function (response) {
            $("#user-modal").modal('show');
            $scope.userId = id;
            $scope.userName = response.data.name;
            $scope.userSurname = response.data.surname;
            $scope.userEmail = response.data.email;
        });
    };

    $scope.updateUser = function () {
        console.log($("#hiddenId").val());
        var data = {
            Id: $("#hiddenId").val(),
            Name: $scope.userName,
            Surname: $scope.userSurname,
            Email: $scope.userEmail
        };

        $http({
            method: 'POST',
            url: '/Admin/User/UpdateUser',
            data: data,
            dataType: "json",
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function (response) {
            if (response.data == "already have") {
                alert("Bu istifadəçi artıq qeydiyyatdadır !");
                return;
            }
            else {
                alert("Uğurlu əməliyyat!");
                $scope.isUpdate = false;
                $scope.isAdd = true;
                location.reload();
            }

        }, function (response) {
            console.log(response);
        });
    }
    function showModal(id) {
        $(`#${id}`).modal('show');
    }
    $scope.clearFields = function () {
        $scope.userName = '';
        $scope.userSurname = '';
        $scope.userEmail = '';
    };
});