angModule.controller('bottomTileController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    dataService.get('/api/BottomTileApi/GetBottomTile/').then(function (res) {
        $scope.bottomtile=res.data.bottomtile;
    }).catch(function (err) {
        console.log(err.data)
    })
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/BottomTileApi/DelBottomTile/'+id;
        $scope.href='/BottomTiles/index';
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
