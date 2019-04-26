angModule.controller('menuPathController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    
    dataService.get('/api/MenuPathApi/GetMenuPath/').then(function (res) {

        $scope.menupath=res.data.menupath;
    }).catch(function (err) {
        console.log(err.data)
    })
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/MenuPathApi/DelMenuPath/'+id;
        $scope.href='/MenuPaths/index';
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
