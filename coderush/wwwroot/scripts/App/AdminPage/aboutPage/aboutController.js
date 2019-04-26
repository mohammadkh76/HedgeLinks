angModule.controller('aboutController',['$scope','dataService','$uibModal',function ($scope,dataService,$uibModal) {
    dataService.get('/api/AboutApi/GetAbout').then(function (res) {
        $scope.about=res.data.about;
    })

    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/AboutApi/DelAbout/'+id;
        $scope.href='/AboutUs/index';
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