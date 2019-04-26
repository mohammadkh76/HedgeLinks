angModule.controller('middleImageController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    dataService.get('/api/MiddleImageApi/GetMiddleImage/').then(function (res) {
        $scope.middleimage=res.data.middleimage;
    }).catch(function (err) {
        console.log(err.data)
    })
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/MiddleImageApi/DelMiddleImage/'+id;
        $scope.href='/MiddleImages/index';
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
