angModule.controller('submenuController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    dataService.get('/api/SubmenuApi/GetSubmenu/').then(function (res) {

        $scope.submenu=res.data.submenu;
    }).catch(function (err) {
        console.log(err.data)
    })

    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/SubmenuApi/DelSubmenu/'+id;
        $scope.href='/Submenus/index';
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
