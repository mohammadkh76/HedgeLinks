angModule.controller('aboutUsController',['$scope','dataService',function ($scope,dataService) {
    dataService.get('/api/HomeApi/GetAbout/').then(function (res) {
        $scope.data=res.data.about;
    });
}])
