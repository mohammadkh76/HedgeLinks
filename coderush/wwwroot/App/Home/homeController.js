adminModule.controller("homeController", ["$scope", "dataService", function ($scope, dataService) {
    dataService.get('api/TopImage/GetAllTopImage/').then(function (res) {
        $scope.topImage=res.data.Data
        console.log(res.data.Data);

    })
    dataService.get('api/CommercialTips/GetAllCommercialTips/').then(function (res) {
        $scope.commercialTips = res.data.Data
        console.log(res.data.Data);

    })
    dataService.get('api/JobIndustry/GetAllJobIndustry/').then(function (res) {
        $scope.jobIndustry = res.data.Data
        console.log(res.data.Data);

    })
    dataService.get('api/Job/GetAllJobCompensation').then(function (res) {
        $scope.compensation = res.data.Data;
    })

    
}]);
