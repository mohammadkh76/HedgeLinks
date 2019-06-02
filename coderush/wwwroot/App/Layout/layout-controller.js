adminModule.controller("layoutController", ["$scope", "dataService", function ($scope, dataService) {
    console.log('hello layout controller')
    dataService.get("/api/Menubar/GetAllMenubar/").then(function (res) {
        console.log(res.data.Data)
        $scope.menubar = res.data.Data;
    })
}]);