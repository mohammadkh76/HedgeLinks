angModule.controller('forSaleController', ['$scope', 'dataService', 'Upload','$window','$uibModal', function ($scope, dataService, Upload,$window,$uibModal) {
    console.log('in the forSaleController');
    dataService.get('/api/ForSaleApi/GetForSale/').then(function (res) {
        $scope.forSale = res.data.ForSale;
        
    })
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/ForSaleApi/DelForSale/'+id;
        $scope.href='/ForSales/index';
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
