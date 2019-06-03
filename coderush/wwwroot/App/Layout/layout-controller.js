adminModule.controller("layoutController", ["$scope", "dataService", function ($scope, dataService) {
    console.log('hello layout controller')
    dataService.get("/api/Menubar/GetAllMenubar/").then(function (res) {
        console.log(res.data.Data)
        $scope.menubar = res.data.Data;
    })
    dataService.get('api/Job/GetAllJobViaCompany/').then(function (res) {
        $scope.jobCompany = res.data.Data;
    })

    dataService.get('api/Job/GetAllJobViaCity/').then(function (res) {
        $scope.jobCity = res.data.Data;
    })
    dataService.get('api/Job/GetAllJobViaTitle/').then(function (res) {
        $scope.jobTitle = res.data.Data;
    })


}]);