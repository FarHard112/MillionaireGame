
app.controller('advertiseController', ['$scope', '$http', function ($scope, $http) {

    $scope.adverts = [];
    var advertiseModal = new bootstrap.Modal(document.getElementById('advertiseDetails'));
    $scope.advert = {};
    //Chart Details

    //End ChartDetails
    $scope.onPageLoad = function () {
        $http.get('/Admin/Advertising/LoadGridData').then(function (response) {
            $scope.adverts = response.data.map(function (ad) {
                var baseUrl = window.location.protocol + '//' + window.location.host;
                ad.ImageUrl = baseUrl + ad.ImageUrl;
                return ad;
            });
            console.log(response.data);
        });
    };

    //Form Submit
    $scope.submitForm = function () {
        var formData = new FormData();
        if ($scope.imageFile) {
            formData.append("file", $scope.imageFile);
        }
        for (var property in $scope.advert) {
            if ($scope.advert.hasOwnProperty(property)) {
                formData.append(property, $scope.advert[property]);
            }
        }

        $http.post('/Admin/Advertising/CreateAdvertise', formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (response) {
            alert("Ugurlu emeliyyat !");
            location.reload();
        }, function (error) {
            // Handle error
            console.error('Error uploading the image and submitting the form:', error);
        });

        $scope.clearForm();
    };

    $scope.editAd = function (ad) {

        $scope.advert = angular.copy(ad);
    };

    $scope.makeAdvertiseActive = function (advertId) {
        console.log(advertId);
        $http.post('/Admin/Advertising/ActivateAdvertise?Id=' + advertId).then(function (response) {
            if (response.data === "success") {
                alert("Aktiv edilidi !");
                $scope.onPageLoad();
            } else if (response.data === "alreadyActive") {
                alert("Ancaq 1 reklam ola bilər . Aktivləşdirmək üçün status aktiv olanı deaktiv edin!");
                return;
            } else {
                // Handle any other errors
                alert("An error occurred while making the ad active.");
            }
        }, function (error) {
            // Handle network errors or server errors
            alert("An error occurred while making the ad active.");
        });
    };
    $scope.deactivateAdvertise = function (advertId) {
        $http.post('/Admin/Advertising/DeactiveAdvertise?Id=' + advertId).then(function (response) {
            if (response.data === "success") {
                alert("Deaktiv edildi!");
                $scope.onPageLoad(); // Refresh the table after successful deactivation
            } else if (response.data === "already") {
                alert("Status deaktivdir !!!");
                return;
            } else {
                // Handle any other errors
                alert("An error occurred while deactivating the ad.");
            }
        }, function (error) {
            // Handle network errors or server errors
            alert("An error occurred while deactivating the ad.");
        });
    };
    $scope.getAbsoluteImageUrl = function (relativeImageUrl) {
        $scope.getAbsoluteImageUrl = function (relativeImageUrl) {
            var baseUrl = window.location.protocol + '//' + window.location.host;
            var absoluteUrl = baseUrl + relativeImageUrl;
        };
    };
    $scope.deleteAdvertise = function (advertId) {
        $http.delete('/Admin/Advertising/DeleteAdvertise?Id=' + advertId).then(function (response) {
            if (response.data === "success") {
                alert("Silindi !");
                $scope.onPageLoad();
            } else if (response.data === "notFound") {
                alert("Ad not found");
            } else {
                // Handle any other errors
                alert("An error occurred while deleting the ad.");
            }
        }, function (error) {
            // Handle network errors or server errors
            alert("An error occurred while deleting the ad.");
        });
    };
    $scope.getAdvertiseDetails = function (id) {
        $http.get('/Admin/Advertising/GetAdvertisementDetails?Id=' + id).then(function (response) {
            if (response.data !== 'notFound') {
                $scope.advertiseDetails = response.data;
                $scope.updateChartData();

                advertiseModal.show();
            } else {
                alert('Advertisement not found');
            }
        });
    };


    $scope.newAdvertiseClick = function () {
        showModal("advertiseModal");
    }
    $scope.closeNewAdvertiseClick = function () {
        hideModal("advertiseModal");
    }

    $scope.clearForm = function () {

        $scope.advert = {};
    };

    $scope.closeAdvertisementModal = function () {
        hideModal("advertiseModal");
    }
    //Advertise Details
    $scope.closeAdvertiseDetails = function () {
        advertiseModal.hide();
    };

    $scope.showAdvertiseDetailsModal = function () {
        advertiseModal.show();
    };
    $scope.updateChartData=function() {
        $scope.advertiseDetails = $scope.advertiseDetails || {
            clicks: 0,
            impressions: 0
            // ...
        };
        $scope.chartLabels = ['Klik sayı', 'Təəssüratlar'];
        $scope.chartSeries = ['Reklam məlumatları'];
        $scope.chartData = [
            [$scope.advertiseDetails.clicks || 0, $scope.advertiseDetails.impressions || 0]
        ];
    }

    function showModal(id) {
        $(`#${id}`).modal('show');
    }
    function hideModal(id) {
        $(`#${id}`).modal('hide');
    }
}]);

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);