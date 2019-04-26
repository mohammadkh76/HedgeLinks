angModule.controller('bottomSliderController',['$scope','dataService','encDec','$uibModal', function ($scope, dataService, encDec,$uibModal) {
    dataService.get('/api/BottomSliderApi/GetBottomSlider/').then(function (res) {
        $scope.bottomslider=res.data.bottomslider;
    }).catch(function (err) {
        console.log(err.data)
    })
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/BottomSliderApi/DelBottomSlider/'+id;
        $scope.href='/BottomSliders/index';
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
