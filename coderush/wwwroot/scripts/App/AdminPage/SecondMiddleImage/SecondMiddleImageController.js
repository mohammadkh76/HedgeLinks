angModule.controller('secondMiddleImageController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    dataService.get('/api/SecondMiddlePageApi/GetSecondMiddleImage/').then(function (res) {
        $scope.secondMiddleImage=res.data.secondMiddleImage;
    }).catch(function (err) {
        console.log(err.data)
    })
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/SecondMiddlePageApi/DelSecondMiddleImage/'+id;
        $scope.href='/secondmiddlepages/index';
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
