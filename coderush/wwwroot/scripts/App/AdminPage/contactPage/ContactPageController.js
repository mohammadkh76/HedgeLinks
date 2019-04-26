angModule.controller('contactPageController',['$scope','dataService','$uibModal',function ($scope,dataService,$uibModal) {
    dataService.get('/api/ContactApi/GetContact').then(function (res) {
        $scope.contact=res.data.contact;
    })
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/ContactApi/DelContactUs/'+id;
        $scope.href='/ContactUs/index';
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