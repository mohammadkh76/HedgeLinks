angModule.controller('addMenuPathController', ['$scope', 'dataService', 'Upload','$window', function ($scope, dataService, Upload,$window) {

    $scope.submit = function (data) {
        $scope.Description=$("#myTextArea").tinymce().getContent();
        debugger;
        $scope.data.Description=$scope.Description;
        console.log($scope.Description);
        dataService.post('/api/MenuPathApi/AddMenuPath/',data).then(function (res) {
            $window.location.href="/MenuPaths/Index";
        })
    }

}]);
