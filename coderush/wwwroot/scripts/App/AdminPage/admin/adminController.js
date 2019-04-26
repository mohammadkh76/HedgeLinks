angModule.controller('adminController',['$scope','dataService','$uibModal',function ($scope,dataService,$uibModal) {
    dataService.get('/api/AdminApi/GetCount').then(function (res) {
        $scope.forsellcount=res.data.forsellcount;
        $scope.propertiescount=res.data.propertiescount;
    })
   
}]);