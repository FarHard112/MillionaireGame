var app = angular.module('yourApp', ['chart.js']);

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('httpInterceptor');
}]);

app.run(['$rootScope', function ($rootScope) {
    $rootScope.showSpinner = false;
    $rootScope.showOverlay = true;

    $rootScope.toggleSpinner = function (visible) {
        $rootScope.showSpinner = visible;
        $rootScope.showOverlay = visible;
    };
}]);

app.factory('httpInterceptor', ['$q', '$rootScope', function ($q, $rootScope) {
    return {
        request: function (config) {
            $rootScope.toggleSpinner(true);
            return config;
        },
        requestError: function (rejection) {
            $rootScope.toggleSpinner(false);
            return $q.reject(rejection);
        },
        response: function (response) {
            $rootScope.toggleSpinner(false);
            return response;
        },
        responseError: function (rejection) {
            $rootScope.toggleSpinner(false);
            return $q.reject(rejection);
        },
    };
}]);
