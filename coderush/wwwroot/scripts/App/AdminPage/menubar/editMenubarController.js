angModule.controller('editMenubarController', ['$scope', 'dataService', 'Upload', '$uibModal', '$window', function ($scope, dataService, Upload, $uibModal, $window) {
    $scope.pathDisabled = false;

    const urlPath = $window.location.pathname;
    var x = urlPath.split('/');
    dataService.get("/api/MenubarApi/GetMenuPath").then(function (res) {
        $scope.menupath = res.data.menupath;
    });
    dataService.get('/api/MenubarApi/GetEditMenubar/' + x[3]).then(function (res) {
        $scope.data = res.data.rec;

    }).catch(function (err) {
        console.log(err.data)
    });
    $scope.submit = function (data) {
        $scope.EMVM = {
            Id: x[3],
            Name: data.Name,
            Path: data.Path,
            SelectedPage: data.SelectedPage,
        };
        dataService.post('/api/MenubarApi/EditMenubar/', $scope.EMVM).then(function (res) {
            $window.location.href = '/Menubars/Index';
        })


    };
    $scope.pathDisable = function (data) {
        if (data!="") {
            $scope.data.Path = "";
            $scope.pathDisabled = true;

        } else {
            $scope.pathDisabled = false;
        }
    }
}]);