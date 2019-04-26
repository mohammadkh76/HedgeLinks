angModule.controller('topImageController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    dataService.get('/api/TopImageApi/GetTopImage/').then(function (res) {

        $scope.topimage=res.data.topimage;
    }).catch(function (err) {
        console.log(err.data)
    })
    
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/TopImageApi/DelTopImage/'+id;
        $scope.href='/TopImages/index';
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
