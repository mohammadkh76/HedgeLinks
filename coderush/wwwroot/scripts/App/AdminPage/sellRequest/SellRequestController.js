angModule.controller('sellRequestController',['$scope','dataService','$uibModal',function ($scope,dataService,$uibModal) {
    dataService.get('/api/SellRequestContactApi/getSellRequestContact').then(function (res) {
      $scope.sellRequestContact=res.data.sellRequestContact;
    })

    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/SellRequestContactApi/DelSellRequestContact/'+id;
        $scope.href='/SellRequest/index';
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