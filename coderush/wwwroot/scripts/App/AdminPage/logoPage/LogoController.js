angModule.controller('logoController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    dataService.get('/api/LogoApi/GetLogo/').then(function (res) {
       
        $scope.logo=res.data.logo;       
    }).catch(function (err) {
        console.log(err.data)        
    });

    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/LogoApi/DelLogo/'+id;
        $scope.href='/logoes/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/Scripts/App/deleteModal/deleteModal.html',
            controller: 'deleteModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }
    
}]);
