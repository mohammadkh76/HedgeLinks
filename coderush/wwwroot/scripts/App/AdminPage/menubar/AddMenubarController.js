angModule.controller('addMenubarController', ['$scope', 'dataService', 'Upload','$window', function ($scope, dataService, Upload,$window) {
    dataService.get("/api/MenubarApi/GetMenuPath").then(function (res) {
            $scope.menupath=res.data.menupath;        
    });
    $scope.submit = function (data) {
        dataService.post('/api/MenubarApi/AddMenubar/',data).then(function (res) {
            $window.location.href="/menubars/Index";
        })
    }
    $scope.pathDisable = function (data) {
        if (data!="") {
            $scope.data.Path = "";
            $scope.pathDisabled = true;

        } else {
            $scope.pathDisabled = false;
        }
    }

}]);
