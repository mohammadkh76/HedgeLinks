angModule.controller('addSubmenuController', ['$scope', 'dataService', 'Upload','$window', function ($scope, dataService, Upload,$window) {
    dataService.get("/api/SubmenuApi/GetMenuPath").then(function (res) {
        $scope.menupath=res.data.menupath;
    });
    dataService.get("/api/SubmenuApi/GetMenubar").then(function (res) {
        $scope.menubar=res.data.menubar;
    });
    $scope.submit = function (data) {
        dataService.post('/api/SubmenuApi/AddSubmenu/',data).then(function (res) {
            $window.location.href="/Submenus/Index";
        })
    }

}]);
