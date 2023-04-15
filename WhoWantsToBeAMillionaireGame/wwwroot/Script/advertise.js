app.controller('advertiseController', ['$scope', '$http', function ($scope, $http) {
    $scope.adverts = [];
    var advertiseModal = new bootstrap.Modal(document.getElementById('advertiseDetails'));
    $scope.advert = {};
    $scope.socialMediaLinkDto = {
        Id: '',
        FacebookUrl: '',
        TikTokUrl: '',
        InstagramUrl: ''
    };

    $scope.onPageLoad = function () {
        $http.get('/Admin/Advertising/LoadGridData').then(function (response) {
            $scope.adverts = response.data.map(function (ad) {
                var baseUrl = window.location.protocol + '//' + window.location.host;
                ad.ImageUrl = baseUrl + ad.ImageUrl;
                return ad;
            });

        });
    };

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
            Swal.fire({
                icon: 'success',
                title: 'Ugurlu emeliyyat !'
            }).then(function () {
                location.reload();
            });
        }, function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Error uploading the image and submitting the form:'
            });
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
                Swal.fire({
                    icon: 'success',
                    title: 'Aktiv edilidi !'
                }).then(function () {
                    $scope.onPageLoad();
                });
            } else if (response.data === "alreadyActive") {
                Swal.fire({
                    icon: 'info',
                    title: 'Ancaq 1 reklam ola bilər . Aktivləşdirmək üçün status aktiv olanı deaktiv edin!'
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'An error occurred while making the ad active.'
                });
            }
        }, function (error) {
            Swal.fire({
                icon: 'error',
                title: 'An error occurred while making the ad active.'
            });
        });
    };
    $scope.deactivateAdvertise = function (advertId) {
        $http.post('/Admin/Advertising/DeactiveAdvertise?Id=' + advertId).then(function (response) {
            if (response.data === "success") {
                Swal.fire({
                    icon: 'success',
                    title: 'Deaktiv edildi!'
                }).then(function () {
                    $scope.onPageLoad();
                });
            } else if (response.data === "already") {
                Swal.fire({
                    icon: 'info',
                    title: 'Status deaktivdir !!!'
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'An error occurred while deactivating the ad.'
                });
            }
        }, function (error) {
            Swal.fire
                ({
                    icon: 'error',
                    title: 'An error occurred while deactivating the ad.'
                });
        });
    };
    $scope.deleteAdvertise = function (advertId) {
        $http.delete('/Admin/Advertising/DeleteAdvertise?Id=' + advertId).then(function (response) {
            if (response.data === "success") {
                Swal.fire({
                    icon: 'success',
                    title: 'Silindi !'
                }).then(function () {
                    $scope.onPageLoad();
                });
            } else if (response.data === "notFound") {
                Swal.fire({
                    icon: 'info',
                    title: 'Ad not found'
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'An error occurred while deleting the ad.'
                });
            }
        }, function (error) {
            Swal.fire({
                icon: 'error',
                title: 'An error occurred while deleting the ad.'
            });
        });
    };
    $scope.getAdvertiseDetails = function (id) {
        $http.get('/Admin/Advertising/GetAdvertisementDetails?Id=' + id).then(function (response) {
            if (response.data !== 'notFound') {
                $scope.advertiseDetails = response.data;
                $scope.updateChartData();

                advertiseModal.show();
            } else {
                Swal.fire({
                    icon: 'info',
                    title: 'Advertisement not found'
                });
            }
        });
    };
    $scope.saveSocialMediaLinks = function () {
        $scope.saveSocialMediaLinks = function () {
            var formData = new FormData();
            for (var property in $scope.socialMediaLinkDto) {
                if ($scope.socialMediaLinkDto.hasOwnProperty(property)) {
                    formData.append(property, $scope.socialMediaLinkDto[property]);
                }
            }
            $http({
                method: 'POST',
                url: '/Admin/Advertising/SaveSocialMediaLinks',
                data: formData,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            }).then(function (response) {
                if (response.data === "OK") {
                    Swal.fire({
                        icon: 'success',
                        title: 'Yadda saxlanıldı !'
                    });
                    $scope.closeSocialMediaModal();
                } else if (response.data === "notFound") {
                    console.log('Error: Social media links not found.');
                }
            }, function (error) {
                console.error('Error while saving social media links:', error);
            });
        };

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

    $scope.closeAdvertiseDetails = function () {
        advertiseModal.hide();
    };

    $scope.showAdvertiseDetailsModal = function () {
        advertiseModal.show();
    };
    $scope.updateChartData = function () {
        $scope.advertiseDetails = $scope.advertiseDetails || {
            clicks: 0,
            impressions: 0
        };
        $scope.chartLabels = ['Klik sayı', 'Təəssüratlar'];
        $scope.chartSeries = ['Reklam məlumatları'];
        $scope.chartData = [
            [$scope.advertiseDetails.clicks || 0, $scope.advertiseDetails.impressions || 0]
        ];
    }
    $scope.openSocialMediaModal = function () {
        $('#socialMediaModal').modal('show');
    };

    $scope.closeSocialMediaModal = function () {
        $('#socialMediaModal').modal('hide');
    };



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