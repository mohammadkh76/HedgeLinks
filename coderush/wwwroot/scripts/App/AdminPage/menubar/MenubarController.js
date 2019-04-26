angModule.controller('menubarController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    dataService.get('/api/MenubarApi/GetMenubar/').then(function (res) {

        $scope.menubar=res.data.menubar;
    }).catch(function (err) {
        console.log(err.data)
    })

    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/MenubarApi/DelMenubar/'+id;
        $scope.href='/Menubars/index';
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
